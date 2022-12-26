using MapsterMapper;
using Mari.Client.Common.Http.ProblemsHandling;
using Mari.Client.Common.Interfaces.Managers;
using Mari.Client.Models.Users;
using Mari.Contracts.Users.GetRequests;
using Mari.Contracts.Users.PutRequests;
using Mari.Http.Services;

namespace Mari.Client.Common.Services.Managers;

public class UserManager : IUserManager
{
    private readonly HttpSender _httpSender;
    private readonly ProblemHandler _problemHandler;
    private readonly IMapper _mapper;

    public UserManager(HttpSender httpSender, IMapper mapper, ProblemHandler problemHandler)
    {
        _httpSender = httpSender;
        _mapper = mapper;
        _problemHandler = problemHandler;
    }

    public async Task<IList<UserModel>> GetAll(CancellationToken token = default)
    {
        var request = new GetAllUsersRequests();
        var response = await _httpSender.GetAsync(request, token);
        if (!response.IsSuccess)
        {
            _problemHandler.HandleProblem(response.Problem);
            return new List<UserModel>();
        }
        return _mapper.Map<IList<UserModel>>(response.Response);
    }

    public async Task Update(UserModel model, CancellationToken token = default)
    {
        var body = _mapper.Map<UpdateUserRequest.Body>(model);
        var request = new UpdateUserRequest(body);
        var response = await _httpSender.PutAsync(request, token);
        if (!response.IsSuccess) _problemHandler.HandleProblem(response.Problem);
    }
}
