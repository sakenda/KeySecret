CREATE TABLE [dbo].[AccessData] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (MAX) NOT NULL,
    [CreatedDate] DATETIME2 (7)  CONSTRAINT [DF_AccessData_CreatedDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_AccessData] PRIMARY KEY CLUSTERED ([Id] ASC)
);

