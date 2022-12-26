using Mari.Http.Common.Classes;
using Mari.Http.Common.Interfaces;

namespace Mari.Http.Requests;

public abstract class PostRequest<TResponse> : Request<TResponse>
    where TResponse : notnull
{
    protected PostRequest(IRequestRoute? route = default, IRequestQuery? query = null, IRequestBody? body = null)
        : base(route, query, body)
    {
    }
}
