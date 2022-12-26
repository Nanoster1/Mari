using Mari.Application.Common.Interfaces.CommonServices;

namespace Mari.Infrastructure.CommonServices;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTimeOffset UtcNow => DateTimeOffset.UtcNow;
}
