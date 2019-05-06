CREATE TABLE [dbo].[Hostel_Persons_Birthday]
(
	[BirthdayId]       UNIQUEIDENTIFIER CONSTRAINT [DF_Hostel_Persons_BirthdayId] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [PersonId]         UNIQUEIDENTIFIER NOT NULL,
	[Day]      INT    NOT NULL,
    [Month]       INT    NOT NULL,
    [Year]    INT           NOT NULL,
	CONSTRAINT [PK_BirthdayId] PRIMARY KEY CLUSTERED ([BirthdayId] ASC),
    CONSTRAINT [FK_Hostel_Persons_Birthdays] FOREIGN KEY ([PersonId]) REFERENCES [dbo].[Hostel_Persons] ([PersonId]),
)
