CREATE TABLE [dbo].[AccessPassword] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [AccessDataId] INT            NOT NULL,
    [Password]     NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_AccessPassword] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AccessPassword_AccessData] FOREIGN KEY ([AccessDataId]) REFERENCES [dbo].[AccessData] ([Id])
);

