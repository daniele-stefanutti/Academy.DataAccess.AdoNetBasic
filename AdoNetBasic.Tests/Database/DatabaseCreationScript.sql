USE [master]

IF EXISTS(select * from sys.databases where name='Airline')
DROP DATABASE [Airline]

CREATE DATABASE [Airline]

USE [Airline]

GO
/****** Object:  Table [dbo].[Airport]    Script Date: 4/15/2023 12:11:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Airport](
	[AirportCode] [char](3) NOT NULL,
	[AirportName] [varchar](50) NOT NULL,
	[ContactNo] [numeric](18, 0) NOT NULL,
	[Longitude] [float] NOT NULL,
	[Latitude] [float] NOT NULL,
	[CountryCode] [char](3) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[AirportCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Country]    Script Date: 4/15/2023 12:11:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Country](
	[CountryName] [varchar](50) NOT NULL,
	[CountryCode] [char](3) NOT NULL,
 CONSTRAINT [country_pk] PRIMARY KEY CLUSTERED 
(
	[CountryCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Flight]    Script Date: 4/15/2023 12:11:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Flight](
	[FlightNo] [varchar](10) NOT NULL,
	[FlightDepartTo] [char](3) NOT NULL,
	[FlightArriveFrom] [char](3) NOT NULL,
	[Distance] [int] NOT NULL,
 CONSTRAINT [FlightNo_pk] PRIMARY KEY CLUSTERED 
(
	[FlightNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FlightAttendant]    Script Date: 4/15/2023 12:11:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FlightAttendant](
	[AttendantID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](50) NOT NULL,
	[LastName] [varchar](50) NOT NULL,
	[DOB] [date] NOT NULL,
	[HireDate] [date] NOT NULL,
	[MentorID] [int] NULL,
 CONSTRAINT [attendantID_pk] PRIMARY KEY CLUSTERED 
(
	[AttendantID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FlightInstance]    Script Date: 4/15/2023 12:11:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FlightInstance](
	[InstanceID] [int] IDENTITY(1,1) NOT NULL,
	[FlightNo] [varchar](10) NOT NULL,
	[PlaneID] [int] NOT NULL,
	[PilotAboardID] [int] NOT NULL,
	[CoPilotAboardID] [int] NOT NULL,
	[FSM_AttendantID] [int] NOT NULL,
	[DateTimeLeave] [datetime] NOT NULL,
	[DateTimeArrive] [datetime] NOT NULL,
 CONSTRAINT [InstanceId_pk] PRIMARY KEY CLUSTERED 
(
	[InstanceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InstanceAttendant]    Script Date: 4/15/2023 12:11:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InstanceAttendant](
	[InstanceID] [int] NOT NULL,
	[AttendantID] [int] NOT NULL,
 CONSTRAINT [InstanceAttendantID_pk] PRIMARY KEY CLUSTERED 
(
	[InstanceID] ASC,
	[AttendantID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pilot]    Script Date: 4/15/2023 12:11:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pilot](
	[PilotID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](50) NOT NULL,
	[LastName] [varchar](50) NOT NULL,
	[DOB] [date] NOT NULL,
	[HoursFlown] [smallint] NOT NULL,
 CONSTRAINT [PilotId_pk] PRIMARY KEY CLUSTERED 
(
	[PilotID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PlaneDetail]    Script Date: 4/15/2023 12:11:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PlaneDetail](
	[PlaneID] [int] IDENTITY(1,1) NOT NULL,
	[ModelNumber] [varchar](10) NOT NULL,
	[RegistrationNo] [varchar](10) NOT NULL,
	[BuiltYear] [smallint] NOT NULL,
	[FirstClassCapacity] [smallint] NOT NULL,
	[EcoCapacity] [smallint] NOT NULL,
 CONSTRAINT [PlaneId_pk] PRIMARY KEY CLUSTERED 
(
	[PlaneID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PlaneModel]    Script Date: 4/15/2023 12:11:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PlaneModel](
	[ModelNumber] [varchar](10) NOT NULL,
	[ManufacturerName] [varchar](50) NOT NULL,
	[PlaneRange] [smallint] NOT NULL,
	[CruiseSpeed] [smallint] NOT NULL,
 CONSTRAINT [ModelN_pk] PRIMARY KEY CLUSTERED 
(
	[ModelNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PlanePilot]    Script Date: 4/15/2023 12:11:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PlanePilot](
	[PlaneModel] [varchar](10) NOT NULL,
	[PilotID] [int] NOT NULL,
 CONSTRAINT [planePilot_pk] PRIMARY KEY CLUSTERED 
(
	[PilotID] ASC,
	[PlaneModel] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Airport] ([AirportCode], [AirportName], [ContactNo], [Longitude], [Latitude], [CountryCode]) VALUES (N'ABC', N'Abc Airport', CAST(985485893 AS Numeric(18, 0)), 43.221, 44.54, N'ENG')
INSERT [dbo].[Airport] ([AirportCode], [AirportName], [ContactNo], [Longitude], [Latitude], [CountryCode]) VALUES (N'AKL', N'Auckland Airport', CAST(6492750789 AS Numeric(18, 0)), 37.0082, 174.785, N'NZL')
INSERT [dbo].[Airport] ([AirportCode], [AirportName], [ContactNo], [Longitude], [Latitude], [CountryCode]) VALUES (N'DXB', N'Dubai International Airport', CAST(6494544432 AS Numeric(18, 0)), 56.54, 33.33, N'UAE')
INSERT [dbo].[Airport] ([AirportCode], [AirportName], [ContactNo], [Longitude], [Latitude], [CountryCode]) VALUES (N'IJK', N'Ijk Airport', CAST(844428322 AS Numeric(18, 0)), 67.54, 54.32, N'POR')
INSERT [dbo].[Airport] ([AirportCode], [AirportName], [ContactNo], [Longitude], [Latitude], [CountryCode]) VALUES (N'MEL', N'Melbourne Airport', CAST(392971600 AS Numeric(18, 0)), 37.669, 144.841, N'AUS')
INSERT [dbo].[Airport] ([AirportCode], [AirportName], [ContactNo], [Longitude], [Latitude], [CountryCode]) VALUES (N'PER', N'Perth Airport', CAST(894788888 AS Numeric(18, 0)), 31.9385, 115.9672, N'AUS')
INSERT [dbo].[Airport] ([AirportCode], [AirportName], [ContactNo], [Longitude], [Latitude], [CountryCode]) VALUES (N'PKR', N'Pokhara Airport', CAST(97761465979 AS Numeric(18, 0)), 28.2, 83.9817, N'NPL')
INSERT [dbo].[Airport] ([AirportCode], [AirportName], [ContactNo], [Longitude], [Latitude], [CountryCode]) VALUES (N'SYD', N'Sydney Airport', CAST(296679111 AS Numeric(18, 0)), 33.9399, 151.1753, N'AUS')
INSERT [dbo].[Airport] ([AirportCode], [AirportName], [ContactNo], [Longitude], [Latitude], [CountryCode]) VALUES (N'TIA', N'Tribhuwan International Airport', CAST(97714113033 AS Numeric(18, 0)), 27.6981, 85.3592, N'NPL')
INSERT [dbo].[Airport] ([AirportCode], [AirportName], [ContactNo], [Longitude], [Latitude], [CountryCode]) VALUES (N'XYZ', N'Xyz Airport', CAST(542345433 AS Numeric(18, 0)), 34.43, 23.32, N'USA')
GO
INSERT [dbo].[Country] ([CountryName], [CountryCode]) VALUES (N'Australia', N'AUS')
INSERT [dbo].[Country] ([CountryName], [CountryCode]) VALUES (N'Austria', N'AUT')
INSERT [dbo].[Country] ([CountryName], [CountryCode]) VALUES (N'Belgium', N'BEL')
INSERT [dbo].[Country] ([CountryName], [CountryCode]) VALUES (N'Brazil', N'BRA')
INSERT [dbo].[Country] ([CountryName], [CountryCode]) VALUES (N'Canada', N'CAN')
INSERT [dbo].[Country] ([CountryName], [CountryCode]) VALUES (N'China', N'CHN')
INSERT [dbo].[Country] ([CountryName], [CountryCode]) VALUES (N'England', N'ENG')
INSERT [dbo].[Country] ([CountryName], [CountryCode]) VALUES (N'Germany', N'GER')
INSERT [dbo].[Country] ([CountryName], [CountryCode]) VALUES (N'Nepal', N'NPL')
INSERT [dbo].[Country] ([CountryName], [CountryCode]) VALUES (N'New Zealand', N'NZL')
INSERT [dbo].[Country] ([CountryName], [CountryCode]) VALUES (N'Portugal', N'POR')
INSERT [dbo].[Country] ([CountryName], [CountryCode]) VALUES (N'Spain', N'ESP')
INSERT [dbo].[Country] ([CountryName], [CountryCode]) VALUES (N'Sweden', N'SWE')
INSERT [dbo].[Country] ([CountryName], [CountryCode]) VALUES (N'United Arab Emirates', N'UAE')
INSERT [dbo].[Country] ([CountryName], [CountryCode]) VALUES (N'United States of America', N'USA')
GO
INSERT [dbo].[Flight] ([FlightNo], [FlightDepartTo], [FlightArriveFrom], [Distance]) VALUES (N'ABC123', N'TIA', N'SYD', 5500)
INSERT [dbo].[Flight] ([FlightNo], [FlightDepartTo], [FlightArriveFrom], [Distance]) VALUES (N'ABC987', N'PER', N'TIA', 8800)
INSERT [dbo].[Flight] ([FlightNo], [FlightDepartTo], [FlightArriveFrom], [Distance]) VALUES (N'JKL980', N'MEL', N'TIA', 9800)
INSERT [dbo].[Flight] ([FlightNo], [FlightDepartTo], [FlightArriveFrom], [Distance]) VALUES (N'PRA172', N'PER', N'DXB', 7400)
INSERT [dbo].[Flight] ([FlightNo], [FlightDepartTo], [FlightArriveFrom], [Distance]) VALUES (N'QR340', N'PKR', N'IJK', 3500)
INSERT [dbo].[Flight] ([FlightNo], [FlightDepartTo], [FlightArriveFrom], [Distance]) VALUES (N'STH650', N'PER', N'MEL', 5680)
INSERT [dbo].[Flight] ([FlightNo], [FlightDepartTo], [FlightArriveFrom], [Distance]) VALUES (N'XCF123', N'ABC', N'AKL', 7600)
INSERT [dbo].[Flight] ([FlightNo], [FlightDepartTo], [FlightArriveFrom], [Distance]) VALUES (N'XYP023', N'XYZ', N'PKR', 5800)
GO
SET IDENTITY_INSERT [dbo].[FlightAttendant] ON 

INSERT [dbo].[FlightAttendant] ([AttendantID], [FirstName], [LastName], [DOB], [HireDate], [MentorID]) VALUES (1, N'Pramesh', N'Shrestha', CAST(N'1989-04-25' AS Date), CAST(N'2004-04-25' AS Date), 6)
INSERT [dbo].[FlightAttendant] ([AttendantID], [FirstName], [LastName], [DOB], [HireDate], [MentorID]) VALUES (2, N'John', N'Rai', CAST(N'1979-04-22' AS Date), CAST(N'2005-04-25' AS Date), 3)
INSERT [dbo].[FlightAttendant] ([AttendantID], [FirstName], [LastName], [DOB], [HireDate], [MentorID]) VALUES (3, N'Mike', N'Magar', CAST(N'1990-04-22' AS Date), CAST(N'2010-04-25' AS Date), 5)
INSERT [dbo].[FlightAttendant] ([AttendantID], [FirstName], [LastName], [DOB], [HireDate], [MentorID]) VALUES (4, N'Hari', N'Cobin', CAST(N'1990-04-22' AS Date), CAST(N'2003-04-25' AS Date), 3)
INSERT [dbo].[FlightAttendant] ([AttendantID], [FirstName], [LastName], [DOB], [HireDate], [MentorID]) VALUES (5, N'Greg', N'Nepal', CAST(N'1991-04-22' AS Date), CAST(N'2002-04-25' AS Date), 4)
INSERT [dbo].[FlightAttendant] ([AttendantID], [FirstName], [LastName], [DOB], [HireDate], [MentorID]) VALUES (6, N'Ram', N'Sharma', CAST(N'1998-04-22' AS Date), CAST(N'2001-04-25' AS Date), 1)
INSERT [dbo].[FlightAttendant] ([AttendantID], [FirstName], [LastName], [DOB], [HireDate], [MentorID]) VALUES (7, N'Amol', N'Pokharel', CAST(N'1980-04-22' AS Date), CAST(N'2008-04-25' AS Date), 1)
INSERT [dbo].[FlightAttendant] ([AttendantID], [FirstName], [LastName], [DOB], [HireDate], [MentorID]) VALUES (8, N'Nishesh', N'Gajurel', CAST(N'1899-04-22' AS Date), CAST(N'2009-04-25' AS Date), 2)
INSERT [dbo].[FlightAttendant] ([AttendantID], [FirstName], [LastName], [DOB], [HireDate], [MentorID]) VALUES (9, N'Pratik', N'Shrestha', CAST(N'1999-04-22' AS Date), CAST(N'2003-04-25' AS Date), 7)
SET IDENTITY_INSERT [dbo].[FlightAttendant] OFF
GO
SET IDENTITY_INSERT [dbo].[FlightInstance] ON 

INSERT [dbo].[FlightInstance] ([InstanceID], [FlightNo], [PlaneID], [PilotAboardID], [CoPilotAboardID], [FSM_AttendantID], [DateTimeLeave], [DateTimeArrive]) VALUES (1, N'PRA172', 3, 1, 2, 5, CAST(N'2015-12-11T10:30:00.000' AS DateTime), CAST(N'2015-12-14T10:30:00.000' AS DateTime))
INSERT [dbo].[FlightInstance] ([InstanceID], [FlightNo], [PlaneID], [PilotAboardID], [CoPilotAboardID], [FSM_AttendantID], [DateTimeLeave], [DateTimeArrive]) VALUES (2, N'XCF123', 3, 2, 3, 1, CAST(N'2015-12-11T10:30:00.000' AS DateTime), CAST(N'2015-12-14T10:30:00.000' AS DateTime))
INSERT [dbo].[FlightInstance] ([InstanceID], [FlightNo], [PlaneID], [PilotAboardID], [CoPilotAboardID], [FSM_AttendantID], [DateTimeLeave], [DateTimeArrive]) VALUES (3, N'JKL980', 4, 3, 1, 2, CAST(N'2015-12-11T10:30:00.000' AS DateTime), CAST(N'2015-12-14T10:30:00.000' AS DateTime))
INSERT [dbo].[FlightInstance] ([InstanceID], [FlightNo], [PlaneID], [PilotAboardID], [CoPilotAboardID], [FSM_AttendantID], [DateTimeLeave], [DateTimeArrive]) VALUES (4, N'QR340', 4, 3, 1, 2, CAST(N'2015-12-10T10:30:00.000' AS DateTime), CAST(N'2015-12-11T10:30:00.000' AS DateTime))
INSERT [dbo].[FlightInstance] ([InstanceID], [FlightNo], [PlaneID], [PilotAboardID], [CoPilotAboardID], [FSM_AttendantID], [DateTimeLeave], [DateTimeArrive]) VALUES (5, N'QR340', 4, 3, 1, 2, CAST(N'2015-12-11T10:30:00.000' AS DateTime), CAST(N'2015-12-12T10:30:00.000' AS DateTime))
INSERT [dbo].[FlightInstance] ([InstanceID], [FlightNo], [PlaneID], [PilotAboardID], [CoPilotAboardID], [FSM_AttendantID], [DateTimeLeave], [DateTimeArrive]) VALUES (6, N'QR340', 4, 3, 1, 2, CAST(N'2015-12-12T10:30:00.000' AS DateTime), CAST(N'2015-12-13T10:30:00.000' AS DateTime))
INSERT [dbo].[FlightInstance] ([InstanceID], [FlightNo], [PlaneID], [PilotAboardID], [CoPilotAboardID], [FSM_AttendantID], [DateTimeLeave], [DateTimeArrive]) VALUES (7, N'PRA172', 3, 1, 2, 5, CAST(N'2015-12-11T10:30:00.000' AS DateTime), CAST(N'2015-12-14T10:30:00.000' AS DateTime))
INSERT [dbo].[FlightInstance] ([InstanceID], [FlightNo], [PlaneID], [PilotAboardID], [CoPilotAboardID], [FSM_AttendantID], [DateTimeLeave], [DateTimeArrive]) VALUES (8, N'ABC987', 2, 5, 3, 3, CAST(N'2015-12-11T10:30:00.000' AS DateTime), CAST(N'2015-12-14T10:30:00.000' AS DateTime))
INSERT [dbo].[FlightInstance] ([InstanceID], [FlightNo], [PlaneID], [PilotAboardID], [CoPilotAboardID], [FSM_AttendantID], [DateTimeLeave], [DateTimeArrive]) VALUES (9, N'ABC123', 3, 1, 2, 4, CAST(N'2015-12-11T10:30:00.000' AS DateTime), CAST(N'2015-12-14T10:30:00.000' AS DateTime))
INSERT [dbo].[FlightInstance] ([InstanceID], [FlightNo], [PlaneID], [PilotAboardID], [CoPilotAboardID], [FSM_AttendantID], [DateTimeLeave], [DateTimeArrive]) VALUES (10, N'JKL980', 4, 3, 1, 2, CAST(N'2015-12-11T10:30:00.000' AS DateTime), CAST(N'2015-12-14T10:30:00.000' AS DateTime))
INSERT [dbo].[FlightInstance] ([InstanceID], [FlightNo], [PlaneID], [PilotAboardID], [CoPilotAboardID], [FSM_AttendantID], [DateTimeLeave], [DateTimeArrive]) VALUES (11, N'STH650', 1, 3, 4, 5, CAST(N'2015-12-11T10:30:00.000' AS DateTime), CAST(N'2015-12-14T10:30:00.000' AS DateTime))
INSERT [dbo].[FlightInstance] ([InstanceID], [FlightNo], [PlaneID], [PilotAboardID], [CoPilotAboardID], [FSM_AttendantID], [DateTimeLeave], [DateTimeArrive]) VALUES (12, N'XCF123', 3, 2, 3, 1, CAST(N'2015-12-11T10:30:00.000' AS DateTime), CAST(N'2015-12-14T10:30:00.000' AS DateTime))
INSERT [dbo].[FlightInstance] ([InstanceID], [FlightNo], [PlaneID], [PilotAboardID], [CoPilotAboardID], [FSM_AttendantID], [DateTimeLeave], [DateTimeArrive]) VALUES (13, N'ABC123', 5, 4, 2, 4, CAST(N'2017-11-11T10:30:00.000' AS DateTime), CAST(N'2017-11-14T10:30:00.000' AS DateTime))
INSERT [dbo].[FlightInstance] ([InstanceID], [FlightNo], [PlaneID], [PilotAboardID], [CoPilotAboardID], [FSM_AttendantID], [DateTimeLeave], [DateTimeArrive]) VALUES (14, N'ABC123', 5, 4, 2, 4, CAST(N'2017-11-11T10:30:00.000' AS DateTime), CAST(N'2017-11-14T10:30:00.000' AS DateTime))
INSERT [dbo].[FlightInstance] ([InstanceID], [FlightNo], [PlaneID], [PilotAboardID], [CoPilotAboardID], [FSM_AttendantID], [DateTimeLeave], [DateTimeArrive]) VALUES (15, N'JKL980', 7, 3, 1, 2, CAST(N'2017-12-11T10:30:00.000' AS DateTime), CAST(N'2017-12-14T10:30:00.000' AS DateTime))
INSERT [dbo].[FlightInstance] ([InstanceID], [FlightNo], [PlaneID], [PilotAboardID], [CoPilotAboardID], [FSM_AttendantID], [DateTimeLeave], [DateTimeArrive]) VALUES (16, N'STH650', 1, 5, 4, 5, CAST(N'2017-12-11T10:30:00.000' AS DateTime), CAST(N'2017-12-14T10:30:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[FlightInstance] OFF
GO
INSERT [dbo].[InstanceAttendant] ([InstanceID], [AttendantID]) VALUES (1, 2)
INSERT [dbo].[InstanceAttendant] ([InstanceID], [AttendantID]) VALUES (1, 7)
INSERT [dbo].[InstanceAttendant] ([InstanceID], [AttendantID]) VALUES (2, 1)
INSERT [dbo].[InstanceAttendant] ([InstanceID], [AttendantID]) VALUES (2, 3)
INSERT [dbo].[InstanceAttendant] ([InstanceID], [AttendantID]) VALUES (2, 8)
INSERT [dbo].[InstanceAttendant] ([InstanceID], [AttendantID]) VALUES (3, 4)
INSERT [dbo].[InstanceAttendant] ([InstanceID], [AttendantID]) VALUES (3, 5)
INSERT [dbo].[InstanceAttendant] ([InstanceID], [AttendantID]) VALUES (4, 1)
INSERT [dbo].[InstanceAttendant] ([InstanceID], [AttendantID]) VALUES (5, 5)
INSERT [dbo].[InstanceAttendant] ([InstanceID], [AttendantID]) VALUES (6, 1)
INSERT [dbo].[InstanceAttendant] ([InstanceID], [AttendantID]) VALUES (7, 3)
INSERT [dbo].[InstanceAttendant] ([InstanceID], [AttendantID]) VALUES (7, 6)
INSERT [dbo].[InstanceAttendant] ([InstanceID], [AttendantID]) VALUES (8, 1)
INSERT [dbo].[InstanceAttendant] ([InstanceID], [AttendantID]) VALUES (8, 3)
INSERT [dbo].[InstanceAttendant] ([InstanceID], [AttendantID]) VALUES (9, 5)
INSERT [dbo].[InstanceAttendant] ([InstanceID], [AttendantID]) VALUES (10, 7)
INSERT [dbo].[InstanceAttendant] ([InstanceID], [AttendantID]) VALUES (10, 8)
INSERT [dbo].[InstanceAttendant] ([InstanceID], [AttendantID]) VALUES (10, 9)
INSERT [dbo].[InstanceAttendant] ([InstanceID], [AttendantID]) VALUES (11, 1)
INSERT [dbo].[InstanceAttendant] ([InstanceID], [AttendantID]) VALUES (11, 2)
INSERT [dbo].[InstanceAttendant] ([InstanceID], [AttendantID]) VALUES (11, 3)
INSERT [dbo].[InstanceAttendant] ([InstanceID], [AttendantID]) VALUES (12, 7)
INSERT [dbo].[InstanceAttendant] ([InstanceID], [AttendantID]) VALUES (13, 4)
INSERT [dbo].[InstanceAttendant] ([InstanceID], [AttendantID]) VALUES (14, 9)
INSERT [dbo].[InstanceAttendant] ([InstanceID], [AttendantID]) VALUES (15, 1)
INSERT [dbo].[InstanceAttendant] ([InstanceID], [AttendantID]) VALUES (15, 6)
INSERT [dbo].[InstanceAttendant] ([InstanceID], [AttendantID]) VALUES (15, 7)
INSERT [dbo].[InstanceAttendant] ([InstanceID], [AttendantID]) VALUES (16, 4)
INSERT [dbo].[InstanceAttendant] ([InstanceID], [AttendantID]) VALUES (16, 5)
INSERT [dbo].[InstanceAttendant] ([InstanceID], [AttendantID]) VALUES (16, 9)
GO
SET IDENTITY_INSERT [dbo].[Pilot] ON 

INSERT [dbo].[Pilot] ([PilotID], [FirstName], [LastName], [DOB], [HoursFlown]) VALUES (1, N'Pramesh', N'Shrestha', CAST(N'1989-04-25' AS Date), 5)
INSERT [dbo].[Pilot] ([PilotID], [FirstName], [LastName], [DOB], [HoursFlown]) VALUES (2, N'Maila', N'Battard', CAST(N'2002-11-15' AS Date), 10)
INSERT [dbo].[Pilot] ([PilotID], [FirstName], [LastName], [DOB], [HoursFlown]) VALUES (3, N'Tom', N'Hardy', CAST(N'1977-09-15' AS Date), 39)
INSERT [dbo].[Pilot] ([PilotID], [FirstName], [LastName], [DOB], [HoursFlown]) VALUES (4, N'Leonardo', N'DiCaprio', CAST(N'1974-11-11' AS Date), 85)
INSERT [dbo].[Pilot] ([PilotID], [FirstName], [LastName], [DOB], [HoursFlown]) VALUES (5, N'Huge', N'Glass', CAST(N'1980-08-22' AS Date), 9)
INSERT [dbo].[Pilot] ([PilotID], [FirstName], [LastName], [DOB], [HoursFlown]) VALUES (6, N'John', N'Fitzgerald', CAST(N'1979-09-18' AS Date), 77)
INSERT [dbo].[Pilot] ([PilotID], [FirstName], [LastName], [DOB], [HoursFlown]) VALUES (7, N'Jennifer', N'Whalen', CAST(N'1987-09-17' AS Date), 10)
INSERT [dbo].[Pilot] ([PilotID], [FirstName], [LastName], [DOB], [HoursFlown]) VALUES (8, N'Michael', N'Hartstein', CAST(N'1996-02-17' AS Date), 18)
INSERT [dbo].[Pilot] ([PilotID], [FirstName], [LastName], [DOB], [HoursFlown]) VALUES (9, N'Steven', N'King', CAST(N'1987-06-17' AS Date), 60)
SET IDENTITY_INSERT [dbo].[Pilot] OFF
GO
SET IDENTITY_INSERT [dbo].[PlaneDetail] ON 

INSERT [dbo].[PlaneDetail] ([PlaneID], [ModelNumber], [RegistrationNo], [BuiltYear], [FirstClassCapacity], [EcoCapacity]) VALUES (1, N'A390', N'AU-1989', 1989, 50, 50)
INSERT [dbo].[PlaneDetail] ([PlaneID], [ModelNumber], [RegistrationNo], [BuiltYear], [FirstClassCapacity], [EcoCapacity]) VALUES (2, N'A380', N'AU-2000', 2000, 100, 200)
INSERT [dbo].[PlaneDetail] ([PlaneID], [ModelNumber], [RegistrationNo], [BuiltYear], [FirstClassCapacity], [EcoCapacity]) VALUES (3, N'A300', N'AU-1970', 1970, 200, 350)
INSERT [dbo].[PlaneDetail] ([PlaneID], [ModelNumber], [RegistrationNo], [BuiltYear], [FirstClassCapacity], [EcoCapacity]) VALUES (4, N'A340', N'AU-1880', 1880, 310, 420)
INSERT [dbo].[PlaneDetail] ([PlaneID], [ModelNumber], [RegistrationNo], [BuiltYear], [FirstClassCapacity], [EcoCapacity]) VALUES (5, N'A390', N'AU-1990', 1990, 110, 230)
INSERT [dbo].[PlaneDetail] ([PlaneID], [ModelNumber], [RegistrationNo], [BuiltYear], [FirstClassCapacity], [EcoCapacity]) VALUES (6, N'737', N'BO-2001', 2001, 40, 120)
INSERT [dbo].[PlaneDetail] ([PlaneID], [ModelNumber], [RegistrationNo], [BuiltYear], [FirstClassCapacity], [EcoCapacity]) VALUES (7, N'777', N'BO-1990', 1990, 155, 450)
INSERT [dbo].[PlaneDetail] ([PlaneID], [ModelNumber], [RegistrationNo], [BuiltYear], [FirstClassCapacity], [EcoCapacity]) VALUES (8, N'779', N'BO-2002', 2002, 121, 244)
INSERT [dbo].[PlaneDetail] ([PlaneID], [ModelNumber], [RegistrationNo], [BuiltYear], [FirstClassCapacity], [EcoCapacity]) VALUES (9, N'787', N'BO-2005', 2005, 195, 340)
INSERT [dbo].[PlaneDetail] ([PlaneID], [ModelNumber], [RegistrationNo], [BuiltYear], [FirstClassCapacity], [EcoCapacity]) VALUES (10, N'787', N'BO-2005-1', 2005, 95, 140)
SET IDENTITY_INSERT [dbo].[PlaneDetail] OFF
GO
INSERT [dbo].[PlaneModel] ([ModelNumber], [ManufacturerName], [PlaneRange], [CruiseSpeed]) VALUES (N'737', N'Boeing', 5600, 780)
INSERT [dbo].[PlaneModel] ([ModelNumber], [ManufacturerName], [PlaneRange], [CruiseSpeed]) VALUES (N'777', N'Boeing', 10000, 892)
INSERT [dbo].[PlaneModel] ([ModelNumber], [ManufacturerName], [PlaneRange], [CruiseSpeed]) VALUES (N'779', N'Boeing', 17000, 922)
INSERT [dbo].[PlaneModel] ([ModelNumber], [ManufacturerName], [PlaneRange], [CruiseSpeed]) VALUES (N'787', N'Boeing', 15000, 903)
INSERT [dbo].[PlaneModel] ([ModelNumber], [ManufacturerName], [PlaneRange], [CruiseSpeed]) VALUES (N'A300', N'Airbus', 13450, 871)
INSERT [dbo].[PlaneModel] ([ModelNumber], [ManufacturerName], [PlaneRange], [CruiseSpeed]) VALUES (N'A340', N'Airbus', 12400, 881)
INSERT [dbo].[PlaneModel] ([ModelNumber], [ManufacturerName], [PlaneRange], [CruiseSpeed]) VALUES (N'A380', N'Airbus', 15700, 900)
INSERT [dbo].[PlaneModel] ([ModelNumber], [ManufacturerName], [PlaneRange], [CruiseSpeed]) VALUES (N'A390', N'Airbus', 17400, 1081)
GO
INSERT [dbo].[PlanePilot] ([PlaneModel], [PilotID]) VALUES (N'737', 1)
INSERT [dbo].[PlanePilot] ([PlaneModel], [PilotID]) VALUES (N'777', 1)
INSERT [dbo].[PlanePilot] ([PlaneModel], [PilotID]) VALUES (N'A340', 1)
INSERT [dbo].[PlanePilot] ([PlaneModel], [PilotID]) VALUES (N'A340', 2)
INSERT [dbo].[PlanePilot] ([PlaneModel], [PilotID]) VALUES (N'A390', 2)
INSERT [dbo].[PlanePilot] ([PlaneModel], [PilotID]) VALUES (N'777', 3)
INSERT [dbo].[PlanePilot] ([PlaneModel], [PilotID]) VALUES (N'A340', 3)
INSERT [dbo].[PlanePilot] ([PlaneModel], [PilotID]) VALUES (N'A380', 3)
INSERT [dbo].[PlanePilot] ([PlaneModel], [PilotID]) VALUES (N'779', 4)
INSERT [dbo].[PlanePilot] ([PlaneModel], [PilotID]) VALUES (N'A340', 4)
INSERT [dbo].[PlanePilot] ([PlaneModel], [PilotID]) VALUES (N'787', 5)
INSERT [dbo].[PlanePilot] ([PlaneModel], [PilotID]) VALUES (N'787', 6)
INSERT [dbo].[PlanePilot] ([PlaneModel], [PilotID]) VALUES (N'A340', 8)
INSERT [dbo].[PlanePilot] ([PlaneModel], [PilotID]) VALUES (N'A340', 9)
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [Country_name_uk]    Script Date: 4/15/2023 12:11:13 PM ******/
ALTER TABLE [dbo].[Country] ADD  CONSTRAINT [Country_name_uk] UNIQUE NONCLUSTERED 
(
	[CountryName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [RegNO_uk]    Script Date: 4/15/2023 12:11:13 PM ******/
ALTER TABLE [dbo].[PlaneDetail] ADD  CONSTRAINT [RegNO_uk] UNIQUE NONCLUSTERED 
(
	[RegistrationNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Airport]  WITH CHECK ADD  CONSTRAINT [CountryCode_fk] FOREIGN KEY([CountryCode])
REFERENCES [dbo].[Country] ([CountryCode])
GO
ALTER TABLE [dbo].[Airport] CHECK CONSTRAINT [CountryCode_fk]
GO
ALTER TABLE [dbo].[Flight]  WITH CHECK ADD  CONSTRAINT [FLightArriceFrom_fk] FOREIGN KEY([FlightArriveFrom])
REFERENCES [dbo].[Airport] ([AirportCode])
GO
ALTER TABLE [dbo].[Flight] CHECK CONSTRAINT [FLightArriceFrom_fk]
GO
ALTER TABLE [dbo].[Flight]  WITH CHECK ADD  CONSTRAINT [FLightDepartTo_fk] FOREIGN KEY([FlightDepartTo])
REFERENCES [dbo].[Airport] ([AirportCode])
GO
ALTER TABLE [dbo].[Flight] CHECK CONSTRAINT [FLightDepartTo_fk]
GO
ALTER TABLE [dbo].[FlightAttendant]  WITH CHECK ADD  CONSTRAINT [mentorID_fk] FOREIGN KEY([MentorID])
REFERENCES [dbo].[FlightAttendant] ([AttendantID])
GO
ALTER TABLE [dbo].[FlightAttendant] CHECK CONSTRAINT [mentorID_fk]
GO
ALTER TABLE [dbo].[FlightInstance]  WITH CHECK ADD  CONSTRAINT [CoPilotAboardId_fk] FOREIGN KEY([CoPilotAboardID])
REFERENCES [dbo].[Pilot] ([PilotID])
GO
ALTER TABLE [dbo].[FlightInstance] CHECK CONSTRAINT [CoPilotAboardId_fk]
GO
ALTER TABLE [dbo].[FlightInstance]  WITH CHECK ADD  CONSTRAINT [FlightNo_fk] FOREIGN KEY([FlightNo])
REFERENCES [dbo].[Flight] ([FlightNo])
GO
ALTER TABLE [dbo].[FlightInstance] CHECK CONSTRAINT [FlightNo_fk]
GO
ALTER TABLE [dbo].[FlightInstance]  WITH CHECK ADD  CONSTRAINT [FSM_AttendantID] FOREIGN KEY([FSM_AttendantID])
REFERENCES [dbo].[FlightAttendant] ([AttendantID])
GO
ALTER TABLE [dbo].[FlightInstance] CHECK CONSTRAINT [FSM_AttendantID]
GO
ALTER TABLE [dbo].[FlightInstance]  WITH CHECK ADD  CONSTRAINT [PilotAboardId_fk] FOREIGN KEY([PilotAboardID])
REFERENCES [dbo].[Pilot] ([PilotID])
GO
ALTER TABLE [dbo].[FlightInstance] CHECK CONSTRAINT [PilotAboardId_fk]
GO
ALTER TABLE [dbo].[FlightInstance]  WITH CHECK ADD  CONSTRAINT [PlaneID_fk] FOREIGN KEY([PlaneID])
REFERENCES [dbo].[PlaneDetail] ([PlaneID])
GO
ALTER TABLE [dbo].[FlightInstance] CHECK CONSTRAINT [PlaneID_fk]
GO
ALTER TABLE [dbo].[InstanceAttendant]  WITH CHECK ADD  CONSTRAINT [AttendantId_fk] FOREIGN KEY([AttendantID])
REFERENCES [dbo].[FlightAttendant] ([AttendantID])
GO
ALTER TABLE [dbo].[InstanceAttendant] CHECK CONSTRAINT [AttendantId_fk]
GO
ALTER TABLE [dbo].[InstanceAttendant]  WITH CHECK ADD  CONSTRAINT [InstanceId_fk] FOREIGN KEY([InstanceID])
REFERENCES [dbo].[FlightInstance] ([InstanceID])
GO
ALTER TABLE [dbo].[InstanceAttendant] CHECK CONSTRAINT [InstanceId_fk]
GO
ALTER TABLE [dbo].[PlaneDetail]  WITH CHECK ADD  CONSTRAINT [ModelN_fk] FOREIGN KEY([ModelNumber])
REFERENCES [dbo].[PlaneModel] ([ModelNumber])
GO
ALTER TABLE [dbo].[PlaneDetail] CHECK CONSTRAINT [ModelN_fk]
GO
ALTER TABLE [dbo].[PlanePilot]  WITH CHECK ADD  CONSTRAINT [PilotID_fk] FOREIGN KEY([PilotID])
REFERENCES [dbo].[Pilot] ([PilotID])
GO
ALTER TABLE [dbo].[PlanePilot] CHECK CONSTRAINT [PilotID_fk]
GO
ALTER TABLE [dbo].[PlanePilot]  WITH CHECK ADD  CONSTRAINT [PlaneModel_fk] FOREIGN KEY([PlaneModel])
REFERENCES [dbo].[PlaneModel] ([ModelNumber])
GO
ALTER TABLE [dbo].[PlanePilot] CHECK CONSTRAINT [PlaneModel_fk]
GO
ALTER TABLE [dbo].[Flight]  WITH CHECK ADD  CONSTRAINT [FlightArriveFrom] CHECK  (([FlightArriveFrom]<>[FlightDepartTo]))
GO
ALTER TABLE [dbo].[Flight] CHECK CONSTRAINT [FlightArriveFrom]
GO
ALTER TABLE [dbo].[FlightInstance]  WITH CHECK ADD  CONSTRAINT [CoPilodAboardId_fk] CHECK  (([CoPilotAboardId]<>[PilotAboardID]))
GO
ALTER TABLE [dbo].[FlightInstance] CHECK CONSTRAINT [CoPilodAboardId_fk]
GO
ALTER TABLE [dbo].[FlightInstance]  WITH CHECK ADD  CONSTRAINT [DateTimeArrive_ck] CHECK  (([DateTimeArrive]>[DateTimeLeave]))
GO
ALTER TABLE [dbo].[FlightInstance] CHECK CONSTRAINT [DateTimeArrive_ck]
GO
