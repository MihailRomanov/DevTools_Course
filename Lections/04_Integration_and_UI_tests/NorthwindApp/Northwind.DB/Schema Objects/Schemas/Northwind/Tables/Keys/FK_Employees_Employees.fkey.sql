ALTER TABLE [Northwind].[Employees]
    ADD CONSTRAINT [FK_Employees_Employees] FOREIGN KEY ([ReportsTo]) REFERENCES [Northwind].[Employees] ([EmployeeID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

