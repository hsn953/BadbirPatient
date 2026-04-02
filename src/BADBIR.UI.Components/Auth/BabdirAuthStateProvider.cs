using System.Security.Claims;
using System.Text.Json;
using BADBIR.Shared.DTOs;
using BADBIR.Shared.Enums;
using Microsoft.AspNetCore.Components.Authorization;

namespace BADBIR.UI.Components.Auth;

/// <summary>
/// Custom Blazor AuthenticationStateProvider that:
/// - Stores the JWT in browser sessionStorage (cleared on tab close)
/// - Restores auth state from sessionStorage on circuit reconnect
/// - Provides the current access token to HTTP service classes
/// </summary>
public class BabdirAuthStateProvider : AuthenticationStateProvider
{
    private const string TokenKey    = "badbir_access_token";
    private const string UserDataKey = "badbir_user_data";

    private readonly SessionStorageService _storage;
    private LoginResponseDto? _currentUser;

    public BabdirAuthStateProvider(SessionStorageService storage)
        => _storage = storage;

    // ─────────────────────────────────────────────────────────────────────────

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        // Try memory cache first (fast path within the circuit)
        if (_currentUser is not null && _currentUser.Expiry > DateTime.UtcNow)
            return BuildState(_currentUser);

        // Try restoring from sessionStorage (e.g. after Blazor reconnect)
        try
        {
            var tokenJson = await _storage.GetAsync(UserDataKey);
            if (!string.IsNullOrEmpty(tokenJson))
            {
                var dto = JsonSerializer.Deserialize<LoginResponseDto>(tokenJson);
                if (dto is not null && dto.Expiry > DateTime.UtcNow)
                {
                    _currentUser = dto;
                    return BuildState(_currentUser);
                }

                // Token expired — clear storage
                await ClearStorageAsync();
            }
        }
        catch
        {
            // JS interop may fail before the circuit is ready (prerender).
            // Return anonymous and the page will trigger auth check after render.
        }

        return BuildAnonymousState();
    }

    /// <summary>Called after a successful login to update auth state.</summary>
    public async Task NotifyLoginAsync(LoginResponseDto dto)
    {
        _currentUser = dto;
        var json = JsonSerializer.Serialize(dto);
        await _storage.SetAsync(UserDataKey, json);
        NotifyAuthenticationStateChanged(Task.FromResult(BuildState(dto)));
    }

    /// <summary>Called on logout to clear auth state.</summary>
    public async Task NotifyLogoutAsync()
    {
        _currentUser = null;
        await ClearStorageAsync();
        NotifyAuthenticationStateChanged(Task.FromResult(BuildAnonymousState()));
    }

    /// <summary>Returns the raw access token, or null if not authenticated.</summary>
    public Task<string?> GetAccessTokenAsync()
        => Task.FromResult(_currentUser?.AccessToken);

    /// <summary>Returns the current user DTO, or null if not authenticated.</summary>
    public LoginResponseDto? CurrentUser => _currentUser;

    // ─────────────────────────────────────────────────────────────────────────

    private async Task ClearStorageAsync()
    {
        try
        {
            await _storage.RemoveAsync(UserDataKey);
        }
        catch { /* interop may not be available during disposal */ }
    }

    private static AuthenticationState BuildState(LoginResponseDto dto)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, dto.UserId),
            new(ClaimTypes.Email,          dto.Email),
            new(ClaimTypes.Name,           dto.Email)
        };
        claims.AddRange(dto.Roles.Select(r => new Claim(ClaimTypes.Role, r)));

        var identity  = new ClaimsIdentity(claims, "jwt");
        var principal = new ClaimsPrincipal(identity);
        return new AuthenticationState(principal);
    }

    private static AuthenticationState BuildAnonymousState()
        => new(new ClaimsPrincipal(new ClaimsIdentity()));
}
