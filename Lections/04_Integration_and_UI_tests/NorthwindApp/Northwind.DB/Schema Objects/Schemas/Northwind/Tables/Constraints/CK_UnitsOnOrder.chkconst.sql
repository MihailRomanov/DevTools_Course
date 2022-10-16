ALTER TABLE [Northwind].[Products]
    ADD CONSTRAINT [CK_UnitsOnOrder] CHECK ([UnitsOnOrder]>=(0));

