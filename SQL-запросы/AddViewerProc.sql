USE [Finist]
GO
/****** Object:  StoredProcedure [dbo].[AddViewer]    Script Date: 28.05.2023 13:27:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[AddViewer]
(
    @Name nvarchar(30),
    @Secondname nvarchar(30),
    @Telephone bigint,
    @Email nvarchar(MAX),
    @DateBirthday datetime = NULL,
    @Photo varbinary(MAX) = NULL,
    @Password nvarchar(MAX),
    @Patronymic nvarchar(MAX) = NULL
)
AS
BEGIN
    
    INSERT INTO Viewer (Name, Secondname, Telephone, Email, DateBirthday, Photo, Password, Patronymic)
    VALUES (@Name, @Secondname, @Telephone, @Email, @DateBirthday, @Photo, @Password, @Patronymic)
END