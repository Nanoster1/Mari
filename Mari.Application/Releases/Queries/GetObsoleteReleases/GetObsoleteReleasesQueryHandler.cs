using ErrorOr;
using Mari.Application.Common.Interfaces.Persistence;
using Mari.Application.Releases.Results;
using MediatR;

namespace Mari.Application.Releases.Queries.GetObsoleteReleases;

public class GetObsoleteReleasesQueryHandler : IRequestHandler<GetObsoleteReleasesQuery, ErrorOr<IList<ReleaseResult>>>
{
    private readonly IReleaseRepository _releaseRepository;

    public GetObsoleteReleasesQueryHandler(IReleaseRepository releaseRepository)
    {
        _releaseRepository = releaseRepository;
    }

    public async Task<ErrorOr<IList<ReleaseResult>>> Handle(GetObsoleteReleasesQuery request, CancellationToken cancellationToken)
    {
        var releases = _releaseRepository.GetObsoleteReleases(request.Range);
        return await releases.Select(ReleaseResult.FromRelease)
            .ToListAsync(cancellationToken);
    }
}
