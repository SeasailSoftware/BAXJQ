
Use HPTSoft
GO
if Not exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Student]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
-- 删除表
CREATE TABLE [dbo].[Student](
	[RecId] [int] IDENTITY(1,1) NOT NULL,
	[MainKey] [int] NOT NULL,
	[SchoolName] [varchar](50) NULL,
	[SchoolType] [varchar](50) NULL,
	[JoinYear] [varchar](50) NULL,
	[ClassName] [varchar](50) NULL,
	[StudentCode] [varchar](30) NULL,
	[StudentName] [varchar](50) NULL,
	[StudentType] [varchar](30) NULL,
	[IDCardNo] [varchar](20) NULL,
	[SerialNo] [varchar](20) NOT NULL,
	[Telephone] [varchar](20) NULL,
	[PerantPhone1] [varchar](20) NULL,
	[PerantPhone2] [varchar](20) NULL
) ON [PRIMARY]
GO
if Not exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[RecordOfSyned]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
-- 删除表
CREATE TABLE [dbo].[RecordOfSyned](
	[RecId] [int] IDENTITY(1,1) NOT NULL,
	[SynedRecId] [int] NOT NULL,
	[SchoolId] [varchar](30) NOT NULL,
	[StudentId] [int] NOT NULL,
	[PayType] [int] NOT NULL,
	[PayMoney] [varchar](30) NOT NULL,
	[Mark] [varchar](50) NULL,
	[RecDateTime] [varchar](30) NOT NULL,
	[ResultCode] [varchar](30) NOT NULL,
	[ResultMessage] [varchar](100) NOT NULL
) ON [PRIMARY]

GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[GetUnSynedRecords]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
-- 删除存储过程
drop procedure [dbo].GetUnSynedRecords
GO

/****** Object:  StoredProcedure [dbo].[GetUnSynedRecords]    Script Date: 06/13/2017 14:20:12 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[GetUnSynedRecords]
AS
BEGIN
SET NOCOUNT ON;
	Declare @Today varchar(20)
	Select @Today = Convert(varchar(20),getdate(),23)
	Create table #Record(RecId int,StudentId int,Account varchar(20),PayType int,PayMoney decimal,Mark varchar(50),RecDateTime varchar(20))
	If Not Exists(Select top 1 * From RecordOfSyned where RecDateTime >= @Today )
		--Insert #Record 
			Select c.RecId,isnull(d.MainKey,0) as StudentId,c.CardSeriaNum as Account,c.DevNo,c.PayType,c.PayMoney,c.Mark,c.RecDateTime 
				from (Select a.RecId,a.DevNo,b.CardSeriaNum, (case a.DealType when '增款' then 0 when '消费' then 1 else 2 end ) As PayType,a.Money as PayMoney,'' as Mark,a.DealTime As RecDatetime from RecordInfo a,PersonInfo b where a.PerId = b.Id and a.DealTime > @Today) c Left Join Student d 
			On c.CardSeriaNum = d.SerialNo 
	Else
		--Insert #Record 
			Select c.RecId,isnull(d.MainKey,0) as StudentId,c.CardSeriaNum as Account,c.DevNo,c.PayType,c.PayMoney,c.Mark,c.RecDateTime From  (Select a.DevNo, a.RecId,b.CardSeriaNum, (case a.DealType when '增款' then 0 when '消费' then 1 else 2 end ) As PayType,a.Money as PayMoney,'' as Mark,a.DealTime As RecDatetime from RecordInfo a,PersonInfo b 
			where a.PerId = b.Id  and a.DealTime > @Today And a.RecId not In (Select SynedRecId From RecordOfSyned where RecDateTime >= @Today)) c Left Join Student d 
			On c.CardSeriaNum = d.SerialNo 
	--Select * from #Record Order By RecId
END

GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[InsertStudent]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
-- 删除存储过程
drop procedure [dbo].InsertStudent
GO

/****** Object:  StoredProcedure [dbo].[InsertStudent]    Script Date: 06/13/2017 14:21:38 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[InsertStudent](@MainKey int ,@SchoolName varchar(50),@SchoolType varchar(50),@JoinYear varchar(50),@ClassName varchar(50),@StudentCode varchar(50),
	@StudentName varchar(50),@StudentType varchar(50),@IDCardNo varchar(50),@SerialNo varchar(50),@Telephone varchar(50),@PerantPhone1 varchar(50),@PerantPhone2 varchar(50))
AS
BEGIN
SET NOCOUNT ON;
	If Exists( Select top 1 * from Student Where MainKey = @MainKey)
		Update Student Set SchoolName = @SchoolName,SchoolType =@SchoolType,JoinYear = @JoinYear,ClassName = @ClassName,StudentCode = @StudentCode,StudentName = @StudentName,
			StudentType = @StudentType,IDCardNo = @IDCardNo,SerialNo = @SerialNo,Telephone = @Telephone,PerantPhone1 = @PerantPhone1,PerantPhone2 = @PerantPhone2
		Where MainKey = @MainKey
	Else
		Insert Student(MainKey,SchoolName,SchoolType,JoinYear,ClassName,StudentCode,StudentName,StudentType,IDCardNo,SerialNo,Telephone,PerantPhone1,PerantPhone2)
			Values(@MainKey,@SchoolName,@SchoolType,@JoinYear,@ClassName,@StudentCode,@StudentName,@StudentType,@IDCardNo,@SerialNo,@Telephone,@PerantPhone1,@PerantPhone2)
END


GO