using Mari.Application.Common.Interfaces.Persistence;
using Mari.Domain.Users;
using Mari.Domain.Users.ValueObjects;
using Mari.Infrastructure.Persistence.Shared;

namespace Mari.Infrastructure.Persistence.Repositories;

public class UserRepository : Repository<User, UserId>, IUserRepository
{
    public UserRepository(MariDbContext context) : base(context)
    {
    }
}
