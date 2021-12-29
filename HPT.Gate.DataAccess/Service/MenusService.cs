using HPT.Gate.DataAccess.Entity;
using HPT.Gate.DataAccess.Service;
using HPT.Gate.Utils.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace HPT.Gate.DataAccess.Entity.Service
{
    public class MenusService
    {
        #region 菜单树数据集
        public static DataTable GetMenuTree()
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "select MenuId as id,MenuText as name,ParMenuId as parid,4 as ImageIndex  from Menus Where EnableFlag = 1 ";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                dt.TableName = "TableName";
                return dt;
            }
        }
        #endregion


        #region 获取所有菜单列表
        public static List<Menus> ToList()
        {
            List<Menus> menuList = new List<Menus>();
            DataTable dt = GetAll();
            foreach (DataRow row in dt.Rows)
            {
                Menus menu = new Menus();
                menu.MenuId = Convert.ToInt32(row["MenuId"]);
                menu.MenuName = row["MenuName"].ToString();
                menu.MenuText = row["MenuText"].ToString();
                menu.ParMenuId = Convert.ToInt32(row["ParMenuId"]);
                menu.Enabled = Convert.ToInt32(row["EnableFlag"]);
                menuList.Add(menu);
            }
            return menuList;
        }
        #endregion


        #region 获取操作员对应的菜单
        public static List<Menus> GetMenusByOperId(int operId)
        {
            List<MenuOfOper> menuOfOperList = MenuOfOperService.ToList().Where(p => p.OperId == operId).ToList();
            return ToList().Where(p => menuOfOperList.Exists(s => s.MenuId == p.MenuId)).ToList();
        }

        public static void Insert(OperInfo oper)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = $"insert into OperInfo(OperName,OperPassword) values ('{oper.OperName}','{oper.OperPass}')";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }
        #endregion

        #region 获取所有操作员
        public static DataTable GetAll()
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "Select * From Menus";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                return dt;
            }
        }
        #endregion

        public static void DeleteMenusOfOper(int operId)
        {
            throw new NotImplementedException();
        }

        public static void InsertMenusOfOper(int operId, int menuId)
        {
            throw new NotImplementedException();
        }

    }
}
