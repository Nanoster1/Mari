using Mari.Application.Authentication.Results;
using Mari.Application.Common.Interfaces.Authentication;
using Mari.Application.Common.Interfaces.Persistence;
using MediatR;
using Mari.Domain.Common;
using ErrorOr;

namespace Mari.Application.Authentication.Queries.Login;

internal class LoginQueryHandler : IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public LoginQueryHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetById(request.UserId, cancellationToken);
        if (user is null) return Errors.User.UserNotFound;
        if (!user.IsActive) return Errors.User.UserIsBlocked;
        var token = _jwtTokenGenerator.GenerateToken(user);
        return new AuthenticationResult(token);
    }
}
