CREATE TABLE [dbo].[Hostel_Water_Reservoir_Sensor] (
    [ReservoirSensorId] UNIQUEIDENTIFIER CONSTRAINT [DF_Hostel_Water_Reservoir_Sensor_ReservoirSensorId] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ReservoirId]       UNIQUEIDENTIFIER NOT NULL,
    [SensorId]          UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_Hostel_Water_Reservoir_Sensor] PRIMARY KEY CLUSTERED ([ReservoirSensorId] ASC),
    CONSTRAINT [FK_Hostel_Water_Reservoir_Sensor_Hostel_Sensors] FOREIGN KEY ([SensorId]) REFERENCES [dbo].[Hostel_Sensors] ([SensorId]),
    CONSTRAINT [FK_Hostel_Water_Reservoir_Sensor_Hostel_Water_Reservoir] FOREIGN KEY ([ReservoirId]) REFERENCES [dbo].[Hostel_Water_Reservoir] ([ReservoirId])
);

