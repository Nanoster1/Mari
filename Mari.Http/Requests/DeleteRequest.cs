using Mari.Http.Common.Classes;
using Mari.Http.Common.Interfaces;
using Mari.Http.Models;

namespace Mari.Http.Requests;

public abstract class DeleteRequest<TResponse> : Request<TResponse>
    where TResponse : notnull
{
    protected DeleteRequest(IRequestRoute? route = null, IRequestQuery? query = null) : base(route, query)
    {
    }
}
