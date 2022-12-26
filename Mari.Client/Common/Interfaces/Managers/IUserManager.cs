using Mari.Client.Models.Users;

namespace Mari.Client.Common.Interfaces.Managers;

public interface IUserManager
{
    Task<IList<UserModel>> GetAll(CancellationToken token = default);
    Task Update(UserModel model, CancellationToken token = default);
}
