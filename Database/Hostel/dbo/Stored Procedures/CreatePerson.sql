CREATE PROCEDURE [dbo].[CreatePerson]
@person uniqueidentifier output,
@firstName nvarchar(50),
@lastName nvarchar(50),
@phone nvarchar(50),
@email nvarchar(50),
@date  bigint,
@role nvarchar(50)
AS
BEGIN
  DECLARE @id uniqueidentifier;
  DECLARE @roleid uniqueidentifier;
  SET @id = (SELECT PersonId FROM Hostel_Persons p WHERE p.Email = @email);
  IF @id IS NULL
  BEGIN
    SET @id = NEWID();
	SET @roleid = (SELECT r.RoleId FROM Hostel_Roles r WHERE r.[Role] = @role); ---LET IT THROW UP!
	INSERT INTO Hostel_Persons(PersonId, FirstName, LastName, Email, Phone, DateRegistered, RoleId)
	VALUES(@id, @firstName, @lastName, @email, @phone, @date, @roleid);
	SET @person = @id;
  END
  ELSE
  BEGIN
	SET @person = @id;
  END
END
