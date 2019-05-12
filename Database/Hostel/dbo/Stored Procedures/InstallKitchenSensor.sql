CREATE PROCEDURE [dbo].[InstallKitchenSensor]
	@kitchen uniqueidentifier,
	@tag nvarchar(50),
	@role nvarchar(50),
	@sensorid uniqueidentifier output
AS
BEGIN
	DECLARE @id uniqueidentifier;
	SET @id = (SELECT KitchenSensorId FROM Hostel_Floor_Kitchen_Sensors WHERE Tag = @tag AND KitchenId = @kitchen);
	IF @id IS NULL
	BEGIN
		SET @id = NEWID();
		INSERT INTO Hostel_Floor_Kitchen_Sensors(KitchenSensorId, KitchenId, Tag, [Role])
		VALUES(@id, @kitchen, @tag, @role)
		SET @sensorid = @id;
	END
	ELSE
	BEGIN
		SET @sensorid = @id
	END
END
