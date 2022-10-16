create view [Northwind].[Products Above Average Price] AS
SELECT Products.ProductName, Products.UnitPrice
FROM [Northwind].Products
WHERE Products.UnitPrice>(SELECT AVG(UnitPrice) From [Northwind].Products)
--ORDER BY Products.UnitPrice DESC