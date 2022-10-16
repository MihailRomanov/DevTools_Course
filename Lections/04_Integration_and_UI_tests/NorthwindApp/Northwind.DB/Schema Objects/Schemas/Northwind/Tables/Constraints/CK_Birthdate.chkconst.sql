ALTER TABLE [Northwind].[Employees]
    ADD CONSTRAINT [CK_Birthdate] CHECK ([BirthDate]<getdate());

