using HPT.Gate.DataAccess.Entity;
using HPT.Gate.Utils.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace HPT.Gate.DataAccess.Service
{
    public class LedDynParaService
    {
        #region 生成列表
        public static List<LedDynPara> ToList()
        {
            List<LedDynPara> list = new List<LedDynPara>();
            DataTable dt = GetAll();
            foreach (DataRow row in dt.Rows)
            {
                LedDynPara obj = new LedDynPara();
                obj.RecId = Convert.ToInt32(row["RecId"]);
                obj.ParaId = Convert.ToInt32(row["ParaId"]);
                obj.ParaName = row["ParaName"].ToString();
                obj.ParaSql = row["ParaSql"].ToString();
                list.Add(obj);
            }
            return list;
        }
        #endregion

        #region 获取所有部门
        public static DataTable GetAll()
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "Select * From Led_DynPara";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                return dt;
            }
        }
        #endregion
    }
}
