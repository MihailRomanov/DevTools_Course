CREATE PROCEDURE [Northwind].[CustOrdersDetail] @OrderID int
AS
SELECT ProductName,
    UnitPrice=ROUND(Od.UnitPrice, 2),
    Quantity,
    Discount=CONVERT(int, Discount * 100), 
    ExtendedPrice=ROUND(CONVERT(money, Quantity * (1 - Discount) * Od.UnitPrice), 2)
FROM [Northwind].Products P, [Northwind].[Order Details] Od
WHERE Od.ProductID = P.ProductID and Od.OrderID = @OrderID