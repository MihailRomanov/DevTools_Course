ALTER TABLE [Northwind].[Order Details]
    ADD CONSTRAINT [FK_Order_Details_Products] FOREIGN KEY ([ProductID]) REFERENCES [Northwind].[Products] ([ProductID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

