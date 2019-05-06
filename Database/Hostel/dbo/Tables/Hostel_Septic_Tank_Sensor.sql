CREATE TABLE [dbo].[Hostel_Septic_Tank_Sensor] (
    [SepticTankSensor] UNIQUEIDENTIFIER CONSTRAINT [DF_Hostel_Septic_Tank_Sensor_SepticTankSensor] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [SepticTankId]     UNIQUEIDENTIFIER NOT NULL,
    [Key] NVARCHAR(50) NOT NULL, 
    [Value] NVARCHAR(50) NOT NULL, 
    [TimeStamp] BIGINT NOT NULL, 
    CONSTRAINT [PK_Hostel_Septic_Tank_Sensor] PRIMARY KEY CLUSTERED ([SepticTankSensor] ASC),
    CONSTRAINT [FK_Hostel_Septic_Tank_Sensor_Hostel_Septic_Tank] FOREIGN KEY ([SepticTankId]) REFERENCES [dbo].[Hostel_Septic_Tank] ([SepticTankId])
);

