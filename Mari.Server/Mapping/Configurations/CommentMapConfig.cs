using Mapster;
using Mari.Application.Comments.Commands.CreateComment;
using Mari.Application.Comments.Commands.DeleteComment;
using Mari.Application.Comments.Commands.UpdateComment;
using Mari.Application.Comments.Queries.GetComments;
using Mari.Application.Comments.Queries.GetSystemComments;
using Mari.Contracts.Comments.DeleteRequest;
using Mari.Contracts.Comments.GetRequests;
using Mari.Contracts.Comments.PatchRequests;
using Mari.Contracts.Comments.PostRequests;
using Mari.Server.Mapping.Extensions;

namespace Mari.Server.Mapping.Configurations;

public class CommentMapConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.MapRequests<GetCommentsRequest.Route, GetCommentsQuery>();
        config.MapRequests<GetSystemCommentsRequest.Route, GetSystemCommentsQuery>();
        config.MapRequests<CreateCommentRequest.Body, CreateCommentCommand>();
        config.MapRequests<DeleteCommentRequest.Route, DeleteCommentCommand>();
        config.MapRequests<UpdateCommentRequest.Body, UpdateCommentCommand>();
    }
}
