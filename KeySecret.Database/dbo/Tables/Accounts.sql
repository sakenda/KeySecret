CREATE TABLE [dbo].[Accounts] (
    [Id] INT IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (MAX) NOT NULL,
    [WebAdress] NVARCHAR(MAX) NULL, 
    [Password] NVARCHAR(128) NOT NULL,
    [CreatedDate] DATETIME2 (7)  DEFAULT getdate() NOT NULL
);