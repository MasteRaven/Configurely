USE [master]
GO
/****** Object:  Database [Configurely]    Script Date: 1/15/2019 2:49:46 AM ******/
CREATE DATABASE [Configurely]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Configurely', FILENAME = N'C:\Users\XBDIA65\Configurely.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Configurely_log', FILENAME = N'C:\Users\XBDIA65\Configurely_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [Configurely] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Configurely].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Configurely] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Configurely] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Configurely] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Configurely] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Configurely] SET ARITHABORT OFF 
GO
ALTER DATABASE [Configurely] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Configurely] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Configurely] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Configurely] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Configurely] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Configurely] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Configurely] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Configurely] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Configurely] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Configurely] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Configurely] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Configurely] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Configurely] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Configurely] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Configurely] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Configurely] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Configurely] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Configurely] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Configurely] SET  MULTI_USER 
GO
ALTER DATABASE [Configurely] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Configurely] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Configurely] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Configurely] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Configurely] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Configurely] SET QUERY_STORE = OFF
GO
USE [Configurely]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
USE [Configurely]
GO
/****** Object:  Table [dbo].[Department]    Script Date: 1/15/2019 2:49:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Department](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Department] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DepartmentField]    Script Date: 1/15/2019 2:49:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DepartmentField](
	[DepartmentID] [int] NOT NULL,
	[FieldID] [int] NOT NULL,
 CONSTRAINT [PK_DepartmentField] PRIMARY KEY CLUSTERED 
(
	[DepartmentID] ASC,
	[FieldID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employee]    Script Date: 1/15/2019 2:49:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employee](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](300) NOT NULL,
	[DepartmentID] [int] NOT NULL,
	[DateCreated] [datetime] NULL,
 CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmployeeData]    Script Date: 1/15/2019 2:49:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmployeeData](
	[EmployeeID] [int] NOT NULL,
	[FieldID] [int] NOT NULL,
	[Value] [nvarchar](500) NOT NULL,
 CONSTRAINT [PK_EmployeeData] PRIMARY KEY CLUSTERED 
(
	[EmployeeID] ASC,
	[FieldID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Field]    Script Date: 1/15/2019 2:49:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Field](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[TypeID] [int] NOT NULL,
	[DefaultValue] [nvarchar](50) NULL,
	[Sort] [int] NULL,
	[Value] [nvarchar](500) NULL,
 CONSTRAINT [PK_Field] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FieldType]    Script Date: 1/15/2019 2:49:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FieldType](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Type] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_FieldType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Department] ON 

INSERT [dbo].[Department] ([ID], [Name]) VALUES (1, N'IT Headquarters')
INSERT [dbo].[Department] ([ID], [Name]) VALUES (2, N'IT Client')
INSERT [dbo].[Department] ([ID], [Name]) VALUES (3, N'Financial')
INSERT [dbo].[Department] ([ID], [Name]) VALUES (4, N'HR')
SET IDENTITY_INSERT [dbo].[Department] OFF
INSERT [dbo].[DepartmentField] ([DepartmentID], [FieldID]) VALUES (1, 2)
INSERT [dbo].[DepartmentField] ([DepartmentID], [FieldID]) VALUES (1, 3)
INSERT [dbo].[DepartmentField] ([DepartmentID], [FieldID]) VALUES (1, 4)
INSERT [dbo].[DepartmentField] ([DepartmentID], [FieldID]) VALUES (1, 5)
INSERT [dbo].[DepartmentField] ([DepartmentID], [FieldID]) VALUES (1, 6)
INSERT [dbo].[DepartmentField] ([DepartmentID], [FieldID]) VALUES (2, 2)
INSERT [dbo].[DepartmentField] ([DepartmentID], [FieldID]) VALUES (2, 3)
INSERT [dbo].[DepartmentField] ([DepartmentID], [FieldID]) VALUES (2, 4)
INSERT [dbo].[DepartmentField] ([DepartmentID], [FieldID]) VALUES (2, 5)
INSERT [dbo].[DepartmentField] ([DepartmentID], [FieldID]) VALUES (3, 2)
INSERT [dbo].[DepartmentField] ([DepartmentID], [FieldID]) VALUES (3, 3)
SET IDENTITY_INSERT [dbo].[Employee] ON 

INSERT [dbo].[Employee] ([ID], [Name], [DepartmentID], [DateCreated]) VALUES (1, N'Luis', 1, CAST(N'2019-01-14T14:20:51.713' AS DateTime))
INSERT [dbo].[Employee] ([ID], [Name], [DepartmentID], [DateCreated]) VALUES (2, N'John', 2, CAST(N'2019-01-14T14:20:51.713' AS DateTime))
INSERT [dbo].[Employee] ([ID], [Name], [DepartmentID], [DateCreated]) VALUES (3, N'Luis 2', 1, CAST(N'2019-01-14T14:20:51.713' AS DateTime))
INSERT [dbo].[Employee] ([ID], [Name], [DepartmentID], [DateCreated]) VALUES (4, N'Teste 2', 3, CAST(N'2019-01-14T22:31:39.893' AS DateTime))
INSERT [dbo].[Employee] ([ID], [Name], [DepartmentID], [DateCreated]) VALUES (5, N'Teste 3', 3, CAST(N'2019-01-15T01:00:38.470' AS DateTime))
SET IDENTITY_INSERT [dbo].[Employee] OFF
INSERT [dbo].[EmployeeData] ([EmployeeID], [FieldID], [Value]) VALUES (1, 2, N'Teste 1')
INSERT [dbo].[EmployeeData] ([EmployeeID], [FieldID], [Value]) VALUES (1, 3, N'Teste 1')
INSERT [dbo].[EmployeeData] ([EmployeeID], [FieldID], [Value]) VALUES (1, 4, N'Female')
INSERT [dbo].[EmployeeData] ([EmployeeID], [FieldID], [Value]) VALUES (1, 5, N'Another Status')
INSERT [dbo].[EmployeeData] ([EmployeeID], [FieldID], [Value]) VALUES (1, 6, N'Active')
INSERT [dbo].[EmployeeData] ([EmployeeID], [FieldID], [Value]) VALUES (3, 2, N'wer')
INSERT [dbo].[EmployeeData] ([EmployeeID], [FieldID], [Value]) VALUES (3, 3, N'wer')
INSERT [dbo].[EmployeeData] ([EmployeeID], [FieldID], [Value]) VALUES (3, 4, N'Male')
INSERT [dbo].[EmployeeData] ([EmployeeID], [FieldID], [Value]) VALUES (3, 5, N'Other Status|Working')
INSERT [dbo].[EmployeeData] ([EmployeeID], [FieldID], [Value]) VALUES (3, 6, N'Active')
INSERT [dbo].[EmployeeData] ([EmployeeID], [FieldID], [Value]) VALUES (4, 2, N'Teste 2 First Name')
INSERT [dbo].[EmployeeData] ([EmployeeID], [FieldID], [Value]) VALUES (5, 2, N'Teste 6')
SET IDENTITY_INSERT [dbo].[Field] ON 

INSERT [dbo].[Field] ([ID], [Name], [TypeID], [DefaultValue], [Sort], [Value]) VALUES (2, N'First Name', 1, NULL, 1, NULL)
INSERT [dbo].[Field] ([ID], [Name], [TypeID], [DefaultValue], [Sort], [Value]) VALUES (3, N'Last Name', 1, NULL, 2, NULL)
INSERT [dbo].[Field] ([ID], [Name], [TypeID], [DefaultValue], [Sort], [Value]) VALUES (4, N'Gender', 4, N'Genderless', 3, N'Male|Female|Genderless')
INSERT [dbo].[Field] ([ID], [Name], [TypeID], [DefaultValue], [Sort], [Value]) VALUES (5, N'Status', 2, N'Other Status', 5, N'Working|Vacation|Other Status|Another Status')
INSERT [dbo].[Field] ([ID], [Name], [TypeID], [DefaultValue], [Sort], [Value]) VALUES (6, N'IsActive', 3, N'Disable', 4, N'Active|Disable')
SET IDENTITY_INSERT [dbo].[Field] OFF
SET IDENTITY_INSERT [dbo].[FieldType] ON 

INSERT [dbo].[FieldType] ([ID], [Type]) VALUES (1, N'Textbox')
INSERT [dbo].[FieldType] ([ID], [Type]) VALUES (2, N'Checkbox')
INSERT [dbo].[FieldType] ([ID], [Type]) VALUES (3, N'Dropdown')
INSERT [dbo].[FieldType] ([ID], [Type]) VALUES (4, N'RadioButton')
SET IDENTITY_INSERT [dbo].[FieldType] OFF
ALTER TABLE [dbo].[DepartmentField]  WITH CHECK ADD  CONSTRAINT [FK_DepartmentField_Department] FOREIGN KEY([DepartmentID])
REFERENCES [dbo].[Department] ([ID])
GO
ALTER TABLE [dbo].[DepartmentField] CHECK CONSTRAINT [FK_DepartmentField_Department]
GO
ALTER TABLE [dbo].[DepartmentField]  WITH CHECK ADD  CONSTRAINT [FK_DepartmentField_Field] FOREIGN KEY([FieldID])
REFERENCES [dbo].[Field] ([ID])
GO
ALTER TABLE [dbo].[DepartmentField] CHECK CONSTRAINT [FK_DepartmentField_Field]
GO
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK_Employee_Department] FOREIGN KEY([DepartmentID])
REFERENCES [dbo].[Department] ([ID])
GO
ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK_Employee_Department]
GO
ALTER TABLE [dbo].[EmployeeData]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeData_Employee] FOREIGN KEY([EmployeeID])
REFERENCES [dbo].[Employee] ([ID])
GO
ALTER TABLE [dbo].[EmployeeData] CHECK CONSTRAINT [FK_EmployeeData_Employee]
GO
ALTER TABLE [dbo].[EmployeeData]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeData_Field] FOREIGN KEY([FieldID])
REFERENCES [dbo].[Field] ([ID])
GO
ALTER TABLE [dbo].[EmployeeData] CHECK CONSTRAINT [FK_EmployeeData_Field]
GO
ALTER TABLE [dbo].[Field]  WITH CHECK ADD  CONSTRAINT [FK_Field_Field] FOREIGN KEY([TypeID])
REFERENCES [dbo].[FieldType] ([ID])
GO
ALTER TABLE [dbo].[Field] CHECK CONSTRAINT [FK_Field_Field]
GO
USE [master]
GO
ALTER DATABASE [Configurely] SET  READ_WRITE 
GO
