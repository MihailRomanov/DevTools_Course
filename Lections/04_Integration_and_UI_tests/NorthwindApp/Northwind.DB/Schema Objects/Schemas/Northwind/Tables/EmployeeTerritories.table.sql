CREATE TABLE [Northwind].[EmployeeTerritories](
	[EmployeeID] [int] NOT NULL,
	[TerritoryID] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_EmployeeTerritories] PRIMARY KEY CLUSTERED 
(
	[EmployeeID] ASC,
	[TerritoryID] ASC
)
)