CREATE TABLE [dbo].[Hostel_Bath_Room_Sensor] (
    [RoomSensor] UNIQUEIDENTIFIER CONSTRAINT [DF_Hostel_Bath_Room_Sensor_RoomSensor] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [BathRoomId] UNIQUEIDENTIFIER NOT NULL,
    [Key] NVARCHAR(50) NOT NULL, 
    [Value] NVARCHAR(50) NOT NULL, 
    [TimeStamp] BIGINT NOT NULL, 
    CONSTRAINT [PK_Hostel_Bath_Room_Sensor] PRIMARY KEY CLUSTERED ([RoomSensor] ASC),
    CONSTRAINT [FK_Hostel_Bath_Room_Sensor_Hostel_Bath_Room] FOREIGN KEY ([BathRoomId]) REFERENCES [dbo].[Hostel_Bath_Room] ([BathRoomId]),
);

