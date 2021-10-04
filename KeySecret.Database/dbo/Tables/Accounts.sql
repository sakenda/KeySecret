CREATE TABLE [dbo].[Accounts] (
    [Id] INT IDENTITY (1, 1) NOT NULL PRIMARY KEY,
    [Name] NVARCHAR (MAX) NULL,
    [WebAdress] NVARCHAR(MAX) NULL, 
    [Password] NVARCHAR(128) NULL,
    [CategoryId] INT NULL,
    [CreatedDate] DATETIME2 (7) DEFAULT getdate() NOT NULL, 
    CONSTRAINT [FK_Accounts_Categories] FOREIGN KEY ([CategoryId]) REFERENCES [Categories]([Id])
);