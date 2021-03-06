USE [master]
GO
/****** Object:  Database [GameStats]    Script Date: 20/05/2022 12:39:00 a. m. ******/
CREATE DATABASE [GameStats]
ALTER DATABASE [GameStats] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [GameStats].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [GameStats] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [GameStats] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [GameStats] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [GameStats] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [GameStats] SET ARITHABORT OFF 
GO
ALTER DATABASE [GameStats] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [GameStats] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [GameStats] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [GameStats] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [GameStats] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [GameStats] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [GameStats] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [GameStats] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [GameStats] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [GameStats] SET  ENABLE_BROKER 
GO
ALTER DATABASE [GameStats] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [GameStats] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [GameStats] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [GameStats] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [GameStats] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [GameStats] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [GameStats] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [GameStats] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [GameStats] SET  MULTI_USER 
GO
ALTER DATABASE [GameStats] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [GameStats] SET DB_CHAINING OFF 
GO
ALTER DATABASE [GameStats] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [GameStats] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [GameStats] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [GameStats] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [GameStats] SET QUERY_STORE = OFF
GO
USE [GameStats]
GO
/****** Object:  Table [dbo].[AdvancedGameStats]    Script Date: 20/05/2022 12:39:00 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AdvancedGameStats](
	[AdvancedGameStatsId] [int] IDENTITY(1,1) NOT NULL,
	[StatsId] [int] NOT NULL,
	[ChampionName] [varchar](20) NOT NULL,
	[DamageDealtToBuildings] [int] NOT NULL,
	[DamagDealtToObjectives] [int] NOT NULL,
	[DamageDealtToTowers] [int] NOT NULL,
	[DamageSelfMitigated] [int] NOT NULL,
	[DetectorWardsPlaced] [int] NOT NULL,
	[DragonKills] [int] NOT NULL,
	[FirstBloodAssit] [bit] NOT NULL,
	[FirstBloodKill] [bit] NOT NULL,
	[FirstTowerAssits] [bit] NOT NULL,
	[FirstTowerKill] [bit] NOT NULL,
	[GoldEarned] [int] NOT NULL,
	[GoldSpent] [int] NOT NULL,
	[TeamPosition] [varchar](8) NOT NULL,
	[InhibitorKills] [int] NOT NULL,
	[InhibitorTakedowns] [int] NOT NULL,
	[InhibitorsLost] [int] NOT NULL,
	[MagicDamageDealtToChampions] [int] NOT NULL,
	[MagicDamageTaken] [int] NOT NULL,
	[LargetstCriticalStrike] [int] NOT NULL,
	[ObjectivesStolen] [int] NOT NULL,
	[PhysicalDamagDealtToChampions] [int] NOT NULL,
	[PhysicalDamageTaken] [int] NOT NULL,
	[Spell1Casts] [int] NOT NULL,
	[Spell2Casts] [int] NOT NULL,
	[Spell3Casts] [int] NOT NULL,
	[Spell4Casts] [int] NOT NULL,
	[Summoner1Casts] [int] NOT NULL,
	[Summoner1Id] [int] NOT NULL,
	[Summoner2Cast] [int] NOT NULL,
	[Summoner2Id] [int] NOT NULL,
	[TimeCCingOthers] [int] NOT NULL,
	[TotalDamageShieldedOnTeammates] [int] NOT NULL,
	[TrueDamageDealtToChampions] [int] NOT NULL,
	[TrueDamageTaken] [int] NOT NULL,
	[TurretKills] [int] NOT NULL,
	[TurretTakedowns] [int] NOT NULL,
	[TurretsLost] [int] NOT NULL,
	[VisionScore] [int] NOT NULL,
	[WardsKilled] [int] NOT NULL,
	[WardsPlaced] [int] NOT NULL,
 CONSTRAINT [PKAdvStats] PRIMARY KEY CLUSTERED 
(
	[AdvancedGameStatsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ChampionMastery]    Script Date: 20/05/2022 12:39:00 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChampionMastery](
	[ChampionMasteryId] [int] IDENTITY(1,1) NOT NULL,
	[SummonerId] [varchar](50) NOT NULL,
	[ChampionId] [int] NOT NULL,
	[ChampionLevel] [int] NOT NULL,
	[ChampionPoints] [int] NOT NULL,
	[ChestGranted] [bit] NOT NULL,
	[TokensEarned] [int] NOT NULL,
 CONSTRAINT [PKChampionMastery] PRIMARY KEY CLUSTERED 
(
	[ChampionMasteryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Games]    Script Date: 20/05/2022 12:39:00 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Games](
	[GameId] [int] IDENTITY(1,1) NOT NULL,
	[summonerId] [varchar](50) NOT NULL,
	[matchId] [varchar](15) NOT NULL,
	[gameStartTimestamp] [bigint] NOT NULL,
	[gameDuration] [int] NOT NULL,
	[gameMode] [varchar](15) NOT NULL,
 CONSTRAINT [PKGame] PRIMARY KEY CLUSTERED 
(
	[GameId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GameStats]    Script Date: 20/05/2022 12:39:00 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GameStats](
	[StatsId] [int] IDENTITY(1,1) NOT NULL,
	[GameId] [int] NOT NULL,
	[ChampLevel] [int] NOT NULL,
	[ChampionId] [int] NOT NULL,
	[Win] [bit] NOT NULL,
	[Kills] [int] NOT NULL,
	[Deaths] [int] NOT NULL,
	[Assists] [int] NOT NULL,
	[TotalMinions] [int] NOT NULL,
	[TotalDamageDealtToChampions] [int] NOT NULL,
	[Doubles] [int] NOT NULL,
	[Triples] [int] NOT NULL,
	[Quadras] [int] NOT NULL,
	[Pentas] [int] NOT NULL,
	[Items1] [int] NOT NULL,
	[Items2] [int] NOT NULL,
	[Items3] [int] NOT NULL,
	[Items4] [int] NOT NULL,
	[Items5] [int] NOT NULL,
	[Items6] [int] NOT NULL,
 CONSTRAINT [PKGameStats] PRIMARY KEY CLUSTERED 
(
	[StatsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Leagues]    Script Date: 20/05/2022 12:39:00 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Leagues](
	[LeagueId] [int] IDENTITY(1,1) NOT NULL,
	[SummonerId] [varchar](50) NOT NULL,
	[QueueId] [int] NOT NULL,
	[LeaguePoints] [int] NOT NULL,
	[Wins] [int] NOT NULL,
	[Losses] [int] NOT NULL,
	[Veteran] [bit] NOT NULL,
	[Inactive] [bit] NOT NULL,
	[FreshBlood] [bit] NOT NULL,
	[HotStreak] [bit] NOT NULL,
 CONSTRAINT [PKLeagues] PRIMARY KEY CLUSTERED 
(
	[LeagueId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Queues]    Script Date: 20/05/2022 12:39:00 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Queues](
	[QueueId] [int] IDENTITY(1,1) NOT NULL,
	[QueueType] [varchar](15) NOT NULL,
 CONSTRAINT [PKQueue] PRIMARY KEY CLUSTERED 
(
	[QueueId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Regions]    Script Date: 20/05/2022 12:39:00 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Regions](
	[RegionID] [varchar](4) NOT NULL,
	[RegionName] [varchar](30) NOT NULL,
 CONSTRAINT [PKRegion] PRIMARY KEY CLUSTERED 
(
	[RegionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Summoners]    Script Date: 20/05/2022 12:39:00 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Summoners](
	[ID] [varchar](50) NOT NULL,
	[AccountID] [varchar](50) NOT NULL,
	[PUUID] [varchar](80) NOT NULL,
	[SummonerName] [varchar](16) NOT NULL,
	[ProfileIconID] [int] NOT NULL,
	[SummonerLevel] [int] NOT NULL,
 CONSTRAINT [PKSummoner] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 20/05/2022 12:39:00 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[SummonerId] [varchar](50) NOT NULL,
	[RegionId] [varchar](4) NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[UserPassword] [varchar](50) NOT NULL,
	[IsAdmin] [bit] NOT NULL,
	[SummonerName] [varchar](50) NOT NULL,
 CONSTRAINT [PKUsers] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT ('NOEXISTE') FOR [SummonerName]
GO
ALTER TABLE [dbo].[AdvancedGameStats]  WITH CHECK ADD  CONSTRAINT [FKASGameStats] FOREIGN KEY([StatsId])
REFERENCES [dbo].[GameStats] ([StatsId])
GO
ALTER TABLE [dbo].[AdvancedGameStats] CHECK CONSTRAINT [FKASGameStats]
GO
ALTER TABLE [dbo].[ChampionMastery]  WITH CHECK ADD  CONSTRAINT [FKCMSummoner] FOREIGN KEY([SummonerId])
REFERENCES [dbo].[Summoners] ([ID])
GO
ALTER TABLE [dbo].[ChampionMastery] CHECK CONSTRAINT [FKCMSummoner]
GO
ALTER TABLE [dbo].[Games]  WITH CHECK ADD  CONSTRAINT [FKGSummoner] FOREIGN KEY([summonerId])
REFERENCES [dbo].[Summoners] ([ID])
GO
ALTER TABLE [dbo].[Games] CHECK CONSTRAINT [FKGSummoner]
GO
ALTER TABLE [dbo].[GameStats]  WITH CHECK ADD  CONSTRAINT [FKGSGame] FOREIGN KEY([GameId])
REFERENCES [dbo].[Games] ([GameId])
GO
ALTER TABLE [dbo].[GameStats] CHECK CONSTRAINT [FKGSGame]
GO
ALTER TABLE [dbo].[Leagues]  WITH CHECK ADD  CONSTRAINT [FKLQueue] FOREIGN KEY([QueueId])
REFERENCES [dbo].[Queues] ([QueueId])
GO
ALTER TABLE [dbo].[Leagues] CHECK CONSTRAINT [FKLQueue]
GO
ALTER TABLE [dbo].[Leagues]  WITH CHECK ADD  CONSTRAINT [FKLSummoner] FOREIGN KEY([SummonerId])
REFERENCES [dbo].[Summoners] ([ID])
GO
ALTER TABLE [dbo].[Leagues] CHECK CONSTRAINT [FKLSummoner]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [PKURegion] FOREIGN KEY([RegionId])
REFERENCES [dbo].[Regions] ([RegionID])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [PKURegion]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [PKUSummoner] FOREIGN KEY([SummonerId])
REFERENCES [dbo].[Summoners] ([ID])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [PKUSummoner]
GO
USE [master]
GO
ALTER DATABASE [GameStats] SET  READ_WRITE 
GO
use GameStats
INSERT INTO Regions VALUES ('BR1', 'Brasil')
INSERT INTO Regions VALUES ('EUN1', 'Europa Nordica y Este')
INSERT INTO Regions VALUES ('EUW1', 'Europa Este')
INSERT INTO Regions VALUES ('JP1', 'Japón')
INSERT INTO Regions VALUES ('KR', 'Korea')
INSERT INTO Regions VALUES ('LA1', 'Latinoamérica Norte')
INSERT INTO Regions VALUES ('LA2', 'Latinoamérica Sur')
INSERT INTO Regions VALUES ('NA1', 'Norte América')
INSERT INTO Regions VALUES ('OC1', 'Oceanía')
INSERT INTO Regions VALUES ('TR1', 'Turquía')
INSERT INTO Regions VALUES ('RU', 'Rusia')