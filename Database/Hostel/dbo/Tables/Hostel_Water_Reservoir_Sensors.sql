CREATE TABLE [dbo].[Hostel_Water_Reservoir_Sensors]
(
	[ReservoirSensorId]  UNIQUEIDENTIFIER CONSTRAINT [DF_Hostel_Water_Reservoir_Sensors] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ReservoirId]    UNIQUEIDENTIFIER NOT NULL,
    [Tag] NVARCHAR (50)    NOT NULL,
	[Role] NVARCHAR (50)    NOT NULL,
    CONSTRAINT [PK_Hostel_Water_Reservoir_Sensors] PRIMARY KEY CLUSTERED ([ReservoirSensorId] ASC),
    CONSTRAINT [FK_Hostel_Water_Reservoir_Sensors_Reservoir] FOREIGN KEY ([ReservoirId]) REFERENCES [dbo].[Hostel_Water_Reservoir_Sensors] ([ReservoirId]),
    CONSTRAINT [IX_Hostel_Water_Reservoir_Sensors] UNIQUE NONCLUSTERED ([Tag] ASC)
)
