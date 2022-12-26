using System.Net.Http.Json;

namespace Mari.Http.Common.Interfaces;

public interface IRequest<TResponse>
    where TResponse : notnull
{
    string GetRoute();
    string GetQueryString();
    JsonContent? GetBodyContent();
}
