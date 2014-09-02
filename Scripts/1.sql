USE [DB_Sahithyolsav]
GO
/****** Object:  StoredProcedure [dbo].[spGetParticipantItemBySectionandItemId]    Script Date: 09/02/2014 23:50:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


ALTER proc [dbo].[spGetParticipantItemBySectionandItemId] ---10,29,12
	@SectionId int,
	@intItemId int,
	@intParticipantToLevelId int
As
SELECT * FROM
(
SELECT
	tbl_ItemList.intParticipantListId,
	tbl_Participant.intParticipantId,
	tbl_ParticipantList.intParticipantToLevelId,
	tbl_Item.intItemId,
	tbl_Section.intSectionID,
	ISNULL(tbl_CodeLetterMap.intCodeLetterID, 0) CodeLetterMapID,
	ISNULL(tbl_Tabulation.intTabulationId, 0) intTabulationId,
	vchPartcipantName,
	tbl_Section.vchSectionName,
	tbl_Item.vchItemName,
	vchChessNo,
	ISNULL(vchCodeLetter, '-') CodeLetter,
	ISNULL(tbl_Tabulation.numMarks, 0) Marks,
	CASE
		WHEN intProgramLevelId = 1 THEN (SELECT
				vchDistrictName + ' District'
			FROM tbl_District
			WHERE intDistrictID = intParticipantLevelId)

		WHEN intProgramLevelId = 2 THEN (SELECT
				vchDivisionName + ' Division'
			FROM tbl_Division
			WHERE intDivisionId = intParticipantLevelId)
		WHEN intProgramLevelId = 3 THEN (SELECT
				vchSectorName + ' Sector'
			FROM tbl_Sector
			WHERE intSectorId = intParticipantLevelId)
		WHEN intProgramLevelId = 4 THEN (SELECT
				vchUnitName + 'Unit'
			FROM tbl_Unit
			WHERE tbl_Unit.intUnitId = intParticipantLevelId)
		WHEN intProgramLevelId = 5 THEN ('Individual')
	END AS 'Participant From',
	DENSE_RANK() OVER (PARTITION BY tbl_ParticipantList.intParticipantLevelId ORDER BY tbl_ItemList.intParticipantListId DESC) Rank,
	tbl_ParticipantList.intParticipantLevelId
FROM tbl_ParticipantList
INNER JOIN tbl_Participant
	ON tbl_Participant.intParticipantId = tbl_ParticipantList.intParticipantId
INNER JOIN tbl_ItemList
	ON tbl_ItemList.intParticipantListId = tbl_ParticipantList.intParticipantListId
INNER JOIN tbl_Item
	ON tbl_Item.intItemId = tbl_ItemList.intItemId
--INNER JOIN tbl_Section
--	ON tbl_ParticipantList.intSectionId = tbl_Section.intSectionID
LEFT JOIN tbl_CodeLetterMap
	ON tbl_ParticipantList.intParticipantListId = tbl_CodeLetterMap.intParticipatListId
	AND tbl_Item.intItemId = tbl_CodeLetterMap.intItemId
LEFT JOIN tbl_Tabulation
	ON tbl_ParticipantList.intParticipantListId = tbl_Tabulation.intParticipatListId
	AND tbl_Item.intItemId = tbl_Tabulation.intItemId
INNER join tbl_GroupSection 
	ON tbl_ParticipantList.intParticipantListId=tbl_GroupSection.intParticipantListId
	INNER JOIN tbl_Section
	ON tbl_GroupSection.intSectionId = tbl_Section.intSectionID
WHERE tbl_Section.intSectionId = @SectionId
AND tbl_ItemList.intItemId = @intItemId
AND intParticipantToLevelId = @intParticipantToLevelId and ISNULL(tbl_ItemList.vchStatus,'No')='Yes'
)A
WHERE A.Rank=1
ORDER BY A.intParticipantLevelId