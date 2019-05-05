CREATE TABLE [dbo].[Hostel_Sensors] (
    [SensorId]  UNIQUEIDENTIFIER CONSTRAINT [DF_Hostel_Sensors_SensorId] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [SensorTag] NVARCHAR (50)    NOT NULL,
    CONSTRAINT [PK_Hostel_Sensors] PRIMARY KEY CLUSTERED ([SensorId] ASC),
    CONSTRAINT [Unique_Sensor] UNIQUE NONCLUSTERED ([SensorTag] ASC)
);

