CREATE TABLE [dbo].[Hostel_Floor_Toilets_Sensor] (
    [ToiletSensorId] UNIQUEIDENTIFIER CONSTRAINT [DF_Hostel_Floor_Toilets_Sensor_ToiletSensorId] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ToiletId]       UNIQUEIDENTIFIER NOT NULL,
    [SensorId]       UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_Hostel_Floor_Toilets_Sensor] PRIMARY KEY CLUSTERED ([ToiletSensorId] ASC),
    CONSTRAINT [FK_Hostel_Floor_Toilets_Sensor_Hostel_Floor_Toilets] FOREIGN KEY ([ToiletId]) REFERENCES [dbo].[Hostel_Floor_Toilets] ([ToiletId]),
    CONSTRAINT [FK_Hostel_Floor_Toilets_Sensor_Hostel_Sensors] FOREIGN KEY ([SensorId]) REFERENCES [dbo].[Hostel_Sensors] ([SensorId])
);

