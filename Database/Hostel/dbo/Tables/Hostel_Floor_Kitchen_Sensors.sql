CREATE TABLE [dbo].[Hostel_Floor_Kitchen_Sensors]
(
	[KitchenSensorId]  UNIQUEIDENTIFIER CONSTRAINT [DF_Hostel_Floor_Kitchen_Sensors] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [KitchenId]    UNIQUEIDENTIFIER NOT NULL,
	[SensorId]    UNIQUEIDENTIFIER NOT NULL,
    [Tag] NVARCHAR (50)    NOT NULL,
	[Role] NVARCHAR (50)    NOT NULL,
    CONSTRAINT [PK_Hostel_Floor_Kitchen_Sensors] PRIMARY KEY CLUSTERED ([KitchenSensorId] ASC),
    CONSTRAINT [FK_Hostel_Floor_Kitchen_Sensors_Kitchen] FOREIGN KEY ([KitchenId]) REFERENCES [dbo].[Hostel_Floor_Kitchen] ([KitchenId]),
    CONSTRAINT [IX_Hostel_Floor_Kitchen_Sensors] UNIQUE NONCLUSTERED ([Tag] ASC)
)
