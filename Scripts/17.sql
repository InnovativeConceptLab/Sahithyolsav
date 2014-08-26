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
           ,[IsAMPM])
     VALUES
           (
			@intSectionId,
			@intItemId,
			@intStageId,
			@dtDate,
			@vchTime,
			@IsAMPM
           )
End
ELSE
BEGIN
	UPDATE [tbl_Schedule]
	SET
	   [intStageId]=@intStageId,
       [dtDate]=@dtDate,
       [vchTime]=@vchTime,
       [IsAMPM]=@IsAMPM
    WHERE 
    intShceduleID=@intShceduleID
END



GO


