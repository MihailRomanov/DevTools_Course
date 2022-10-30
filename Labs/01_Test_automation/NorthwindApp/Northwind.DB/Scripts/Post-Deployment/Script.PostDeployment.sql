/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
DELETE FROM [Northwind].[Order Details];
DELETE FROM [Northwind].[Orders];
DELETE FROM [Northwind].[EmployeeTerritories];
DELETE FROM [Northwind].[Territories];
DELETE FROM [Northwind].[Products];
DELETE FROM [Northwind].[Suppliers];
DELETE FROM [Northwind].[Shippers];
DELETE FROM [Northwind].[Region];
DELETE FROM [Northwind].[Employees];
DELETE FROM [Northwind].[Customers];
DELETE FROM [Northwind].[Categories];
:r .\Categories.sql
:r .\Customers.sql
:r .\Employees.sql
:r .\Region.sql
:r .\Shippers.sql
:r .\Suppliers.sql
:r .\Products.sql
:r .\Territories.sql
:r .\EmployeeTerritories.sql
:r .\Orders.sql
:r .\OrderDetails.sql
