CREATE TABLE [Northwind].[CustomerCustomerDemo](
	[CustomerID] [nchar](5) NOT NULL,
	[CustomerTypeID] [nchar](10) NOT NULL,
 CONSTRAINT [PK_CustomerCustomerDemo] PRIMARY KEY CLUSTERED 
(
	[CustomerID] ASC,
	[CustomerTypeID] ASC
)
)