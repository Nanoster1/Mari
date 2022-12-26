using ErrorOr;
using Mari.Application.Releases.Results;
using MediatR;

namespace Mari.Application.Releases.Queries.GetAllPlatforms;

public record GetAllPlatformsQuery : IRequest<ErrorOr<IList<PlatformResult>>>;
