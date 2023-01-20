using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Mari.Desktop.ViewModels;
using Mari.Desktop.Views;
using Microsoft.Extensions.DependencyInjection;

namespace Mari.Desktop;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        var services = ConfigureServices();
        var provider = BuildServiceProvider(services);

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = provider.GetRequiredService<MainWindowViewModel>()
            };
        }

        base.OnFrameworkInitializationCompleted();
    }

    private static IServiceCollection ConfigureServices() => new ServiceCollection().AddApplication();
    private static IServiceProvider BuildServiceProvider(IServiceCollection services) => services.BuildServiceProvider();
}
