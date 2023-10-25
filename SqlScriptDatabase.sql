USE [master]
GO
/****** Object:  Database [Finist]    Script Date: 25.10.2023 11:55:14 ******/
CREATE DATABASE [Finist]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Finist', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\Finist.mdf' , SIZE = 14592KB , MAXSIZE = UNLIMITED, FILEGROWTH = 10%)
 LOG ON 
( NAME = N'Finist_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\Finist_log.ldf' , SIZE = 6912KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Finist] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Finist].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Finist] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Finist] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Finist] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Finist] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Finist] SET ARITHABORT OFF 
GO
ALTER DATABASE [Finist] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Finist] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Finist] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Finist] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Finist] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Finist] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Finist] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Finist] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Finist] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Finist] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Finist] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Finist] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Finist] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Finist] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Finist] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Finist] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Finist] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Finist] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Finist] SET  MULTI_USER 
GO
ALTER DATABASE [Finist] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Finist] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Finist] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Finist] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [Finist] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Finist] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [Finist] SET QUERY_STORE = OFF
GO
USE [Finist]
GO
/****** Object:  UserDefinedFunction [dbo].[GetAverageTicketPrice]    Script Date: 25.10.2023 11:55:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[GetAverageTicketPrice] ()
RETURNS int
AS
BEGIN
    DECLARE @avgPrice int
    SELECT @avgPrice = AVG(TotalSum)
    FROM dbo.Ticket

    RETURN @avgPrice
END
GO
/****** Object:  UserDefinedFunction [dbo].[GetCountRecordingActor]    Script Date: 25.10.2023 11:55:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[GetCountRecordingActor](@IdActor int)
RETURNS int
AS
BEGIN 
		DECLARE @countRecord int 
		SELECT @countRecord=COUNT(*)
		FROM ActorToFilm
		WHERE IDActor=@IdActor
		RETURN @countRecord
END
GO
/****** Object:  UserDefinedFunction [dbo].[GetFilmViewCount]    Script Date: 25.10.2023 11:55:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[GetFilmViewCount] (@filmId int)
RETURNS int
AS
BEGIN
    DECLARE @viewCount int
    SELECT @viewCount = COUNT(*)
    FROM dbo.Ticket
    WHERE IDFilm = @filmId

    RETURN @viewCount
END
GO
/****** Object:  Table [dbo].[Actor]    Script Date: 25.10.2023 11:55:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Actor](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](30) NOT NULL,
	[Secondname] [nvarchar](30) NOT NULL,
	[DateBirthday] [date] NULL,
	[Gender] [nvarchar](50) NULL,
 CONSTRAINT [PK_Actor] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ActorToFilm]    Script Date: 25.10.2023 11:55:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ActorToFilm](
	[IDActor] [int] NOT NULL,
	[IDFilm] [int] NOT NULL,
 CONSTRAINT [PK_ActorToFilm_1] PRIMARY KEY CLUSTERED 
(
	[IDActor] ASC,
	[IDFilm] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Movies]    Script Date: 25.10.2023 11:55:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Movies](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FilmName] [nvarchar](80) NOT NULL,
	[Budget] [nvarchar](max) NOT NULL,
	[CompanyPublisher] [nvarchar](80) NOT NULL,
	[DateRelease] [date] NOT NULL,
	[Director] [nvarchar](max) NOT NULL,
	[Rating] [float] NULL,
	[Poster] [varbinary](max) NOT NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_Film] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[ActorForMovies]    Script Date: 25.10.2023 11:55:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ActorForMovies]
AS
SELECT        TOP (2) dbo.Movies.FilmName, dbo.Movies.Poster, dbo.Actor.Name, dbo.Actor.Secondname, dbo.Movies.ID
FROM            dbo.Movies INNER JOIN
                         dbo.ActorToFilm ON dbo.Movies.ID = dbo.ActorToFilm.IDFilm INNER JOIN
                         dbo.Actor ON dbo.ActorToFilm.IDActor = dbo.Actor.ID
ORDER BY NEWID()
GO
/****** Object:  Table [dbo].[Worker]    Script Date: 25.10.2023 11:55:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Worker](
	[ID] [int] NOT NULL,
	[Secondname] [nvarchar](30) NOT NULL,
	[Name] [nvarchar](30) NOT NULL,
	[Patronymic] [nvarchar](max) NOT NULL,
	[PassportSeries] [int] NOT NULL,
	[PassportNumber] [int] NOT NULL,
	[Telephone] [bigint] NOT NULL,
	[Photo] [varbinary](max) NULL,
 CONSTRAINT [PK_Worker] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ViewingRoom]    Script Date: 25.10.2023 11:55:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ViewingRoom](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[NumberViewingRoom] [int] NOT NULL,
	[IDWorker] [int] NULL,
 CONSTRAINT [PK_ViewingRoom] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WorkerToViewingRoom]    Script Date: 25.10.2023 11:55:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WorkerToViewingRoom](
	[IDWorker] [int] NOT NULL,
	[IDNumberViewingRoom] [int] NOT NULL,
 CONSTRAINT [PK_WorkerToViewingRoom_1] PRIMARY KEY CLUSTERED 
(
	[IDWorker] ASC,
	[IDNumberViewingRoom] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[ViewWorkerToRoom]    Script Date: 25.10.2023 11:55:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ViewWorkerToRoom]
AS
SELECT        dbo.ViewingRoom.*, dbo.Worker.Patronymic, dbo.Worker.Secondname
FROM            dbo.WorkerToViewingRoom INNER JOIN
                         dbo.ViewingRoom ON dbo.WorkerToViewingRoom.IDNumberViewingRoom = dbo.ViewingRoom.ID INNER JOIN
                         dbo.Worker ON dbo.WorkerToViewingRoom.IDWorker = dbo.Worker.ID
GO
/****** Object:  Table [dbo].[Country]    Script Date: 25.10.2023 11:55:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Country](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[NameCountry] [nvarchar](max) NULL,
 CONSTRAINT [PK_Country] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CountryToFilm]    Script Date: 25.10.2023 11:55:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CountryToFilm](
	[IDCountry] [int] NOT NULL,
	[IDFilm] [int] NOT NULL,
 CONSTRAINT [PK_CountryToFilm_1] PRIMARY KEY CLUSTERED 
(
	[IDCountry] ASC,
	[IDFilm] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Food]    Script Date: 25.10.2023 11:55:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Food](
	[ID] [int] NOT NULL,
	[NameFood] [nvarchar](150) NOT NULL,
	[Cost] [int] NOT NULL,
	[Photo] [varbinary](max) NULL,
 CONSTRAINT [PK_Food] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FoodToTicket]    Script Date: 25.10.2023 11:55:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FoodToTicket](
	[IDTicket] [int] NOT NULL,
	[IDFood] [int] NOT NULL,
 CONSTRAINT [PK_FoodToViewer_1] PRIMARY KEY CLUSTERED 
(
	[IDTicket] ASC,
	[IDFood] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MovieSchedule]    Script Date: 25.10.2023 11:55:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MovieSchedule](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MovieId] [int] NOT NULL,
	[ScreeningTime] [datetime] NOT NULL,
	[IDRoom] [int] NULL,
	[Duration] [int] NOT NULL,
 CONSTRAINT [PK__MovieSch__3214EC0788E25511] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ticket]    Script Date: 25.10.2023 11:55:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ticket](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[IDFilm] [int] NOT NULL,
	[TotalSum] [int] NOT NULL,
	[IDMovieSchedule] [int] NOT NULL,
	[IDViewer] [int] NOT NULL,
	[IDRooms] [int] NULL,
 CONSTRAINT [PK_Ticket] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Viewer]    Script Date: 25.10.2023 11:55:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Viewer](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](30) NOT NULL,
	[Secondname] [nvarchar](30) NOT NULL,
	[Patronymic] [nvarchar](max) NULL,
	[Telephone] [bigint] NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[DateBirthday] [date] NULL,
	[Photo] [varbinary](max) NULL,
	[Password] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Viewer] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[ActorToFilm]  WITH CHECK ADD  CONSTRAINT [FK_ActorToFilm_Actor1] FOREIGN KEY([IDActor])
REFERENCES [dbo].[Actor] ([ID])
GO
ALTER TABLE [dbo].[ActorToFilm] CHECK CONSTRAINT [FK_ActorToFilm_Actor1]
GO
ALTER TABLE [dbo].[ActorToFilm]  WITH CHECK ADD  CONSTRAINT [FK_ActorToFilm_Movies] FOREIGN KEY([IDFilm])
REFERENCES [dbo].[Movies] ([ID])
GO
ALTER TABLE [dbo].[ActorToFilm] CHECK CONSTRAINT [FK_ActorToFilm_Movies]
GO
ALTER TABLE [dbo].[CountryToFilm]  WITH CHECK ADD  CONSTRAINT [FK_CountryToFilm_Country1] FOREIGN KEY([IDCountry])
REFERENCES [dbo].[Country] ([ID])
GO
ALTER TABLE [dbo].[CountryToFilm] CHECK CONSTRAINT [FK_CountryToFilm_Country1]
GO
ALTER TABLE [dbo].[CountryToFilm]  WITH CHECK ADD  CONSTRAINT [FK_CountryToFilm_Movies] FOREIGN KEY([IDFilm])
REFERENCES [dbo].[Movies] ([ID])
GO
ALTER TABLE [dbo].[CountryToFilm] CHECK CONSTRAINT [FK_CountryToFilm_Movies]
GO
ALTER TABLE [dbo].[FoodToTicket]  WITH CHECK ADD  CONSTRAINT [FK_FoodToTicket_Ticket] FOREIGN KEY([IDTicket])
REFERENCES [dbo].[Ticket] ([ID])
GO
ALTER TABLE [dbo].[FoodToTicket] CHECK CONSTRAINT [FK_FoodToTicket_Ticket]
GO
ALTER TABLE [dbo].[FoodToTicket]  WITH CHECK ADD  CONSTRAINT [FK_FoodToViewer_Food] FOREIGN KEY([IDFood])
REFERENCES [dbo].[Food] ([ID])
GO
ALTER TABLE [dbo].[FoodToTicket] CHECK CONSTRAINT [FK_FoodToViewer_Food]
GO
ALTER TABLE [dbo].[MovieSchedule]  WITH CHECK ADD  CONSTRAINT [FK__MovieSche__Movie__160F4887] FOREIGN KEY([MovieId])
REFERENCES [dbo].[Movies] ([ID])
GO
ALTER TABLE [dbo].[MovieSchedule] CHECK CONSTRAINT [FK__MovieSche__Movie__160F4887]
GO
ALTER TABLE [dbo].[MovieSchedule]  WITH CHECK ADD  CONSTRAINT [FK_MovieSchedule_ViewingRoom] FOREIGN KEY([IDRoom])
REFERENCES [dbo].[ViewingRoom] ([ID])
GO
ALTER TABLE [dbo].[MovieSchedule] CHECK CONSTRAINT [FK_MovieSchedule_ViewingRoom]
GO
ALTER TABLE [dbo].[Ticket]  WITH CHECK ADD  CONSTRAINT [FK_Ticket_Movies] FOREIGN KEY([IDFilm])
REFERENCES [dbo].[Movies] ([ID])
GO
ALTER TABLE [dbo].[Ticket] CHECK CONSTRAINT [FK_Ticket_Movies]
GO
ALTER TABLE [dbo].[Ticket]  WITH CHECK ADD  CONSTRAINT [FK_Ticket_MovieSchedule] FOREIGN KEY([IDMovieSchedule])
REFERENCES [dbo].[MovieSchedule] ([Id])
GO
ALTER TABLE [dbo].[Ticket] CHECK CONSTRAINT [FK_Ticket_MovieSchedule]
GO
ALTER TABLE [dbo].[Ticket]  WITH CHECK ADD  CONSTRAINT [FK_Ticket_Viewer] FOREIGN KEY([IDViewer])
REFERENCES [dbo].[Viewer] ([ID])
GO
ALTER TABLE [dbo].[Ticket] CHECK CONSTRAINT [FK_Ticket_Viewer]
GO
ALTER TABLE [dbo].[WorkerToViewingRoom]  WITH CHECK ADD  CONSTRAINT [FK_WorkerToViewingRoom_ViewingRoom1] FOREIGN KEY([IDNumberViewingRoom])
REFERENCES [dbo].[ViewingRoom] ([ID])
GO
ALTER TABLE [dbo].[WorkerToViewingRoom] CHECK CONSTRAINT [FK_WorkerToViewingRoom_ViewingRoom1]
GO
ALTER TABLE [dbo].[WorkerToViewingRoom]  WITH CHECK ADD  CONSTRAINT [FK_WorkerToViewingRoom_Worker1] FOREIGN KEY([IDWorker])
REFERENCES [dbo].[Worker] ([ID])
GO
ALTER TABLE [dbo].[WorkerToViewingRoom] CHECK CONSTRAINT [FK_WorkerToViewingRoom_Worker1]
GO
/****** Object:  StoredProcedure [dbo].[AddViewer]    Script Date: 25.10.2023 11:55:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AddViewer]
	
    @Name nvarchar(MAX) = NULL,
    @Secondname nvarchar(MAX) = NULL,
    @Patronymic nvarchar(MAX) = NULL,
    @TicketNumber int = NULL,
    @Telephone nvarchar(MAX) = NULL,
    @Email nvarchar(MAX) = NULL,
    @DateBirthday date = NULL,
    @Photo varbinary(MAX) = NULL,
    @Password nvarchar(MAX) = NULL

AS
BEGIN
    SET NOCOUNT ON;
	print 1
    -- Проверка типов данных
    IF NOT(ISNULL(@Name, '') = '' OR LEN(@Name) > 0) OR
       NOT(ISNULL(@Secondname, '') = '' OR LEN(@Secondname) > 0) OR
       NOT(ISNULL(@Patronymic, '') = '' OR LEN(@Patronymic) > 0) OR
    
       NOT(ISNULL(@Telephone, '') = '' OR LEN(@Telephone) > 0) OR
       NOT(ISNULL(@Email, '') = '' OR LEN(@Email) > 0) OR
       NOT(ISNULL(@DateBirthday, '') = '' OR LEN(@DateBirthday) > 0) OR
   
       NOT(ISNULL(@Password, '') = '' OR LEN(@Password) > 0)
    BEGIN
        RAISERROR('Invalid parameter types.', 16, 1);
        RETURN 1;
    END
	
    -- Проверка на допустимость значений
   
   print 2
   
    -- Добавление записи в таблицу
    INSERT INTO Viewer (Name, Secondname, Patronymic, TicketNumber, Telephone, Email, DateBirthday, Photo, Password)
    VALUES (@Name, @Secondname, @Patronymic, @TicketNumber, @Telephone, @Email, @DateBirthday, @Photo, @Password);
	RETURN 0;
END
GO
/****** Object:  StoredProcedure [dbo].[GetDates]    Script Date: 25.10.2023 11:55:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
CREATE PROCEDURE [dbo].[GetDates]
    @MovieId INT
AS
SELECT *
FROM MovieSchedule 

WHERE MovieId = @MovieId
ORDER BY ScreeningTime ASC
GO
/****** Object:  StoredProcedure [dbo].[GetMovieSchedule]    Script Date: 25.10.2023 11:55:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetMovieSchedule]
AS
BEGIN
    SELECT Movies.FilmName, MovieSchedule.ScreeningTime
    FROM Movies
    INNER JOIN MovieSchedule ON MovieSchedule.MovieId = Movies.ID
    ORDER BY Movies.FilmName
END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[41] 4[20] 2[26] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Movies"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 229
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ActorToFilm"
            Begin Extent = 
               Top = 6
               Left = 267
               Bottom = 102
               Right = 441
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Actor"
            Begin Extent = 
               Top = 6
               Left = 479
               Bottom = 136
               Right = 653
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 5025
         Width = 3585
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 3015
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ActorForMovies'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ActorForMovies'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "WorkerToViewingRoom"
            Begin Extent = 
               Top = 8
               Left = 102
               Bottom = 104
               Right = 320
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ViewingRoom"
            Begin Extent = 
               Top = 150
               Left = 63
               Bottom = 263
               Right = 270
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Worker"
            Begin Extent = 
               Top = 6
               Left = 539
               Bottom = 136
               Right = 717
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 5415
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewWorkerToRoom'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewWorkerToRoom'
GO
USE [master]
GO
ALTER DATABASE [Finist] SET  READ_WRITE 
GO
