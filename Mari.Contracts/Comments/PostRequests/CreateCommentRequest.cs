using Mari.Contracts.Common.Routes.Server;
using Mari.Http.Common.Classes;
using Mari.Http.Models;
using Mari.Http.Requests;

namespace Mari.Contracts.Comments.PostRequests;

public class CreateCommentRequest : PostRequest<VoidResponse>
{
    public const string ConstRouteTemplate = $"{ServerRoutes.Controllers.Comment}/create_comment";
    public override string RouteTemplate => ConstRouteTemplate;

    public CreateCommentRequest(Body body) : base(body: body)
    {
    }

    public record Body(
        Guid ReleaseId,
        int UserId,
        string Content)
        : RequestBody;
}
