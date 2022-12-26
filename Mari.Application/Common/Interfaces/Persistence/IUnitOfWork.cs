namespace Mari.Application.Common.Interfaces.Persistence;

public interface IUnitOfWork
{
    Task SaveChangesAsync(CancellationToken token = default);
}
