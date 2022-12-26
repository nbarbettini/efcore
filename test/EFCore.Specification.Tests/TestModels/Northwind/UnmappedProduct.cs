// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Microsoft.EntityFrameworkCore.TestModels.Northwind;

public class UnmappedProduct
{
    public int ProductID { get; set; }
    public string ProductName { get; set; }
    public int? SupplierID { get; set; }
    public int? CategoryID { get; set; }
    public string QuantityPerUnit { get; set; }
    public decimal? UnitPrice { get; set; }
    public ushort UnitsInStock { get; set; }
    public ushort? UnitsOnOrder { get; set; }
    public ushort? ReorderLevel { get; set; }
    public bool Discontinued { get; set; }
}
