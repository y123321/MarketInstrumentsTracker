
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
