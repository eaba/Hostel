CREATE PROCEDURE [dbo].[CreateReservoir]
@tag nvarchar(50),
@height int,
@hostel uniqueidentifier,
@reservoir uniqueidentifier output
AS
BEGIN
	DECLARE @id uniqueidentifier;
	SET @id = (SELECT ReservoirId FROM Hostel_Water_Reservoir WHERE Tag = @tag AND Hostel = @hostel);
	IF @id IS NULL
	BEGIN
		SET @id = NEWID();
		INSERT INTO Hostel_Water_Reservoir(ReservoirId, Hostel, Height, Tag) VALUES(@id, @hostel, @height, @tag);
		SET @reservoir = @id;
	END
	ELSE
	BEGIN
	 SET @reservoir = @id;
	END
END
