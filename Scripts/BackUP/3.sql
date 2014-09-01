USE [DB_Sahithyolsav]
GO
/****** Object:  StoredProcedure [dbo].[spSaveIteem]    Script Date: 08/15/2014 15:27:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


ALTER Proc [dbo].[spSaveIteem] 
	@intItemId int,
	@vchItemName varchar(50),
	@vchItemCode varchar(50),
    @intSectionId int,
    @isGroupItem bit,
    @intMaxNumberOfParticpant int,
    @intMarkForFirstPlace int,
    @intMarkForSecondPlace int,
    @intMarkForThirdPlace int
As
If not Exists( SELECT intSectionID FROM [tbl_Item] WHERE intItemId=@intItemId)
BEGIN
INSERT INTO [DB_Sahithyolsav].[dbo].[tbl_Item]
           ([vchItemName]
           ,[vchItemCode]
           ,[intSectionId]
           ,[isGroupItem]
           ,[intMaxNumberOfParticpant]
           ,[intMarkForFirstPlace]
           ,[intMarkForSecondPlace]
           ,[intMarkForThirdPlace])
     VALUES
           (
            @vchItemName,
            @vchItemCode,
            @intSectionId,
            @isGroupItem,
            @intMaxNumberOfParticpant,
            @intMarkForFirstPlace,
            @intMarkForSecondPlace,
            @intMarkForThirdPlace
            )
END
ELSE
BEGIN
	UPDATE tbl_Item
	SET
	 [vchItemName]=@vchItemName,
	 [vchItemCode]=@vchItemCode,
	 [intSectionId]=@intSectionId,
	 [isGroupItem]=@isGroupItem,
	 [intMaxNumberOfParticpant]=@intMaxNumberOfParticpant
	WHERE intSectionID=@intSectionId
END
