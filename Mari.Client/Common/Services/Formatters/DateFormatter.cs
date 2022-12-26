using Humanizer;
using Mari.Client.Common.Interfaces.Formatters;

namespace Mari.Client.Common.Services.Formatters;

public class DateFormatter : IDateFormatter
{
    private const string ConstantFormatDate = "dd.MM.yyyy";
    private const string ConstantFormatDateTime = "dd.MM.yyyy hh:mm:ss";

    public string FormatDate(DateTimeOffset? dateTime, bool utcDate = false)
    {
        return dateTime?.ToLocalTime().ToString(ConstantFormatDate) ?? string.Empty;
    }

    public string FormatDateTime(DateTimeOffset? dateTime, bool utcDate = false)
    {

        return Convert(dateTime, utcDate)?.ToString(ConstantFormatDateTime) ?? string.Empty;
    }
    public string Humanize(DateTimeOffset? dateTime, bool utcDate = false)
    {
        return Convert(dateTime, utcDate)?.Humanize() ?? string.Empty;
    }

    public DateTime? Convert(DateTimeOffset? dateTime, bool utcDate = false)
    {
        if (dateTime is null) return null;
        return utcDate ? dateTime.Value.UtcDateTime : dateTime.Value.LocalDateTime;
    }
}
