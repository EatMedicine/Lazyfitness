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
	userId INT PRIMARY KEY IDENTITY(1, 1),
	loginId NVARCHAR(50),
	userPwd NVARCHAR(50)
)
GO
select * from userSecurity
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
	userStatus INT DEFAULT 1,
	userAccount INT DEFAULT 0
)
GO
select * from userInfo

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

insert into resourceArea values(12315,'Unknown','Unknown')
select * from resourceArea

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
select * from quesArea

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
select * from postArea


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

insert into resourceInfo values(12315,110,'NO.1',1,GETDATE(),0,0,'Num')
insert into resourceInfo values(12315,111,'NO.2',1,GETDATE(),0,0,'mm')
insert into resourceInfo values(12315,112,'NO.3',1,GETDATE(),0,0,'nn')
insert into resourceInfo values(12315,113,'NO.4',1,GETDATE(),100,0,'nn')
insert into resourceInfo values(12315,114,'NO.5',1,GETDATE(),50,0,'nn')
insert into resourceInfo values(12315,115,'NO.6',1,GETDATE(),500,0,'nn')
insert into resourceInfo values(12315,116,'NO.7',1,GETDATE(),10,0,'nn')
insert into resourceInfo values(12315,117,'NO.8',1,GETDATE(),20,0,'nn')
insert into resourceInfo values(12315,118,'NO.9',1,GETDATE(),30,0,'nn')
insert into resourceInfo values(12315,119,'NO.10',1,GETDATE(),5000,0,'nn')
insert into resourceInfo values(12315,120,'NO.11',1,GETDATE(),200,0,'nn')
insert into resourceInfo values(12315,122,'NO.12',1,GETDATE(),110,0,'nn')
insert into resourceInfo values(12315,123,'NO.13',1,GETDATE(),120,0,'nn')

select * from resourceInfo

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
select * from postInfo
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
insert into postReply values(12,1,GETDATE(),'嗨，你好呀！')
select * from postReply


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
select * from quesAnswInfo

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
insert into quesAnswReply values(13,1,GETDATE(),'嗨，你好呀！',1)
select * from quesAnswReply
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

select * from backManager
go

insert into backManager values('666', 'E10ADC3949BA59ABBE56E057F20F883E')

/*
select * from userSecurity
go
select * from resourceInfo
go

insert userSecurity(userPwd) values('wqe')
insert userInfo(userId) values(762441)
*/
--select userPwd from userSecurity where userId = (select userId from userInfo where userName = 'test1')