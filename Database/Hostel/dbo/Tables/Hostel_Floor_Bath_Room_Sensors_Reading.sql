CREATE TABLE [dbo].[Hostel_Bath_Room_Sensor_Reading] (
    [BathRoomSensor] UNIQUEIDENTIFIER CONSTRAINT [DF_Hostel_Bath_Room_Sensors_Readings] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [BathroomSensorId] UNIQUEIDENTIFIER NOT NULL,
    [Key] NVARCHAR(50) NOT NULL, 
    [Value] NVARCHAR(50) NOT NULL, 
    [TimeStamp] BIGINT NOT NULL, 
    CONSTRAINT [PK_Hostel_Bath_Room_Sensors_SensorId] PRIMARY KEY CLUSTERED ([BathRoomSensor] ASC),
    CONSTRAINT [FK_Hostel_Bath_Room_Sensor_Hostel_Bath_Rooms] FOREIGN KEY ([BathroomSensorId]) REFERENCES [dbo].[Hostel_Floor_Bath_Room_Sensors] ([BathroomSensorId]),
);

