CREATE PROCEDURE [dbo].[spAccounts_DeleteItem]
	@Id int
AS
BEGIN
	SET NOCOUNT ON;
	
	DELETE
	FROM Accounts
	WHERE Id = @Id

END
RETURN 0
