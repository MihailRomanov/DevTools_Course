create view [Northwind].[Order Subtotals] AS
SELECT "Order Details".OrderID, Sum(CONVERT(money,("Order Details".UnitPrice*Quantity*(1-Discount)/100))*100) AS Subtotal
FROM [Northwind]."Order Details"
GROUP BY "Order Details".OrderID