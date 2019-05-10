CREATE PROCEDURE [dbo].[CreateBathRoom]
@tag nvarchar(50),
@floor uniqueidentifier,
@bathroom uniqueidentifier output
AS
BEGIN
	DECLARE @id uniqueidentifier;
	SET @id = (SELECT BathRoomId FROM Hostel_Floor_Bath_Room WHERE Tag = @tag AND FloorId = @floor);
	IF @id IS NULL
	BEGIN
		SET @id = NEWID();
		INSERT INTO Hostel_Floor_Bath_Room(BathRoomId, FloorId, Tag) VALUES(@id, @floor, @tag);
		SET @bathroom = @id;
	END
	ELSE
	BEGIN
	 SET @bathroom = @id;
	END
END
