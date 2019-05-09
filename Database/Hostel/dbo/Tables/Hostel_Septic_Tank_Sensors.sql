CREATE TABLE [dbo].[Hostel_Septic_Tank_Sensors]
(
	[SepticSensorId]  UNIQUEIDENTIFIER CONSTRAINT [DF_Hostel_Septic_Tank_Sensors] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [SepticTankId]    UNIQUEIDENTIFIER NOT NULL,
    [Tag] NVARCHAR (50)    NOT NULL,
	[Role] NVARCHAR (50)    NOT NULL,
    CONSTRAINT [PK_Hostel_Septic_Tank_Sensors] PRIMARY KEY CLUSTERED ([SepticSensorId] ASC),
    CONSTRAINT [FK_Hostel_Septic_Tank_Sensors_Septic] FOREIGN KEY ([SepticTankId]) REFERENCES [dbo].[Hostel_Septic_Tank] ([SepticTankId]),
    CONSTRAINT [IX_Hostel_Septic_Tank_Sensors] UNIQUE NONCLUSTERED ([Tag] ASC)
)
