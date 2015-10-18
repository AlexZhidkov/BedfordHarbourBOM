CREATE TABLE [dbo].[Suppliers] (
    [Id]      INT            IDENTITY (1, 1) NOT NULL,
    [Name]    NVARCHAR (MAX) NULL,
    [Contact] NVARCHAR (MAX) NULL,
    [Phone]   NVARCHAR (MAX) NULL,
    [Notes]   NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_dbo.Suppliers] PRIMARY KEY CLUSTERED ([Id] ASC)
);

