--******采用驼峰命名法*******--

--*******创建数据库*******--
IF EXISTS (SELECT * FROM sys.databases WHERE name = 'Lazyfitness')  
DROP DATABASE Lazyfitness
GO
CREATE DATABASE Lazyfitness
GO

--*******使用数据库*******--
USE Lazyfitness
GO

--*******创建用户个人安全信息表*******--
IF EXISTS(SELECT * FROM SYSOBJECTS WHERE name = 'userSecurity')
DROP TABLE userSecurity
GO
CREATE TABLE userSecurity
(
	userId INT PRIMARY KEY,
	userPwd NVARCHAR(50)
)
GO

--*******创建用户个人信息表*******--
IF EXISTS(SELECT * FROM SYSOBJECTS WHERE name = 'userInfo')
DROP TABLE userInfo
GO
CREATE TABLE userInfo
(
	userId INT PRIMARY KEY Foreign Key References userSecurity(userId),
	userName NVARCHAR(50),
	userAge INT,--0~999岁
	userSex INT,--男或女
	userTel NVARCHAR(11),--11位电话号码
	userStatus INT,
	userAccount INT
)
GO


--*******创建专区表*******--
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

--*******创建资源信息分区表*******--
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

--*******创建问答分区表*******--
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

--*******创建论坛分区表*******--
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



--*******创建文章表（文章这个称呼不恰当）暂定__资源信息表__*******--
IF EXISTS(SELECT * FROM SYSOBJECTS WHERE name = 'resourceInfo')
DROP TABLE resourceInfo
GO
CREATE TABLE resourceInfo
(
	areaId INT,
	resourceId INT,
	resourceName NVARCHAR(50),
	userId INT,
	resourceTime DATETIME,--可以用datetime能存储年月日时分秒
	pageView INT,
	isTop INT,
	resourceContent TEXT,
	Primary Key(areaId,resourceId),
	Foreign Key (areaId) References resourceArea(areaId)
)
GO

--*******创建帖子信息表*******--
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

--*******创建帖子回复表*******--
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



--*******创建问答帖子表*******--
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

--*******创建问答回帖表*******--
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

--*******创建点卡充值表*******--
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

--*******创建后台管理者表*******--
IF EXISTS(SELECT * FROM SYSOBJECTS WHERE name = 'backManager')
DROP TABLE backManager
GO
CREATE TABLE backManager
(
	managerId INT PRIMARY KEY,
	managerPwd NVARCHAR(50)
)