using ErrorOr;
using Mari.Application.Common.Interfaces.Persistence;
using Mari.Application.Common.Specifications;
using Mari.Application.Releases.Results;
using Mari.Domain.Releases.Enums;
using MediatR;

namespace Mari.Application.Releases.Queries.GetInWorkReleases;

internal class GetInWorkReleasesQueryHandler : IRequestHandler<GetInWorkReleasesQuery, ErrorOr<IList<ReleaseResult>>>
{
    private readonly IReleaseRepository _releaseRepository;

    public GetInWorkReleasesQueryHandler(IReleaseRepository releaseRepository)
    {
        _releaseRepository = releaseRepository;
    }

    public async Task<ErrorOr<IList<ReleaseResult>>> Handle(GetInWorkReleasesQuery request, CancellationToken cancellationToken)
    {
        var spec = ReleaseSpecs.StatusIn(
            ReleaseStatus.Developing,
            ReleaseStatus.Testing,
            ReleaseStatus.MarketModeration,
            ReleaseStatus.InPublicationProcess,
            ReleaseStatus.Paused,
            ReleaseStatus.PlannedDocumentApproval);

        return await _releaseRepository.FindMany(spec, request.Range)
            .Select(ReleaseResult.FromRelease)
            .ToListAsync(cancellationToken);
    }
}
