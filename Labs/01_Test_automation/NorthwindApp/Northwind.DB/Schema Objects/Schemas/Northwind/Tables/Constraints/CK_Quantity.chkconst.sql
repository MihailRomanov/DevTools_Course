ALTER TABLE [Northwind].[Order Details]
    ADD CONSTRAINT [CK_Quantity] CHECK ([Quantity]>(0));

