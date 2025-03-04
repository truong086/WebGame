USE [webGame]
GO
/****** Object:  Table [dbo].[congtycungcap]    Script Date: 9/19/2024 2:09:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[congtycungcap](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ten] [nvarchar](250) NULL,
	[diachi] [nvarchar](500) NULL,
	[email] [nvarchar](500) NULL,
	[ngaytao] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[noinhap]    Script Date: 9/19/2024 2:09:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[noinhap](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ten] [nvarchar](200) NULL,
	[ngaytao] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[orderDetais]    Script Date: 9/19/2024 2:09:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[orderDetais](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[order_id] [int] NULL,
	[sanpham_id] [int] NULL,
	[soluong] [int] NULL,
	[tongtien] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[orders]    Script Date: 9/19/2024 2:09:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[orders](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ten] [nvarchar](100) NULL,
	[statuc] [int] NULL,
	[ngaytao] [datetime] NULL,
	[users_id] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[phieunhap]    Script Date: 9/19/2024 2:09:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[phieunhap](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[sanpham_id] [int] NULL,
	[soluong] [int] NULL,
	[noinhap_id] [int] NULL,
	[nguoinhap_id] [int] NULL,
	[tongtien] [int] NULL,
	[ngaynhap] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[phieuxuat]    Script Date: 9/19/2024 2:09:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[phieuxuat](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[sanpham_id] [int] NULL,
	[soluong] [int] NULL,
	[congtycungcap_id] [int] NULL,
	[ghichu] [nvarchar](500) NULL,
	[nguoicap_id] [int] NULL,
	[tongtien] [int] NULL,
	[ngaycap] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[sanpham]    Script Date: 9/19/2024 2:09:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[sanpham](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ten] [nvarchar](50) NULL,
	[noidung] [nvarchar](500) NULL,
	[solanclick] [int] NULL,
	[gia] [int] NULL,
	[ngaytao] [datetime] NULL,
	[theloai_id] [int] NULL,
	[hinhanh] [nvarchar](500) NULL,
	[soluong] [int] NULL,
 CONSTRAINT [PK__sanpham__3213E83F268E74AC] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[sualoi]    Script Date: 9/19/2024 2:09:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[sualoi](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ten] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[takhoan]    Script Date: 9/19/2024 2:09:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[takhoan](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[tentaikhoan] [nvarchar](50) NULL,
	[matkhau] [nvarchar](50) NULL,
	[fullname] [nvarchar](50) NULL,
	[email] [nvarchar](200) NULL,
	[phanquyen] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[theloai]    Script Date: 9/19/2024 2:09:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[theloai](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ten] [nvarchar](50) NULL,
	[ngaytao] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[congtycungcap] ON 

INSERT [dbo].[congtycungcap] ([id], [ten], [diachi], [email], [ngaytao]) VALUES (1, N'ABCs', N'AABC', N'ABC', CAST(N'2024-02-24T13:00:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[congtycungcap] OFF
GO
SET IDENTITY_INSERT [dbo].[noinhap] ON 

INSERT [dbo].[noinhap] ([id], [ten], [ngaytao]) VALUES (4, N'ABCs', CAST(N'2024-02-24T18:31:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[noinhap] OFF
GO
SET IDENTITY_INSERT [dbo].[orderDetais] ON 

INSERT [dbo].[orderDetais] ([id], [order_id], [sanpham_id], [soluong], [tongtien]) VALUES (1, 1, 1, 10000, NULL)
INSERT [dbo].[orderDetais] ([id], [order_id], [sanpham_id], [soluong], [tongtien]) VALUES (2, 1, 1, 111, NULL)
INSERT [dbo].[orderDetais] ([id], [order_id], [sanpham_id], [soluong], [tongtien]) VALUES (3, 2, 6, 1, 4123123)
INSERT [dbo].[orderDetais] ([id], [order_id], [sanpham_id], [soluong], [tongtien]) VALUES (4, 3, 6, 1, 4123123)
INSERT [dbo].[orderDetais] ([id], [order_id], [sanpham_id], [soluong], [tongtien]) VALUES (5, 6, 1, 1, 1000000)
INSERT [dbo].[orderDetais] ([id], [order_id], [sanpham_id], [soluong], [tongtien]) VALUES (6, 7, 5, 1, 123124)
INSERT [dbo].[orderDetais] ([id], [order_id], [sanpham_id], [soluong], [tongtien]) VALUES (7, 9, 6, 1, 4123123)
INSERT [dbo].[orderDetais] ([id], [order_id], [sanpham_id], [soluong], [tongtien]) VALUES (8, 12, 6, 1, 4123123)
INSERT [dbo].[orderDetais] ([id], [order_id], [sanpham_id], [soluong], [tongtien]) VALUES (9, 14, 7, 1, 1000)
INSERT [dbo].[orderDetais] ([id], [order_id], [sanpham_id], [soluong], [tongtien]) VALUES (10, 15, 7, 1, 1000)
INSERT [dbo].[orderDetais] ([id], [order_id], [sanpham_id], [soluong], [tongtien]) VALUES (11, 17, 6, 1, 4123123)
INSERT [dbo].[orderDetais] ([id], [order_id], [sanpham_id], [soluong], [tongtien]) VALUES (12, 18, 4, 1, 5124123)
INSERT [dbo].[orderDetais] ([id], [order_id], [sanpham_id], [soluong], [tongtien]) VALUES (13, 19, 8, 1, 11100)
INSERT [dbo].[orderDetais] ([id], [order_id], [sanpham_id], [soluong], [tongtien]) VALUES (14, 20, 8, 1, 11100)
INSERT [dbo].[orderDetais] ([id], [order_id], [sanpham_id], [soluong], [tongtien]) VALUES (15, 21, 1, 1, 10000000)
INSERT [dbo].[orderDetais] ([id], [order_id], [sanpham_id], [soluong], [tongtien]) VALUES (16, 21, 8, 1, 11100)
INSERT [dbo].[orderDetais] ([id], [order_id], [sanpham_id], [soluong], [tongtien]) VALUES (17, 21, 4, 1, 5124123)
INSERT [dbo].[orderDetais] ([id], [order_id], [sanpham_id], [soluong], [tongtien]) VALUES (18, 22, 6, 1, 4123123)
INSERT [dbo].[orderDetais] ([id], [order_id], [sanpham_id], [soluong], [tongtien]) VALUES (19, 23, 6, 1, 4123123)
INSERT [dbo].[orderDetais] ([id], [order_id], [sanpham_id], [soluong], [tongtien]) VALUES (20, 24, 6, 1, 4123123)
INSERT [dbo].[orderDetais] ([id], [order_id], [sanpham_id], [soluong], [tongtien]) VALUES (21, 24, 1, 1, 10000000)
INSERT [dbo].[orderDetais] ([id], [order_id], [sanpham_id], [soluong], [tongtien]) VALUES (22, 25, 6, 1, 4123123)
INSERT [dbo].[orderDetais] ([id], [order_id], [sanpham_id], [soluong], [tongtien]) VALUES (23, 26, 7, 1, 1000)
INSERT [dbo].[orderDetais] ([id], [order_id], [sanpham_id], [soluong], [tongtien]) VALUES (24, 27, 5, 2, 246248)
INSERT [dbo].[orderDetais] ([id], [order_id], [sanpham_id], [soluong], [tongtien]) VALUES (25, 27, 6, 1, 4123123)
INSERT [dbo].[orderDetais] ([id], [order_id], [sanpham_id], [soluong], [tongtien]) VALUES (26, 27, 1, 1, 10000000)
INSERT [dbo].[orderDetais] ([id], [order_id], [sanpham_id], [soluong], [tongtien]) VALUES (27, 28, 8, 1, 11100)
SET IDENTITY_INSERT [dbo].[orderDetais] OFF
GO
SET IDENTITY_INSERT [dbo].[orders] ON 

INSERT [dbo].[orders] ([id], [ten], [statuc], [ngaytao], [users_id]) VALUES (1, N'Đơn hàng 1', 1, CAST(N'2023-05-02T22:53:00.000' AS DateTime), NULL)
INSERT [dbo].[orders] ([id], [ten], [statuc], [ngaytao], [users_id]) VALUES (2, N'Don hang - 20230507141207', 1, CAST(N'2023-05-07T14:12:07.687' AS DateTime), NULL)
INSERT [dbo].[orders] ([id], [ten], [statuc], [ngaytao], [users_id]) VALUES (3, N'Don hang - 20230507141338', 1, CAST(N'2023-05-07T14:13:38.653' AS DateTime), NULL)
INSERT [dbo].[orders] ([id], [ten], [statuc], [ngaytao], [users_id]) VALUES (6, N'Don hang - 20230508155130', 1, CAST(N'2023-05-08T15:51:30.673' AS DateTime), 1)
INSERT [dbo].[orders] ([id], [ten], [statuc], [ngaytao], [users_id]) VALUES (7, N'Don hang - 20230508180202', 1, CAST(N'2023-05-08T18:02:02.690' AS DateTime), 1)
INSERT [dbo].[orders] ([id], [ten], [statuc], [ngaytao], [users_id]) VALUES (8, N'Don hang - 20230531133401', 1, CAST(N'2023-05-31T13:34:01.143' AS DateTime), 1)
INSERT [dbo].[orders] ([id], [ten], [statuc], [ngaytao], [users_id]) VALUES (9, N'Don hang - 20230531133438', 1, CAST(N'2023-05-31T13:34:38.190' AS DateTime), 1)
INSERT [dbo].[orders] ([id], [ten], [statuc], [ngaytao], [users_id]) VALUES (10, N'Don hang - 20230531134135', 1, CAST(N'2023-05-31T13:41:35.603' AS DateTime), 1)
INSERT [dbo].[orders] ([id], [ten], [statuc], [ngaytao], [users_id]) VALUES (11, N'Don hang - 20230531134202', 1, CAST(N'2023-05-31T13:42:02.447' AS DateTime), 1)
INSERT [dbo].[orders] ([id], [ten], [statuc], [ngaytao], [users_id]) VALUES (12, N'Don hang - 20230531134224', 1, CAST(N'2023-05-31T13:42:24.767' AS DateTime), 1)
INSERT [dbo].[orders] ([id], [ten], [statuc], [ngaytao], [users_id]) VALUES (13, N'Don hang - 20230531134412', 1, CAST(N'2023-05-31T13:44:12.307' AS DateTime), 1)
INSERT [dbo].[orders] ([id], [ten], [statuc], [ngaytao], [users_id]) VALUES (14, N'Don hang - 20230531134452', 1, CAST(N'2023-05-31T13:44:52.817' AS DateTime), 1)
INSERT [dbo].[orders] ([id], [ten], [statuc], [ngaytao], [users_id]) VALUES (15, N'Don hang - 20230531134604', 1, CAST(N'2023-05-31T13:46:04.823' AS DateTime), 1)
INSERT [dbo].[orders] ([id], [ten], [statuc], [ngaytao], [users_id]) VALUES (16, N'Don hang - 20230531134616', 1, CAST(N'2023-05-31T13:46:16.763' AS DateTime), 1)
INSERT [dbo].[orders] ([id], [ten], [statuc], [ngaytao], [users_id]) VALUES (17, N'Don hang - 20230628202842', 1, CAST(N'2023-06-28T20:28:42.763' AS DateTime), 1)
INSERT [dbo].[orders] ([id], [ten], [statuc], [ngaytao], [users_id]) VALUES (18, N'Don hang - 20231208220259', 1, CAST(N'2023-12-08T22:02:59.300' AS DateTime), 1)
INSERT [dbo].[orders] ([id], [ten], [statuc], [ngaytao], [users_id]) VALUES (19, N'Don hang - 20240307162223', 1, CAST(N'2024-03-07T16:22:23.130' AS DateTime), 2)
INSERT [dbo].[orders] ([id], [ten], [statuc], [ngaytao], [users_id]) VALUES (20, N'Don hang - 20240326181324', 1, CAST(N'2024-03-26T18:13:24.497' AS DateTime), 2)
INSERT [dbo].[orders] ([id], [ten], [statuc], [ngaytao], [users_id]) VALUES (21, N'Don hang - 20240402211505', 1, CAST(N'2024-04-02T21:15:05.457' AS DateTime), 2)
INSERT [dbo].[orders] ([id], [ten], [statuc], [ngaytao], [users_id]) VALUES (22, N'Don hang - 20240511195533', 1, CAST(N'2024-05-11T19:55:33.893' AS DateTime), 2)
INSERT [dbo].[orders] ([id], [ten], [statuc], [ngaytao], [users_id]) VALUES (23, N'Don hang - 20240522220335', 1, CAST(N'2024-05-22T22:03:35.097' AS DateTime), 2)
INSERT [dbo].[orders] ([id], [ten], [statuc], [ngaytao], [users_id]) VALUES (24, N'Don hang - 20240522220438', 1, CAST(N'2024-05-22T22:04:38.573' AS DateTime), 2)
INSERT [dbo].[orders] ([id], [ten], [statuc], [ngaytao], [users_id]) VALUES (25, N'Don hang - 20240524130225', 1, CAST(N'2024-05-24T13:02:25.737' AS DateTime), 2)
INSERT [dbo].[orders] ([id], [ten], [statuc], [ngaytao], [users_id]) VALUES (26, N'Don hang - 20240525150328', 1, CAST(N'2024-05-25T15:03:28.270' AS DateTime), 2)
INSERT [dbo].[orders] ([id], [ten], [statuc], [ngaytao], [users_id]) VALUES (27, N'Don hang - 20240608103802', 1, CAST(N'2024-06-08T10:38:02.760' AS DateTime), 2)
INSERT [dbo].[orders] ([id], [ten], [statuc], [ngaytao], [users_id]) VALUES (28, N'Don hang - 20240610131938', 1, CAST(N'2024-06-10T13:19:38.167' AS DateTime), 2)
SET IDENTITY_INSERT [dbo].[orders] OFF
GO
SET IDENTITY_INSERT [dbo].[phieunhap] ON 

INSERT [dbo].[phieunhap] ([id], [sanpham_id], [soluong], [noinhap_id], [nguoinhap_id], [tongtien], [ngaynhap]) VALUES (1, 2, 123, 4, NULL, 123, NULL)
INSERT [dbo].[phieunhap] ([id], [sanpham_id], [soluong], [noinhap_id], [nguoinhap_id], [tongtien], [ngaynhap]) VALUES (2, 1, 123, 4, 2, 123, NULL)
SET IDENTITY_INSERT [dbo].[phieunhap] OFF
GO
SET IDENTITY_INSERT [dbo].[phieuxuat] ON 

INSERT [dbo].[phieuxuat] ([id], [sanpham_id], [soluong], [congtycungcap_id], [ghichu], [nguoicap_id], [tongtien], [ngaycap]) VALUES (1, 6, 123345, 1, N'ABC', 2, 123777, CAST(N'2024-02-20T01:29:00.000' AS DateTime))
INSERT [dbo].[phieuxuat] ([id], [sanpham_id], [soluong], [congtycungcap_id], [ghichu], [nguoicap_id], [tongtien], [ngaycap]) VALUES (2, 1, 123, 1, N'ABC', 2, 123, CAST(N'2024-02-26T03:35:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[phieuxuat] OFF
GO
SET IDENTITY_INSERT [dbo].[sanpham] ON 

INSERT [dbo].[sanpham] ([id], [ten], [noidung], [solanclick], [gia], [ngaytao], [theloai_id], [hinhanh], [soluong]) VALUES (1, N'Best Xbox Racing Games 2024', N'Racing games may not often make it onto games of the year lists or feature prominently at rewards ceremonies, but it''s still a competitive video games genre packed with excellent titles and driven by a passionate community of car lovers', 118, 10000000, CAST(N'2023-05-02T22:44:00.000' AS DateTime), 1, N'https://tse4.mm.bing.net/th?id=OIP.i9d4zapfV5z8qcU4FGbKUgHaHa&pid=Api&P=0&h=220', NULL)
INSERT [dbo].[sanpham] ([id], [ten], [noidung], [solanclick], [gia], [ngaytao], [theloai_id], [hinhanh], [soluong]) VALUES (2, N'Game Mobile', N'Racing games may not often make it onto games of the year lists or feature prominently at rewards ceremonies, but it''s still a competitive video games genre packed with excellent titles and driven by a passionate community of car lovers', 10006, 10000, CAST(N'2023-05-04T22:23:00.000' AS DateTime), 2, N'https://i.ytimg.com/vi/-i0OIy5zSrs/maxresdefault.jpg', NULL)
INSERT [dbo].[sanpham] ([id], [ten], [noidung], [solanclick], [gia], [ngaytao], [theloai_id], [hinhanh], [soluong]) VALUES (3, N'Best story games of all time 2023', N'The 25 Best PC Games to Play Right Now IGN Best story games on PC 2023 | PCGamesN The Top 100 Video Games of All Time IGN 25 Best PS4 Games to Play Right Now IGN The 25 Best PC Games to Play Right Now IGN The Best iPhone Games for 2023 | PCMag Best story games on PC 2023 |', 1112, 1234, CAST(N'2023-05-04T23:50:00.000' AS DateTime), 3, N'https://assets-prd.ignimgs.com/2022/09/23/top25modernpcgames-slideshow-1663951022529.jpg', NULL)
INSERT [dbo].[sanpham] ([id], [ten], [noidung], [solanclick], [gia], [ngaytao], [theloai_id], [hinhanh], [soluong]) VALUES (4, N'Five Ways Artificial Intelligence ', N'Corporates internationally have been taking optimistic steps through artificial intelligence to make generation and workspaces extra reachable for the particularly abled. As part of their comprehensive coverage, tech giants are now constructing AI-powered', 1239, 5124123, CAST(N'2023-05-04T23:51:00.000' AS DateTime), 3, N'https://ichef.bbci.co.uk/news/976/cpsprodpb/13729/production/_112375697_1331db7a-17c0-4401-8cac-6a2309ff49b6.jpg', NULL)
INSERT [dbo].[sanpham] ([id], [ten], [noidung], [solanclick], [gia], [ngaytao], [theloai_id], [hinhanh], [soluong]) VALUES (5, N'The Game Awards 2023', N'Geoff Keighley has stated that he’ll hold a live streamed announcement ceremony on Monday, November 13, 2023, during which all nominees for The Game Awards 2023 will be revealed.', 1234568, 123124, CAST(N'2023-05-04T23:53:00.000' AS DateTime), 2, N'https://tse3.mm.bing.net/th?id=OIP.KnOMRHA1QxJSjBUa6gub_AHaEC&pid=Api&P=0&h=220', NULL)
INSERT [dbo].[sanpham] ([id], [ten], [noidung], [solanclick], [gia], [ngaytao], [theloai_id], [hinhanh], [soluong]) VALUES (6, N'Video Game Development Studios', N'The world at large may never be the same after COVID-19, as millions of souls have been lost to the virus and the very way that the world interacts with one another has been altered. Gaming is no different', 149, 4123123, CAST(N'2023-05-04T23:54:00.000' AS DateTime), 1, N'https://static0.gamerantimages.com/wordpress/wp-content/uploads/2021/10/game-development-1.jpg', NULL)
INSERT [dbo].[sanpham] ([id], [ten], [noidung], [solanclick], [gia], [ngaytao], [theloai_id], [hinhanh], [soluong]) VALUES (7, N'Assassin’s Creed Mirage ', N'The ICS-translate team includes many dedicated gamers, and game localisation is one of our core services so it’s little surprise that the language choices made in Ubisoft''s highly anticipated release, Assassin''s Creed Mirage has been a major focus of office discussion.', 12141, 1000, CAST(N'2023-05-05T07:38:00.000' AS DateTime), 5, N'https://assets-global.website-files.com/63e64e21f7433282c5043d27/652037ff29628ab229f6bacc_ACM_art_Standard_Wild_20220910_10.20pm_CEST_Paris-Time-HD-scaled.jpg', NULL)
INSERT [dbo].[sanpham] ([id], [ten], [noidung], [solanclick], [gia], [ngaytao], [theloai_id], [hinhanh], [soluong]) VALUES (8, N'3D Video Game Making Websites', N'3D video game making websites. View the play mode control scheme (we now support azerty too) version check You can create games directly on this site without installing anything or learning a programming language. There is no programming or scripting needed.', 135, 11100, CAST(N'2023-05-05T07:41:00.000' AS DateTime), 5, N'https://i.pinimg.com/originals/93/d5/03/93d5038433e45e508bda4df824944070.jpg', NULL)
SET IDENTITY_INSERT [dbo].[sanpham] OFF
GO
SET IDENTITY_INSERT [dbo].[takhoan] ON 

INSERT [dbo].[takhoan] ([id], [tentaikhoan], [matkhau], [fullname], [email], [phanquyen]) VALUES (1, N'A123', N'123s', N'A', N'a@gmail.com', 1)
INSERT [dbo].[takhoan] ([id], [tentaikhoan], [matkhau], [fullname], [email], [phanquyen]) VALUES (2, N'ABC', N'202cb962ac59075b964b07152d234b70', N'A', N'a', 1)
INSERT [dbo].[takhoan] ([id], [tentaikhoan], [matkhau], [fullname], [email], [phanquyen]) VALUES (10, N'ABCs', N'202cb962ac59075b964b07152d234b70', N'qưeqwe', N'dưew', 1)
SET IDENTITY_INSERT [dbo].[takhoan] OFF
GO
SET IDENTITY_INSERT [dbo].[theloai] ON 

INSERT [dbo].[theloai] ([id], [ten], [ngaytao]) VALUES (1, N'Quần áo', CAST(N'2023-05-02T15:19:00.000' AS DateTime))
INSERT [dbo].[theloai] ([id], [ten], [ngaytao]) VALUES (2, N'Quần áo Nam', CAST(N'2023-05-02T22:26:00.000' AS DateTime))
INSERT [dbo].[theloai] ([id], [ten], [ngaytao]) VALUES (3, N'Quần áo nữ', CAST(N'2023-12-16T21:39:00.000' AS DateTime))
INSERT [dbo].[theloai] ([id], [ten], [ngaytao]) VALUES (4, N'Quần áo thể thao', CAST(N'2023-12-16T21:40:00.000' AS DateTime))
INSERT [dbo].[theloai] ([id], [ten], [ngaytao]) VALUES (5, N'Quần áo ABC', NULL)
SET IDENTITY_INSERT [dbo].[theloai] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__takhoan__8ADCB8A7C737903E]    Script Date: 9/19/2024 2:09:38 PM ******/
ALTER TABLE [dbo].[takhoan] ADD UNIQUE NONCLUSTERED 
(
	[tentaikhoan] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__takhoan__AB6E616473B1DB63]    Script Date: 9/19/2024 2:09:38 PM ******/
ALTER TABLE [dbo].[takhoan] ADD UNIQUE NONCLUSTERED 
(
	[email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[orderDetais]  WITH CHECK ADD FOREIGN KEY([order_id])
REFERENCES [dbo].[orders] ([id])
GO
ALTER TABLE [dbo].[orderDetais]  WITH CHECK ADD  CONSTRAINT [FK__orderDeta__sanph__3D5E1FD2] FOREIGN KEY([sanpham_id])
REFERENCES [dbo].[sanpham] ([id])
GO
ALTER TABLE [dbo].[orderDetais] CHECK CONSTRAINT [FK__orderDeta__sanph__3D5E1FD2]
GO
ALTER TABLE [dbo].[orders]  WITH CHECK ADD FOREIGN KEY([users_id])
REFERENCES [dbo].[takhoan] ([id])
GO
ALTER TABLE [dbo].[phieunhap]  WITH CHECK ADD FOREIGN KEY([nguoinhap_id])
REFERENCES [dbo].[takhoan] ([id])
GO
ALTER TABLE [dbo].[phieunhap]  WITH CHECK ADD FOREIGN KEY([noinhap_id])
REFERENCES [dbo].[noinhap] ([id])
GO
ALTER TABLE [dbo].[phieunhap]  WITH CHECK ADD  CONSTRAINT [FK__phieunhap__sanph__2CF2ADDF] FOREIGN KEY([sanpham_id])
REFERENCES [dbo].[sanpham] ([id])
GO
ALTER TABLE [dbo].[phieunhap] CHECK CONSTRAINT [FK__phieunhap__sanph__2CF2ADDF]
GO
ALTER TABLE [dbo].[phieuxuat]  WITH CHECK ADD FOREIGN KEY([congtycungcap_id])
REFERENCES [dbo].[congtycungcap] ([id])
GO
ALTER TABLE [dbo].[phieuxuat]  WITH CHECK ADD FOREIGN KEY([nguoicap_id])
REFERENCES [dbo].[takhoan] ([id])
GO
ALTER TABLE [dbo].[phieuxuat]  WITH CHECK ADD  CONSTRAINT [FK__phieuxuat__sanph__31B762FC] FOREIGN KEY([sanpham_id])
REFERENCES [dbo].[sanpham] ([id])
GO
ALTER TABLE [dbo].[phieuxuat] CHECK CONSTRAINT [FK__phieuxuat__sanph__31B762FC]
GO
ALTER TABLE [dbo].[sanpham]  WITH CHECK ADD  CONSTRAINT [FK__sanpham__theloai__47DBAE45] FOREIGN KEY([theloai_id])
REFERENCES [dbo].[theloai] ([id])
GO
ALTER TABLE [dbo].[sanpham] CHECK CONSTRAINT [FK__sanpham__theloai__47DBAE45]
GO
