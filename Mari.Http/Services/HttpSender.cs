using System.Net.Http.Json;
using Mari.Http.Common.Interfaces;
using Mari.Http.Models;
using Mari.Http.Requests;
using Throw;

namespace Mari.Http.Services;

public class HttpSender
{
    private readonly IHttpClientFactory _httpClientFactory;

    private string? clientName;

    public HttpSender(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public HttpClient Client { get; set; } = null!;

    public string? HttpClientName
    {
        get => clientName;
        set => Client = string.IsNullOrWhiteSpace(clientName = value)
            ? _httpClientFactory.CreateClient()
            : _httpClientFactory.CreateClient(clientName);
    }

    public async Task<ProblemOr<TResponse>> GetAsync<TResponse>(
        GetRequest<TResponse> request,
        CancellationToken cancellationToken = default)
        where TResponse : notnull
    {
        var absoluteRoute = CreateUri(request, Client.BaseAddress);
        var response = await Client.GetAsync(absoluteRoute, cancellationToken);
        return await HandleHttpResponse<TResponse>(response, cancellationToken);
    }

    public async Task<ProblemOr<TResponse>> PostAsync<TResponse>(
        PostRequest<TResponse> request,
        CancellationToken cancellationToken = default)
        where TResponse : notnull
    {
        var absoluteRoute = CreateUri(request, Client.BaseAddress);
        var response = await Client.PostAsync(absoluteRoute, request.BodyContent.GetBody(), cancellationToken);
        return await HandleHttpResponse<TResponse>(response, cancellationToken);
    }

    public async Task<ProblemOr<VoidResponse>> PutAsync(
        PutRequest request,
        CancellationToken cancellationToken = default)
    {
        var absoluteRoute = CreateUri(request, Client.BaseAddress);
        var response = await Client.PutAsync(absoluteRoute, request.BodyContent.GetBody(), cancellationToken);
        return await HandleHttpResponse<VoidResponse>(response, cancellationToken);
    }

    public async Task<ProblemOr<TResponse>> DeleteAsync<TResponse>(
        DeleteRequest<TResponse> request,
        CancellationToken cancellationToken = default)
        where TResponse : notnull
    {
        var absoluteRoute = CreateUri(request, Client.BaseAddress);
        var response = await Client.DeleteAsync(absoluteRoute, cancellationToken);
        return await HandleHttpResponse<TResponse>(response, cancellationToken);
    }

    public async Task<ProblemOr<TResponse>> PatchAsync<TResponse>(
        PatchRequest<TResponse> request,
        CancellationToken cancellationToken = default)
        where TResponse : notnull
    {
        var absoluteRoute = CreateUri(request, Client.BaseAddress);
        var response = await Client.PatchAsync(absoluteRoute, request.BodyContent.GetBody(), cancellationToken);
        return await HandleHttpResponse<TResponse>(response, cancellationToken);
    }

    private async Task<ProblemOr<TResponse>> HandleHttpResponse<TResponse>(
        HttpResponseMessage httpResponse,
        CancellationToken cancellationToken)
        where TResponse : notnull
    {
        if (httpResponse.IsSuccessStatusCode)
        {
            if (typeof(TResponse) == typeof(VoidResponse))
            {
                return new(
                    httpResponse: httpResponse
                );
            }

            var response = await httpResponse.Content.ReadFromJsonAsync<TResponse>(cancellationToken: cancellationToken);
            response.ThrowIfNull();
            return new(
                httpResponse: httpResponse,
                response: response);
        }
        else
        {
            var problem = await httpResponse.Content.ReadFromJsonAsync<ProblemDetails>(cancellationToken: cancellationToken);
            problem.ThrowIfNull();
            return new(
                httpResponse: httpResponse,
                problem: problem);
        }
    }

    private Uri CreateUri<TResponse>(IRequest<TResponse> request, Uri? baseAddress = null)
        where TResponse : notnull
    {
        var routeUri = new Uri(request.GetRoute(), UriKind.RelativeOrAbsolute);
        var builderUri = routeUri.IsAbsoluteUri ? routeUri : new Uri(baseAddress!, routeUri);
        var builder = new UriBuilder(builderUri);
        builder.Query = request.GetQueryString();
        return builder.Uri;
    }
}
