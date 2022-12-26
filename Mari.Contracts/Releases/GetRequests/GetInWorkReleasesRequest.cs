using Mari.Contracts.Common.Routes.Server;
using Mari.Contracts.Releases.Responses;
using Mari.Http.Models;
using Mari.Http.Requests;

namespace Mari.Contracts.Releases.GetRequests;

public class GetInWorkReleasesRequest : GetRequest<IEnumerable<ReleaseResponse>>
{
    public const string ConstRouteTemplate = $"{ServerRoutes.Controllers.Release}/in_work";
    public override string RouteTemplate => ConstRouteTemplate;
}
