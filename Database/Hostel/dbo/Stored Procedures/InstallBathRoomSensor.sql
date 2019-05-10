CREATE PROCEDURE [dbo].[InstallBathRoomSensor]
	@bathroom uniqueidentifier,
	@tag nvarchar(50),
	@role nvarchar(50),
	@sensorid uniqueidentifier output
AS
BEGIN
	DECLARE @id uniqueidentifier;
	SET @id = (SELECT BathroomSensorId FROM Hostel_Floor_Bath_Room_Sensors WHERE Tag = @tag AND BathRoomId = @bathroom);
	IF @id IS NULL
	BEGIN
		INSERT INTO Hostel_Floor_Bath_Room_Sensors(BathroomSensorId, BathRoomId, Tag, [Role])
		VALUES(@id, @bathroom, @tag, @role)
		SET @sensorid = @id;
	END
	ELSE
	BEGIN
		SET @sensorid = @id
	END
END
