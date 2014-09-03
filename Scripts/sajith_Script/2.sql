USE [DB_Sahithyolsav]
GO
/****** Object:  StoredProcedure [dbo].[spSaveSchedule]    Script Date: 09/02/2014 20:15:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


ALTER Proc [dbo].[spSaveSchedule]
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
       [intItemId]=@intItemId,
       [intSectionId]=@intSectionId,
     
       [IsAMPM]=@IsAMPM
    WHERE 
    intShceduleID=@intShceduleID
END



