CREATE PROCEDURE [dbo].[spAccounts_GetOneItem]
	@Id INT
AS
BEGIN
	SET NOCOUNT ON;

	SELECT [Id],
		   [Name],
		   [WebAdress],
		   [Password],
		   [CreatedDate],
		   [CategoryId]
	FROM Accounts
	WHERE Id = @Id

END
