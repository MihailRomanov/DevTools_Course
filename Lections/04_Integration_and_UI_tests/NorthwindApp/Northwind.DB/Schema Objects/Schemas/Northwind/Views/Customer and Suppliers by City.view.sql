create view [Northwind].[Customer and Suppliers by City] AS
SELECT City, CompanyName, ContactName, 'Customers' AS Relationship 
FROM [Northwind].Customers
UNION SELECT City, CompanyName, ContactName, 'Suppliers'
FROM [Northwind].Suppliers
--ORDER BY City, CompanyName