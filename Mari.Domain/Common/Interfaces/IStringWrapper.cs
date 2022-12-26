namespace Mari.Domain.Common.Interfaces;

public interface IStringWrapper
{
    abstract static string Pattern { get; }
    abstract static uint? MaxLength { get; }
    abstract static uint? MinLength { get; }
}
