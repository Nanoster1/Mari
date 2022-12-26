using ErrorOr;
using Mari.Domain.Releases.ValueObjects;
using MediatR;

namespace Mari.Application.Releases.Commands.CreateReleaseFromDraft;

public record CreateReleaseFromDraftCommand : IRequest<ErrorOr<Created>>
{
    public CreateReleaseFromDraftCommand(Guid releaseId)
    {
        ReleaseId = ReleaseId.Create(releaseId);
    }

    public ReleaseId ReleaseId { get; }
}
