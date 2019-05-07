CREATE TABLE [dbo].[Hostel_Bath_Room_Sensors_Reading] (
    [BathRoomSensor] UNIQUEIDENTIFIER CONSTRAINT [DF_Hostel_Bath_Room_Sensors_Reading] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [BathroomSensorId] UNIQUEIDENTIFIER NOT NULL,
    [Key] NVARCHAR(50) NOT NULL, 
    [Value] NVARCHAR(50) NOT NULL, 
    [TimeStamp] BIGINT NOT NULL, 
    CONSTRAINT [PK_Hostel_Bath_Room_Sensor] PRIMARY KEY CLUSTERED ([BathRoomSensor] ASC),
    CONSTRAINT [FK_Hostel_Bath_Room_Sensor_Hostel_Bath_Room] FOREIGN KEY ([BathroomSensorId]) REFERENCES [dbo].[Hostel_Bath_Room_Sensors] ([BathroomSensorId]),
);

