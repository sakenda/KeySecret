CREATE PROCEDURE [dbo].[spAccounts_GetAllItems]
AS
BEGIN
	SET NOCOUNT ON;

	SELECT [Id], [Name], [WebAdress], [Password], [CreatedDate]
	FROM Accounts

END
RETURN 0
