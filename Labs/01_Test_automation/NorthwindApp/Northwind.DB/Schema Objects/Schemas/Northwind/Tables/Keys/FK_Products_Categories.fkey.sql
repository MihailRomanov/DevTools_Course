ALTER TABLE [Northwind].[Products]
    ADD CONSTRAINT [FK_Products_Categories] FOREIGN KEY ([CategoryID]) REFERENCES [Northwind].[Categories] ([CategoryID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

