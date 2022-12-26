using System.Text.RegularExpressions;
using System.Web;
using Mari.Client.Common.Interfaces.Formatters;
using Microsoft.AspNetCore.Components;

namespace Mari.Client.Common.Services.Formatters;

public class RegexDescriptionFormatter : IDescriptionFormatter
{
    private static Regex Pattern { get; } = new Regex("\r?\n|\r",RegexOptions.Compiled);
    private static string Replacement { get; } = "<br />";
    
    public MarkupString Format(string description)
    {
        var encodedDescription = HttpUtility.HtmlEncode(description);
        return (MarkupString)Pattern.Replace(encodedDescription,Replacement);
    }
}
