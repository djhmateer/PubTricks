
USE [PubTricks_Dev]
GO
/****** Object:  ForeignKey [RoleApplication]    Script Date: 10/26/2011 09:23:10 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[RoleApplication]') AND parent_object_id = OBJECT_ID(N'[dbo].[Roles]'))
ALTER TABLE [dbo].[Roles] DROP CONSTRAINT [RoleApplication]
GO
/****** Object:  ForeignKey [UserApplication]    Script Date: 10/26/2011 09:23:10 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[UserApplication]') AND parent_object_id = OBJECT_ID(N'[dbo].[Users]'))
ALTER TABLE [dbo].[Users] DROP CONSTRAINT [UserApplication]
GO
/****** Object:  ForeignKey [FK_TricksCategories_Categories]    Script Date: 10/26/2011 09:23:10 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TricksCategories_Categories]') AND parent_object_id = OBJECT_ID(N'[dbo].[TricksCategories]'))
ALTER TABLE [dbo].[TricksCategories] DROP CONSTRAINT [FK_TricksCategories_Categories]
GO
/****** Object:  ForeignKey [FK_TricksCategories_Tricks]    Script Date: 10/26/2011 09:23:10 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TricksCategories_Tricks]') AND parent_object_id = OBJECT_ID(N'[dbo].[TricksCategories]'))
ALTER TABLE [dbo].[TricksCategories] DROP CONSTRAINT [FK_TricksCategories_Tricks]
GO
/****** Object:  ForeignKey [FK_Comments_Tricks]    Script Date: 10/26/2011 09:23:11 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Comments_Tricks]') AND parent_object_id = OBJECT_ID(N'[dbo].[Comments]'))
ALTER TABLE [dbo].[Comments] DROP CONSTRAINT [FK_Comments_Tricks]
GO
/****** Object:  ForeignKey [UsersInRoleRole]    Script Date: 10/26/2011 09:23:11 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[UsersInRoleRole]') AND parent_object_id = OBJECT_ID(N'[dbo].[UsersInRoles]'))
ALTER TABLE [dbo].[UsersInRoles] DROP CONSTRAINT [UsersInRoleRole]
GO
/****** Object:  ForeignKey [UsersInRoleUser]    Script Date: 10/26/2011 09:23:11 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[UsersInRoleUser]') AND parent_object_id = OBJECT_ID(N'[dbo].[UsersInRoles]'))
ALTER TABLE [dbo].[UsersInRoles] DROP CONSTRAINT [UsersInRoleUser]
GO
/****** Object:  ForeignKey [UserProfile]    Script Date: 10/26/2011 09:23:11 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[UserProfile]') AND parent_object_id = OBJECT_ID(N'[dbo].[Profiles]'))
ALTER TABLE [dbo].[Profiles] DROP CONSTRAINT [UserProfile]
GO
/****** Object:  ForeignKey [MembershipApplication]    Script Date: 10/26/2011 09:23:11 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[MembershipApplication]') AND parent_object_id = OBJECT_ID(N'[dbo].[Memberships]'))
ALTER TABLE [dbo].[Memberships] DROP CONSTRAINT [MembershipApplication]
GO
/****** Object:  ForeignKey [MembershipUser]    Script Date: 10/26/2011 09:23:11 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[MembershipUser]') AND parent_object_id = OBJECT_ID(N'[dbo].[Memberships]'))
ALTER TABLE [dbo].[Memberships] DROP CONSTRAINT [MembershipUser]
GO
/****** Object:  Table [dbo].[Memberships]    Script Date: 10/26/2011 09:23:11 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[MembershipApplication]') AND parent_object_id = OBJECT_ID(N'[dbo].[Memberships]'))
ALTER TABLE [dbo].[Memberships] DROP CONSTRAINT [MembershipApplication]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[MembershipUser]') AND parent_object_id = OBJECT_ID(N'[dbo].[Memberships]'))
ALTER TABLE [dbo].[Memberships] DROP CONSTRAINT [MembershipUser]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Memberships]') AND type in (N'U'))
DROP TABLE [dbo].[Memberships]
GO
/****** Object:  Table [dbo].[Profiles]    Script Date: 10/26/2011 09:23:11 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[UserProfile]') AND parent_object_id = OBJECT_ID(N'[dbo].[Profiles]'))
ALTER TABLE [dbo].[Profiles] DROP CONSTRAINT [UserProfile]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Profiles]') AND type in (N'U'))
DROP TABLE [dbo].[Profiles]
GO
/****** Object:  Table [dbo].[UsersInRoles]    Script Date: 10/26/2011 09:23:11 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[UsersInRoleRole]') AND parent_object_id = OBJECT_ID(N'[dbo].[UsersInRoles]'))
ALTER TABLE [dbo].[UsersInRoles] DROP CONSTRAINT [UsersInRoleRole]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[UsersInRoleUser]') AND parent_object_id = OBJECT_ID(N'[dbo].[UsersInRoles]'))
ALTER TABLE [dbo].[UsersInRoles] DROP CONSTRAINT [UsersInRoleUser]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UsersInRoles]') AND type in (N'U'))
DROP TABLE [dbo].[UsersInRoles]
GO
/****** Object:  Table [dbo].[Comments]    Script Date: 10/26/2011 09:23:11 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Comments_Tricks]') AND parent_object_id = OBJECT_ID(N'[dbo].[Comments]'))
ALTER TABLE [dbo].[Comments] DROP CONSTRAINT [FK_Comments_Tricks]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Comments]') AND type in (N'U'))
DROP TABLE [dbo].[Comments]
GO
/****** Object:  Table [dbo].[TricksCategories]    Script Date: 10/26/2011 09:23:10 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TricksCategories_Categories]') AND parent_object_id = OBJECT_ID(N'[dbo].[TricksCategories]'))
ALTER TABLE [dbo].[TricksCategories] DROP CONSTRAINT [FK_TricksCategories_Categories]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TricksCategories_Tricks]') AND parent_object_id = OBJECT_ID(N'[dbo].[TricksCategories]'))
ALTER TABLE [dbo].[TricksCategories] DROP CONSTRAINT [FK_TricksCategories_Tricks]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TricksCategories]') AND type in (N'U'))
DROP TABLE [dbo].[TricksCategories]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 10/26/2011 09:23:10 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[UserApplication]') AND parent_object_id = OBJECT_ID(N'[dbo].[Users]'))
ALTER TABLE [dbo].[Users] DROP CONSTRAINT [UserApplication]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Users]') AND type in (N'U'))
DROP TABLE [dbo].[Users]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 10/26/2011 09:23:10 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[RoleApplication]') AND parent_object_id = OBJECT_ID(N'[dbo].[Roles]'))
ALTER TABLE [dbo].[Roles] DROP CONSTRAINT [RoleApplication]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Roles]') AND type in (N'U'))
DROP TABLE [dbo].[Roles]
GO
/****** Object:  Table [dbo].[Tricks]    Script Date: 10/26/2011 09:23:10 ******/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Tricks_DateCreated]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Tricks] DROP CONSTRAINT [DF_Tricks_DateCreated]
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tricks]') AND type in (N'U'))
DROP TABLE [dbo].[Tricks]
GO
/****** Object:  Table [dbo].[Applications]    Script Date: 10/26/2011 09:23:10 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Applications]') AND type in (N'U'))
DROP TABLE [dbo].[Applications]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 10/26/2011 09:23:10 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Categories]') AND type in (N'U'))
DROP TABLE [dbo].[Categories]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 10/26/2011 09:23:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Categories]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Categories](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET IDENTITY_INSERT [dbo].[Categories] ON
INSERT [dbo].[Categories] ([ID], [Name]) VALUES (1, N'Fun')
INSERT [dbo].[Categories] ([ID], [Name]) VALUES (2, N'Physical')
INSERT [dbo].[Categories] ([ID], [Name]) VALUES (3, N'Kids')
SET IDENTITY_INSERT [dbo].[Categories] OFF
/****** Object:  Table [dbo].[Applications]    Script Date: 10/26/2011 09:23:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Applications]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Applications](
	[ApplicationName] [nvarchar](235) NOT NULL,
	[ApplicationId] [uniqueidentifier] NOT NULL,
	[Description] [nvarchar](256) NULL,
PRIMARY KEY CLUSTERED 
(
	[ApplicationId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
INSERT [dbo].[Applications] ([ApplicationName], [ApplicationId], [Description]) VALUES (N'/', N'eee01e5b-119b-4ea7-aefe-8519b33faa5b', NULL)
/****** Object:  Table [dbo].[Tricks]    Script Date: 10/26/2011 09:23:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tricks]') AND type in (N'U'))
BEGIN
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
END
GO
SET IDENTITY_INSERT [dbo].[Tricks] ON
INSERT [dbo].[Tricks] ([ID], [Name], [Description], [VideoURL], [DateCreated], [Votes], [Thumbnail], [LongDescription], [VideoSolutionURL], [VideoFileName], [VideoFileNameReveal]) VALUES (86, N'Uncross Your Arms', N'Uncross your arms description - very funny to watch', N'www.youtube.com/v/2_3BJq5srL4?version=3', CAST(0x00009F7500000000 AS DateTime), 7, N'UncrossArms-100x100.png', N'This is a good trick especially for kids or friends who have drunk more than 4 pints of beer (or 2 pints of cider.. never again...)', N'www.youtube.com/v/IyctbzxAA7U?version=3', N'UncrossArms.mp4', N'UncrossArmsReveal.mp4')
INSERT [dbo].[Tricks] ([ID], [Name], [Description], [VideoURL], [DateCreated], [Votes], [Thumbnail], [LongDescription], [VideoSolutionURL], [VideoFileName], [VideoFileNameReveal]) VALUES (90, N'Pen Trick', N'This is the pen trick description', N'www.youtube.com/v/OQXZXat-RPQ?version=3', CAST(0x00009F8100DEE13C AS DateTime), 12, N'PenTrickImage-100x100.png', N'The pen trick is one of my favourite all time tricks.. if there is one you remember try this one!
', N'www.youtube.com/v/ILEWo_-Fib8?version=3', N'PenTrick.mp4', N'PenTrickReveal.mp4')
INSERT [dbo].[Tricks] ([ID], [Name], [Description], [VideoURL], [DateCreated], [Votes], [Thumbnail], [LongDescription], [VideoSolutionURL], [VideoFileName], [VideoFileNameReveal]) VALUES (115, N'Beer trap', N'Beer trap description', N'www.youtube.com/v/NsXyrPN-eNo?version=3', CAST(0x00009F1C00000000 AS DateTime), 6, N'Beer-100x100.png', N'Best to do this one after your mates have had a lot to drink!', N'', N'Beer.mp4', NULL)
INSERT [dbo].[Tricks] ([ID], [Name], [Description], [VideoURL], [DateCreated], [Votes], [Thumbnail], [LongDescription], [VideoSolutionURL], [VideoFileName], [VideoFileNameReveal]) VALUES (116, N'Foot and Hand Circles', N'Foot and Hand circles description', N'www.youtube.com/v/TsGhmK8Zgtc?version=3', CAST(0x00009F8800935F31 AS DateTime), 4, N'FootAndHand-100x100.png', N'Foot and hand circles requires some serious concentration!', N'', N'FootAndHand.mp4', NULL)
INSERT [dbo].[Tricks] ([ID], [Name], [Description], [VideoURL], [DateCreated], [Votes], [Thumbnail], [LongDescription], [VideoSolutionURL], [VideoFileName], [VideoFileNameReveal]) VALUES (117, N'Coin Trick', N'The amazing coin trick', N'www.youtube.com/v/-hnnpzBSnU8?version=3', CAST(0x00009F3B00000000 AS DateTime), 8, N'CoinTrick-100x100.png', N'The coin trick is a good one!', N'www.youtube.com/v/rlhAj5_i56I?version=3', N'CoinTrick.mp4', N'CoinTrickReveal.mp4')
SET IDENTITY_INSERT [dbo].[Tricks] OFF
/****** Object:  Table [dbo].[Roles]    Script Date: 10/26/2011 09:23:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Roles]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Roles](
	[ApplicationId] [uniqueidentifier] NOT NULL,
	[RoleId] [uniqueidentifier] NOT NULL,
	[RoleName] [nvarchar](256) NOT NULL,
	[Description] [nvarchar](256) NULL,
PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
INSERT [dbo].[Roles] ([ApplicationId], [RoleId], [RoleName], [Description]) VALUES (N'eee01e5b-119b-4ea7-aefe-8519b33faa5b', N'296e9fca-491f-4e49-8245-c4d36a7d3046', N'Administrator', NULL)
/****** Object:  Table [dbo].[Users]    Script Date: 10/26/2011 09:23:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Users]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Users](
	[ApplicationId] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[IsAnonymous] [bit] NOT NULL,
	[LastActivityDate] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
INSERT [dbo].[Users] ([ApplicationId], [UserId], [UserName], [IsAnonymous], [LastActivityDate]) VALUES (N'eee01e5b-119b-4ea7-aefe-8519b33faa5b', N'711a11d9-5428-42ac-90d3-931afaf9e6ab', N'dave5', 0, CAST(0x00009F8001721296 AS DateTime))
INSERT [dbo].[Users] ([ApplicationId], [UserId], [UserName], [IsAnonymous], [LastActivityDate]) VALUES (N'eee01e5b-119b-4ea7-aefe-8519b33faa5b', N'a39999fa-db5a-49f2-a391-9ca812346f5e', N'dave2', 0, CAST(0x00009F870148BFD5 AS DateTime))
/****** Object:  Table [dbo].[TricksCategories]    Script Date: 10/26/2011 09:23:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TricksCategories]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[TricksCategories](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TrickID] [int] NOT NULL,
	[CategoryID] [int] NOT NULL,
 CONSTRAINT [PK_TricksCategories] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET IDENTITY_INSERT [dbo].[TricksCategories] ON
INSERT [dbo].[TricksCategories] ([ID], [TrickID], [CategoryID]) VALUES (8, 86, 1)
INSERT [dbo].[TricksCategories] ([ID], [TrickID], [CategoryID]) VALUES (9, 90, 1)
INSERT [dbo].[TricksCategories] ([ID], [TrickID], [CategoryID]) VALUES (10, 117, 1)
SET IDENTITY_INSERT [dbo].[TricksCategories] OFF
/****** Object:  Table [dbo].[Comments]    Script Date: 10/26/2011 09:23:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Comments]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Comments](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TrickID] [int] NOT NULL,
	[CommentText] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_Comments] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET IDENTITY_INSERT [dbo].[Comments] ON
INSERT [dbo].[Comments] ([ID], [TrickID], [CommentText]) VALUES (164, 90, N'This is a great one')
INSERT [dbo].[Comments] ([ID], [TrickID], [CommentText]) VALUES (165, 90, N'I really like this')
INSERT [dbo].[Comments] ([ID], [TrickID], [CommentText]) VALUES (166, 117, N'Not bad - got kids into it!')
SET IDENTITY_INSERT [dbo].[Comments] OFF
/****** Object:  Table [dbo].[UsersInRoles]    Script Date: 10/26/2011 09:23:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UsersInRoles]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[UsersInRoles](
	[UserId] [uniqueidentifier] NOT NULL,
	[RoleId] [uniqueidentifier] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
INSERT [dbo].[UsersInRoles] ([UserId], [RoleId]) VALUES (N'a39999fa-db5a-49f2-a391-9ca812346f5e', N'296e9fca-491f-4e49-8245-c4d36a7d3046')
/****** Object:  Table [dbo].[Profiles]    Script Date: 10/26/2011 09:23:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Profiles]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Profiles](
	[UserId] [uniqueidentifier] NOT NULL,
	[PropertyNames] [nvarchar](4000) NOT NULL,
	[PropertyValueStrings] [nvarchar](4000) NOT NULL,
	[PropertyValueBinary] [image] NOT NULL,
	[LastUpdatedDate] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Memberships]    Script Date: 10/26/2011 09:23:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Memberships]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Memberships](
	[ApplicationId] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[Password] [nvarchar](128) NOT NULL,
	[PasswordFormat] [int] NOT NULL,
	[PasswordSalt] [nvarchar](128) NOT NULL,
	[Email] [nvarchar](256) NULL,
	[PasswordQuestion] [nvarchar](256) NULL,
	[PasswordAnswer] [nvarchar](128) NULL,
	[IsApproved] [bit] NOT NULL,
	[IsLockedOut] [bit] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[LastLoginDate] [datetime] NOT NULL,
	[LastPasswordChangedDate] [datetime] NOT NULL,
	[LastLockoutDate] [datetime] NOT NULL,
	[FailedPasswordAttemptCount] [int] NOT NULL,
	[FailedPasswordAttemptWindowStart] [datetime] NOT NULL,
	[FailedPasswordAnswerAttemptCount] [int] NOT NULL,
	[FailedPasswordAnswerAttemptWindowsStart] [datetime] NOT NULL,
	[Comment] [nvarchar](256) NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
INSERT [dbo].[Memberships] ([ApplicationId], [UserId], [Password], [PasswordFormat], [PasswordSalt], [Email], [PasswordQuestion], [PasswordAnswer], [IsApproved], [IsLockedOut], [CreateDate], [LastLoginDate], [LastPasswordChangedDate], [LastLockoutDate], [FailedPasswordAttemptCount], [FailedPasswordAttemptWindowStart], [FailedPasswordAnswerAttemptCount], [FailedPasswordAnswerAttemptWindowsStart], [Comment]) VALUES (N'eee01e5b-119b-4ea7-aefe-8519b33faa5b', N'711a11d9-5428-42ac-90d3-931afaf9e6ab', N'rnZ6nz2baA5Fqed3kOYqmvz/49BipeQ4D5gol9Sfe6o=', 1, N'y/Bhl4S83tU9CYwvwwZaUg==', N'aasdf@asdf.com', NULL, NULL, 1, 0, CAST(0x00009F8001720A59 AS DateTime), CAST(0x00009F8001721296 AS DateTime), CAST(0x00009F8001720A59 AS DateTime), CAST(0xFFFF2FB300000000 AS DateTime), 0, CAST(0xFFFF2FB300000000 AS DateTime), 0, CAST(0xFFFF2FB300000000 AS DateTime), NULL)
INSERT [dbo].[Memberships] ([ApplicationId], [UserId], [Password], [PasswordFormat], [PasswordSalt], [Email], [PasswordQuestion], [PasswordAnswer], [IsApproved], [IsLockedOut], [CreateDate], [LastLoginDate], [LastPasswordChangedDate], [LastLockoutDate], [FailedPasswordAttemptCount], [FailedPasswordAttemptWindowStart], [FailedPasswordAnswerAttemptCount], [FailedPasswordAnswerAttemptWindowsStart], [Comment]) VALUES (N'eee01e5b-119b-4ea7-aefe-8519b33faa5b', N'a39999fa-db5a-49f2-a391-9ca812346f5e', N'a+JD/rYfv+GcZu8gJWEqXidv5ne+GNr1349q2OW1hAs=', 1, N'1JujpOt3CdKw5NDq818c+g==', N'davemateer@gmail.com', NULL, NULL, 1, 0, CAST(0x00009F7101729233 AS DateTime), CAST(0x00009F870148BFD5 AS DateTime), CAST(0x00009F7101729233 AS DateTime), CAST(0xFFFF2FB300000000 AS DateTime), 0, CAST(0xFFFF2FB300000000 AS DateTime), 0, CAST(0xFFFF2FB300000000 AS DateTime), NULL)
/****** Object:  ForeignKey [RoleApplication]    Script Date: 10/26/2011 09:23:10 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[RoleApplication]') AND parent_object_id = OBJECT_ID(N'[dbo].[Roles]'))
ALTER TABLE [dbo].[Roles]  WITH CHECK ADD  CONSTRAINT [RoleApplication] FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[Applications] ([ApplicationId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[RoleApplication]') AND parent_object_id = OBJECT_ID(N'[dbo].[Roles]'))
ALTER TABLE [dbo].[Roles] CHECK CONSTRAINT [RoleApplication]
GO
/****** Object:  ForeignKey [UserApplication]    Script Date: 10/26/2011 09:23:10 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[UserApplication]') AND parent_object_id = OBJECT_ID(N'[dbo].[Users]'))
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [UserApplication] FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[Applications] ([ApplicationId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[UserApplication]') AND parent_object_id = OBJECT_ID(N'[dbo].[Users]'))
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [UserApplication]
GO
/****** Object:  ForeignKey [FK_TricksCategories_Categories]    Script Date: 10/26/2011 09:23:10 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TricksCategories_Categories]') AND parent_object_id = OBJECT_ID(N'[dbo].[TricksCategories]'))
ALTER TABLE [dbo].[TricksCategories]  WITH CHECK ADD  CONSTRAINT [FK_TricksCategories_Categories] FOREIGN KEY([CategoryID])
REFERENCES [dbo].[Categories] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TricksCategories_Categories]') AND parent_object_id = OBJECT_ID(N'[dbo].[TricksCategories]'))
ALTER TABLE [dbo].[TricksCategories] CHECK CONSTRAINT [FK_TricksCategories_Categories]
GO
/****** Object:  ForeignKey [FK_TricksCategories_Tricks]    Script Date: 10/26/2011 09:23:10 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TricksCategories_Tricks]') AND parent_object_id = OBJECT_ID(N'[dbo].[TricksCategories]'))
ALTER TABLE [dbo].[TricksCategories]  WITH CHECK ADD  CONSTRAINT [FK_TricksCategories_Tricks] FOREIGN KEY([TrickID])
REFERENCES [dbo].[Tricks] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TricksCategories_Tricks]') AND parent_object_id = OBJECT_ID(N'[dbo].[TricksCategories]'))
ALTER TABLE [dbo].[TricksCategories] CHECK CONSTRAINT [FK_TricksCategories_Tricks]
GO
/****** Object:  ForeignKey [FK_Comments_Tricks]    Script Date: 10/26/2011 09:23:11 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Comments_Tricks]') AND parent_object_id = OBJECT_ID(N'[dbo].[Comments]'))
ALTER TABLE [dbo].[Comments]  WITH CHECK ADD  CONSTRAINT [FK_Comments_Tricks] FOREIGN KEY([TrickID])
REFERENCES [dbo].[Tricks] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Comments_Tricks]') AND parent_object_id = OBJECT_ID(N'[dbo].[Comments]'))
ALTER TABLE [dbo].[Comments] CHECK CONSTRAINT [FK_Comments_Tricks]
GO
/****** Object:  ForeignKey [UsersInRoleRole]    Script Date: 10/26/2011 09:23:11 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[UsersInRoleRole]') AND parent_object_id = OBJECT_ID(N'[dbo].[UsersInRoles]'))
ALTER TABLE [dbo].[UsersInRoles]  WITH CHECK ADD  CONSTRAINT [UsersInRoleRole] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([RoleId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[UsersInRoleRole]') AND parent_object_id = OBJECT_ID(N'[dbo].[UsersInRoles]'))
ALTER TABLE [dbo].[UsersInRoles] CHECK CONSTRAINT [UsersInRoleRole]
GO
/****** Object:  ForeignKey [UsersInRoleUser]    Script Date: 10/26/2011 09:23:11 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[UsersInRoleUser]') AND parent_object_id = OBJECT_ID(N'[dbo].[UsersInRoles]'))
ALTER TABLE [dbo].[UsersInRoles]  WITH CHECK ADD  CONSTRAINT [UsersInRoleUser] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[UsersInRoleUser]') AND parent_object_id = OBJECT_ID(N'[dbo].[UsersInRoles]'))
ALTER TABLE [dbo].[UsersInRoles] CHECK CONSTRAINT [UsersInRoleUser]
GO
/****** Object:  ForeignKey [UserProfile]    Script Date: 10/26/2011 09:23:11 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[UserProfile]') AND parent_object_id = OBJECT_ID(N'[dbo].[Profiles]'))
ALTER TABLE [dbo].[Profiles]  WITH CHECK ADD  CONSTRAINT [UserProfile] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[UserProfile]') AND parent_object_id = OBJECT_ID(N'[dbo].[Profiles]'))
ALTER TABLE [dbo].[Profiles] CHECK CONSTRAINT [UserProfile]
GO
/****** Object:  ForeignKey [MembershipApplication]    Script Date: 10/26/2011 09:23:11 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[MembershipApplication]') AND parent_object_id = OBJECT_ID(N'[dbo].[Memberships]'))
ALTER TABLE [dbo].[Memberships]  WITH CHECK ADD  CONSTRAINT [MembershipApplication] FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[Applications] ([ApplicationId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[MembershipApplication]') AND parent_object_id = OBJECT_ID(N'[dbo].[Memberships]'))
ALTER TABLE [dbo].[Memberships] CHECK CONSTRAINT [MembershipApplication]
GO
/****** Object:  ForeignKey [MembershipUser]    Script Date: 10/26/2011 09:23:11 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[MembershipUser]') AND parent_object_id = OBJECT_ID(N'[dbo].[Memberships]'))
ALTER TABLE [dbo].[Memberships]  WITH CHECK ADD  CONSTRAINT [MembershipUser] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[MembershipUser]') AND parent_object_id = OBJECT_ID(N'[dbo].[Memberships]'))
ALTER TABLE [dbo].[Memberships] CHECK CONSTRAINT [MembershipUser]
GO
