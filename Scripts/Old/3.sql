USE [DB_Sahithyolsav]
GO
/****** Object:  StoredProcedure [dbo].[spGetItemWiseReport]    Script Date: 09/03/2014 21:13:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER proc [dbo].[spGetItemWiseReport] --10,29,12
	@param1 int,
	@param2 int,
	@param3 int,
	@param4 int,
	@param5 int
As
--SELECT TOP 3
--	--DENSE_RANK() OVER (PARTITION BY tbl_ItemList.intItemId ORDER BY tbl_Tabulation.numMarks DESC) Rank_Order,
--	--intParticipantToLevelId,tbl_Section.intSectionId,tbl_ItemList.intItemId,
--	vchPartcipantName 'Name',
--	tbl_Section.vchSectionName 'Section',
--	tbl_Item.vchItemName 'Item',
--	CASE
--		WHEN intProgramLevelId = 1 THEN (SELECT
--				vchDistrictName + ' District'
--			FROM tbl_District
--			WHERE intDistrictID = intParticipantLevelId)

--		WHEN intProgramLevelId = 2 THEN (SELECT
--				vchDivisionName + ' Division'
--			FROM tbl_Division
--			WHERE intDivisionId = intParticipantLevelId)
--		WHEN intProgramLevelId = 3 THEN (SELECT
--				vchSectorName + ' Sector'
--			FROM tbl_Sector
--			WHERE intSectorId = intParticipantLevelId)
--		WHEN intProgramLevelId = 4 THEN (SELECT
--				vchUnitName + 'Unit'
--			FROM tbl_Unit
--			WHERE tbl_Unit.intUnitId = intParticipantLevelId)
--		WHEN intProgramLevelId = 5 THEN ('Individual')
--	END AS 'Team',
--	vchChessNo 'Chess No',
--	ISNULL(vchCodeLetter, '-') 'Code Letter',
--	--ISNULL(tbl_Tabulation.numMarks, 0) 'Mark',
--	ISNULL(tbl_Tabulation.intPointToParticipant, 0) +
--	(SELECT 
--		CASE WHEN DENSE_RANK() OVER (PARTITION BY tbl_ItemList.intItemId ORDER BY tbl_Tabulation.numMarks DESC)=1 THEN
--				intMarkForFirstPlace
--		     WHEN DENSE_RANK() OVER (PARTITION BY tbl_ItemList.intItemId ORDER BY tbl_Tabulation.numMarks DESC)=2 THEN
--				intMarkForSecondPlace
--		     WHEN DENSE_RANK() OVER (PARTITION BY tbl_ItemList.intItemId ORDER BY tbl_Tabulation.numMarks DESC)=3 THEN
--				intMarkForThirdPlace
--			End as Marks 
--	  FROM tbl_Item WHERE intItemId=@param2 ) 'Point'
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

--WHERE tbl_Section.intSectionId = @param1
--AND tbl_ItemList.intItemId = @param2
--AND intParticipantToLevelId = @param3 and ISNULL(tbl_ItemList.vchStatus,'No')='Yes'
--ORDER BY numMarks desc 


SELECT C.Name,C.Section,C.Item,C.Team,C.[Chess No],C.Mark,
CASE WHEN C.Mark > =70  THEN
		'A'
	WHEN 
		C.Mark > =60 AND C.Mark <=69 THEN
		'B'
	WHEN 
		C.Mark > =50 AND C.Mark <=59 THEN
		'C'
	ELSE
		'D'
	END AS 'Grade',
SUM(C.P1) 'POINT' FROM
(
SELECT *,
 CASE WHEN a.Rank_Order=1 THEN
		(SELECT intMarkForFirstPlace FROM tbl_Item WHERE intItemId=@param2)
      WHEN A.Rank_Order=2 THEN
	 	(SELECT intMarkForSecondPlace FROM tbl_Item WHERE intItemId=@param2)
      WHEN A.Rank_Order=3 THEN
		(SELECT intMarkForThirdPlace FROM tbl_Item WHERE intItemId=@param2)
 Else	
	0
End as P1
FROM
(
SELECT TOP 100
	DENSE_RANK() OVER (PARTITION BY tbl_ItemList.intItemId ORDER BY tbl_Tabulation.numMarks DESC) Rank_Order,
	vchPartcipantName 'Name',
	tbl_Section.vchSectionName 'Section',
	tbl_Item.vchItemName 'Item',
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
		WHEN intProgramLevelId = @param3 THEN ('Individual')
	END AS 'Team',
	vchChessNo 'Chess No',
	ISNULL(vchCodeLetter, '-') 'Code Letter',
	ISNULL(tbl_Tabulation.numMarks, 0) 'Mark'
	
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

WHERE tbl_Section.intSectionId = @param1
AND tbl_ItemList.intItemId = @param2
AND intParticipantToLevelId = @param3 and ISNULL(tbl_ItemList.vchStatus,'No')='Yes'
ORDER BY numMarks desc 
)A
WHERE ISNULL(A.Mark,0) > 0
UNION ALL

SELECT * FROM(
SELECT TOP 100
    DENSE_RANK() OVER (PARTITION BY tbl_ItemList.intItemId ORDER BY tbl_Tabulation.numMarks DESC) Rank_Order,
	vchPartcipantName 'Name',
	tbl_Section.vchSectionName 'Section',
	tbl_Item.vchItemName 'Item',
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
	END AS 'Team',
	vchChessNo 'Chess No',
	ISNULL(vchCodeLetter, '-') 'Code Letter',
	ISNULL(tbl_Tabulation.numMarks, 0) 'Mark',
	ISNULL(tbl_Tabulation.intPointToParticipant, 0) 'P1'
	
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

WHERE tbl_Section.intSectionId = @param1
AND tbl_ItemList.intItemId = @param2
AND intParticipantToLevelId = @param3 and ISNULL(tbl_ItemList.vchStatus,'No')='Yes'
ORDER BY numMarks desc 
)B
WHERE B.Mark > 0
)C
GROUP BY c.Rank_Order,c.Name,c.Section,c.Item,c.Team,c.[Chess No],c.[Code Letter],c.Mark