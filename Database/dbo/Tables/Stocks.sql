CREATE TABLE [dbo].[Stocks] (
    [Id]        INT             IDENTITY (1, 1) NOT NULL,
    [PartId]    INT             NOT NULL,
    [Count]     INT             NOT NULL,
    [CountDate] DATETIME        NULL,
    [Cost]      DECIMAL (18, 2) NOT NULL,
    [Notes]     NVARCHAR (MAX)  NULL,
    CONSTRAINT [PK_dbo.Stocks] PRIMARY KEY CLUSTERED ([Id] ASC)
);

