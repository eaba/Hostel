CREATE TABLE [dbo].[Hostel_Floor_Kitchen_Sensor] (
    [KitchenSensor] UNIQUEIDENTIFIER CONSTRAINT [DF_Hostel_Floor_Kitchen_Sensor_KitchenSensor] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [KitchenId]     UNIQUEIDENTIFIER NOT NULL,
    [SensorId]      UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_Hostel_Floor_Kitchen_Sensor] PRIMARY KEY CLUSTERED ([KitchenSensor] ASC),
    CONSTRAINT [FK_Hostel_Floor_Kitchen_Sensor_Hostel_Floor_Kitchen] FOREIGN KEY ([KitchenId]) REFERENCES [dbo].[Hostel_Floor_Kitchen] ([KitchenId]),
    CONSTRAINT [FK_Hostel_Floor_Kitchen_Sensor_Hostel_Sensors] FOREIGN KEY ([SensorId]) REFERENCES [dbo].[Hostel_Sensors] ([SensorId])
);

