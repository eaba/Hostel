CREATE PROCEDURE [dbo].[CreateFloor]
@tag nvarchar(50),
@hostel uniqueidentifier,
@floor uniqueidentifier output
AS
BEGIN
	DECLARE @id uniqueidentifier;
	SET @id = (SELECT FloorId FROM Hostel_Floors WHERE Tag = @tag AND HostelId = @hostel);
	IF @id IS NULL
	BEGIN
		SET @id = NEWID();
		INSERT INTO Hostel_Floors(FloorId, HostelId, Tag) VALUES(@id, @hostel, @tag);
		SET @floor = @id;
	END
	ELSE
	BEGIN
	 SET @floor = @id;
	END
END
