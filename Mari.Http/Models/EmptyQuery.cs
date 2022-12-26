using Mari.Http.Common.Interfaces;

namespace Mari.Http.Models;

public record EmptyQuery : IRequestQuery
{
    public string GetQueryString() => string.Empty;
}
