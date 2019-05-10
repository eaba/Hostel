CREATE PROCEDURE [dbo].[CreateKitchen]
@tag nvarchar(50),
@floor uniqueidentifier,
@kitchen uniqueidentifier output
AS
BEGIN
	DECLARE @id uniqueidentifier;
	SET @id = (SELECT KitchenId FROM Hostel_Floor_Kitchen WHERE Tag = @tag AND FloorId = @floor);
	IF @id IS NULL
	BEGIN
		SET @id = NEWID();
		INSERT INTO Hostel_Floor_Kitchen(KitchenId, FloorId, Tag) VALUES(@id, @floor, @tag);
		SET @kitchen = @id;
	END
	ELSE
	BEGIN
	 SET @kitchen = @id;
	END
END
