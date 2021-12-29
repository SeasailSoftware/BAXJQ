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
    public class VoiceService
    {
        #region 获取所有部门
        public static DataTable GetAll()
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "Select * From Voice";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                return dt;
            }
        }
        #endregion

        #region 生成列表
        public static List<Voice> ToList()
        {
            List<Voice> voiceList = new List<Voice>();
            DataTable dt = GetAll();
            foreach (DataRow row in dt.Rows)
            {
                Voice voice = new Voice();
                voice.Vid = Convert.ToInt32(row["Vid"]);
                voice.VName = row["VName"].ToString();
                voiceList.Add(voice);
            }
            return voiceList;
        }
        #endregion
    }
}
