using ErrorOr;
using Mari.Application.Common.Interfaces.Persistence;
using Mari.Application.Releases.Results;
using MediatR;
using Mari.Domain.Common;

namespace Mari.Application.Releases.Queries.GetRelease;

internal class GetReleaseByIdQueryHandler : IRequestHandler<GetReleaseByIdQuery, ErrorOr<ReleaseResult>>
{
    private readonly IReleaseRepository _releaseRepository;

    public GetReleaseByIdQueryHandler(IReleaseRepository releaseRepository)
    {
        _releaseRepository = releaseRepository;
    }

    public async Task<ErrorOr<ReleaseResult>> Handle(GetReleaseByIdQuery request, CancellationToken cancellationToken)
    {
        var release = await _releaseRepository.GetById(request.ReleaseId, cancellationToken);
        if (release is null) return Errors.Release.ReleaseNotFound;
        return ReleaseResult.FromRelease(release);
    }
}
