CREATE PROCEDURE [dbo].[spCategories_GetOneItem]
	@Id int
AS
BEGIN
	SET NOCOUNT ON;

	SELECT [Id], [Name]
	FROM Categories
	WHERE Id = @Id

END
RETURN 0
