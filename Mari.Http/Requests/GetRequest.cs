using Mari.Http.Common.Classes;
using Mari.Http.Common.Interfaces;

namespace Mari.Http.Requests;

public abstract class GetRequest<TResponse> : Request<TResponse>
    where TResponse : notnull
{
    protected GetRequest(IRequestRoute? route = default, IRequestQuery? query = default) : base(route, query)
    {
    }
}
