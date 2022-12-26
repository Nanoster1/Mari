using System.Net.Http.Json;
using Mari.Http.Common.Interfaces;

namespace Mari.Http.Common.Classes;

public abstract record RequestBody : IRequestBody
{
    public virtual JsonContent? GetBody() => JsonContent.Create((object)this);
}
