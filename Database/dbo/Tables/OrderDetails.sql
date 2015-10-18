CREATE TABLE [dbo].[OrderDetails] (
    [Id]      INT             IDENTITY (1, 1) NOT NULL,
    [Price]   DECIMAL (10, 2) NOT NULL,
    [Count]   INT             NOT NULL,
    [Notes]   NVARCHAR (MAX)  NULL,
    [OrderId] INT             NOT NULL,
    [PartId]  INT             DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_dbo.OrderDetails] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.OrderDetails_dbo.Orders_Order_Id] FOREIGN KEY ([OrderId]) REFERENCES [dbo].[Orders] ([Id]) ON DELETE CASCADE
);

