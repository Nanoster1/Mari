namespace Mari.Desktop.Paths;

public static class ConfigurationPaths
{
    public static readonly string AppSettingsPath = 
        Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources/appsettings.json");
}
