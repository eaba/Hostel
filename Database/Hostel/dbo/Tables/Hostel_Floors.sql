CREATE TABLE [dbo].[Hostel_Floors] (
    [FloorId]   UNIQUEIDENTIFIER CONSTRAINT [DF_Hostel_Floors_FloorId] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [Tag] NVARCHAR (50)    NOT NULL,
    CONSTRAINT [PK_Hostel_Floors] PRIMARY KEY CLUSTERED ([FloorId] ASC),
    CONSTRAINT [Unique_Floor_Name] UNIQUE NONCLUSTERED ([Tag] ASC)
);

