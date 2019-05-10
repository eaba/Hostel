CREATE TABLE [dbo].[Hostel_Floor_Rooms_Tenancy]
(
	[RoomTenancyId]  UNIQUEIDENTIFIER CONSTRAINT [DF_Hostel_Floor_Rooms_Tenancy] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [RoomId] UNIQUEIDENTIFIER NOT NULL,
    [StartDate] BIGINT    NOT NULL,
    [EndDate] BIGINT NOT NULL, 
    [Rate] NVARCHAR(50) NOT NULL, 
    [PaymentType] NVARCHAR(50) NOT NULL, 
    [Price] DECIMAL NOT NULL, 
    CONSTRAINT [PK_Hostel_Floor_Rooms_Tenancy] PRIMARY KEY CLUSTERED ([RoomTenancyId] ASC),
    CONSTRAINT [FK_Hostel_Floor_Rooms_Tenancy_Hostel_Floor_Rooms] FOREIGN KEY ([RoomId]) REFERENCES [dbo].[Hostel_Floor_Rooms] ([RoomId]),
    CONSTRAINT [XQ_Hostel_Floor_Rooms_Tenancy] UNIQUE NONCLUSTERED ([RoomId] ASC)
)
