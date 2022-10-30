ALTER TABLE [Northwind].[Products]
    ADD CONSTRAINT [CK_Products_UnitPrice] CHECK ([UnitPrice]>=(0));

