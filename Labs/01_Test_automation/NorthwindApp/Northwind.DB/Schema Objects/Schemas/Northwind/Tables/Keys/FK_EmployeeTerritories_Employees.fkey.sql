ALTER TABLE [Northwind].[EmployeeTerritories]
    ADD CONSTRAINT [FK_EmployeeTerritories_Employees] FOREIGN KEY ([EmployeeID]) REFERENCES [Northwind].[Employees] ([EmployeeID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

