create view [Northwind].[Order Details Extended] AS
SELECT "Order Details".OrderID, "Order Details".ProductID, Products.ProductName, 
	"Order Details".UnitPrice, "Order Details".Quantity, "Order Details".Discount, 
	(CONVERT(money,("Order Details".UnitPrice*Quantity*(1-Discount)/100))*100) AS ExtendedPrice
FROM [Northwind].Products INNER JOIN [Northwind]."Order Details" ON Products.ProductID = "Order Details".ProductID
--ORDER BY "Order Details".OrderID
