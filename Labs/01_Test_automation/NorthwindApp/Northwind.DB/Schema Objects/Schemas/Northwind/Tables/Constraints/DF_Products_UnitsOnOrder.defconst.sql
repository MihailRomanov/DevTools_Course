ALTER TABLE [Northwind].[Products]
    ADD CONSTRAINT [DF_Products_UnitsOnOrder] DEFAULT ((0)) FOR [UnitsOnOrder];

