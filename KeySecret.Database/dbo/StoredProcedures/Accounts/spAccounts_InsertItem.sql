CREATE PROCEDURE [dbo].[spAccounts_InsertItem]
	@Name NVARCHAR(MAX),
	@WebAdress NVARCHAR(MAX),
	@Password NVARCHAR(MAX),
	@CategoryId INT
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO Accounts ([Name], [WebAdress], [Password], [CategoryId])
				  VALUES (@Name, @WebAdress, @Password, @CategoryId)

END
RETURN 0
