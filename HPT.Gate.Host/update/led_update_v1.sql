USE [HPTGate_S_V_2_7_3]
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

alter table DeptInfo add DeptCode int not null Default 0
alter table DeptInfo add DeptCodeLength int not null Default 0
alter table DeptInfo add IsbindingEmpCode int not null Default 0

GO


USE [HPTGate_S_V_2_7_3]
GO

/****** Object:  Table [dbo].[LedController]    Script Date: 04/19/2017 16:00:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO
If Exists(select * from sysobjects where name='Led_LedController' and xtype='U')  
	Drop Table Led_LedController
GO

CREATE TABLE [dbo].[Led_LedController](
	[Lid] [int] IDENTITY(1,1) NOT NULL,
	[ControlType] [int] NOT NULL,
	[protocol] [int] NOT NULL,
	[width] [int] NOT NULL,
	[heigth] [int] NOT NULL,
	[IPaddress] [varchar](20) NOT NULL,
	[Port] [int] NOT NULL,
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

USE [HPTGate_S_V_2_7_3]
GO

/****** Object:  Table [dbo].[Led_AreaInfo]    Script Date: 04/19/2017 16:01:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

If Exists(select * from sysobjects where name='Led_LedController' and xtype='U')  
	Drop Table Led_LedController
GO
CREATE TABLE [dbo].[Led_AreaInfo](
	[RecId] [int] IDENTITY(1,1) NOT NULL,
	[AreaId] [int] NOT NULL,
	[LID] [int] NOT NULL,
	[Point_X] [int] NOT NULL,
	[Point_Y] [int] NOT NULL,
	[Width] [int] NOT NULL,
	[Height] [int] NOT NULL,
	[BordreType] [int] NOT NULL,
	[BorderNo] [int] NOT NULL,
	[BorderLength] [int] NOT NULL,
	[BorderSpeed] [int] NOT NULL,
	[BorderEffect] [int] NOT NULL,
	[TextFont] [varchar](30) NOT NULL,
	[TextFontSize] [int] NOT NULL,
	[TextBold] [int] NOT NULL,
	[Text] [varchar](1000) NOT NULL,
	[SingleLine] [int] NOT NULL,
	[DisplayEffect] [int] NOT NULL,
	[Speed] [int] NOT NULL,
	[Stay] [int] NOT NULL,
	[Interval] [int] NOT NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Led_AreaInfo] ADD  DEFAULT (3) FOR [Interval]
GO


USE [HPTGate_S_V_2_7_3]
GO

/****** Object:  Table [dbo].[Attend_Data]    Script Date: 04/11/2017 14:54:51 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO
If Exists(select * from sysobjects where name='Attend_Data' and xtype='U')  
	Drop Table Attend_Data
GO
CREATE TABLE [dbo].[Attend_Data](
	[RecId] [int] IDENTITY(1,1) NOT NULL,
	[DeptId] [int] NOT NULL,
	[DeptName] [varchar](30) NOT NULL,
	[EmpId] [int] NOT NULL,
	[EmpCode] [varchar](30) NOT NULL,
	[EmpName] [varchar](50) NOT NULL,
	[CardNo] [varchar](20) NOT NULL,
	[DeviceId] [int] NOT NULL,
	[DeviceName] [varchar](30) NOT NULL,
	[RecDate] [varchar](20) NOT NULL,
	[RecTime] [varchar](20) NOT NULL,
	[IOFlag] [varchar](10) NOT NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Attend_Data] ADD  CONSTRAINT [DF_AttendData_N_DeptName]  DEFAULT ('公司') FOR [DeptName]
GO

USE [HPTGate_S_V_2_7_3]
GO

/****** Object:  Table [dbo].[Attend_Detail]    Script Date: 04/11/2017 14:55:04 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO
If Exists(select * from sysobjects where name='Attend_Detail' and xtype='U')  
	Drop Table Attend_Detail
GO
CREATE TABLE [dbo].[Attend_Detail](
	[RecId] [int] IDENTITY(1,1) NOT NULL,
	[DeptId] [int] NOT NULL,
	[DeptName] [varchar](30) NOT NULL,
	[EmpId] [int] NOT NULL,
	[EmpCode] [varchar](20) NOT NULL,
	[EmpName] [varchar](30) NOT NULL,
	[RecDate] [varchar](20) NOT NULL,
	[Record] [varchar](1000) NULL,
	[SignIn] [int] NOT NULL,
	[IOCount] [int] NOT NULL,
	[Minutes] [int] NOT NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Attend_Detail] ADD  CONSTRAINT [DF_AttendDetail_N_SignIn]  DEFAULT (0) FOR [SignIn]
GO

ALTER TABLE [dbo].[Attend_Detail] ADD  CONSTRAINT [DF_AttendDetail_N_Minutes]  DEFAULT (0) FOR [Minutes]
GO


USE [HPTGate_S_V_2_7_3]
GO

/****** Object:  StoredProcedure [dbo].[Attend_CreateAttendData]    Script Date: 04/11/2017 14:55:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[Attend_CreateAttendData](@EmpId int,@BeginDate varchar(20),@EndDate Varchar(20),@BeginTime varchar(20),@EndDay int,@EndTime varchar(20)) 
AS
BEGIN
SET NOCOUNT ON;
	--先删除考勤记录与考勤明细表
	Declare @DeptId int,@DeptName varchar(30),@EmpCode varchar(20),@EmpName varchar(30),@SecondDay varchar(20)
	Select @SecondDay = Convert(Varchar(20),Convert(datetime,@EndDate)+1,23)
	Select @DeptId = b.DeptId,@DeptName =b.DeptName,@EmpCode = a.EmpCode,@EmpName = a.EmpName From EmpInfo a,DeptInfo b 
		where a.DeptId = b.DeptId and a.EmpId = @EmpId
	Create Table #CardNo(CardNo varchar(20))
	Insert #CardNo Select CardNo from CardInfo where EmpId = @EmpId And CardStatus = 1 
	--删除原来考勤数据
	If(@EndDay = 0)
	--考勤周期在当天
	Begin
		Delete From Attend_Data where EmpId = @EmpId and RecDate = @BeginDate  and RecTime >= @BeginTime
		Delete From Attend_Data where EmpId = @EmpId and RecDate > @BeginDate  and RecDate < @EndDate
		Delete From Attend_Data where EmpId = @EmpId and RecDate = @EndDate and RecTime <=@EndTime
	End	Else 
	Begin
	--考勤周期在当天和次日
		Delete From Attend_Data where EmpId = @EmpId and RecDate = @BeginDate  and RecTime >=@BeginTime
		Delete From Attend_Data where EmpId = @EmpId and RecDate > @BeginDate  and RecDate <= @EndDate
		Delete From Attend_Data Where EmpId = @EmpId and RecDate = @SecondDay and RecTime <= @EndTime
	End	
	--删除考勤明细表
	Delete From Attend_Detail where EmpId = @EmpId and RecDate >= @BeginDate and RecDate <=@EndDate
	--在导入考勤数据
	If Exists(Select top 1 * from #CardNo)
	Begin
		Declare @Begin varchar(100),@End varchar(100)
		If(@EndDay = 0)
		Begin
			Select @Begin = @BeginDate + ' ' + @BeginTime;
			Select @End = @BeginDate +' ' + @EndTime
		End Else
		Begin
			Select @Begin = @BeginDate + ' ' + @BeginTime;
			Select @End = @SecondDay +' ' + @EndTime
		ENd

		Insert Attend_Data Select @DeptId,@DeptName, @EmpId,@EmpCode,@EmpName,a.CardNo,b.DeviceId,b.DeviceName,CONVERT(varchar(100), convert(datetime,a.RecDateTime), 23),SUBSTRING(CONVERT(varchar(100), convert(datetime,a.RecDateTime), 24),1,5),a.IOFlag
				from Record a,DeviceInfo b where a.DeviceId = b.DeviceId and a.CardNo IN ( Select CardNo From #CardNo ) and RecDateTime >= @Begin and RecDateTime <= @End
	End

END

GO


USE [HPTGate_S_V_2_7_3]
GO

/****** Object:  StoredProcedure [dbo].[Attend_GetOriginalRecord]    Script Date: 04/11/2017 14:56:07 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[Attend_GetOriginalRecord](@BeginDate varchar(20),@EndDate varchar(20),@DeptId int,@DeptType int,@EmpCode varchar(20),@EmpName varchar(20))
AS
BEGIN
SET NOCOUNT ON;
		Declare @sql varchar(2000)
		--不包括下级部门
		IF(@DeptType = 0)
		Begin
			Select @sql = 'Select DeptId,DeptName,EmpId,EmpCode,EmpName,CardNo,DeviceId,DeviceName,RecDate,RecTime,IOFlag from Attend_Data Where 1 = 1  '
			Select @sql = @sql + ' And DeptId = '+ Cast(@DeptId as varchar(5)) 
			Select @sql = @sql + case @EmpCode when '' then '' else '  and EmpCode = '+@EmpCode  end 
			Select @sql = @sql + Case @EmpName when '' then '' else '  and EmpName = '+@EmpName  end 
			Select @sql = @sql + ' and recDate >='''+@BeginDate +''' '
			Select @sql = @sql + ' and RecDate <='''+@EndDate +''' ' 
			Select @sql = @sql +'  Order by DeptId,EmpId,recDate,RecTime'
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
			Insert #dept exec 	GetAllDeptByParDeptId @DeptId
			Select @sql = 'Select DeptId,DeptName,EmpId,EmpCode,EmpName,CardNo,DeviceId,DeviceName,RecDate,RecTime,IOFlag from Attend_Data Where 1 = 1 '
			Select @sql = @sql + ' And DeptId in (Select Id from #dept) ' 
			Select @sql = @sql + case @EmpCode when '' then '' else '  and EmpCode = '+@EmpCode  end 
			Select @sql = @sql + Case @EmpName when '' then '' else '  and EmpName = '+@EmpName  end 
			Select @sql = @sql + ' and recDate >='''+@BeginDate +''' '
			Select @sql = @sql + ' and RecDate <='''+@EndDate +''' ' 
			Select @sql = @sql +'  Order by DeptId,EmpId,recDate,RecTime'
		End
		Exec (@sql)
END


--Select * from Attend_Data
GO


USE [HPTGate_S_V_2_7_3]
GO

/****** Object:  StoredProcedure [dbo].[Attend_GetProcEmpList]    Script Date: 04/11/2017 14:56:38 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



Create PROCEDURE [dbo].[Attend_GetProcEmpList](@DeptId int,@DeptType int,@EmpCode varchar(20),@EmpName varchar(20))
AS
BEGIN
SET NOCOUNT ON;
		Declare @sql varchar(2000)
		--不包括下级部门
		IF(@DeptType = 0)
		Begin
			Select @sql = 'Select a.DeptId,b.EmpId,b.EmpCode,b.EmpName from DeptInfo a,EmpInfo b where a.DeptId = b.DeptId '
			Select @sql = @sql + ' And a.DeptId = '+ Cast(@DeptId as varchar(5)) 
			Select @sql = @sql + case @EmpCode when '' then '' else '  and b.EmpCode = '+@EmpCode  end 
			Select @sql = @sql + Case @EmpName when '' then '' else '  and b.EmpName = '+@EmpName  end 
			Select @sql = @sql +'  Order by b.EmpId'
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
			Insert #dept exec 	GetAllDeptByParDeptId @DeptId
			Select @sql = 'Select a.DeptId,b.EmpId,b.EmpCode,b.EmpName from DeptInfo a,EmpInfo b where a.DeptId = b.DeptId '
			Select @sql = @sql + ' And a.DeptId in (Select Id from #dept)' 
			Select @sql = @sql + case @EmpCode when '' then '' else '  and b.EmpCode = '+@EmpCode  end 
			Select @sql = @sql + Case @EmpName when '' then '' else '  and b.EmpName = '+@EmpName  end 
			Select @sql = @sql +'  Order by b.EmpId'
		End
		Exec (@sql)
END



GO


USE [HPTGate_S_V_2_7_3]
GO

/****** Object:  StoredProcedure [dbo].[Attend_InsertAttendDetailNoRecord]    Script Date: 04/11/2017 14:57:01 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[Attend_InsertAttendDetailNoRecord](@EmpId int,@RecDate varchar(20)) 
AS
BEGIN
SET NOCOUNT ON;
	
	Declare @DeptId int,@DeptName varchar(30),@EmpCode varchar(10),@EmpName varchar(30),@Record varchar(100),@SignIn int,@IOCount int,@Minutes int
	Select @DeptId =b.DeptId,@DeptName = b.DeptName,@EmpCode = a.EmpCode,@EmpName = a.EmpName From EmpInfo a,DeptInfo b 
		where a.DeptId = b.Deptid and a.EmpId = @EmpId
	Select @Record = ''
	Select @SignIn = 0
	Select @IOCount = 0
	Select @Minutes = 0
	Insert Into Attend_Detail(DeptId,DeptName,EmpId,EmpCode,EmpName,RecDate,Record,SignIn,IOCount,Minutes)
		values(@DeptId,@DeptName,@EmpId,@EmpCode,@EmpName,@RecDate,@Record,@SignIn,@IOCount,@Minutes)
	
END

GO


USE [HPTGate_S_V_2_7_3]
GO

/****** Object:  StoredProcedure [dbo].[Attend_ReportOfAttendDetail]    Script Date: 04/11/2017 14:57:16 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[Attend_ReportOfAttendDetail](@BeginDate varchar(20),@EndDate varchar(20),@DeptId int,@DeptType int,@EmpCode varchar(20),@EmpName varchar(20))
AS
BEGIN
SET NOCOUNT ON;
		Declare @sql varchar(2000)
		--不包括下级部门
		IF(@DeptType = 0)
		Begin
			Select @sql = 'Select DeptId,DeptName,EmpId,EmpCode,EmpName,RecDate,Record,SignIn,IOCount,Minutes from Attend_Detail Where 1 = 1 '
			Select @sql = @sql + ' And DeptId = '+ Cast(@DeptId as varchar(5)) 
			Select @sql = @sql + case @EmpCode when '' then '' else '  and EmpCode = '+@EmpCode  end 
			Select @sql = @sql + Case @EmpName when '' then '' else '  and EmpName = '+@EmpName  end 
			Select @sql = @sql +'  Order by DeptId,EmpId,recDate'
			--Select * from Attend_Detail
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
			Insert #dept exec 	GetAllDeptByParDeptId @DeptId
			Select @sql = 'Select DeptId,DeptName,EmpId,EmpCode,EmpName,RecDate,Record,SignIn,IOCount,Minutes from Attend_Detail Where 1 = 1 '
			Select @sql = @sql + ' And a.DeptId in (Select Id from #dept)' 
			Select @sql = @sql + case @EmpCode when '' then '' else '  and EmpCode = '+@EmpCode  end 
			Select @sql = @sql + Case @EmpName when '' then '' else '  and EmpName = '+@EmpName  end 
			Select @sql = @sql +'  Order by DeptId,EmpId,recDate'
		End
		Exec (@sql)
END

--select * from AttendDetail where recdate >='2015-08-01'

GO


USE [HPTGate_S_V_2_7_3]
GO

/****** Object:  StoredProcedure [dbo].[Attend_ReportOfSummaryDept]    Script Date: 04/11/2017 14:57:30 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[Attend_ReportOfSummaryDept](@BeginDate varchar(20),@EndDate varchar(20),@DeptId int,@DeptType int)
AS
BEGIN
SET NOCOUNT ON;
		Create table #Summary(DeptId int,DeptName varchar(30), RecDate varchar(20),CountOfSignIn int,CountOfIO int,TotalOfMinutes int,DuringOneHour int,DuringThreeHours int,DuringFiveHours int,DuringNineHours int,AboveNineHours int)
		Create table #Dept(	id   int,pid int, caption varchar(50),level int)
		--不包括下级部门
		IF(@DeptType = 0)
		Begin
			Insert #dept Select DeptId,ParDeptId,DeptName,2 From DeptInfo  where DeptId = @DeptId
		End
		--包括下级部门
		If(@DeptType = 1)
		Begin
			Insert #Dept exec 	GetAllDeptByParDeptId @DeptId
		End
		Insert #Summary 
			Select DeptId,DeptName,RecDate,Sum(SignIn),Sum(IOCount),Sum(Minutes),
				Sum(case when Minutes < 60 then 1 else 0 end ), Sum(case when Minutes >= 60 and Minutes <180 then 1 else 0 end ),Sum(case when Minutes >= 180 and Minutes <300 then 1 else 0 end ) ,Sum(case when Minutes >= 300 and Minutes <540 then 1 else 0 end ),Sum(case when Minutes >540 then 1 else 0 end )  from Attend_Detail 
				Where DeptId in (Select Id From #Dept ) And RecDate >=@BeginDate And RecDate <= @EndDate group By DeptId,DeptName,RecDate
		Select * From #Summary
		Drop Table #Summary
END


--exec ReportOfAttendSummary '2016-07-01','2016-07-31',32,1,'',''
--select * from AttendDetail where recdate >='2015-08-01'
--Select * from DeptInfo

--Select * from Attend_Detail

GO

USE [HPTGate_S_V_2_7_3]
GO

/****** Object:  StoredProcedure [dbo].[Attend_ReportOfSummaryPersonal]    Script Date: 04/11/2017 14:57:53 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[Attend_ReportOfSummaryPersonal](@BeginDate varchar(20),@EndDate varchar(20),@EmpCode varchar(20),@EmpName varchar(20))
AS
BEGIN
SET NOCOUNT ON;
	Create table #Summary(EmpId int,EmpName varchar(30), RecDate varchar(50),CountOfSignIn int,CountOfIO int,TotalOfMinutes int,DuringOneHour int,DuringThreeHours int,DuringFiveHours int,DuringNineHours int,AboveNineHours int)
	Declare @EmpId int,@Days int
	If(@EmpCode = '')
	Begin
		If(@EmpName = '')
			Select @EmpId = 0
		Else
			Select @EmpId = EmpId From EmpInfo where EmpName = @EmpName
	End Else
	Begin
		if(@EmpName ='')
			Select @EmpId = EmpId From EmpInfo where EmpCode = @EmpCode
		Else
			Select @EmpId = EmpId From EmpInfo Where EmpCode = @EmpCode and EmpName = @EmpName
	End
	Select @Days = DATEDIFF( day,Convert(DateTime,@BeginDate) ,Convert(DateTime,@EndDate))+1
	Insert #Summary 
			Select EmpId,EmpName,('('+@BeginDate+'~'+@EndDate+')') as RecDate, @Days ,Sum(IOCount),Sum(Minutes),
				Sum(case when Minutes < 60 then 1 else 0 end ), Sum(case when Minutes >= 60 and Minutes <180 then 1 else 0 end ),Sum(case when Minutes >= 180 and Minutes <300 then 1 else 0 end ) ,Sum(case when Minutes >= 300 and Minutes <540 then 1 else 0 end ),Sum(case when Minutes >540 then 1 else 0 end )  from Attend_Detail 
				Where EmpId = @EmpId  And RecDate >=@BeginDate And RecDate <= @EndDate group By EmpId,EmpName
	Select * From #Summary
	Drop Table #Summary
END


GO






