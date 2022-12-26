using System.Net.Http.Json;
using Mari.Http.Common.Interfaces;
using Mari.Http.Models;

namespace Mari.Http.Common.Classes;

public abstract class Request<TResponse> : IRequest<TResponse>
    where TResponse : notnull
{
    protected Request(IRequestRoute? route = default, IRequestQuery? query = default, IRequestBody? body = default)
    {
        RouteParams = route ?? new EmptyRoute();
        QueryParams = query ?? new EmptyQuery();
        BodyContent = body ?? new EmptyBody();
    }

    public IRequestRoute RouteParams { get; set; }
    public IRequestQuery QueryParams { get; set; }
    public IRequestBody BodyContent { get; set; }

    public abstract string RouteTemplate { get; }

    public string GetRoute() => RouteParams.GetRouteString(RouteTemplate);
    public string GetQueryString() => QueryParams.GetQueryString();
    public JsonContent? GetBodyContent() => BodyContent.GetBody();
}
