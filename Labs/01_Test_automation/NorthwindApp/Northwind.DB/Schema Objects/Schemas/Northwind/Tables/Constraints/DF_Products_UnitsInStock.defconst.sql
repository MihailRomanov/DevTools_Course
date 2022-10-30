ALTER TABLE [Northwind].[Products]
    ADD CONSTRAINT [DF_Products_UnitsInStock] DEFAULT ((0)) FOR [UnitsInStock];

