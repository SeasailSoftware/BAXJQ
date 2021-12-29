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
    public class LeaveTypeService
    {
        public static List<LeaveType> ToList()
        {
            List<LeaveType> list = new List<LeaveType>();
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "Select * From LeaveType";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                foreach (DataRow row in dt.Rows)
                {
                    LeaveType type = new LeaveType();
                    type.TypeId = Convert.ToInt32(row["TypeId"]);
                    type.TypeName = row["TypeName"].ToString();
                    list.Add(type);
                }
            }
            return list;
        }
    }
}
