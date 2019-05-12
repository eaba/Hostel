CREATE PROCEDURE [dbo].[InstallSepticSensor]
	@septictank uniqueidentifier,
	@tag nvarchar(50),
	@role nvarchar(50),
	@sensorid uniqueidentifier output
AS
BEGIN
	DECLARE @id uniqueidentifier;
	SET @id = (SELECT SepticSensorId FROM Hostel_Septic_Tank_Sensors WHERE Tag = @tag AND SepticTankId = @septictank);
	IF @id IS NULL
	BEGIN
		SET @id = NEWID();
		INSERT INTO Hostel_Septic_Tank_Sensors(SepticSensorId, SepticTankId, Tag, [Role])
		VALUES(@id, @septictank, @tag, @role)
		SET @sensorid = @id;
	END
	ELSE
	BEGIN
		SET @sensorid = @id
	END
END