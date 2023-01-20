using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Mari.Desktop.Views.Common.Extensions;

namespace Mari.Desktop.Views.Components;

public partial class NavMenu : UserControl
{
    private readonly Border _navBorder;

    private bool _isHidden;
    private double? _borderWidth;

    public NavMenu()
    {
        InitializeComponent();
        _navBorder = this.FindControlOrThrow<Border>("NavBorder");
    }

    public bool IsHidden
    {
        get => _isHidden;
        set => _isHidden = OnIsHiddenChanged(value);
    }

    private bool OnIsHiddenChanged(in bool isHidden)
    {
        _borderWidth ??= _navBorder.Width;
        _navBorder.Width = isHidden ? 0 : _borderWidth.Value;
        return isHidden;
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}
