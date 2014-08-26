USE [DB_Sahithyolsav]
GO

/****** Object:  StoredProcedure [dbo].[spSaveStage]    Script Date: 08/27/2014 01:27:13 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


Create proc [dbo].[spSaveStage]
	@intStageId int,
	@VchStageName varchar(50),
	@vchPlace varchar(50)
As

INSERT INTO [DB_Sahithyolsav].[dbo].[tbl_Stage]
           ([VchStageName]
           ,[vchPlace])
     VALUES
           (@VchStageName,@vchPlace)

GO


