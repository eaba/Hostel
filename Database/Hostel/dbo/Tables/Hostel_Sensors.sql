CREATE TABLE [dbo].[Hostel_Sensors] (
    [SensorId]  UNIQUEIDENTIFIER CONSTRAINT [DF_Hostel_Sensors_SensorId] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [Tag] NVARCHAR (50)    NOT NULL,
    [Role] NVARCHAR(50) NOT NULL, 
    CONSTRAINT [PK_Hostel_Sensors] PRIMARY KEY CLUSTERED ([SensorId] ASC),
    CONSTRAINT [Unique_Sensor] UNIQUE NONCLUSTERED ([Tag] ASC)
);

