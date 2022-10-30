ALTER TABLE [Northwind].[Order Details]
    ADD CONSTRAINT [DF_Order_Details_UnitPrice] DEFAULT ((0)) FOR [UnitPrice];

