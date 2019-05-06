CREATE TABLE [dbo].[Hostel_Floor_Toilets] (
    [ToiletId]  UNIQUEIDENTIFIER CONSTRAINT [DF_Hostel_Floor_Toilets_ToiletId] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [FloorId]   UNIQUEIDENTIFIER NOT NULL,
    [Tag] NVARCHAR (50)    NOT NULL,
    CONSTRAINT [PK_Hostel_Floor_Toilets] PRIMARY KEY CLUSTERED ([ToiletId] ASC),
    CONSTRAINT [FK_Hostel_Floor_Toilets_Hostel_Floors] FOREIGN KEY ([FloorId]) REFERENCES [dbo].[Hostel_Floors] ([FloorId])
);

