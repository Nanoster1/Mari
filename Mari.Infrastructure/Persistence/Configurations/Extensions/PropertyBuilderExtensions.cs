using Mari.Domain.Common.Interfaces;
using Mari.Domain.Common.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mari.Infrastructure.Persistence.Configurations.Extensions;

public static class PropertyBuilderExtensions
{
    public static PropertyBuilder<TWrapper> IsValueObjectWrapper<TBase, TWrapper>(this PropertyBuilder<TWrapper> propertyBuilder)
        where TWrapper : ValueObjectWrapper<TBase, TWrapper>, new()
        where TBase : IComparable<TBase>
    {
        var comparer = new ValueObjectComparer();

        propertyBuilder.HasConversion(
            valueObject => valueObject.Value,
            value => ValueObjectWrapper<TBase, TWrapper>.Create(value),
            new ValueComparer<TWrapper>((x, y) => comparer.Equals(x, y), x => x.GetHashCode()));

        return propertyBuilder;
    }

    public static PropertyBuilder<TWrapper> IsStringWrapper<TWrapper>(this PropertyBuilder<TWrapper> propertyBuilder)
        where TWrapper : ValueObjectWrapper<string, TWrapper>, IStringWrapper, new()
    {
        propertyBuilder.IsValueObjectWrapper<string, TWrapper>();

        if (TWrapper.MaxLength is not null)
        {
            propertyBuilder.HasMaxLength(((int)TWrapper.MaxLength.Value));
        }

        return propertyBuilder;
    }
}
