using Avalonia.Controls;
using Throw;

namespace Mari.Desktop.Views.Common.Extensions;

public static class AvaloniaControlExtensions
{
    public static T FindControlOrThrow<T>(this IControl control, string name) where T : class, IControl
    {
        return control.FindControl<T>(name)
            .ThrowIfNull($"Control not found: {name}")
            .Value;
    }
}
