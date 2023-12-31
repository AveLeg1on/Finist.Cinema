USE [Finist]
GO
/****** Object:  UserDefinedFunction [dbo].[GetFilmViewCount]    Script Date: 09.05.2023 16:25:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER FUNCTION [dbo].[GetFilmViewCount] (@filmId int)
RETURNS int
AS
BEGIN
    DECLARE @viewCount int
    SELECT @viewCount = COUNT(*)
    FROM dbo.Ticket
    WHERE IDFilm = @filmId

    RETURN @viewCount
END