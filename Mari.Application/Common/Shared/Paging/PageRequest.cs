namespace Mari.Application.Common.Shared.Paging;

public abstract record PageRequest
{
    public PageRequest(int? page = null, int pageSize = 0)
    {
        Page = page;
        PageSize = pageSize;
    }

    public int? Page { get; }
    public int PageSize { get; }
    public Range Range => Page is null ? Range.All : (((int)Page - 1) * PageSize)..((int)Page * PageSize);
}
