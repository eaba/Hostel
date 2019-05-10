CREATE PROCEDURE [dbo].[CreateToilet]
@tag nvarchar(50),
@floor uniqueidentifier,
@toilet uniqueidentifier output
AS
BEGIN
	DECLARE @id uniqueidentifier;
	SET @id = (SELECT ToiletId FROM Hostel_Floor_Toilets WHERE Tag = @tag AND FloorId = @floor);
	IF @id IS NULL
	BEGIN
		SET @id = NEWID();
		INSERT INTO Hostel_Floor_Toilets(ToiletId, FloorId, Tag) VALUES(@id, @floor, @tag);
		SET @toilet = @id;
	END
	ELSE
	BEGIN
	 SET @toilet = @id;
	END
END
