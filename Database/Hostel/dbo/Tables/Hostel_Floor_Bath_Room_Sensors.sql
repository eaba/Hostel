CREATE TABLE [dbo].[Hostel_Floor_Bath_Room_Sensors]
(
	[BathroomSensorId]  UNIQUEIDENTIFIER CONSTRAINT [Hostel_Bath_Room_Sensors_BathroomSensorId] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [BathRoomId]    UNIQUEIDENTIFIER NOT NULL,
	[SensorId]    UNIQUEIDENTIFIER NOT NULL,
    [Tag] NVARCHAR (50)    NOT NULL,
	[Role] NVARCHAR (50)    NOT NULL,
    CONSTRAINT [PK_Hostel_Bath_Room_Sensors] PRIMARY KEY CLUSTERED ([BathroomSensorId] ASC),
    CONSTRAINT [FK_Hostel_Bath_Room_Sensors_Bathroom] FOREIGN KEY ([BathRoomId]) REFERENCES [dbo].[Hostel_Floor_Bath_Room] ([BathRoomId]),
    CONSTRAINT [IX_Hostel_Bath_Room_Sensors] UNIQUE NONCLUSTERED ([Tag] ASC)
)
