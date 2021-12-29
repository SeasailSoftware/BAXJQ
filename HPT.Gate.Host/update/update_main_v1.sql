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
/****** Object:  StoredProcedure [dbo].[GetAllEmpByDeptId]    Script Date: 04/26/2017 15:43:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[GetAllEmpByDeptIdHasPhoto](@DeptId int) 
AS
BEGIN
SET NOCOUNT ON;
	Create table #t(Id int,Pid int,caption varchar(30),Level int)
	Insert #t exec GetAllDeptByParDeptId @DeptId
	Select EmpId,EmpCode,EmpName from EmpInfo  where datalength(Photo)>10 and DeptId in(Select Id from #t)
END