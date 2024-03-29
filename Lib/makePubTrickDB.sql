USE [PubTricks_Dev]
GO
/****** Object:  ForeignKey [FK_Comments_Tricks]    Script Date: 10/25/2011 13:58:25 ******/
ALTER TABLE [dbo].[Comments] DROP CONSTRAINT [FK_Comments_Tricks]
GO
/****** Object:  ForeignKey [FK_TricksCategories_Categories]    Script Date: 10/25/2011 13:58:26 ******/
ALTER TABLE [dbo].[TricksCategories] DROP CONSTRAINT [FK_TricksCategories_Categories]
GO
/****** Object:  ForeignKey [FK_TricksCategories_Tricks]    Script Date: 10/25/2011 13:58:26 ******/
ALTER TABLE [dbo].[TricksCategories] DROP CONSTRAINT [FK_TricksCategories_Tricks]
GO
/****** Object:  Table [dbo].[Comments]    Script Date: 10/25/2011 13:58:25 ******/
ALTER TABLE [dbo].[Comments] DROP CONSTRAINT [FK_Comments_Tricks]
GO
DROP TABLE [dbo].[Comments]
GO
/****** Object:  Table [dbo].[TricksCategories]    Script Date: 10/25/2011 13:58:26 ******/
ALTER TABLE [dbo].[TricksCategories] DROP CONSTRAINT [FK_TricksCategories_Categories]
GO
ALTER TABLE [dbo].[TricksCategories] DROP CONSTRAINT [FK_TricksCategories_Tricks]
GO
DROP TABLE [dbo].[TricksCategories]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 10/25/2011 13:58:25 ******/
DROP TABLE [dbo].[Categories]
GO
/****** Object:  Table [dbo].[Tricks]    Script Date: 10/25/2011 13:58:26 ******/
ALTER TABLE [dbo].[Tricks] DROP CONSTRAINT [DF_Tricks_DateCreated]
GO
DROP TABLE [dbo].[Tricks]
GO
/****** Object:  Table [dbo].[Tricks]    Script Date: 10/25/2011 13:58:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tricks](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](255) NULL,
	[VideoURL] [nvarchar](255) NULL,
	[DateCreated] [datetime] NOT NULL CONSTRAINT [DF_Tricks_DateCreated]  DEFAULT (getdate()),
	[Votes] [int] NULL,
	[Thumbnail] [nvarchar](255) NULL,
	[LongDescription] [nvarchar](1024) NULL,
	[VideoSolutionURL] [nvarchar](255) NULL,
	[VideoFileName] [nvarchar](255) NULL,
	[VideoFileNameReveal] [nvarchar](255) NULL,
 CONSTRAINT [PK_Tricks] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Tricks] ON
INSERT [dbo].[Tricks] ([ID], [Name], [Description], [VideoURL], [DateCreated], [Votes], [Thumbnail], [LongDescription], [VideoSolutionURL], [VideoFileName], [VideoFileNameReveal]) VALUES (86, N'Uncross Your Arms', N'Uncross your arms description - very funny to watch', N'www.youtube.com/v/2_3BJq5srL4?version=3', CAST(0x00009F7500000000 AS DateTime), 7, N'UncrossArms-100x100.png', N'This is a good trick especially for kids or friends who have drunk more than 4 pints of beer (or 2 pints of cider.. never again...)', N'www.youtube.com/v/IyctbzxAA7U?version=3', N'UncrossArms.mp4', N'UncrossArmsReveal.mp4')
INSERT [dbo].[Tricks] ([ID], [Name], [Description], [VideoURL], [DateCreated], [Votes], [Thumbnail], [LongDescription], [VideoSolutionURL], [VideoFileName], [VideoFileNameReveal]) VALUES (90, N'Pen Trick', N'This is the pen trick description', N'www.youtube.com/v/OQXZXat-RPQ?version=3', CAST(0x00009F8100DEE13C AS DateTime), 12, N'PenTrickImage-100x100.png', N'The pen trick is one of my favourite all time tricks.. if there is one you remember try this one!
', N'www.youtube.com/v/ILEWo_-Fib8?version=3', N'PenTrick.mp4', N'PenTrickReveal.mp4')
INSERT [dbo].[Tricks] ([ID], [Name], [Description], [VideoURL], [DateCreated], [Votes], [Thumbnail], [LongDescription], [VideoSolutionURL], [VideoFileName], [VideoFileNameReveal]) VALUES (115, N'Beer trap', N'Beer trap description', N'www.youtube.com/v/NsXyrPN-eNo?version=3', CAST(0x00009F1C00000000 AS DateTime), 6, N'Beer-100x100.png', N'Best to do this one after your mates have had a lot to drink!', N'', N'Beer.mp4', NULL)
INSERT [dbo].[Tricks] ([ID], [Name], [Description], [VideoURL], [DateCreated], [Votes], [Thumbnail], [LongDescription], [VideoSolutionURL], [VideoFileName], [VideoFileNameReveal]) VALUES (116, N'Foot and Hand Circles', N'Foot and Hand circles description', N'www.youtube.com/v/TsGhmK8Zgtc?version=3', CAST(0x00009F7600000000 AS DateTime), 3, N'FootAndHand-100x100.png', N'Foot and hand circles requires some serious concentration!', N'', N'FootAndHand.mp4', NULL)
INSERT [dbo].[Tricks] ([ID], [Name], [Description], [VideoURL], [DateCreated], [Votes], [Thumbnail], [LongDescription], [VideoSolutionURL], [VideoFileName], [VideoFileNameReveal]) VALUES (117, N'Coin Trick', N'The amazing coin trick', N'www.youtube.com/v/-hnnpzBSnU8?version=3', CAST(0x00009F3B00000000 AS DateTime), 8, N'CoinTrick-100x100.png', N'The coin trick is a good one!', N'www.youtube.com/v/rlhAj5_i56I?version=3', N'CoinTrick.mp4', N'CoinTrickReveal.mp4')
SET IDENTITY_INSERT [dbo].[Tricks] OFF
/****** Object:  Table [dbo].[Categories]    Script Date: 10/25/2011 13:58:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Categories] ON
INSERT [dbo].[Categories] ([ID], [Name]) VALUES (1, N'Fun')
INSERT [dbo].[Categories] ([ID], [Name]) VALUES (2, N'Physical')
INSERT [dbo].[Categories] ([ID], [Name]) VALUES (3, N'Kids')
SET IDENTITY_INSERT [dbo].[Categories] OFF
/****** Object:  Table [dbo].[TricksCategories]    Script Date: 10/25/2011 13:58:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TricksCategories](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TrickID] [int] NOT NULL,
	[CategoryID] [int] NOT NULL,
 CONSTRAINT [PK_TricksCategories] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[TricksCategories] ON
INSERT [dbo].[TricksCategories] ([ID], [TrickID], [CategoryID]) VALUES (8, 86, 1)
INSERT [dbo].[TricksCategories] ([ID], [TrickID], [CategoryID]) VALUES (9, 90, 1)
INSERT [dbo].[TricksCategories] ([ID], [TrickID], [CategoryID]) VALUES (10, 117, 1)
SET IDENTITY_INSERT [dbo].[TricksCategories] OFF
/****** Object:  Table [dbo].[Comments]    Script Date: 10/25/2011 13:58:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comments](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TrickID] [int] NOT NULL,
	[CommentText] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_Comments] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Comments] ON
INSERT [dbo].[Comments] ([ID], [TrickID], [CommentText]) VALUES (164, 90, N'This is a great one')
INSERT [dbo].[Comments] ([ID], [TrickID], [CommentText]) VALUES (165, 90, N'I really like this')
INSERT [dbo].[Comments] ([ID], [TrickID], [CommentText]) VALUES (166, 117, N'Not bad - got kids into it!')
SET IDENTITY_INSERT [dbo].[Comments] OFF
/****** Object:  ForeignKey [FK_Comments_Tricks]    Script Date: 10/25/2011 13:58:25 ******/
ALTER TABLE [dbo].[Comments]  WITH CHECK ADD  CONSTRAINT [FK_Comments_Tricks] FOREIGN KEY([TrickID])
REFERENCES [dbo].[Tricks] ([ID])
GO
ALTER TABLE [dbo].[Comments] CHECK CONSTRAINT [FK_Comments_Tricks]
GO
/****** Object:  ForeignKey [FK_TricksCategories_Categories]    Script Date: 10/25/2011 13:58:26 ******/
ALTER TABLE [dbo].[TricksCategories]  WITH CHECK ADD  CONSTRAINT [FK_TricksCategories_Categories] FOREIGN KEY([CategoryID])
REFERENCES [dbo].[Categories] ([ID])
GO
ALTER TABLE [dbo].[TricksCategories] CHECK CONSTRAINT [FK_TricksCategories_Categories]
GO
/****** Object:  ForeignKey [FK_TricksCategories_Tricks]    Script Date: 10/25/2011 13:58:26 ******/
ALTER TABLE [dbo].[TricksCategories]  WITH CHECK ADD  CONSTRAINT [FK_TricksCategories_Tricks] FOREIGN KEY([TrickID])
REFERENCES [dbo].[Tricks] ([ID])
GO
ALTER TABLE [dbo].[TricksCategories] CHECK CONSTRAINT [FK_TricksCategories_Tricks]
GO
