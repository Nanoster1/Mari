using Mari.Contracts.Common.Routes.Server;
using Mari.Http.Common.Classes;
using Mari.Http.Models;
using Mari.Http.Requests;

namespace Mari.Contracts.Releases.PatchRequests;

public class SetInWorkStatusRequest : PatchRequest<VoidResponse>
{
    public const string ConstRouteTemplate = $"{ServerRoutes.Controllers.Release}/in_work/{{{nameof(Route.ReleaseId)}}}";
    public override string RouteTemplate => ConstRouteTemplate;

    public SetInWorkStatusRequest(Route route) : base(route: route)
    {
    }

    public record Route(Guid ReleaseId) : RequestRoute;
}
