ALTER TABLE [Northwind].[Products]
    ADD CONSTRAINT [CK_ReorderLevel] CHECK ([ReorderLevel]>=(0));

