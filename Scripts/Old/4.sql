USE [DB_Sahithyolsav]
GO
/****** Object:  StoredProcedure [dbo].[spTeamPoint]    Script Date: 09/03/2014 22:04:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


ALTER proc [dbo].[spTeamPoint]
	@param1 int,
	@param2 int,
	@param3 int,
	@param4 int,
	@param5 int
As

--if @param4=1
--Begin
--SELECT
--	tbl_District.vchDistrictName 'TEAM',
--	SUM(A.intPointToTeam) 'POINT'
--FROM (SELECT
--	DENSE_RANK() OVER (PARTITION BY intItemId ORDER BY intPointToTeam DESC) Rank,
--	intParticipantLevelId,
--	intItemId,
--	intPointToTeam
--FROM tbl_ParticipantList
--INNER JOIN tbl_Participant
--	ON tbl_ParticipantList.intParticipantId = tbl_Participant.intParticipantId
--INNER JOIN tbl_Tabulation
--	ON tbl_Tabulation.intParticipatListId = tbl_ParticipantList.intParticipantListId
--WHERE intParticipantToLevelId = @param3
--GROUP BY	intParticipantLevelId,
--			intItemId,
--			intPointToTeam) A
--INNER JOIN tbl_District
--	ON A.intParticipantLevelId = tbl_District.intDistrictID
--GROUP BY	A.intParticipantLevelId,
--			vchDistrictName
--ORDER BY SUM(A.intPointToTeam) DESC

--END
--IF @param4 = 2 BEGIN
--SELECT
--	tbl_Division.vchDivisionName 'TEAM',
--	SUM(A.intPointToTeam) 'POINT'
--FROM (SELECT
--	DENSE_RANK() OVER (PARTITION BY intItemId ORDER BY intPointToTeam DESC) Rank,
--	intParticipantLevelId,
--	intItemId,
--	intPointToTeam
--FROM tbl_ParticipantList
--INNER JOIN tbl_Participant
--	ON tbl_ParticipantList.intParticipantId = tbl_Participant.intParticipantId
--INNER JOIN tbl_Tabulation
--	ON tbl_Tabulation.intParticipatListId = tbl_ParticipantList.intParticipantListId
--WHERE intParticipantToLevelId = @param3
--GROUP BY	intParticipantLevelId,
--			intItemId,
--			intPointToTeam) A
--INNER JOIN tbl_Division
--	ON A.intParticipantLevelId = tbl_Division.intDivisionId
--GROUP BY	A.intParticipantLevelId,
--			vchDivisionName
--ORDER BY SUM(A.intPointToTeam) DESC
--END

--IF @param4 = 3 BEGIN
--SELECT
--	tbl_Sector.vchSectorName 'TEAM',
--	SUM(A.intPointToTeam) 'POINT'
--FROM (SELECT
--	DENSE_RANK() OVER (PARTITION BY intItemId ORDER BY intPointToTeam DESC) Rank,
--	intParticipantLevelId,
--	intItemId,
--	intPointToTeam
--FROM tbl_ParticipantList
--INNER JOIN tbl_Participant
--	ON tbl_ParticipantList.intParticipantId = tbl_Participant.intParticipantId
--INNER JOIN tbl_Tabulation
--	ON tbl_Tabulation.intParticipatListId = tbl_ParticipantList.intParticipantListId
--WHERE intParticipantToLevelId = @param3
--GROUP BY	intParticipantLevelId,
--			intItemId,
--			intPointToTeam) A
--INNER JOIN tbl_sector
--	ON A.intParticipantLevelId = tbl_sector.intSectorId
--GROUP BY	A.intParticipantLevelId,
--			vchSectorName
--ORDER BY SUM(A.intPointToTeam) DESC

--END

--IF @param4 = 4 BEGIN

--SELECT
--	tbl_Unit.vchUnitName 'TEAM',
--	SUM(A.intPointToTeam) 'POINT'
--FROM (SELECT
--	DENSE_RANK() OVER (PARTITION BY intItemId ORDER BY intPointToTeam DESC) Rank,
--	intParticipantLevelId,
--	intItemId,
--	intPointToTeam
--FROM tbl_ParticipantList
--INNER JOIN tbl_Participant
--	ON tbl_ParticipantList.intParticipantId = tbl_Participant.intParticipantId
--INNER JOIN tbl_Tabulation
--	ON tbl_Tabulation.intParticipatListId = tbl_ParticipantList.intParticipantListId
--WHERE intParticipantToLevelId = @param3
--GROUP BY	intParticipantLevelId,
--			intItemId,
--			intPointToTeam) A
--INNER JOIN tbl_Unit
--	ON A.intParticipantLevelId = tbl_Unit.intUnitId
--GROUP BY	A.intParticipantLevelId,
--			vchUnitName
--ORDER BY SUM(A.intPointToTeam) DESC
--END

---Report Modified

--SELECT
----A.intParticipantLevelId,
--A.Team 'TEAM',SUM(A.Point)'POINT' FROM
--(
--SELECT 
--	DENSE_RANK() OVER (PARTITION BY tbl_ItemList.intItemId ORDER BY tbl_Tabulation.numMarks DESC) Rank_Order,
--	intParticipantLevelId,tbl_Section.intSectionId,tbl_ItemList.intItemId,
--	vchPartcipantName 'Name',
--	tbl_Section.vchSectionName 'Section',
--	tbl_Item.vchItemName 'Item',
--	CASE
--		WHEN intProgramLevelId = 1 THEN (SELECT
--				vchDistrictName --+ ' District'
--			FROM tbl_District
--			WHERE intDistrictID = intParticipantLevelId)

--		WHEN intProgramLevelId = 2 THEN (SELECT
--				vchDivisionName --+ ' Division'
--			FROM tbl_Division
--			WHERE intDivisionId = intParticipantLevelId)
--		WHEN intProgramLevelId = 3 THEN (SELECT
--				vchSectorName --+ ' Sector'
--			FROM tbl_Sector
--			WHERE intSectorId = intParticipantLevelId)
--		WHEN intProgramLevelId = 4 THEN (SELECT
--				vchUnitName --+ 'Unit'
--			FROM tbl_Unit
--			WHERE tbl_Unit.intUnitId = intParticipantLevelId)
--		WHEN intProgramLevelId = 5 THEN ('Individual')
--	END AS 'Team',
--	vchChessNo 'Chess No',
--	ISNULL(vchCodeLetter, '-') 'Code Letter',
--	--ISNULL(tbl_Tabulation.numMarks, 0) 'Mark',
--	ISNULL(tbl_Tabulation.intPointToParticipant, 0) +
--	(SELECT 
--		CASE WHEN DENSE_RANK() OVER (PARTITION BY tbl_ItemList.intItemId ORDER BY tbl_Tabulation.numMarks DESC)=1 AND ISNULL(tbl_Tabulation.numMarks, 0) >0 THEN
--				intMarkForFirstPlace
--		     WHEN DENSE_RANK() OVER (PARTITION BY tbl_ItemList.intItemId ORDER BY tbl_Tabulation.numMarks DESC)=2 AND ISNULL(tbl_Tabulation.numMarks, 0) >0 THEN
--				intMarkForSecondPlace
--		     WHEN DENSE_RANK() OVER (PARTITION BY tbl_ItemList.intItemId ORDER BY tbl_Tabulation.numMarks DESC)=3 AND ISNULL(tbl_Tabulation.numMarks, 0) >0 THEN
--				intMarkForThirdPlace
--			End as Marks 
--	  FROM tbl_Item WHERE intItemId=tbl_ItemList.intItemId ) 'Point'
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

--WHERE 
----tbl_Section.intSectionId = @param1
----AND tbl_ItemList.intItemId = @param2
----AND 
--intParticipantToLevelId = @param3 and ISNULL(tbl_ItemList.vchStatus,'No')='Yes'
----ORDER BY numMarks desc 
--)A
--GROUP BY A.intParticipantLevelId,A.Team
--ORDER BY SUM(A.Point) DESC


SELECT C.Team 'TEAM',SUM(C.P1) 'POINT' 
FROM
(
	SELECT *,
	 CASE WHEN a.Rank_Order=1 THEN
			(SELECT intMarkForFirstPlace FROM tbl_Item WHERE intItemId=A.itemId)
		  WHEN A.Rank_Order=2 THEN
	 		(SELECT intMarkForSecondPlace FROM tbl_Item WHERE intItemId=A.itemId)
		  WHEN A.Rank_Order=3 THEN
			(SELECT intMarkForThirdPlace FROM tbl_Item WHERE intItemId=A.itemId)
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
		tbl_Item.intItemId 'itemId',
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
			WHEN intProgramLevelId = 1 THEN ('Individual')
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

	WHERE 
	--tbl_Section.intSectionId = @param1 AND tbl_ItemList.intItemId = @param2 AND 
	intParticipantToLevelId = @param3 and ISNULL(tbl_ItemList.vchStatus,'No')='Yes'
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
		tbl_Item.intItemId 'itemId',
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

	WHERE 
	--tbl_Section.intSectionId = @param1
	--AND tbl_ItemList.intItemId = @param2
	--AND 
	intParticipantToLevelId = @param3 and ISNULL(tbl_ItemList.vchStatus,'No')='Yes'
	ORDER BY numMarks desc 
	)B
	WHERE B.Mark > 0
)C
GROUP BY C.Team
ORDER BY SUM(C.P1) DESC

