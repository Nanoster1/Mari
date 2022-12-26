using Mari.Contracts.Common.Routes.Server;
using Mari.Contracts.Releases.Responses;
using Mari.Http.Common.Classes;
using Mari.Http.Requests;

namespace Mari.Contracts.Releases.GetRequests;

public class GetReleaseByIdRequest : GetRequest<ReleaseResponse>
{
    public const string ConstRouteTemplate = $"{ServerRoutes.Controllers.Release}/{{{nameof(Route.Id)}}}";
    public override string RouteTemplate => ConstRouteTemplate;

    public GetReleaseByIdRequest(Route route) : base(route: route)
    {
    }

    public record Route(Guid Id) : RequestRoute;
}
