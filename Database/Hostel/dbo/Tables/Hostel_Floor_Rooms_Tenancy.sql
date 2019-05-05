CREATE TABLE [dbo].[Hostel_Floor_Rooms_Tenancy] (
    [RoomTenancyId] UNIQUEIDENTIFIER CONSTRAINT [DF_Hostel_Floor_Rooms_Tenancy_RoomTenancyId] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [RoomId]        UNIQUEIDENTIFIER NOT NULL,
    [PersonId]      UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_Hostel_Floor_Rooms_Tenancy] PRIMARY KEY CLUSTERED ([RoomTenancyId] ASC),
    CONSTRAINT [FK_Hostel_Floor_Rooms_Tenancy_Hostel_Floor_Rooms] FOREIGN KEY ([RoomId]) REFERENCES [dbo].[Hostel_Floor_Rooms] ([RoomId]),
    CONSTRAINT [FK_Hostel_Floor_Rooms_Tenancy_Hostel_Persons] FOREIGN KEY ([PersonId]) REFERENCES [dbo].[Hostel_Persons] ([PersonId]),
    CONSTRAINT [IX_Hostel_Floor_Rooms_Tenancy] UNIQUE NONCLUSTERED ([PersonId] ASC)
);

