CREATE TABLE [Northwind].[CustomerDemographics](
	[CustomerTypeID] [nchar](10) NOT NULL,
	[CustomerDesc] [ntext] NULL,
 CONSTRAINT [PK_CustomerDemographics] PRIMARY KEY CLUSTERED 
(
	[CustomerTypeID] ASC
)
)