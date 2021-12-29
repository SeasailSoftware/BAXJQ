using HPT.Gate.DataAccess.Entity;
using HPT.Gate.DataAccess.Entity.Entity;
using HPT.Gate.Utils.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace hpt.gate.DataAccess.Service
{
    public class CardUpdateService
    {
        static CardUpdateService()
        {
            try
            {
                using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
                {
                    StringBuilder buffer = new StringBuilder();
                    buffer.AppendLine($"ALTER PROCEDURE[dbo].[GetCardListUpdate] (@DeviceId int,@EmpId int,@Rights int)");
                    buffer.AppendLine($"AS");
                    buffer.AppendLine($"BEGIN");
                    buffer.AppendLine($"SET NOCOUNT ON;");
                    buffer.AppendLine($"declare @count int,@BlackName int");
                    buffer.AppendLine($"--卡总数");
                    buffer.AppendLine($"select @count =isnull(Max(EmpId) *5,0) From EmpInfo");
                    buffer.AppendLine($"if(@Count =0) return");
                    buffer.AppendLine($"Create Table #Card(EmpId int,Type int,CardNo varchar(20),CardId int,TotalNum int,BlackName int,CardType int,CardCode varchar(20),Row1 varchar(30),Row2 varchar(30),Row3 varchar(30),");
                    buffer.AppendLine($" InRight int,OutRight int,VoiceNo int,Photo int,VacationId int,InTimeGroupNo int,OutTimeGroupNo int,BeginDate varchar(30),EndDate varchar(30),FingerData Image)");
                    buffer.AppendLine($"--人员已删除");
                    buffer.AppendLine($"Select @BlackName = case @Rights when 1 then 0 else 1 end");
                    buffer.AppendLine($"Declare @TicketType int,@Content1 varchar(20),@Content2 varchar(20),@Content3 varchar(20)");
                    buffer.AppendLine($"Create Table #Content(Content1 varchar(30),Content2 varchar(30),Content3 varchar(30))");
                    buffer.AppendLine($"Select Top 1 @TicketType = TicketType From EmpInfo where EmpId = @EmpId");
                    buffer.AppendLine($"Insert #Content Exec GetDisplayContent @EmpId,@TicketType");
                    buffer.AppendLine($"Select Top 1 @Content1 = Content1,@Content2 =Content2,@Content3 = Content3 From #Content");
                    buffer.AppendLine($"Insert #Card Select @EmpId,1,case b.ICCardNo when '' then 'FFFFFFFF' else b.ICCardNo end,(@EmpId-1)*5,@Count,@BlackName,a.CardType,@EmpId,@Content1,@Content2,@Content3,");
                    buffer.AppendLine($"a.InRight, a.OutRight, a.VoiceNo, a.Photo, a.VacationId, a.InTimeGroupNo, a.OutTimeGroupNo, b.BeginDate, b.EndDate ,0x00 From TicketType a, EmpInfo b");
                    buffer.AppendLine($"Where a.RecId = b.TicketType  And b.EmpId = @EmpId");
                    buffer.AppendLine($"Insert #Card Select @EmpId,2,case b.IDSerial when '' then 'FFFFFFFF' else b.IDSerial end,(@EmpId-1)*5+1,@Count,@BlackName,a.CardType,@EmpId,@Content1,@Content2,@Content3,");
                    buffer.AppendLine($"a.InRight, a.OutRight, a.VoiceNo, a.Photo, a.VacationId, a.InTimeGroupNo, a.OutTimeGroupNo, b.BeginDate, b.EndDate ,0x00 From TicketType a, EmpInfo b");
                    buffer.AppendLine($"Where a.RecId = b.TicketType  And b.EmpId = @EmpId");
                    buffer.AppendLine($" Insert #Card Select @EmpId,3,case b.IDCardNo when '' then 'FFFFFFFF' else b.IDCardNo end,(@EmpId-1)*5+2,@Count,@BlackName,a.CardType,@EmpId,@Content1,@Content2,@Content3,");
                    buffer.AppendLine($"a.InRight, a.OutRight, a.VoiceNo, a.Photo, a.VacationId, a.InTimeGroupNo, a.OutTimeGroupNo, b.BeginDate, b.EndDate ,0x00 From TicketType a, EmpInfo b");
                    buffer.AppendLine($"Where a.RecId = b.TicketType  And b.EmpId = @EmpId");
                    buffer.AppendLine($"Insert #Card Select @EmpId,5,dbo.IntToHex((@EmpId-1)*5+3),(@EmpId-1)*5+3,@Count,@BlackName,a.CardType,@EmpId,@Content1,@Content2,@Content3,");
                    buffer.AppendLine($"a.InRight, a.OutRight, a.VoiceNo, a.Photo, a.VacationId, a.InTimeGroupNo, a.OutTimeGroupNo, b.BeginDate, b.EndDate , b.FingerData1 From TicketType a, EmpInfo b");
                    buffer.AppendLine($"Where a.RecId = b.TicketType  And b.EmpId = @EmpId");
                    buffer.AppendLine($" Insert #Card Select @EmpId,6,dbo.IntToHex((@EmpId-1)*5+4),(@EmpId-1)*5+4,@Count,@BlackName,a.CardType,@EmpId,@Content1,@Content2,@Content3,");
                    buffer.AppendLine($" a.InRight, a.OutRight, a.VoiceNo, a.Photo, a.VacationId, a.InTimeGroupNo, a.OutTimeGroupNo, b.BeginDate, b.EndDate ,0x00 From TicketType a, EmpInfo b");
                    buffer.AppendLine($"Where a.RecId = b.TicketType  And b.EmpId = @EmpId");
                    buffer.AppendLine($"Select* From #Card");
                    buffer.AppendLine($"END");
                    DbCommand cmd = dbHelper.GetSqlStringCommond(buffer.ToString());
                    dbHelper.ExecuteNonQuery(cmd);
                }
            }
            catch
            {

            }

        }

        #region 获取人员同步卡列表
        public static List<CardUpdate> GetCardUpdateList(DataSynTask task)
        {

            List<CardUpdate> qdCard = new List<CardUpdate>();
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "GetCardListUpdate";
                DbCommand cmd = dbHelper.GetStoredProcCommond(sql);
                dbHelper.AddInParameter(cmd, "@DeviceId", DbType.Int32, task.DeviceId);
                dbHelper.AddInParameter(cmd, "@EmpId", DbType.Int32, task.EmpId);
                dbHelper.AddInParameter(cmd, "@Rights", DbType.Int32, task.Rights);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                foreach (DataRow row in dt.Rows)
                {
                    CardUpdate card = new CardUpdate();
                    card.EmpId = Convert.ToInt32(row["EmpId"]);
                    card.Type = Convert.ToInt32(row["Type"]);
                    card.CardNo = row["CardNo"].ToString();
                    if (string.IsNullOrEmpty(card.CardNo))
                        card.CardNo = "FFFFFFFF";
                    card.CardId = Convert.ToInt32(row["CardId"]);
                    card.TotalNum = Convert.ToInt32(row["TotalNum"]);
                    card.BlackName = Convert.ToInt32(row["BlackName"]);
                    card.CardType = Convert.ToInt32(row["CardType"]);
                    card.CardCode = Convert.ToInt32(row["CardCode"]);
                    card.Row1 = row["Row1"].ToString();
                    card.Row2 = row["Row2"].ToString();
                    card.Row3 = row["Row3"].ToString();
                    card.InRight = Convert.ToInt32(row["InRight"]);
                    card.OutRight = Convert.ToInt32(row["OutRight"]);
                    card.VoiceNo = Convert.ToInt32(row["VoiceNo"]);
                    card.Photo = Convert.ToInt32(row["Photo"]);
                    card.VacationId = Convert.ToInt32(row["VacationId"]);
                    card.InTimeGroupNo = Convert.ToInt32(row["InTimeGroupNo"]);
                    card.OutTimeGroupNo = Convert.ToInt32(row["OutTimeGroupNo"]);
                    card.BeginDate = row["BeginDate"].ToString();
                    card.EndDate = row["EndDate"].ToString();
                    card.FingerPrintData = (byte[])row["FingerData"];
                    qdCard.Add(card);

                }
            }
            return qdCard;
        }

        #endregion

    }
}
