using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace Mari.Desktop.Views.Components;

public partial class AppBar : UserControl
{
    public AppBar()
    {
        InitializeComponent();
    }

    public event Action OnOpenButtonClick = () => { };

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void OpenButton_OnClick(object sender, RoutedEventArgs e)
    {
        OnOpenButtonClick.Invoke();
    }
}
