ALTER TABLE [Northwind].[Order Details]
    ADD CONSTRAINT [CK_Discount] CHECK ([Discount]>=(0) AND [Discount]<=(1));

