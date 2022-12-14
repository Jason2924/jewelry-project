USE [JewelryShopping]
GO
/****** Object:  Table [dbo].[ItemMst]    Script Date: 10/12/2020 9:28:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ItemMst](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[StyleCode] [varchar](50) NOT NULL,
	[Brand_ID] [int] NOT NULL,
	[Category_ID] [int] NOT NULL,
	[Certificate_ID] [int] NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[NumberInSet] [tinyint] NULL,
	[Quantity] [int] NULL,
	[GoldKarat] [tinyint] NOT NULL,
	[GoldWeight] [numeric](10, 3) NOT NULL,
	[Wastage] [tinyint] NULL,
	[Price] [numeric](10, 2) NULL,
	[MRP] [numeric](10, 2) NULL,
	[Description] [text] NULL,
	[Image] [varchar](255) NULL,
	[CreatedDate] [smalldatetime] NULL,
 CONSTRAINT [PK_ItemMst_1] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DiscountMst]    Script Date: 10/12/2020 9:28:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DiscountMst](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Item_ID] [int] NULL,
	[Percentage] [tinyint] NOT NULL,
	[Description] [text] NULL,
	[StartDate] [date] NULL,
	[EndDate] [date] NULL,
 CONSTRAINT [PK_Discount] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[ItemView]    Script Date: 10/12/2020 9:28:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ItemView] AS SELECT i.ID AS Item_ID, 
	SUM(CASE WHEN GETDATE() BETWEEN d.StartDate AND d.EndDate THEN d.Percentage ELSE 0 END) AS 'Discount', 
	CAST((i.Price - ISNULL(i.Price * SUM(CASE WHEN GETDATE() BETWEEN d.StartDate AND d.EndDate THEN d.Percentage ELSE 0 END) / 100, 0)) AS DECIMAL(18,2)) AS 'Sale' 
FROM ItemMst AS i LEFT JOIN DiscountMst AS d ON d.Item_ID = i.ID
GROUP BY i.ID, i.Price
GO
/****** Object:  Table [dbo].[AdminMst]    Script Date: 10/12/2020 9:28:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AdminMst](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Username] [varchar](50) NOT NULL,
	[Password] [varchar](50) NOT NULL,
 CONSTRAINT [PK_AdminMst_1] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BrandMst]    Script Date: 10/12/2020 9:28:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BrandMst](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_BrandMst] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CategoryMst]    Script Date: 10/12/2020 9:28:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CategoryMst](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_CategoryMst] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CertificateMst]    Script Date: 10/12/2020 9:28:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CertificateMst](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Type] [varchar](50) NOT NULL,
 CONSTRAINT [PK_CertificateMst] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InquiryMst]    Script Date: 10/12/2020 9:28:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InquiryMst](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FullName] [varchar](50) NOT NULL,
	[Address] [text] NOT NULL,
	[City] [varchar](100) NOT NULL,
	[Country] [varchar](50) NOT NULL,
	[Phone] [varchar](15) NOT NULL,
	[Email] [varchar](255) NOT NULL,
	[Content] [text] NOT NULL,
	[Status] [bit] NULL,
	[CreatedDate] [smalldatetime] NULL,
 CONSTRAINT [PK_Inquiry] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderDetailMst]    Script Date: 10/12/2020 9:28:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetailMst](
	[Order_ID] [int] NOT NULL,
	[Item_ID] [int] NOT NULL,
	[Price] [numeric](10, 2) NOT NULL,
	[Quantity] [int] NOT NULL,
 CONSTRAINT [PK_OrderDetail] PRIMARY KEY CLUSTERED 
(
	[Order_ID] ASC,
	[Item_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderMst]    Script Date: 10/12/2020 9:28:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderMst](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[User_ID] [int] NOT NULL,
	[FullName] [varchar](150) NOT NULL,
	[Address] [text] NOT NULL,
	[City] [varchar](100) NOT NULL,
	[Country] [varchar](50) NOT NULL,
	[Phone] [varchar](15) NOT NULL,
	[Email] [varchar](255) NOT NULL,
	[CreditCard] [varchar](50) NULL,
	[Status] [tinyint] NOT NULL,
	[CreatedDate] [smalldatetime] NULL,
 CONSTRAINT [PK_OrderMst] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StoneMst]    Script Date: 10/12/2020 9:28:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StoneMst](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Item_ID] [int] NULL,
	[StoneQuality_ID] [int] NOT NULL,
	[StoneType_ID] [int] NOT NULL,
	[Carat] [numeric](10, 2) NOT NULL,
	[Dimension] [varchar](100) NOT NULL,
	[Amount] [int] NOT NULL,
 CONSTRAINT [PK_StoneMst] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StoneQualityMst]    Script Date: 10/12/2020 9:28:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StoneQualityMst](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Quality] [varchar](50) NOT NULL,
 CONSTRAINT [PK_DiamondQualityMst] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StoneTypeMst]    Script Date: 10/12/2020 9:28:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StoneTypeMst](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_StoneType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserMst]    Script Date: 10/12/2020 9:28:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserMst](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Username] [varchar](50) NOT NULL,
	[Password] [varchar](50) NOT NULL,
	[FullName] [varchar](150) NOT NULL,
	[Address] [text] NOT NULL,
	[City] [varchar](100) NOT NULL,
	[Country] [varchar](50) NOT NULL,
	[Phone] [varchar](15) NULL,
	[Email] [varchar](255) NOT NULL,
	[Birthday] [date] NULL,
 CONSTRAINT [PK_UserMst_1] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[AdminMst] ON 

INSERT [dbo].[AdminMst] ([ID], [Username], [Password]) VALUES (1, N'admin', N'202cb962ac59075b964b07152d234b70')
SET IDENTITY_INSERT [dbo].[AdminMst] OFF
SET IDENTITY_INSERT [dbo].[BrandMst] ON 

INSERT [dbo].[BrandMst] ([ID], [Name]) VALUES (1, N'Tiffany & Co')
INSERT [dbo].[BrandMst] ([ID], [Name]) VALUES (2, N'Harry Winston')
INSERT [dbo].[BrandMst] ([ID], [Name]) VALUES (3, N'Cartier')
INSERT [dbo].[BrandMst] ([ID], [Name]) VALUES (4, N'Chopard')
INSERT [dbo].[BrandMst] ([ID], [Name]) VALUES (5, N'Van Cleef & Arpels')
SET IDENTITY_INSERT [dbo].[BrandMst] OFF
SET IDENTITY_INSERT [dbo].[CategoryMst] ON 

INSERT [dbo].[CategoryMst] ([ID], [Name]) VALUES (1, N'Ring')
INSERT [dbo].[CategoryMst] ([ID], [Name]) VALUES (2, N'Necklace')
INSERT [dbo].[CategoryMst] ([ID], [Name]) VALUES (3, N'Earring')
SET IDENTITY_INSERT [dbo].[CategoryMst] OFF
SET IDENTITY_INSERT [dbo].[CertificateMst] ON 

INSERT [dbo].[CertificateMst] ([ID], [Type]) VALUES (1, N'918')
INSERT [dbo].[CertificateMst] ([ID], [Type]) VALUES (2, N'920')
INSERT [dbo].[CertificateMst] ([ID], [Type]) VALUES (3, N'922')
INSERT [dbo].[CertificateMst] ([ID], [Type]) VALUES (4, N'924')
INSERT [dbo].[CertificateMst] ([ID], [Type]) VALUES (5, N'926')
SET IDENTITY_INSERT [dbo].[CertificateMst] OFF
SET IDENTITY_INSERT [dbo].[DiscountMst] ON 

INSERT [dbo].[DiscountMst] ([ID], [Item_ID], [Percentage], [Description], [StartDate], [EndDate]) VALUES (1, 1, 20, N'Sale off 20%', CAST(N'2020-09-20' AS Date), CAST(N'2020-10-20' AS Date))
INSERT [dbo].[DiscountMst] ([ID], [Item_ID], [Percentage], [Description], [StartDate], [EndDate]) VALUES (2, 2, 35, N'Sale off 35%', CAST(N'2020-09-20' AS Date), CAST(N'2020-10-20' AS Date))
INSERT [dbo].[DiscountMst] ([ID], [Item_ID], [Percentage], [Description], [StartDate], [EndDate]) VALUES (3, 1, 5, N'Sale off 5%', CAST(N'2020-09-20' AS Date), CAST(N'2020-10-01' AS Date))
INSERT [dbo].[DiscountMst] ([ID], [Item_ID], [Percentage], [Description], [StartDate], [EndDate]) VALUES (4, 2, 10, N'Sale off 10%', CAST(N'2020-10-10' AS Date), CAST(N'2020-10-20' AS Date))
INSERT [dbo].[DiscountMst] ([ID], [Item_ID], [Percentage], [Description], [StartDate], [EndDate]) VALUES (5, 8, 10, N'Weekend Sale', CAST(N'2020-10-09' AS Date), CAST(N'2020-10-31' AS Date))
INSERT [dbo].[DiscountMst] ([ID], [Item_ID], [Percentage], [Description], [StartDate], [EndDate]) VALUES (6, 10, 20, N'Weekend Sale', CAST(N'2020-10-10' AS Date), CAST(N'2020-10-31' AS Date))
INSERT [dbo].[DiscountMst] ([ID], [Item_ID], [Percentage], [Description], [StartDate], [EndDate]) VALUES (7, 14, 25, N'Sale off 25%', CAST(N'2020-10-12' AS Date), CAST(N'2020-10-31' AS Date))
INSERT [dbo].[DiscountMst] ([ID], [Item_ID], [Percentage], [Description], [StartDate], [EndDate]) VALUES (8, 12, 35, N'Sale off 35%', CAST(N'2020-10-01' AS Date), CAST(N'2020-10-31' AS Date))
INSERT [dbo].[DiscountMst] ([ID], [Item_ID], [Percentage], [Description], [StartDate], [EndDate]) VALUES (9, 16, 30, N'Sale off 30%', CAST(N'2020-10-06' AS Date), CAST(N'2020-10-29' AS Date))
INSERT [dbo].[DiscountMst] ([ID], [Item_ID], [Percentage], [Description], [StartDate], [EndDate]) VALUES (10, 17, 20, N'Sale off 20%', CAST(N'2020-10-12' AS Date), CAST(N'2020-11-04' AS Date))
SET IDENTITY_INSERT [dbo].[DiscountMst] OFF
SET IDENTITY_INSERT [dbo].[InquiryMst] ON 

INSERT [dbo].[InquiryMst] ([ID], [FullName], [Address], [City], [Country], [Phone], [Email], [Content], [Status], [CreatedDate]) VALUES (1, N'	Roosevelt L Mitchell', N'886  Summit Street', N'Davenport', N'USA', N'563-508-4876', N'tt1afnwwkg@temporary-mail.net', N'Style Code : #0001, Brand : Diamond Town, Gold Karat : 10, Gold Weight : 15
Stone : 1, Quality : 1, Carat : 1.5, Amount : 1', 0, CAST(N'2020-09-30T12:10:00' AS SmallDateTime))
INSERT [dbo].[InquiryMst] ([ID], [FullName], [Address], [City], [Country], [Phone], [Email], [Content], [Status], [CreatedDate]) VALUES (2, N'Glenn M Johnson', N'2328  Warner Street', N'Doral', N'USA', N'305-994-7685', N'5kgxpf3j4ce@temporary-mail.net', N'Style Code : #0002, Brand : Diamond Town, Gold Karat : 10, Gold Weight : 15
Stone : 1, Quality : 1, Carat : 1.5, Amount : 1
Stone : 3, Quality : 2, Carat : 1.5, Amount : 2
Stone : 4, Quality : 3, Carat : 2, Amount : 3', 1, CAST(N'2020-10-09T16:10:00' AS SmallDateTime))
INSERT [dbo].[InquiryMst] ([ID], [FullName], [Address], [City], [Country], [Phone], [Email], [Content], [Status], [CreatedDate]) VALUES (3, N'	Antonio I Bailey', N'1003  Cambridge Court', N'Fayetteville', N'USA', N'479-841-5702', N't2anh00oyv@temporary-mail.net', N'Style Code : #0001, Brand : Diamond Town, Gold Karat : 10, Gold Weight : 15
Stone : 1, Quality : 1, Carat : 1.5, Amount : 1', 1, CAST(N'2020-10-09T16:17:00' AS SmallDateTime))
INSERT [dbo].[InquiryMst] ([ID], [FullName], [Address], [City], [Country], [Phone], [Email], [Content], [Status], [CreatedDate]) VALUES (4, N'	Jessica W Greiner', N'2935  Grey Fox Farm Road', N'Houston', N'USA', N'281-777-8530', N'za46azltq5l@temporary-mail.net', N'Style Code : #0001, Brand : Diamond Town, Gold Karat : 10, Gold Weight : 15
Stone : 1, Quality : 1, Carat : 1.5, Amount : 1', 0, CAST(N'2020-10-09T16:19:00' AS SmallDateTime))
INSERT [dbo].[InquiryMst] ([ID], [FullName], [Address], [City], [Country], [Phone], [Email], [Content], [Status], [CreatedDate]) VALUES (5, N'	Robert S Jacobs', N'279  Stark Hollow Road', N'NACHES', N'USA', N'970-949-5522', N'd7n5or98nls@temporary-mail.net', N'Style Code : #0002, Brand : Diamond Town, Gold Karat : 10, Gold Weight : 15
Stone : 1, Quality : 1, Carat : 1.5, Amount : 1
Stone : 3, Quality : 2, Carat : 1.5, Amount : 2
Stone : 4, Quality : 3, Carat : 2, Amount : 3', 1, CAST(N'2020-10-09T17:09:00' AS SmallDateTime))
SET IDENTITY_INSERT [dbo].[InquiryMst] OFF
SET IDENTITY_INSERT [dbo].[ItemMst] ON 

INSERT [dbo].[ItemMst] ([ID], [StyleCode], [Brand_ID], [Category_ID], [Certificate_ID], [Name], [NumberInSet], [Quantity], [GoldKarat], [GoldWeight], [Wastage], [Price], [MRP], [Description], [Image], [CreatedDate]) VALUES (1, N'0001', 1, 1, 1, N'Diamond Ring', 1, 6, 5, CAST(20.000 AS Numeric(10, 3)), 10, CAST(500.00 AS Numeric(10, 2)), CAST(550.00 AS Numeric(10, 2)), N'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur luctus sapien orci, a mollis risus mattis a. Suspendisse consectetur fermentum arcu ac tempus. Nunc turpis diam, convallis sed sollicitudin ut, consequat vitae neque. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Donec quis dui non est rhoncus rutrum. Morbi condimentum lectus lacus, vitae pharetra est ullamcorper in. Nullam non tellus efficitur neque viverra aliquet. Ut rhoncus pharetra dui. In aliquet, ex non volutpat mollis, sapien ante ullamcorper urna, vel sollicitudin dolor lorem quis velit. Vestibulum vitae sollicitudin sem. Quisque vitae facilisis felis, eu ullamcorper diam. Nullam ut mollis risus, eget faucibus metus.', N'1.png', NULL)
INSERT [dbo].[ItemMst] ([ID], [StyleCode], [Brand_ID], [Category_ID], [Certificate_ID], [Name], [NumberInSet], [Quantity], [GoldKarat], [GoldWeight], [Wastage], [Price], [MRP], [Description], [Image], [CreatedDate]) VALUES (2, N'0002', 2, 1, 2, N'Silver Ring', 1, 15, 3, CAST(30.000 AS Numeric(10, 3)), 10, CAST(300.00 AS Numeric(10, 2)), CAST(400.00 AS Numeric(10, 2)), N'Maecenas porta commodo pharetra. Pellentesque sollicitudin velit in molestie blandit. Curabitur blandit at nunc vel lacinia. Ut non imperdiet ex. In commodo suscipit urna a ornare. Quisque viverra erat nec volutpat accumsan. Sed non bibendum turpis. Sed aliquam convallis sem, sed rutrum neque ultrices ac. Nam aliquam risus sem, vel placerat ante scelerisque sit amet.', N'2.png', NULL)
INSERT [dbo].[ItemMst] ([ID], [StyleCode], [Brand_ID], [Category_ID], [Certificate_ID], [Name], [NumberInSet], [Quantity], [GoldKarat], [GoldWeight], [Wastage], [Price], [MRP], [Description], [Image], [CreatedDate]) VALUES (3, N'0003', 3, 2, 3, N'Ruby Nacklace', 1, 0, 8, CAST(15.000 AS Numeric(10, 3)), 5, CAST(400.00 AS Numeric(10, 2)), CAST(400.00 AS Numeric(10, 2)), N'Nulla maximus fringilla justo, ac congue enim posuere id. Curabitur in sapien viverra, eleifend enim eget, varius ex. Nulla commodo augue et turpis sagittis venenatis. Mauris dolor mi, varius vitae dignissim et, faucibus in velit. Mauris consequat, nibh at ornare maximus, tellus purus varius lacus, eu cursus purus ex in metus. Cras non tempor ligula. Vestibulum nunc lectus, consequat eget dui bibendum, pretium aliquet massa. Integer non tincidunt tortor. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Fusce vel nunc sed magna vulputate tempor. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Fusce semper ipsum urna. Ut ut est odio. Etiam sed sollicitudin elit.', N'3.png', NULL)
INSERT [dbo].[ItemMst] ([ID], [StyleCode], [Brand_ID], [Category_ID], [Certificate_ID], [Name], [NumberInSet], [Quantity], [GoldKarat], [GoldWeight], [Wastage], [Price], [MRP], [Description], [Image], [CreatedDate]) VALUES (6, N'0006', 1, 1, 1, N'Couple Rings', 2, 10, 4, CAST(12.000 AS Numeric(10, 3)), 0, CAST(800.00 AS Numeric(10, 2)), CAST(1000.00 AS Numeric(10, 2)), N'Pellentesque elementum nulla ipsum. Mauris eget dignissim augue. Vestibulum id dignissim justo. Fusce sollicitudin lorem imperdiet eros malesuada gravida. Aenean egestas efficitur luctus. Sed ultricies nunc sit amet nunc aliquam porta. Ut auctor ornare neque, at accumsan ligula finibus a. Ut a rutrum orci. Vestibulum posuere at ex vel rutrum. Vivamus egestas lacus non sodales gravida.', N'6.png', NULL)
INSERT [dbo].[ItemMst] ([ID], [StyleCode], [Brand_ID], [Category_ID], [Certificate_ID], [Name], [NumberInSet], [Quantity], [GoldKarat], [GoldWeight], [Wastage], [Price], [MRP], [Description], [Image], [CreatedDate]) VALUES (8, N'0007', 4, 3, 4, N'Gold Earrings', 2, 10, 18, CAST(5.000 AS Numeric(10, 3)), 0, CAST(0.00 AS Numeric(10, 2)), CAST(200.00 AS Numeric(10, 2)), N'Pellentesque elementum nulla ipsum. Mauris eget dignissim augue. Vestibulum id dignissim justo. Fusce sollicitudin lorem imperdiet eros malesuada gravida. Aenean egestas efficitur luctus. Sed ultricies nunc sit amet nunc aliquam porta. Ut auctor ornare neque, at accumsan ligula finibus a. Ut a rutrum orci. Vestibulum posuere at ex vel rutrum. Vivamus egestas lacus non sodales gravida.', N'8.png', NULL)
INSERT [dbo].[ItemMst] ([ID], [StyleCode], [Brand_ID], [Category_ID], [Certificate_ID], [Name], [NumberInSet], [Quantity], [GoldKarat], [GoldWeight], [Wastage], [Price], [MRP], [Description], [Image], [CreatedDate]) VALUES (9, N'0008', 5, 1, 3, N'Wedding Rings', 2, 8, 20, CAST(4.000 AS Numeric(10, 3)), 8, CAST(550.00 AS Numeric(10, 2)), CAST(600.00 AS Numeric(10, 2)), N'Pellentesque elementum nulla ipsum. Mauris eget dignissim augue. Vestibulum id dignissim justo. Fusce sollicitudin lorem imperdiet eros malesuada gravida. Aenean egestas efficitur luctus. Sed ultricies nunc sit amet nunc aliquam porta. Ut auctor ornare neque, at accumsan ligula finibus a. Ut a rutrum orci. Vestibulum posuere at ex vel rutrum. Vivamus egestas lacus non sodales gravida.', N'9.png', NULL)
INSERT [dbo].[ItemMst] ([ID], [StyleCode], [Brand_ID], [Category_ID], [Certificate_ID], [Name], [NumberInSet], [Quantity], [GoldKarat], [GoldWeight], [Wastage], [Price], [MRP], [Description], [Image], [CreatedDate]) VALUES (10, N'0009', 3, 2, 3, N'Diamond Flower Necklace', 1, 5, 18, CAST(5.000 AS Numeric(10, 3)), 10, CAST(150.00 AS Numeric(10, 2)), CAST(180.00 AS Numeric(10, 2)), N'Pellentesque elementum nulla ipsum. Mauris eget dignissim augue. Vestibulum id dignissim justo. Fusce sollicitudin lorem imperdiet eros malesuada gravida. Aenean egestas efficitur luctus. Sed ultricies nunc sit amet nunc aliquam porta. Ut auctor ornare neque, at accumsan ligula finibus a. Ut a rutrum orci. Vestibulum posuere at ex vel rutrum. Vivamus egestas lacus non sodales gravida.', N'0.png', NULL)
INSERT [dbo].[ItemMst] ([ID], [StyleCode], [Brand_ID], [Category_ID], [Certificate_ID], [Name], [NumberInSet], [Quantity], [GoldKarat], [GoldWeight], [Wastage], [Price], [MRP], [Description], [Image], [CreatedDate]) VALUES (11, N'0010', 2, 1, 1, N'DIamon Wedding Ring', 1, 20, 0, CAST(0.000 AS Numeric(10, 3)), 0, CAST(180.00 AS Numeric(10, 2)), CAST(200.00 AS Numeric(10, 2)), N'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur luctus sapien orci, a mollis risus mattis a. Suspendisse consectetur fermentum arcu ac tempus. Nunc turpis diam, convallis sed sollicitudin ut, consequat vitae neque. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Donec quis dui non est rhoncus rutrum. Morbi condimentum lectus lacus, vitae pharetra est ullamcorper in. Nullam non tellus efficitur neque viverra aliquet. Ut rhoncus pharetra dui. In aliquet, ex non volutpat mollis, sapien ante ullamcorper urna, vel sollicitudin dolor lorem quis velit. Vestibulum vitae sollicitudin sem. Quisque vitae facilisis felis, eu ullamcorper diam. Nullam ut mollis risus, eget faucibus metus.', N'11.png', NULL)
INSERT [dbo].[ItemMst] ([ID], [StyleCode], [Brand_ID], [Category_ID], [Certificate_ID], [Name], [NumberInSet], [Quantity], [GoldKarat], [GoldWeight], [Wastage], [Price], [MRP], [Description], [Image], [CreatedDate]) VALUES (12, N'0011', 4, 2, 5, N'Gold Oyster Necklace', 1, 8, 24, CAST(6.000 AS Numeric(10, 3)), 10, CAST(150.00 AS Numeric(10, 2)), CAST(200.00 AS Numeric(10, 2)), NULL, N'12.png', NULL)
INSERT [dbo].[ItemMst] ([ID], [StyleCode], [Brand_ID], [Category_ID], [Certificate_ID], [Name], [NumberInSet], [Quantity], [GoldKarat], [GoldWeight], [Wastage], [Price], [MRP], [Description], [Image], [CreatedDate]) VALUES (13, N'0012', 5, 2, 5, N'Amethyst  Cover Nacklace', 1, 10, 0, CAST(0.000 AS Numeric(10, 3)), 0, CAST(0.00 AS Numeric(10, 2)), CAST(200.00 AS Numeric(10, 2)), N'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur luctus sapien orci, a mollis risus mattis a. Suspendisse consectetur fermentum arcu ac tempus. Nunc turpis diam, convallis sed sollicitudin ut, consequat vitae neque. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Donec quis dui non est rhoncus rutrum. Morbi condimentum lectus lacus, vitae pharetra est ullamcorper in. Nullam non tellus efficitur neque viverra aliquet. Ut rhoncus pharetra dui. In aliquet, ex non volutpat mollis, sapien ante ullamcorper urna, vel sollicitudin dolor lorem quis velit. Vestibulum vitae sollicitudin sem. Quisque vitae facilisis felis, eu ullamcorper diam. Nullam ut mollis risus, eget faucibus metus.', N'13.png', NULL)
INSERT [dbo].[ItemMst] ([ID], [StyleCode], [Brand_ID], [Category_ID], [Certificate_ID], [Name], [NumberInSet], [Quantity], [GoldKarat], [GoldWeight], [Wastage], [Price], [MRP], [Description], [Image], [CreatedDate]) VALUES (14, N'0013', 1, 3, 2, N'Water Drop Earrings', 2, 25, 22, CAST(10.000 AS Numeric(10, 3)), 20, CAST(300.00 AS Numeric(10, 2)), CAST(300.00 AS Numeric(10, 2)), N'Pellentesque elementum nulla ipsum. Mauris eget dignissim augue. Vestibulum id dignissim justo. Fusce sollicitudin lorem imperdiet eros malesuada gravida. Aenean egestas efficitur luctus. Sed ultricies nunc sit amet nunc aliquam porta. Ut auctor ornare neque, at accumsan ligula finibus a. Ut a rutrum orci. Vestibulum posuere at ex vel rutrum. Vivamus egestas lacus non sodales gravida.', N'14.png', NULL)
INSERT [dbo].[ItemMst] ([ID], [StyleCode], [Brand_ID], [Category_ID], [Certificate_ID], [Name], [NumberInSet], [Quantity], [GoldKarat], [GoldWeight], [Wastage], [Price], [MRP], [Description], [Image], [CreatedDate]) VALUES (15, N'0014', 2, 1, 3, N'Gold Diamond Ring', 1, 28, 20, CAST(9.000 AS Numeric(10, 3)), 0, CAST(330.00 AS Numeric(10, 2)), CAST(330.00 AS Numeric(10, 2)), N'Nulla maximus fringilla justo, ac congue enim posuere id. Curabitur in sapien viverra, eleifend enim eget, varius ex. Nulla commodo augue et turpis sagittis venenatis. Mauris dolor mi, varius vitae dignissim et, faucibus in velit. Mauris consequat, nibh at ornare maximus, tellus purus varius lacus, eu cursus purus ex in metus. Cras non tempor ligula. Vestibulum nunc lectus, consequat eget dui bibendum, pretium aliquet massa. Integer non tincidunt tortor. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Fusce vel nunc sed magna vulputate tempor. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Fusce semper ipsum urna. Ut ut est odio. Etiam sed sollicitudin elit.', N'15.png', NULL)
INSERT [dbo].[ItemMst] ([ID], [StyleCode], [Brand_ID], [Category_ID], [Certificate_ID], [Name], [NumberInSet], [Quantity], [GoldKarat], [GoldWeight], [Wastage], [Price], [MRP], [Description], [Image], [CreatedDate]) VALUES (16, N'0015', 4, 2, 2, N'Colorful Necklace', 1, 15, 18, CAST(7.000 AS Numeric(10, 3)), 0, CAST(350.00 AS Numeric(10, 2)), CAST(350.00 AS Numeric(10, 2)), N'Maecenas porta commodo pharetra. Pellentesque sollicitudin velit in molestie blandit. Curabitur blandit at nunc vel lacinia. Ut non imperdiet ex. In commodo suscipit urna a ornare. Quisque viverra erat nec volutpat accumsan. Sed non bibendum turpis. Sed aliquam convallis sem, sed rutrum neque ultrices ac. Nam aliquam risus sem, vel placerat ante scelerisque sit amet.', N'16.png', NULL)
INSERT [dbo].[ItemMst] ([ID], [StyleCode], [Brand_ID], [Category_ID], [Certificate_ID], [Name], [NumberInSet], [Quantity], [GoldKarat], [GoldWeight], [Wastage], [Price], [MRP], [Description], [Image], [CreatedDate]) VALUES (17, N'0016', 2, 3, 4, N'Ruby Flower Earrings', 2, 15, 0, CAST(0.000 AS Numeric(10, 3)), 0, CAST(400.00 AS Numeric(10, 2)), CAST(400.00 AS Numeric(10, 2)), N'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur luctus sapien orci, a mollis risus mattis a. Suspendisse consectetur fermentum arcu ac tempus. Nunc turpis diam, convallis sed sollicitudin ut, consequat vitae neque. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Donec quis dui non est rhoncus rutrum. Morbi condimentum lectus lacus, vitae pharetra est ullamcorper in. Nullam non tellus efficitur neque viverra aliquet. Ut rhoncus pharetra dui. In aliquet, ex non volutpat mollis, sapien ante ullamcorper urna, vel sollicitudin dolor lorem quis velit. Vestibulum vitae sollicitudin sem. Quisque vitae facilisis felis, eu ullamcorper diam. Nullam ut mollis risus, eget faucibus metus.', N'17.png', NULL)
INSERT [dbo].[ItemMst] ([ID], [StyleCode], [Brand_ID], [Category_ID], [Certificate_ID], [Name], [NumberInSet], [Quantity], [GoldKarat], [GoldWeight], [Wastage], [Price], [MRP], [Description], [Image], [CreatedDate]) VALUES (18, N'0017', 4, 3, 4, N'Circle Earrings', 2, 17, 24, CAST(10.000 AS Numeric(10, 3)), 0, CAST(300.00 AS Numeric(10, 2)), CAST(300.00 AS Numeric(10, 2)), N'Pellentesque elementum nulla ipsum. Mauris eget dignissim augue. Vestibulum id dignissim justo. Fusce sollicitudin lorem imperdiet eros malesuada gravida. Aenean egestas efficitur luctus. Sed ultricies nunc sit amet nunc aliquam porta. Ut auctor ornare neque, at accumsan ligula finibus a. Ut a rutrum orci. Vestibulum posuere at ex vel rutrum. Vivamus egestas lacus non sodales gravida.', N'18.png', NULL)
INSERT [dbo].[ItemMst] ([ID], [StyleCode], [Brand_ID], [Category_ID], [Certificate_ID], [Name], [NumberInSet], [Quantity], [GoldKarat], [GoldWeight], [Wastage], [Price], [MRP], [Description], [Image], [CreatedDate]) VALUES (19, N'0020', 5, 3, 5, N'Gold Diamon Earrings', 2, 40, 22, CAST(15.000 AS Numeric(10, 3)), 20, CAST(270.00 AS Numeric(10, 2)), CAST(300.00 AS Numeric(10, 2)), N'Pellentesque elementum nulla ipsum. Mauris eget dignissim augue. Vestibulum id dignissim justo. Fusce sollicitudin lorem imperdiet eros malesuada gravida. Aenean egestas efficitur luctus. Sed ultricies nunc sit amet nunc aliquam porta. Ut auctor ornare neque, at accumsan ligula finibus a. Ut a rutrum orci. Vestibulum posuere at ex vel rutrum. Vivamus egestas lacus non sodales gravida.', N'19.png', NULL)
INSERT [dbo].[ItemMst] ([ID], [StyleCode], [Brand_ID], [Category_ID], [Certificate_ID], [Name], [NumberInSet], [Quantity], [GoldKarat], [GoldWeight], [Wastage], [Price], [MRP], [Description], [Image], [CreatedDate]) VALUES (20, N'0021', 1, 2, 2, N'Heart Necklace', 1, 22, 20, CAST(6.000 AS Numeric(10, 3)), 10, CAST(300.00 AS Numeric(10, 2)), CAST(300.00 AS Numeric(10, 2)), N'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur luctus sapien orci, a mollis risus mattis a. Suspendisse consectetur fermentum arcu ac tempus. Nunc turpis diam, convallis sed sollicitudin ut, consequat vitae neque. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Donec quis dui non est rhoncus rutrum. Morbi condimentum lectus lacus, vitae pharetra est ullamcorper in. Nullam non tellus efficitur neque viverra aliquet. Ut rhoncus pharetra dui. In aliquet, ex non volutpat mollis, sapien ante ullamcorper urna, vel sollicitudin dolor lorem quis velit. Vestibulum vitae sollicitudin sem. Quisque vitae facilisis felis, eu ullamcorper diam. Nullam ut mollis risus, eget faucibus metus.', N'20.png', NULL)
SET IDENTITY_INSERT [dbo].[ItemMst] OFF
INSERT [dbo].[OrderDetailMst] ([Order_ID], [Item_ID], [Price], [Quantity]) VALUES (1, 1, CAST(500.00 AS Numeric(10, 2)), 3)
INSERT [dbo].[OrderDetailMst] ([Order_ID], [Item_ID], [Price], [Quantity]) VALUES (1, 3, CAST(400.00 AS Numeric(10, 2)), 2)
INSERT [dbo].[OrderDetailMst] ([Order_ID], [Item_ID], [Price], [Quantity]) VALUES (3, 2, CAST(300.00 AS Numeric(10, 2)), 2)
INSERT [dbo].[OrderDetailMst] ([Order_ID], [Item_ID], [Price], [Quantity]) VALUES (4, 1, CAST(500.00 AS Numeric(10, 2)), 4)
SET IDENTITY_INSERT [dbo].[OrderMst] ON 

INSERT [dbo].[OrderMst] ([ID], [User_ID], [FullName], [Address], [City], [Country], [Phone], [Email], [CreditCard], [Status], [CreatedDate]) VALUES (1, 1, N'	Donald A Milligan', N'3420  Cardinal Lane', N'Cleveland', N'USA', N'216-948-8431', N'cig3mpt325g@temporary-mail.net', N'4556097187899626', 1, CAST(N'2020-10-05T17:35:00' AS SmallDateTime))
INSERT [dbo].[OrderMst] ([ID], [User_ID], [FullName], [Address], [City], [Country], [Phone], [Email], [CreditCard], [Status], [CreatedDate]) VALUES (2, 2, N'	Margaret S Mitchell', N'3079  Cambridge Drive', N'LONG VALLEY', N'USA', N'623-533-3119', N'htcl25sqoqw@temporary-mail.net', N'4085083514614433
', 1, CAST(N'2020-10-05T17:35:00' AS SmallDateTime))
INSERT [dbo].[OrderMst] ([ID], [User_ID], [FullName], [Address], [City], [Country], [Phone], [Email], [CreditCard], [Status], [CreatedDate]) VALUES (3, 4, N'	Charles A Little', N'4245  Tail Ends Road', N'Green Bay', N'USA', N'920-535-3679', N'7xj0h7zuru8@temporary-mail.net', N'4539276317782991', 1, CAST(N'2020-10-05T17:35:00' AS SmallDateTime))
INSERT [dbo].[OrderMst] ([ID], [User_ID], [FullName], [Address], [City], [Country], [Phone], [Email], [CreditCard], [Status], [CreatedDate]) VALUES (4, 1, N'	Dorotha R Hicks', N'718  New Creek Road', N'Huntsville', N'USA', N'256-841-0564', N'ys1lxyhsst@temporary-mail.net', N'4556097187899626', 2, CAST(N'2020-10-06T19:05:00' AS SmallDateTime))
SET IDENTITY_INSERT [dbo].[OrderMst] OFF
SET IDENTITY_INSERT [dbo].[StoneMst] ON 

INSERT [dbo].[StoneMst] ([ID], [Item_ID], [StoneQuality_ID], [StoneType_ID], [Carat], [Dimension], [Amount]) VALUES (1, 1, 1, 1, CAST(5.00 AS Numeric(10, 2)), N'5x5 mm', 1)
INSERT [dbo].[StoneMst] ([ID], [Item_ID], [StoneQuality_ID], [StoneType_ID], [Carat], [Dimension], [Amount]) VALUES (2, 2, 6, 1, CAST(3.00 AS Numeric(10, 2)), N'3x3 mm', 4)
INSERT [dbo].[StoneMst] ([ID], [Item_ID], [StoneQuality_ID], [StoneType_ID], [Carat], [Dimension], [Amount]) VALUES (4, 3, 3, 2, CAST(8.00 AS Numeric(10, 2)), N'15x10 mm', 1)
INSERT [dbo].[StoneMst] ([ID], [Item_ID], [StoneQuality_ID], [StoneType_ID], [Carat], [Dimension], [Amount]) VALUES (6, 2, 7, 1, CAST(69.00 AS Numeric(10, 2)), N'5', 6)
INSERT [dbo].[StoneMst] ([ID], [Item_ID], [StoneQuality_ID], [StoneType_ID], [Carat], [Dimension], [Amount]) VALUES (8, 3, 1, 4, CAST(69.00 AS Numeric(10, 2)), N'3', 5)
INSERT [dbo].[StoneMst] ([ID], [Item_ID], [StoneQuality_ID], [StoneType_ID], [Carat], [Dimension], [Amount]) VALUES (9, 10, 3, 3, CAST(3.00 AS Numeric(10, 2)), N'5x5 mm', 6)
INSERT [dbo].[StoneMst] ([ID], [Item_ID], [StoneQuality_ID], [StoneType_ID], [Carat], [Dimension], [Amount]) VALUES (10, 10, 1, 1, CAST(5.00 AS Numeric(10, 2)), N'10x10 mm', 1)
INSERT [dbo].[StoneMst] ([ID], [Item_ID], [StoneQuality_ID], [StoneType_ID], [Carat], [Dimension], [Amount]) VALUES (11, 14, 2, 1, CAST(1.00 AS Numeric(10, 2)), N'2x2 mm', 16)
INSERT [dbo].[StoneMst] ([ID], [Item_ID], [StoneQuality_ID], [StoneType_ID], [Carat], [Dimension], [Amount]) VALUES (12, 6, 3, 1, CAST(2.00 AS Numeric(10, 2)), N'4x4 mm', 1)
INSERT [dbo].[StoneMst] ([ID], [Item_ID], [StoneQuality_ID], [StoneType_ID], [Carat], [Dimension], [Amount]) VALUES (13, 6, 6, 1, CAST(1.00 AS Numeric(10, 2)), N'2x2 mm', 4)
INSERT [dbo].[StoneMst] ([ID], [Item_ID], [StoneQuality_ID], [StoneType_ID], [Carat], [Dimension], [Amount]) VALUES (14, 9, 1, 1, CAST(5.00 AS Numeric(10, 2)), N'10x10 mm', 1)
INSERT [dbo].[StoneMst] ([ID], [Item_ID], [StoneQuality_ID], [StoneType_ID], [Carat], [Dimension], [Amount]) VALUES (15, 9, 2, 1, CAST(2.50 AS Numeric(10, 2)), N'5x5 mm', 2)
INSERT [dbo].[StoneMst] ([ID], [Item_ID], [StoneQuality_ID], [StoneType_ID], [Carat], [Dimension], [Amount]) VALUES (16, 11, 5, 1, CAST(3.00 AS Numeric(10, 2)), N'6x6 mm', 1)
INSERT [dbo].[StoneMst] ([ID], [Item_ID], [StoneQuality_ID], [StoneType_ID], [Carat], [Dimension], [Amount]) VALUES (17, 12, 3, 5, CAST(4.00 AS Numeric(10, 2)), N'2x10 mm', 1)
INSERT [dbo].[StoneMst] ([ID], [Item_ID], [StoneQuality_ID], [StoneType_ID], [Carat], [Dimension], [Amount]) VALUES (18, 12, 5, 4, CAST(1.00 AS Numeric(10, 2)), N'2x2 mm', 20)
INSERT [dbo].[StoneMst] ([ID], [Item_ID], [StoneQuality_ID], [StoneType_ID], [Carat], [Dimension], [Amount]) VALUES (19, 15, 3, 1, CAST(4.00 AS Numeric(10, 2)), N'6x6 mm', 1)
INSERT [dbo].[StoneMst] ([ID], [Item_ID], [StoneQuality_ID], [StoneType_ID], [Carat], [Dimension], [Amount]) VALUES (20, 16, 2, 1, CAST(3.00 AS Numeric(10, 2)), N'3x3 mm', 2)
INSERT [dbo].[StoneMst] ([ID], [Item_ID], [StoneQuality_ID], [StoneType_ID], [Carat], [Dimension], [Amount]) VALUES (21, 16, 3, 3, CAST(3.00 AS Numeric(10, 2)), N'3x3 mm', 2)
INSERT [dbo].[StoneMst] ([ID], [Item_ID], [StoneQuality_ID], [StoneType_ID], [Carat], [Dimension], [Amount]) VALUES (22, 16, 1, 2, CAST(5.00 AS Numeric(10, 2)), N'6x6 mm', 1)
INSERT [dbo].[StoneMst] ([ID], [Item_ID], [StoneQuality_ID], [StoneType_ID], [Carat], [Dimension], [Amount]) VALUES (23, 17, 4, 2, CAST(4.00 AS Numeric(10, 2)), N'3x9', 12)
INSERT [dbo].[StoneMst] ([ID], [Item_ID], [StoneQuality_ID], [StoneType_ID], [Carat], [Dimension], [Amount]) VALUES (24, 17, 3, 1, CAST(3.00 AS Numeric(10, 2)), N'4x4 mm', 1)
INSERT [dbo].[StoneMst] ([ID], [Item_ID], [StoneQuality_ID], [StoneType_ID], [Carat], [Dimension], [Amount]) VALUES (25, 19, 1, 5, CAST(5.00 AS Numeric(10, 2)), N'10x10 mm', 1)
INSERT [dbo].[StoneMst] ([ID], [Item_ID], [StoneQuality_ID], [StoneType_ID], [Carat], [Dimension], [Amount]) VALUES (26, 20, 2, 2, CAST(4.00 AS Numeric(10, 2)), N'6x6 mm', 1)
INSERT [dbo].[StoneMst] ([ID], [Item_ID], [StoneQuality_ID], [StoneType_ID], [Carat], [Dimension], [Amount]) VALUES (27, 20, 2, 4, CAST(2.00 AS Numeric(10, 2)), N'3x3 mm', 10)
SET IDENTITY_INSERT [dbo].[StoneMst] OFF
SET IDENTITY_INSERT [dbo].[StoneQualityMst] ON 

INSERT [dbo].[StoneQualityMst] ([ID], [Quality]) VALUES (1, N'VVS1')
INSERT [dbo].[StoneQualityMst] ([ID], [Quality]) VALUES (2, N'VVS2')
INSERT [dbo].[StoneQualityMst] ([ID], [Quality]) VALUES (3, N'VS1')
INSERT [dbo].[StoneQualityMst] ([ID], [Quality]) VALUES (4, N'VS2')
INSERT [dbo].[StoneQualityMst] ([ID], [Quality]) VALUES (5, N'SI1')
INSERT [dbo].[StoneQualityMst] ([ID], [Quality]) VALUES (6, N'SI2')
INSERT [dbo].[StoneQualityMst] ([ID], [Quality]) VALUES (7, N'I1')
INSERT [dbo].[StoneQualityMst] ([ID], [Quality]) VALUES (8, N'I2')
SET IDENTITY_INSERT [dbo].[StoneQualityMst] OFF
SET IDENTITY_INSERT [dbo].[StoneTypeMst] ON 

INSERT [dbo].[StoneTypeMst] ([ID], [Name]) VALUES (1, N'Diamond')
INSERT [dbo].[StoneTypeMst] ([ID], [Name]) VALUES (2, N'Ruby')
INSERT [dbo].[StoneTypeMst] ([ID], [Name]) VALUES (3, N'Sapphire')
INSERT [dbo].[StoneTypeMst] ([ID], [Name]) VALUES (4, N'Emerald')
INSERT [dbo].[StoneTypeMst] ([ID], [Name]) VALUES (5, N'Amethyst')
SET IDENTITY_INSERT [dbo].[StoneTypeMst] OFF
SET IDENTITY_INSERT [dbo].[UserMst] ON 

INSERT [dbo].[UserMst] ([ID], [Username], [Password], [FullName], [Address], [City], [Country], [Phone], [Email], [Birthday]) VALUES (1, N'user1', N'202cb962ac59075b964b07152d234b70', N'Donald A Milligan', N'3420  Cardinal Lane', N'Cleveland', N'USA', N'216-948-8431', N'cig3mpt325g@temporary-mail.net', CAST(N'1998-01-15' AS Date))
INSERT [dbo].[UserMst] ([ID], [Username], [Password], [FullName], [Address], [City], [Country], [Phone], [Email], [Birthday]) VALUES (2, N'mytilene', N'202cb962ac59075b964b07152d234b70', N'	Margaret S Mitchell', N'3079  Cambridge Drive', N'LONG VALLEY', N'USA', N'623-533-3119', N'htcl25sqoqw@temporary-mail.net', CAST(N'1966-08-17' AS Date))
INSERT [dbo].[UserMst] ([ID], [Username], [Password], [FullName], [Address], [City], [Country], [Phone], [Email], [Birthday]) VALUES (3, N'PhilLande9', N'202cb962ac59075b964b07152d234b70', N'	Linda D Oneal', N'4635  Hannah Street', N'Hickory', N'USA', N'828-314-5249', N'89radq0qxic@temporary-mail.net', CAST(N'1972-08-22' AS Date))
INSERT [dbo].[UserMst] ([ID], [Username], [Password], [FullName], [Address], [City], [Country], [Phone], [Email], [Birthday]) VALUES (4, N'corri', N'202cb962ac59075b964b07152d234b70', N'	Krystal J Cooper', N'4166  Stadium Drive', N'Framingham', N'USA', N'907-633-8724', N'qbobn0wubpi@temporary-mail.net', CAST(N'1999-06-25' AS Date))
INSERT [dbo].[UserMst] ([ID], [Username], [Password], [FullName], [Address], [City], [Country], [Phone], [Email], [Birthday]) VALUES (5, N'eric29965', N'202cb962ac59075b964b07152d234b70', N'Ruth G Gray', N'1250  Melody Lane', N'Hopewell', N'USA', N'804-452-5465', N'hlnats8fh1j@temporary-mail.net', CAST(N'1993-04-07' AS Date))
SET IDENTITY_INSERT [dbo].[UserMst] OFF
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AdminMst]    Script Date: 10/12/2020 9:28:22 PM ******/
ALTER TABLE [dbo].[AdminMst] ADD  CONSTRAINT [IX_AdminMst] UNIQUE NONCLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_ItemMst]    Script Date: 10/12/2020 9:28:22 PM ******/
ALTER TABLE [dbo].[ItemMst] ADD  CONSTRAINT [IX_ItemMst] UNIQUE NONCLUSTERED 
(
	[StyleCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_UserMst]    Script Date: 10/12/2020 9:28:22 PM ******/
ALTER TABLE [dbo].[UserMst] ADD  CONSTRAINT [IX_UserMst] UNIQUE NONCLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[DiscountMst]  WITH CHECK ADD  CONSTRAINT [FK_Discount_ItemMst] FOREIGN KEY([Item_ID])
REFERENCES [dbo].[ItemMst] ([ID])
GO
ALTER TABLE [dbo].[DiscountMst] CHECK CONSTRAINT [FK_Discount_ItemMst]
GO
ALTER TABLE [dbo].[ItemMst]  WITH CHECK ADD  CONSTRAINT [FK_ItemMst_BrandMst] FOREIGN KEY([Brand_ID])
REFERENCES [dbo].[BrandMst] ([ID])
GO
ALTER TABLE [dbo].[ItemMst] CHECK CONSTRAINT [FK_ItemMst_BrandMst]
GO
ALTER TABLE [dbo].[ItemMst]  WITH CHECK ADD  CONSTRAINT [FK_ItemMst_CategoryMst] FOREIGN KEY([Category_ID])
REFERENCES [dbo].[CategoryMst] ([ID])
GO
ALTER TABLE [dbo].[ItemMst] CHECK CONSTRAINT [FK_ItemMst_CategoryMst]
GO
ALTER TABLE [dbo].[ItemMst]  WITH CHECK ADD  CONSTRAINT [FK_ItemMst_CertificateMst] FOREIGN KEY([Certificate_ID])
REFERENCES [dbo].[CertificateMst] ([ID])
GO
ALTER TABLE [dbo].[ItemMst] CHECK CONSTRAINT [FK_ItemMst_CertificateMst]
GO
ALTER TABLE [dbo].[OrderDetailMst]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetailMst_ItemMst] FOREIGN KEY([Item_ID])
REFERENCES [dbo].[ItemMst] ([ID])
GO
ALTER TABLE [dbo].[OrderDetailMst] CHECK CONSTRAINT [FK_OrderDetailMst_ItemMst]
GO
ALTER TABLE [dbo].[OrderDetailMst]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetailMst_OrderMst] FOREIGN KEY([Order_ID])
REFERENCES [dbo].[OrderMst] ([ID])
GO
ALTER TABLE [dbo].[OrderDetailMst] CHECK CONSTRAINT [FK_OrderDetailMst_OrderMst]
GO
ALTER TABLE [dbo].[OrderMst]  WITH CHECK ADD  CONSTRAINT [FK_OrderMst_UserMst] FOREIGN KEY([User_ID])
REFERENCES [dbo].[UserMst] ([ID])
GO
ALTER TABLE [dbo].[OrderMst] CHECK CONSTRAINT [FK_OrderMst_UserMst]
GO
ALTER TABLE [dbo].[StoneMst]  WITH CHECK ADD  CONSTRAINT [FK_StoneMst_ItemMst] FOREIGN KEY([Item_ID])
REFERENCES [dbo].[ItemMst] ([ID])
GO
ALTER TABLE [dbo].[StoneMst] CHECK CONSTRAINT [FK_StoneMst_ItemMst]
GO
ALTER TABLE [dbo].[StoneMst]  WITH CHECK ADD  CONSTRAINT [FK_StoneMst_StoneQualityMst] FOREIGN KEY([StoneQuality_ID])
REFERENCES [dbo].[StoneQualityMst] ([ID])
GO
ALTER TABLE [dbo].[StoneMst] CHECK CONSTRAINT [FK_StoneMst_StoneQualityMst]
GO
ALTER TABLE [dbo].[StoneMst]  WITH CHECK ADD  CONSTRAINT [FK_StoneMst_StoneTypeMst] FOREIGN KEY([StoneType_ID])
REFERENCES [dbo].[StoneTypeMst] ([ID])
GO
ALTER TABLE [dbo].[StoneMst] CHECK CONSTRAINT [FK_StoneMst_StoneTypeMst]
GO
