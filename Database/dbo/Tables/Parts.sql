CREATE TABLE [dbo].[Parts] (
    [Id]             INT              IDENTITY (1, 1) NOT NULL,
    [Type]           INT              NOT NULL,
    [Number]         NVARCHAR (MAX)   NULL,
    [Description]    NVARCHAR (MAX)   NULL,
    [IsOwnMake]      BIT              NOT NULL,
    [Length]         INT              NOT NULL,
    [OwnCost]        DECIMAL (25, 13) NOT NULL,
    [ComponentsCost] DECIMAL (25, 13) NOT NULL,
    [Notes]          NVARCHAR (MAX)   NULL,
    [Count]          INT              DEFAULT ((0)) NOT NULL,
    [CountDate]      DATETIME         DEFAULT ('1900-01-01T00:00:00.000') NULL,
    [OnOrder]        INT              DEFAULT ((0)) NOT NULL,
    [Capability]     INT              DEFAULT ((0)) NOT NULL,
    [Demand]         INT              DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_dbo.Parts] PRIMARY KEY CLUSTERED ([Id] ASC)
);

