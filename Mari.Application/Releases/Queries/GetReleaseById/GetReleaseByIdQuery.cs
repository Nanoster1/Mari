using ErrorOr;
using Mari.Application.Releases.Results;
using Mari.Domain.Releases.ValueObjects;
using MediatR;

namespace Mari.Application.Releases.Queries.GetRelease;

public record GetReleaseByIdQuery : IRequest<ErrorOr<ReleaseResult>>
{
    public GetReleaseByIdQuery(Guid id)
    {
        ReleaseId = ReleaseId.Create(id);
    }

    public ReleaseId ReleaseId { get; set; }
}
