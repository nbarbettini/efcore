// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Microsoft.EntityFrameworkCore.Metadata;

#pragma warning disable CS1591

public class RelationalAdHocMapper : AdHocMapper
{
    public RelationalAdHocMapper(AdHocMapperDependencies dependencies)
        : base(dependencies)
    {
    }

    public override IEntityType MapAdHocEntityType(Type clrType)
    {
        var entityType = CreateEntityType(clrType);

        var relationalModel = (RelationalModel)Dependencies.Model.GetRelationalModel();

        var tableMappings = new List<TableMapping>();
        entityType.AddRuntimeAnnotation(RelationalAnnotationNames.DefaultMappings, tableMappings);
        entityType.AddRuntimeAnnotation(RelationalAnnotationNames.TableMappings, tableMappings);

        var (tableName, schema) = GetTableName(clrType);
        var table = new Table(tableName, schema, relationalModel);
        var tableMapping = new TableMapping(entityType, table, false);
        tableMappings.Add(tableMapping);

        foreach (var (member, field, propertyName) in GetMembersToMap(clrType))
        {
            var property = CreateProperty(propertyName, member, field, entityType);

            var columnName = GetColumnName(propertyName, member, field);
            var column = new Column(columnName, ((RelationalTypeMapping)property.TypeMapping).StoreType, table)
            {
                IsNullable = ((IReadOnlyProperty)property).IsNullable
            };

            table.Columns.Add(column.Name, column);
            var columnMapping = new ColumnMapping(property, column, tableMapping);
            tableMapping.AddColumnMapping(columnMapping);
            column.AddPropertyMapping(columnMapping);

            var columnMappings = new SortedSet<ColumnMapping>(ColumnMappingBaseComparer.Instance);
            property.AddRuntimeAnnotation(RelationalAnnotationNames.TableColumnMappings, columnMappings);
            columnMappings.Add(columnMapping);
        }

        return ((RuntimeModel)Dependencies.Model).GetOrAddAdHocEntityType(entityType);
    }

    protected virtual (string TableName, string? Schema) GetTableName(Type clrType)
    {
        var tableAttribute = clrType.GetCustomAttributes<TableAttribute>(inherit: false).FirstOrDefault();

        return (tableAttribute?.Name ?? clrType.Name, tableAttribute?.Schema);
    }

    protected virtual string GetColumnName(string propertyName, MemberInfo member, FieldInfo? field)
        => member.GetCustomAttributes<ColumnAttribute>(inherit: false).FirstOrDefault()?.Name ?? propertyName;
}
