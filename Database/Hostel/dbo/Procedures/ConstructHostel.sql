CREATE PROCEDURE [dbo].[ConstructHostel]
@name nvarchar(50),
@address nvarchar(50),
@hostel uniqueidentifier output
AS
BEGIN
	DECLARE @id uniqueidentifier;
	SET @id = (SELECT HostelId FROM Hostel WHERE [Name] = @name);
	IF @id IS NULL
	BEGIN
		SET @id = NEWID();
		INSERT INTO Hostel(HostelId, [Name], [Address]) VALUES(@id, @name, @address);
		SET @hostel = @id;
	END
	ELSE
	BEGIN
	 SET @hostel = @id;
	END
END