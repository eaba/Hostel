CREATE TABLE [dbo].[Hostel_Floor_Rooms_Tenants]
(
	[TenancyTenantId]  UNIQUEIDENTIFIER CONSTRAINT [DF_Hostel_Floor_Rooms_Tenants] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [PersonId] UNIQUEIDENTIFIER NOT NULL,
    [RoomTenancyId] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_Hostel_Floor_Rooms_Tenants] PRIMARY KEY CLUSTERED ([TenancyTenantId] ASC),
    CONSTRAINT [FK_Tenant_Persons] FOREIGN KEY ([PersonId]) REFERENCES [dbo].[Hostel_Persons] ([PersonId]),
    CONSTRAINT [FK_RoomTenancy] FOREIGN KEY ([RoomTenancyId]) REFERENCES [dbo].[Hostel_Floor_Rooms_Tenancy] ([RoomTenancyId]) ON DELETE CASCADE,
    CONSTRAINT [Unique_Tenancy_Tenants] UNIQUE NONCLUSTERED ([PersonId] ASC, [RoomTenancyId] ASC)
)
