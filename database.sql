USE [master]
GO
/****** Object:  Database [FotoWorld]    Script Date: 18.02.2023 13:26:44 ******/
CREATE DATABASE [FotoWorld]

GO
ALTER DATABASE [FotoWorld] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [FotoWorld].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [FotoWorld] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [FotoWorld] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [FotoWorld] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [FotoWorld] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [FotoWorld] SET ARITHABORT OFF 
GO
ALTER DATABASE [FotoWorld] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [FotoWorld] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [FotoWorld] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [FotoWorld] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [FotoWorld] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [FotoWorld] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [FotoWorld] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [FotoWorld] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [FotoWorld] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [FotoWorld] SET  DISABLE_BROKER 
GO
ALTER DATABASE [FotoWorld] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [FotoWorld] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [FotoWorld] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [FotoWorld] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [FotoWorld] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [FotoWorld] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [FotoWorld] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [FotoWorld] SET RECOVERY FULL 
GO
ALTER DATABASE [FotoWorld] SET  MULTI_USER 
GO
ALTER DATABASE [FotoWorld] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [FotoWorld] SET DB_CHAINING OFF 
GO
ALTER DATABASE [FotoWorld] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [FotoWorld] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [FotoWorld] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [FotoWorld] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'FotoWorld', N'ON'
GO
ALTER DATABASE [FotoWorld] SET QUERY_STORE = OFF
GO
USE [FotoWorld]
GO
/****** Object:  Table [dbo].[FollowedOffers]    Script Date: 18.02.2023 13:26:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FollowedOffers](
	[ID] [int] NOT NULL identity(1,1),
	[userID] [int] NOT NULL,
	[offerID] [int] NOT NULL,
 CONSTRAINT [PK_FollowedOffers] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OfferGalleries]    Script Date: 18.02.2023 13:26:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OfferGalleries](
	[ID] [int] NOT NULL identity(1,1),
	[offerID] [int] NOT NULL,
	[photoID] [int] NOT NULL,
 CONSTRAINT [PK_OfferGalleries] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Offers]    Script Date: 18.02.2023 13:26:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Offers](
	[ID] [int] NOT NULL identity(1,1),
	[operatorID] [int] NOT NULL,
	[title] [nvarchar](150) NOT NULL,
	[description] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Offers] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OperatorRatings]    Script Date: 18.02.2023 13:26:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OperatorRatings](
	[ID] [int] NOT NULL identity(1,1),
	[operatorID] [int] NOT NULL,
	[userID] [int] NOT NULL,
	[stars] [float] NOT NULL,
	[comment] [nvarchar](150) NULL,
 CONSTRAINT [PK_OperatorRatings] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Operators]    Script Date: 18.02.2023 13:26:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Operators](
	[ID] [int] NOT NULL identity(1,1),
	[username] [nvarchar](75) NOT NULL,
	[hashedPassword] [nvarchar](max) NOT NULL,
	[passwordSalt] [nvarchar](max) NOT NULL,
	[email] [nvarchar](100) NOT NULL,
	[phoneNumber] [nchar](20) NULL,
	[isCompany] [bit] NOT NULL,
	[availability] [nvarchar](50) NOT NULL,
	[locationCity] [nvarchar](75) NOT NULL,
	[operatingRadius] [int] NOT NULL,
	[services] [int] NOT NULL,
	[isActice] [bit] NOT NULL,
 CONSTRAINT [PK_Operators] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OperatorServices]    Script Date: 18.02.2023 13:26:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OperatorServices](
	[ID] [int] NOT NULL identity(1,1),
	[photo] [bit] NOT NULL,
	[dronePhoto] [bit] NOT NULL,
	[droneFilm] [bit] NOT NULL,
	[filming] [bit] NOT NULL,
 CONSTRAINT [PK_OperatorServices] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Photos]    Script Date: 18.02.2023 13:26:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Photos](
	[ID] [int] NOT NULL identity(1,1),
	[photoURL] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_Photos] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 18.02.2023 13:26:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[ID] [int] NOT NULL identity(1,1),
	[username] [nvarchar](50) NOT NULL,
	[hashedPassword] [nvarchar](max) NOT NULL,
	[passwordSalt] [nvarchar](max) NOT NULL,
	[email] [nvarchar](100) NOT NULL,
	[phoneNumber] [nvarchar](20) NULL,
	[isActice] [bit] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
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
ALTER TABLE [dbo].[OfferGalleries]  WITH CHECK ADD  CONSTRAINT [FK_OfferGalleries_Offers] FOREIGN KEY([offerID])
REFERENCES [dbo].[Offers] ([ID])
GO
ALTER TABLE [dbo].[OfferGalleries] CHECK CONSTRAINT [FK_OfferGalleries_Offers]
GO
ALTER TABLE [dbo].[OfferGalleries]  WITH CHECK ADD  CONSTRAINT [FK_OfferGalleries_Photos] FOREIGN KEY([photoID])
REFERENCES [dbo].[Photos] ([ID])
GO
ALTER TABLE [dbo].[OfferGalleries] CHECK CONSTRAINT [FK_OfferGalleries_Photos]
GO
ALTER TABLE [dbo].[Offers]  WITH CHECK ADD  CONSTRAINT [FK_Offers_Operators] FOREIGN KEY([operatorID])
REFERENCES [dbo].[Operators] ([ID])
GO
ALTER TABLE [dbo].[Offers] CHECK CONSTRAINT [FK_Offers_Operators]
GO
ALTER TABLE [dbo].[OperatorRatings]  WITH CHECK ADD  CONSTRAINT [FK_OperatorRatings_OperatorRatings] FOREIGN KEY([operatorID])
REFERENCES [dbo].[Operators] ([ID])
GO
ALTER TABLE [dbo].[OperatorRatings] CHECK CONSTRAINT [FK_OperatorRatings_OperatorRatings]
GO
ALTER TABLE [dbo].[OperatorRatings]  WITH CHECK ADD  CONSTRAINT [FK_OperatorRatings_Users] FOREIGN KEY([userID])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[OperatorRatings] CHECK CONSTRAINT [FK_OperatorRatings_Users]
GO
ALTER TABLE [dbo].[Operators]  WITH CHECK ADD  CONSTRAINT [FK_Operators_OperatorServices] FOREIGN KEY([services])
REFERENCES [dbo].[OperatorServices] ([ID])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[Operators] CHECK CONSTRAINT [FK_Operators_OperatorServices]
GO
USE [master]
GO
ALTER DATABASE [FotoWorld] SET  READ_WRITE 
GO
