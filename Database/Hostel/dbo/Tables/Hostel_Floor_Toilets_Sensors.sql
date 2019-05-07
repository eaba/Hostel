CREATE TABLE [dbo].[Hostel_Floor_Toilets_Sensors]
(
	[ToiletSensorId]  UNIQUEIDENTIFIER CONSTRAINT [DF_Hostel_Floor_Toilets_Sensors] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ToiletId]    UNIQUEIDENTIFIER NOT NULL,
	[SensorId]    UNIQUEIDENTIFIER NOT NULL,
    [Tag] NVARCHAR (50)    NOT NULL,
	[Role] NVARCHAR (50)    NOT NULL,
    CONSTRAINT [PK_Hostel_Floor_Toilets_Sensors] PRIMARY KEY CLUSTERED ([ToiletSensorId] ASC),
    CONSTRAINT [FK_Hostel_Floor_Toilets_Sensors_Toilets] FOREIGN KEY ([ToiletId]) REFERENCES [dbo].[Hostel_Floor_Toilets] ([ToiletId]),
    CONSTRAINT [IX_Hostel_Floor_Toilets_Sensors] UNIQUE NONCLUSTERED ([Tag] ASC)
)
