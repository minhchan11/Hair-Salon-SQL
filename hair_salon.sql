USE [hair_salon]
GO
/****** Object:  Table [dbo].[clients]    Script Date: 2/24/2017 5:13:33 PM ******/
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
/****** Object:  Table [dbo].[feedbacks]    Script Date: 2/24/2017 5:13:33 PM ******/
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
/****** Object:  Table [dbo].[stylists]    Script Date: 2/24/2017 5:13:33 PM ******/
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

INSERT [dbo].[clients] ([id], [name], [request], [date], [stylist_id]) VALUES (3, N'Tammy', N'Hair', CAST(N'2017-02-15T00:00:00.000' AS DateTime), 2)
INSERT [dbo].[clients] ([id], [name], [request], [date], [stylist_id]) VALUES (4, N'Katy Perry', N'Make me fab', CAST(N'2017-02-28T00:00:00.000' AS DateTime), 2)
SET IDENTITY_INSERT [dbo].[clients] OFF
SET IDENTITY_INSERT [dbo].[feedbacks] ON 

INSERT [dbo].[feedbacks] ([id], [preference], [client_id]) VALUES (1, N'fdafasd', 2)
INSERT [dbo].[feedbacks] ([id], [preference], [client_id]) VALUES (2, N'fdasfas', 2)
INSERT [dbo].[feedbacks] ([id], [preference], [client_id]) VALUES (3, N'fsdfas', 1)
INSERT [dbo].[feedbacks] ([id], [preference], [client_id]) VALUES (4, N'fasdf', 1)
INSERT [dbo].[feedbacks] ([id], [preference], [client_id]) VALUES (5, N'fsdfa', 1)
INSERT [dbo].[feedbacks] ([id], [preference], [client_id]) VALUES (6, N'Feel really good', 3)
INSERT [dbo].[feedbacks] ([id], [preference], [client_id]) VALUES (7, N'Feel really good', 3)
INSERT [dbo].[feedbacks] ([id], [preference], [client_id]) VALUES (8, N'"So So..Not Too Good"', 4)
SET IDENTITY_INSERT [dbo].[feedbacks] OFF
SET IDENTITY_INSERT [dbo].[stylists] ON 

INSERT [dbo].[stylists] ([id], [name]) VALUES (1, N'John')
INSERT [dbo].[stylists] ([id], [name]) VALUES (2, N'Kimberly Kardashian')
SET IDENTITY_INSERT [dbo].[stylists] OFF
