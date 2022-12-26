namespace Mari.Client.Models.Releases;

public class PlatformModel
{
    public string Name { get; set; } = string.Empty;
    public VersionModel LastVersion { get; set; } = new();
}
