using Mari.Http.Common.Interfaces;

namespace Mari.Http.Models;

public record EmptyRoute : IRequestRoute
{
    public string GetRouteString(string routeTemplate) => routeTemplate;
}
