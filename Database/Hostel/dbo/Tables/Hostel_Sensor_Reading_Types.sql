CREATE TABLE [dbo].[Hostel_Sensor_Reading_Types]
(
	[ReadingId] UNIQUEIDENTIFIER CONSTRAINT [DF_Hostel_Sensor_Reading_Types_ReadingId] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [Reading]       NVARCHAR(50) NOT NULL,
	CONSTRAINT [PK_Hostel_Sensor_Reading_Types_ReadingId] PRIMARY KEY CLUSTERED ([ReadingId] ASC),
)
