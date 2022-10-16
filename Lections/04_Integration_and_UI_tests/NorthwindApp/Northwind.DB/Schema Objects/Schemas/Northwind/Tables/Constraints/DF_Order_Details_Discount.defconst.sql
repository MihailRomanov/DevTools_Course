ALTER TABLE [Northwind].[Order Details]
    ADD CONSTRAINT [DF_Order_Details_Discount] DEFAULT ((0)) FOR [Discount];

