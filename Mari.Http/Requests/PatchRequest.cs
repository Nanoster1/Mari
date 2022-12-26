using Mari.Http.Common.Classes;
using Mari.Http.Common.Interfaces;

namespace Mari.Http.Requests;

public abstract class PatchRequest<TResponse> : Request<TResponse>
    where TResponse : notnull
{
    protected PatchRequest(IRequestRoute? route = null, IRequestQuery? query = null, IRequestBody? body = null)
        : base(route, query, body)
    {
    }
}
