USE Lazyfitness
GO

IF EXISTS (SELECT * FROM sysobjects WHERE name = 'viewShowInfo')
DROP VIEW viewShowInfo /*ɾ����ͼ*/
GO
CREATE VIEW viewShowInfo /*������ͼ*/
AS
	SELECT userName, userSex, userAge FROM userInfo
GO
--SELECT * FROM viewShowInfo
--GO
IF EXISTS (SELECT * FROM sysobjects WHERE name = 'viewShowList')
DROP VIEW viewShowList /*ɾ����ͼ*/
GO
CREATE VIEW viewShowList /*������ͼ*/
AS
	SELECT resourceName, (SELECT userName  FROM userInfo WHERE userId = resourceInfo.userId) as author
	 FROM resourceInfo		
GO

--SELECT * FROM viewShowList
--GO