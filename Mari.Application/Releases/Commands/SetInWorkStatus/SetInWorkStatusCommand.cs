using ErrorOr;
using Mari.Domain.Releases.ValueObjects;
using MediatR;

namespace Mari.Application.Releases.Commands.SetInWorkStatus;

public record SetInWorkStatusCommand : IRequest<ErrorOr<Updated>>
{
    public SetInWorkStatusCommand(Guid releaseId)
    {
        ReleaseId = ReleaseId.Create(releaseId);
    }

    public ReleaseId ReleaseId { get; }
}
