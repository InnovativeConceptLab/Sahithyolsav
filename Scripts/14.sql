USE [DB_Sahithyolsav]
GO

/****** Object:  Table [dbo].[tbl_Schedule]    Script Date: 08/27/2014 01:26:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[tbl_Schedule](
	[intShceduleID] [int] IDENTITY(1,1) NOT NULL,
	[intSectionId] [int] NULL,
	[intItemId] [int] NULL,
	[intStageId] [int] NULL,
	[dtDate] [datetime] NULL,
	[vchTime] [varchar](10) NULL,
	[IsAMPM] [varchar](10) NULL,
 CONSTRAINT [PK_tbl_Schedule] PRIMARY KEY CLUSTERED 
(
	[intShceduleID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


