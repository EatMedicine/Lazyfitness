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
	userId INT PRIMARY KEY IDENTITY(1, 1), --用户唯一指定id 用于各个涉及用户的表的连接
	loginId NVARCHAR(50), --仅仅用于登录的登录名 显示请用userInfo中的userName（昵称）
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
	userSex INT,--0男 1女 2保密
	userEmail NVARCHAR(50),--邮箱
	userStatus INT DEFAULT 1, --0禁言,1注册会员,2正式会员,3管理员
	userAccount INT DEFAULT 0, --用户金钱 
	userIntroduce NVARCHAR(200),	--用户简介
	userHeaderPic NVARCHAR(200)	--用户头像地址
)
GO

--*******创建资源信息分区表*******--
IF EXISTS(SELECT * FROM SYSOBJECTS WHERE name = 'resourceArea')
DROP TABLE resourceArea
GO
CREATE TABLE resourceArea
(
	areaId INT PRIMARY KEY IDENTITY(1, 1),
	areaName NVARCHAR(50), --分区名
	areaBrief TEXT --简介 
)
GO

--*******创建问答分区表*******--
IF EXISTS(SELECT * FROM SYSOBJECTS WHERE name = 'quesArea')
DROP TABLE quesArea
GO
CREATE TABLE quesArea
(
	areaId INT PRIMARY KEY IDENTITY(1, 1),
	areaName NVARCHAR(50),--分区名
	areaBrief TEXT --简介
)
GO

--*******创建论坛分区表*******--
IF EXISTS(SELECT * FROM SYSOBJECTS WHERE name = 'postArea')
DROP TABLE postArea
GO
CREATE TABLE postArea
(
	areaId INT PRIMARY KEY IDENTITY(1, 1),
	areaName NVARCHAR(50),--分区名
	areaBrief TEXT --简介
)
GO

--*******创建文章表（文章这个称呼不恰当）暂定__资源信息表__*******--
IF EXISTS(SELECT * FROM SYSOBJECTS WHERE name = 'resourceInfo')
DROP TABLE resourceInfo
GO
CREATE TABLE resourceInfo
(
	areaId INT,
	resourceId INT IDENTITY(1, 1),
	resourceName NVARCHAR(50), --文章标题
	userId INT,
	resourceTime DATETIME,--可以用datetime能存储年月日时分秒
	pageView INT, --浏览量
	isTop INT, --是否置顶 0不置顶1置顶
	resourceContent TEXT,
	PRIMARY KEY(areaId, resourceId)
)
GO

--*******创建帖子信息表*******--
IF EXISTS(SELECT * FROM SYSOBJECTS WHERE name = 'postInfo')
DROP TABLE postInfo
GO
CREATE TABLE postInfo
(
	areaId INT,
	postId INT IDENTITY(1, 1),
	postTitle NVARCHAR(50),
	userId INT,
	postTime DATETIME,
	pageView INT, --浏览量
	isPost INT, --是否置顶0不置顶1置顶
	amount INT, --该贴的金额（目前还没实际用上）
	postStatus INT,
	postContent TEXT,
	PRIMARY KEY(areaId, postId)
)
GO

--*******创建帖子回复表*******--
IF EXISTS(SELECT * FROM SYSOBJECTS WHERE name = 'postReply')
DROP TABLE postReply
GO
CREATE TABLE postReply
(
	id int primary key identity(1, 1),
	postId INT,
	userId INT,
	replyTime DATETIME,
	replyContent TEXT,	
)
GO

--*******创建问答帖子表*******--
IF EXISTS(SELECT * FROM SYSOBJECTS WHERE name = 'quesAnswInfo')
DROP TABLE quesAnswInfo
GO
CREATE TABLE quesAnswInfo
(
	areaId INT,
	quesAnswId INT IDENTITY(1, 1),
	quesAnswTitle NVARCHAR(50),
	userId INT,
	quesAnswTime DATETIME,
	pageView INT,--浏览量
	isPost INT, --是否置顶0不置顶1置顶
	quesAnswStatus INT, --问题状态 0为关闭 1为未解决 2为已解决
	amount INT, --悬赏金额
	quesAnswContent Text,
	PRIMARY KEY(areaId, quesAnswId)
)
GO

--*******创建问答回帖表*******--
IF EXISTS(SELECT * FROM SYSOBJECTS WHERE name = 'quesAnswReply')
DROP TABLE quesAnswReply
GO
CREATE TABLE quesAnswReply
(
	id int primary key identity(1, 1),
	quesAnswId INT,
	userId INT,
	replyTime DATETIME,
	replyContent TEXT,
	isAgree INT,
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

go

IF EXISTS(SELECT * FROM SYSOBJECTS WHERE name = 'serverShowInfo')
DROP TABLE serverShowInfo
GO
CREATE TABLE serverShowInfo
(
	id INT PRIMARY KEY IDENTITY(1, 1),
	areaId INT, --用于标记这条记录用于哪个大区 0为首页 1为文章区 当flag=2的时候（0Hello页的信息，包括图片地址和slogan 1为模板页的图片地址 2为网站名字
	title NVARCHAR(50),	--标题
	pictureAdr NVARCHAR(200),	--图片地址 若为记录则为空
	url NVARCHAR(200),	--链接地址
	flag INT,	--区分公告或是轮播图 0轮播图 1公告 2网站显示信息
	Infostatus INT	--决定是否启用
)
GO


IF EXISTS(SELECT * FROM SYSOBJECTS WHERE name = 'userStatusName')
DROP TABLE userStatusName
GO
CREATE TABLE userStatusName
(
	userStatus INT PRIMARY KEY,
	statusName NVARCHAR(50)
)
GO




insert into backManager values('666', 'E10ADC3949BA59ABBE56E057F20F883E')
