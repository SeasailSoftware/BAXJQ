using HPT.Gate.Utils.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace HPT.Gate.DataAccess.Entity.Service
{
    public class BarcodeService
    {

        #region 生成列表
        public static List<Barcode> ToList()
        {
            List<Barcode> list = new List<Barcode>();
            DataTable dt = GetAll();
            foreach (DataRow row in dt.Rows)
            {
                Barcode barcode = new Barcode();
                barcode.RecId = Convert.ToInt32(row["RecId"]);
                barcode.BarcodeNo = row["BarcodeNo"].ToString();
                string devList = row["DevList"].ToString();
                barcode.DevList = GetDevList(devList);
                barcode.EffectTime = Convert.ToInt32(row["EffectTime"]);
                barcode.TimesOfIn = Convert.ToInt32(row["TimesOfIn"]);
                barcode.TimesOfInLeft = Convert.ToInt32(row["TimesOfInLeft"]);
                barcode.TimesOfOut = Convert.ToInt32(row["TimesOfOut"]);
                barcode.TimesOfOutLeft = Convert.ToInt32(row["TimesOfOutLeft"]);
                barcode.CreateTime = row["CreateTime"].ToString();
                barcode.OutOfTime = row["OutOfTime"].ToString();
                list.Add(barcode);
            }
            return list;
        }
        #endregion

        #region 转换成设备列表编号
        private static List<int> GetDevList(string devList)
        {
            if (string.IsNullOrWhiteSpace(devList))
                devList = "0";
            List<int> list = new List<int>();
            string[] str = devList.Split(',');
            foreach (string s in str)
            {
                int temp = Convert.ToInt32(s);
                list.Add(temp);
            }
            return list;
        }

        #endregion


        #region 获取所有部门
        public static DataTable GetAll()
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "Select * From BarCode";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                return dt;
            }
        }
        #endregion

        #region 更新条码进出次数
        public static void UpdateBarcodeNumofIO(int recId, int ioFlag, int currentNum)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = string.Empty;
                switch (ioFlag)
                {
                    case 3:
                        sql += $"Update Barcode Set TimesOfInLeft = {currentNum} Where RecId ={recId}";
                        break;
                    case 4:
                        sql += $"Update Barcode Set TimesOfOutLeft = {currentNum} Where RecId ={recId}";
                        break;
                }
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }
        #endregion

        #region 获取条码信息
        public static Barcode GetByNumber(string barcodeNo)
        {
            return ToList().FirstOrDefault(p => p.BarcodeNo.Contains(barcodeNo));
        }
        #endregion

        #region 查找条码
        public static List<Barcode> Find(string beginTime, string endTime)
        {
            DateTime dtBegin = Convert.ToDateTime(beginTime);
            DateTime dtEnd = Convert.ToDateTime(endTime);
            return ToList().Where(p => Convert.ToDateTime(p.CreateTime) >= dtBegin && Convert.ToDateTime(p.CreateTime) < dtEnd).ToList();
        }
        #endregion

        #region 检查条码是否已经存在
        public static bool CheckBarcodeExists(string barcode)
        {
            return ToList().Exists(p => barcode.Equals(p.BarcodeNo));
        }
        #endregion

        #region 添加条码
        public static void Insert(Barcode barcode)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                StringBuilder buffer = new StringBuilder();
                for (int i = 0; i < barcode.DevList.Count; i++)
                {
                    buffer.Append(barcode.DevList[i].ToString());
                    if (i < barcode.DevList.Count - 1)
                        buffer.Append(",");
                }
                string recTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string outOfTime = DateTime.Now.AddMinutes(barcode.EffectTime).ToString("yyyy-MM-dd HH:mm:ss");
                string sql = $"{Environment.NewLine}If Not Exists(Select top 1 * From Barcode Where BarcodeNo ='{barcode.BarcodeNo}' )";
                sql += $"{Environment.NewLine}Insert Barcode(BarcodeNo,DevList,EffectTime,TimesOfIn,TimesOfInLeft,TimesOfOut,TimesOfOutLeft,CreateTime,OutOfTime)";
                sql += $"{Environment.NewLine}  Values('{barcode.BarcodeNo}','{buffer.ToString()}',{barcode.EffectTime},{barcode.TimesOfIn},{0},{barcode.TimesOfOut},{0},'{recTime}','{outOfTime}')";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }
        #endregion


    }
}
