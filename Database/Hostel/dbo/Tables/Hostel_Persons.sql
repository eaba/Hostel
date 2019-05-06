CREATE TABLE [dbo].[Hostel_Persons] (
    [PersonId]       UNIQUEIDENTIFIER CONSTRAINT [DF_Hostel_Owner_PersonId] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [FirstName]      NVARCHAR (50)    NOT NULL,
    [LastName]       NVARCHAR (50)    NOT NULL,
    [Phone]    NVARCHAR(20)           NOT NULL,
    [Email]          NVARCHAR (100)   NOT NULL,
    [DateRegistered] BIGINT           NOT NULL,
    [RoleId]         UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_Hostel_Owner] PRIMARY KEY CLUSTERED ([PersonId] ASC),
    CONSTRAINT [FK_Hostel_Persons_Hostel_Roles] FOREIGN KEY ([PersonId]) REFERENCES [dbo].[Hostel_Roles] ([RoleId]),
    CONSTRAINT [UniqueEmail] UNIQUE NONCLUSTERED ([Email] ASC),
    CONSTRAINT [UniquePhone] UNIQUE NONCLUSTERED ([Phone] ASC)
);

