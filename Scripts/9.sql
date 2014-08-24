USE [DB_Sahithyolsav]
GO
/****** Object:  StoredProcedure [dbo].[spSaveTabulation]    Script Date: 08/15/2014 23:37:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER proc [dbo].[spSaveTabulation]
    @intTabulationId int,
	@intParticipatListId int,
    @intCodeLetterID int,
    @numMarks numeric(18,2),
    @dtEventDate datetime,
    @intItemId int,
    @intPointToParticipant int,
    @intPointToTeam int,
    @vchGrade varchar(10)
As
If not exists(Select * from [tbl_Tabulation] where intTabulationId=@intTabulationId)
BEGIN
	INSERT INTO [tbl_Tabulation]
			   ([intParticipatListId]
			   ,[intCodeLetterID]
			   ,[numMarks]
			   ,[dtEventDate]
			   ,[intItemId]
			   ,[intPointToParticipant]
			   ,[intPointToTeam]
			   ,[vchGrade])
		 VALUES
			   (
			    @intParticipatListId, 
			    @intCodeLetterID,
			    @numMarks,
			    @dtEventDate,
			    @intItemId,
			    @intPointToParticipant,
			    @intPointToTeam,
			    @vchGrade
                )
END
ELSE
BEGIN
	UPDATE [tbl_Tabulation]
	SET 
		intParticipatListId=@intParticipatListId, 
	    intCodeLetterID=@intCodeLetterID,
		numMarks=@numMarks,
	    dtEventDate=@dtEventDate,
	    intItemId=@intItemId,
	    intPointToParticipant=@intPointToParticipant,
	    intPointToTeam=@intPointToTeam,vchGrade=@vchGrade
	    
	where intTabulationId=@intTabulationId
END

