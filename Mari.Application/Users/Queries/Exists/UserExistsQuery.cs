using ErrorOr;
using Mari.Domain.Users.ValueObjects;
using MediatR;

namespace Mari.Application.Users.Queries.Exists;

public record UserExistsQuery : IRequest<ErrorOr<bool>>
{
    public UserExistsQuery(string userId)
    {
        UserId = UserId.Create(userId);
    }

    public UserId UserId { get; }
}
