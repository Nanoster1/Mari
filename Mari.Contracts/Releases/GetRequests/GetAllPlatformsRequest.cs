using Mari.Contracts.Releases.Responses;
using Mari.Http.Models;
using Mari.Http.Requests;
using Mari.Contracts.Common.Routes.Server;

namespace Mari.Contracts.Releases.GetRequests;

public class GetAllPlatformsRequest : GetRequest<IEnumerable<PlatformResponse>>
{
    public const string ConstRouteTemplate = $"{ServerRoutes.Controllers.Release}/get_all_platforms";
    public override string RouteTemplate => ConstRouteTemplate;
}
