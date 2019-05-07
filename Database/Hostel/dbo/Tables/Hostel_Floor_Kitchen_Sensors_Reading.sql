CREATE TABLE [dbo].[Hostel_Floor_Kitchen_Sensors_Reading] (
    [KitchenSensor] UNIQUEIDENTIFIER CONSTRAINT [DF_Hostel_Floor_Kitchen_Sensors_Reading_KitchenSensor] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [KitchenSensorId]     UNIQUEIDENTIFIER NOT NULL,
    [Key] NVARCHAR(50) NOT NULL, 
    [Value] NVARCHAR(50) NOT NULL, 
    [TimeStamp] BIGINT NOT NULL, 
    CONSTRAINT [PK_Hostel_Floor_Kitchen_Sensors_Reading] PRIMARY KEY CLUSTERED ([KitchenSensor] ASC),
    CONSTRAINT [FK_Hostel_Floor_Kitchen_Sensors_Reading_Hostel_Floor_Kitchen] FOREIGN KEY ([KitchenSensorId]) REFERENCES [dbo].[Hostel_Floor_Kitchen_Sensors] ([KitchenSensorId])
);

