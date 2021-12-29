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
    public class CameraOfDeviceService
    {

        #region 生成列表
        public static List<CameraOfDevice> ToList()
        {
            List<CameraOfDevice> list = new List<CameraOfDevice>();
            DataTable dt = GetAll();
            foreach (DataRow row in dt.Rows)
            {
                CameraOfDevice obj = new CameraOfDevice();
                obj.RecId = Convert.ToInt32(row["RecId"]);
                obj.DeviceId = Convert.ToInt32(row["DeviceId"]);
                obj.DeviceName = row["DeviceName"].ToString();
                obj.InCamId = Convert.ToInt32(row["InCamId"]);
                obj.InCamName = row["InCamName"].ToString();
                obj.OutCamId = Convert.ToInt32(row["OutCamId"]);
                obj.OutCamName = row["OutCamName"].ToString();
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
                string sql = "Select * From CameraOfDevice";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                return dt;
            }
        }
        #endregion

        #region 检查设备是否已经绑定了摄像头
        public static bool CheckCamOfDevice(int devId)
        {
            return ToList().Exists(p => p.DeviceId == devId);
        }
        #endregion

        #region 添加绑定
        public static void Insert(CameraOfDevice binding)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = string.Format("Insert Into CameraOfDevice(DeviceId,DeviceName,InCamId,InCamName,OutCamId,OutCamName) Values({0},'{1}',{2},'{3}',{4},'{5}')", binding.DeviceId, binding.DeviceName, binding.InCamId, binding.InCamName, binding.OutCamId, binding.OutCamName);
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }
        #endregion

        #region 获取绑定信息
        public static CameraOfDevice GetByRecId(int _RecId)
        {
            return ToList().FirstOrDefault(p => p.RecId == _RecId);
        }

        #endregion

        #region 更新绑定关系
        public static void Update(CameraOfDevice binding)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = string.Format("Update CameraOfDevice Set InCamId = {0},InCamName ='{1}',OutCamId = {2},OutCamName ='{3}' where DeviceId ={4}", binding.InCamId, binding.InCamName, binding.OutCamId, binding.OutCamName, binding.DeviceId);
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }

        #endregion

        #region 删除绑定关系
        public static void Del(int devId)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = string.Format("Delete From CameraOfDevice Where DeviceId = {0}", devId);
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }
        #endregion

    }
}
