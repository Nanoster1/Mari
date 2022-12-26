namespace Mari.Client.Common.Interfaces.Formatters;

public interface IDateFormatter
{
    string FormatDate(DateTimeOffset? dateTime, bool utcDate = false);
    string FormatDateTime(DateTimeOffset? dateTime, bool utcDate = false);
    string Humanize(DateTimeOffset? dateTime, bool utcDate = false);
}
