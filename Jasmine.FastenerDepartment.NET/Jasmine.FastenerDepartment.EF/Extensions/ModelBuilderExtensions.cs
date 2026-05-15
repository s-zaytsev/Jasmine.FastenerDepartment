using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jasmine.FastenerDepartment.EF.Extensions;

internal static class ModelBuilderExtensions
{
    public static PropertyBuilder<TProperty> UseSerialColumn<TProperty, TEntity>(
        this PropertyBuilder<TProperty> propertyBuilder,
        string sequenceName = null)
        where TEntity : class
    {
        if (string.IsNullOrEmpty(sequenceName))
        {
            var entityType = typeof(TEntity);
            var propertyName = propertyBuilder.Metadata.Name;
            sequenceName = $"\"{entityType.Name}_{propertyName}_seq\"";
        }

        return propertyBuilder.HasDefaultValueSql($"nextval('{sequenceName}')");
    }
}
