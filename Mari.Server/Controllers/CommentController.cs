using MapsterMapper;
using Mari.Application.Comments.Commands.CreateComment;
using Mari.Application.Comments.Commands.DeleteComment;
using Mari.Application.Comments.Commands.UpdateComment;
using Mari.Application.Comments.Queries.GetComments;
using Mari.Application.Comments.Queries.GetSystemComments;
using Mari.Contracts.Comments.DeleteRequest;
using Mari.Contracts.Comments.GetRequests;
using Mari.Contracts.Comments.PatchRequests;
using Mari.Contracts.Comments.PostRequests;
using Mari.Contracts.Comments.Responces;
using Mari.Contracts.Common.Routes.Server;
using Mari.Server.Controllers.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Mari.Server.Controllers;

[Route(ServerRoutes.Controllers.Comment)]
public class CommentController : ApiController
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public CommentController(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    [HttpGet(GetCommentsRequest.ConstRouteTemplate)]
    public async Task<ActionResult<IEnumerable<CommentResponse>>> GetComments(
        [FromRoute] GetCommentsRequest.Route route,
        CancellationToken token)
    {
        var query = _mapper.Map<GetCommentsQuery>(route);

        var result = await _sender.Send(query, token);
        if (result.IsError) return Problem(result.Errors);

        var response = _mapper.Map<IEnumerable<CommentResponse>>(result.Value);
        return Ok(response);
    }

    [HttpGet(GetSystemCommentsRequest.ConstRouteTemplate)]
    public async Task<ActionResult<IEnumerable<CommentResponse>>> GetSystemComments(
        [FromRoute] GetSystemCommentsRequest.Route route)
    {
        var query = _mapper.Map<GetSystemCommentsQuery>(route);

        var result = await _sender.Send(query);
        if (result.IsError) return Problem(result.Errors);

        var response = _mapper.Map<IEnumerable<CommentResponse>>(result.Value);
        return Ok(response);
    }

    [HttpPost(CreateCommentRequest.ConstRouteTemplate)]
    public async Task<ActionResult> CreateComment(
        [FromBody] CreateCommentRequest.Body body)
    {
        var command = _mapper.Map<CreateCommentCommand>(body);

        var result = await _sender.Send(command);
        if (result.IsError) return Problem(result.Errors);

        return Ok();
    }

    [HttpDelete(DeleteCommentRequest.ConstRouteTemplate)]
    public async Task<ActionResult> DeleteComment(
        [FromRoute] DeleteCommentRequest.Route route)
    {
        var command = _mapper.Map<DeleteCommentCommand>(route);

        var result = await _sender.Send(command);
        if (result.IsError) return Problem(result.Errors);

        return Ok();
    }

    [HttpPatch(UpdateCommentRequest.ConstRouteTemplate)]
    public async Task<ActionResult> UpdateComment(
        [FromBody] UpdateCommentRequest.Body body)
    {
        var command = _mapper.Map<UpdateCommentCommand>(body);

        var result = await _sender.Send(command);
        if (result.IsError) return Problem(result.Errors);

        return Ok();
    }
}
