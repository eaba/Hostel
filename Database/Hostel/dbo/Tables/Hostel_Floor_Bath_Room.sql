CREATE TABLE [dbo].[Hostel_Floor_Bath_Room] (
    [BathRoomId] UNIQUEIDENTIFIER CONSTRAINT [DF_Hostel_Bath_Room_BathRoomId] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [FloorId]    UNIQUEIDENTIFIER NOT NULL,
    [Tag]    NVARCHAR (50)    NOT NULL,
    CONSTRAINT [PK_Hostel_Bath_Room] PRIMARY KEY CLUSTERED ([BathRoomId] ASC),
    CONSTRAINT [FK_Hostel_Bath_Room_Hostel_Floors] FOREIGN KEY ([FloorId]) REFERENCES [dbo].[Hostel_Floor] ([FloorId])
);

