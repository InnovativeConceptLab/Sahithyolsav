USE [DB_Sahithyolsav]
GO
/****** Object:  StoredProcedure [dbo].[SpGetParticipantByLevelId]    Script Date: 08/27/2014 21:37:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER proc [dbo].[SpGetParticipantByLevelId]
	@LevelId as int
As
SELECT
	intParticipantListId,
	tbl_Participant.vchPartcipantName,
	tbl_ParticipantList.intProgramLevelId,
	tbl_ParticipantList.intParticipantLevelId,
	tbl_ParticipantList.vchChessNo,
	CASE
		WHEN intProgramLevelId = 1 THEN 'State'
		WHEN intProgramLevelId = 2 THEN 'District'
		WHEN intProgramLevelId = 3 THEN 'Division'
		WHEN intProgramLevelId = 4 THEN 'Sector'
		WHEN intProgramLevelId = 5 THEN 'Unit'
	END AS 'Program Level',
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
	vchSectionName 'Section Name',
	isnull(tbl_Participant.intAge,0)Age,
	tbl_Participant.vchCampusName,
	tbl_Participant.vchCourse
FROM tbl_ParticipantList
INNER JOIN tbl_Participant
	ON tbl_ParticipantList.intParticipantId = tbl_Participant.intParticipantId
INNER JOIN tbl_Section
	ON tbl_Section.intSectionID = tbl_ParticipantList.intSectionId
WHERE intProgramLevelId = @LevelId
