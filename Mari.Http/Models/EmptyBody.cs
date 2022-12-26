using System.Net.Http.Json;
using Mari.Http.Common.Interfaces;

namespace Mari.Http.Models;

public sealed record EmptyBody : IRequestBody
{
    public JsonContent? GetBody() => null;
}
