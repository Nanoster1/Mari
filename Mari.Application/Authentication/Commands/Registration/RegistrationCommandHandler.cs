using ErrorOr;
using Mari.Application.Authentication.Results;
using Mari.Application.Common.Interfaces.Persistence;
using MediatR;
using Mari.Domain.Common;
using Mari.Domain.Users;
using Mari.Application.Common.Interfaces.Authentication;

namespace Mari.Application.Authentication.Commands.Registration;

internal class RegistrationCommandHandler : IRequestHandler<RegistrationCommand, ErrorOr<AuthenticationResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUnitOfWork _unitOfWork;

    public RegistrationCommandHandler(
        IUserRepository userRepository,
        IJwtTokenGenerator jwtTokenGenerator,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(RegistrationCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetById(request.UserId, cancellationToken);
        if (user is not null) return Errors.User.UserAlreadyExists;

        var userCreateResult = User.Create(username: request.Username);
        if (userCreateResult.IsError) return userCreateResult.Errors;

        user = userCreateResult.Value;
        user.SetId(request.UserId);
        user = await _userRepository.Insert(user, cancellationToken);

        var unblockResult = user.UnblockUser();
        if (unblockResult.IsError) return unblockResult.Errors;

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        var token = _jwtTokenGenerator.GenerateToken(user);
        return new AuthenticationResult(token);
    }
}
