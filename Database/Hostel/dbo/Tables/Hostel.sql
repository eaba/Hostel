CREATE TABLE [dbo].[Hostel] (
    [Hostel]            UNIQUEIDENTIFIER CONSTRAINT [DF_Hostel_Hostel] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [Name]              NVARCHAR (50)    NOT NULL,
    [Address]           NVARCHAR (200)   NOT NULL,
    [MaxNoiseLevel]     INT              NOT NULL,
    [CurrentNoiseLevel] INT              NULL,
    CONSTRAINT [PK_Hostel] PRIMARY KEY CLUSTERED ([Hostel] ASC)
);

