CREATE PROCEDURE [Northwind].[CustOrdersOrders] @CustomerID nchar(5)
AS
SELECT OrderID, 
	OrderDate,
	RequiredDate,
	ShippedDate
FROM [Northwind].Orders
WHERE CustomerID = @CustomerID
ORDER BY OrderID