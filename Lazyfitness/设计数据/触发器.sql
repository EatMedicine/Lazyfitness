USE Lazyfitness
GO

--当管理员删除用户的时候删除全部信息
IF (OBJECT_ID('T_DeleteALL', 'TR') IS NOT NULL)
DROP TRIGGER T_DeleteALL
GO
CREATE TRIGGER T_DeleteALL
ON userSecurity
INSTEAD OF DELETE
AS
DECLARE @userId INT
BEGIN
	SELECT @userId = deleted.userId FROM deleted
	DELETE FROM quesAnswReply WHERE userId = @userId
	DELETE FROM quesAnswInfo WHERE userId = @userId
	DELETE FROM postReply WHERE userId = @userId
	DELETE FROM postInfo WHERE userId = @userId
	DELETE FROM resourceInfo WHERE userId = @userId
	DELETE FROM userInfo WHERE userId = @userId
	DELETE FROM userSecurity WHERE userId = @userId

END
GO
delete from userSecurity where userId = 2