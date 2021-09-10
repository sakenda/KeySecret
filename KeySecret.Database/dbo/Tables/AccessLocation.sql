CREATE TABLE [dbo].[AccessLocation] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [AccessDataId]   INT            NOT NULL,
    [LocationString] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_AccessLocation] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AccessLocation_AccessData] FOREIGN KEY ([AccessDataId]) REFERENCES [dbo].[AccessData] ([Id])
);

