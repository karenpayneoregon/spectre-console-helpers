USE [master]
GO
/****** Object:  Database [EF.SelectiveUpdates]    Script Date: 4/10/2025 4:45:08 AM ******/
CREATE DATABASE [EF.SelectiveUpdates]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'EF.SelectiveUpdates', FILENAME = N'C:\Users\paynek\EF.SelectiveUpdates.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'EF.SelectiveUpdates_log', FILENAME = N'C:\Users\paynek\EF.SelectiveUpdates_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [EF.SelectiveUpdates] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [EF.SelectiveUpdates].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [EF.SelectiveUpdates] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [EF.SelectiveUpdates] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [EF.SelectiveUpdates] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [EF.SelectiveUpdates] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [EF.SelectiveUpdates] SET ARITHABORT OFF 
GO
ALTER DATABASE [EF.SelectiveUpdates] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [EF.SelectiveUpdates] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [EF.SelectiveUpdates] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [EF.SelectiveUpdates] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [EF.SelectiveUpdates] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [EF.SelectiveUpdates] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [EF.SelectiveUpdates] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [EF.SelectiveUpdates] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [EF.SelectiveUpdates] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [EF.SelectiveUpdates] SET  ENABLE_BROKER 
GO
ALTER DATABASE [EF.SelectiveUpdates] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [EF.SelectiveUpdates] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [EF.SelectiveUpdates] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [EF.SelectiveUpdates] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [EF.SelectiveUpdates] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [EF.SelectiveUpdates] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [EF.SelectiveUpdates] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [EF.SelectiveUpdates] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [EF.SelectiveUpdates] SET  MULTI_USER 
GO
ALTER DATABASE [EF.SelectiveUpdates] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [EF.SelectiveUpdates] SET DB_CHAINING OFF 
GO
ALTER DATABASE [EF.SelectiveUpdates] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [EF.SelectiveUpdates] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [EF.SelectiveUpdates] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [EF.SelectiveUpdates] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [EF.SelectiveUpdates] SET QUERY_STORE = OFF
GO
USE [EF.SelectiveUpdates]
GO
/****** Object:  Table [dbo].[Person]    Script Date: 4/10/2025 4:45:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Person](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NULL,
	[FirstName] [nvarchar](max) NULL,
	[LastName] [nvarchar](max) NULL,
	[BirthDate] [date] NULL,
 CONSTRAINT [PK_Person] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Person] ON 

INSERT [dbo].[Person] ([Id], [Title], [FirstName], [LastName], [BirthDate]) VALUES (1, N'Mr', N'James', N'Gallagher', CAST(N'1957-08-07' AS Date))
INSERT [dbo].[Person] ([Id], [Title], [FirstName], [LastName], [BirthDate]) VALUES (2, N'Mrs', N'Kate', N'Gallagher', CAST(N'1960-05-11' AS Date))
INSERT [dbo].[Person] ([Id], [Title], [FirstName], [LastName], [BirthDate]) VALUES (3, N'Mr', N'Billy bob', N'Smith', CAST(N'1989-09-23' AS Date))
SET IDENTITY_INSERT [dbo].[Person] OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary key' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Person', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Mr Miss Mrs' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Person', @level2type=N'COLUMN',@level2name=N'Title'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'First name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Person', @level2type=N'COLUMN',@level2name=N'FirstName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'last name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Person', @level2type=N'COLUMN',@level2name=N'LastName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Their birth date' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Person', @level2type=N'COLUMN',@level2name=N'BirthDate'
GO
USE [master]
GO
ALTER DATABASE [EF.SelectiveUpdates] SET  READ_WRITE 
GO
