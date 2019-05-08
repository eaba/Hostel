CREATE TABLE [dbo].[Hostel_Water_Reservoir] (
    [ReservoirId]    UNIQUEIDENTIFIER CONSTRAINT [DF_Hostel_Water_Reservoir_ReservoirId] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [Hostel]         UNIQUEIDENTIFIER NOT NULL,
    [Height]       INT              NOT NULL,
    [Tag] NVARCHAR(50) NOT NULL, 
    CONSTRAINT [PK_Hostel_Water_Reservoir] PRIMARY KEY CLUSTERED ([ReservoirId] ASC),
    CONSTRAINT [FK_Reservoir_Hostel] FOREIGN KEY ([Hostel]) REFERENCES [dbo].[Hostel] ([HostelId])
);

