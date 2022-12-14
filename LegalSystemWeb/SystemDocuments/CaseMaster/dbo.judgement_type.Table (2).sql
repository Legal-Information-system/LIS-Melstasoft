USE [LIS_DB]
GO
/****** Object:  Table [dbo].[judgement_type]    Script Date: 12/16/2022 10:36:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[judgement_type](
	[judgement_type_id] [int] IDENTITY(1,1) NOT NULL,
	[judgement_type_name] [varchar](50) NOT NULL,
	[is_active] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[judgement_type_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[judgement_type] ON 

INSERT [dbo].[judgement_type] ([judgement_type_id], [judgement_type_name], [is_active]) VALUES (1, N'Favour of the Company', 1)
INSERT [dbo].[judgement_type] ([judgement_type_id], [judgement_type_name], [is_active]) VALUES (2, N'Favour of the Other Party', 1)
INSERT [dbo].[judgement_type] ([judgement_type_id], [judgement_type_name], [is_active]) VALUES (3, N'Other', 1)
INSERT [dbo].[judgement_type] ([judgement_type_id], [judgement_type_name], [is_active]) VALUES (4, N'Test 123', 0)
INSERT [dbo].[judgement_type] ([judgement_type_id], [judgement_type_name], [is_active]) VALUES (5, N'Test 12', 0)
INSERT [dbo].[judgement_type] ([judgement_type_id], [judgement_type_name], [is_active]) VALUES (6, N'Test 1', 0)
INSERT [dbo].[judgement_type] ([judgement_type_id], [judgement_type_name], [is_active]) VALUES (7, N'khvj', 1)
SET IDENTITY_INSERT [dbo].[judgement_type] OFF
GO
ALTER TABLE [dbo].[judgement_type] ADD  DEFAULT ((1)) FOR [is_active]
GO
