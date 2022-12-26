using Mari.Contracts.Common.Routes.Server;
using Mari.Contracts.Users.Responce;
using Mari.Http.Requests;

namespace Mari.Contracts.Users.GetRequests;

public class GetAllUsersRequests: GetRequest<IEnumerable<UserResponce>>
{

    public const string ConstRouteTemplate = $"{ServerRoutes.Controllers.User}/get_all_users";
    public override string RouteTemplate => ConstRouteTemplate;
 
}
