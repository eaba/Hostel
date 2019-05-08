CREATE PROCEDURE [dbo].[CreateSepticTank]
@tag nvarchar(50),
@height int,
@hostel uniqueidentifier,
@septictank uniqueidentifier output
AS
BEGIN
	DECLARE @id uniqueidentifier;
	SET @id = (SELECT SepticTankId FROM Hostel_Septic_Tank WHERE Tag = @tag AND Hostel = @hostel);
	IF @id IS NULL
	BEGIN
		SET @id = NEWID();
		INSERT INTO Hostel_Septic_Tank(SepticTankId, Hostel, Height, Tag) VALUES(@id, @hostel, @height, @tag);
		SET @septictank = @id;
	END
	ELSE
	BEGIN
	 SET @septictank = @id;
	END
END