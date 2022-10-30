ALTER TABLE [Northwind].[Orders]
    ADD CONSTRAINT [FK_Orders_Employees] FOREIGN KEY ([EmployeeID]) REFERENCES [Northwind].[Employees] ([EmployeeID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

