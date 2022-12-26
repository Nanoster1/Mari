using ErrorOr;
using Mari.Domain.Releases.ValueObjects;
using MediatR;

namespace Mari.Application.Releases.Commands.DeleteRelease;

public record DeleteReleaseCommand : IRequest<ErrorOr<Deleted>>
{
    public DeleteReleaseCommand(Guid id)
    {
        Id = ReleaseId.Create(id);
    }

    public ReleaseId Id { get; }
}
