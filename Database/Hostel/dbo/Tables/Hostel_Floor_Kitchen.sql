CREATE TABLE [dbo].[Hostel_Floor_Kitchen] (
    [KitchenId]  UNIQUEIDENTIFIER CONSTRAINT [DF_Hostel_Kitchen_KitchenId] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [FloorId]    UNIQUEIDENTIFIER NOT NULL,
    [Tag] NVARCHAR (50)    NOT NULL,
    CONSTRAINT [PK_Hostel_Kitchen] PRIMARY KEY CLUSTERED ([KitchenId] ASC),
    CONSTRAINT [FK_Hostel_Kitchen_Hostel_Floors] FOREIGN KEY ([FloorId]) REFERENCES [dbo].[Hostel_Floor] ([FloorId]),
    CONSTRAINT [IX_Hostel_Kitchen] UNIQUE NONCLUSTERED ([Tag] ASC)
);

