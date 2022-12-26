using Mari.Contracts.Common.Routes.Server;
using Mari.Http.Common.Classes;
using Mari.Http.Requests;

namespace Mari.Contracts.Users.PutRequests;

public class UpdateUserRequest: PutRequest
{
    public const string ConstRouteTemplate = $"{ServerRoutes.Controllers.User}/update_user";
    public override string RouteTemplate => ConstRouteTemplate;

    public UpdateUserRequest(Body body) : base(body: body)
    {
    }
    
    public record Body(
        int Id,
        string Username,
        string Role,
        List<string> Notifications,
        bool IsActive
    ):RequestBody;
}
