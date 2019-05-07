CREATE TABLE [dbo].[Hostel_Floor_Toilets_Sensors_Reading] (
    [ToiletSensorReadingId] UNIQUEIDENTIFIER CONSTRAINT [DF_Hostel_Floor_Toilets_Sensors_Reading_ToiletSensorId] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ToiletSensorId]       UNIQUEIDENTIFIER NOT NULL,
    [Key] NVARCHAR(50) NOT NULL, 
    [Value] NVARCHAR(50) NOT NULL, 
    [TimeStamp] BIGINT NOT NULL, 
    CONSTRAINT [PK_Hostel_Floor_Toilets_Sensor] PRIMARY KEY CLUSTERED ([ToiletSensorReadingId] ASC),
    CONSTRAINT [FK_Hostel_Floor_Toilets_Sensor_Hostel_Floor_Toilets] FOREIGN KEY ([ToiletSensorId]) REFERENCES [dbo].[Hostel_Floor_Toilets_Sensors] ([ToiletSensorId]),
);

