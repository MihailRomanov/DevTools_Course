ALTER TABLE [Northwind].[Order Details]
    ADD CONSTRAINT [FK_Order_Details_Orders] FOREIGN KEY ([OrderID]) REFERENCES [Northwind].[Orders] ([OrderID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

