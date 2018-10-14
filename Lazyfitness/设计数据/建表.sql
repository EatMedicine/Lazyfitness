--******�����շ�������*******--

--*******�������ݿ�*******--
IF EXISTS (SELECT * FROM sys.databases WHERE name = 'Lazyfitness')  
DROP DATABASE Lazyfitness
GO
CREATE DATABASE Lazyfitness
GO

--*******ʹ�����ݿ�*******--
USE Lazyfitness
GO

--*******�����û����˰�ȫ��Ϣ��*******--
IF EXISTS(SELECT * FROM SYSOBJECTS WHERE name = 'userSecurity')
DROP TABLE userSecurity
GO
CREATE TABLE userSecurity
(
	userId INT PRIMARY KEY,
	userPwd NVARCHAR(50)
)
GO

--*******�����û�������Ϣ��*******--
IF EXISTS(SELECT * FROM SYSOBJECTS WHERE name = 'userInfo')
DROP TABLE userInfo
GO
CREATE TABLE userInfo
(
	userId INT PRIMARY KEY Foreign Key References userSecurity(userId),
	userName NVARCHAR(50),
	userAge INT,--0~999��
	userSex INT,--�л�Ů
	userTel NVARCHAR(11),--11λ�绰����
	userStatus INT,
	userAccount INT
)
GO


--*******����ר����*******--
--IF EXISTS(SELECT * FROM SYSOBJECTS WHERE name = 'specialArea')
--DROP TABLE specialArea
--GO
--CREATE TABLE specialArea
--(
--	areaId INT PRIMARY KEY,
--	areaName NVARCHAR(50),
--	areaBrief TEXT
--)
GO

--*******������Դ��Ϣ������*******--
IF EXISTS(SELECT * FROM SYSOBJECTS WHERE name = 'resourceArea')
DROP TABLE resourceArea
GO
CREATE TABLE resourceArea
(
	areaId INT PRIMARY KEY,
	areaName NVARCHAR(50),
	areaBrief TEXT
)
GO

--*******�����ʴ������*******--
IF EXISTS(SELECT * FROM SYSOBJECTS WHERE name = 'quesArea')
DROP TABLE quesArea
GO
CREATE TABLE quesArea
(
	areaId INT PRIMARY KEY,
	areaName NVARCHAR(50),
	areaBrief TEXT
)
GO

--*******������̳������*******--
IF EXISTS(SELECT * FROM SYSOBJECTS WHERE name = 'postArea')
DROP TABLE postArea
GO
CREATE TABLE postArea
(
	areaId INT PRIMARY KEY,
	areaName NVARCHAR(50),
	areaBrief TEXT
)
GO



--*******�������±���������ƺ���ǡ�����ݶ�__��Դ��Ϣ��__*******--
IF EXISTS(SELECT * FROM SYSOBJECTS WHERE name = 'resourceInfo')
DROP TABLE resourceInfo
GO
CREATE TABLE resourceInfo
(
	areaId INT,
	resourceId INT,
	resourceName NVARCHAR(50),
	userId INT,
	resourceTime DATETIME,--������datetime�ܴ洢������ʱ����
	pageView INT,
	isTop INT,
	resourceContent TEXT,
	Primary Key(areaId,resourceId),
	Foreign Key (areaId) References resourceArea(areaId)
)
GO

--*******����������Ϣ��*******--
IF EXISTS(SELECT * FROM SYSOBJECTS WHERE name = 'postInfo')
DROP TABLE postInfo
GO
CREATE TABLE postInfo
(
	areaId INT,
	postId INT,
	postTitle NVARCHAR(50),
	userId INT,
	postTime DATETIME,
	pageView INT,
	isPost INT,
	isPay INT,
	amount INT,
	postStatus INT,
	postContent TEXT,
	Primary Key(areaId,postId),
	Foreign Key (areaId) References postArea(areaId)
)
GO

--*******�������ӻظ���*******--
IF EXISTS(SELECT * FROM SYSOBJECTS WHERE name = 'postReply')
DROP TABLE postReply
GO
CREATE TABLE postReply
(
	postId INT,
	userId INT,
	replyTime DATETIME,
	replyContent TEXT,
	Primary Key (postId)
)
GO



--*******�����ʴ����ӱ�*******--
IF EXISTS(SELECT * FROM SYSOBJECTS WHERE name = 'quesAnswInfo')
DROP TABLE quesAnswInfo
GO
CREATE TABLE quesAnswInfo
(
	areaId INT,
	quesAnswId INT,
	quesAnswTitle NVARCHAR(50),
	userId INT,
	quesAnswTime DATETIME,
	pageView INT,
	isPost INT,
	quesAnswStatus INT,
	amount INT,
	quesAnswContent Text,
	Primary Key(areaId,quesAnswId),
	Foreign Key (areaId) References quesArea(areaId)
)
GO

--*******�����ʴ������*******--
IF EXISTS(SELECT * FROM SYSOBJECTS WHERE name = 'quesAnswReply')
DROP TABLE quesAnswReply
GO
CREATE TABLE quesAnswReply
(
	quesAnswId INT,
	userId INT,
	replyTime DATETIME,
	replyContent TEXT,
	isAgree INT,
	Primary Key(quesAnswId)
)
GO

--*******�����㿨��ֵ��*******--
IF EXISTS(SELECT * FROM SYSOBJECTS WHERE name = 'recharge')
DROP TABLE recharge
GO
CREATE TABLE recharge
(
	rechargeId NVARCHAR(50) PRIMARY KEY,
	rechargePwd NVARCHAR(50),
	amount INT,
	isAvailable INT
)
GO

--*******������̨�����߱�*******--
IF EXISTS(SELECT * FROM SYSOBJECTS WHERE name = 'backManager')
DROP TABLE backManager
GO
CREATE TABLE backManager
(
	managerId INT PRIMARY KEY,
	managerPwd NVARCHAR(50)
)