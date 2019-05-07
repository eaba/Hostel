CREATE TABLE [dbo].[Hostel_Septic_Tank] (
    [SepticTankId]   UNIQUEIDENTIFIER CONSTRAINT [DF_Hostel_Septic_Tank_SepticTankId] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [Hostel]         UNIQUEIDENTIFIER NOT NULL,
    [Height]       INT              NOT NULL,
    [Tag] NVARCHAR(50) NOT NULL, 
    CONSTRAINT [PK_Hostel_Septic_Tank] PRIMARY KEY CLUSTERED ([SepticTankId] ASC),
    CONSTRAINT [FK_SepticTank_Hostel] FOREIGN KEY ([Hostel]) REFERENCES [dbo].[Hostel] ([Hostel])
);

