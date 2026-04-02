using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using BADBIR.UI.Components.Auth;

namespace BADBIR.UI.Components.Services.Api;

/// <summary>
/// Base class for API service implementations.
/// Provides helper methods for authenticated HTTP calls and consistent error handling.
/// </summary>
public abstract class ApiServiceBase
{
    protected readonly HttpClient Http;
    private readonly BabdirAuthStateProvider _authProvider;

    protected ApiServiceBase(HttpClient http, BabdirAuthStateProvider authProvider)
    {
        Http          = http;
        _authProvider = authProvider;
    }

    /// <summary>Attaches the current bearer token to the HTTP client.</summary>
    protected async Task AttachTokenAsync()
    {
        var token = await _authProvider.GetAccessTokenAsync();
        if (!string.IsNullOrEmpty(token))
            Http.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
    }

    protected async Task<ApiResult> PostAsync<TBody>(string url, TBody body)
    {
        await AttachTokenAsync();
        try
        {
            var response = await Http.PostAsJsonAsync(url, body);
            if (response.IsSuccessStatusCode) return ApiResult.Success();
            return ApiResult.Failure(await ExtractErrorAsync(response));
        }
        catch (Exception ex)
        {
            return ApiResult.Failure(FriendlyNetworkError(ex));
        }
    }

    protected async Task<ApiResult<T>> PostAsync<TBody, T>(string url, TBody body)
    {
        await AttachTokenAsync();
        try
        {
            var response = await Http.PostAsJsonAsync(url, body);
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadFromJsonAsync<T>();
                return ApiResult<T>.Success(data!);
            }
            return ApiResult<T>.Failure(await ExtractErrorAsync(response));
        }
        catch (Exception ex)
        {
            return ApiResult<T>.Failure(FriendlyNetworkError(ex));
        }
    }

    protected async Task<ApiResult<T>> GetAsync<T>(string url)
    {
        await AttachTokenAsync();
        try
        {
            var response = await Http.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadFromJsonAsync<T>();
                return ApiResult<T>.Success(data!);
            }
            return ApiResult<T>.Failure(await ExtractErrorAsync(response));
        }
        catch (Exception ex)
        {
            return ApiResult<T>.Failure(FriendlyNetworkError(ex));
        }
    }

    protected async Task<ApiResult> PatchAsync<TBody>(string url, TBody body)
    {
        await AttachTokenAsync();
        try
        {
            var response = await Http.PatchAsJsonAsync(url, body);
            if (response.IsSuccessStatusCode) return ApiResult.Success();
            return ApiResult.Failure(await ExtractErrorAsync(response));
        }
        catch (Exception ex)
        {
            return ApiResult.Failure(FriendlyNetworkError(ex));
        }
    }

    private static async Task<string> ExtractErrorAsync(HttpResponseMessage response)
    {
        if (response.StatusCode == HttpStatusCode.Unauthorized)
            return "Your session has expired. Please log in again.";

        if (response.StatusCode == HttpStatusCode.Forbidden)
            return "You do not have permission to perform this action.";

        try
        {
            var json = await response.Content.ReadFromJsonAsync<JsonElement>();

            if (json.TryGetProperty("error", out var errEl))
                return errEl.GetString() ?? "An unexpected error occurred.";

            if (json.TryGetProperty("errors", out var errsEl))
            {
                var messages = new List<string>();
                if (errsEl.ValueKind == JsonValueKind.Array)
                    foreach (var el in errsEl.EnumerateArray())
                        if (el.GetString() is { } s) messages.Add(s);
                return messages.Count > 0
                    ? string.Join(" ", messages)
                    : "Validation failed. Please check your input.";
            }
        }
        catch { /* fall through */ }

        return response.StatusCode switch
        {
            HttpStatusCode.NotFound         => "The requested resource was not found.",
            HttpStatusCode.Conflict         => "A conflict occurred. You may have already submitted this.",
            HttpStatusCode.BadRequest       => "Please check your input and try again.",
            HttpStatusCode.InternalServerError => "A server error occurred. Please try again later.",
            _                               => $"Unexpected error ({(int)response.StatusCode})."
        };
    }

    private static string FriendlyNetworkError(Exception ex) =>
        ex is HttpRequestException or TaskCanceledException
            ? "Unable to connect to the server. Please check your internet connection."
            : "An unexpected error occurred. Please try again.";
}
