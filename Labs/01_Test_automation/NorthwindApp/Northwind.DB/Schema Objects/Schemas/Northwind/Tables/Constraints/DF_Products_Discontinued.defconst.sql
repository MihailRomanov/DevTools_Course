ALTER TABLE [Northwind].[Products]
    ADD CONSTRAINT [DF_Products_Discontinued] DEFAULT ((0)) FOR [Discontinued];

