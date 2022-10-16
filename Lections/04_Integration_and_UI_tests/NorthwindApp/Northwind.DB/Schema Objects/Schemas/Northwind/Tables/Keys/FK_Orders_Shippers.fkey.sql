ALTER TABLE [Northwind].[Orders]
    ADD CONSTRAINT [FK_Orders_Shippers] FOREIGN KEY ([ShipVia]) REFERENCES [Northwind].[Shippers] ([ShipperID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

