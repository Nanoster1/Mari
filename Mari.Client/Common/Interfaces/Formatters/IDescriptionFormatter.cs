using Microsoft.AspNetCore.Components;

namespace Mari.Client.Common.Interfaces.Formatters;

public interface IDescriptionFormatter
{
    MarkupString Format(string description);
}
