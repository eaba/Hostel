CREATE TABLE [dbo].[Hostel_Floor_Rooms_Sensors] (
    [RoomSensor] UNIQUEIDENTIFIER CONSTRAINT [DF_Hostel_Floor_Room_Sensors_RoomSensor] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [RoomId]     UNIQUEIDENTIFIER NOT NULL,
    [SensorId]   UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_Hostel_Floor_Room_Sensors] PRIMARY KEY CLUSTERED ([RoomSensor] ASC),
    CONSTRAINT [FK_Hostel_Floor_Rooms_Sensors_Hostel_Floor_Rooms] FOREIGN KEY ([RoomId]) REFERENCES [dbo].[Hostel_Floor_Rooms] ([RoomId]),
    CONSTRAINT [FK_Hostel_Floor_Rooms_Sensors_Hostel_Sensors] FOREIGN KEY ([SensorId]) REFERENCES [dbo].[Hostel_Sensors] ([SensorId])
);

