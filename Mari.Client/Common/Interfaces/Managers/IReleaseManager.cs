using Mari.Client.Models.Releases;

namespace Mari.Client.Common.Interfaces.Managers;

public interface IReleaseManager
{
    Task Create(ReleaseModel release, CancellationToken token = default);
    Task CreateDraft(ReleaseModel release, CancellationToken token = default);
    Task CreateFromDraft(ReleaseModel release, CancellationToken token = default);
    Task<ReleaseModel?> GetById(Guid id, CancellationToken token = default);
    Task<IList<ReleaseModel>> GetCurrent(CancellationToken token = default);
    Task<IList<ReleaseModel>> GetObsolete(CancellationToken token = default);
    Task<IList<ReleaseModel>> GetPlanned(CancellationToken token = default);
    Task<IList<ReleaseModel>> GetInWork(CancellationToken token = default);
    Task<IList<PlatformModel>> GetAllPlatforms(CancellationToken token = default);
    Task Update(ReleaseModel model, CancellationToken token = default);
    Task UpdateDraft(ReleaseModel model, CancellationToken token = default);
    Task Delete(Guid id, CancellationToken token = default);
    Task SetCompleteStatus(Guid id, CancellationToken token = default);
    Task SetInWorkStatus(Guid id, CancellationToken token = default);
}
