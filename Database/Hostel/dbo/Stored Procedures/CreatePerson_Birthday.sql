CREATE PROCEDURE [dbo].[CreatePerson_Birthday]
@person uniqueidentifier,
@month int,
@year int,
@day int
AS
BEGIN
  IF NOT EXISTS (SELECT PersonId FROM Hostel_Persons_Birthday p WHERE p.PersonId = @person)
  BEGIN
	INSERT INTO Hostel_Persons_Birthday(PersonId, [Day], [Month], [Year])
	VALUES(@person, @day, @month, @year);
  END
  ELSE
  BEGIN
	UPDATE Hostel_Persons_Birthday SET [Day] = @day, [Month] = @month, [Year] = @year
	WHERE PersonId = @person;
  END
END
