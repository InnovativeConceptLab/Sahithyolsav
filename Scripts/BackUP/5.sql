USE [DB_Sahithyolsav]
GO
/****** Object:  StoredProcedure [dbo].[spSaveParticipantList]    Script Date: 08/15/2014 18:46:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER Proc [dbo].[spSaveParticipantList]
	@intParticipantListId int,
	@intParticipantId int,
	@vchChessNo varchar(50),
	@intSectionId int,
	@intItemListID int,
	@dtDateAdded date,
	@intadedUserId int,
	@intProgramLevelId int,
	@intParticipantLevelId int,
	@IsActive bit,
	@intParticipantToLevelId int,
	@intParticipantLevelTypeID int,
	@isGroupPaticipant bit,
	@intNumberOfPaticiapnt int,
	@OutParticipantListId int out
As
Declare @TempTbl Table(ID int)
If not exists(Select * from [tbl_ParticipantList] where intParticipantListId=@intParticipantListId)
 BEGIN
	INSERT INTO [tbl_ParticipantList]
			   ([intParticipantId]
			   ,[vchChessNo]
			   ,[intSectionId]
			   ,[intItemListID]
			   ,[dtDateAdded]
			   ,[intadedUserId]
			   ,[intProgramLevelId]
			   ,[intParticipantLevelId]
			   ,[IsActive],
			   [intParticipantToLevelId],[intParticipantLevelTypeID],[isGroupPaticipant],[intNumberOfPaticiapnt])
	     OUTPUT inserted.intParticipantListId INTO @TempTbl(ID)
		 VALUES
			   (
					@intParticipantId ,
					@vchChessNo,
					@intSectionId,
					@intItemListID,
					@dtDateAdded,
					@intadedUserId,
					@intProgramLevelId,
					@intParticipantLevelId,
					@IsActive,@intParticipantToLevelId,@intParticipantLevelTypeID,@isGroupPaticipant,@intNumberOfPaticiapnt
			   )
		 Select @OutParticipantListId=ID  from @TempTbl
 END
ELSE
 BEGIN
	UPDATE 	[tbl_ParticipantList]
	Set 
		       vchChessNo=@vchChessNo,
			   intSectionId=@intSectionId,
			   intItemListID=@intItemListID,
			   dtDateAdded=@dtDateAdded,
			   intadedUserId=@intadedUserId,
			   intProgramLevelId=@intProgramLevelId,
			   intParticipantLevelId=@intParticipantLevelId,
			   IsActive=@IsActive,intParticipantToLevelId=@intParticipantToLevelId,
			   intParticipantLevelTypeID=@intParticipantLevelTypeID
	 OUTPUT inserted.intParticipantListId INTO @TempTbl(ID)
	 WHERE intParticipantListId=@intParticipantListId
	Select @OutParticipantListId=ID  from @TempTbl
			   
 END


