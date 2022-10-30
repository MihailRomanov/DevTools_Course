ALTER TABLE [Northwind].[CustomerCustomerDemo]
    ADD CONSTRAINT [FK_CustomerCustomerDemo_Customers] FOREIGN KEY ([CustomerID]) REFERENCES [Northwind].[Customers] ([CustomerID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

