CREATE PROCEDURE [dbo].[spCategories_UpdateItem]
	@Id INT,
	@Name NVARCHAR(MAX)
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE Categories
	SET [Name] = @Name
	WHERE [Id] = @Id;

END
RETURN 0
