using ErrorOr;
using Mari.Application.Authentication.Results;
using Mari.Domain.Users.ValueObjects;
using MediatR;

namespace Mari.Application.Authentication.Queries.Login;

public record LoginQuery : IRequest<ErrorOr<AuthenticationResult>>
{
    public LoginQuery(string userId)
    {
        UserId = UserId.Create(userId);
    }

    public UserId UserId { get; }
}
