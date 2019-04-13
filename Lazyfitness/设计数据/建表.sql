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
	userId INT PRIMARY KEY IDENTITY(1, 1), --�û�Ψһָ��id ���ڸ����漰�û��ı������
	loginId NVARCHAR(50), --�������ڵ�¼�ĵ�¼�� ��ʾ����userInfo�е�userName���ǳƣ�
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
	userSex INT,--0�� 1Ů 2����
	userEmail NVARCHAR(50),--����
	userStatus INT DEFAULT 1, --0����,1ע���Ա,2��ʽ��Ա,3����Ա
	userAccount INT DEFAULT 0, --�û���Ǯ 
	userIntroduce NVARCHAR(200),	--�û����
	userHeaderPic NVARCHAR(200)	--�û�ͷ���ַ
)
GO

--*******������Դ��Ϣ������*******--
IF EXISTS(SELECT * FROM SYSOBJECTS WHERE name = 'resourceArea')
DROP TABLE resourceArea
GO
CREATE TABLE resourceArea
(
	areaId INT PRIMARY KEY IDENTITY(1, 1),
	areaName NVARCHAR(50), --������
	areaBrief TEXT --��� 
)
GO

--*******�����ʴ������*******--
IF EXISTS(SELECT * FROM SYSOBJECTS WHERE name = 'quesArea')
DROP TABLE quesArea
GO
CREATE TABLE quesArea
(
	areaId INT PRIMARY KEY IDENTITY(1, 1),
	areaName NVARCHAR(50),--������
	areaBrief TEXT --���
)
GO

--*******������̳������*******--
IF EXISTS(SELECT * FROM SYSOBJECTS WHERE name = 'postArea')
DROP TABLE postArea
GO
CREATE TABLE postArea
(
	areaId INT PRIMARY KEY IDENTITY(1, 1),
	areaName NVARCHAR(50),--������
	areaBrief TEXT --���
)
GO

--*******�������±���������ƺ���ǡ�����ݶ�__��Դ��Ϣ��__*******--
IF EXISTS(SELECT * FROM SYSOBJECTS WHERE name = 'resourceInfo')
DROP TABLE resourceInfo
GO
CREATE TABLE resourceInfo
(
	areaId INT,
	resourceId INT IDENTITY(1, 1),
	resourceName NVARCHAR(50), --���±���
	userId INT,
	resourceTime DATETIME,--������datetime�ܴ洢������ʱ����
	pageView INT, --�����
	isTop INT, --�Ƿ��ö� 0���ö�1�ö�
	resourceContent TEXT,
	PRIMARY KEY(areaId, resourceId)
)
GO

--*******����������Ϣ��*******--
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
	pageView INT, --�����
	isPost INT, --�Ƿ��ö�0���ö�1�ö�
	amount INT, --�����Ľ�Ŀǰ��ûʵ�����ϣ�
	postStatus INT,
	postContent TEXT,
	PRIMARY KEY(areaId, postId)
)
GO

--*******�������ӻظ���*******--
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

--*******�����ʴ����ӱ�*******--
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
	pageView INT,--�����
	isPost INT, --�Ƿ��ö�0���ö�1�ö�
	quesAnswStatus INT, --����״̬ 0Ϊ�ر� 1Ϊδ��� 2Ϊ�ѽ��
	amount INT, --���ͽ��
	quesAnswContent Text,
	PRIMARY KEY(areaId, quesAnswId)
)
GO

--*******�����ʴ������*******--
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

go

IF EXISTS(SELECT * FROM SYSOBJECTS WHERE name = 'serverShowInfo')
DROP TABLE serverShowInfo
GO
CREATE TABLE serverShowInfo
(
	id INT PRIMARY KEY IDENTITY(1, 1),
	areaId INT, --���ڱ��������¼�����ĸ����� 0Ϊ��ҳ 1Ϊ������ ��flag=2��ʱ��0Helloҳ����Ϣ������ͼƬ��ַ��slogan 1Ϊģ��ҳ��ͼƬ��ַ 2Ϊ��վ����
	title NVARCHAR(50),	--����
	pictureAdr NVARCHAR(200),	--ͼƬ��ַ ��Ϊ��¼��Ϊ��
	url NVARCHAR(200),	--���ӵ�ַ
	flag INT,	--���ֹ�������ֲ�ͼ 0�ֲ�ͼ 1���� 2��վ��ʾ��Ϣ
	Infostatus INT	--�����Ƿ�����
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
