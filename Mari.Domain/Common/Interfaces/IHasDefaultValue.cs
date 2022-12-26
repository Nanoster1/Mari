namespace Mari.Domain.Common.Interfaces;

public interface IHasDefaultValue<T>
{
    static abstract T Default { get; }
}
