using ErrorOr;
using Mari.Application.Common.Shared.Paging;
using Mari.Application.Releases.Results;
using MediatR;

namespace Mari.Application.Releases.Queries.GetCurrentReleases;

public record GetCurrentReleasesQuery(int? Page = null, int PageSize = 0) :
    PageRequest(Page, PageSize),
    IRequest<ErrorOr<IList<ReleaseResult>>>;
