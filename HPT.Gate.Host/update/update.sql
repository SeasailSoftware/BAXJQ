USE [{DBName}]
GO

/****** Object:  Table [dbo].[CameraInfo]    Script Date: 04/21/2017 14:52:26 ******/
SET ANSI_NULLS ON
GO
TrunCate Table Menus
Insert Menus(MenuId,MenuName,MenuText,ParMenuId,EnableFlag) values(0,	'OperRights',	'菜单权限',	-1,	1)
Insert Menus(MenuId,MenuName,MenuText,ParMenuId,EnableFlag) values(1,	'EBG_Personal',	'人事管理',	0,	1)
Insert Menus(MenuId,MenuName,MenuText,ParMenuId,EnableFlag) values(11,	'BTI_Dept',	'部门设置',	1,	1)
Insert Menus(MenuId,MenuName,MenuText,ParMenuId,EnableFlag) values(12,	'BTI_EmpAndCard',	'人员与卡证管理',	1,	1)
Insert Menus(MenuId,MenuName,MenuText,ParMenuId,EnableFlag) values(2,	'EBG_Device',	'设备管理',	0,	1)
Insert Menus(MenuId,MenuName,MenuText,ParMenuId,EnableFlag) values(21,	'BTI_PlaceAndDevice',	'设备登记',	2,	1)
Insert Menus(MenuId,MenuName,MenuText,ParMenuId,EnableFlag) values(22,	'BTI_DeviceSetting',	'设备参数设置',	2,	1)
Insert Menus(MenuId,MenuName,MenuText,ParMenuId,EnableFlag) values(23,	'BTI_DeviceRights',	'设备权限管理',	2,	1)
Insert Menus(MenuId,MenuName,MenuText,ParMenuId,EnableFlag) values(4,	'EBG_Reports',	'报表中心',	0,	1)
Insert Menus(MenuId,MenuName,MenuText,ParMenuId,EnableFlag) values(41,	'BTI_ReportOfEmp',	'人员报表',	4,	1)
Insert Menus(MenuId,MenuName,MenuText,ParMenuId,EnableFlag) values(42,	'BTI_ReportOfRight',	'权限报表',	4,	1)
Insert Menus(MenuId,MenuName,MenuText,ParMenuId,EnableFlag) values(43,	'BTI_ReportOfRecord',	'记录报表',	4,	1)
Insert Menus(MenuId,MenuName,MenuText,ParMenuId,EnableFlag) values(44,	'BTI_IDCardReport',	'身份证记录报表',	4,	1)
Insert Menus(MenuId,MenuName,MenuText,ParMenuId,EnableFlag) values(5,	'EBG_System',	'系统设置',	0,	1)
Insert Menus(MenuId,MenuName,MenuText,ParMenuId,EnableFlag) values(51,	'BTI_Oper',	'用户管理',	5,	1)
Insert Menus(MenuId,MenuName,MenuText,ParMenuId,EnableFlag) values(52,	'BTI_Password',	'修改密码',	5,	1)
Insert Menus(MenuId,MenuName,MenuText,ParMenuId,EnableFlag) values(53,	'BTI_OperLog',	'操作日志',	5,	1)
Insert Menus(MenuId,MenuName,MenuText,ParMenuId,EnableFlag) values(54,	'BTI_SysPara',	'系统参数设置',	5,	1)
Insert Menus(MenuId,MenuName,MenuText,ParMenuId,EnableFlag) values(6,	'EBG_About',	'关于',	0,	1)
Insert Menus(MenuId,MenuName,MenuText,ParMenuId,EnableFlag) values(61,	'BTI_About',	'关于我们',	6,	1)
Insert Menus(MenuId,MenuName,MenuText,ParMenuId,EnableFlag) values(7,	'EBG_Attend',	'考勤管理',	0,	1)
Insert Menus(MenuId,MenuName,MenuText,ParMenuId,EnableFlag) values(71,	'BTI_AttendRules',	'考勤制度设置',	7,	1)
Insert Menus(MenuId,MenuName,MenuText,ParMenuId,EnableFlag) values(72,	'BTI_AttendProc',	'考勤分析处理',	7,	1)
Insert Menus(MenuId,MenuName,MenuText,ParMenuId,EnableFlag) values(73,	'BTI_OriginalRecord',	'原始刷卡记录表',	7,	1)
Insert Menus(MenuId,MenuName,MenuText,ParMenuId,EnableFlag) values(74,	'BTI_AttendDetailPersonal',	'个人考勤明细表',	7,	1)
Insert Menus(MenuId,MenuName,MenuText,ParMenuId,EnableFlag) values(75,	'BTI_AttendSummaryPersonal',	'个人考勤汇总表',	7,	1)
Insert Menus(MenuId,MenuName,MenuText,ParMenuId,EnableFlag) values(76,	'BTI_AttendSummaryDept',	'部门考勤汇总表',	7,	1)

Truncate table MenuOfOper
Insert MenuOfOper Select 1,MenuId From Menus 

GO
SET ANSI_PADDING OFF

GO

	if Not exists (select * from sysobjects where id = object_id(N'[dbo].[ImageOfRecord]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) 
	Begin

		CREATE TABLE [dbo].[ImageOfRecord](
			[RecId] [int] IDENTITY(1,1) NOT NULL,
			[CamId] [int] NOT NULL,
			[DeviceId] [int] NOT NULL,
			[IOFlag] [varchar](20) NOT NULL,
			[RecDateTime] [varchar](30) NOT NULL,
			[ImageString] [image] NOT NULL
		) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

	End

GO
If Not Exists(select * from syscolumns where id=object_id('DeptInfo')   and   name='DeptCode' ) 
	alter table DeptInfo add DeptCode int not null Default 0
GO
If Not Exists(select * from syscolumns where id=object_id('DeptInfo')   and   name='DeptCodeLength' ) 
	alter table DeptInfo add DeptCodeLength int not null Default 0
GO
If Not Exists(select * from syscolumns where id=object_id('DeptInfo')   and   name='IsbindingEmpCode' ) 
	alter table DeptInfo add IsbindingEmpCode int not null Default 0

GO
/****** Object:  StoredProcedure [dbo].[GetAllEmpByDeptId]    Script Date: 04/26/2017 15:43:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
if Exists(select name from sysobjects where NAME = 'GetAllEmpByDeptIdHasPhoto' and type='P') 
    drop procedure GetAllEmpByDeptIdHasPhoto 
	
GO
Create PROCEDURE [dbo].[GetAllEmpByDeptIdHasPhoto](@DeptId int) 
AS
BEGIN
SET NOCOUNT ON;
	Create table #t(Id int,Pid int,caption varchar(30),Level int)
	Insert #t exec GetAllDeptByParDeptId @DeptId
	Select EmpId,EmpCode,EmpName from EmpInfo  where datalength(Photo)>10 and DeptId in(Select Id from #t)
END

/*************************
添加人员信息与卡信息
**************************/

GO
If Not Exists(select * from syscolumns where id=object_id('DeviceInfo')   and   name='Enabled' ) 
	alter table DeviceInfo add Enabled int not null  default 1 

GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[HPT_AddEmpAndCard]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
-- 删除存储过程
drop procedure [dbo].HPT_AddEmpAndCard
GO

/****** Object:  StoredProcedure [dbo].[HPT_AddEmpAndCard]    Script Date: 05/23/2017 14:26:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[HPT_AddEmpAndCard](@EmpCode varchar(10),@EmpName varchar(30),@CardNo varchar(10),@Photo image,@Row1 varchar(16),@Row2 varchar(16),@Row3 varchar(16)) 
AS
BEGIN
SET NOCOUNT ON;
set xact_abort on
   Declare @EmpId int,@BeginDate varchar(30),@CardId int,@DeptId int,@CardCode varchar(8)
   Select @DeptId = DeptId From DeptInfo where DeptName = '公司'
   Select @CardNo = dbo.IntToHex(Convert(bigint,@CardNo))
   
      --存在人员编号
   If Exists(Select top 1 * from EmpInfo Where EmpCode = @EmpCode)
   Begin
   		Select @EmpId = EmpId From EmpInfo where EmpCode = @EmpCode
   		Update EmpInfo Set EmpName = @EmpName,Photo = @Photo Where EmpCode = @EmpCode
		If Exists(Select top 1 * From CardInfo where CardNo = @CardNo and CardStatus = 1)
			Update CardInfo Set Content1 = @Row1,Content2 = @Row2,Content3 = @Row3
		Else
			Exec [InsertCardInfo] @EmpId,1,@CardNo,1 ,0,@CardCode,1,1,7,1,0,0,0,@BeginDate,'2099-01-01',1,'姓名:',2,@Row1,
						1,'编号:',1,@Row2,1,'部门:',0,@Row3
   End Else
   Begin 
   		--插入人员信息
		Exec InsertEmpInfo @EmpCode ,@EmpName,'','男','',@DeptId ,'','','汉','1990-01-01','','2000-01-01',@photo
		Select @EmpId = EmpId From EmpInfo where EmpCode = @EmpCode 
		If(@EmpId >0)
		Begin
			Select @CardCode = Convert(varchar(8),@EmpId)
			Exec [InsertCardInfo] @EmpId,1,@CardNo,1 ,0,@CardCode,1,1,7,1,0,0,0,@BeginDate,'2099-01-01',1,'姓名:',2,@Row1,
						1,'编号:',1,@Row2,1,'部门:',0,@Row3
		End
   End 
      --对所有设备授权
   Select @CardId = CardId From CardInfo where CardNo = @CardNo and CardStatus = 1 and EmpId =@EmpId
   Create table #Device(DeviceId int)
   Insert #Device Select DeviceId from DeviceInfo
   while Exists(Select top 1 * from #Device)
   Begin
		Declare @DeviceId int
		Select top 1 @DeviceId = DeviceId From #Device
		Exec [InsertEmpRight] @CardId,1,@empid,@DeviceId,1
		Delete From #Device where DeviceId = @DeviceId
   End
END


/****************************
删除人员信息与卡信息
****************************/
GO
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[HPT_DeleteEmpAndCard]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
-- 删除存储过程
drop procedure [dbo].HPT_DeleteEmpAndCard
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


--删除人员信息以及人员的卡信息
-- =============================================
Create PROCEDURE [dbo].[HPT_DeleteEmpAndCard] (@EmpCode varchar(20),@EmpName varchar(20),@CardNo varchar(20))
AS
BEGIN
	Declare @EmpId int
	Select @EmpId = EmpId From EmpInfo where EmpCode = @EmpCode and EmpName = @EmpName
	if(@empid>0)
	begin
		--取消卡
		Declare @CardId int
		Select @CardId = CardId from CardInfo where EmpId = @EmpId and CardStatus = 1 and CardNo = @CardNo
		update cardinfo set CardNo ='FFFFFFFF',cardstatus = 0  where CardId = @CardId
		--删除人员
		delete from EmpInfo where Empid = @Empid
		--改变同步标志
		Update EmpRightOfDevice set updateFlag = 0 where CardId = @CardId and EmpID = @EmpId
	end
END

GO


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[AskForLeave]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
-- 删除表
drop table [dbo].[AskForLeave]
GO

/****** Object:  Table [dbo].[AskForLeave]    Script Date: 05/23/2017 16:28:38 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[AskForLeave](
	[RecId] [int] IDENTITY(1,1) NOT NULL,
	[CardNo] [varchar](20) NOT NULL,
	[BeginDate] [varchar](30) NOT NULL,
	[EndDate] [varchar](30) NOT NULL,
	[Active] [int] NOT NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[AskForLeave] ADD  CONSTRAINT [DF_AskForLeave_Active]  DEFAULT (0) FOR [Active]
GO


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[StringToDateTime]') and xtype in (N'FN', N'IF', N'TF'))
drop function [dbo].[StringToDateTime]
GO
set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO
Create FUNCTION [dbo].[StringToDateTime]
 (@strTime varchar(20))  --字符串形式的时间。格式：YYYYMMDDHHMISS --> YYYY-MM-DD HH:MI:SS
RETURNS datetime
AS
Begin
 Declare @strTmp  as varchar(20)
 Set @strTime = ltrim(rtrim(@strTime))
 Set @strTmp = subString(@strTime, 1, 4)    -- "YYYY"
 Set @strTmp = @strTmp + '-'     -- "YYYY-"
 Set @strTmp = @strTmp + subString(@strTime, 5, 2)  -- "YYYY-MM"
 Set @strTmp = @strTmp + '-'     -- "YYYY-MM-"
 Set @strTmp = @strTmp + subString(@strTime, 7, 2)  -- "YYYY-MM-DD"
 Set @strTmp = @strTmp + ' '     -- "YYYY-MM-DD "
 Set @strTmp = @strTmp + subString(@strTime, 9, 2)  -- "YYYY-MM-DD HH"
 Set @strTmp = @strTmp + ':'     -- "YYYY-MM-DD HH:"
 Set @strTmp = @strTmp + subString(@strTime, 11, 2)  -- "YYYY-MM-DD HH:MI"
 Set @strTmp = @strTmp + ':'     -- "YYYY-MM-DD HH:MI:"
 Set @strTmp = @strTmp + subString(@strTime, 13, 2)  -- "YYYY-MM-DD HH:MI:SS"
 
 return cast(@strTmp as datetime)
End

GO


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[HPT_AddLeave]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
-- 删除存储过程
drop procedure [dbo].HPT_AddLeave
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE HPT_AddLeave(@CardNo varchar(20),@BeginTime varchar(20),@EndTime varchar(20))
AS
BEGIN
	SET NOCOUNT ON;
	If Not Exists(Select top 1 * from CardInfo where CardNo = @CardNo and CardStatus = 1) Return
	Select @BeginTime = Convert(Varchar(20),dbo.StringToDateTime(@BeginTime),20)
	Select @EndTime = Convert(Varchar(20),dbo.StringToDateTime(@EndTime),20)
	Insert AskForLeave(CardNo,BeginDate,EndDate)
		values(@CardNo,@BeginTime,@EndTime)
END

GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[GetDisplayContent]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
-- 删除存储过程
drop procedure [dbo].GetDisplayContent
GO

/****** Object:  StoredProcedure [dbo].[GetDisplayContent]    Script Date: 05/23/2017 20:15:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[GetDisplayContent](@EmpId int,@Type int,@DisplayType int,@Text varchar(20),@Column int,@Content varchar(20))
AS
BEGIN
SET NOCOUNT ON;
	Declare @ReturnContent varchar(20)
	If(@DisplayType = 1)
		Select @ReturnContent = @Content
	Else Begin
		Declare @ColumnName varchar(20),@Sql varchar(1000)
		Select @ColumnName = ColumnName from CardPara where CId =@Column
		Create table #Para(DeptName varchar(20),EmpCode varchar(20),EmpName varchar(20),EnglishName varchar(20),Sex varchar(10),Photo varchar(11),Nation varchar(20),CardNo varchar(20),CardCode varchar(20))
		Insert #para Select Top 1 a.DeptName,b.EmpCode,b.EmpName,b.EnglishName,b.Sex,b.Telephone,b.Nationality,c.CardNo,c.CardCode  
			from DeptInfo a,EmpInfo b,CardInfo c where a.DeptId = b.DeptId and c.EmpId = b.EmpId and C.CardStatus = 1 and b.EmpId = @EmpId and c.Type = @Type
		Create table #Content(Content varchar(20))
		Select @Sql = 'Insert #Content Select '+@ColumnName +' From #Para '
		Exec (@Sql)
		Select @ReturnContent = @Text + Content From #Content
		--select * from cardpara
	End
	Select @ReturnContent
END

GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[InsertCardInfo]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
-- 删除存储过程
drop procedure [dbo].InsertCardInfo
GO
/****** Object:  StoredProcedure [dbo].[InsertCardInfo]    Script Date: 05/23/2017 20:14:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create  PROCEDURE [dbo].[InsertCardInfo](@EmpId varchar(30),@Type int,@CardNo varchar(50),@blackName int ,@cardType int,
@cardCode varchar(20),@inRight int,@outRight int,@voiceNo int,@photo int,@vacationId int,@inTimeGroupNo int,@outTimeGroupNo int,
@beginDate varchar(20),@endDate varchar(20),@DisplayType1 int,@Text1 varchar(10),@Column1 int,@Content1  varchar(20)
,@DisplayType2 int,@Text2 varchar(10),@Column2 int,@Content2  varchar(20),@DisplayType3 int,@Text3 varchar(10),@Column3 int,@Content3  varchar(20))
--exec InsertCardInfo '0003522173','1684173990','2016-02-25 03:34','admin','SZA22173','0','0','1','1','0','1','0','1','1','2016-02-25','2099-01-01','财神'
AS
BEGIN
SET NOCOUNT ON;
	declare @Cardid int
----------------------------------------------------
	--首先判断是否有数据
	if not exists(Select top 1 * from CardInfo)
	Begin
		Truncate table CardInfo
		Truncate table EmpRightOfDevice
		insert into CardInfo(Type,Cardno,Cardstatus,Empid,BlackName,cardType,CardCode,InRight,OutRight,VoiceNo,Photo,VacationId,InTimeGroupNo,OutTimeGroupNo,
			BeginDate,EndDate,DisplayType1,Text1,Column1,Content1,DisplayType2,Text2,Column2,Content2,DisplayType3,Text3,Column3,Content3)
			values(@Type,@cardno,1,@empid,@blackName,@cardType,@cardCode,@inRight,@outRight,@voiceNo,@photo,@vacationId,@inTimeGroupNo,@outTimeGroupNo,
				@beginDate,@endDate,@DisplayType1,@Text1,@Column1,@Content1,@DisplayType2,@Text2,@Column2,@Content2,@DisplayType3,@Text3,@Column3,@Content3)
	End Else
	Begin
		--其次判断是否存在已经注销的卡号
		if exists(Select top 1 * from CardInfo where CardNo ='FFFFFFFF' and CardStatus = 0)
		Begin
			Create Table #t(cardId int)
			insert #t select top 1000 cardid from cardinfo where cardstatus = 0 and CardNo = 'FFFFFFFF'
			while exists(select top 1 * from #t)
			begin
				select top 1 @cardId  = CardId from #t order by CardId
				if exists(select top 1 * from empRightOfDevice where updateFlag = 0 and Cardid = @cardid)
				begin
					delete from #t where Cardid = @Cardid
				end else
				begin
					update cardinfo set Type=@Type, Cardno =@CardNo,Cardstatus = 1,Empid =@empid,blackName =@BlackName,CardType = @CardType,InRight =@InRight,OutRight=@OutRight,VoiceNo = @VoiceNo,Photo =@Photo,
						cardCode= @cardCode,VacationId = @VacationId,InTimeGroupNo = @InTimeGroupNo,OutTimeGroupNo = @OutTimeGroupNo,BeginDate = @BeginDate,EndDate = @EndDate,DisplayType1=@DisplayType1,Text1 =@Text1,
						Column1=@Column1,Content1=@Content1,DisplayType2=@DisplayType2,Text2=@Text2,Column2=@Column2,Content2=@Content2,DisplayType3=@DisplayType3,Text3=@Text3,Column3=@Column3,Content3=@Content3
						where cardid = @cardid
					--从中间位置插入的卡信息需要删除对应的权限关系
					Delete From empRightOfDevice where CardId = @CardId
					delete from #t
					break
				end
			end
			--假如依然没有添加成功,直接插入
			if not exists(Select top 1 * from CardInfo where CardNo = @CardNo and CardStatus = 1)
			Begin
				insert into CardInfo(Type,Cardno,Cardstatus,Empid,BlackName,cardType,CardCode,InRight,OutRight,VoiceNo,Photo,VacationId,InTimeGroupNo,OutTimeGroupNo,
					BeginDate,EndDate,DisplayType1,Text1,Column1,Content1,DisplayType2,Text2,Column2,Content2,DisplayType3,Text3,Column3,Content3)
					values(@Type,@cardno,1,@empid,@blackName,@cardType,@cardCode,@inRight,@outRight,@voiceNo,@photo,@vacationId,@inTimeGroupNo,@outTimeGroupNo,
						@beginDate,@endDate,@DisplayType1,@Text1,@Column1,@Content1,@DisplayType2,@Text2,@Column2,@Content2,@DisplayType3,@Text3,@Column3,@Content3)
			End
		End Else
		--不存在注销的卡则直接插入
		Begin
			insert into CardInfo(Type,Cardno,Cardstatus,Empid,BlackName,cardType,CardCode,InRight,OutRight,VoiceNo,Photo,VacationId,InTimeGroupNo,OutTimeGroupNo,
				BeginDate,EndDate,DisplayType1,Text1,Column1,Content1,DisplayType2,Text2,Column2,Content2,DisplayType3,Text3,Column3,Content3)
				values(@Type,@cardno,1,@empid,@blackName,@cardType,@cardCode,@inRight,@outRight,@voiceNo,@photo,@vacationId,@inTimeGroupNo,@outTimeGroupNo,
					@beginDate,@endDate,@DisplayType1,@Text1,@Column1,@Content1,@DisplayType2,@Text2,@Column2,@Content2,@DisplayType3,@Text3,@Column3,@Content3)
		End
		
	End
	--最后更新显示内容
	Create Table #content(Content varchar(20))
	Insert #content Exec GetDisplayContent @EmpId,@Type,@DisplayType1,@Text1,@Column1,@Content1
	Select top 1 @Content1 = Content From #Content
	Truncate table #Content
	Insert #content Exec GetDisplayContent @EmpId,@Type,@DisplayType2,@Text2,@Column2,@Content2
	Select top 1 @Content2 = Content From #Content
	Truncate table #Content
	Insert #content Exec GetDisplayContent @EmpId,@Type,@DisplayType3,@Text3,@Column3,@Content3
	Select top 1 @Content3 = Content From #Content
	Update CardInfo Set Content1 = @Content1,Content2 = @Content2,Content3 = @Content3 where EmpId = @EmpId and CardNo = @CardNo and CardStatus = 1
END

GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FastAddEmpAndCard]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
-- 删除存储过程
drop procedure [dbo].FastAddEmpAndCard
GO
GO
/****** Object:  StoredProcedure [dbo].[InsertEmpInfo]    Script Date: 05/23/2017 20:42:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[FastAddEmpAndCard](@DeptId int,@EmpCode varchar(20),@EmpName varchar(20),@CardType int,@CardNo varchar(20))
AS
BEGIN
SET NOCOUNT ON;
	Begin Tran
		--插入人员信息
		Insert into EmpInfo(EmpCode,EmpName,EnglishName,Sex,IdentityCard,DeptId,TelePhone,BirthDay,Nationality,BornEarth,Marrige,JoinDate,Photo)
			values(@Empcode,@EmpName,'','男','',@DeptId,'','','汉','','','',0x00)
		Declare @EmpId int,@Content1 varchar(20),@Content2 varchar(20),@Content3 varchar(20),@BeginDate varchar(20)
		Select @EmpId = EmpId From EmpInfo where EmpCode = @EmpCode and EmpName = @EmpName
		Select @Content1 = '部门:'+DeptName From DeptInfo where DeptId  =(Select DeptId From EmpInfo where EmpId = @EmpId)
		Select @Content2 = '编号:'+@EmpCode
		Select @Content2 = '姓名:'+@EmpName		
		Select @BeginDate = Convert(varchar(100),getdate(),23)
		Exec InsertCardInfo @EmpId,@CardType,@CardNo,0,0,@EmpId,1,1,7,0,0,1,1,@BeginDate,'2099-01-01',
			0,'',1,@Content1,0,'',2,@Content2,0,'',3,@Content3
	commit tran 
END


GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Led_LedController]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
-- 删除表
drop table [dbo].[Led_LedController]
GO

/****** Object:  Table [dbo].[AskForLeave]    Script Date: 05/23/2017 16:28:38 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Led_LedController](
	[RecId] [int] IDENTITY(1,1) NOT NULL,
	[Lid] [int] NOT NULL,
	[ControlType] [int] NOT NULL,
	[protocol] [int] NOT NULL,
	[width] [int] NOT NULL,
	[heigth] [int] NOT NULL,
	[IPaddress] [varchar](20) NOT NULL,
	[Port] [int] NOT NULL
) ON [PRIMARY]

GO
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ReportOfRecord_Capture]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
-- 删除存储过程
drop procedure [dbo].ReportOfRecord_Capture
GO
GO
/****** Object:  StoredProcedure [dbo].[InsertEmpInfo]    Script Date: 05/23/2017 20:42:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[ReportOfRecord_Capture](@DeptId int,@DeptType int,@deviceId int,@EmpCode varchar(20),@EmpName varchar(20),@cardNo varchar(40),@BeginDate varchar(20),@EndDate varchar(20))
AS
BEGIN
SET NOCOUNT ON;
		Declare @sql varchar(2000),@SqlEmpCode varchar(100),@SqlEmpName varchar(100),@SqlCardNo varchar(100)
		Create Table #EmpAndCard(DeptId int,DeptName varchar(20),EmpId int,EmpCode varchar(20),EmpName varchar(20),CardNo varchar(20))
		--不包括下级部门
		IF(@DeptType = 0)
		Begin
			Insert #EmpAndCard Select a.DeptId,a.DeptName,b.EmpId,b.EmpCode,b.EmpName,c.CardNo from DeptInfo a,EmpInfo b,CardInfo c 
				Where a.DeptId = b.DeptId and b.EmpId = c.EmpId and c.CardStatus = 1 and a.DeptId = @DeptId
		End
		--包括下级部门
		If(@DeptType = 1)
		Begin
			Create table #Dept	
			(	
				id   int,            --节点id  
				pid int,            --父节点id  
				caption varchar(50), --部门名称  
				level int       --层级  
			)
			Insert #dept exec 	GetAllDeptidOfParDept @DeptId
			Insert #EmpAndCard  Select a.DeptId,a.DeptName,b.EmpId,b.EmpCode,b.EmpName,c.CardNo from DeptInfo a,EmpInfo b,CardInfo c 
				Where a.DeptId = b.DeptId and b.EmpId = c.EmpId and c.CardStatus = 1 and a.DeptId in(Select #Dept.id From #Dept)
		End
		
		Create table #Record(DeviceId int,DeviceName varchar(30),CardNo varchar(20),RecDatetime varchar(30),IOFlag varchar(10),RecordType varchar(20),ImageOfRecord Image)
		Insert #record Select a.DeviceId,b.DeviceName,a.CardNo,a.RecDateTime,a.IOFlag,a.RecordType,c.ImageString from Record a,DeviceInfo b,ImageOfRecord c 
			where a.DeviceId = b.DeviceId and a.RecDateTime >=@BeginDate and a.RecDatetime <= @EndDate and a.DeviceId = c.DeviceId and a.RecDateTime =c.RecDateTime 
				And a.IOFlag = c.IOFlag 
		If @CardNo = ''
			Select * from #EmpAndCard a ,#Record b where a.CardNo = b.CardNo order by a.DeptId,a.EmpId,b.RecDatetime
		Else
			Select * from #EmpAndCard a ,#Record b where a.CardNo = b.CardNo and a.CardNo = @CardNo order by a.DeptId,a.EmpId,b.RecDatetime

END


GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ReportOfExceptionRecord]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
-- 删除存储过程
drop procedure [dbo].ReportOfExceptionRecord
GO
GO
/****** Object:  StoredProcedure [dbo].[InsertEmpInfo]    Script Date: 05/23/2017 20:42:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE ReportOfExceptionRecord(@DeviceId int,@BeginDate varchar(20),@EndDate varchar(20))

AS
BEGIN
SET NOCOUNT ON;

	If @DeviceId = 0
		Select * from (Select A.DeviceId,b.DeviceName,a.CardNo,a.IOFlag,a.RecDatetime,a.RecordType From Record a,DeviceInfo b 
			where a.RecDateTime >=@BeginDate and a.RecDatetime <=@EndDate and a.DeviceId = b.DeviceId and RecordType <> '有效票') c Left Join ImageOfRecord d
			On d.DeviceId = c.DeviceId and d.RecDateTime =c.RecDateTime And d.IOFlag = c.IOFlag		Order by c.RecDatetime asc	 
	Else
		Select * from ( Select A.DeviceId,b.DeviceName,a.CardNo,a.IOFlag,a.RecDatetime,a.RecordType From Record a,DeviceInfo b 
			where a.RecDateTime >=@BeginDate and a.RecDatetime <=@EndDate and a.DeviceId = b.DeviceId And a.DeviceId = @DeviceId and RecordType <> '有效票') c Left Join ImageOfRecord d
		On d.DeviceId = c.DeviceId and d.RecDateTime =c.RecDateTime And d.IOFlag = c.IOFlag		 Order by c.RecDatetime asc	 
END


GO