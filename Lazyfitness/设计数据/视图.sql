USE Lazyfitness
GO

IF EXISTS (SELECT * FROM sysobjects WHERE name = 'viewShowInfo')
DROP VIEW viewShowInfo /*删除视图*/
GO
CREATE VIEW viewShowInfo /*创建视图*/
AS
	SELECT userName, userSex, userAge FROM userInfo
GO
--SELECT * FROM viewShowInfo
--GO
IF EXISTS (SELECT * FROM sysobjects WHERE name = 'viewShowList')
DROP VIEW viewShowList /*删除视图*/
GO
CREATE VIEW viewShowList /*创建视图*/
AS
	SELECT resourceName, (SELECT userName  FROM userInfo WHERE userId = resourceInfo.userId) as author
	 FROM resourceInfo		
GO

--SELECT * FROM viewShowList
--GO