CREATE TABLE [dbo].[Accounts] (
    [Id] INT IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (MAX) NULL,
    [WebAdress] NVARCHAR(MAX) NULL, 
    [Password] NVARCHAR(128) NULL,
    [CreatedDate] DATETIME2 (7)  DEFAULT getdate() NOT NULL
);