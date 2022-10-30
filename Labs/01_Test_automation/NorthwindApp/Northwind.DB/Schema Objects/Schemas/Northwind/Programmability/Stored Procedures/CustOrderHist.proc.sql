CREATE PROCEDURE [Northwind].[CustOrderHist] @CustomerID nchar(5)
AS
SELECT ProductName, Total=SUM(Quantity)
FROM [Northwind].Products P, [Northwind].[Order Details] OD, [Northwind].Orders O, [Northwind].Customers C
WHERE C.CustomerID = @CustomerID
AND C.CustomerID = O.CustomerID AND O.OrderID = OD.OrderID AND OD.ProductID = P.ProductID
GROUP BY ProductName