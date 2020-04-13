USE [master]
GO
/****** Object:  Database [MarketsTracker]    Script Date: 14/04/2020 0:33:00 ******/
CREATE DATABASE [MarketsTracker]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'MarketsTracker', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\MarketsTracker.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'MarketsTracker_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\MarketsTracker_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [MarketsTracker] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MarketsTracker].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [MarketsTracker] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [MarketsTracker] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [MarketsTracker] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [MarketsTracker] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [MarketsTracker] SET ARITHABORT OFF 
GO
ALTER DATABASE [MarketsTracker] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [MarketsTracker] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [MarketsTracker] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [MarketsTracker] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [MarketsTracker] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [MarketsTracker] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [MarketsTracker] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [MarketsTracker] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [MarketsTracker] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [MarketsTracker] SET  DISABLE_BROKER 
GO
ALTER DATABASE [MarketsTracker] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [MarketsTracker] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [MarketsTracker] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [MarketsTracker] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [MarketsTracker] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [MarketsTracker] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [MarketsTracker] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [MarketsTracker] SET RECOVERY FULL 
GO
ALTER DATABASE [MarketsTracker] SET  MULTI_USER 
GO
ALTER DATABASE [MarketsTracker] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [MarketsTracker] SET DB_CHAINING OFF 
GO
ALTER DATABASE [MarketsTracker] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [MarketsTracker] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [MarketsTracker] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'MarketsTracker', N'ON'
GO
ALTER DATABASE [MarketsTracker] SET QUERY_STORE = OFF
GO
USE [MarketsTracker]
GO
/****** Object:  Table [dbo].[instrument]    Script Date: 14/04/2020 0:33:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[instrument](
	[instrumentId] [int] NOT NULL,
	[name] [varchar](41) NOT NULL,
	[symbol] [varchar](7) NOT NULL,
	[instrumentType] [varchar](9) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[instrumentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[user]    Script Date: 14/04/2020 0:33:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[user](
	[userId] [int] IDENTITY(1,1) NOT NULL,
	[userName] [varchar](200) NOT NULL,
	[firstName] [varchar](200) NOT NULL,
	[lastName] [varchar](200) NOT NULL,
	[passwordHash] [varbinary](max) NOT NULL,
	[passwordSalt] [varbinary](max) NOT NULL,
 CONSTRAINT [PK_users] PRIMARY KEY CLUSTERED 
(
	[userId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[userInstrument]    Script Date: 14/04/2020 0:33:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[userInstrument](
	[userId] [int] NOT NULL,
	[instrumentId] [int] NOT NULL,
 CONSTRAINT [PK_usersInstruments] PRIMARY KEY CLUSTERED 
(
	[userId] ASC,
	[instrumentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[instrument] ([instrumentId], [name], [symbol], [instrumentType]) VALUES (1, N'Euro US Dollar', N'EUR/USD', N'currency')
INSERT [dbo].[instrument] ([instrumentId], [name], [symbol], [instrumentType]) VALUES (6, N'Euro British Pound', N'EUR/GBP', N'currency')
INSERT [dbo].[instrument] ([instrumentId], [name], [symbol], [instrumentType]) VALUES (9, N'Euro Japanese Yen', N'EUR/JPY', N'currency')
INSERT [dbo].[instrument] ([instrumentId], [name], [symbol], [instrumentType]) VALUES (10, N'Euro Swiss Franc', N'EUR/CHF', N'currency')
INSERT [dbo].[instrument] ([instrumentId], [name], [symbol], [instrumentType]) VALUES (15, N'Euro Australian Dollar', N'EUR/AUD', N'currency')
INSERT [dbo].[instrument] ([instrumentId], [name], [symbol], [instrumentType]) VALUES (16, N'Euro Canadian Dollar', N'EUR/CAD', N'currency')
INSERT [dbo].[instrument] ([instrumentId], [name], [symbol], [instrumentType]) VALUES (52, N'Euro New Zealand Dollar', N'EUR/NZD', N'currency')
INSERT [dbo].[instrument] ([instrumentId], [name], [symbol], [instrumentType]) VALUES (175, N'Euro Stoxx 50', N'STOXX50', N'indice')
INSERT [dbo].[instrument] ([instrumentId], [name], [symbol], [instrumentType]) VALUES (1487, N'Australian Dollar Euro', N'AUD/EUR', N'currency')
INSERT [dbo].[instrument] ([instrumentId], [name], [symbol], [instrumentType]) VALUES (1525, N'Canadian Dollar Euro', N'CAD/EUR', N'currency')
INSERT [dbo].[instrument] ([instrumentId], [name], [symbol], [instrumentType]) VALUES (2124, N'US Dollar Euro', N'USD/EUR', N'currency')
INSERT [dbo].[instrument] ([instrumentId], [name], [symbol], [instrumentType]) VALUES (15978, N'Euronet Worldwide Inc', N'EEFT', N'equities')
INSERT [dbo].[instrument] ([instrumentId], [name], [symbol], [instrumentType]) VALUES (956731, N'Investing.com Euro Index', N'inveur', N'indice')
INSERT [dbo].[instrument] ([instrumentId], [name], [symbol], [instrumentType]) VALUES (976573, N'Sygnia Itrix Euro Stoxx 50 ETF', N'SYGEUJ', N'etf')
INSERT [dbo].[instrument] ([instrumentId], [name], [symbol], [instrumentType]) VALUES (997393, N'NewWave EUR Currency Exchange Traded Note', N'NEWEURJ', N'etf')
INSERT [dbo].[instrument] ([instrumentId], [name], [symbol], [instrumentType]) VALUES (998227, N'Diesel European Gasoil Futures', N'DSEL1c1', N'commodity')
SET ANSI_PADDING ON
GO
/****** Object:  Index [IDX_USERS_USERNAME]    Script Date: 14/04/2020 0:33:01 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IDX_USERS_USERNAME] ON [dbo].[user]
(
	[userName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[userInstrument]  WITH CHECK ADD  CONSTRAINT [FK_usersInstrument_instrument] FOREIGN KEY([instrumentId])
REFERENCES [dbo].[instrument] ([instrumentId])
GO
ALTER TABLE [dbo].[userInstrument] CHECK CONSTRAINT [FK_usersInstrument_instrument]
GO
ALTER TABLE [dbo].[userInstrument]  WITH CHECK ADD  CONSTRAINT [FK_usersInstrument_user] FOREIGN KEY([userId])
REFERENCES [dbo].[user] ([userId])
GO
ALTER TABLE [dbo].[userInstrument] CHECK CONSTRAINT [FK_usersInstrument_user]
GO
USE [master]
GO
ALTER DATABASE [MarketsTracker] SET  READ_WRITE 
GO
