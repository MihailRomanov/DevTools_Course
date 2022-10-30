create view [Northwind].[Current Product List] AS
SELECT Product_List.ProductID, Product_List.ProductName
FROM [Northwind].Products AS Product_List
WHERE (((Product_List.Discontinued)=0))