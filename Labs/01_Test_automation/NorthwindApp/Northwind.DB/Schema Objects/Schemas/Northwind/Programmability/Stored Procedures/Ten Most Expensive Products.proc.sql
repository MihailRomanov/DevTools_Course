create procedure [Northwind].[Ten Most Expensive Products] AS
SET ROWCOUNT 10
SELECT Products.ProductName AS TenMostExpensiveProducts, Products.UnitPrice
FROM [Northwind].Products
ORDER BY Products.UnitPrice DESC