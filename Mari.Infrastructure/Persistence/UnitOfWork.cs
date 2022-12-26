using Mari.Application.Common.Interfaces.Persistence;

namespace Mari.Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{

    private readonly MariDbContext _context;

    public UnitOfWork(MariDbContext context)
    {
        _context = context;
    }

    public async Task SaveChangesAsync(CancellationToken token = default)
    {
        await _context.SaveChangesAsync(token);
    }
}
