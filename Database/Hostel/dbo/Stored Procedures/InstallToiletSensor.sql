CREATE PROCEDURE [dbo].[InstallToiletSensor]
	@toilet uniqueidentifier,
	@tag nvarchar(50),
	@role nvarchar(50),
	@sensorid uniqueidentifier output
AS
BEGIN
	DECLARE @id uniqueidentifier;
	SET @id = (SELECT ToiletSensorId FROM Hostel_Floor_Toilets_Sensors WHERE Tag = @tag AND ToiletId = @toilet);
	IF @id IS NULL
	BEGIN
		SET @id = NEWID();
		INSERT INTO Hostel_Floor_Toilets_Sensors(ToiletSensorId, ToiletId, Tag, [Role])
		VALUES(@id, @toilet, @tag, @role)
		SET @sensorid = @id;
	END
	ELSE
	BEGIN
		SET @sensorid = @id
	END
END
