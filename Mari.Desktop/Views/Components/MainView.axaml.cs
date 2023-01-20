using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Mari.Desktop.Views.Components;

public partial class MainView : UserControl
{
    public MainView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}
