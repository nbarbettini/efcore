// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Microsoft.EntityFrameworkCore.Metadata;

#pragma warning disable CS1591

public interface IAdHocMapper
{
    IEntityType MapAdHocEntityType(Type clrType);
}
