using ErrorOr;
using Mari.Domain.Releases.ValueObjects;
using MediatR;

namespace Mari.Application.Releases.Commands.SetCompleteStatus;

public class SetCompleteStatusCommand : IRequest<ErrorOr<Updated>>
{
    public SetCompleteStatusCommand(Guid releaseId)
    {
        ReleaseId = ReleaseId.Create(releaseId);
    }

    public ReleaseId ReleaseId { get; }
}
