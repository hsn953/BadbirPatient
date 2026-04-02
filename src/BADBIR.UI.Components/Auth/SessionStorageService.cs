using Microsoft.JSInterop;

namespace BADBIR.UI.Components.Auth;

/// <summary>
/// Thin wrapper around the browser's sessionStorage via JS interop.
/// In Blazor Server the JS interop call happens over the SignalR circuit.
/// </summary>
public class SessionStorageService
{
    private readonly IJSRuntime _js;

    public SessionStorageService(IJSRuntime js) => _js = js;

    public async Task SetAsync(string key, string value) =>
        await _js.InvokeVoidAsync("sessionStorage.setItem", key, value);

    public async Task<string?> GetAsync(string key) =>
        await _js.InvokeAsync<string?>("sessionStorage.getItem", key);

    public async Task RemoveAsync(string key) =>
        await _js.InvokeVoidAsync("sessionStorage.removeItem", key);
}
