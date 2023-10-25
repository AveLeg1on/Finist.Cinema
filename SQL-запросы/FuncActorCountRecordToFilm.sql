CREATE FUNCTION dbo.GetCountRecordingActor(@IdActor int)
RETURNS int
AS
BEGIN 
		DECLARE @countRecord int 
		SELECT @countRecord=COUNT(*)
		FROM ActorToFilm
		WHERE IDActor=@IdActor
		RETURN @countRecord
END
