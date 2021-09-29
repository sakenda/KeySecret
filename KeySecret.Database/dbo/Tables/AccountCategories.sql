CREATE TABLE [dbo].[AccountCategories]
(
    [AccountId] INT NOT NULL, 
    [CategoryId] INT NOT NULL, 
    CONSTRAINT [UQ_Category_AccountId] UNIQUE NONCLUSTERED ([AccountId] ASC),
    CONSTRAINT [FK_AccountCategories_Categories] FOREIGN KEY ([CategoryId]) REFERENCES [Categories] ([Id]),
    CONSTRAINT [FK_AccountCategories_Accounts] FOREIGN KEY ([AccountId]) REFERENCES [Accounts] ([Id])
)
