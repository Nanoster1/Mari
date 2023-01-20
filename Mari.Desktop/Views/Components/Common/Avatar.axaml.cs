using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Mari.Desktop.Views.Common.Extensions;
using Throw;

namespace Mari.Desktop.Views.Components.Common;

public partial class Avatar : UserControl
{
    private readonly ImageBrush _image;

    public Avatar()
    {
        InitializeComponent();
        _image = (ImageBrush)this.FindControlOrThrow<Ellipse>("Image").Fill
            .ThrowIfNull()
            .Value;
    }

    public IBitmap Source { set => _image.Source = value; }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}
