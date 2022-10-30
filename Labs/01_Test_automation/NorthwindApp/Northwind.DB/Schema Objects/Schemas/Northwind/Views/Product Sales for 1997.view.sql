create view [Northwind].[Product Sales for 1997] AS
SELECT Categories.CategoryName, Products.ProductName, 
Sum(CONVERT(money,("Order Details".UnitPrice*Quantity*(1-Discount)/100))*100) AS ProductSales
FROM ([Northwind].Categories INNER JOIN [Northwind].Products ON Categories.CategoryID = Products.CategoryID) 
	INNER JOIN ([Northwind].Orders 
		INNER JOIN [Northwind]."Order Details" ON Orders.OrderID = "Order Details".OrderID) 
	ON Products.ProductID = "Order Details".ProductID
WHERE (((Orders.ShippedDate) Between '19970101' And '19971231'))
GROUP BY Categories.CategoryName, Products.ProductName
