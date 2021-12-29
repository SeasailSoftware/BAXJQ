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
    public class MenuOfOperService
    {
        #region 生成列表
        public static List<MenuOfOper> ToList()
        {
            List<MenuOfOper> list = new List<MenuOfOper>();
            DataTable dt = GetAll();
            foreach (DataRow row in dt.Rows)
            {
                MenuOfOper obj = new MenuOfOper();
                obj.Id = Convert.ToInt32(row["Id"]);
                obj.OperId = Convert.ToInt32(row["OperId"]);
                obj.MenuId = Convert.ToInt32(row["MenuId"]);
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
                string sql = "Select * From MenuOfOper";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                return dt;
            }
        }
        #endregion

        #region 添加用户对菜单的权限
        public static void Insert(MenuOfOper menuOfOper)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = $"Insert into MenuOfOper(OperId,MenuId) values({menuOfOper.OperId},{menuOfOper.MenuId})";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }
        #endregion

        #region 删除操作员对应的菜单权限
        public static void Del(int operId)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = $"Delete from MenuOfOper where OperId ={operId}";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }
        #endregion

    }
}
