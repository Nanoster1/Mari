using Mari.Client.Models.Users;

namespace Mari.Client.Common.Interfaces.Managers;

public interface ICurrentUserManager
{
    Task<CurrentUserModel?> Get();
}
