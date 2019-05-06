CREATE PROCEDURE [dbo].[CreateFloor]
@tag nvarchar(50),
@floor uniqueidentifier output
AS
BEGIN
	DECLARE @id uniqueidentifier;
	SET @id = (SELECT FloorId FROM Hostel_Floors WHERE Tag = @tag);
	IF @id IS NULL
	BEGIN
		SET @id = NEWID();
		INSERT INTO Hostel_Floors(FloorId, Tag) VALUES(@id, @tag);
		SET @floor = @id;
	END
	ELSE
	BEGIN
	 SET @floor = @id;
	END
END
