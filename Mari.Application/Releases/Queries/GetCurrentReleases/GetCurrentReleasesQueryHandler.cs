using ErrorOr;
using Mari.Application.Common.Interfaces.Persistence;
using Mari.Application.Releases.Results;
using MediatR;

namespace Mari.Application.Releases.Queries.GetCurrentReleases;

internal class GetCurrentReleasesQueryHandler : IRequestHandler<GetCurrentReleasesQuery, ErrorOr<IList<ReleaseResult>>>
{
    private readonly IReleaseRepository _releaseRepository;

    public GetCurrentReleasesQueryHandler(IReleaseRepository releaseRepository)
    {
        _releaseRepository = releaseRepository;
    }

    public async Task<ErrorOr<IList<ReleaseResult>>> Handle(GetCurrentReleasesQuery request, CancellationToken token)
    {
        var currentReleases = _releaseRepository.GetCurrentReleases(request.Range);
        return await currentReleases.Select(ReleaseResult.FromRelease)
            .ToListAsync();
    }
}
