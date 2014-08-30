USE [DB_Sahithyolsav]
GO

/****** Object:  Table [dbo].[tbl_GroupSection]    Script Date: 08/30/2014 17:31:31 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tbl_GroupSection](
	[intGrpSectID] [int] IDENTITY(1,1) NOT NULL,
	[intParticipantListID] [int] NOT NULL,
	[intSectionID] [int] NOT NULL
) ON [PRIMARY]

GO