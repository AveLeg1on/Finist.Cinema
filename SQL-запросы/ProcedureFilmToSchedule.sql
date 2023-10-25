CREATE PROCEDURE GetMovieSchedule
AS
BEGIN
    SELECT Movies.FilmName, MovieSchedule.ScreeningTime
    FROM Movies
    INNER JOIN MovieSchedule ON MovieSchedule.MovieId = Movies.ID
    ORDER BY Movies.FilmName
END