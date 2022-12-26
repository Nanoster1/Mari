using ErrorOr;
using Mari.Application.Comments.Results;
using Mari.Application.Common.Shared.Paging;
using Mari.Domain.Releases.ValueObjects;
using MediatR;

namespace Mari.Application.Comments.Queries.GetSystemComments;

public record GetSystemCommentsQuery :
    PageRequest,
    IRequest<ErrorOr<IList<CommentResult>>>
{
    public GetSystemCommentsQuery(Guid releaseId, int? page = null, int pageSize = 0) : base(page, pageSize)
    {
        ReleaseId = ReleaseId.Create(releaseId);
    }

    public ReleaseId ReleaseId { get; }
}
