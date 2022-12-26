using Mari.Http.Common.Classes;
using Mari.Http.Common.Interfaces;
using Mari.Http.Models;

namespace Mari.Http.Requests;

public abstract class PutRequest : Request<VoidResponse>
{
    protected PutRequest(IRequestRoute? route = null, IRequestQuery? query = null, IRequestBody? body = null)
        : base(route, query, body)
    {
    }
}
