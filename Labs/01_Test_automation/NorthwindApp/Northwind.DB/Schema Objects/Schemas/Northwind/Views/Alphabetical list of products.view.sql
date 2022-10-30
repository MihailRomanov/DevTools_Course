create view [Northwind].[Alphabetical list of products] AS
SELECT Products.*, Categories.CategoryName
FROM [Northwind].Categories INNER JOIN [Northwind].Products ON Categories.CategoryID = Products.CategoryID
WHERE (((Products.Discontinued)=0))