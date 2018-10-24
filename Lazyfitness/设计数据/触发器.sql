USE Lazyfitness
GO

--当管理员删除用户的时候删除全部信息
IF (OBJECT_ID('T_DeleteUser', 'TR') IS NOT NULL)
DROP TRIGGER T_DeleteUser
GO
CREATE TRIGGER T_DeleteUser
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
--delete from userSecurity where userId = 2

--当管理员删除资源分区的时候删除全部信息
IF (OBJECT_ID('T_DeleteResourceArea', 'TR') IS NOT NULL)
DROP TRIGGER T_DeleteResourceArea
GO
CREATE TRIGGER T_DeleteResourceArea
ON resourceArea
INSTEAD OF DELETE
AS
DECLARE @areaId INT
BEGIN
	SELECT @areaId = deleted.areaId FROM deleted
	DELETE FROM resourceInfo WHERE areaId = @areaId
END
GO