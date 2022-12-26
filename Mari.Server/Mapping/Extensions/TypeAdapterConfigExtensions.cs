using Mapster;

namespace Mari.Server.Mapping.Extensions;

public static class TypeAdapterConfigExtensions
{
    public static void MapRequests<TWebRequest, TApplicationRequest>(this TypeAdapterConfig config)
    {
        config.ForType<TWebRequest, TApplicationRequest>()
            .MapToConstructor(true);
    }
}
