using ErrorOr;
using Mari.Application.Common.Interfaces.Persistence;
using MediatR;

namespace Mari.Application.Users.Queries.Exists;

internal class UserExistsQueryHandler : IRequestHandler<UserExistsQuery, ErrorOr<bool>>
{
    private readonly IUserRepository _userRepository;

    public UserExistsQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<bool>> Handle(UserExistsQuery request, CancellationToken cancellationToken)
    {
        return await _userRepository.Exists(request.UserId, cancellationToken);
    }
}
