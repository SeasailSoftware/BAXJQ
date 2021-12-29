USE [HPTGate_S_V_2_7_3]
GO

/****** Object:  Table [dbo].[CameraInfo]    Script Date: 04/21/2017 14:52:26 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON

SET ANSI_PADDING ON
GO
GO
If Exists(select * from sysobjects where name='Cam_CameraInfo' and xtype='U')  
	Drop Table Cam_CameraInfo
GO
CREATE TABLE [dbo].[Cam_CameraInfo](
	[CamId] [int] IDENTITY(1,1) NOT NULL,
	[CamName] [varchar](20) NOT NULL,
	[IPAddress] [varchar](20) NOT NULL,
	[Port] [int] NOT NULL,
	[UserName] [varchar](20) NULL,
	[Password] [varchar](20) NULL,
	[Mark] [varchar](20) NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

USE [HPTGate_S_V_2_7_3]
GO

/****** Object:  Table [dbo].[CameraOfDevice]    Script Date: 04/21/2017 14:56:25 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO
If Exists(select * from sysobjects where name='Cam_CameraOfDevice' and xtype='U')  
	Drop Table Cam_CameraOfDevice
GO
CREATE TABLE [dbo].[Cam_CameraOfDevice](
	[RecId] [int] IDENTITY(1,1) NOT NULL,
	[InCamId] [int] NOT NULL,
	[InCamName] [varchar](30) NOT NULL,
	[DeviceId] [int] NOT NULL,
	[OutCamId] [int] NOT NULL,
	[OutCamName] [varchar](30) NOT NULL,
	[DeviceName] [varchar](30) NOT NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


