ALTER TABLE [Northwind].[Order Details]
    ADD CONSTRAINT [CK_UnitPrice] CHECK ([UnitPrice]>=(0));

