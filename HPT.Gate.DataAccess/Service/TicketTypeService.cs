using HPT.Gate.Utils.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace HPT.Gate.DataAccess.Entity.Service
{
    public class TicketTypeService
    {
        #region 获取所有票类
        public static DataTable GetAll()
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "Select * From TicketType";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                return dt;
            }
        }
        #endregion

        #region 生成列表
        public static List<TicketType> ToList()
        {
            List<TicketType> ticketTypes = new List<TicketType>();
            DataTable dt = GetAll();
            foreach (DataRow row in dt.Rows)
            {
                TicketType ticketType = new TicketType();
                ticketType.RecId = Convert.ToInt32(row["RecId"]);
                ticketType.Name = row["Name"].ToString();
                ticketType.TypeId = Convert.ToInt32(row["TypeId"]);
                ticketType.BlackName = Convert.ToInt32(row["BlackName"]);
                ticketType.CardType = Convert.ToInt32(row["CardType"]);
                ticketType.InRight = Convert.ToInt32(row["InRight"]);
                ticketType.OutRight = Convert.ToInt32(row["OutRight"]);
                ticketType.VoiceNo = Convert.ToInt32(row["VoiceNo"]);
                ticketType.Photo = Convert.ToInt32(row["Photo"]);
                ticketType.VacationId = Convert.ToInt32(row["VacationId"]);
                ticketType.IntimeGroupNo = Convert.ToInt32(row["IntimeGroupNo"]);
                ticketType.OutTimeGroupNo = Convert.ToInt32(row["OutTimeGroupNo"]);
                ticketType.AntiSubmarine = Convert.ToInt32(row["AntiSubmarine"]);
                ticketType.LimitEnabled = Convert.ToInt32(row["LimitEnabled"]);
                ticketType.TimegroupLimitEnabled = Convert.ToInt32(row["TimegroupLimitEnabled"]);
                ticketType.LimitTypeOfTimeGroupLimit = Convert.ToInt32(row["LimitTypeOfTimeGroupLimit"]);
                ticketType.TimesOfTimeGroupLimit = Convert.ToInt32(row["TimesOfTimeGroupLimit"]);
                ticketType.EffectDateLimitEnabled = Convert.ToInt32(row["EffectDateLimitEnabled"]);
                ticketType.LimitTypeOfEffectDateLimit = Convert.ToInt32(row["LimitTypeOfEffectDateLimit"]);
                ticketType.TimesOfEffectDateLimit = Convert.ToInt32(row["TimesOfEffectDateLimit"]);
                ticketType.LimitTimeEnabled = Convert.ToInt32(row["LimitTimeEnabled"]);
                ticketType.MinutesOfLimitTime = Convert.ToInt32(row["MinutesOfLimitTime"]);
                ticketType.DisplayType1 = Convert.ToInt32(row["DisplayType1"]);
                ticketType.Text1 = row["Text1"].ToString();
                ticketType.Column1 = Convert.ToInt32(row["Column1"]);
                ticketType.Content1 = row["Content1"].ToString();
                ticketType.DisplayType2 = Convert.ToInt32(row["DisplayType2"]);
                ticketType.Text2 = row["Text2"].ToString();
                ticketType.Column2 = Convert.ToInt32(row["Column2"]);
                ticketType.Content2 = row["Content2"].ToString();
                ticketType.DisplayType3 = Convert.ToInt32(row["DisplayType3"]);
                ticketType.Text3 = row["Text3"].ToString();
                ticketType.Column3 = Convert.ToInt32(row["Column3"]);
                ticketType.Content3 = row["Content3"].ToString();
                ticketTypes.Add(ticketType);
            }
            return ticketTypes;
        }

        #endregion

        #region 删除票类
        public static void Del(int recId)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = $"{Environment.NewLine}Delete From TicketType Where RecId = {recId}";
                sql += $"	{Environment.NewLine}Update DevRightOfEmp Set UpdateFlag = 0 Where EmpId in (Select EmpId From EmpInfo Where TicketType ={recId})";
                sql += $"{Environment.NewLine}Update EmpInfo Set TicketType = 1 Where TicketType = {recId}";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }
        #endregion

        #region 添加票类
        public static void Insert(TicketType cardType)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "INSERT INTO TicketType(Name,TypeId,BlackName,CardType,InRight,OutRight,VoiceNo,Photo,VacationId,";
                sql += "IntimeGroupNo,OutTimeGroupNo,AntiSubmarine,LimitEnabled,TimegroupLimitEnabled,";
                sql += "LimitTypeOfTimeGroupLimit,TimesOfTimeGroupLimit,EffectDateLimitEnabled,LimitTypeOfEffectDateLimit,TimesOfEffectDateLimit,LimitTimeEnabled,MinutesOfLimitTime,";
                sql += "DisplayType1,Text1,Column1,Content1,DisplayType2,Text2,Column2,Content2,DisplayType3,Text3,Column3,Content3)";
                sql += $" VALUES ('{cardType.Name}',{cardType.TypeId},{cardType.BlackName},{cardType.CardType},{cardType.InRight},{cardType.OutRight},{cardType.VoiceNo},{cardType.Photo},{cardType.VacationId},";
                sql += $"{cardType.IntimeGroupNo},{cardType.OutTimeGroupNo},{cardType.AntiSubmarine},{cardType.LimitEnabled},{cardType.TimegroupLimitEnabled},";
                sql += $"{cardType.LimitTypeOfTimeGroupLimit},{cardType.TimesOfTimeGroupLimit},{cardType.EffectDateLimitEnabled},{cardType.LimitTypeOfEffectDateLimit},{cardType.TimesOfEffectDateLimit},{cardType.LimitTimeEnabled},{cardType.MinutesOfLimitTime},";
                sql += $"{cardType.DisplayType1},'{cardType.Text1}',{cardType.Column1},'{cardType.Content1}',{cardType.DisplayType2},'{cardType.Text2}',{cardType.Column2},'{cardType.Content2}',{cardType.DisplayType3},'{cardType.Text3}',{cardType.Column3},'{cardType.Content3}')";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }
        #endregion

        #region 加载卡类列表
        public static TicketType GetByRecId(int recId)
        {
            return ToList()?.FirstOrDefault(s =>s!=null&&s.RecId == recId);
        }
        #endregion

        #region 修改卡类信息
        public static void Update(TicketType cardType)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "UpdateTicketType";
                DbCommand cmd = dbHelper.GetStoredProcCommond(sql);
                dbHelper.AddInParameter(cmd, "@RecId", DbType.Int32, cardType.RecId);
                dbHelper.AddInParameter(cmd, "@Name", DbType.String, cardType.Name);
                dbHelper.AddInParameter(cmd, "@TypeId", DbType.Int32, cardType.TypeId);
                dbHelper.AddInParameter(cmd, "@BlackName", DbType.Int32, cardType.BlackName);
                dbHelper.AddInParameter(cmd, "@CardType", DbType.Int32, cardType.CardType);
                dbHelper.AddInParameter(cmd, "@InRight", DbType.Int32, cardType.InRight);
                dbHelper.AddInParameter(cmd, "@OutRight", DbType.Int32, cardType.OutRight);
                dbHelper.AddInParameter(cmd, "@VoiceNo", DbType.Int32, cardType.VoiceNo);
                dbHelper.AddInParameter(cmd, "@Photo", DbType.Int32, cardType.Photo);
                dbHelper.AddInParameter(cmd, "@VacationId", DbType.Int32, cardType.VacationId);
                dbHelper.AddInParameter(cmd, "@IntimeGroupNo", DbType.Int32, cardType.IntimeGroupNo);
                dbHelper.AddInParameter(cmd, "@OutTimeGroupNo", DbType.Int32, cardType.OutTimeGroupNo);
                dbHelper.AddInParameter(cmd, "@AntiSubmarine", DbType.Int32, cardType.AntiSubmarine);
                dbHelper.AddInParameter(cmd, "@LimitEnabled", DbType.Int32, cardType.LimitEnabled);
                dbHelper.AddInParameter(cmd, "@TimegroupLimitEnabled", DbType.Int32, cardType.TimegroupLimitEnabled);
                dbHelper.AddInParameter(cmd, "@LimitTypeOfTimeGroupLimit", DbType.Int32, cardType.LimitTypeOfTimeGroupLimit);
                dbHelper.AddInParameter(cmd, "@TimesOfTimeGroupLimit", DbType.Int32, cardType.TimesOfTimeGroupLimit);
                dbHelper.AddInParameter(cmd, "@EffectDateLimitEnabled", DbType.Int32, cardType.EffectDateLimitEnabled);
                dbHelper.AddInParameter(cmd, "@LimitTypeOfEffectDateLimit", DbType.Int32, cardType.LimitTypeOfEffectDateLimit);
                dbHelper.AddInParameter(cmd, "@TimesOfEffectDateLimit", DbType.Int32, cardType.TimesOfEffectDateLimit);
                dbHelper.AddInParameter(cmd, "@LimitTimeEnabled", DbType.Int32, cardType.LimitTimeEnabled);
                dbHelper.AddInParameter(cmd, "@MinutesOfLimitTime", DbType.Int32, cardType.MinutesOfLimitTime);
                dbHelper.AddInParameter(cmd, "@DisplayType1", DbType.Int32, cardType.DisplayType1);
                dbHelper.AddInParameter(cmd, "@Text1", DbType.String, cardType.Text1);
                dbHelper.AddInParameter(cmd, "@Column1", DbType.Int32, cardType.Column1);
                dbHelper.AddInParameter(cmd, "@Content1", DbType.String, cardType.Content1);
                dbHelper.AddInParameter(cmd, "@DisplayType2", DbType.Int32, cardType.DisplayType2);
                dbHelper.AddInParameter(cmd, "@Text2", DbType.String, cardType.Text2);
                dbHelper.AddInParameter(cmd, "@Column2", DbType.Int32, cardType.Column2);
                dbHelper.AddInParameter(cmd, "@Content2", DbType.String, cardType.Content2);
                dbHelper.AddInParameter(cmd, "@DisplayType3", DbType.Int32, cardType.DisplayType3);
                dbHelper.AddInParameter(cmd, "@Text3", DbType.String, cardType.Text3);
                dbHelper.AddInParameter(cmd, "@Column3", DbType.Int32, cardType.Column3);
                dbHelper.AddInParameter(cmd, "@Content3", DbType.String, cardType.Content3);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }
        #endregion
    }
}
