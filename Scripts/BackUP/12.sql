USE [DB_Sahithyolsav]
GO
/****** Object:  StoredProcedure [dbo].[spGetIndiVidualMarks]    Script Date: 08/16/2014 19:57:59 ******/
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

SELECT *
FROM 
(
SELECT vchPartcipantName 'Name',SUM(ISNULL(intPointToParticipant,0)) 'POINT',vchChessNo 'Chess No'
FROM tbl_ParticipantList
INNER JOIN tbl_Participant
	ON tbl_Participant.intParticipantId = tbl_ParticipantList.intParticipantId
INNER JOIN tbl_ItemList
	ON tbl_ItemList.intParticipantListId = tbl_ParticipantList.intParticipantListId
INNER JOIN tbl_Item
	ON tbl_Item.intItemId = tbl_ItemList.intItemId
INNER JOIN tbl_Section
	ON tbl_ParticipantList.intSectionId = tbl_Section.intSectionID
LEFT JOIN tbl_CodeLetterMap
	ON tbl_ParticipantList.intParticipantListId = tbl_CodeLetterMap.intParticipatListId
	AND tbl_Item.intItemId = tbl_CodeLetterMap.intItemId
LEFT JOIN tbl_Tabulation
	ON tbl_ParticipantList.intParticipantListId = tbl_Tabulation.intParticipatListId
	AND tbl_Item.intItemId = tbl_Tabulation.intItemId
WHERE intParticipantToLevelId = @param3 and ISNULL(tbl_ItemList.vchStatus,'No')='Yes'
GROUP BY vchPartcipantName,vchChessNo
)A
ORDER BY A.POINT DESC
