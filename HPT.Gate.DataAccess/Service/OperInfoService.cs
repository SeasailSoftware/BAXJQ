using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HPT.Gate.DataAccess.Entity;
using HPT.Gate.Utils.Helper;
using System.Data.Common;
using System.Data;

namespace HPT.Gate.DataAccess.Service
{
    public class OperInfoService
    {
        #region 获取所有操作员
        public static DataTable GetAll()
        {
            List<OperInfo> list = new List<OperInfo>();
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "Select * from OperInfo";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                return dt;
            }
        }
        #endregion


        #region 获取操作员列表
        public static List<OperInfo> ToList()
        {
            List<OperInfo> list = new List<OperInfo>();
            DataTable dt = GetAll();
            foreach (DataRow row in dt.Rows)
            {
                OperInfo oper = new OperInfo();
                oper.OperId = Convert.ToInt32(row["OperId"]);
                oper.OperName = row["OperName"].ToString();
                oper.OperPass = row["OperPassword"].ToString();
                oper.Descr = row["Descr"].ToString();
                list.Add(oper);
            }
            return list;
        }

        #endregion



        #region 获取操作员详细信息
        public static OperInfo GetOperDetail(int operId, string operName = "", string operPass = "")
        {
            List<OperInfo> operList = ToList();
            if (!string.IsNullOrWhiteSpace(operPass))
                return operList.FirstOrDefault(s => s.OperId == operId && operName.Equals(s.OperName) && operPass.Equals(s.OperPass));
            if (!string.IsNullOrWhiteSpace(operName))
                return operList.FirstOrDefault(s => s.OperId == operId && operName.Equals(s.OperName));
            return operList.FirstOrDefault(s => s.OperId == operId);
        }
        #endregion


        #region 修改密码
        public static void Update(OperInfo oper)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = $"Update OperInfo Set OperName ='{oper.OperName}',OperPassword ='{oper.OperPass}' Where OperId ={oper.OperId}";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }
        #endregion

        #region 检查用户名是否存在
        public static bool CheckOperName(string operName)
        {
            return ToList().Exists(s => operName.Equals(s.OperName));
        }
        #endregion

        #region 获取用户信息
        public static OperInfo GetById(int operId)
        {
            return ToList().FirstOrDefault(p => p.OperId == operId);
        }

        #endregion

        #region 删除用户
        public static void Del(int operId)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "DeleteOper";
                DbCommand cmd = dbHelper.GetStoredProcCommond(sql);
                dbHelper.AddInParameter(cmd, "@OperId", DbType.Int32, operId);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }
        #endregion

    }
}
