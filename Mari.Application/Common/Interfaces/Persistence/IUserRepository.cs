using Mari.Application.Common.Interfaces.Persistence.Shared;
using Mari.Domain.Users;
using Mari.Domain.Users.ValueObjects;

namespace Mari.Application.Common.Interfaces.Persistence;

public interface IUserRepository : IRepository<User, UserId>
{
}
