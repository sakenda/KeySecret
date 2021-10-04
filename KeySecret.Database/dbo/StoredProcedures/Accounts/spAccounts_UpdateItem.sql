CREATE PROCEDURE [dbo].[spAccounts_UpdateItem]
	@Id INT,
	@Name NVARCHAR(MAX),
	@WebAdress NVARCHAR(MAX),
	@Password NVARCHAR(MAX),
	@CategoryId INT
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE Accounts
	SET [Name] = @Name,
		[WebAdress] = @WebAdress,
		[Password] = @Password,
		[CategoryId] = @CategoryId
	WHERE [Id] = @Id;

END
RETURN 0
