USE [DB_Sahithyolsav]
GO
/****** Object:  StoredProcedure [dbo].[spGetParticipantBySectionId]    Script Date: 08/30/2014 18:58:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER proc [dbo].[spGetParticipantBySectionId]
	@SectionId int,
	@intParticipantToLevelId int,
	@intParticipantLevelId int 
As
SELECT
	tbl_ParticipantList.intParticipantListId,
	tbl_Participant.intParticipantId,
	tbl_ParticipantList.intParticipantToLevelId,
	tbl_GroupSection.intSectionID,
	vchPartcipantName,
	tbl_Section.vchSectionName,
	vchChessNo,
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
	END AS 'Participant From'
FROM tbl_ParticipantList
INNER JOIN tbl_Participant
	ON tbl_Participant.intParticipantId = tbl_ParticipantList.intParticipantId
INNER JOIN tbl_Section
	ON tbl_ParticipantList.intSectionId = tbl_Section.intSectionID
	inner join tbl_GroupSection 
	ON tbl_ParticipantList.intParticipantListId=tbl_GroupSection.intParticipantListId
WHERE tbl_GroupSection.intSectionId=@SectionId
AND intParticipantToLevelId = @intParticipantToLevelId AND intParticipantLevelId=@intParticipantLevelId