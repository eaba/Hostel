CREATE TABLE [dbo].[Hostel_Floor_Room_Tenancy_Duration] (
    [TenancyDurationId] UNIQUEIDENTIFIER CONSTRAINT [DF_Hostel_Floor_Room_Tenancy_Duration_TenancyDurationId] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [RoomTenancy]       UNIQUEIDENTIFIER NOT NULL,
    [StartDate]         NVARCHAR (50)    NOT NULL,
    [EndDate]           NVARCHAR (50)    NOT NULL,
    CONSTRAINT [PK_Hostel_Floor_Room_Tenancy_Duration] PRIMARY KEY CLUSTERED ([TenancyDurationId] ASC),
    CONSTRAINT [FK_Hostel_Floor_Room_Tenancy_Duration_Hostel_Floor_Rooms_Tenancy] FOREIGN KEY ([RoomTenancy]) REFERENCES [dbo].[Hostel_Floor_Rooms_Tenancy] ([RoomTenancyId]),
    CONSTRAINT [IX_Hostel_Floor_Room_Tenancy_Duration] UNIQUE NONCLUSTERED ([RoomTenancy] ASC)
);

