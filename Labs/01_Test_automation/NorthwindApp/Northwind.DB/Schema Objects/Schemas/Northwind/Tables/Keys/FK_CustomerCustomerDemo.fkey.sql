ALTER TABLE [Northwind].[CustomerCustomerDemo]
    ADD CONSTRAINT [FK_CustomerCustomerDemo] FOREIGN KEY ([CustomerTypeID]) REFERENCES [Northwind].[CustomerDemographics] ([CustomerTypeID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

