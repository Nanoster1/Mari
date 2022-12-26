using MapsterMapper;
using Mari.Client.Common.Http.ProblemsHandling;
using Mari.Client.Common.Interfaces.Managers;
using Mari.Client.Models.Releases;
using Mari.Contracts.Releases.DeleteRequests;
using Mari.Contracts.Releases.GetRequests;
using Mari.Contracts.Releases.PatchRequests;
using Mari.Contracts.Releases.PostRequests;
using Mari.Contracts.Releases.PutRequests;
using Mari.Http.Services;


namespace Mari.Client.Common.Services.Managers;

public class ReleaseManager : IReleaseManager
{
    private readonly HttpSender _httpSender;
    private readonly ProblemHandler _problemHandler;
    private readonly IMapper _mapper;

    public ReleaseManager(HttpSender httpSender, IMapper mapper, ProblemHandler problemHandler)
    {
        _httpSender = httpSender;
        _mapper = mapper;
        _problemHandler = problemHandler;
    }

    public async Task Create(ReleaseModel release, CancellationToken token = default)
    {
        var body = _mapper.Map<CreateReleaseRequest.Body>(release);
        var request = new CreateReleaseRequest(body);
        var response = await _httpSender.PostAsync(request, token);
        if (!response.IsSuccess) _problemHandler.HandleProblem(response.Problem);
    }

    public async Task CreateDraft(ReleaseModel release, CancellationToken token = default)
    {
        var body = _mapper.Map<CreateDraftReleaseRequest.Body>(release);
        var request = new CreateDraftReleaseRequest(body);
        var response = await _httpSender.PostAsync(request, token);
        if (!response.IsSuccess) _problemHandler.HandleProblem(response.Problem);
    }

    public async Task CreateFromDraft(ReleaseModel release, CancellationToken token = default)
    {
        var route = _mapper.Map<CreateReleaseFromDraftRequest.Route>(release);
        var request = new CreateReleaseFromDraftRequest(route);
        var response = await _httpSender.PostAsync(request, token);
        if (!response.IsSuccess) _problemHandler.HandleProblem(response.Problem);
    }

    public async Task Delete(Guid id, CancellationToken token = default)
    {
        var route = new DeleteReleaseRequest.Route(id);
        var request = new DeleteReleaseRequest(route);
        var response = await _httpSender.DeleteAsync(request, token);
        if (!response.IsSuccess) _problemHandler.HandleProblem(response.Problem);
    }

    public async Task<IList<PlatformModel>> GetAllPlatforms(CancellationToken token = default)
    {
        var request = new GetAllPlatformsRequest();
        var response = await _httpSender.GetAsync(request, token);
        if (!response.IsSuccess)
        {
            _problemHandler.HandleProblem(response.Problem);
            return new List<PlatformModel>();
        }
        return _mapper.Map<IList<PlatformModel>>(response.Response);
    }

    public async Task<ReleaseModel?> GetById(Guid id, CancellationToken token = default)
    {
        var route = new GetReleaseByIdRequest.Route(id);
        var request = new GetReleaseByIdRequest(route);
        var response = await _httpSender.GetAsync(request, token);
        if (!response.IsSuccess)
        {
            _problemHandler.HandleProblem(response.Problem);
            return null;
        }
        return _mapper.Map<ReleaseModel>(response.Response);
    }

    public async Task<IList<ReleaseModel>> GetCurrent(CancellationToken token = default)
    {
        var request = new GetCurrentReleasesRequest();
        var response = await _httpSender.GetAsync(request, token);
        if (!response.IsSuccess)
        {
            _problemHandler.HandleProblem(response.Problem);
            return new List<ReleaseModel>();
        }
        return _mapper.Map<IList<ReleaseModel>>(response.Response);
    }

    public async Task<IList<ReleaseModel>> GetObsolete(CancellationToken token = default)
    {
        var request = new GetObsoleteReleasesRequest();
        var response = await _httpSender.GetAsync(request, token);
        if (!response.IsSuccess)
        {
            _problemHandler.HandleProblem(response.Problem);
            return new List<ReleaseModel>();
        }
        return _mapper.Map<IList<ReleaseModel>>(response.Response);
    }

    public async Task<IList<ReleaseModel>> GetInWork(CancellationToken token = default)
    {
        var request = new GetInWorkReleasesRequest();
        var response = await _httpSender.GetAsync(request, token);
        if (!response.IsSuccess)
        {
            _problemHandler.HandleProblem(response.Problem);
            return new List<ReleaseModel>();
        }
        return _mapper.Map<IList<ReleaseModel>>(response.Response);
    }

    public async Task<IList<ReleaseModel>> GetPlanned(CancellationToken token = default)
    {
        var request = new GetPlannedReleasesRequest();
        var response = await _httpSender.GetAsync(request, token);
        if (!response.IsSuccess)
        {
            _problemHandler.HandleProblem(response.Problem);
            return new List<ReleaseModel>();
        }
        return _mapper.Map<IList<ReleaseModel>>(response.Response);
    }

    public async Task Update(ReleaseModel model, CancellationToken token = default)
    {
        var body = _mapper.Map<UpdateReleaseRequest.Body>(model);
        var request = new UpdateReleaseRequest(body);
        var response = await _httpSender.PutAsync(request, token);
        if (!response.IsSuccess) _problemHandler.HandleProblem(response.Problem);
    }

    public async Task SetCompleteStatus(Guid id, CancellationToken cancellationToken = default)
    {
        var route = new SetCompleteStatusRequest.Route(id);
        var request = new SetCompleteStatusRequest(route);
        var response = await _httpSender.PatchAsync(request, cancellationToken);
        if (!response.IsSuccess) _problemHandler.HandleProblem(response.Problem);
    }

    public async Task SetInWorkStatus(Guid id, CancellationToken cancellationToken = default)
    {
        var route = new SetInWorkStatusRequest.Route(id);
        var request = new SetInWorkStatusRequest(route);
        var response = await _httpSender.PatchAsync(request, cancellationToken);
        if (!response.IsSuccess) _problemHandler.HandleProblem(response.Problem);
    }

    public async Task UpdateDraft(ReleaseModel model, CancellationToken token = default)
    {
        var body = _mapper.Map<UpdateDraftReleaseRequest.Body>(model);
        var request = new UpdateDraftReleaseRequest(body);
        var response = await _httpSender.PutAsync(request, token);
        if (!response.IsSuccess) _problemHandler.HandleProblem(response.Problem);
    }
}
