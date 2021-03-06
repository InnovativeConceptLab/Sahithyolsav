USE [DB_Sahithyolsav]
GO
/****** Object:  StoredProcedure [dbo].[spTeamPoint]    Script Date: 08/16/2014 19:38:49 ******/
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

if @param4=1
Begin
SELECT
	tbl_District.vchDistrictName 'TEAM',
	SUM(A.intPointToTeam) 'POINT'
FROM (SELECT
	DENSE_RANK() OVER (PARTITION BY intItemId ORDER BY intPointToTeam DESC) Rank,
	intParticipantLevelId,
	intItemId,
	intPointToTeam
FROM tbl_ParticipantList
INNER JOIN tbl_Participant
	ON tbl_ParticipantList.intParticipantId = tbl_Participant.intParticipantId
INNER JOIN tbl_Tabulation
	ON tbl_Tabulation.intParticipatListId = tbl_ParticipantList.intParticipantListId
WHERE intParticipantToLevelId = @param3
GROUP BY	intParticipantLevelId,
			intItemId,
			intPointToTeam) A
INNER JOIN tbl_District
	ON A.intParticipantLevelId = tbl_District.intDistrictID
GROUP BY	A.intParticipantLevelId,
			vchDistrictName
ORDER BY SUM(A.intPointToTeam) DESC

END
IF @param4 = 2 BEGIN
SELECT
	tbl_Division.vchDivisionName 'TEAM',
	SUM(A.intPointToTeam) 'POINT'
FROM (SELECT
	DENSE_RANK() OVER (PARTITION BY intItemId ORDER BY intPointToTeam DESC) Rank,
	intParticipantLevelId,
	intItemId,
	intPointToTeam
FROM tbl_ParticipantList
INNER JOIN tbl_Participant
	ON tbl_ParticipantList.intParticipantId = tbl_Participant.intParticipantId
INNER JOIN tbl_Tabulation
	ON tbl_Tabulation.intParticipatListId = tbl_ParticipantList.intParticipantListId
WHERE intParticipantToLevelId = @param3
GROUP BY	intParticipantLevelId,
			intItemId,
			intPointToTeam) A
INNER JOIN tbl_Division
	ON A.intParticipantLevelId = tbl_Division.intDivisionId
GROUP BY	A.intParticipantLevelId,
			vchDivisionName
ORDER BY SUM(A.intPointToTeam) DESC
END

IF @param4 = 3 BEGIN
SELECT
	tbl_Sector.vchSectorName 'TEAM',
	SUM(A.intPointToTeam) 'POINT'
FROM (SELECT
	DENSE_RANK() OVER (PARTITION BY intItemId ORDER BY intPointToTeam DESC) Rank,
	intParticipantLevelId,
	intItemId,
	intPointToTeam
FROM tbl_ParticipantList
INNER JOIN tbl_Participant
	ON tbl_ParticipantList.intParticipantId = tbl_Participant.intParticipantId
INNER JOIN tbl_Tabulation
	ON tbl_Tabulation.intParticipatListId = tbl_ParticipantList.intParticipantListId
WHERE intParticipantToLevelId = @param3
GROUP BY	intParticipantLevelId,
			intItemId,
			intPointToTeam) A
INNER JOIN tbl_sector
	ON A.intParticipantLevelId = tbl_sector.intSectorId
GROUP BY	A.intParticipantLevelId,
			vchSectorName
ORDER BY SUM(A.intPointToTeam) DESC

END

IF @param4 = 4 BEGIN

SELECT
	tbl_Unit.vchUnitName 'TEAM',
	SUM(A.intPointToTeam) 'POINT'
FROM (SELECT
	DENSE_RANK() OVER (PARTITION BY intItemId ORDER BY intPointToTeam DESC) Rank,
	intParticipantLevelId,
	intItemId,
	intPointToTeam
FROM tbl_ParticipantList
INNER JOIN tbl_Participant
	ON tbl_ParticipantList.intParticipantId = tbl_Participant.intParticipantId
INNER JOIN tbl_Tabulation
	ON tbl_Tabulation.intParticipatListId = tbl_ParticipantList.intParticipantListId
WHERE intParticipantToLevelId = @param3
GROUP BY	intParticipantLevelId,
			intItemId,
			intPointToTeam) A
INNER JOIN tbl_Unit
	ON A.intParticipantLevelId = tbl_Unit.intUnitId
GROUP BY	A.intParticipantLevelId,
			vchUnitName
ORDER BY SUM(A.intPointToTeam) DESC
END