namespace Mari.Contracts.Common.Routes.Client;

public static partial class ClientRoutes
{
    public static class Pages
    {
        public const string Index = $"{Prefix}/";
        public const string TokenHandler = $"{Prefix}/token";
        public const string Archive = $"{Prefix}/archive";
        public const string Settings = $"{Prefix}/setting";
        public const string ReleasesInfo = $"{Prefix}/info/{{Id}}";
    }
}
