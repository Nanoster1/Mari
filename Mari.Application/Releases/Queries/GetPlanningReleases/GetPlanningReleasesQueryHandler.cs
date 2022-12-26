using ErrorOr;
using Mari.Application.Common.Interfaces.Persistence;
using Mari.Application.Common.Specifications;
using Mari.Application.Releases.Results;
using Mari.Domain.Releases.Enums;
using MediatR;

namespace Mari.Application.Releases.Queries.GetPlannedReleases;

internal class GetPlannedReleasesQueryHandler : IRequestHandler<GetPlannedReleasesQuery, ErrorOr<IList<ReleaseResult>>>
{
    private readonly IReleaseRepository _releaseRepository;

    public GetPlannedReleasesQueryHandler(IReleaseRepository releaseRepository)
    {
        _releaseRepository = releaseRepository;
    }

    public async Task<ErrorOr<IList<ReleaseResult>>> Handle(GetPlannedReleasesQuery request, CancellationToken cancellationToken)
    {
        var spec = ReleaseSpecs.StatusIn(ReleaseStatus.Planning);
        var releases = await _releaseRepository.FindMany(spec, request.Range)
            .ToListAsync(cancellationToken);
        return releases.Select(release => ReleaseResult.FromRelease(release))
            .ToList();
    }
}
