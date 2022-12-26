using System.Net.Http.Json;

namespace Mari.Http.Common.Interfaces;

public interface IRequestBody
{
    JsonContent? GetBody();
}
