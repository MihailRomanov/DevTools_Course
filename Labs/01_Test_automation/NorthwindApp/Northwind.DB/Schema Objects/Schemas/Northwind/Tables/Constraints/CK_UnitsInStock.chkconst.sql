ALTER TABLE [Northwind].[Products]
    ADD CONSTRAINT [CK_UnitsInStock] CHECK ([UnitsInStock]>=(0));

