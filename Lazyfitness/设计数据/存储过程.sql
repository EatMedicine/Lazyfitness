USE Lazyfitness
GO


--�û���¼�Ĵ洢����
IF EXISTS(SELECT name FROM SYSOBJECTS
			WHERE name = 'P_UsersLogin' AND TYPE = 'P')
DROP PROC P_UsersLogin
GO
CREATE PROC P_UsersLogin
@loginId NVARCHAR(20),--�û�Id
@userPwd NVARCHAR(20)--����
AS
	select loginId,userPwd from userSecurity where loginId=@loginId and userPwd=@userPwd
GO

--�����֤��û��loginId�Ĵ洢����
IF EXISTS(SELECT name FROM SYSOBJECTS
			WHERE name = 'P_IsExitId' AND TYPE = 'P')
DROP PROC P_IsExitId
GO
CREATE PROC P_IsExitId
@loginId INT
as
	SELECT loginId FROM userSecurity where loginId = @loginId	
GO