USE [master]
GO
/****** Object:  Database [FotoWorldTest]    Script Date: 16.03.2023 22:48:04 ******/
CREATE DATABASE [FotoWorldTest]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'FotoWorldTest', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.YARIDSQLSERVER\MSSQL\DATA\FotoWorldTest.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'FotoWorldTest_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.YARIDSQLSERVER\MSSQL\DATA\FotoWorldTest_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [FotoWorldTest] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [FotoWorldTest].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [FotoWorldTest] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [FotoWorldTest] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [FotoWorldTest] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [FotoWorldTest] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [FotoWorldTest] SET ARITHABORT OFF 
GO
ALTER DATABASE [FotoWorldTest] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [FotoWorldTest] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [FotoWorldTest] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [FotoWorldTest] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [FotoWorldTest] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [FotoWorldTest] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [FotoWorldTest] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [FotoWorldTest] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [FotoWorldTest] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [FotoWorldTest] SET  DISABLE_BROKER 
GO
ALTER DATABASE [FotoWorldTest] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [FotoWorldTest] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [FotoWorldTest] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [FotoWorldTest] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [FotoWorldTest] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [FotoWorldTest] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [FotoWorldTest] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [FotoWorldTest] SET RECOVERY FULL 
GO
ALTER DATABASE [FotoWorldTest] SET  MULTI_USER 
GO
ALTER DATABASE [FotoWorldTest] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [FotoWorldTest] SET DB_CHAINING OFF 
GO
ALTER DATABASE [FotoWorldTest] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [FotoWorldTest] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [FotoWorldTest] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [FotoWorldTest] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'FotoWorldTest', N'ON'
GO
ALTER DATABASE [FotoWorldTest] SET QUERY_STORE = ON
GO
ALTER DATABASE [FotoWorldTest] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [FotoWorldTest]
GO
/****** Object:  Table [dbo].[FollowedOffers]    Script Date: 16.03.2023 22:48:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FollowedOffers](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[offerID] [int] NOT NULL,
	[userID] [int] NOT NULL,
 CONSTRAINT [PK_FollowedOffers] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OfferPhotos]    Script Date: 16.03.2023 22:48:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OfferPhotos](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[offerID] [int] NOT NULL,
	[photoID] [int] NOT NULL,
 CONSTRAINT [PK_OfferPhotos] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Offers]    Script Date: 16.03.2023 22:48:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Offers](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[operatorID] [int] NOT NULL,
	[title] [nvarchar](100) NOT NULL,
	[description] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Offers] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OperatorRatings]    Script Date: 16.03.2023 22:48:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OperatorRatings](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[operatorID] [int] NOT NULL,
	[userID] [int] NOT NULL,
	[stars] [float] NOT NULL,
	[comment] [nvarchar](500) NOT NULL,
 CONSTRAINT [PK_OperatorRatings] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Operators]    Script Date: 16.03.2023 22:48:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Operators](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[accountID] [int] NOT NULL,
	[isCompany] [bit] NOT NULL,
	[availability] [nvarchar](50) NOT NULL,
	[locationCity] [nvarchar](150) NOT NULL,
	[operatingRadius] [int] NOT NULL,
	[photo] [bit] NOT NULL,
	[dronePhoto] [bit] NOT NULL,
	[filming] [bit] NOT NULL,
	[droneFilming] [bit] NOT NULL,
 CONSTRAINT [PK_Operators] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Photos]    Script Date: 16.03.2023 22:48:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Photos](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[photoURL] [nvarchar](200) NOT NULL,
 CONSTRAINT [PK_Photos] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 16.03.2023 22:48:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[username] [nvarchar](50) NOT NULL,
	[hashedPassword] [nvarchar](250) NOT NULL,
	[passwordSalt] [nvarchar](250) NOT NULL,
	[email] [nvarchar](100) NOT NULL,
	[phoneNumber] [nvarchar](20) NOT NULL,
	[isActive] [bit] NOT NULL,
	[isOperator] [bit] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[FollowedOffers] ON 

INSERT [dbo].[FollowedOffers] ([ID], [offerID], [userID]) VALUES (1, 1, 2)
SET IDENTITY_INSERT [dbo].[FollowedOffers] OFF
GO
SET IDENTITY_INSERT [dbo].[OfferPhotos] ON 

INSERT [dbo].[OfferPhotos] ([ID], [offerID], [photoID]) VALUES (1, 1, 1)
INSERT [dbo].[OfferPhotos] ([ID], [offerID], [photoID]) VALUES (2, 1, 2)
INSERT [dbo].[OfferPhotos] ([ID], [offerID], [photoID]) VALUES (3, 1, 3)
INSERT [dbo].[OfferPhotos] ([ID], [offerID], [photoID]) VALUES (4, 1, 4)
INSERT [dbo].[OfferPhotos] ([ID], [offerID], [photoID]) VALUES (5, 2, 5)
INSERT [dbo].[OfferPhotos] ([ID], [offerID], [photoID]) VALUES (6, 2, 6)
INSERT [dbo].[OfferPhotos] ([ID], [offerID], [photoID]) VALUES (7, 2, 7)
INSERT [dbo].[OfferPhotos] ([ID], [offerID], [photoID]) VALUES (8, 2, 8)
SET IDENTITY_INSERT [dbo].[OfferPhotos] OFF
GO
SET IDENTITY_INSERT [dbo].[Offers] ON 

INSERT [dbo].[Offers] ([ID], [operatorID], [title], [description]) VALUES (1, 3, N'offerTitle1', N'offerDesc1')
INSERT [dbo].[Offers] ([ID], [operatorID], [title], [description]) VALUES (2, 4, N'offerTitle2', N'offerDesc2')
SET IDENTITY_INSERT [dbo].[Offers] OFF
GO
SET IDENTITY_INSERT [dbo].[OperatorRatings] ON 

INSERT [dbo].[OperatorRatings] ([ID], [operatorID], [userID], [stars], [comment]) VALUES (1, 3, 2, 5, N'opinion1')
INSERT [dbo].[OperatorRatings] ([ID], [operatorID], [userID], [stars], [comment]) VALUES (2, 4, 2, 1, N'opinion2')
SET IDENTITY_INSERT [dbo].[OperatorRatings] OFF
GO
SET IDENTITY_INSERT [dbo].[Operators] ON 

INSERT [dbo].[Operators] ([ID], [accountID], [isCompany], [availability], [locationCity], [operatingRadius], [photo], [dronePhoto], [filming], [droneFilming]) VALUES (3, 6, 1, N'pon-nd', N'Wroclaw', 100, 1, 1, 1, 1)
INSERT [dbo].[Operators] ([ID], [accountID], [isCompany], [availability], [locationCity], [operatingRadius], [photo], [dronePhoto], [filming], [droneFilming]) VALUES (4, 7, 0, N'pon-nd', N'Wroclaw', 100, 0, 0, 1, 1)
SET IDENTITY_INSERT [dbo].[Operators] OFF
GO
SET IDENTITY_INSERT [dbo].[Photos] ON 

INSERT [dbo].[Photos] ([ID], [photoURL]) VALUES (1, N'photoPath1')
INSERT [dbo].[Photos] ([ID], [photoURL]) VALUES (2, N'photoPath2')
INSERT [dbo].[Photos] ([ID], [photoURL]) VALUES (3, N'photoPath3')
INSERT [dbo].[Photos] ([ID], [photoURL]) VALUES (4, N'photoPath4')
INSERT [dbo].[Photos] ([ID], [photoURL]) VALUES (5, N'photoPath5')
INSERT [dbo].[Photos] ([ID], [photoURL]) VALUES (6, N'photoPath6')
INSERT [dbo].[Photos] ([ID], [photoURL]) VALUES (7, N'photoPath7')
INSERT [dbo].[Photos] ([ID], [photoURL]) VALUES (8, N'photoPath8')
SET IDENTITY_INSERT [dbo].[Photos] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([ID], [username], [hashedPassword], [passwordSalt], [email], [phoneNumber], [isActive], [isOperator]) VALUES (2, N'user1', N'i9vFiizmVv/Ivc+xe+FDLD7okZ7dDRQAfGb5TFmYFjA=', N'Id3o3sBXquL6zZ1TOaDeDw==', N'email1@email.com', N'123456789', 1, 0)
INSERT [dbo].[Users] ([ID], [username], [hashedPassword], [passwordSalt], [email], [phoneNumber], [isActive], [isOperator]) VALUES (3, N'user2', N'cxcEsFtlb3pUCXcmnUADC0BnUzjzNew/Mbe4IjFYmPI=', N'BO7nqxp7macX/Aq0E63jJw==', N'email2@email.com', N'123456789', 0, 0)
INSERT [dbo].[Users] ([ID], [username], [hashedPassword], [passwordSalt], [email], [phoneNumber], [isActive], [isOperator]) VALUES (6, N'operator1', N'2gNxA6lZCshfFkvm8k3WjA90BLrtjVURHywwDRMNSLc=', N'nPzCR2/1VRm3s9hb5t5+FA==', N'emailop1@email.com', N'123456789', 1, 1)
INSERT [dbo].[Users] ([ID], [username], [hashedPassword], [passwordSalt], [email], [phoneNumber], [isActive], [isOperator]) VALUES (7, N'operator2', N'4D2KTPyqp24T+EAyWnh+gyUVNFF0ROtwwNW/TRx0irs=', N'R5/WScVUPwQHNalIfxvk1A==', N'emailop2@email.com', N'123456789', 1, 1)
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_isActive]  DEFAULT ((0)) FOR [isActive]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_isOperator]  DEFAULT ((0)) FOR [isOperator]
GO
ALTER TABLE [dbo].[FollowedOffers]  WITH CHECK ADD  CONSTRAINT [FK_FollowedOffers_Offers] FOREIGN KEY([offerID])
REFERENCES [dbo].[Offers] ([ID])
GO
ALTER TABLE [dbo].[FollowedOffers] CHECK CONSTRAINT [FK_FollowedOffers_Offers]
GO
ALTER TABLE [dbo].[FollowedOffers]  WITH CHECK ADD  CONSTRAINT [FK_FollowedOffers_Users] FOREIGN KEY([userID])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[FollowedOffers] CHECK CONSTRAINT [FK_FollowedOffers_Users]
GO
ALTER TABLE [dbo].[OfferPhotos]  WITH CHECK ADD  CONSTRAINT [FK_OfferPhotos_Offers] FOREIGN KEY([offerID])
REFERENCES [dbo].[Offers] ([ID])
GO
ALTER TABLE [dbo].[OfferPhotos] CHECK CONSTRAINT [FK_OfferPhotos_Offers]
GO
ALTER TABLE [dbo].[OfferPhotos]  WITH CHECK ADD  CONSTRAINT [FK_OfferPhotos_Photos] FOREIGN KEY([photoID])
REFERENCES [dbo].[Photos] ([ID])
GO
ALTER TABLE [dbo].[OfferPhotos] CHECK CONSTRAINT [FK_OfferPhotos_Photos]
GO
ALTER TABLE [dbo].[Offers]  WITH CHECK ADD  CONSTRAINT [FK_Offers_Operators] FOREIGN KEY([operatorID])
REFERENCES [dbo].[Operators] ([ID])
GO
ALTER TABLE [dbo].[Offers] CHECK CONSTRAINT [FK_Offers_Operators]
GO
ALTER TABLE [dbo].[OperatorRatings]  WITH CHECK ADD  CONSTRAINT [FK_OperatorRatings_Operators] FOREIGN KEY([operatorID])
REFERENCES [dbo].[Operators] ([ID])
GO
ALTER TABLE [dbo].[OperatorRatings] CHECK CONSTRAINT [FK_OperatorRatings_Operators]
GO
ALTER TABLE [dbo].[OperatorRatings]  WITH CHECK ADD  CONSTRAINT [FK_OperatorRatings_Users] FOREIGN KEY([userID])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[OperatorRatings] CHECK CONSTRAINT [FK_OperatorRatings_Users]
GO
ALTER TABLE [dbo].[Operators]  WITH CHECK ADD  CONSTRAINT [FK_Operators_Users] FOREIGN KEY([accountID])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Operators] CHECK CONSTRAINT [FK_Operators_Users]
GO
USE [master]
GO
ALTER DATABASE [FotoWorldTest] SET  READ_WRITE 
GO
