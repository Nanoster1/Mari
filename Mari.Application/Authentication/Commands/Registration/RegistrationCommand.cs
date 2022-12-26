using ErrorOr;
using Mari.Application.Authentication.Results;
using Mari.Domain.Users.ValueObjects;
using MediatR;

namespace Mari.Application.Authentication.Commands.Registration;

public record RegistrationCommand : IRequest<ErrorOr<AuthenticationResult>>
{
    public RegistrationCommand(string userId, string username)
    {
        UserId = UserId.Create(userId);
        Username = Username.Create(username);
    }

    public UserId UserId { get; }
    public Username Username { get; }
}
