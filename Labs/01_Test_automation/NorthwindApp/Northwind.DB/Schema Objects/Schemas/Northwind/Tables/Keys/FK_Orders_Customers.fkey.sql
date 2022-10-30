ALTER TABLE [Northwind].[Orders]
    ADD CONSTRAINT [FK_Orders_Customers] FOREIGN KEY ([CustomerID]) REFERENCES [Northwind].[Customers] ([CustomerID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

