CREATE PROCEDURE [dbo].[CreateRoom]
@tag nvarchar(50),
@floor uniqueidentifier,
@room uniqueidentifier output
AS
BEGIN
	DECLARE @id uniqueidentifier;
	SET @id = (SELECT RoomId FROM Hostel_Floor_Rooms WHERE Tag = @tag AND FloorId = @floor);
	IF @id IS NULL
	BEGIN
		SET @id = NEWID();
		INSERT INTO Hostel_Floor_Rooms(RoomId, FloorId, Tag) VALUES(@id, @floor, @tag);
		SET @room = @id;
	END
	ELSE
	BEGIN
	 SET @room = @id;
	END
END
