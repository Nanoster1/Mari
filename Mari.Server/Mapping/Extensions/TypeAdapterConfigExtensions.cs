using Mapster;

namespace Mari.Server.Mapping.Extensions;

public static class TypeAdapterConfigExtensions
{
    public static TypeAdapterSetter<TWebRequest, TApplicationRequest> MapRequests<TWebRequest, TApplicationRequest>(this TypeAdapterConfig config)
    {
        return config.NewConfig<TWebRequest, TApplicationRequest>()
            .MapToConstructor(true)
            .RequireDestinationMemberSource(true)
            .IgnoreNullValues(false);
    }
}
