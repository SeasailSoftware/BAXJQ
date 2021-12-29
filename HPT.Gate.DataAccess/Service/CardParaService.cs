using HPT.Gate.DataAccess.Entity.Entity;
using HPT.Gate.Utils.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace HPT.Gate.DataAccess.Entity.Service
{
    public class CardParaService
    {
        #region 获取所有部门
        public static DataTable GetAll()
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "Select * From CardPara";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                return dt;
            }
        }
        #endregion

        #region 生成列表
        public static List<CardPara> ToList()
        {
            List<CardPara> cardParas = new List<CardPara>();
            DataTable dt = GetAll();
            foreach (DataRow row in dt.Rows)
            {
                CardPara cardPara = new CardPara();
                cardPara.Cid = Convert.ToInt32(row["Cid"]);
                cardPara.CName = row["CName"].ToString();
                cardPara.ColumnName = row["ColumnName"].ToString();
                cardParas.Add(cardPara);
            }
            return cardParas;
        }
        #endregion
    }
}
