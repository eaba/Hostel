CREATE TABLE [dbo].[Hostel] (
    [HostelId]            UNIQUEIDENTIFIER CONSTRAINT [DF_Hostel_Hostel] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [Name]              NVARCHAR (50)    NOT NULL,
    [Address]           NVARCHAR (200)   NOT NULL,
    CONSTRAINT [PK_Hostel] PRIMARY KEY CLUSTERED ([HostelId] ASC),
	CONSTRAINT [Unique_Name] UNIQUE NONCLUSTERED ([Name] ASC)
);

