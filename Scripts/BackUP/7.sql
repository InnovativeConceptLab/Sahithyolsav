

Create proc spSaveGroupParticiapnt
	@intGrpParticipant int,
	@intParticipantListId int,
	@vchGroupParticipantName varchar(500)
As
INSERT INTO 
	[tbl_ParticipantGroupList] ([intParticipantListId]
		, [vchGroupParticipantName])
	VALUES (@intParticipantListId
           ,@vchGroupParticipantName)


