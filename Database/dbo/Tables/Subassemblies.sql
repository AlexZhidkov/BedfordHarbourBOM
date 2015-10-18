CREATE TABLE [dbo].[Subassemblies] (
    [Id]               INT              IDENTITY (1, 1) NOT NULL,
    [AssemblyId]       INT              NOT NULL,
    [SubassemblyId]    INT              NOT NULL,
    [InheritedCost]    DECIMAL (25, 13) NOT NULL,
    [CostContribution] DECIMAL (25, 13) NOT NULL,
    [Notes]            NVARCHAR (MAX)   NULL,
    [Capability]       INT              DEFAULT ((0)) NOT NULL,
    [Demand]           DECIMAL (25, 13) DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_dbo.Subassemblies] PRIMARY KEY CLUSTERED ([Id] ASC)
);

