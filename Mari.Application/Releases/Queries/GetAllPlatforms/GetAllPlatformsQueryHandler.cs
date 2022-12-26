using ErrorOr;
using Mari.Application.Common.Interfaces.Persistence;
using Mari.Application.Common.Specifications;
using Mari.Application.Releases.Dto;
using Mari.Application.Releases.Results;
using Mari.Domain.Releases.ValueObjects;
using MediatR;

namespace Mari.Application.Releases.Queries.GetAllPlatforms;

internal class GetAllPlatformsQueryHandler : IRequestHandler<GetAllPlatformsQuery, ErrorOr<IList<PlatformResult>>>
{
    private readonly IReleaseRepository _releaseRepository;

    public GetAllPlatformsQueryHandler(IReleaseRepository releaseRepository)
    {
        _releaseRepository = releaseRepository;
    }

    public async Task<ErrorOr<IList<PlatformResult>>> Handle(GetAllPlatformsQuery request, CancellationToken token)
    {
        var platforms = await _releaseRepository.GetAllPlatforms().ToListAsync(token);
        var result = new List<PlatformResult>();

        foreach (var platform in platforms)
        {
            var platformSpec = ReleaseSpecs.PlatformIn(platform);
            var version = await _releaseRepository.GetMaxVersion(platformSpec, token) ?? ReleaseVersion.MinValue;
            result.Add(new PlatformResult(platform.Name, ReleaseVersionDto.FromVersion(version)));
        }

        return result;
    }
}
