ALTER TABLE [Northwind].[EmployeeTerritories]
    ADD CONSTRAINT [FK_EmployeeTerritories_Territories] FOREIGN KEY ([TerritoryID]) REFERENCES [Northwind].[Territories] ([TerritoryID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

