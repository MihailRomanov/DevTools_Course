ALTER TABLE [Northwind].[Orders]
    ADD CONSTRAINT [DF_Orders_Freight] DEFAULT ((0)) FOR [Freight];

