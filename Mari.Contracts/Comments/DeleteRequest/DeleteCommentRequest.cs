using Mari.Contracts.Common.Routes.Server;
using Mari.Http.Common.Classes;
using Mari.Http.Models;
using Mari.Http.Requests;

namespace Mari.Contracts.Comments.DeleteRequest;

public class DeleteCommentRequest : DeleteRequest<VoidResponse>
{
    private new Route RouteParams => (Route)base.RouteParams;
    
    public const string ConstRouteTemplate = $"{ServerRoutes.Controllers.Comment}/delete/{{{nameof(Route.Id)}}}";
    public override string RouteTemplate => ConstRouteTemplate;

    public DeleteCommentRequest(Route route) : base(route:route)
    {
    }

    public record Route(Guid Id) : RequestRoute;
}
