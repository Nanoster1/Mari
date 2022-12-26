namespace Mari.Server.Settings;

public class HostSettings
{
    public const string SectionName = "HostSettings";
    public string Scheme { get; set; } = null!;
    public string Host { get; set; } = null!;
    public int Port { get; set; } = -1;
}
