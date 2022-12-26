// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.Data.SqlClient;

namespace Microsoft.EntityFrameworkCore.Query;

public class FromSqlQueryUnmappedSqlServerTest : FromSqlQueryUnmappedTestBase<NorthwindQuerySqlServerFixture<NoopModelCustomizer>>
{
    public FromSqlQueryUnmappedSqlServerTest(NorthwindQuerySqlServerFixture<NoopModelCustomizer> fixture, ITestOutputHelper testOutputHelper)
        : base(fixture)
    {
        //Fixture.TestSqlLoggerFactory.SetTestOutputHelper(testOutputHelper);
    }

    public override async Task FromSqlRaw_queryable_simple_unmapped(bool async)
    {
        await base.FromSqlRaw_queryable_simple_unmapped(async);

        AssertSql(
            """
SELECT * FROM "Customers" WHERE "ContactName" LIKE '%z%'
""");
    }

    public override async Task FromSqlRaw_queryable_simple_columns_out_of_order_unmapped(bool async)
    {
        await base.FromSqlRaw_queryable_simple_columns_out_of_order_unmapped(async);

        AssertSql(
            """
SELECT "Region", "PostalCode", "Phone", "Fax", "CustomerID", "Country", "ContactTitle", "ContactName", "CompanyName", "City", "Address" FROM "Customers"
""");
    }

    public override async Task FromSqlRaw_queryable_simple_columns_out_of_order_and_extra_columns_unmapped(bool async)
    {
        await base.FromSqlRaw_queryable_simple_columns_out_of_order_and_extra_columns_unmapped(async);

        AssertSql(
            """
SELECT "Region", "PostalCode", "PostalCode" AS "Foo", "Phone", "Fax", "CustomerID", "Country", "ContactTitle", "ContactName", "CompanyName", "City", "Address" FROM "Customers"
""");
    }

    public override async Task FromSqlRaw_queryable_composed_unmapped(bool async)
    {
        await base.FromSqlRaw_queryable_composed_unmapped(async);

        AssertSql(
"""
SELECT [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[CustomerID], [c].[Fax], [c].[Phone], [c].[Region], [c].[PostalCode]
FROM (
    SELECT * FROM "Customers"
) AS [c]
WHERE [c].[ContactName] LIKE N'%z%'
""");
    }

    public override async Task FromSqlRaw_queryable_composed_after_removing_whitespaces_unmapped(bool async)
    {
        await base.FromSqlRaw_queryable_composed_after_removing_whitespaces_unmapped(async);

        AssertSql(
"""
SELECT [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[CustomerID], [c].[Fax], [c].[Phone], [c].[Region], [c].[PostalCode]
FROM (

        


    SELECT
    * FROM "Customers"
) AS [c]
WHERE [c].[ContactName] LIKE N'%z%'
""");
    }

    public override async Task FromSqlRaw_queryable_composed_compiled_unmapped(bool async)
    {
        await base.FromSqlRaw_queryable_composed_compiled_unmapped(async);

        AssertSql(
"""
SELECT [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[CustomerID], [c].[Fax], [c].[Phone], [c].[Region], [c].[PostalCode]
FROM (
    SELECT * FROM "Customers"
) AS [c]
WHERE [c].[ContactName] LIKE N'%z%'
""");
    }

    public override async Task FromSqlRaw_queryable_composed_compiled_with_DbParameter_unmapped(bool async)
    {
        await base.FromSqlRaw_queryable_composed_compiled_with_DbParameter_unmapped(async);

        AssertSql(
"""
customer='CONSH' (Nullable = false) (Size = 5)

SELECT [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[CustomerID], [c].[Fax], [c].[Phone], [c].[Region], [c].[PostalCode]
FROM (
    SELECT * FROM "Customers" WHERE "CustomerID" = @customer
) AS [c]
WHERE [c].[ContactName] LIKE N'%z%'
""");
    }

    public override async Task FromSqlRaw_queryable_composed_compiled_with_nameless_DbParameter_unmapped(bool async)
    {
        await base.FromSqlRaw_queryable_composed_compiled_with_nameless_DbParameter_unmapped(async);

        AssertSql(
"""
p0='CONSH' (Nullable = false) (Size = 5)

SELECT [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[CustomerID], [c].[Fax], [c].[Phone], [c].[Region], [c].[PostalCode]
FROM (
    SELECT * FROM "Customers" WHERE "CustomerID" = @p0
) AS [c]
WHERE [c].[ContactName] LIKE N'%z%'
""");
    }

    public override async Task FromSqlRaw_queryable_composed_compiled_with_parameter_unmapped(bool async)
    {
        await base.FromSqlRaw_queryable_composed_compiled_with_parameter_unmapped(async);

        AssertSql(
"""
SELECT [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[CustomerID], [c].[Fax], [c].[Phone], [c].[Region], [c].[PostalCode]
FROM (
    SELECT * FROM "Customers" WHERE "CustomerID" = N'CONSH'
) AS [c]
WHERE [c].[ContactName] LIKE N'%z%'
""");
    }

    public override async Task FromSqlRaw_composed_contains_unmapped(bool async)
    {
        await base.FromSqlRaw_composed_contains_unmapped(async);

        AssertSql(
"""
SELECT [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[CustomerID], [c].[Fax], [c].[Phone], [c].[Region], [c].[PostalCode]
FROM [Customers] AS [c]
WHERE EXISTS (
    SELECT 1
    FROM (
        SELECT * FROM "Orders"
    ) AS [o]
    WHERE [o].[CustomerID] = [c].[CustomerID])
""");
    }

    public override async Task FromSqlRaw_composed_contains2_unmapped(bool async)
    {
        await base.FromSqlRaw_composed_contains2_unmapped(async);

        AssertSql(
"""
SELECT [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[CustomerID], [c].[Fax], [c].[Phone], [c].[Region], [c].[PostalCode]
FROM [Customers] AS [c]
WHERE [c].[CustomerID] = N'ALFKI' AND EXISTS (
    SELECT 1
    FROM (
        SELECT * FROM "Orders"
    ) AS [o]
    WHERE [o].[CustomerID] = [c].[CustomerID])
""");
    }

    public override async Task FromSqlRaw_queryable_multiple_composed_unmapped(bool async)
    {
        await base.FromSqlRaw_queryable_multiple_composed_unmapped(async);

        AssertSql(
"""
SELECT [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[CustomerID], [c].[Fax], [c].[Phone], [c].[Region], [c].[PostalCode], [o].[CustomerID], [o].[EmployeeID], [o].[Freight], [o].[OrderDate], [o].[OrderID], [o].[RequiredDate], [o].[ShipAddress], [o].[ShipCity], [o].[ShipCountry], [o].[ShipName], [o].[ShipPostalCode], [o].[ShipRegion], [o].[ShipVia], [o].[ShippedDate]
FROM (
    SELECT * FROM "Customers"
) AS [c]
CROSS JOIN (
    SELECT * FROM "Orders"
) AS [o]
WHERE [c].[CustomerID] = [o].[CustomerID]
""");
    }

    public override async Task FromSqlRaw_queryable_multiple_composed_with_closure_parameters_unmapped(bool async)
    {
        await base.FromSqlRaw_queryable_multiple_composed_with_closure_parameters_unmapped(async);

        AssertSql(
"""
p0='1997-01-01T00:00:00.0000000'
p1='1998-01-01T00:00:00.0000000'

SELECT [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[CustomerID], [c].[Fax], [c].[Phone], [c].[Region], [c].[PostalCode], [o].[CustomerID], [o].[EmployeeID], [o].[Freight], [o].[OrderDate], [o].[OrderID], [o].[RequiredDate], [o].[ShipAddress], [o].[ShipCity], [o].[ShipCountry], [o].[ShipName], [o].[ShipPostalCode], [o].[ShipRegion], [o].[ShipVia], [o].[ShippedDate]
FROM (
    SELECT * FROM "Customers"
) AS [c]
CROSS JOIN (
    SELECT * FROM "Orders" WHERE "OrderDate" BETWEEN @p0 AND @p1
) AS [o]
WHERE [c].[CustomerID] = [o].[CustomerID]
""");
    }

    public override async Task FromSqlRaw_queryable_multiple_composed_with_parameters_and_closure_parameters_unmapped(bool async)
    {
        await base.FromSqlRaw_queryable_multiple_composed_with_parameters_and_closure_parameters_unmapped(async);

        AssertSql(
"""
p0='London' (Size = 4000)
p1='1997-01-01T00:00:00.0000000'
p2='1998-01-01T00:00:00.0000000'

SELECT [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[CustomerID], [c].[Fax], [c].[Phone], [c].[Region], [c].[PostalCode], [o].[CustomerID], [o].[EmployeeID], [o].[Freight], [o].[OrderDate], [o].[OrderID], [o].[RequiredDate], [o].[ShipAddress], [o].[ShipCity], [o].[ShipCountry], [o].[ShipName], [o].[ShipPostalCode], [o].[ShipRegion], [o].[ShipVia], [o].[ShippedDate]
FROM (
    SELECT * FROM "Customers" WHERE "City" = @p0
) AS [c]
CROSS JOIN (
    SELECT * FROM "Orders" WHERE "OrderDate" BETWEEN @p1 AND @p2
) AS [o]
WHERE [c].[CustomerID] = [o].[CustomerID]
""",
            //
"""
p0='Berlin' (Size = 4000)
p1='1998-04-01T00:00:00.0000000'
p2='1998-05-01T00:00:00.0000000'

SELECT [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[CustomerID], [c].[Fax], [c].[Phone], [c].[Region], [c].[PostalCode], [o].[CustomerID], [o].[EmployeeID], [o].[Freight], [o].[OrderDate], [o].[OrderID], [o].[RequiredDate], [o].[ShipAddress], [o].[ShipCity], [o].[ShipCountry], [o].[ShipName], [o].[ShipPostalCode], [o].[ShipRegion], [o].[ShipVia], [o].[ShippedDate]
FROM (
    SELECT * FROM "Customers" WHERE "City" = @p0
) AS [c]
CROSS JOIN (
    SELECT * FROM "Orders" WHERE "OrderDate" BETWEEN @p1 AND @p2
) AS [o]
WHERE [c].[CustomerID] = [o].[CustomerID]
""");
    }

    public override async Task FromSqlRaw_queryable_multiple_line_query_unmapped(bool async)
    {
        await base.FromSqlRaw_queryable_multiple_line_query_unmapped(async);

        AssertSql(
            """
SELECT *
FROM "Customers"
WHERE "City" = 'London'
""");
    }

    public override async Task FromSqlRaw_queryable_composed_multiple_line_query_unmapped(bool async)
    {
        await base.FromSqlRaw_queryable_composed_multiple_line_query_unmapped(async);

        AssertSql(
"""
SELECT [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[CustomerID], [c].[Fax], [c].[Phone], [c].[Region], [c].[PostalCode]
FROM (
    SELECT *
    FROM "Customers"
) AS [c]
WHERE [c].[City] = N'London'
""");
    }

    public override async Task FromSqlRaw_queryable_with_parameters_unmapped(bool async)
    {
        await base.FromSqlRaw_queryable_with_parameters_unmapped(async);

        AssertSql(
            """
p0='London' (Size = 4000)
p1='Sales Representative' (Size = 4000)

SELECT * FROM "Customers" WHERE "City" = @p0 AND "ContactTitle" = @p1
""");
    }

    public override async Task FromSqlRaw_queryable_with_parameters_inline_unmapped(bool async)
    {
        await base.FromSqlRaw_queryable_with_parameters_inline_unmapped(async);

        AssertSql(
            """
p0='London' (Size = 4000)
p1='Sales Representative' (Size = 4000)

SELECT * FROM "Customers" WHERE "City" = @p0 AND "ContactTitle" = @p1
""");
    }

    public override async Task FromSqlInterpolated_queryable_with_parameters_interpolated_unmapped(bool async)
    {
        await base.FromSqlInterpolated_queryable_with_parameters_interpolated_unmapped(async);

        AssertSql(
            """
p0='London' (Size = 4000)
p1='Sales Representative' (Size = 4000)

SELECT * FROM "Customers" WHERE "City" = @p0 AND "ContactTitle" = @p1
""");
    }

    public override async Task FromSql_queryable_with_parameters_interpolated_unmapped(bool async)
    {
        await base.FromSql_queryable_with_parameters_interpolated_unmapped(async);

        AssertSql(
            """
p0='London' (Size = 4000)
p1='Sales Representative' (Size = 4000)

SELECT * FROM "Customers" WHERE "City" = @p0 AND "ContactTitle" = @p1
""");
    }

    public override async Task FromSqlInterpolated_queryable_with_parameters_inline_interpolated_unmapped(bool async)
    {
        await base.FromSqlInterpolated_queryable_with_parameters_inline_interpolated_unmapped(async);

        AssertSql(
            """
p0='London' (Size = 4000)
p1='Sales Representative' (Size = 4000)

SELECT * FROM "Customers" WHERE "City" = @p0 AND "ContactTitle" = @p1
""");
    }

    public override async Task FromSql_queryable_with_parameters_inline_interpolated_unmapped(bool async)
    {
        await base.FromSql_queryable_with_parameters_inline_interpolated_unmapped(async);

        AssertSql(
            """
p0='London' (Size = 4000)
p1='Sales Representative' (Size = 4000)

SELECT * FROM "Customers" WHERE "City" = @p0 AND "ContactTitle" = @p1
""");
    }

    public override async Task FromSqlInterpolated_queryable_multiple_composed_with_parameters_and_closure_parameters_interpolated_unmapped(
        bool async)
    {
        await base.FromSqlInterpolated_queryable_multiple_composed_with_parameters_and_closure_parameters_interpolated_unmapped(async);

        AssertSql(
"""
p0='London' (Size = 4000)
p1='1997-01-01T00:00:00.0000000'
p2='1998-01-01T00:00:00.0000000'

SELECT [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[CustomerID], [c].[Fax], [c].[Phone], [c].[Region], [c].[PostalCode], [o].[CustomerID], [o].[EmployeeID], [o].[Freight], [o].[OrderDate], [o].[OrderID], [o].[RequiredDate], [o].[ShipAddress], [o].[ShipCity], [o].[ShipCountry], [o].[ShipName], [o].[ShipPostalCode], [o].[ShipRegion], [o].[ShipVia], [o].[ShippedDate]
FROM (
    SELECT * FROM "Customers" WHERE "City" = @p0
) AS [c]
CROSS JOIN (
    SELECT * FROM "Orders" WHERE "OrderDate" BETWEEN @p1 AND @p2
) AS [o]
WHERE [c].[CustomerID] = [o].[CustomerID]
""",
            //
"""
p0='Berlin' (Size = 4000)
p1='1998-04-01T00:00:00.0000000'
p2='1998-05-01T00:00:00.0000000'

SELECT [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[CustomerID], [c].[Fax], [c].[Phone], [c].[Region], [c].[PostalCode], [o].[CustomerID], [o].[EmployeeID], [o].[Freight], [o].[OrderDate], [o].[OrderID], [o].[RequiredDate], [o].[ShipAddress], [o].[ShipCity], [o].[ShipCountry], [o].[ShipName], [o].[ShipPostalCode], [o].[ShipRegion], [o].[ShipVia], [o].[ShippedDate]
FROM (
    SELECT * FROM "Customers" WHERE "City" = @p0
) AS [c]
CROSS JOIN (
    SELECT * FROM "Orders" WHERE "OrderDate" BETWEEN @p1 AND @p2
) AS [o]
WHERE [c].[CustomerID] = [o].[CustomerID]
""");
    }

    public override async Task FromSql_queryable_multiple_composed_with_parameters_and_closure_parameters_interpolated_unmapped(
        bool async)
    {
        await base.FromSql_queryable_multiple_composed_with_parameters_and_closure_parameters_interpolated_unmapped(async);

        AssertSql(
"""
p0='London' (Size = 4000)
p1='1997-01-01T00:00:00.0000000'
p2='1998-01-01T00:00:00.0000000'

SELECT [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[CustomerID], [c].[Fax], [c].[Phone], [c].[Region], [c].[PostalCode], [o].[CustomerID], [o].[EmployeeID], [o].[Freight], [o].[OrderDate], [o].[OrderID], [o].[RequiredDate], [o].[ShipAddress], [o].[ShipCity], [o].[ShipCountry], [o].[ShipName], [o].[ShipPostalCode], [o].[ShipRegion], [o].[ShipVia], [o].[ShippedDate]
FROM (
    SELECT * FROM "Customers" WHERE "City" = @p0
) AS [c]
CROSS JOIN (
    SELECT * FROM "Orders" WHERE "OrderDate" BETWEEN @p1 AND @p2
) AS [o]
WHERE [c].[CustomerID] = [o].[CustomerID]
""",
            //
"""
p0='Berlin' (Size = 4000)
p1='1998-04-01T00:00:00.0000000'
p2='1998-05-01T00:00:00.0000000'

SELECT [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[CustomerID], [c].[Fax], [c].[Phone], [c].[Region], [c].[PostalCode], [o].[CustomerID], [o].[EmployeeID], [o].[Freight], [o].[OrderDate], [o].[OrderID], [o].[RequiredDate], [o].[ShipAddress], [o].[ShipCity], [o].[ShipCountry], [o].[ShipName], [o].[ShipPostalCode], [o].[ShipRegion], [o].[ShipVia], [o].[ShippedDate]
FROM (
    SELECT * FROM "Customers" WHERE "City" = @p0
) AS [c]
CROSS JOIN (
    SELECT * FROM "Orders" WHERE "OrderDate" BETWEEN @p1 AND @p2
) AS [o]
WHERE [c].[CustomerID] = [o].[CustomerID]
""");
    }

    public override async Task FromSqlRaw_queryable_with_null_parameter_unmapped(bool async)
    {
        await base.FromSqlRaw_queryable_with_null_parameter_unmapped(async);

        AssertSql(
            """
p0=NULL (Nullable = false)

SELECT * FROM "Employees" WHERE "ReportsTo" = @p0 OR ("ReportsTo" IS NULL AND @p0 IS NULL)
""");
    }

    public override async Task<string> FromSqlRaw_queryable_with_parameters_and_closure_unmapped(bool async)
    {
        var queryString = await base.FromSqlRaw_queryable_with_parameters_and_closure_unmapped(async);

        AssertSql(
"""
p0='London' (Size = 4000)
@__contactTitle_1='Sales Representative' (Size = 30)

SELECT [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[CustomerID], [c].[Fax], [c].[Phone], [c].[Region], [c].[PostalCode]
FROM (
    SELECT * FROM "Customers" WHERE "City" = @p0
) AS [c]
WHERE [c].[ContactTitle] = @__contactTitle_1
""");

        return null;
    }

    public override async Task FromSqlRaw_queryable_simple_cache_key_includes_query_string_unmapped(bool async)
    {
        await base.FromSqlRaw_queryable_simple_cache_key_includes_query_string_unmapped(async);

        AssertSql(
            """
SELECT * FROM "Customers" WHERE "City" = 'London'
""",
            //
            """
SELECT * FROM "Customers" WHERE "City" = 'Seattle'
""");
    }

    public override async Task FromSqlRaw_queryable_with_parameters_cache_key_includes_parameters_unmapped(bool async)
    {
        await base.FromSqlRaw_queryable_with_parameters_cache_key_includes_parameters_unmapped(async);

        AssertSql(
            """
p0='London' (Size = 4000)
p1='Sales Representative' (Size = 4000)

SELECT * FROM "Customers" WHERE "City" = @p0 AND "ContactTitle" = @p1
""",
            //
            """
p0='Madrid' (Size = 4000)
p1='Accounting Manager' (Size = 4000)

SELECT * FROM "Customers" WHERE "City" = @p0 AND "ContactTitle" = @p1
""");
    }

    public override async Task FromSqlRaw_queryable_simple_as_no_tracking_not_composed_unmapped(bool async)
    {
        await base.FromSqlRaw_queryable_simple_as_no_tracking_not_composed_unmapped(async);

        AssertSql(
            """
SELECT * FROM "Customers"
""");
    }

    public override async Task FromSqlRaw_queryable_simple_projection_composed_unmapped(bool async)
    {
        await base.FromSqlRaw_queryable_simple_projection_composed_unmapped(async);

        AssertSql(
"""
SELECT [u].[ProductName]
FROM (
    SELECT *
    FROM "Products"
    WHERE "Discontinued" <> CAST(1 AS bit)
    AND (("UnitsInStock" + "UnitsOnOrder") < "ReorderLevel")
) AS [u]
""");
    }

    public override async Task FromSqlRaw_annotations_do_not_affect_successive_calls_unmapped(bool async)
    {
        await base.FromSqlRaw_annotations_do_not_affect_successive_calls_unmapped(async);

        AssertSql(
"""
SELECT * FROM "Customers" WHERE "ContactName" LIKE '%z%'
""",
            //
"""
SELECT [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[CustomerID], [c].[Fax], [c].[Phone], [c].[Region], [c].[PostalCode]
FROM [Customers] AS [c]
""");
    }

    public override async Task FromSqlRaw_composed_with_nullable_predicate_unmapped(bool async)
    {
        await base.FromSqlRaw_composed_with_nullable_predicate_unmapped(async);

        AssertSql(
"""
SELECT [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[CustomerID], [c].[Fax], [c].[Phone], [c].[Region], [c].[PostalCode]
FROM (
    SELECT * FROM "Customers"
) AS [c]
WHERE [c].[ContactName] = [c].[CompanyName]
""");
    }

    public override async Task FromSqlRaw_with_dbParameter_unmapped(bool async)
    {
        await base.FromSqlRaw_with_dbParameter_unmapped(async);

        AssertSql(
            """
@city='London' (Nullable = false) (Size = 6)

SELECT * FROM "Customers" WHERE "City" = @city
""");
    }

    public override async Task FromSqlRaw_with_dbParameter_without_name_prefix_unmapped(bool async)
    {
        await base.FromSqlRaw_with_dbParameter_without_name_prefix_unmapped(async);
        AssertSql(
            """
city='London' (Nullable = false) (Size = 6)

SELECT * FROM "Customers" WHERE "City" = @city
""");
    }

    public override async Task FromSqlRaw_with_dbParameter_mixed_unmapped(bool async)
    {
        await base.FromSqlRaw_with_dbParameter_mixed_unmapped(async);

        AssertSql(
            """
p0='London' (Size = 4000)
@title='Sales Representative' (Nullable = false) (Size = 20)

SELECT * FROM "Customers" WHERE "City" = @p0 AND "ContactTitle" = @title
""",
            //
            """
@city='London' (Nullable = false) (Size = 6)
p1='Sales Representative' (Size = 4000)

SELECT * FROM "Customers" WHERE "City" = @city AND "ContactTitle" = @p1
""");
    }

    public override async Task FromSqlRaw_with_db_parameters_called_multiple_times_unmapped(bool async)
    {
        await base.FromSqlRaw_with_db_parameters_called_multiple_times_unmapped(async);

        AssertSql(
            """
@id='ALFKI' (Nullable = false) (Size = 5)

SELECT * FROM "Customers" WHERE "CustomerID" = @id
""",
            //
            """
@id='ALFKI' (Nullable = false) (Size = 5)

SELECT * FROM "Customers" WHERE "CustomerID" = @id
""");
    }

    public override async Task FromSqlInterpolated_with_inlined_db_parameter_unmapped(bool async)
    {
        await base.FromSqlInterpolated_with_inlined_db_parameter_unmapped(async);

        AssertSql(
            """
@somename='ALFKI' (Nullable = false) (Size = 5)

SELECT * FROM "Customers" WHERE "CustomerID" = @somename
""");
    }

    public override async Task FromSql_with_inlined_db_parameter_unmapped(bool async)
    {
        await base.FromSql_with_inlined_db_parameter_unmapped(async);

        AssertSql(
            """
@somename='ALFKI' (Nullable = false) (Size = 5)

SELECT * FROM "Customers" WHERE "CustomerID" = @somename
""");
    }

    public override async Task FromSqlInterpolated_with_inlined_db_parameter_without_name_prefix_unmapped(bool async)
    {
        await base.FromSqlInterpolated_with_inlined_db_parameter_without_name_prefix_unmapped(async);

        AssertSql(
            """
somename='ALFKI' (Nullable = false) (Size = 5)

SELECT * FROM "Customers" WHERE "CustomerID" = @somename
""");
    }

    public override async Task FromSql_with_inlined_db_parameter_without_name_prefix_unmapped(bool async)
    {
        await base.FromSql_with_inlined_db_parameter_without_name_prefix_unmapped(async);

        AssertSql(
            """
somename='ALFKI' (Nullable = false) (Size = 5)

SELECT * FROM "Customers" WHERE "CustomerID" = @somename
""");
    }

    public override async Task FromSqlInterpolated_parameterization_issue_12213_unmapped(bool async)
    {
        await base.FromSqlInterpolated_parameterization_issue_12213_unmapped(async);

        AssertSql(
"""
p0='10300'

SELECT [o].[OrderID]
FROM (
    SELECT * FROM "Orders" WHERE "OrderID" >= @p0
) AS [o]
""",
            //
"""
@__max_0='10400'
p0='10300'

SELECT [o].[OrderID]
FROM [Orders] AS [o]
WHERE [o].[OrderID] <= @__max_0 AND EXISTS (
    SELECT 1
    FROM (
        SELECT * FROM "Orders" WHERE "OrderID" >= @p0
    ) AS [o0]
    WHERE [o0].[OrderID] = [o].[OrderID])
""",
            //
"""
@__max_0='10400'
p0='10300'

SELECT [o].[OrderID]
FROM [Orders] AS [o]
WHERE [o].[OrderID] <= @__max_0 AND EXISTS (
    SELECT 1
    FROM (
        SELECT * FROM "Orders" WHERE "OrderID" >= @p0
    ) AS [o0]
    WHERE [o0].[OrderID] = [o].[OrderID])
""");
    }

    public override async Task FromSqlRaw_does_not_parameterize_interpolated_string_unmapped(bool async)
    {
        await base.FromSqlRaw_does_not_parameterize_interpolated_string_unmapped(async);

        AssertSql(
            """
p0='10250'

SELECT * FROM "Orders" WHERE "OrderID" < @p0
""");
    }

    public override async Task FromSqlRaw_with_set_operation_unmapped(bool async)
    {
        await base.FromSqlRaw_with_set_operation_unmapped(async);

        AssertSql(
"""
SELECT [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[CustomerID], [c].[Fax], [c].[Phone], [c].[Region], [c].[PostalCode]
FROM (
    SELECT * FROM "Customers" WHERE "City" = 'London'
) AS [c]
UNION ALL
SELECT [c0].[Address], [c0].[City], [c0].[CompanyName], [c0].[ContactName], [c0].[ContactTitle], [c0].[Country], [c0].[CustomerID], [c0].[Fax], [c0].[Phone], [c0].[Region], [c0].[PostalCode]
FROM (
    SELECT * FROM "Customers" WHERE "City" = 'Berlin'
) AS [c0]
""");
    }

    public override async Task Line_endings_after_Select_unmapped(bool async)
    {
        await base.Line_endings_after_Select_unmapped(async);

        AssertSql(
"""
SELECT [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[CustomerID], [c].[Fax], [c].[Phone], [c].[Region], [c].[PostalCode]
FROM (
    SELECT
    * FROM "Customers"
) AS [c]
WHERE [c].[City] = N'Seattle'
""");
    }

    public override async Task FromSqlRaw_in_subquery_with_dbParameter_unmapped(bool async)
    {
        await base.FromSqlRaw_in_subquery_with_dbParameter_unmapped(async);

        AssertSql(
"""
@city='London' (Nullable = false) (Size = 6)

SELECT [o].[CustomerID], [o].[EmployeeID], [o].[Freight], [o].[OrderDate], [o].[OrderID], [o].[RequiredDate], [o].[ShipAddress], [o].[ShipCity], [o].[ShipCountry], [o].[ShipName], [o].[ShipPostalCode], [o].[ShipRegion], [o].[ShipVia], [o].[ShippedDate]
FROM [Orders] AS [o]
WHERE EXISTS (
    SELECT 1
    FROM (
        SELECT * FROM "Customers" WHERE "City" = @city
    ) AS [c]
    WHERE [c].[CustomerID] = [o].[CustomerID])
""");
    }

    public override async Task FromSqlRaw_in_subquery_with_positional_dbParameter_without_name_unmapped(bool async)
    {
        await base.FromSqlRaw_in_subquery_with_positional_dbParameter_without_name_unmapped(async);

        AssertSql(
"""
p0='London' (Nullable = false) (Size = 6)

SELECT [o].[CustomerID], [o].[EmployeeID], [o].[Freight], [o].[OrderDate], [o].[OrderID], [o].[RequiredDate], [o].[ShipAddress], [o].[ShipCity], [o].[ShipCountry], [o].[ShipName], [o].[ShipPostalCode], [o].[ShipRegion], [o].[ShipVia], [o].[ShippedDate]
FROM [Orders] AS [o]
WHERE EXISTS (
    SELECT 1
    FROM (
        SELECT * FROM "Customers" WHERE "City" = @p0
    ) AS [c]
    WHERE [c].[CustomerID] = [o].[CustomerID])
""");
    }

    public override async Task FromSqlRaw_in_subquery_with_positional_dbParameter_with_name_unmapped(bool async)
    {
        await base.FromSqlRaw_in_subquery_with_positional_dbParameter_with_name_unmapped(async);

        AssertSql(
"""
@city='London' (Nullable = false) (Size = 6)

SELECT [o].[CustomerID], [o].[EmployeeID], [o].[Freight], [o].[OrderDate], [o].[OrderID], [o].[RequiredDate], [o].[ShipAddress], [o].[ShipCity], [o].[ShipCountry], [o].[ShipName], [o].[ShipPostalCode], [o].[ShipRegion], [o].[ShipVia], [o].[ShippedDate]
FROM [Orders] AS [o]
WHERE EXISTS (
    SELECT 1
    FROM (
        SELECT * FROM "Customers" WHERE "City" = @city
    ) AS [c]
    WHERE [c].[CustomerID] = [o].[CustomerID])
""");
    }

    public override async Task FromSqlRaw_with_dbParameter_mixed_in_subquery_unmapped(bool async)
    {
        await base.FromSqlRaw_with_dbParameter_mixed_in_subquery_unmapped(async);

        AssertSql(
"""
p0='London' (Size = 4000)
@title='Sales Representative' (Nullable = false) (Size = 20)

SELECT [o].[CustomerID], [o].[EmployeeID], [o].[Freight], [o].[OrderDate], [o].[OrderID], [o].[RequiredDate], [o].[ShipAddress], [o].[ShipCity], [o].[ShipCountry], [o].[ShipName], [o].[ShipPostalCode], [o].[ShipRegion], [o].[ShipVia], [o].[ShippedDate]
FROM [Orders] AS [o]
WHERE EXISTS (
    SELECT 1
    FROM (
        SELECT * FROM "Customers" WHERE "City" = @p0 AND "ContactTitle" = @title
    ) AS [c]
    WHERE [c].[CustomerID] = [o].[CustomerID])
""",
            //
"""
@city='London' (Nullable = false) (Size = 6)
p1='Sales Representative' (Size = 4000)

SELECT [o].[CustomerID], [o].[EmployeeID], [o].[Freight], [o].[OrderDate], [o].[OrderID], [o].[RequiredDate], [o].[ShipAddress], [o].[ShipCity], [o].[ShipCountry], [o].[ShipName], [o].[ShipPostalCode], [o].[ShipRegion], [o].[ShipVia], [o].[ShippedDate]
FROM [Orders] AS [o]
WHERE EXISTS (
    SELECT 1
    FROM (
        SELECT * FROM "Customers" WHERE "City" = @city AND "ContactTitle" = @p1
    ) AS [c]
    WHERE [c].[CustomerID] = [o].[CustomerID])
""");
    }

    public override async Task Multiple_occurrences_of_FromSql_with_db_parameter_adds_parameter_only_once_unmapped(bool async)
    {
        await base.Multiple_occurrences_of_FromSql_with_db_parameter_adds_parameter_only_once_unmapped(async);

        AssertSql(
"""
city='Seattle' (Nullable = false) (Size = 7)

SELECT [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[CustomerID], [c].[Fax], [c].[Phone], [c].[Region], [c].[PostalCode]
FROM (
    SELECT * FROM "Customers" WHERE "City" = @city
) AS [c]
INTERSECT
SELECT [c0].[Address], [c0].[City], [c0].[CompanyName], [c0].[ContactName], [c0].[ContactTitle], [c0].[Country], [c0].[CustomerID], [c0].[Fax], [c0].[Phone], [c0].[Region], [c0].[PostalCode]
FROM (
    SELECT * FROM "Customers" WHERE "City" = @city
) AS [c0]
""");
    }

    public override async Task FromSqlRaw_composed_with_common_table_expression_unmapped(bool async)
    {
        var exception =
            await Assert.ThrowsAsync<InvalidOperationException>(() => base.FromSqlRaw_composed_with_common_table_expression_unmapped(async));

        Assert.Equal(RelationalStrings.FromSqlNonComposable, exception.Message);
    }

    protected override DbParameter CreateDbParameter(string name, object value)
        => new SqlParameter { ParameterName = name, Value = value };

    private void AssertSql(params string[] expected)
        => Fixture.TestSqlLoggerFactory.AssertBaseline(expected);
}
