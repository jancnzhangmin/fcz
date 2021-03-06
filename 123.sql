USE [master]
GO
/****** Object:  Database [pocdatabase]    Script Date: 07/08/2016 10:54:21 ******/
CREATE DATABASE [pocdatabase] ON  PRIMARY 
( NAME = N'pocdatabase', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\pocdatabase.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'pocdatabase_log', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\pocdatabase_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [pocdatabase] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [pocdatabase].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [pocdatabase] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [pocdatabase] SET ANSI_NULLS OFF
GO
ALTER DATABASE [pocdatabase] SET ANSI_PADDING OFF
GO
ALTER DATABASE [pocdatabase] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [pocdatabase] SET ARITHABORT OFF
GO
ALTER DATABASE [pocdatabase] SET AUTO_CLOSE OFF
GO
ALTER DATABASE [pocdatabase] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [pocdatabase] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [pocdatabase] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [pocdatabase] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [pocdatabase] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [pocdatabase] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [pocdatabase] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [pocdatabase] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [pocdatabase] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [pocdatabase] SET  DISABLE_BROKER
GO
ALTER DATABASE [pocdatabase] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [pocdatabase] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [pocdatabase] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [pocdatabase] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [pocdatabase] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [pocdatabase] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [pocdatabase] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [pocdatabase] SET  READ_WRITE
GO
ALTER DATABASE [pocdatabase] SET RECOVERY FULL
GO
ALTER DATABASE [pocdatabase] SET  MULTI_USER
GO
ALTER DATABASE [pocdatabase] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [pocdatabase] SET DB_CHAINING OFF
GO
EXEC sys.sp_db_vardecimal_storage_format N'pocdatabase', N'ON'
GO
USE [pocdatabase]
GO
/****** Object:  Table [dbo].[Login]    Script Date: 07/08/2016 10:54:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Login](
	[login_id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](50) NULL,
	[login_name] [varchar](50) NULL,
	[password] [varchar](50) NULL,
	[user_right] [varchar](50) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[land_town]    Script Date: 07/08/2016 10:54:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[land_town](
	[land_town_id] [int] IDENTITY(1,1) NOT NULL,
	[land_area] [nvarchar](max) NULL,
	[built_up_area] [nvarchar](max) NULL,
	[common_area] [nvarchar](max) NULL,
	[sharing_area] [nvarchar](max) NULL,
	[land_grade] [nvarchar](max) NULL,
	[land_id] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[land_countryside]    Script Date: 07/08/2016 10:54:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[land_countryside](
	[land_countryside_id] [int] IDENTITY(1,1) NOT NULL,
	[land_countryside_area] [nvarchar](max) NULL,
	[cultivated_land] [nvarchar](max) NULL,
	[dry_land] [nvarchar](max) NULL,
	[paddy_field] [nvarchar](max) NULL,
	[orchard] [nvarchar](max) NULL,
	[forest_land] [nvarchar](max) NULL,
	[grassland] [nvarchar](max) NULL,
	[inmate_mining] [nvarchar](max) NULL,
	[construction_land] [nvarchar](max) NULL,
	[homestead_land] [nvarchar](max) NULL,
	[traffic_land] [nvarchar](max) NULL,
	[water_land] [nvarchar](max) NULL,
	[unused_land] [nvarchar](max) NULL,
	[land_id] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[land]    Script Date: 07/08/2016 10:54:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[land](
	[land_id] [int] IDENTITY(1,1) NOT NULL,
	[land_tile] [nvarchar](50) NULL,
	[land_word] [nvarchar](50) NULL,
	[land_number] [nvarchar](50) NULL,
	[land_government] [nvarchar](50) NULL,
	[land_date] [datetime] NULL,
	[land_user] [nvarchar](50) NULL,
	[land_idcardnumber] [nvarchar](50) NULL,
	[land_address] [nvarchar](max) NULL,
	[land_map] [nvarchar](50) NULL,
	[land_ground] [nvarchar](50) NULL,
	[land_use] [nvarchar](max) NULL,
	[land_use_period] [nvarchar](50) NULL,
	[land_east] [nvarchar](max) NULL,
	[land_south] [nvarchar](max) NULL,
	[land_west] [nvarchar](max) NULL,
	[land_north] [nvarchar](max) NULL,
	[land_office] [nvarchar](max) NULL,
	[land_send_date] [datetime] NULL,
	[land_remarks] [text] NULL,
	[land_change_items] [text] NULL,
	[land_figure] [text] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[house_use_land]    Script Date: 07/08/2016 10:54:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[house_use_land](
	[house_use_land_id] [int] IDENTITY(1,1) NOT NULL,
	[use_area] [varchar](50) NULL,
	[company] [varchar](50) NULL,
	[card_number] [varchar](50) NULL,
	[zidihao] [varchar](50) NULL,
	[house_id] [int] NOT NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[house_obligee]    Script Date: 07/08/2016 10:54:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[house_obligee](
	[house_obligee_id] [int] IDENTITY(1,1) NOT NULL,
	[obligee] [varchar](50) NULL,
	[type] [varchar](50) NULL,
	[room_number] [varchar](50) NULL,
	[jianshu] [varchar](50) NULL,
	[built_up_area] [varchar](50) NULL,
	[right_value] [varchar](50) NULL,
	[duration_right] [varchar](50) NULL,
	[logout_date] [varchar](50) NULL,
	[house_id] [int] NOT NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[house_nature]    Script Date: 07/08/2016 10:54:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[house_nature](
	[house_nature_id] [int] IDENTITY(1,1) NOT NULL,
	[nature] [varchar](50) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[house_located]    Script Date: 07/08/2016 10:54:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[house_located](
	[house_located_id] [int] IDENTITY(1,1) NOT NULL,
	[located] [varchar](max) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[house_deed_tax]    Script Date: 07/08/2016 10:54:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[house_deed_tax](
	[house_deed_tax_id] [int] IDENTITY(1,1) NOT NULL,
	[datetime] [varchar](50) NULL,
	[price] [varchar](50) NULL,
	[type] [varchar](50) NULL,
	[tax_rate] [varchar](50) NULL,
	[amount_money] [varchar](50) NULL,
	[remarks] [varchar](50) NULL,
	[house_id] [int] NOT NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[house_condition]    Script Date: 07/08/2016 10:54:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[house_condition](
	[house_condition_id] [int] IDENTITY(1,1) NOT NULL,
	[building_number] [varchar](50) NULL,
	[room_number] [varchar](50) NULL,
	[jianshu] [varchar](50) NULL,
	[building_structure] [varchar](max) NULL,
	[cengshu] [varchar](50) NULL,
	[built_up_area] [varchar](50) NULL,
	[remarks] [text] NULL,
	[house_id] [int] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[house_commonor]    Script Date: 07/08/2016 10:54:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[house_commonor](
	[house_commonor_id] [int] IDENTITY(1,1) NOT NULL,
	[commonor] [varchar](50) NULL,
	[share] [varchar](50) NULL,
	[number] [varchar](50) NULL,
	[comments] [text] NULL,
	[house_id] [int] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[house]    Script Date: 07/08/2016 10:54:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[house](
	[house_id] [int] IDENTITY(1,1) NOT NULL,
	[house_word] [varchar](50) NULL,
	[house_number] [varchar](50) NULL,
	[house_owner] [varchar](50) NULL,
	[house_nature] [int] NOT NULL,
	[house_idcardnumber] [varchar](50) NULL,
	[house_located] [varchar](50) NULL,
	[house_dihao] [varchar](50) NULL,
	[house_postscript] [varchar](max) NULL,
	[house_witness] [varchar](50) NULL,
	[house_proofreader] [varchar](50) NULL,
	[house_autograph] [varchar](50) NULL,
	[house_licensing_date] [datetime] NULL,
	[house_office] [varchar](50) NULL,
	[house_banzhenren] [varchar](50) NULL,
	[house_tianfatime] [datetime] NULL,
	[house_figure] [varchar](50) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
