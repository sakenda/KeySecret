CREATE PROCEDURE [dbo].[spCategories_GetAllItems]
AS
BEGIN
	SET NOCOUNT ON;

	SELECT [Id], [Name]
	FROM Categories

END
RETURN 0
