CREATE PROCEDURE [dbo].[CreateRoom]
@tag nvarchar(50),
@floor uniqueidentifier,
@room uniqueidentifier output
AS
BEGIN
	DECLARE @id uniqueidentifier;
	SET @id = (SELECT RoomId FROM Hostel_Floor_Rooms WHERE Tag = @tag AND HostelId = @hostel);
	IF @id IS NULL
	BEGIN
		SET @id = NEWID();
		INSERT INTO Hostel_Floor(FloorId, HostelId, Tag) VALUES(@id, @hostel, @tag);
		SET @floor = @id;
	END
	ELSE
	BEGIN
	 SET @floor = @id;
	END
END
