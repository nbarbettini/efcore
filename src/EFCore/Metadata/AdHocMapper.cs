// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Microsoft.EntityFrameworkCore.Metadata;

#pragma warning disable CS1591

public class AdHocMapper : IAdHocMapper
{
    public AdHocMapper(AdHocMapperDependencies dependencies)
    {
        Dependencies = dependencies;
    }

    public virtual AdHocMapperDependencies Dependencies { get; }

    public virtual IEntityType MapAdHocEntityType(Type clrType)
    {
        throw new InvalidOperationException("Ad-hoc entity types are not supported by this database provider.");
    }

    protected virtual RuntimeEntityType CreateEntityType(Type clrType)
    {
        if (Dependencies.Model is not RuntimeModel runtimeModel)
        {
            throw new InvalidOperationException("Ad-hoc entity types can only be used at runtime.");
        }

        return new RuntimeEntityType(
            clrType.DisplayName(), clrType, false, runtimeModel, null, null, ChangeTrackingStrategy.Snapshot, null, false, null);
    }

    protected virtual RuntimeProperty CreateProperty(
        string propertyName,
        MemberInfo member,
        FieldInfo? field,
        RuntimeEntityType entityType)
    {
        var nullable = !GetRequired(propertyName, member, field);
        var (maxLength, unicode, precision, scale) = GetFacets(propertyName, member, field);

        var property = entityType.AddProperty(
            name: propertyName,
            clrType: member.GetMemberType(),
            propertyInfo: member as PropertyInfo,
            fieldInfo: field ?? member as FieldInfo,
            nullable: nullable,
            maxLength: maxLength,
            unicode: unicode,
            precision: precision,
            scale: scale);

        var typeMapping = FindTypeMapping(property);
        if (typeMapping == null)
        {
            throw new Exception("Bang!");
        }

        property.TypeMapping = typeMapping;

        return property;
    }

    protected virtual IEnumerable<(MemberInfo Member, FieldInfo? Field, string Name)> GetMembersToMap(Type clrType)
        => clrType.GetRuntimeProperties()
            .Where(
                p => p.IsCandidateProperty(needsWrite: true, publicOnly: true)
                    && !p.GetCustomAttributes<NotMappedAttribute>(inherit: false).Any())
            .Select(m => ((MemberInfo)m, (FieldInfo?)null, m.Name));

    protected virtual (int? MaxLength, bool? Unicode, int? Precision, int? Scale) GetFacets(
        string name, MemberInfo member, FieldInfo? field)
    {
        var maxLength = member.GetCustomAttributes<MaxLengthAttribute>(inherit: false).FirstOrDefault()?.Length
            ?? member.GetCustomAttributes<StringLengthAttribute>(inherit: false).FirstOrDefault()?.MaximumLength;

        var unicode = member.GetCustomAttributes<UnicodeAttribute>(inherit: false).FirstOrDefault()?.IsUnicode;

        var precisionAttribute = member.GetCustomAttributes<PrecisionAttribute>(inherit: false).FirstOrDefault();

        return (maxLength, unicode, precisionAttribute?.Precision, precisionAttribute?.Scale);
    }

    protected virtual bool GetRequired(
        string name, MemberInfo member, FieldInfo? field)
    {
        var propertyType = member.GetMemberType();

        return member.GetCustomAttributes<RequiredAttribute>(inherit: false).Any()
            || IsNonNullableReferenceType()
            || !propertyType.IsNullableType();

        bool IsNonNullableReferenceType()
        {
            if (propertyType.IsValueType)
            {
                return false;
            }

            var nullabilityInfoContext = new NullabilityInfoContext();
            var nullabilityInfo = member switch
            {
                PropertyInfo propertyInfo => nullabilityInfoContext.Create(propertyInfo),
                FieldInfo fieldInfo => nullabilityInfoContext.Create(fieldInfo),
                _ => null
            };

            return nullabilityInfo?.ReadState == NullabilityState.NotNull;
        }
    }

    protected virtual CoreTypeMapping? FindTypeMapping(RuntimeProperty property)
        => Dependencies.TypeMappingSource.FindMapping(property);
}
