USE [Finist]
GO
/****** Object:  StoredProcedure [dbo].[GetDates]    Script Date: 25.05.2023 11:32:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
ALTER PROCEDURE [dbo].[GetDates]
    @MovieId INT
AS
SELECT *
FROM MovieSchedule 

WHERE MovieId = @MovieId
ORDER BY ScreeningTime ASC
