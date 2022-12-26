using Mari.Contracts.Common.Routes.Server;
using Mari.Http.Common.Classes;
using Mari.Http.Models;
using Mari.Http.Requests;

namespace Mari.Contracts.Releases.DeleteRequests;

public class DeleteReleaseRequest : DeleteRequest<VoidResponse>
{
    private new Route RouteParams => (Route)base.RouteParams;

    public const string ConstRouteTemplate = $"{ServerRoutes.Controllers.Release}/delete/{{{nameof(Route.Id)}}}";
    public override string RouteTemplate => ConstRouteTemplate;

    public DeleteReleaseRequest(Route route) : base(route: route)
    {
    }

    public record Route(Guid Id) : RequestRoute;
}
