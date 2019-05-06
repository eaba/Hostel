CREATE TABLE [dbo].[Hostel_Floor_Rooms] (
    [RoomId]  UNIQUEIDENTIFIER CONSTRAINT [DF_Hostel_Floor_Rooms_RoomId] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [FloorId] UNIQUEIDENTIFIER NOT NULL,
    [Tag] NVARCHAR (50)    NOT NULL,
    CONSTRAINT [PK_Hostel_Floor_Rooms] PRIMARY KEY CLUSTERED ([RoomId] ASC),
    CONSTRAINT [FK_Hostel_Floor_Rooms_Hostel_Floors] FOREIGN KEY ([FloorId]) REFERENCES [dbo].[Hostel_Floors] ([FloorId]),
    CONSTRAINT [Unique_Floor_Room] UNIQUE NONCLUSTERED ([Tag] ASC)
);

