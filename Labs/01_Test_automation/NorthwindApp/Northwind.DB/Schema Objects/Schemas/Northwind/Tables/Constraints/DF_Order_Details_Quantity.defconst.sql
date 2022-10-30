ALTER TABLE [Northwind].[Order Details]
    ADD CONSTRAINT [DF_Order_Details_Quantity] DEFAULT ((1)) FOR [Quantity];

