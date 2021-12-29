USE[HPTGMS5]
GO

/****** Object:  Table [dbo].[CameraInfo]    Script Date: 04/21/2017 14:52:26 ******/
SET ANSI_NULLS ON
GO
TrunCate Table Menus
Insert Menus(MenuId,MenuName,MenuText,ParMenuId,EnableFlag) values(0,	'OperRights',	'菜单权限',	-1,	1)
Insert Menus(MenuId,MenuName,MenuText,ParMenuId,EnableFlag) values(1,	'EBG_Personal',	'人事管理',	0,	1)
Insert Menus(MenuId,MenuName,MenuText,ParMenuId,EnableFlag) values(11,	'BTI_Dept',	'部门设置',	1,	1)
Insert Menus(MenuId,MenuName,MenuText,ParMenuId,EnableFlag) values(12,	'BTI_CardType',	'卡类设置',	1,	1)
Insert Menus(MenuId,MenuName,MenuText,ParMenuId,EnableFlag) values(13,	'BTI_EmpAndCard',	'人员与卡证管理',	1,	1)
Insert Menus(MenuId,MenuName,MenuText,ParMenuId,EnableFlag) values(14,	'BTI_DeviceRights',	'设备权限管理',	1,	1)
Insert Menus(MenuId,MenuName,MenuText,ParMenuId,EnableFlag) values(15,	'BTI_BarCode',	'条码设置',	1,	1)

Insert Menus(MenuId,MenuName,MenuText,ParMenuId,EnableFlag) values(2,	'EBG_Attend',	'考勤管理',	0,	1)
Insert Menus(MenuId,MenuName,MenuText,ParMenuId,EnableFlag) values(21,	'BTI_AttendRules',	'考勤制度设置',	2,	1)
Insert Menus(MenuId,MenuName,MenuText,ParMenuId,EnableFlag) values(22,	'BTI_AttendShifts',	'人员排班',	2,	1)
Insert Menus(MenuId,MenuName,MenuText,ParMenuId,EnableFlag) values(23,	'BTI_AttendData',	'考勤数据处理',	2,	1)
Insert Menus(MenuId,MenuName,MenuText,ParMenuId,EnableFlag) values(24,	'BTI_AttendProc',	'考勤分析处理',	2,	1)
Insert Menus(MenuId,MenuName,MenuText,ParMenuId,EnableFlag) values(25,	'BTI_AttendReports',	'考勤报表中心',	2,	1)

Insert Menus(MenuId,MenuName,MenuText,ParMenuId,EnableFlag) values(4,	'EBG_System',	'系统设置',	0,	1)
Insert Menus(MenuId,MenuName,MenuText,ParMenuId,EnableFlag) values(41,	'BTI_Oper',	'用户管理',	4,	1)
Insert Menus(MenuId,MenuName,MenuText,ParMenuId,EnableFlag) values(42,	'BTI_Password',	'修改密码',	4,	1)
Insert Menus(MenuId,MenuName,MenuText,ParMenuId,EnableFlag) values(43,	'BTI_OperLog',	'操作日志',	4,	1)

Insert Menus(MenuId,MenuName,MenuText,ParMenuId,EnableFlag) values(5,	'EBG_About',	'关于',	0,	1)
Insert Menus(MenuId,MenuName,MenuText,ParMenuId,EnableFlag) values(51,	'BTI_About',	'关于我们',	5,	1)

Truncate table MenuOfOper
Insert MenuOfOper Select 1,MenuId From Menus 