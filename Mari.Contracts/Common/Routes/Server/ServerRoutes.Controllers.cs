namespace Mari.Contracts.Common.Routes.Server;

public static partial class ServerRoutes
{
    public static class Controllers
    {
        public const string Error = $"/error";
        public const string Authentication = $"{Prefix}/auth";
        public const string Release = $"{Prefix}/release";
        public const string Comment = $"{Prefix}/comment";
        public const string User = $"{Prefix}/user";
        public const string Platform = $"{Prefix}/platform";
    }
}
