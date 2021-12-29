USE [HPTGMS30]
GO

/****** Object:  Table [dbo].[CardInfo]    Script Date: 09/28/2017 16:55:46 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CardInfo]') AND type in (N'U'))
DROP TABLE [dbo].[CardInfo]
GO

USE [HPTGMS30]
GO

/****** Object:  Table [dbo].[CardInfo]    Script Date: 09/28/2017 16:55:46 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CardInfo](
	[cardId] [int] IDENTITY(0,1) NOT FOR REPLICATION NOT NULL,
	[Type] [int] NOT NULL,
	[TicketType] [int] NOT NULL,
	[CardNo] [varchar](20) NOT NULL,
	[EmpId] [int] NOT NULL,
	[BeginDate] [varchar](20) NOT NULL,
	[EndDate] [varchar](20) NOT NULL,
	[CardStatus] [int] NOT NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

USE [HPTGMS30]
GO

/****** Object:  StoredProcedure [dbo].[AddCardNew]    Script Date: 09/28/2017 17:21:40 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AddCardNew]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[AddCardNew]
GO

USE [HPTGMS30]
GO

/****** Object:  StoredProcedure [dbo].[AddCardNew]    Script Date: 09/28/2017 17:21:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

Create  PROCEDURE [dbo].[AddCardNew](@EmpId varchar(30),@TicketType int,@Type int,@CardNo varchar(50),@beginDate varchar(20),@endDate varchar(20))
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
		insert into CardInfo(Type,TicketType,CardNo,EmpId,BeginDate,EndDate) Values(@Type,@TicketType,@CardNo,@EmpId,@BeginDate,@EndDate)
		Return
	End 
	
	--其次判断是否存在已经注销的卡号
	if exists(Select top 1 * from CardInfo where CardNo ='FFFFFFFF' and CardStatus = 0)
	Begin
		select top 1 @cardid = CardId from cardinfo where cardstatus = 0 and CardNo = 'FFFFFFFF'
		Delete From EmpRightOfDevice Where CardId = @CardId
		Update CardInfo Set CardStatus = 1,Type = @Type,TicketType =@TicketType,CardNo = @CardNo,EmpId =@EmpId,BeginDate = @BeginDate,EndDate = @EndDate
			Where CardId = @CardId
		Return
	End 
		
	--不存在注销的卡则直接插入
	insert into CardInfo(Type,TicketType,CardNo,EmpId,BeginDate,EndDate) Values(@Type,@TicketType,@CardNo,@EmpId,@BeginDate,@EndDate)
		
END

GO




