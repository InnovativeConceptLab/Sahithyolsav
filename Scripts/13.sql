USE [DB_Sahithyolsav]
GO

/****** Object:  Table [dbo].[tbl_Stage]    Script Date: 08/27/2014 01:25:23 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[tbl_Stage](
	[intStageId] [int] IDENTITY(1,1) NOT NULL,
	[VchStageName] [varchar](50) NULL,
	[vchPlace] [varchar](50) NULL,
 CONSTRAINT [PK_tbl_Stage] PRIMARY KEY CLUSTERED 
(
	[intStageId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


