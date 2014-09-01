USE [DB_Sahithyolsav]
GO

/****** Object:  Table [dbo].[tbl_ParticipantGroupList]    Script Date: 08/15/2014 20:28:45 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[tbl_ParticipantGroupList](
	[intGroupParticipantId] [int] NOT NULL,
	[intParticipantListId] [int] NULL,
	[vchGroupParticipantName] [varchar](500) NULL,
 CONSTRAINT [PK_tbl_ParticipantGroupList] PRIMARY KEY CLUSTERED 
(
	[intGroupParticipantId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


