CREATE TABLE [dbo].[Hostel_Water_Reservoir_Sensors_Reading] (
    [SensorReadingId] UNIQUEIDENTIFIER CONSTRAINT [DF_Hostel_Water_Reservoir_Sensor_ReservoirSensorId] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ReservoirSensorId]       UNIQUEIDENTIFIER NOT NULL,
    [Key] NVARCHAR(50) NOT NULL, 
    [Value] NVARCHAR(50) NOT NULL, 
    [TimeStamp] BIGINT NOT NULL, 
    CONSTRAINT [PK_Hostel_Water_Reservoir_Sensor] PRIMARY KEY CLUSTERED ([SensorReadingId] ASC),
    CONSTRAINT [FK_Hostel_Water_Reservoir_Sensor_Hostel_Water_Reservoir] FOREIGN KEY ([ReservoirSensorId]) REFERENCES [dbo].[Hostel_Water_Reservoir_Sensors] ([ReservoirSensorId])
);

