using System.Net.Http.Headers;
using Blazored.LocalStorage;

namespace Mari.Client.Common.Http.DelegatingHandlers;

public class ApiAuthorizationHeaderHandler : DelegatingHandler
{
    private readonly ILocalStorageService _localStorage;

    public ApiAuthorizationHeaderHandler(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var token = await _localStorage.GetItemAsync<string>(LocalStorageKeys.Authentication.Token);

        if (token is not null)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("bearer", token);
        }

        return await base.SendAsync(request, cancellationToken);
    }
}
