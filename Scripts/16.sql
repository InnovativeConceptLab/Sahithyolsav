USE [DB_Sahithyolsav]
GO
/****** Object:  StoredProcedure [dbo].[spSaveParticipant]    Script Date: 08/27/2014 00:22:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER proc [dbo].[spSaveParticipant]
	 @intParticipantId int,
	 @vchPartcipantName varchar(100),
     @intUnitId int,
     @isActive bit,
     @vchImagePath varchar(50),
     @intAge int,
     @vchCampusName varchar(50),
     @vchCourse varchar(50),
     @ParticipantId int out
As
Declare @TempTbl Table(ID int)
If not exists(Select * from tbl_Participant where intParticipantId=@intParticipantId)
BEGIN
INSERT INTO [tbl_Participant]
           ([vchPartcipantName]
           ,[intUnitId]
           ,[isActive]
           ,[vchImagePath]
           ,[intAge],
           [vchCampusName],
          [vchCourse] )
     OUTPUT inserted.intParticipantId INTO @TempTbl(ID)
     VALUES
           (@vchPartcipantName,
           @intUnitId,
           @isActive,
           @vchImagePath,
           @intAge,
           @vchCampusName,
           @vchCourse)
      Select @ParticipantId=ID  from @TempTbl
End
ELSE
	BEGIN
		UPDATE  [tbl_Participant]
		SET 
		  vchPartcipantName= @vchPartcipantName,
           intUnitId= @intUnitId,
           isActive=isActive,
           vchImagePath=@vchImagePath,
           intAge=@intAge,
           vchCampusName=@vchCampusName,
           vchCourse=@vchCourse
         OUTPUT inserted.intParticipantId INTO @TempTbl(ID)
        WHERE intParticipantId=@intParticipantId
        
         Select @ParticipantId=ID  from @TempTbl
	END

