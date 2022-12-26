namespace Mari.Application.Common.Interfaces.CommonServices;

public interface IDateTimeProvider
{
    DateTimeOffset UtcNow { get; }
}
