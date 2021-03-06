USE [hair_salon_test]
GO
/****** Object:  Table [dbo].[clients]    Script Date: 2/24/2017 5:14:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[clients](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL,
	[request] [varchar](255) NULL,
	[date] [datetime] NULL,
	[stylist_id] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[feedbacks]    Script Date: 2/24/2017 5:14:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[feedbacks](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[preference] [varchar](255) NULL,
	[client_id] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[stylists]    Script Date: 2/24/2017 5:14:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[stylists](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[clients] ON 

INSERT [dbo].[clients] ([id], [name], [request], [date], [stylist_id]) VALUES (14, N'Wendy', N'curly', CAST(N'2017-06-04T00:00:00.000' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[clients] OFF
