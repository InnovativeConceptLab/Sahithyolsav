USE [DB_Sahithyolsav]
GO
/****** Object:  StoredProcedure [dbo].[spSavveItemList]    Script Date: 08/30/2014 17:48:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[spSaveGroupSection]
		@intGrpSectID int,
	    @intParticipantListID int,
        @intSectionID int
       
As
If not exists(Select * from tbl_GroupSection where intGrpSectID=@intGrpSectID)
BEGIN
INSERT INTO [tbl_GroupSection]
           ([intParticipantListId]
           ,[intSectionID])
     VALUES
           (@intParticipantListId, 
           @intSectionID)
END
ELSE
BEGIN
	UPDATE [tbl_GroupSection]
	Set
		intSectionID=@intSectionID
		WHERE intGrpSectID=@intGrpSectID and [intParticipantListId]=@intParticipantListId 
    
END

