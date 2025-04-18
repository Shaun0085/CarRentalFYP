USE [CarRentalDatabase]
GO
ALTER DATABASE [CarRentalDatabase] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [CarRentalDatabase].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [CarRentalDatabase] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [CarRentalDatabase] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [CarRentalDatabase] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [CarRentalDatabase] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [CarRentalDatabase] SET ARITHABORT OFF 
GO
ALTER DATABASE [CarRentalDatabase] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [CarRentalDatabase] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [CarRentalDatabase] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [CarRentalDatabase] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [CarRentalDatabase] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [CarRentalDatabase] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [CarRentalDatabase] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [CarRentalDatabase] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [CarRentalDatabase] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [CarRentalDatabase] SET  DISABLE_BROKER 
GO
ALTER DATABASE [CarRentalDatabase] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [CarRentalDatabase] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [CarRentalDatabase] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [CarRentalDatabase] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [CarRentalDatabase] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [CarRentalDatabase] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [CarRentalDatabase] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [CarRentalDatabase] SET RECOVERY FULL 
GO
ALTER DATABASE [CarRentalDatabase] SET  MULTI_USER 
GO
ALTER DATABASE [CarRentalDatabase] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [CarRentalDatabase] SET DB_CHAINING OFF 
GO
ALTER DATABASE [CarRentalDatabase] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [CarRentalDatabase] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [CarRentalDatabase] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [CarRentalDatabase] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'CarRentalDatabase', N'ON'
GO
ALTER DATABASE [CarRentalDatabase] SET QUERY_STORE = ON
GO
ALTER DATABASE [CarRentalDatabase] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [CarRentalDatabase]
GO
/****** Object:  Table [dbo].[Admins]    Script Date: 4/14/2025 3:24:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Admins](
	[AdminId] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](max) NULL,
	[Password] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[AdminId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Billings]    Script Date: 4/14/2025 3:24:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Billings](
	[BillingID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](100) NULL,
	[LastName] [varchar](100) NULL,
	[PhoneNumber] [int] NULL,
	[Address] [varchar](255) NULL,
	[City] [varchar](100) NULL,
	[PostCode] [varchar](100) NULL,
	[Country] [varchar](100) NULL,
	[State] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[BillingID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Bookings]    Script Date: 4/14/2025 3:24:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bookings](
	[BookingID] [int] IDENTITY(1,1) NOT NULL,
	[CustomerName] [nvarchar](max) NULL,
	[ICNumber] [nvarchar](max) NULL,
	[CustomerPhoneNumber] [nvarchar](max) NULL,
	[PickUpLocation] [nvarchar](max) NULL,
	[PickUpDate] [date] NULL,
	[PickUpTime] [time](7) NULL,
	[DropOffLocation] [nvarchar](max) NULL,
	[DropOffDate] [date] NULL,
	[DropOffTime] [time](7) NULL,
	[BabySeat] [nvarchar](max) NULL,
	[UsbCable] [nvarchar](max) NULL,
	[PhoneHolder] [nvarchar](max) NULL,
	[VehicleID] [int] NULL,
	[PaymentID] [int] NULL,
	[BookingStatusID] [int] NULL,
	[LocationID] [int] NULL,
	[CarID] [int] NULL,
	[Amount] [int] NULL,
	[Email] [varchar](max) NULL,
	[PaymentType] [varchar](max) NULL,
	[OrderID] [varchar](max) NULL,
	[BillingID] [int] NULL,
	[Notes] [varchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[BookingID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BookingStatuses]    Script Date: 4/14/2025 3:24:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BookingStatuses](
	[BookingStatusID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[BookingStatusID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cars]    Script Date: 4/14/2025 3:24:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cars](
	[CarID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[PlateNumber] [nvarchar](max) NULL,
	[Year] [int] NULL,
	[Color] [nvarchar](max) NULL,
	[Notes] [nvarchar](max) NULL,
	[CarStatusID] [int] NULL,
	[VehicleID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[CarID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CarStatuses]    Script Date: 4/14/2025 3:24:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CarStatuses](
	[CarStatusID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[CarStatusID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Locations]    Script Date: 4/14/2025 3:24:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Locations](
	[LocationID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Fee] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[LocationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Payments]    Script Date: 4/14/2025 3:24:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Payments](
	[PaymentID] [int] IDENTITY(1,1) NOT NULL,
	[Date] [date] NULL,
	[CallBackStatus] [nvarchar](max) NULL,
	[ReferenceNumber] [varchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[PaymentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VehicleCategories]    Script Date: 4/14/2025 3:24:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VehicleCategories](
	[VehicleCategoryID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[VehicleCategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VehicleFuels]    Script Date: 4/14/2025 3:24:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VehicleFuels](
	[VehicleFuelID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[VehicleFuelID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Vehicles]    Script Date: 4/14/2025 3:24:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Vehicles](
	[VehicleID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Image] [varbinary](max) NULL,
	[Price] [int] NULL,
	[NumberOfSeat] [int] NULL,
	[Deposit] [int] NULL,
	[EngineCapacity] [int] NULL,
	[Year] [int] NULL,
	[VehicleCategoryID] [int] NULL,
	[VehicleTransmissionID] [int] NULL,
	[VehicleFuelID] [int] NULL,
	[ImageTwo] [varbinary](max) NULL,
	[ImageThree] [varbinary](max) NULL,
	[ImageFour] [varbinary](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[VehicleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VehicleTransmissions]    Script Date: 4/14/2025 3:24:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VehicleTransmissions](
	[VehicleTransmissionID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[VehicleTransmissionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Index [IX_Vehicles_VehicleCategoryID]    Script Date: 4/14/2025 3:24:21 PM ******/
CREATE NONCLUSTERED INDEX [IX_Vehicles_VehicleCategoryID] ON [dbo].[Vehicles]
(
	[VehicleCategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Vehicles_VehicleTransmissionID]    Script Date: 4/14/2025 3:24:21 PM ******/
CREATE NONCLUSTERED INDEX [IX_Vehicles_VehicleTransmissionID] ON [dbo].[Vehicles]
(
	[VehicleTransmissionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Bookings]  WITH CHECK ADD FOREIGN KEY([BookingStatusID])
REFERENCES [dbo].[BookingStatuses] ([BookingStatusID])
GO
ALTER TABLE [dbo].[Bookings]  WITH CHECK ADD FOREIGN KEY([CarID])
REFERENCES [dbo].[Cars] ([CarID])
GO
ALTER TABLE [dbo].[Bookings]  WITH CHECK ADD FOREIGN KEY([LocationID])
REFERENCES [dbo].[Locations] ([LocationID])
GO
ALTER TABLE [dbo].[Bookings]  WITH CHECK ADD FOREIGN KEY([PaymentID])
REFERENCES [dbo].[Payments] ([PaymentID])
GO
ALTER TABLE [dbo].[Bookings]  WITH CHECK ADD FOREIGN KEY([VehicleID])
REFERENCES [dbo].[Vehicles] ([VehicleID])
GO
ALTER TABLE [dbo].[Bookings]  WITH CHECK ADD  CONSTRAINT [FK_Billings_BillingID] FOREIGN KEY([BillingID])
REFERENCES [dbo].[Billings] ([BillingID])
GO
ALTER TABLE [dbo].[Bookings] CHECK CONSTRAINT [FK_Billings_BillingID]
GO
ALTER TABLE [dbo].[Cars]  WITH CHECK ADD FOREIGN KEY([CarStatusID])
REFERENCES [dbo].[CarStatuses] ([CarStatusID])
GO
ALTER TABLE [dbo].[Cars]  WITH CHECK ADD FOREIGN KEY([VehicleID])
REFERENCES [dbo].[Vehicles] ([VehicleID])
GO
ALTER TABLE [dbo].[Vehicles]  WITH CHECK ADD  CONSTRAINT [FK_Vehicles_ToVehicleCategories] FOREIGN KEY([VehicleCategoryID])
REFERENCES [dbo].[VehicleCategories] ([VehicleCategoryID])
GO
ALTER TABLE [dbo].[Vehicles] CHECK CONSTRAINT [FK_Vehicles_ToVehicleCategories]
GO
ALTER TABLE [dbo].[Vehicles]  WITH CHECK ADD  CONSTRAINT [FK_Vehicles_ToVehicleFuels] FOREIGN KEY([VehicleFuelID])
REFERENCES [dbo].[VehicleFuels] ([VehicleFuelID])
GO
ALTER TABLE [dbo].[Vehicles] CHECK CONSTRAINT [FK_Vehicles_ToVehicleFuels]
GO
ALTER TABLE [dbo].[Vehicles]  WITH CHECK ADD  CONSTRAINT [FK_Vehicles_ToVehicleTransmissions] FOREIGN KEY([VehicleTransmissionID])
REFERENCES [dbo].[VehicleTransmissions] ([VehicleTransmissionID])
GO
ALTER TABLE [dbo].[Vehicles] CHECK CONSTRAINT [FK_Vehicles_ToVehicleTransmissions]
GO
USE [master]
GO
ALTER DATABASE [CarRentalDatabase] SET  READ_WRITE 
GO
