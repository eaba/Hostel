CREATE TABLE [dbo].[Hostel_Tenants]--https://docs.microsoft.com/en-us/sql/relational-databases/json/store-json-documents-in-sql-tables?view=sql-server-2017
(
	[HostelTenantsId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Hostel] UNIQUEIDENTIFIER NOT NULL, 
    [Tenants] NVARCHAR(MAX) NOT NULL,
	INDEX cci CLUSTERED COLUMNSTORE ---If you expect to have a large number of JSON documents in your collection, we recommend adding a CLUSTERED COLUMNSTORE index on the collection
)
