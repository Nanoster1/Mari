using MapsterMapper;
using Mari.Client.Common.Http.ProblemsHandling;
using Mari.Client.Common.Interfaces.Managers;
using Mari.Client.Models.Comments;
using Mari.Contracts.Comments.DeleteRequest;
using Mari.Contracts.Comments.GetRequests;
using Mari.Contracts.Comments.PatchRequests;
using Mari.Contracts.Comments.PostRequests;
using Mari.Http.Services;

namespace Mari.Client.Common.Services.Managers;

public class CommentManager : ICommentManager
{
    private readonly HttpSender _httpSender;
    private readonly ProblemHandler _problemHandler;
    private readonly IMapper _mapper;

    public CommentManager(HttpSender httpSender, IMapper mapper, ProblemHandler problemHandler)
    {
        _httpSender = httpSender;
        _mapper = mapper;
        _problemHandler = problemHandler;
    }

    public async Task Create(CommentModel comment, CancellationToken token = default)
    {
        var body = _mapper.Map<CreateCommentRequest.Body>(comment);
        var request = new CreateCommentRequest(body);
        var response = await _httpSender.PostAsync(request, token);
        if (!response.IsSuccess) _problemHandler.HandleProblem(response.Problem);
    }

    public async Task<IList<CommentModel>> GetAllUserComment(Guid releaseId, CancellationToken token = default)
    {
        var route = new GetCommentsRequest.Route(releaseId);
        var request = new GetCommentsRequest(route);
        var response = await _httpSender.GetAsync(request, token);
        if (!response.IsSuccess)
        {
            _problemHandler.HandleProblem(response.Problem);
            //return new List<CommentModel>();
        }

        return _mapper.Map<IList<CommentModel>>(response.Response);
    }

    public async Task<IList<CommentModel>> GetAllSystemComment(Guid releaseId, CancellationToken token = default)
    {
        var route = new GetSystemCommentsRequest.Route(releaseId);
        var request = new GetSystemCommentsRequest(route);
        var response = await _httpSender.GetAsync(request, token);
        if (!response.IsSuccess)
        {
            _problemHandler.HandleProblem(response.Problem);
            return new List<CommentModel>();
        }

        return _mapper.Map<IList<CommentModel>>(response.Response);
    }

    public async Task UpdateComment(CommentModel comment, CancellationToken token = default)
    {
        var body = _mapper.Map<UpdateCommentRequest.Body>(comment);
        var request = new UpdateCommentRequest(body);
        var response = await _httpSender.PatchAsync(request, token);
        if (!response.IsSuccess) _problemHandler.HandleProblem(response.Problem);
    }

    public async Task DeleteComment(Guid commnetId, CancellationToken token = default)
    {
        var route = new DeleteCommentRequest.Route(commnetId);
        var request = new DeleteCommentRequest(route);
        var response = await _httpSender.DeleteAsync(request, token);
        if (!response.IsSuccess) _problemHandler.HandleProblem(response.Problem);
    }
}

