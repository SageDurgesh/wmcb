USE [master]
GO
/****** Object:  Database [wmcb]    Script Date: 5/16/2015 11:26:20 PM ******/
CREATE DATABASE [wmcb]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'wmcb', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.WMCB\MSSQL\DATA\wmcb.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'wmcb_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.WMCB\MSSQL\DATA\wmcb_log.ldf' , SIZE = 3456KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [wmcb] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [wmcb].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [wmcb] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [wmcb] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [wmcb] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [wmcb] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [wmcb] SET ARITHABORT OFF 
GO
ALTER DATABASE [wmcb] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [wmcb] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [wmcb] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [wmcb] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [wmcb] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [wmcb] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [wmcb] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [wmcb] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [wmcb] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [wmcb] SET  DISABLE_BROKER 
GO
ALTER DATABASE [wmcb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [wmcb] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [wmcb] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [wmcb] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [wmcb] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [wmcb] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [wmcb] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [wmcb] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [wmcb] SET  MULTI_USER 
GO
ALTER DATABASE [wmcb] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [wmcb] SET DB_CHAINING OFF 
GO
ALTER DATABASE [wmcb] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [wmcb] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [wmcb] SET DELAYED_DURABILITY = DISABLED 
GO
USE [wmcb]
GO
/****** Object:  User [wmcb_cust]    Script Date: 5/16/2015 11:26:21 PM ******/
CREATE USER [wmcb_cust] WITHOUT LOGIN WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_datareader] ADD MEMBER [wmcb_cust]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [wmcb_cust]
GO
/****** Object:  Table [dbo].[Division]    Script Date: 5/16/2015 11:26:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Division](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](500) NOT NULL,
	[TeamId] [int] NOT NULL,
 CONSTRAINT [PK_Division] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Documents]    Script Date: 5/16/2015 11:26:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Documents](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](500) NOT NULL,
	[Path] [nvarchar](500) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[DocumentType] [int] NOT NULL,
 CONSTRAINT [PK_Documents] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Grounds]    Script Date: 5/16/2015 11:26:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Grounds](
	[ID] [int] NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[Address] [varchar](500) NULL,
	[PermitLink] [nvarchar](500) NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_Grounds] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Matches]    Script Date: 5/16/2015 11:26:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Matches](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](200) NOT NULL,
	[HomeTeamId] [int] NULL,
	[AwayTeamId] [int] NULL,
	[TeamWon] [int] NULL,
	[IsReviewed] [bit] NULL,
	[HomeTeamScore] [int] NULL,
	[AwayTeamScore] [int] NULL,
 CONSTRAINT [PK_Matches] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[NewsFeed]    Script Date: 5/16/2015 11:26:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[NewsFeed](
	[ID] [int] NOT NULL,
	[Headline] [varchar](500) NOT NULL,
	[Content] [text] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_NewsFeed] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Players]    Script Date: 5/16/2015 11:26:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Players](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[TeamId] [int] NOT NULL,
 CONSTRAINT [PK_Players] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PlayerStats]    Script Date: 5/16/2015 11:26:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PlayerStats](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PlayerId] [int] NOT NULL,
	[TeamId] [int] NOT NULL,
	[MatchId] [int] NOT NULL,
	[BallsFaced] [int] NULL,
	[BattingRuns] [int] NULL,
	[HowOut] [int] NULL,
	[BowlerNumber] [int] NULL,
	[Bowler] [int] NULL,
	[BowlingRuns] [int] NULL,
	[OversBowled] [decimal](18, 0) NULL,
	[Wickets] [int] NULL,
	[MaidenOvers] [int] NULL,
	[Fielder] [int] NULL,
 CONSTRAINT [PK_PlayerStats] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Roles]    Script Date: 5/16/2015 11:26:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Roles](
	[ID] [int] NOT NULL,
	[Name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Schedule]    Script Date: 5/16/2015 11:26:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Schedule](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Tournament] [varchar](100) NULL,
	[MatchDate] [date] NULL,
	[Week] [varchar](100) NULL,
	[Day] [varchar](100) NULL,
	[Date] [varchar](100) NULL,
	[Division] [varchar](100) NULL,
	[Home] [varchar](100) NULL,
	[Away] [varchar](100) NULL,
	[Field] [varchar](100) NULL,
	[Time] [varchar](100) NULL,
 CONSTRAINT [PK_Schedule] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Sponsors]    Script Date: 5/16/2015 11:26:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sponsors](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[SponsorshipType] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Sponsors] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SponsorshipType]    Script Date: 5/16/2015 11:26:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SponsorshipType](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Description] [varchar](100) NOT NULL,
 CONSTRAINT [PK_SponsorshipType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TeamPlayers]    Script Date: 5/16/2015 11:26:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TeamPlayers](
	[TeamId] [int] NOT NULL,
	[PlayerId] [int] NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Teams]    Script Date: 5/16/2015 11:26:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Teams](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[ContactID1] [int] NULL,
	[ContactID2] [int] NULL,
	[ContactID3] [int] NULL,
	[Active] [bit] NOT NULL,
	[Points] [int] NULL,
 CONSTRAINT [PK_Teams] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

/****** Object:  Table [dbo].[Tournament]    Script Date: 5/16/2015 11:26:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Tournament](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](500) NOT NULL,
 CONSTRAINT [PK_Tournament] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UserHistory]    Script Date: 5/16/2015 11:26:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserHistory](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[TeamID] [int] NOT NULL,
	[LastDate] [datetime] NOT NULL,
 CONSTRAINT [PK_UserHistory] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserRoles]    Script Date: 5/16/2015 11:26:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRoles](
	[UserID] [int] NOT NULL,
	[RoleID] [int] NOT NULL,
 CONSTRAINT [PK_UserRoles] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC,
	[RoleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Users]    Script Date: 5/16/2015 11:26:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Users](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](100) NOT NULL,
	[LastName] [varchar](100) NOT NULL,
	[Email] [varchar](200) NOT NULL,
	[Password] [varchar](500) NULL,
	[Phone] [varchar](50) NULL,
	[PhotoID] [int] NULL,
	[AllowLogin] [bit] NOT NULL,
	[RegDate] [datetime] NULL,
	[TeamId] [int] NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Division]  WITH CHECK ADD  CONSTRAINT [FK_DivisionTeam_ID] FOREIGN KEY([TeamId])
REFERENCES [dbo].[Teams] ([ID])
GO
ALTER TABLE [dbo].[Division] CHECK CONSTRAINT [FK_DivisionTeam_ID]
GO
ALTER TABLE [dbo].[Matches]  WITH CHECK ADD  CONSTRAINT [FK_Team_ID1] FOREIGN KEY([HomeTeamId])
REFERENCES [dbo].[Teams] ([ID])
GO
ALTER TABLE [dbo].[Matches] CHECK CONSTRAINT [FK_Team_ID1]
GO
ALTER TABLE [dbo].[Matches]  WITH CHECK ADD  CONSTRAINT [FK_Team_ID2] FOREIGN KEY([AwayTeamId])
REFERENCES [dbo].[Teams] ([ID])
GO
ALTER TABLE [dbo].[Matches] CHECK CONSTRAINT [FK_Team_ID2]
GO
ALTER TABLE [dbo].[Matches]  WITH CHECK ADD  CONSTRAINT [FK_TeamWon_ID] FOREIGN KEY([TeamWon])
REFERENCES [dbo].[Teams] ([ID])
GO
ALTER TABLE [dbo].[Matches] CHECK CONSTRAINT [FK_TeamWon_ID]
GO
ALTER TABLE [dbo].[NewsFeed]  WITH CHECK ADD  CONSTRAINT [FK_NewsFeed_Users] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[NewsFeed] CHECK CONSTRAINT [FK_NewsFeed_Users]
GO
ALTER TABLE [dbo].[NewsFeed]  WITH CHECK ADD  CONSTRAINT [FK_NewsFeed_Users1] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[NewsFeed] CHECK CONSTRAINT [FK_NewsFeed_Users1]
GO
ALTER TABLE [dbo].[Players]  WITH CHECK ADD  CONSTRAINT [FK_Teams_ID] FOREIGN KEY([TeamId])
REFERENCES [dbo].[Teams] ([ID])
GO
ALTER TABLE [dbo].[Players] CHECK CONSTRAINT [FK_Teams_ID]
GO
ALTER TABLE [dbo].[PlayerStats]  WITH CHECK ADD  CONSTRAINT [FK_Match_ID] FOREIGN KEY([MatchId])
REFERENCES [dbo].[Matches] ([ID])
GO
ALTER TABLE [dbo].[PlayerStats] CHECK CONSTRAINT [FK_Match_ID]
GO
ALTER TABLE [dbo].[PlayerStats]  WITH CHECK ADD  CONSTRAINT [FK_Players_ID] FOREIGN KEY([PlayerId])
REFERENCES [dbo].[Players] ([ID])
GO
ALTER TABLE [dbo].[PlayerStats] CHECK CONSTRAINT [FK_Players_ID]
GO
ALTER TABLE [dbo].[PlayerStats]  WITH CHECK ADD  CONSTRAINT [FK_Team_ID] FOREIGN KEY([TeamId])
REFERENCES [dbo].[Teams] ([ID])
GO
ALTER TABLE [dbo].[PlayerStats] CHECK CONSTRAINT [FK_Team_ID]
GO
ALTER TABLE [dbo].[TeamPlayers]  WITH CHECK ADD  CONSTRAINT [FK_TeamPlayer_ID] FOREIGN KEY([TeamId])
REFERENCES [dbo].[Teams] ([ID])
GO
ALTER TABLE [dbo].[TeamPlayers] CHECK CONSTRAINT [FK_TeamPlayer_ID]
GO
ALTER TABLE [dbo].[UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_UserRoles_Roles] FOREIGN KEY([RoleID])
REFERENCES [dbo].[Roles] ([ID])
GO
ALTER TABLE [dbo].[UserRoles] CHECK CONSTRAINT [FK_UserRoles_Roles]
GO
ALTER TABLE [dbo].[UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_UserRoles_Users] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[UserRoles] CHECK CONSTRAINT [FK_UserRoles_Users]
GO
USE [master]
GO
ALTER DATABASE [wmcb] SET  READ_WRITE 
GO
