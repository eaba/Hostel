CREATE TABLE [dbo].[Hostel_Roles] (
    [RoleId] UNIQUEIDENTIFIER CONSTRAINT [DF_Hostel_Roles_RoleId] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [Role]   NVARCHAR (50)    NOT NULL,
    CONSTRAINT [PK_Hostel_Roles] PRIMARY KEY CLUSTERED ([RoleId] ASC)
);


GO
CREATE NONCLUSTERED INDEX [Unique_Role]
    ON [dbo].[Hostel_Roles]([Role] ASC);

