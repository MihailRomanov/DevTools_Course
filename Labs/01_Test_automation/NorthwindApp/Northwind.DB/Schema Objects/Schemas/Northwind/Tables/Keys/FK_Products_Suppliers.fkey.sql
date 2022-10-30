ALTER TABLE [Northwind].[Products]
    ADD CONSTRAINT [FK_Products_Suppliers] FOREIGN KEY ([SupplierID]) REFERENCES [Northwind].[Suppliers] ([SupplierID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

