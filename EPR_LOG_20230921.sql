USE [EPR_LOG]
GO
/****** Object:  Table [dbo].[ApiLogs]    Script Date: 21.09.2023 10:05:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ApiLogs](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Detail] [nvarchar](max) NOT NULL,
	[Date] [datetime] NOT NULL,
	[Audit] [varchar](50) NOT NULL,
 CONSTRAINT [PK_ApiLogs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WebLogs]    Script Date: 21.09.2023 10:05:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WebLogs](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Detail] [nvarchar](max) NOT NULL,
	[Date] [datetime] NOT NULL,
	[Audit] [int] NOT NULL,
 CONSTRAINT [PK_UILogs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[ApiLogs] ADD  CONSTRAINT [DF_ApiLogs_Audit_1]  DEFAULT ('INFO') FOR [Audit]
GO
ALTER TABLE [dbo].[WebLogs] ADD  CONSTRAINT [DF_WebLogs_Audit]  DEFAULT ((0)) FOR [Audit]
GO
