USE [DB_Sahithyolsav]
GO
/****** Object:  StoredProcedure [dbo].[spGetIndiVidualMarks]    Script Date: 09/04/2014 21:31:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER proc [dbo].[spGetIndiVidualMarks]
	@param1 int,
	@param2 int,
	@param3 int,
	@param4 int,
	@param5 int
As

--SELECT *
--FROM 
--(
--SELECT vchPartcipantName 'Name',SUM(ISNULL(intPointToParticipant,0)) 'POINT',vchChessNo 'Chess No'
--FROM tbl_ParticipantList
--INNER JOIN tbl_Participant
--	ON tbl_Participant.intParticipantId = tbl_ParticipantList.intParticipantId
--INNER JOIN tbl_ItemList
--	ON tbl_ItemList.intParticipantListId = tbl_ParticipantList.intParticipantListId
--INNER JOIN tbl_Item
--	ON tbl_Item.intItemId = tbl_ItemList.intItemId
--INNER JOIN tbl_Section
--	ON tbl_ParticipantList.intSectionId = tbl_Section.intSectionID
--LEFT JOIN tbl_CodeLetterMap
--	ON tbl_ParticipantList.intParticipantListId = tbl_CodeLetterMap.intParticipatListId
--	AND tbl_Item.intItemId = tbl_CodeLetterMap.intItemId
--LEFT JOIN tbl_Tabulation
--	ON tbl_ParticipantList.intParticipantListId = tbl_Tabulation.intParticipatListId
--	AND tbl_Item.intItemId = tbl_Tabulation.intItemId
--WHERE intParticipantToLevelId = @param3 and ISNULL(tbl_ItemList.vchStatus,'No')='Yes'
--and tbl_Section.intSectionID=@param1
--GROUP BY vchPartcipantName,vchChessNo
--)A
--ORDER BY A.POINT DESC
SELECT * FROM
(
SELECT vchPartcipantName 'Name',tbl_ParticipantList.vchChessNo 'Chess No',tbl_District.vchDistrictName 'Team',
 --B.intParticipatListId,
 SUM(B.Mark) 'Mark' FROM
(

SELECT intParticipatListId,SUM(intPointToParticipant) 'Mark' FROM tbl_Tabulation
INNER JOIN tbl_Item ON tbl_Item.intItemId=tbl_Tabulation.intItemId
WHERE tbl_Item.isGroupItem=0
GROUP BY intParticipatListId

UNION
SELECT A.intParticipatListId,
  CASE WHEN A.Rank_Order=1 THEN
			SUM(A.intMarkForFirstPlace)
	   WHEN A.Rank_Order=2 THEN
			SUM(A.intMarkForSecondPlace)
	   WHEN A.Rank_Order=3 THEN
			SUM(A.intMarkForThirdPlace)
  ELSE
  0
  END AS Mark
FROM
(
SELECT 
	 DENSE_RANK() OVER (PARTITION BY tbl_Tabulation.intItemId ORDER BY tbl_Tabulation.numMarks DESC) Rank_Order,
	 intMarkForFirstPlace,intMarkForSecondPlace,intMarkForThirdPlace,
	intParticipatListId,tbl_Item.intItemId,intPointToParticipant FROM tbl_Tabulation
	INNER JOIN tbl_Item ON tbl_Item.intItemId=tbl_Tabulation.intItemId
	WHERE tbl_Item.isGroupItem=0
	
)A
GROUP BY A.intParticipatListId,A.Rank_Order

)B
INNER JOIN tbl_GroupSection ON B.intParticipatListId=tbl_GroupSection.intParticipantListID
INNER JOIN tbl_ParticipantList ON B.intParticipatListId=tbl_ParticipantList.intParticipantListId
INNER JOIN tbl_Participant ON tbl_ParticipantList.intParticipantId=tbl_Participant.intParticipantId
INNER JOIN tbl_District ON tbl_ParticipantList.intParticipantLevelId=tbl_District.intDistrictID
WHERE tbl_GroupSection.intSectionID=@param1
GROUP BY B.intParticipatListId,vchPartcipantName,tbl_ParticipantList.vchChessNo ,tbl_District.vchDistrictName 
)C
ORDER BY C.Mark DESC
