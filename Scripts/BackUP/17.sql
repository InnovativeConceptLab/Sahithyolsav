
USE [DB_Sahithyolsav]
GO
DROP TABLE tbl_Schedule
/****** Object:  Table [dbo].[tbl_Schedule]    Script Date: 08/30/2014 23:41:29 ******/
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
	[vchTime1] [varchar](10) NULL,
 CONSTRAINT [PK_tbl_Schedule] PRIMARY KEY CLUSTERED 
(
	[intShceduleID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO



USE [DB_Sahithyolsav]
GO

/****** Object:  StoredProcedure [dbo].[spSaveSchedule]    Script Date: 08/27/2014 01:27:51 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE Proc [dbo].[spSaveSchedule]
	@intShceduleID int,
	@intSectionId int,
    @intItemId int,
    @intStageId int,
    @dtDate datetime,
    @vchTime varchar(10),
    @vchTime1 varchar(10),
    @IsAMPM varchar(10)
    
As
If not exists(Select * from [tbl_Schedule] where intShceduleID=@intShceduleID)
BEGIN
INSERT INTO [tbl_Schedule]
           ([intSectionId]
           ,[intItemId]
           ,[intStageId]
           ,[dtDate]
           ,[vchTime]
           ,[vchTime1]
           ,[IsAMPM])
     VALUES
           (
			@intSectionId,
			@intItemId,
			@intStageId,
			@dtDate,
			@vchTime,@vchTime1,
			@IsAMPM
           )
End
ELSE
BEGIN
	UPDATE [tbl_Schedule]
	SET
	   [intStageId]=@intStageId,
       [dtDate]=@dtDate,
       [vchTime]=@vchTime,vchTime1=@vchTime1,
       [IsAMPM]=@IsAMPM
    WHERE 
    intShceduleID=@intShceduleID
END



GO


