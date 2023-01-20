using Avalonia.Controls;
using Mari.Desktop.Views.Common.Extensions;
using Mari.Desktop.Views.Components;

namespace Mari.Desktop.Views;

public partial class MainWindow : Window
{
    private readonly AppBar _appBar;
    private readonly NavMenu _navMenu;

    public MainWindow()
    {
        InitializeComponent();
        _appBar = this.FindControlOrThrow<AppBar>("AppBar");
        _navMenu = this.FindControlOrThrow<NavMenu>("NavMenu");

        _appBar.OnOpenButtonClick += OnNavMenuOpenButtonClick;
    }

    private void OnNavMenuOpenButtonClick()
    {
        _navMenu.IsHidden = !_navMenu.IsHidden;
    }
}
