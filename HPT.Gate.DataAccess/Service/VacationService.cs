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
    public class VacationService
    {
        #region 生成列表
        public static List<Vacation> ToList()
        {
            List<Vacation> list = new List<Vacation>();
            DataTable dt = GetAll();
            foreach (DataRow row in dt.Rows)
            {
                Vacation obj = new Vacation();
                obj.Vid = Convert.ToInt32(row["Vid"]);
                obj.VName = row["VName"].ToString();
                obj.VBeginDate = row["VBeginDate"].ToString();
                obj.VEndDate = row["VEndDate"].ToString();
                obj.VDesc = row["VDesc"].ToString();
                obj.Status = Convert.ToInt32(row["Status"]);
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
                string sql = "Select * From Vacation ";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                return dt;
            }
        }
        #endregion

        #region 获取有效列表
        public static List<Vacation> GetEffectList()
        {
            return ToList().Where(s => s.Status == 1).ToList();
        }

        #endregion

        #region 添加节假日
        public static void Insert(Vacation vacation)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                StringBuilder buffer = new StringBuilder();
                buffer.AppendLine($"Declare @Vid int");
                buffer.AppendLine($"select @Vid = min(Vid) from Vacation where status = 0");
                buffer.AppendLine($"Update Vacation set status = 1, VName ='{vacation.VName}', VBeginDate ='{vacation.VBeginDate}', VEndDate ='{vacation.VEndDate}', VDesc ='{vacation.VDesc}' where Vid = @Vid and Status = 0");
                DbCommand cmd = dbHelper.GetSqlStringCommond(buffer.ToString());
                dbHelper.ExecuteNonQuery(cmd);
            }
        }
        #endregion

        #region 获取节假日信息
        public static Vacation GetByVid(int _Vid)
        {
            return GetEffectList().FirstOrDefault(p => p.Vid == _Vid);
        }
        #endregion

        #region 更新节假日
        public static void Update(Vacation vacation)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = $"update  vacation set vname ='{vacation.VName}',VBeginDate ='{vacation.VBeginDate}',VEndDate ='{vacation.VEndDate}',Vdesc ='{vacation.VDesc}'  where vid ={vacation.Vid}";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }

        #endregion

        #region 删除节假日
        public static void Del(int id)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = $" Update vacation set status = 0 where vid ={id}";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }
        #endregion

    }
}
