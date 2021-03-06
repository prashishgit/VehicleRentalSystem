USE [master]
GO
/****** Object:  Database [VehicleRentalDB]    Script Date: 6/16/2019 1:38:21 PM ******/
CREATE DATABASE [VehicleRentalDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'VehicleRentalDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\VehicleRentalDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'VehicleRentalDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\VehicleRentalDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [VehicleRentalDB] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [VehicleRentalDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [VehicleRentalDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [VehicleRentalDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [VehicleRentalDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [VehicleRentalDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [VehicleRentalDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [VehicleRentalDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [VehicleRentalDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [VehicleRentalDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [VehicleRentalDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [VehicleRentalDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [VehicleRentalDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [VehicleRentalDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [VehicleRentalDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [VehicleRentalDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [VehicleRentalDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [VehicleRentalDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [VehicleRentalDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [VehicleRentalDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [VehicleRentalDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [VehicleRentalDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [VehicleRentalDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [VehicleRentalDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [VehicleRentalDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [VehicleRentalDB] SET  MULTI_USER 
GO
ALTER DATABASE [VehicleRentalDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [VehicleRentalDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [VehicleRentalDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [VehicleRentalDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [VehicleRentalDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [VehicleRentalDB] SET QUERY_STORE = OFF
GO
USE [VehicleRentalDB]
GO
/****** Object:  Table [dbo].[tblBanner]    Script Date: 6/16/2019 1:38:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblBanner](
	[BannerId] [int] IDENTITY(1,1) NOT NULL,
	[Photo] [nvarchar](50) NULL,
	[Title] [nvarchar](50) NULL,
	[Description] [nvarchar](50) NOT NULL,
	[HeadingOne] [nvarchar](50) NULL,
	[HeadingTwo] [nvarchar](50) NULL,
 CONSTRAINT [PK_tblBanner] PRIMARY KEY CLUSTERED 
(
	[BannerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblBooking]    Script Date: 6/16/2019 1:38:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblBooking](
	[BookingId] [int] IDENTITY(1,1) NOT NULL,
	[VehicleId] [int] NULL,
	[UserId] [int] NULL,
	[CitizenshipPhoto] [nvarchar](50) NULL,
	[PickUpDate] [nvarchar](50) NULL,
	[DropOffDate] [nvarchar](50) NULL,
	[TotalAmount] [int] NULL,
	[AmountPaid] [int] NULL,
	[BookingStatus] [nvarchar](50) NULL,
 CONSTRAINT [PK_tblBooking] PRIMARY KEY CLUSTERED 
(
	[BookingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblCategory]    Script Date: 6/16/2019 1:38:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblCategory](
	[VehicleCategoryId] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [nvarchar](50) NULL,
 CONSTRAINT [PK_tblCategory] PRIMARY KEY CLUSTERED 
(
	[VehicleCategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblComments]    Script Date: 6/16/2019 1:38:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblComments](
	[CommentId] [int] IDENTITY(1,1) NOT NULL,
	[Comments] [varchar](7999) NULL,
	[ThisDateTime] [datetime] NULL,
	[VehicleId] [int] NULL,
	[Rating] [int] NULL,
	[UserName] [nvarchar](50) NULL,
 CONSTRAINT [PK_tblComments] PRIMARY KEY CLUSTERED 
(
	[CommentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblItem]    Script Date: 6/16/2019 1:38:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblItem](
	[VehicleId] [int] IDENTITY(1,1) NOT NULL,
	[VehicleCategoryId] [int] NULL,
	[VehiclePrice] [nvarchar](50) NULL,
	[VehicleTitle] [nvarchar](500) NULL,
	[VehiclePhoto] [nvarchar](50) NULL,
	[Description] [nvarchar](max) NULL,
	[VehicleStatus] [nvarchar](50) NULL,
 CONSTRAINT [PK_tblItem] PRIMARY KEY CLUSTERED 
(
	[VehicleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblRole]    Script Date: 6/16/2019 1:38:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblRole](
	[RoleId] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](50) NULL,
 CONSTRAINT [PK_tblRole] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblUser]    Script Date: 6/16/2019 1:38:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblUser](
	[UserId] [int] NOT NULL,
	[RoleId] [int] NULL,
	[UserName] [nvarchar](50) NULL,
	[Password] [nvarchar](50) NULL,
	[FullName] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[CitizenshipNumber] [nvarchar](50) NULL,
 CONSTRAINT [PK_tblUser] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblUserRole]    Script Date: 6/16/2019 1:38:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblUserRole](
	[UserRoleId] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [int] NULL,
	[UserId] [int] NULL,
 CONSTRAINT [PK_tblUserRole] PRIMARY KEY CLUSTERED 
(
	[UserRoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[tblBanner] ON 

INSERT [dbo].[tblBanner] ([BannerId], [Photo], [Title], [Description], [HeadingOne], [HeadingTwo]) VALUES (6, N'banner1.gif', N'Your Dream Car', N'This is a Good Car', N'Book ', N'Now')
INSERT [dbo].[tblBanner] ([BannerId], [Photo], [Title], [Description], [HeadingOne], [HeadingTwo]) VALUES (7, N'car_banner.gif', N'Hello World', N'This is a Good Car', N'Book ', N'Now')
SET IDENTITY_INSERT [dbo].[tblBanner] OFF
SET IDENTITY_INSERT [dbo].[tblBooking] ON 

INSERT [dbo].[tblBooking] ([BookingId], [VehicleId], [UserId], [CitizenshipPhoto], [PickUpDate], [DropOffDate], [TotalAmount], [AmountPaid], [BookingStatus]) VALUES (2, 3, 2, N'Citizenship.jpg', N'2019-05-31', N'2019-06-01', 10500, 10500, N'Checked Out')
INSERT [dbo].[tblBooking] ([BookingId], [VehicleId], [UserId], [CitizenshipPhoto], [PickUpDate], [DropOffDate], [TotalAmount], [AmountPaid], [BookingStatus]) VALUES (5, 4, 5, N'canadian-citizenship-card-photo-old.jpg', N'2019-06-22', N'2019-07-02', 15000, 15000, N'Checked Out')
INSERT [dbo].[tblBooking] ([BookingId], [VehicleId], [UserId], [CitizenshipPhoto], [PickUpDate], [DropOffDate], [TotalAmount], [AmountPaid], [BookingStatus]) VALUES (8, 3, 1, N'1.jpg', N'2019-06-02', N'2019-06-18', 56000, 56000, N'Checked Out')
INSERT [dbo].[tblBooking] ([BookingId], [VehicleId], [UserId], [CitizenshipPhoto], [PickUpDate], [DropOffDate], [TotalAmount], [AmountPaid], [BookingStatus]) VALUES (11, 1, 5, N'2.jpg', N'2019-06-04', N'2019-06-20', 40000, 40000, N'Checked Out')
INSERT [dbo].[tblBooking] ([BookingId], [VehicleId], [UserId], [CitizenshipPhoto], [PickUpDate], [DropOffDate], [TotalAmount], [AmountPaid], [BookingStatus]) VALUES (12, 6, 5, N'3.jpg', N'2019-06-04', N'2019-06-18', 16800, 16800, N'Checked Out')
INSERT [dbo].[tblBooking] ([BookingId], [VehicleId], [UserId], [CitizenshipPhoto], [PickUpDate], [DropOffDate], [TotalAmount], [AmountPaid], [BookingStatus]) VALUES (13, 9, 1, N'3.jpg', N'2019-06-07', N'2019-06-27', 50000, 20000, N'Confirm')
INSERT [dbo].[tblBooking] ([BookingId], [VehicleId], [UserId], [CitizenshipPhoto], [PickUpDate], [DropOffDate], [TotalAmount], [AmountPaid], [BookingStatus]) VALUES (15, 9, 1, N'nrn.jpg', N'2019-06-05', N'2019-06-27', 55000, 6000, N'Confirm')
INSERT [dbo].[tblBooking] ([BookingId], [VehicleId], [UserId], [CitizenshipPhoto], [PickUpDate], [DropOffDate], [TotalAmount], [AmountPaid], [BookingStatus]) VALUES (18, 7, 1, N'4.jpg', N'2019-06-05', N'2019-06-19', 63924, 63924, N'Checked Out')
INSERT [dbo].[tblBooking] ([BookingId], [VehicleId], [UserId], [CitizenshipPhoto], [PickUpDate], [DropOffDate], [TotalAmount], [AmountPaid], [BookingStatus]) VALUES (19, 3, 1, N'4.jpg', N'2019-06-05', N'2019-06-19', 49000, 4001, N'Confirm')
INSERT [dbo].[tblBooking] ([BookingId], [VehicleId], [UserId], [CitizenshipPhoto], [PickUpDate], [DropOffDate], [TotalAmount], [AmountPaid], [BookingStatus]) VALUES (20, 1, 1, N'yellowstar.gif', N'2019-06-07', N'2019-06-19', 30000, 30000, N'Checked Out')
INSERT [dbo].[tblBooking] ([BookingId], [VehicleId], [UserId], [CitizenshipPhoto], [PickUpDate], [DropOffDate], [TotalAmount], [AmountPaid], [BookingStatus]) VALUES (21, 1, 2, N'Does-dual-citizenship-mean-lower-taxes.jpg', N'2019-06-07', N'2019-06-20', 32500, 20000, N'Confirm')
INSERT [dbo].[tblBooking] ([BookingId], [VehicleId], [UserId], [CitizenshipPhoto], [PickUpDate], [DropOffDate], [TotalAmount], [AmountPaid], [BookingStatus]) VALUES (22, 1, 2, N'Capture.JPG', N'2019-06-12', N'2019-06-19', 17500, 2, N'Confirm')
INSERT [dbo].[tblBooking] ([BookingId], [VehicleId], [UserId], [CitizenshipPhoto], [PickUpDate], [DropOffDate], [TotalAmount], [AmountPaid], [BookingStatus]) VALUES (23, 3, 1, N'Capture.JPG', N'2019-06-12', N'2019-06-20', 28000, 5, N'Confirm')
SET IDENTITY_INSERT [dbo].[tblBooking] OFF
SET IDENTITY_INSERT [dbo].[tblCategory] ON 

INSERT [dbo].[tblCategory] ([VehicleCategoryId], [CategoryName]) VALUES (1, N'Bike')
INSERT [dbo].[tblCategory] ([VehicleCategoryId], [CategoryName]) VALUES (2, N'Car')
INSERT [dbo].[tblCategory] ([VehicleCategoryId], [CategoryName]) VALUES (3, N'Jeep')
SET IDENTITY_INSERT [dbo].[tblCategory] OFF
SET IDENTITY_INSERT [dbo].[tblComments] ON 

INSERT [dbo].[tblComments] ([CommentId], [Comments], [ThisDateTime], [VehicleId], [Rating], [UserName]) VALUES (1, N'Hello', CAST(N'2019-06-07T20:53:08.337' AS DateTime), 1, 4, N'Kshitij')
INSERT [dbo].[tblComments] ([CommentId], [Comments], [ThisDateTime], [VehicleId], [Rating], [UserName]) VALUES (2, N'Hi', CAST(N'2019-06-07T20:55:31.233' AS DateTime), 1, 4, N'Kshitij')
INSERT [dbo].[tblComments] ([CommentId], [Comments], [ThisDateTime], [VehicleId], [Rating], [UserName]) VALUES (3, N'Hello', CAST(N'2019-06-07T20:58:02.930' AS DateTime), 1, 3, N'Abhishek')
INSERT [dbo].[tblComments] ([CommentId], [Comments], [ThisDateTime], [VehicleId], [Rating], [UserName]) VALUES (4, N'hi', CAST(N'2019-06-07T21:08:03.443' AS DateTime), 1, 2, N'Prashish')
INSERT [dbo].[tblComments] ([CommentId], [Comments], [ThisDateTime], [VehicleId], [Rating], [UserName]) VALUES (5, N'The experience was very good', CAST(N'2019-06-07T21:18:48.223' AS DateTime), 3, 4, N'Prashish')
INSERT [dbo].[tblComments] ([CommentId], [Comments], [ThisDateTime], [VehicleId], [Rating], [UserName]) VALUES (6, N'Hello', CAST(N'2019-06-07T22:01:27.883' AS DateTime), 3, 5, N'Aashish')
INSERT [dbo].[tblComments] ([CommentId], [Comments], [ThisDateTime], [VehicleId], [Rating], [UserName]) VALUES (7, N'Very Good.', CAST(N'2019-06-07T22:59:05.417' AS DateTime), 3, 1, N'Sunil')
INSERT [dbo].[tblComments] ([CommentId], [Comments], [ThisDateTime], [VehicleId], [Rating], [UserName]) VALUES (8, N'This is a fantastic car/
', CAST(N'2019-06-07T23:29:33.783' AS DateTime), 3, 1, N'Suman')
INSERT [dbo].[tblComments] ([CommentId], [Comments], [ThisDateTime], [VehicleId], [Rating], [UserName]) VALUES (9, N'One of the best bike in Nepal.', CAST(N'2019-06-07T23:47:21.737' AS DateTime), 9, 5, N'Aashish')
INSERT [dbo].[tblComments] ([CommentId], [Comments], [ThisDateTime], [VehicleId], [Rating], [UserName]) VALUES (10, N'Hello World', CAST(N'2019-06-07T23:50:21.013' AS DateTime), 9, 3, N'Lucifer')
INSERT [dbo].[tblComments] ([CommentId], [Comments], [ThisDateTime], [VehicleId], [Rating], [UserName]) VALUES (11, N'R1 high CC bike.', CAST(N'2019-06-07T23:50:54.563' AS DateTime), 9, 4, N'Samir')
INSERT [dbo].[tblComments] ([CommentId], [Comments], [ThisDateTime], [VehicleId], [Rating], [UserName]) VALUES (12, N'One of the most expensive scooter in Nepal.', CAST(N'2019-06-08T00:02:18.640' AS DateTime), 6, 5, N'Mahendra')
INSERT [dbo].[tblComments] ([CommentId], [Comments], [ThisDateTime], [VehicleId], [Rating], [UserName]) VALUES (13, N'Best Bike in Nepal', CAST(N'2019-06-10T19:41:48.167' AS DateTime), 1, 5, N'')
INSERT [dbo].[tblComments] ([CommentId], [Comments], [ThisDateTime], [VehicleId], [Rating], [UserName]) VALUES (14, N'RC', CAST(N'2019-06-10T19:42:10.050' AS DateTime), 1, 5, N'')
INSERT [dbo].[tblComments] ([CommentId], [Comments], [ThisDateTime], [VehicleId], [Rating], [UserName]) VALUES (15, N'One of the best Vehicle in Nepal.', CAST(N'2019-06-10T19:42:42.893' AS DateTime), 1, 5, N'')
INSERT [dbo].[tblComments] ([CommentId], [Comments], [ThisDateTime], [VehicleId], [Rating], [UserName]) VALUES (16, N'Test Comment', CAST(N'2019-06-11T19:39:59.127' AS DateTime), 3, 4, N'')
INSERT [dbo].[tblComments] ([CommentId], [Comments], [ThisDateTime], [VehicleId], [Rating], [UserName]) VALUES (17, N'It is a good vehicle', CAST(N'2019-06-16T08:17:33.480' AS DateTime), 7, 5, N'')
INSERT [dbo].[tblComments] ([CommentId], [Comments], [ThisDateTime], [VehicleId], [Rating], [UserName]) VALUES (18, N'It is good.', CAST(N'2019-06-16T11:08:57.107' AS DateTime), 8, 4, N'Prashish')
SET IDENTITY_INSERT [dbo].[tblComments] OFF
SET IDENTITY_INSERT [dbo].[tblItem] ON 

INSERT [dbo].[tblItem] ([VehicleId], [VehicleCategoryId], [VehiclePrice], [VehicleTitle], [VehiclePhoto], [Description], [VehicleStatus]) VALUES (1, 1, N'2500', N'KTM-RC 390', N'KTM-RC.jpg', N'Sporty look and a great bike.', N'Book Now')
INSERT [dbo].[tblItem] ([VehicleId], [VehicleCategoryId], [VehiclePrice], [VehicleTitle], [VehiclePhoto], [Description], [VehicleStatus]) VALUES (3, 2, N'3500', N'Renault Kwid', N'Renault-Kwid.jpg', N'Family Car.', N'Book Now')
INSERT [dbo].[tblItem] ([VehicleId], [VehicleCategoryId], [VehiclePrice], [VehicleTitle], [VehiclePhoto], [Description], [VehicleStatus]) VALUES (4, 2, N'5000', N'Hilux', N'Hilux.jpg', N'Hilux is a super power Vehicle', N'Available Now')
INSERT [dbo].[tblItem] ([VehicleId], [VehicleCategoryId], [VehiclePrice], [VehicleTitle], [VehiclePhoto], [Description], [VehicleStatus]) VALUES (6, 1, N'1200', N'Vespa Scooter', N'Vespa.jpg', N'Old Classy Scooter', N'Book Now')
INSERT [dbo].[tblItem] ([VehicleId], [VehicleCategoryId], [VehiclePrice], [VehicleTitle], [VehiclePhoto], [Description], [VehicleStatus]) VALUES (7, 2, N'4566', N'Bugati', N'Buggati.jpg', N'This is a Good Car', N'Book Now')
INSERT [dbo].[tblItem] ([VehicleId], [VehicleCategoryId], [VehiclePrice], [VehicleTitle], [VehiclePhoto], [Description], [VehicleStatus]) VALUES (8, 1, N'500', N'Bullet Bike', N'Bike1.jpg', N'Best for off roads', N'Available Now')
INSERT [dbo].[tblItem] ([VehicleId], [VehicleCategoryId], [VehiclePrice], [VehicleTitle], [VehiclePhoto], [Description], [VehicleStatus]) VALUES (9, 1, N'2500', N'YAMAHA R15 V3', N'DUCATI.jpg', N'Sporty look and a great bike.', N'Available Now')
INSERT [dbo].[tblItem] ([VehicleId], [VehicleCategoryId], [VehiclePrice], [VehicleTitle], [VehiclePhoto], [Description], [VehicleStatus]) VALUES (10, 3, N'3000', N'Wrangler', N'Wrangler.jpg', N'The Jeep Wrangler (JK) is the third generation of the Jeep Wrangler off-road vehicle. The Wrangler was unveiled at the 2006 North American International Auto Show in Detroit, the JK series 2007 Wrangler Unlimited at the 2006 New York Auto Show.', N'Available')
INSERT [dbo].[tblItem] ([VehicleId], [VehicleCategoryId], [VehiclePrice], [VehicleTitle], [VehiclePhoto], [Description], [VehicleStatus]) VALUES (11, NULL, N'5000', N'Buggati Divo', N'Buggati Divo.jpg', N'You need to own a buggati chiron to rent a buggati divo', N'Available')
INSERT [dbo].[tblItem] ([VehicleId], [VehicleCategoryId], [VehiclePrice], [VehicleTitle], [VehiclePhoto], [Description], [VehicleStatus]) VALUES (12, NULL, N'2500', N'Kawasaki Ninja', N'Ninja.jpg', N'The Kawasaki Ninja 300 is powered by a 296cc, liquid-cooled, parallel twin engine, which produces 39PS at 11,000rpm and 27Nm at 10,000rpm, mated to a 6-speed gearbox. The 300cc Kawasaki is equipped with 37mm telescopic front forks and a five-way adjustable gas-charged monoshock at the rear.', N'Available Now')
INSERT [dbo].[tblItem] ([VehicleId], [VehicleCategoryId], [VehiclePrice], [VehicleTitle], [VehiclePhoto], [Description], [VehicleStatus]) VALUES (13, NULL, N'4000', N'McLaren', N'Mclaren.jpeg', N'McLaren Automotive. McLaren Automotive (formerly known as McLaren Cars) is a British automotive manufacturer based at the McLaren Technology Centre in Woking, Surrey. The main products of the company are sports cars, usually produced in-house at designated production facilities.', N'Book Now')
SET IDENTITY_INSERT [dbo].[tblItem] OFF
SET IDENTITY_INSERT [dbo].[tblRole] ON 

INSERT [dbo].[tblRole] ([RoleId], [RoleName]) VALUES (1, N'Admin')
INSERT [dbo].[tblRole] ([RoleId], [RoleName]) VALUES (2, N'User')
SET IDENTITY_INSERT [dbo].[tblRole] OFF
INSERT [dbo].[tblUser] ([UserId], [RoleId], [UserName], [Password], [FullName], [Email], [CitizenshipNumber]) VALUES (1, 1, N'Prashish', N'1234', N'Prashish Maani Shrestha', N'pralex0009@gmail.com', N'98321')
INSERT [dbo].[tblUser] ([UserId], [RoleId], [UserName], [Password], [FullName], [Email], [CitizenshipNumber]) VALUES (2, 2, N'Kshitij', N'1234', N'Kshitij Chaudhary', NULL, NULL)
INSERT [dbo].[tblUser] ([UserId], [RoleId], [UserName], [Password], [FullName], [Email], [CitizenshipNumber]) VALUES (3, 1, N'Aashish', N'1234', N'Aashish Karki', N'aashish.karki@apexcollege.edu.np', N'123456')
INSERT [dbo].[tblUser] ([UserId], [RoleId], [UserName], [Password], [FullName], [Email], [CitizenshipNumber]) VALUES (5, 2, N'Mahendra', N'1234', N'Mahendra Singh Dhoni', N'prashish.shrestha@apexcollege.edu.np', N'124234')
SET IDENTITY_INSERT [dbo].[tblUserRole] ON 

INSERT [dbo].[tblUserRole] ([UserRoleId], [RoleId], [UserId]) VALUES (1, 1, 1)
INSERT [dbo].[tblUserRole] ([UserRoleId], [RoleId], [UserId]) VALUES (2, 2, 2)
INSERT [dbo].[tblUserRole] ([UserRoleId], [RoleId], [UserId]) VALUES (3, 1, 3)
INSERT [dbo].[tblUserRole] ([UserRoleId], [RoleId], [UserId]) VALUES (5, 2, 5)
SET IDENTITY_INSERT [dbo].[tblUserRole] OFF
ALTER TABLE [dbo].[tblBooking]  WITH CHECK ADD  CONSTRAINT [FK_tblBooking_tblItem] FOREIGN KEY([VehicleId])
REFERENCES [dbo].[tblItem] ([VehicleId])
GO
ALTER TABLE [dbo].[tblBooking] CHECK CONSTRAINT [FK_tblBooking_tblItem]
GO
ALTER TABLE [dbo].[tblBooking]  WITH CHECK ADD  CONSTRAINT [FK_tblBooking_tblUser] FOREIGN KEY([UserId])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblBooking] CHECK CONSTRAINT [FK_tblBooking_tblUser]
GO
ALTER TABLE [dbo].[tblComments]  WITH CHECK ADD  CONSTRAINT [FK_tblComments_tblItem] FOREIGN KEY([VehicleId])
REFERENCES [dbo].[tblItem] ([VehicleId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tblComments] CHECK CONSTRAINT [FK_tblComments_tblItem]
GO
ALTER TABLE [dbo].[tblItem]  WITH CHECK ADD  CONSTRAINT [FK_tblItem_tblCategory] FOREIGN KEY([VehicleCategoryId])
REFERENCES [dbo].[tblCategory] ([VehicleCategoryId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tblItem] CHECK CONSTRAINT [FK_tblItem_tblCategory]
GO
ALTER TABLE [dbo].[tblUser]  WITH CHECK ADD  CONSTRAINT [FK_tblUser_tblRole] FOREIGN KEY([RoleId])
REFERENCES [dbo].[tblRole] ([RoleId])
GO
ALTER TABLE [dbo].[tblUser] CHECK CONSTRAINT [FK_tblUser_tblRole]
GO
ALTER TABLE [dbo].[tblUserRole]  WITH CHECK ADD  CONSTRAINT [FK_tblUserRole_tblRole] FOREIGN KEY([RoleId])
REFERENCES [dbo].[tblRole] ([RoleId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tblUserRole] CHECK CONSTRAINT [FK_tblUserRole_tblRole]
GO
ALTER TABLE [dbo].[tblUserRole]  WITH CHECK ADD  CONSTRAINT [FK_tblUserRole_tblUser] FOREIGN KEY([UserId])
REFERENCES [dbo].[tblUser] ([UserId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tblUserRole] CHECK CONSTRAINT [FK_tblUserRole_tblUser]
GO
USE [master]
GO
ALTER DATABASE [VehicleRentalDB] SET  READ_WRITE 
GO
