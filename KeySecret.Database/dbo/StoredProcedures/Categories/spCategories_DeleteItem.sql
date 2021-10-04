CREATE PROCEDURE [dbo].[spCategories_DeleteItem]
	@Id int
AS
BEGIN
	SET NOCOUNT ON;
	
	DELETE
	FROM Categories
	WHERE Id = @Id

END
RETURN 0
