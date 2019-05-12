CREATE PROCEDURE [dbo].[InstallReservoirSensor]
	@reservoir uniqueidentifier,
	@tag nvarchar(50),
	@role nvarchar(50),
	@sensorid uniqueidentifier output
AS
BEGIN
	DECLARE @id uniqueidentifier;
	SET @id = (SELECT ReservoirSensorId FROM Hostel_Water_Reservoir_Sensors WHERE Tag = @tag AND ReservoirId = @reservoir);
	IF @id IS NULL
	BEGIN
		SET @id = NEWID();
		INSERT INTO Hostel_Water_Reservoir_Sensors(ReservoirSensorId, ReservoirId, Tag, [Role])
		VALUES(@id, @reservoir, @tag, @role)
		SET @sensorid = @id;
	END
	ELSE
	BEGIN
		SET @sensorid = @id
	END
END
