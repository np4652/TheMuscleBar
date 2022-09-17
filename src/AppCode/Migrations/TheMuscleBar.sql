USE [master]
GO
/****** Object:  Database [TheMuscleBar]    Script Date: 17-09-2022 19:28:22 ******/
CREATE DATABASE [TheMuscleBar]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'TheMuscleBar', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\TheMuscleBar.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'TheMuscleBar_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\TheMuscleBar_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [TheMuscleBar] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [TheMuscleBar].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [TheMuscleBar] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [TheMuscleBar] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [TheMuscleBar] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [TheMuscleBar] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [TheMuscleBar] SET ARITHABORT OFF 
GO
ALTER DATABASE [TheMuscleBar] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [TheMuscleBar] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [TheMuscleBar] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [TheMuscleBar] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [TheMuscleBar] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [TheMuscleBar] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [TheMuscleBar] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [TheMuscleBar] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [TheMuscleBar] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [TheMuscleBar] SET  ENABLE_BROKER 
GO
ALTER DATABASE [TheMuscleBar] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [TheMuscleBar] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [TheMuscleBar] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [TheMuscleBar] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [TheMuscleBar] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [TheMuscleBar] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [TheMuscleBar] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [TheMuscleBar] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [TheMuscleBar] SET  MULTI_USER 
GO
ALTER DATABASE [TheMuscleBar] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [TheMuscleBar] SET DB_CHAINING OFF 
GO
ALTER DATABASE [TheMuscleBar] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [TheMuscleBar] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [TheMuscleBar] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [TheMuscleBar] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [TheMuscleBar] SET QUERY_STORE = OFF
GO
USE [TheMuscleBar]
GO
/****** Object:  Schema [HangFire]    Script Date: 17-09-2022 19:28:22 ******/
CREATE SCHEMA [HangFire]
GO
/****** Object:  Table [dbo].[ApplicationRole]    Script Date: 17-09-2022 19:28:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ApplicationRole](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ConcurrencyStamp] [nvarchar](1000) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	[NormalizedName] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_ApplicationRole] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ErrorLog]    Script Date: 17-09-2022 19:28:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ErrorLog](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ErrorMsg] [nvarchar](240) NOT NULL,
	[ErrorFrom] [nvarchar](50) NOT NULL,
	[ErrorNumber] [nvarchar](10) NOT NULL,
	[EntryOn] [datetime] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ledger]    Script Date: 17-09-2022 19:28:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ledger](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [bigint] NOT NULL,
	[TransactionType] [char](1) NOT NULL,
	[Amount] [numeric](18, 2) NOT NULL,
	[discount] [numeric](18, 2) NOT NULL,
	[PaymentMode] [tinyint] NOT NULL,
	[EntryOn] [datetime] NOT NULL,
	[EntryBy] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NLogs]    Script Date: 17-09-2022 19:28:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NLogs](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[When] [nvarchar](255) NOT NULL,
	[Message] [nvarchar](255) NOT NULL,
	[Level] [nvarchar](255) NOT NULL,
	[Exception] [nvarchar](255) NOT NULL,
	[Trace] [nvarchar](255) NOT NULL,
	[Logger] [nvarchar](255) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RoleClaims]    Script Date: 17-09-2022 19:28:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoleClaims](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ClaimType] [nvarchar](1000) NOT NULL,
	[ClaimValue] [nvarchar](1000) NOT NULL,
	[RoleId] [bigint] NOT NULL,
	[Country] [nvarchar](50) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserClaims]    Script Date: 17-09-2022 19:28:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserClaims](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ClaimType] [nvarchar](1000) NOT NULL,
	[ClaimValue] [nvarchar](1000) NOT NULL,
	[UserId] [nvarchar](256) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserLogins]    Script Date: 17-09-2022 19:28:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserLogins](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[ProviderDisplayName] [nvarchar](1000) NOT NULL,
	[UserId] [nvarchar](256) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRoles]    Script Date: 17-09-2022 19:28:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRoles](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](256) NOT NULL,
	[RoleId] [nvarchar](128) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 17-09-2022 19:28:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[AccessFailedCount] [bigint] NOT NULL,
	[ConcurrencyStamp] [nvarchar](1000) NOT NULL,
	[Email] [nvarchar](256) NOT NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetime] NOT NULL,
	[NormalizedEmail] [nvarchar](256) NOT NULL,
	[NormalizedUserName] [nvarchar](256) NOT NULL,
	[PasswordHash] [nvarchar](256) NOT NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[PhoneNumber] [nvarchar](255) NOT NULL,
	[SecurityStamp] [nvarchar](1000) NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[UserName] [nvarchar](256) NOT NULL,
	[RefreshToken] [nvarchar](256) NULL,
	[RefreshTokenExpiryTime] [datetime] NULL,
	[Name] [nvarchar](80) NOT NULL,
	[Gender] [nchar](1) NOT NULL,
	[DOB] [datetime] NOT NULL,
	[Address] [nvarchar](160) NOT NULL,
	[AdharNo] [nvarchar](20) NOT NULL,
	[MaritalStatus] [nchar](1) NOT NULL,
	[Occupation] [nvarchar](15) NOT NULL,
	[ReferBy] [bigint] NOT NULL,
	[MembershipType] [nchar](1) NOT NULL,
	[EntryOn] [datetime] NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserSubscription]    Script Date: 17-09-2022 19:28:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserSubscription](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [bigint] NOT NULL,
	[DateFrom] [datetime] NOT NULL,
	[DateTo] [datetime] NOT NULL,
	[LedgerId] [bigint] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserTokens]    Script Date: 17-09-2022 19:28:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserTokens](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](256) NOT NULL,
	[LoginProvider] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](128) NOT NULL,
	[Value] [nvarchar](128) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VersionInfo]    Script Date: 17-09-2022 19:28:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VersionInfo](
	[Version] [bigint] NOT NULL,
	[AppliedOn] [datetime] NULL,
	[Description] [nvarchar](1024) NULL
) ON [PRIMARY]
GO
/****** Object:  Index [UC_Version]    Script Date: 17-09-2022 19:28:22 ******/
CREATE UNIQUE CLUSTERED INDEX [UC_Version] ON [dbo].[VersionInfo]
(
	[Version] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Table [HangFire].[AggregatedCounter]    Script Date: 17-09-2022 19:28:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [HangFire].[AggregatedCounter](
	[Key] [nvarchar](100) NOT NULL,
	[Value] [bigint] NOT NULL,
	[ExpireAt] [datetime] NULL,
 CONSTRAINT [PK_HangFire_CounterAggregated] PRIMARY KEY CLUSTERED 
(
	[Key] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [HangFire].[Counter]    Script Date: 17-09-2022 19:28:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [HangFire].[Counter](
	[Key] [nvarchar](100) NOT NULL,
	[Value] [int] NOT NULL,
	[ExpireAt] [datetime] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [CX_HangFire_Counter]    Script Date: 17-09-2022 19:28:22 ******/
CREATE CLUSTERED INDEX [CX_HangFire_Counter] ON [HangFire].[Counter]
(
	[Key] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Table [HangFire].[Hash]    Script Date: 17-09-2022 19:28:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [HangFire].[Hash](
	[Key] [nvarchar](100) NOT NULL,
	[Field] [nvarchar](100) NOT NULL,
	[Value] [nvarchar](max) NULL,
	[ExpireAt] [datetime2](7) NULL,
 CONSTRAINT [PK_HangFire_Hash] PRIMARY KEY CLUSTERED 
(
	[Key] ASC,
	[Field] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [HangFire].[Job]    Script Date: 17-09-2022 19:28:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [HangFire].[Job](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[StateId] [bigint] NULL,
	[StateName] [nvarchar](20) NULL,
	[InvocationData] [nvarchar](max) NOT NULL,
	[Arguments] [nvarchar](max) NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[ExpireAt] [datetime] NULL,
 CONSTRAINT [PK_HangFire_Job] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [HangFire].[JobParameter]    Script Date: 17-09-2022 19:28:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [HangFire].[JobParameter](
	[JobId] [bigint] NOT NULL,
	[Name] [nvarchar](40) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_HangFire_JobParameter] PRIMARY KEY CLUSTERED 
(
	[JobId] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [HangFire].[JobQueue]    Script Date: 17-09-2022 19:28:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [HangFire].[JobQueue](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[JobId] [bigint] NOT NULL,
	[Queue] [nvarchar](50) NOT NULL,
	[FetchedAt] [datetime] NULL,
 CONSTRAINT [PK_HangFire_JobQueue] PRIMARY KEY CLUSTERED 
(
	[Queue] ASC,
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [HangFire].[List]    Script Date: 17-09-2022 19:28:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [HangFire].[List](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Key] [nvarchar](100) NOT NULL,
	[Value] [nvarchar](max) NULL,
	[ExpireAt] [datetime] NULL,
 CONSTRAINT [PK_HangFire_List] PRIMARY KEY CLUSTERED 
(
	[Key] ASC,
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [HangFire].[Schema]    Script Date: 17-09-2022 19:28:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [HangFire].[Schema](
	[Version] [int] NOT NULL,
 CONSTRAINT [PK_HangFire_Schema] PRIMARY KEY CLUSTERED 
(
	[Version] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [HangFire].[Server]    Script Date: 17-09-2022 19:28:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [HangFire].[Server](
	[Id] [nvarchar](200) NOT NULL,
	[Data] [nvarchar](max) NULL,
	[LastHeartbeat] [datetime] NOT NULL,
 CONSTRAINT [PK_HangFire_Server] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [HangFire].[Set]    Script Date: 17-09-2022 19:28:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [HangFire].[Set](
	[Key] [nvarchar](100) NOT NULL,
	[Score] [float] NOT NULL,
	[Value] [nvarchar](256) NOT NULL,
	[ExpireAt] [datetime] NULL,
 CONSTRAINT [PK_HangFire_Set] PRIMARY KEY CLUSTERED 
(
	[Key] ASC,
	[Value] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [HangFire].[State]    Script Date: 17-09-2022 19:28:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [HangFire].[State](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[JobId] [bigint] NOT NULL,
	[Name] [nvarchar](20) NOT NULL,
	[Reason] [nvarchar](100) NULL,
	[CreatedAt] [datetime] NOT NULL,
	[Data] [nvarchar](max) NULL,
 CONSTRAINT [PK_HangFire_State] PRIMARY KEY CLUSTERED 
(
	[JobId] ASC,
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[ApplicationRole] ON 
GO
INSERT [dbo].[ApplicationRole] ([Id], [ConcurrencyStamp], [Name], [NormalizedName]) VALUES (1, N'e11d0b01-c7e9-44da-a550-086dbe98fdef', N'Admin', N'ADMIN')
GO
INSERT [dbo].[ApplicationRole] ([Id], [ConcurrencyStamp], [Name], [NormalizedName]) VALUES (2, N'76e1e804-89ef-45ca-9ffb-b5961a41e63e', N'Trainer', N'TRAINER')
GO
INSERT [dbo].[ApplicationRole] ([Id], [ConcurrencyStamp], [Name], [NormalizedName]) VALUES (3, N'a64fb25c-95e2-4256-aaf6-048c6cb7388a', N'Customer', N'CUSTOMER')
GO
SET IDENTITY_INSERT [dbo].[ApplicationRole] OFF
GO
SET IDENTITY_INSERT [dbo].[ErrorLog] ON 
GO
INSERT [dbo].[ErrorLog] ([Id], [ErrorMsg], [ErrorFrom], [ErrorNumber], [EntryOn]) VALUES (8, N'The conversion of a varchar data type to a datetime data type resulted in an out-of-range value.', N'proc_CollectFee', N'242', CAST(N'2022-09-17T11:44:39.383' AS DateTime))
GO
INSERT [dbo].[ErrorLog] ([Id], [ErrorMsg], [ErrorFrom], [ErrorNumber], [EntryOn]) VALUES (9, N'The INSERT statement conflicted with the FOREIGN KEY constraint "FK_Ledger_Users". The conflict occurred in database "TheMuscleBar", table "dbo.Users", column ''Id''.', N'proc_CollectFee', N'547', CAST(N'2022-09-17T12:03:18.677' AS DateTime))
GO
INSERT [dbo].[ErrorLog] ([Id], [ErrorMsg], [ErrorFrom], [ErrorNumber], [EntryOn]) VALUES (10, N'The INSERT statement conflicted with the FOREIGN KEY constraint "FK_Ledger_Users". The conflict occurred in database "TheMuscleBar", table "dbo.Users", column ''Id''.', N'proc_CollectFee', N'547', CAST(N'2022-09-17T12:07:37.193' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[ErrorLog] OFF
GO
SET IDENTITY_INSERT [dbo].[Ledger] ON 
GO
INSERT [dbo].[Ledger] ([Id], [UserId], [TransactionType], [Amount], [discount], [PaymentMode], [EntryOn], [EntryBy]) VALUES (6, 2, N'1', CAST(500.00 AS Numeric(18, 2)), CAST(10.00 AS Numeric(18, 2)), 1, CAST(N'2022-09-17T12:09:30.350' AS DateTime), 1)
GO
INSERT [dbo].[Ledger] ([Id], [UserId], [TransactionType], [Amount], [discount], [PaymentMode], [EntryOn], [EntryBy]) VALUES (7, 2, N'1', CAST(500.00 AS Numeric(18, 2)), CAST(10.00 AS Numeric(18, 2)), 1, CAST(N'2022-09-17T12:22:01.507' AS DateTime), 1)
GO
INSERT [dbo].[Ledger] ([Id], [UserId], [TransactionType], [Amount], [discount], [PaymentMode], [EntryOn], [EntryBy]) VALUES (8, 2, N'1', CAST(500.00 AS Numeric(18, 2)), CAST(10.00 AS Numeric(18, 2)), 1, CAST(N'2022-09-17T13:43:34.117' AS DateTime), 1)
GO
SET IDENTITY_INSERT [dbo].[Ledger] OFF
GO
SET IDENTITY_INSERT [dbo].[NLogs] ON 
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (1, N'Sep 14 2022  5:56PM', N'Could not find stored procedure ''proc_DashboardDonutChart''.', N'Error', N'Could not find stored procedure ''proc_DashboardDonutChart''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (2, N'Sep 14 2022  5:56PM', N'Could not find stored procedure ''proc_DashboardPieChart''.', N'Error', N'Could not find stored procedure ''proc_DashboardPieChart''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (3, N'Sep 14 2022  5:56PM', N'Could not find stored procedure ''proc_GetTransactionReport''.', N'Error', N'Could not find stored procedure ''proc_GetTransactionReport''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (4, N'Sep 14 2022  5:56PM', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (5, N'Sep 14 2022  5:56PM', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (6, N'Sep 14 2022  5:56PM', N'Could not find stored procedure ''proc_getDashboardGraphData''.', N'Error', N'Could not find stored procedure ''proc_getDashboardGraphData''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (7, N'Sep 14 2022  5:56PM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (8, N'Sep 14 2022  5:56PM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_DashboardDonutChart''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (9, N'Sep 14 2022  5:56PM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_DashboardPieChart''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (10, N'Sep 14 2022  5:56PM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (15, N'Sep 14 2022  5:56PM', N'Could not find stored procedure ''proc_users''.', N'Error', N'Could not find stored procedure ''proc_users''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (16, N'Sep 14 2022  6:02PM', N'Could not find stored procedure ''proc_users''.', N'Error', N'Could not find stored procedure ''proc_users''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (18, N'Sep 15 2022  4:20PM', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (19, N'Sep 15 2022  4:20PM', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (20, N'Sep 15 2022  4:20PM', N'Could not find stored procedure ''proc_GetTransactionReport''.', N'Error', N'Could not find stored procedure ''proc_GetTransactionReport''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (21, N'Sep 15 2022  4:20PM', N'Could not find stored procedure ''proc_DashboardDonutChart''.', N'Error', N'Could not find stored procedure ''proc_DashboardDonutChart''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (22, N'Sep 15 2022  4:20PM', N'Could not find stored procedure ''proc_getDashboardGraphData''.', N'Error', N'Could not find stored procedure ''proc_getDashboardGraphData''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (23, N'Sep 15 2022  4:20PM', N'Could not find stored procedure ''proc_DashboardPieChart''.', N'Error', N'Could not find stored procedure ''proc_DashboardPieChart''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (60, N'Sep 15 2022  4:57PM', N'Could not find stored procedure ''proc_DashboardDonutChart''.', N'Error', N'Could not find stored procedure ''proc_DashboardDonutChart''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (61, N'Sep 15 2022  4:57PM', N'Could not find stored procedure ''proc_DashboardPieChart''.', N'Error', N'Could not find stored procedure ''proc_DashboardPieChart''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (17, N'Sep 14 2022  6:03PM', N'Could not find stored procedure ''proc_users''.', N'Error', N'Could not find stored procedure ''proc_users''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (24, N'Sep 15 2022  4:20PM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (25, N'Sep 15 2022  4:20PM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (32, N'Sep 15 2022  4:55PM', N'Could not find stored procedure ''proc_DashboardDonutChart''.', N'Error', N'Could not find stored procedure ''proc_DashboardDonutChart''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (33, N'Sep 15 2022  4:55PM', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (34, N'Sep 15 2022  4:55PM', N'Could not find stored procedure ''proc_DashboardPieChart''.', N'Error', N'Could not find stored procedure ''proc_DashboardPieChart''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (35, N'Sep 15 2022  4:55PM', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (36, N'Sep 15 2022  4:55PM', N'Could not find stored procedure ''proc_getDashboardGraphData''.', N'Error', N'Could not find stored procedure ''proc_getDashboardGraphData''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (52, N'Sep 15 2022  4:56PM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_DashboardDonutChart''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (54, N'Sep 15 2022  4:56PM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (55, N'Sep 15 2022  4:56PM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (56, N'Sep 15 2022  4:56PM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_DashboardPieChart''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (62, N'Sep 15 2022  4:57PM', N'Could not find stored procedure ''proc_GetTransactionReport''.', N'Error', N'Could not find stored procedure ''proc_GetTransactionReport''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (63, N'Sep 15 2022  4:57PM', N'Could not find stored procedure ''proc_getDashboardGraphData''.', N'Error', N'Could not find stored procedure ''proc_getDashboardGraphData''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (64, N'Sep 15 2022  4:57PM', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (65, N'Sep 15 2022  4:57PM', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (102, N'Sep 15 2022  5:33PM', N'Could not find stored procedure ''proc_DashboardDonutChart''.', N'Error', N'Could not find stored procedure ''proc_DashboardDonutChart''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (103, N'Sep 15 2022  5:33PM', N'Could not find stored procedure ''proc_getDashboardGraphData''.', N'Error', N'Could not find stored procedure ''proc_getDashboardGraphData''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (104, N'Sep 15 2022  5:33PM', N'Could not find stored procedure ''proc_GetTransactionReport''.', N'Error', N'Could not find stored procedure ''proc_GetTransactionReport''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (26, N'Sep 15 2022  4:20PM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_DashboardPieChart''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (27, N'Sep 15 2022  4:20PM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_DashboardDonutChart''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (37, N'Sep 15 2022  4:55PM', N'Could not find stored procedure ''proc_GetTransactionReport''.', N'Error', N'Could not find stored procedure ''proc_GetTransactionReport''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (38, N'Sep 15 2022  4:55PM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (39, N'Sep 15 2022  4:55PM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_DashboardDonutChart''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (40, N'Sep 15 2022  4:55PM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (41, N'Sep 15 2022  4:55PM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_DashboardPieChart''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (46, N'Sep 15 2022  4:56PM', N'Could not find stored procedure ''proc_DashboardDonutChart''.', N'Error', N'Could not find stored procedure ''proc_DashboardDonutChart''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (47, N'Sep 15 2022  4:56PM', N'Could not find stored procedure ''proc_getDashboardGraphData''.', N'Error', N'Could not find stored procedure ''proc_getDashboardGraphData''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (48, N'Sep 15 2022  4:56PM', N'Could not find stored procedure ''proc_DashboardPieChart''.', N'Error', N'Could not find stored procedure ''proc_DashboardPieChart''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (49, N'Sep 15 2022  4:56PM', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (50, N'Sep 15 2022  4:56PM', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (51, N'Sep 15 2022  4:56PM', N'Could not find stored procedure ''proc_GetTransactionReport''.', N'Error', N'Could not find stored procedure ''proc_GetTransactionReport''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (74, N'Sep 15 2022  4:58PM', N'Could not find stored procedure ''proc_DashboardDonutChart''.', N'Error', N'Could not find stored procedure ''proc_DashboardDonutChart''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (75, N'Sep 15 2022  4:58PM', N'Could not find stored procedure ''proc_getDashboardGraphData''.', N'Error', N'Could not find stored procedure ''proc_getDashboardGraphData''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (76, N'Sep 15 2022  4:58PM', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (77, N'Sep 15 2022  4:58PM', N'Could not find stored procedure ''proc_DashboardPieChart''.', N'Error', N'Could not find stored procedure ''proc_DashboardPieChart''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (78, N'Sep 15 2022  4:58PM', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (79, N'Sep 15 2022  4:58PM', N'Could not find stored procedure ''proc_GetTransactionReport''.', N'Error', N'Could not find stored procedure ''proc_GetTransactionReport''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (66, N'Sep 15 2022  4:57PM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_DashboardDonutChart''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (68, N'Sep 15 2022  4:57PM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_DashboardPieChart''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (70, N'Sep 15 2022  4:57PM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (71, N'Sep 15 2022  4:57PM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (80, N'Sep 15 2022  4:58PM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_DashboardDonutChart''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (88, N'Sep 15 2022  5:30PM', N'Could not find stored procedure ''proc_GetTransactionReport''.', N'Error', N'Could not find stored procedure ''proc_GetTransactionReport''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (89, N'Sep 15 2022  5:30PM', N'Could not find stored procedure ''proc_DashboardDonutChart''.', N'Error', N'Could not find stored procedure ''proc_DashboardDonutChart''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (90, N'Sep 15 2022  5:30PM', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (91, N'Sep 15 2022  5:30PM', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (92, N'Sep 15 2022  5:30PM', N'Could not find stored procedure ''proc_getDashboardGraphData''.', N'Error', N'Could not find stored procedure ''proc_getDashboardGraphData''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (93, N'Sep 15 2022  5:30PM', N'Could not find stored procedure ''proc_DashboardPieChart''.', N'Error', N'Could not find stored procedure ''proc_DashboardPieChart''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (94, N'Sep 15 2022  5:30PM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (95, N'Sep 15 2022  5:30PM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_DashboardPieChart''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (96, N'Sep 15 2022  5:30PM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (97, N'Sep 15 2022  5:30PM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_DashboardDonutChart''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (108, N'Sep 15 2022  5:33PM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_DashboardDonutChart''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (187, N'Sep 15 2022  6:13PM', N'Could not find stored procedure ''proc_DashboardDonutChart''.', N'Error', N'Could not find stored procedure ''proc_DashboardDonutChart''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (188, N'Sep 15 2022  6:13PM', N'Could not find stored procedure ''proc_DashboardPieChart''.', N'Error', N'Could not find stored procedure ''proc_DashboardPieChart''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (189, N'Sep 15 2022  6:13PM', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (82, N'Sep 15 2022  4:58PM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (83, N'Sep 15 2022  4:58PM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (86, N'Sep 15 2022  4:58PM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_DashboardPieChart''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (105, N'Sep 15 2022  5:33PM', N'Could not find stored procedure ''proc_DashboardPieChart''.', N'Error', N'Could not find stored procedure ''proc_DashboardPieChart''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (106, N'Sep 15 2022  5:33PM', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (107, N'Sep 15 2022  5:33PM', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (116, N'Sep 15 2022  5:33PM', N'Could not find stored procedure ''proc_DashboardDonutChart''.', N'Error', N'Could not find stored procedure ''proc_DashboardDonutChart''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (117, N'Sep 15 2022  5:33PM', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (118, N'Sep 15 2022  5:33PM', N'Could not find stored procedure ''proc_getDashboardGraphData''.', N'Error', N'Could not find stored procedure ''proc_getDashboardGraphData''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (119, N'Sep 15 2022  5:33PM', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (120, N'Sep 15 2022  5:33PM', N'Could not find stored procedure ''proc_DashboardPieChart''.', N'Error', N'Could not find stored procedure ''proc_DashboardPieChart''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (121, N'Sep 15 2022  5:33PM', N'Could not find stored procedure ''proc_GetTransactionReport''.', N'Error', N'Could not find stored procedure ''proc_GetTransactionReport''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (122, N'Sep 15 2022  5:33PM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_DashboardDonutChart''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (124, N'Sep 15 2022  5:33PM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_DashboardPieChart''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (125, N'Sep 15 2022  5:33PM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (128, N'Sep 15 2022  5:33PM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (130, N'Sep 15 2022  5:36PM', N'Could not find stored procedure ''proc_GetTransactionReport''.', N'Error', N'Could not find stored procedure ''proc_GetTransactionReport''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (131, N'Sep 15 2022  5:36PM', N'Could not find stored procedure ''proc_getDashboardGraphData''.', N'Error', N'Could not find stored procedure ''proc_getDashboardGraphData''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (132, N'Sep 15 2022  5:36PM', N'Could not find stored procedure ''proc_DashboardDonutChart''.', N'Error', N'Could not find stored procedure ''proc_DashboardDonutChart''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (109, N'Sep 15 2022  5:33PM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_DashboardPieChart''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (110, N'Sep 15 2022  5:33PM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (111, N'Sep 15 2022  5:33PM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (135, N'Sep 15 2022  5:36PM', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (136, N'Sep 15 2022  5:36PM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_DashboardDonutChart''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (137, N'Sep 15 2022  5:36PM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_DashboardPieChart''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (140, N'Sep 15 2022  5:36PM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (141, N'Sep 15 2022  5:36PM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (150, N'Sep 15 2022  5:59PM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_DashboardDonutChart''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (151, N'Sep 15 2022  5:59PM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (152, N'Sep 15 2022  5:59PM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_DashboardPieChart''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (153, N'Sep 15 2022  5:59PM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (158, N'Sep 15 2022  6:05PM', N'Could not find stored procedure ''proc_DashboardDonutChart''.', N'Error', N'Could not find stored procedure ''proc_DashboardDonutChart''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (159, N'Sep 15 2022  6:05PM', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (160, N'Sep 15 2022  6:05PM', N'Could not find stored procedure ''proc_getDashboardGraphData''.', N'Error', N'Could not find stored procedure ''proc_getDashboardGraphData''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (161, N'Sep 15 2022  6:05PM', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (162, N'Sep 15 2022  6:05PM', N'Could not find stored procedure ''proc_GetTransactionReport''.', N'Error', N'Could not find stored procedure ''proc_GetTransactionReport''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (163, N'Sep 15 2022  6:05PM', N'Could not find stored procedure ''proc_DashboardPieChart''.', N'Error', N'Could not find stored procedure ''proc_DashboardPieChart''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (186, N'Sep 15 2022  6:13PM', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (133, N'Sep 15 2022  5:36PM', N'Could not find stored procedure ''proc_DashboardPieChart''.', N'Error', N'Could not find stored procedure ''proc_DashboardPieChart''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (164, N'Sep 15 2022  6:05PM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (165, N'Sep 15 2022  6:05PM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (166, N'Sep 15 2022  6:05PM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_DashboardDonutChart''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (167, N'Sep 15 2022  6:05PM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_DashboardPieChart''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (178, N'Sep 15 2022  6:07PM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_DashboardDonutChart''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (179, N'Sep 15 2022  6:07PM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (180, N'Sep 15 2022  6:07PM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_DashboardPieChart''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (181, N'Sep 15 2022  6:07PM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (190, N'Sep 15 2022  6:13PM', N'Could not find stored procedure ''proc_getDashboardGraphData''.', N'Error', N'Could not find stored procedure ''proc_getDashboardGraphData''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (191, N'Sep 15 2022  6:13PM', N'Could not find stored procedure ''proc_GetTransactionReport''.', N'Error', N'Could not find stored procedure ''proc_GetTransactionReport''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (200, N'Sep 15 2022  6:32PM', N'Could not find stored procedure ''proc_DashboardDonutChart''.', N'Error', N'Could not find stored procedure ''proc_DashboardDonutChart''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (201, N'Sep 15 2022  6:32PM', N'Could not find stored procedure ''proc_getDashboardGraphData''.', N'Error', N'Could not find stored procedure ''proc_getDashboardGraphData''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (202, N'Sep 15 2022  6:32PM', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (207, N'Sep 15 2022  6:32PM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (216, N'Sep 15 2022  6:36PM', N'Cannot insert the value NULL into column ''EntryOn'', table ''TheMuscleBar.dbo.ErrorLog''; column does not allow nulls. INSERT fails.
The statement has been terminated.', N'Error', N'Cannot insert the value NULL into column ''EntryOn'', table ''TheMuscleBar.dbo.ErrorLog''; column does not allow nulls. INSERT fails.
The statement has been terminated.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (219, N'Sep 17 2022  6:11AM', N'Could not find stored procedure ''proc_DashboardPieChart''.', N'Error', N'Could not find stored procedure ''proc_DashboardPieChart''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (220, N'Sep 17 2022  6:11AM', N'Could not find stored procedure ''proc_GetTransactionReport''.', N'Error', N'Could not find stored procedure ''proc_GetTransactionReport''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (134, N'Sep 15 2022  5:36PM', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (144, N'Sep 15 2022  5:59PM', N'Could not find stored procedure ''proc_DashboardDonutChart''.', N'Error', N'Could not find stored procedure ''proc_DashboardDonutChart''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (145, N'Sep 15 2022  5:59PM', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (146, N'Sep 15 2022  5:59PM', N'Could not find stored procedure ''proc_DashboardPieChart''.', N'Error', N'Could not find stored procedure ''proc_DashboardPieChart''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (147, N'Sep 15 2022  5:59PM', N'Could not find stored procedure ''proc_getDashboardGraphData''.', N'Error', N'Could not find stored procedure ''proc_getDashboardGraphData''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (148, N'Sep 15 2022  5:59PM', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (149, N'Sep 15 2022  5:59PM', N'Could not find stored procedure ''proc_GetTransactionReport''.', N'Error', N'Could not find stored procedure ''proc_GetTransactionReport''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (192, N'Sep 15 2022  6:13PM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_DashboardPieChart''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (193, N'Sep 15 2022  6:13PM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_DashboardDonutChart''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (194, N'Sep 15 2022  6:13PM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (195, N'Sep 15 2022  6:13PM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (203, N'Sep 15 2022  6:32PM', N'Could not find stored procedure ''proc_GetTransactionReport''.', N'Error', N'Could not find stored procedure ''proc_GetTransactionReport''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (204, N'Sep 15 2022  6:32PM', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (205, N'Sep 15 2022  6:32PM', N'Could not find stored procedure ''proc_DashboardPieChart''.', N'Error', N'Could not find stored procedure ''proc_DashboardPieChart''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (206, N'Sep 15 2022  6:32PM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_DashboardDonutChart''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (208, N'Sep 15 2022  6:33PM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (223, N'Sep 17 2022  6:11AM', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (224, N'Sep 17 2022  6:11AM', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (235, N'Sep 17 2022  6:20AM', N'Could not find stored procedure ''proc_DashboardDonutChart''.', N'Error', N'Could not find stored procedure ''proc_DashboardDonutChart''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (172, N'Sep 15 2022  6:06PM', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (173, N'Sep 15 2022  6:06PM', N'Could not find stored procedure ''proc_DashboardDonutChart''.', N'Error', N'Could not find stored procedure ''proc_DashboardDonutChart''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (174, N'Sep 15 2022  6:06PM', N'Could not find stored procedure ''proc_getDashboardGraphData''.', N'Error', N'Could not find stored procedure ''proc_getDashboardGraphData''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (175, N'Sep 15 2022  6:06PM', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (176, N'Sep 15 2022  6:06PM', N'Could not find stored procedure ''proc_DashboardPieChart''.', N'Error', N'Could not find stored procedure ''proc_DashboardPieChart''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (177, N'Sep 15 2022  6:06PM', N'Could not find stored procedure ''proc_GetTransactionReport''.', N'Error', N'Could not find stored procedure ''proc_GetTransactionReport''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (209, N'Sep 15 2022  6:33PM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_DashboardPieChart''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (214, N'Sep 15 2022  6:35PM', N'Cannot insert the value NULL into column ''EntryOn'', table ''TheMuscleBar.dbo.ErrorLog''; column does not allow nulls. INSERT fails.
The statement has been terminated.', N'Error', N'Cannot insert the value NULL into column ''EntryOn'', table ''TheMuscleBar.dbo.ErrorLog''; column does not allow nulls. INSERT fails.
The statement has been terminated.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (215, N'Sep 15 2022  6:35PM', N'Cannot insert the value NULL into column ''EntryOn'', table ''TheMuscleBar.dbo.ErrorLog''; column does not allow nulls. INSERT fails.
The statement has been terminated.', N'Error', N'Cannot insert the value NULL into column ''EntryOn'', table ''TheMuscleBar.dbo.ErrorLog''; column does not allow nulls. INSERT fails.
The statement has been terminated.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (221, N'Sep 17 2022  6:11AM', N'Could not find stored procedure ''proc_DashboardDonutChart''.', N'Error', N'Could not find stored procedure ''proc_DashboardDonutChart''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (225, N'Sep 17 2022  6:11AM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_DashboardPieChart''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (226, N'Sep 17 2022  6:11AM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (234, N'Sep 17 2022  6:12AM', N'Cannot insert the value NULL into column ''EntryOn'', table ''TheMuscleBar.dbo.ErrorLog''; column does not allow nulls. INSERT fails.
The statement has been terminated.', N'Error', N'Cannot insert the value NULL into column ''EntryOn'', table ''TheMuscleBar.dbo.ErrorLog''; column does not allow nulls. INSERT fails.
The statement has been terminated.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (236, N'Sep 17 2022  6:20AM', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (237, N'Sep 17 2022  6:20AM', N'Could not find stored procedure ''proc_getDashboardGraphData''.', N'Error', N'Could not find stored procedure ''proc_getDashboardGraphData''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (238, N'Sep 17 2022  6:20AM', N'Could not find stored procedure ''proc_DashboardPieChart''.', N'Error', N'Could not find stored procedure ''proc_DashboardPieChart''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (217, N'Sep 15 2022  6:36PM', N'Cannot insert the value NULL into column ''EntryOn'', table ''TheMuscleBar.dbo.ErrorLog''; column does not allow nulls. INSERT fails.
The statement has been terminated.', N'Error', N'Cannot insert the value NULL into column ''EntryOn'', table ''TheMuscleBar.dbo.ErrorLog''; column does not allow nulls. INSERT fails.
The statement has been terminated.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (218, N'Sep 15 2022  6:36PM', N'Cannot insert the value NULL into column ''EntryOn'', table ''TheMuscleBar.dbo.ErrorLog''; column does not allow nulls. INSERT fails.
The statement has been terminated.', N'Error', N'Cannot insert the value NULL into column ''EntryOn'', table ''TheMuscleBar.dbo.ErrorLog''; column does not allow nulls. INSERT fails.
The statement has been terminated.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (222, N'Sep 17 2022  6:11AM', N'Could not find stored procedure ''proc_getDashboardGraphData''.', N'Error', N'Could not find stored procedure ''proc_getDashboardGraphData''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (263, N'Sep 17 2022  6:45AM', N'Could not find stored procedure ''proc_DashboardDonutChart''.', N'Error', N'Could not find stored procedure ''proc_DashboardDonutChart''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (264, N'Sep 17 2022  6:45AM', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (265, N'Sep 17 2022  6:45AM', N'Could not find stored procedure ''proc_getDashboardGraphData''.', N'Error', N'Could not find stored procedure ''proc_getDashboardGraphData''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (266, N'Sep 17 2022  6:45AM', N'Could not find stored procedure ''proc_DashboardPieChart''.', N'Error', N'Could not find stored procedure ''proc_DashboardPieChart''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (267, N'Sep 17 2022  6:45AM', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (268, N'Sep 17 2022  6:45AM', N'Could not find stored procedure ''proc_GetTransactionReport''.', N'Error', N'Could not find stored procedure ''proc_GetTransactionReport''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (277, N'Sep 17 2022  6:46AM', N'Could not find stored procedure ''proc_DashboardDonutChart''.', N'Error', N'Could not find stored procedure ''proc_DashboardDonutChart''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (278, N'Sep 17 2022  6:46AM', N'Could not find stored procedure ''proc_DashboardPieChart''.', N'Error', N'Could not find stored procedure ''proc_DashboardPieChart''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (279, N'Sep 17 2022  6:46AM', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (280, N'Sep 17 2022  6:46AM', N'Could not find stored procedure ''proc_getDashboardGraphData''.', N'Error', N'Could not find stored procedure ''proc_getDashboardGraphData''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (281, N'Sep 17 2022  6:46AM', N'Could not find stored procedure ''proc_GetTransactionReport''.', N'Error', N'Could not find stored procedure ''proc_GetTransactionReport''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (282, N'Sep 17 2022  6:46AM', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (291, N'Sep 17 2022  6:50AM', N'Error converting data type nchar to tinyint.', N'Error', N'Error converting data type nchar to tinyint.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (307, N'Sep 17 2022  7:02AM', N'Could not find stored procedure ''proc_getDashboardGraphData''.', N'Error', N'Could not find stored procedure ''proc_getDashboardGraphData''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (308, N'Sep 17 2022  7:02AM', N'Could not find stored procedure ''proc_DashboardDonutChart''.', N'Error', N'Could not find stored procedure ''proc_DashboardDonutChart''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (229, N'Sep 17 2022  6:11AM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_DashboardDonutChart''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (230, N'Sep 17 2022  6:11AM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (233, N'Sep 17 2022  6:12AM', N'Cannot insert the value NULL into column ''EntryOn'', table ''TheMuscleBar.dbo.ErrorLog''; column does not allow nulls. INSERT fails.
The statement has been terminated.', N'Error', N'Cannot insert the value NULL into column ''EntryOn'', table ''TheMuscleBar.dbo.ErrorLog''; column does not allow nulls. INSERT fails.
The statement has been terminated.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (283, N'Sep 17 2022  6:46AM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_DashboardDonutChart''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (284, N'Sep 17 2022  6:46AM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_DashboardPieChart''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (285, N'Sep 17 2022  6:46AM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (286, N'Sep 17 2022  6:46AM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (309, N'Sep 17 2022  7:02AM', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (310, N'Sep 17 2022  7:02AM', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (311, N'Sep 17 2022  7:02AM', N'Could not find stored procedure ''proc_DashboardPieChart''.', N'Error', N'Could not find stored procedure ''proc_DashboardPieChart''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (312, N'Sep 17 2022  7:02AM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (313, N'Sep 17 2022  7:02AM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (314, N'Sep 17 2022  7:02AM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_DashboardDonutChart''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (315, N'Sep 17 2022  7:02AM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_DashboardPieChart''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (320, N'Sep 17 2022  7:04AM', N'Could not find stored procedure ''proc_DashboardDonutChart''.', N'Error', N'Could not find stored procedure ''proc_DashboardDonutChart''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (321, N'Sep 17 2022  7:04AM', N'Could not find stored procedure ''proc_getDashboardGraphData''.', N'Error', N'Could not find stored procedure ''proc_getDashboardGraphData''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (322, N'Sep 17 2022  7:04AM', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (323, N'Sep 17 2022  7:04AM', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (239, N'Sep 17 2022  6:20AM', N'Could not find stored procedure ''proc_GetTransactionReport''.', N'Error', N'Could not find stored procedure ''proc_GetTransactionReport''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (255, N'Sep 17 2022  6:31AM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (256, N'Sep 17 2022  6:31AM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (257, N'Sep 17 2022  6:31AM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_DashboardDonutChart''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (258, N'Sep 17 2022  6:31AM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_DashboardPieChart''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (269, N'Sep 17 2022  6:45AM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_DashboardDonutChart''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (270, N'Sep 17 2022  6:45AM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (271, N'Sep 17 2022  6:45AM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_DashboardPieChart''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (272, N'Sep 17 2022  6:45AM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (296, N'Sep 17 2022  6:53AM', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (297, N'Sep 17 2022  6:53AM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_DashboardDonutChart''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (299, N'Sep 17 2022  6:53AM', N'Could not find stored procedure ''proc_DashboardPieChart''.', N'Error', N'Could not find stored procedure ''proc_DashboardPieChart''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (300, N'Sep 17 2022  6:53AM', N'Could not find stored procedure ''proc_getDashboardGraphData''.', N'Error', N'Could not find stored procedure ''proc_getDashboardGraphData''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (301, N'Sep 17 2022  6:53AM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (303, N'Sep 17 2022  6:53AM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (305, N'Sep 17 2022  6:53AM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_DashboardPieChart''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (336, N'Sep 17 2022  7:32AM', N'Could not find stored procedure ''proc_DashboardPieChart''.', N'Error', N'Could not find stored procedure ''proc_DashboardPieChart''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (337, N'Sep 17 2022  7:32AM', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (338, N'Sep 17 2022  7:32AM', N'Could not find stored procedure ''proc_DashboardDonutChart''.', N'Error', N'Could not find stored procedure ''proc_DashboardDonutChart''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (240, N'Sep 17 2022  6:20AM', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (241, N'Sep 17 2022  6:20AM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_DashboardDonutChart''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (242, N'Sep 17 2022  6:20AM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (243, N'Sep 17 2022  6:20AM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_DashboardPieChart''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (244, N'Sep 17 2022  6:20AM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (249, N'Sep 17 2022  6:31AM', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (250, N'Sep 17 2022  6:31AM', N'Could not find stored procedure ''proc_getDashboardGraphData''.', N'Error', N'Could not find stored procedure ''proc_getDashboardGraphData''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (251, N'Sep 17 2022  6:31AM', N'Could not find stored procedure ''proc_DashboardDonutChart''.', N'Error', N'Could not find stored procedure ''proc_DashboardDonutChart''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (252, N'Sep 17 2022  6:31AM', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (253, N'Sep 17 2022  6:31AM', N'Could not find stored procedure ''proc_GetTransactionReport''.', N'Error', N'Could not find stored procedure ''proc_GetTransactionReport''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (254, N'Sep 17 2022  6:31AM', N'Could not find stored procedure ''proc_DashboardPieChart''.', N'Error', N'Could not find stored procedure ''proc_DashboardPieChart''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (292, N'Sep 17 2022  6:50AM', N'Error converting data type nchar to tinyint.', N'Error', N'Error converting data type nchar to tinyint.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (293, N'Sep 17 2022  6:53AM', N'Could not find stored procedure ''proc_DashboardDonutChart''.', N'Error', N'Could not find stored procedure ''proc_DashboardDonutChart''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (294, N'Sep 17 2022  6:53AM', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (295, N'Sep 17 2022  6:53AM', N'Could not find stored procedure ''proc_GetTransactionReport''.', N'Error', N'Could not find stored procedure ''proc_GetTransactionReport''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (325, N'Sep 17 2022  7:04AM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_DashboardDonutChart''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (327, N'Sep 17 2022  7:04AM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (333, N'Sep 17 2022  7:04AM', N'Could not find stored procedure ''proc_DashboardDonutChart''.', N'Error', N'Could not find stored procedure ''proc_DashboardDonutChart''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (334, N'Sep 17 2022  7:04AM', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (335, N'Sep 17 2022  7:04AM', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (324, N'Sep 17 2022  7:04AM', N'Could not find stored procedure ''proc_DashboardPieChart''.', N'Error', N'Could not find stored procedure ''proc_DashboardPieChart''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (329, N'Sep 17 2022  7:04AM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_DashboardPieChart''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (330, N'Sep 17 2022  7:04AM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (339, N'Sep 17 2022  7:32AM', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (340, N'Sep 17 2022  7:33AM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_DashboardPieChart''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (341, N'Sep 17 2022  7:33AM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_DashboardDonutChart''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (342, N'Sep 17 2022  7:33AM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (343, N'Sep 17 2022  7:33AM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (352, N'Sep 17 2022  7:33AM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (353, N'Sep 17 2022  7:33AM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (356, N'Sep 17 2022  7:33AM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_DashboardPieChart''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (357, N'Sep 17 2022  7:33AM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_DashboardDonutChart''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (387, N'Sep 17 2022  7:35AM', N'Could not find stored procedure ''proc_DashboardPieChart''.', N'Error', N'Could not find stored procedure ''proc_DashboardPieChart''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (388, N'Sep 17 2022  7:36AM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (390, N'Sep 17 2022  7:36AM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (391, N'Sep 17 2022  7:36AM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_DashboardDonutChart''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (396, N'Sep 17 2022  7:36AM', N'Could not find stored procedure ''proc_DashboardDonutChart''.', N'Error', N'Could not find stored procedure ''proc_DashboardDonutChart''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (397, N'Sep 17 2022  7:36AM', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (398, N'Sep 17 2022  7:36AM', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (348, N'Sep 17 2022  7:33AM', N'Could not find stored procedure ''proc_DashboardDonutChart''.', N'Error', N'Could not find stored procedure ''proc_DashboardDonutChart''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (349, N'Sep 17 2022  7:33AM', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (350, N'Sep 17 2022  7:33AM', N'Could not find stored procedure ''proc_DashboardPieChart''.', N'Error', N'Could not find stored procedure ''proc_DashboardPieChart''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (351, N'Sep 17 2022  7:33AM', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (360, N'Sep 17 2022  7:34AM', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (361, N'Sep 17 2022  7:34AM', N'Could not find stored procedure ''proc_DashboardPieChart''.', N'Error', N'Could not find stored procedure ''proc_DashboardPieChart''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (362, N'Sep 17 2022  7:34AM', N'Could not find stored procedure ''proc_DashboardDonutChart''.', N'Error', N'Could not find stored procedure ''proc_DashboardDonutChart''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (363, N'Sep 17 2022  7:34AM', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (364, N'Sep 17 2022  7:34AM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (366, N'Sep 17 2022  7:35AM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_DashboardDonutChart''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (368, N'Sep 17 2022  7:35AM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (369, N'Sep 17 2022  7:35AM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_DashboardPieChart''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (372, N'Sep 17 2022  7:35AM', N'Could not find stored procedure ''proc_DashboardDonutChart''.', N'Error', N'Could not find stored procedure ''proc_DashboardDonutChart''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (373, N'Sep 17 2022  7:35AM', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (374, N'Sep 17 2022  7:35AM', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (375, N'Sep 17 2022  7:35AM', N'Could not find stored procedure ''proc_DashboardPieChart''.', N'Error', N'Could not find stored procedure ''proc_DashboardPieChart''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (376, N'Sep 17 2022  7:35AM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_DashboardDonutChart''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (445, N'Sep 17 2022  8:03AM', N'Error parsing column 4 (TransactionType=c - String)', N'Error', N'Error parsing column 4 (TransactionType=c - String)', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (446, N'Sep 17 2022  8:04AM', N'Error parsing column 4 (TransactionType=c - String)', N'Error', N'Error parsing column 4 (TransactionType=c - String)', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (378, N'Sep 17 2022  7:35AM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (379, N'Sep 17 2022  7:35AM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_DashboardPieChart''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (380, N'Sep 17 2022  7:35AM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (393, N'Sep 17 2022  7:36AM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_DashboardPieChart''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (399, N'Sep 17 2022  7:36AM', N'Could not find stored procedure ''proc_DashboardPieChart''.', N'Error', N'Could not find stored procedure ''proc_DashboardPieChart''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (400, N'Sep 17 2022  7:36AM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (401, N'Sep 17 2022  7:36AM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_DashboardDonutChart''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (404, N'Sep 17 2022  7:36AM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_DashboardPieChart''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (405, N'Sep 17 2022  7:36AM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (420, N'Sep 17 2022  7:38AM', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (421, N'Sep 17 2022  7:38AM', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (422, N'Sep 17 2022  7:38AM', N'Could not find stored procedure ''proc_DashboardPieChart''.', N'Error', N'Could not find stored procedure ''proc_DashboardPieChart''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (423, N'Sep 17 2022  7:38AM', N'Could not find stored procedure ''proc_DashboardDonutChart''.', N'Error', N'Could not find stored procedure ''proc_DashboardDonutChart''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (432, N'Sep 17 2022  7:39AM', N'Could not find stored procedure ''proc_DashboardDonutChart''.', N'Error', N'Could not find stored procedure ''proc_DashboardDonutChart''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (433, N'Sep 17 2022  7:39AM', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (434, N'Sep 17 2022  7:39AM', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (435, N'Sep 17 2022  7:39AM', N'Could not find stored procedure ''proc_DashboardPieChart''.', N'Error', N'Could not find stored procedure ''proc_DashboardPieChart''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (384, N'Sep 17 2022  7:35AM', N'Could not find stored procedure ''proc_DashboardDonutChart''.', N'Error', N'Could not find stored procedure ''proc_DashboardDonutChart''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (385, N'Sep 17 2022  7:35AM', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (386, N'Sep 17 2022  7:35AM', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (444, N'Sep 17 2022  7:43AM', N'Could not find stored procedure ''proc_users''.', N'Error', N'Could not find stored procedure ''proc_users''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (408, N'Sep 17 2022  7:36AM', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (409, N'Sep 17 2022  7:36AM', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (410, N'Sep 17 2022  7:36AM', N'Could not find stored procedure ''proc_DashboardDonutChart''.', N'Error', N'Could not find stored procedure ''proc_DashboardDonutChart''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (411, N'Sep 17 2022  7:36AM', N'Could not find stored procedure ''proc_DashboardPieChart''.', N'Error', N'Could not find stored procedure ''proc_DashboardPieChart''.', N'', N'TheMuscleBar.AppCode.DAL.DapperRepository')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (412, N'Sep 17 2022  7:36AM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_DashboardDonutChart''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (413, N'Sep 17 2022  7:36AM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (416, N'Sep 17 2022  7:36AM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_DashboardPieChart''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (417, N'Sep 17 2022  7:36AM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (424, N'Sep 17 2022  7:38AM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (426, N'Sep 17 2022  7:38AM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (427, N'Sep 17 2022  7:38AM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_DashboardPieChart''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (428, N'Sep 17 2022  7:38AM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_DashboardDonutChart''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (436, N'Sep 17 2022  7:39AM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_DashboardDonutChart''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (438, N'Sep 17 2022  7:40AM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (439, N'Sep 17 2022  7:40AM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_Dashboardsummary''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
INSERT [dbo].[NLogs] ([Id], [When], [Message], [Level], [Exception], [Trace], [Logger]) VALUES (440, N'Sep 17 2022  7:40AM', N'An unhandled exception has occurred while executing the request.', N'Error', N'Could not find stored procedure ''proc_DashboardPieChart''.', N'', N'Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware')
GO
SET IDENTITY_INSERT [dbo].[NLogs] OFF
GO
SET IDENTITY_INSERT [dbo].[UserRoles] ON 
GO
INSERT [dbo].[UserRoles] ([Id], [UserId], [RoleId]) VALUES (1, N'1', N'1')
GO
INSERT [dbo].[UserRoles] ([Id], [UserId], [RoleId]) VALUES (2, N'2', N'3')
GO
SET IDENTITY_INSERT [dbo].[UserRoles] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 
GO
INSERT [dbo].[Users] ([Id], [AccessFailedCount], [ConcurrencyStamp], [Email], [EmailConfirmed], [LockoutEnabled], [LockoutEnd], [NormalizedEmail], [NormalizedUserName], [PasswordHash], [PhoneNumberConfirmed], [PhoneNumber], [SecurityStamp], [TwoFactorEnabled], [UserName], [RefreshToken], [RefreshTokenExpiryTime], [Name], [Gender], [DOB], [Address], [AdharNo], [MaritalStatus], [Occupation], [ReferBy], [MembershipType], [EntryOn], [IsActive]) VALUES (1, 0, N'411ecb83-6ae9-4c86-9364-e85fc290e9ea', N'Admin', 0, 0, CAST(N'2022-09-14T23:26:14.000' AS DateTime), N'Admin', N'Admin', N'AQAAAAEAACcQAAAAEBAePcp00ZZ3yX9g7osGDRy0shH6up80DmruJmCASZz+Yq43qPac2Xy2pBrE6DkiBA==', 0, N'9936301548', N'', 0, N'Admin', NULL, NULL, N'Amit Singh', N'M', CAST(N'2020-01-01T00:00:00.000' AS DateTime), N'N/A', N'N/A', N'S', N'Service', 0, N'4', CAST(N'2022-09-14T00:00:00.000' AS DateTime), 1)
GO
INSERT [dbo].[Users] ([Id], [AccessFailedCount], [ConcurrencyStamp], [Email], [EmailConfirmed], [LockoutEnabled], [LockoutEnd], [NormalizedEmail], [NormalizedUserName], [PasswordHash], [PhoneNumberConfirmed], [PhoneNumber], [SecurityStamp], [TwoFactorEnabled], [UserName], [RefreshToken], [RefreshTokenExpiryTime], [Name], [Gender], [DOB], [Address], [AdharNo], [MaritalStatus], [Occupation], [ReferBy], [MembershipType], [EntryOn], [IsActive]) VALUES (2, 0, N'1539f81c-ddbf-4275-96f9-c0a950be3217', N'ri@gmail.com', 0, 0, CAST(N'2022-09-14T23:27:29.000' AS DateTime), N'RI@GMAIL.COM', N'RI@GMAIL.COM', N'AQAAAAEAACcQAAAAEAUtOw6em8pNCSA76y1zFsQPxe3o92MeWqwGDTjPAworKFDxVsYlmRhc6nQtZLmdMA==', 0, N'9936301548', N'', 0, N'ri@gmail.com', NULL, NULL, N'Risabh', N'M', CAST(N'2022-09-14T00:00:00.000' AS DateTime), N'N/A', N'N/A', N'S', N'Service', 0, N'1', CAST(N'2022-09-14T23:27:29.000' AS DateTime), 1)
GO
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
SET IDENTITY_INSERT [dbo].[UserSubscription] ON 
GO
INSERT [dbo].[UserSubscription] ([Id], [UserId], [DateFrom], [DateTo], [LedgerId]) VALUES (2, 2, CAST(N'2022-09-01T00:00:00.000' AS DateTime), CAST(N'2022-09-30T00:00:00.000' AS DateTime), 8)
GO
SET IDENTITY_INSERT [dbo].[UserSubscription] OFF
GO
INSERT [dbo].[VersionInfo] ([Version], [AppliedOn], [Description]) VALUES (202106280001, CAST(N'2022-09-14T17:56:14.000' AS DateTime), N'InitialTables_202106280001')
GO
INSERT [dbo].[VersionInfo] ([Version], [AppliedOn], [Description]) VALUES (202106280002, CAST(N'2022-09-14T17:56:14.000' AS DateTime), N'InitialSeed_202106280002')
GO
INSERT [dbo].[VersionInfo] ([Version], [AppliedOn], [Description]) VALUES (202106280003, CAST(N'2022-09-14T17:56:14.000' AS DateTime), N'InitialStoreprocedure_202106280003')
GO
INSERT [HangFire].[Schema] ([Version]) VALUES (7)
GO
INSERT [HangFire].[Server] ([Id], [Data], [LastHeartbeat]) VALUES (N'laptop-jolkf6fr:9444:9b630551-34e6-4c5f-a591-a4252acd7db1', N'{"WorkerCount":20,"Queues":["default"],"StartedAt":"2022-09-17T13:55:37.8977019Z"}', CAST(N'2022-09-17T13:57:08.640' AS DateTime))
GO
/****** Object:  Index [IX_HangFire_AggregatedCounter_ExpireAt]    Script Date: 17-09-2022 19:28:22 ******/
CREATE NONCLUSTERED INDEX [IX_HangFire_AggregatedCounter_ExpireAt] ON [HangFire].[AggregatedCounter]
(
	[ExpireAt] ASC
)
WHERE ([ExpireAt] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_HangFire_Hash_ExpireAt]    Script Date: 17-09-2022 19:28:22 ******/
CREATE NONCLUSTERED INDEX [IX_HangFire_Hash_ExpireAt] ON [HangFire].[Hash]
(
	[ExpireAt] ASC
)
WHERE ([ExpireAt] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_HangFire_Job_ExpireAt]    Script Date: 17-09-2022 19:28:22 ******/
CREATE NONCLUSTERED INDEX [IX_HangFire_Job_ExpireAt] ON [HangFire].[Job]
(
	[ExpireAt] ASC
)
INCLUDE([StateName]) 
WHERE ([ExpireAt] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_HangFire_Job_StateName]    Script Date: 17-09-2022 19:28:22 ******/
CREATE NONCLUSTERED INDEX [IX_HangFire_Job_StateName] ON [HangFire].[Job]
(
	[StateName] ASC
)
WHERE ([StateName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_HangFire_List_ExpireAt]    Script Date: 17-09-2022 19:28:22 ******/
CREATE NONCLUSTERED INDEX [IX_HangFire_List_ExpireAt] ON [HangFire].[List]
(
	[ExpireAt] ASC
)
WHERE ([ExpireAt] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_HangFire_Server_LastHeartbeat]    Script Date: 17-09-2022 19:28:22 ******/
CREATE NONCLUSTERED INDEX [IX_HangFire_Server_LastHeartbeat] ON [HangFire].[Server]
(
	[LastHeartbeat] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_HangFire_Set_ExpireAt]    Script Date: 17-09-2022 19:28:22 ******/
CREATE NONCLUSTERED INDEX [IX_HangFire_Set_ExpireAt] ON [HangFire].[Set]
(
	[ExpireAt] ASC
)
WHERE ([ExpireAt] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_HangFire_Set_Score]    Script Date: 17-09-2022 19:28:22 ******/
CREATE NONCLUSTERED INDEX [IX_HangFire_Set_Score] ON [HangFire].[Set]
(
	[Key] ASC,
	[Score] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Ledger]  WITH CHECK ADD  CONSTRAINT [FK_Ledger_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Ledger] CHECK CONSTRAINT [FK_Ledger_Users]
GO
ALTER TABLE [dbo].[UserSubscription]  WITH CHECK ADD  CONSTRAINT [FK_UserSubscription_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[UserSubscription] CHECK CONSTRAINT [FK_UserSubscription_Users]
GO
ALTER TABLE [HangFire].[JobParameter]  WITH CHECK ADD  CONSTRAINT [FK_HangFire_JobParameter_Job] FOREIGN KEY([JobId])
REFERENCES [HangFire].[Job] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [HangFire].[JobParameter] CHECK CONSTRAINT [FK_HangFire_JobParameter_Job]
GO
ALTER TABLE [HangFire].[State]  WITH CHECK ADD  CONSTRAINT [FK_HangFire_State_Job] FOREIGN KEY([JobId])
REFERENCES [HangFire].[Job] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [HangFire].[State] CHECK CONSTRAINT [FK_HangFire_State_Job]
GO
/****** Object:  StoredProcedure [dbo].[AddUser]    Script Date: 17-09-2022 19:28:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create proc [dbo].[AddUser](@Id int,@SecurityStamp nvarchar(max)='' ,@PhoneNumberConfirmed bit,              
                                                     @PhoneNumber nvarchar(15)='',@PasswordHash nvarchar(max) ,@NormalizedUserName nvarchar(256) ,               
                                                     @NormalizedEmail nvarchar (256) ,@LockoutEnd datetimeoffset(7) = '',@LockoutEnabled bit,
                                                     @EmailConfirmed bit ,@Email nvarchar(256) ,@ConcurrencyStamp nvarchar(max) ,@AccessFailedCount int,
                                                     @TwoFactorEnabled bit,@UserName nvarchar(256),@Role varchar(30),@Name varchar(100),
                                                     @Gender char(1),@DOB varchar(11),@Address varchar(160),@AdharNo varchar(20),@MaritalStatus char(1),
                                                     @Occupation varchar(15),@ReferBy varchar(10),@MembershipType char(1)
                                                     )                
                            AS            
                            BEGIN            
                             Begin Try            
                              Begin Tran      
                               insert into Users (UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,
                                                  PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount,[Name],IsActive,
                                                  Gender,DOB,Address,AdharNo,MaritalStatus,Occupation,ReferBy,MembershipType,EntryOn)
                                           values(@UserName,@NormalizedUserName,@Email,@NormalizedEmail,@EmailConfirmed,@PasswordHash,ISNULL(@SecurityStamp,''),
                                                  @ConcurrencyStamp,ISNULL(@PhoneNumber,''),@PhoneNumberConfirmed,@TwoFactorEnabled,ISNULL(@LockoutEnd,GetDate()),
                                                  @LockoutEnabled,@AccessFailedCount,@Name,1,@Gender,@DOB,@Address,@AdharNo,@MaritalStatus,@Occupation,0,@MembershipType,Getdate())
                               Select @Id = SCOPE_IDENTITY()            
                               Declare @RoleId int            
                               Select @RoleId = Id from ApplicationRole(nolock) where [Name]=@Role            
                               insert into UserRoles(UserId,RoleId) values(@Id,@RoleId)            
                              Commit Tran            
                             End Try            
                             Begin Catch            
                              Rollback Tran            
                              SET NOCOUNT ON    
                              DECLARE @ErrorNumber varchar(15) = Error_Number(),@ERRORMESSAGE varchar(max)=ERROR_MESSAGE();    
                              insert into ErrorLog(ErrorMsg,ErrorNumber,ErrorFrom,EntryOn) values(@ERRORMESSAGE,@ErrorNumber,'AddUser',GETDATE());    
                              --declare @t table (n int not null unique);    
                              THROW 50000,@ERRORMESSAGE,1;    
                             End Catch            
                             end
GO
/****** Object:  StoredProcedure [dbo].[proc_CollectFee]    Script Date: 17-09-2022 19:28:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[proc_CollectFee]  
       @UserId int,  
       @Amount numeric(18,2),  
       @Discount numeric(18,2),  
       @FromDate varchar(12),  
       @ToDate varchar(12),  
       @TransactionType char(1),  
       @PaymentMode  tinyint,  
       @EntryBy int  
AS  
BEGIN  
 BEGIN TRY  
  BEGIN TRAN  
   DECLARE @LedgerId int  
   insert into Ledger(UserId,TransactionType,Amount,discount,PaymentMode,EntryOn,EntryBy) Values (@UserId,@TransactionType,@Amount,@discount,@PaymentMode,GETDATE(),@EntryBy)  
   SET @LedgerId = SCOPE_IDENTITY()  
   Declare @SubscriptionId int = 0 
   Select @SubscriptionId = Id from UserSubscription(nolock) where UserId = @UserId
   IF @SubscriptionId = 0
		Insert into UserSubscription(UserId,DateFrom,DateTo,LedgerId) values (@UserId,@FromDate,@ToDate,@LedgerId)  
	Else
		UPDATE UserSubscription SET DateFrom=@FromDate , DateTo = @ToDate , LedgerId = @LedgerId Where Id = @SubscriptionId
   Select 1 StatusCode,'Success' ResponseText  
  COMMIT  
 END TRY  
 BEGIN CATCH  
  ROLLBACK  
  Insert into ErrorLog(ErrorMsg,ErrorFrom,ErrorNumber,EntryOn) Values(ERROR_MESSAGE(),ERROR_PROCEDURE(),ERROR_NUMBER(),GetDate())
  Select -1 StatusCode,'Something went wrong' ResponseText  
 END CATCH  
END
GO
/****** Object:  StoredProcedure [dbo].[proc_getUserRole]    Script Date: 17-09-2022 19:28:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[proc_getUserRole]      
@Id bigint=0,      
@Email varchar(120)='',  
@mobileNo varchar(12)=''  
as      
begin      
if(@Id=0 and @Email='')      
begin      
select * from UserRoles  where 1=2      
return       
end      
if(@Id=0 and @Email<>'')      
begin      
select  @Id=Id from Users where NormalizedUserName=@Email      
end      
      
select RoleId from UserRoles where UserID=@Id      
end
GO
/****** Object:  StoredProcedure [dbo].[proc_SaveNLog]    Script Date: 17-09-2022 19:28:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[proc_SaveNLog] @msg varchar(max),@level varchar(max),@exception varchar(max),
                                                    @trace varchar(max),@logger varchar(max)  
                          As  
                          Begin
                          	INSERT INTO [NLogs]([When],[Message],[Level],Exception,Trace,Logger) VALUES (getutcdate(),@msg,@level,@exception,@trace,@logger)  
                          End  
GO
/****** Object:  StoredProcedure [dbo].[UpdateUser]    Script Date: 17-09-2022 19:28:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Proc [dbo].[UpdateUser]
							                  @Id int,
							                  @PasswordHash nvarchar(max) ,							
							                  @TwoFactorEnabled bit,
							                  @RefreshToken varchar(256) = '',
							                  @RefreshTokenExpiryTime varchar(256) = null
                            AS    
                            BEGIN    
                             IF ISNULL(@PasswordHash,'')<>''  
                            	Update Users Set PasswordHash=@PasswordHash where Id=@Id
                             IF ISNULL(@RefreshToken,'')<>''
                            	Update Users Set RefreshToken=@RefreshToken , RefreshTokenExpiryTime = @RefreshTokenExpiryTime where Id=@Id
                            
                            END
GO
USE [master]
GO
ALTER DATABASE [TheMuscleBar] SET  READ_WRITE 
GO
