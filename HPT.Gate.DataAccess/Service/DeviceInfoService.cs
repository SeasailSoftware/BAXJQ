using HPT.Gate.Utils.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace HPT.Gate.DataAccess.Entity.Service
{
    public class DeviceInfoService
    {
        static DeviceInfoService()
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                try
                {
                    StringBuilder buffer = new StringBuilder();
                    buffer.AppendLine($"If COL_LENGTH('DeviceInfo','DeviceType') Is Null");
                    buffer.AppendLine($"Alter Table DeviceInfo Add DeviceType int Not Null Default (0)");
                    DbCommand cmd = dbHelper.GetSqlStringCommond(buffer.ToString());
                    dbHelper.ExecuteNonQuery(cmd);
                }
                catch (Exception ex)
                {
                    var val = ex.Message;
                }
            }
        }
        #region 生成列表
        public static List<DeviceInfo> ToList()
        {
            List<DeviceInfo> devList = new List<DeviceInfo>();
            DataTable dt = GetAll();
            foreach (DataRow row in dt.Rows)
            {
                DeviceInfo device = new DeviceInfo();
                device.DeviceType = Convert.ToInt32(row["DeviceType"]);
                device.DeviceId = Convert.ToInt32(row["DeviceId"]);
                device.DeviceName = row["DeviceName"].ToString();
                device.PlaceId = Convert.ToInt32(row["PlaceId"]);
                device.Mac = row["Mac"].ToString();
                device.ServerIP = row["ServerIP"].ToString();
                device.ServerPort = Convert.ToInt32(row["ServerPort"]);
                device.IPAddress = row["IPAddress"].ToString();
                device.SubNet = row["SubNet"].ToString();
                device.GateWay = row["GateWay"].ToString();
                device.Port = Convert.ToInt32(row["Port"]);
                device.HardVersion = row["HardVersion"].ToString();
                device.SoftVersion = row["SoftVersion"].ToString();
                devList.Add(device);
            }
            return devList;
        }
        #endregion

        #region 获取所有部门
        public static DataTable GetAll()
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "Select * From DeviceInfo";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                return dt;
            }
        }
        #endregion

        #region 获取树形结构
        public static DataTable GetDeptTreeDable()
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "GetDeviceAndPlaceTree";
                DbCommand cmd = dbHelper.GetStoredProcCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                return dt;
            }
        }
        #endregion

        #region 获取可用的设备编号
        public static int GetAvarilableDevId()
        {
            int max = 0;
            List<DeviceInfo> devList = ToList();
            if (devList.Count > 0)
            {
                max = ToList().Max(p => p.DeviceId);
            }
            return max + 1;
        }
        #endregion

        #region 检查设备是否已经存在
        public static bool CheckExists(string mac)
        {
            List<DeviceInfo> devList = ToList();
            if (devList.Count == 0)
                return false;
            return ToList().Exists(p => mac.Equals(p.Mac));
        }

        #endregion

        #region 添加设备信息
        public static bool Insert(DeviceInfo device, out string msg)
        {
            List<DeviceInfo> devices = ToList();
            if (devices.Exists(p => p.DeviceId == device.DeviceId))
            {
                msg = $"机器号[{device.DeviceId}]已重复!";
                return false;
            }
            if (devices.Exists(p => p.IPAddress.Equals(device.IPAddress)))
            {
                msg = $"IP地址[{device.IPAddress}]已经存在!";
                return false;
            }
            if (devices.Exists(p => p.Mac.Equals(device.Mac)))
            {
                msg = $"物理地址[{device.Mac}]已经存在!";
                return false;
            }
            try
            {
                using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
                {
                    StringBuilder buffer = new StringBuilder();
                    buffer.AppendLine($"Insert Into DeviceInfo(DeviceId,DeviceName,PlaceId,DeviceType,MAC,ServerIp,ServerPort,IPAddress,SubNet,GateWay,Port,HardVersion,SoftVersion)");
                    buffer.AppendLine($"Values({device.DeviceId}, '{device.DeviceName}', {device.PlaceId}, {device.DeviceType},'{device.Mac}', '{device.ServerIP}', {device.ServerPort}, '{device.IPAddress}', '{device.SubNet}','{device.GateWay}', {device.Port}, '{device.HardVersion}', '{device.SoftVersion}')");
                    DbCommand cmd = dbHelper.GetSqlStringCommond(buffer.ToString());
                    dbHelper.ExecuteNonQuery(cmd);
                }
                msg = "添加成功!";
                return true;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return false;
            }
        }

        #endregion

        #region 获取设备信息
        public static DeviceInfo GetByDeviceId(int devId)
        {
            return ToList().FirstOrDefault(p => p.DeviceId == devId);
        }
        #endregion

        #region 更新设备信息
        public static bool Update(DeviceInfo device, out string msg)
        {
            List<DeviceInfo> devices = ToList();
            if (devices.Exists(p => p.DeviceId != device.DeviceId && p.IPAddress.Equals(device.IPAddress)))
            {
                msg = $"IP地址[{device.IPAddress}]重复!";
                return false;
            }
            if (devices.Exists(p => p.DeviceId != device.DeviceId && p.Mac.Equals(device.Mac)))
            {
                msg = $"物理地址Mac[{device.Mac}]重复!";
                return false;
            }
            try
            {
                using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
                {
                    StringBuilder buffer = new StringBuilder();
                    buffer.Append($"Update DeviceInfo Set DeviceName ='{device.DeviceName}',PlaceId ={device.PlaceId},DeviceType ={device.DeviceType},Mac='{device.Mac}',IPAddress='{device.IPAddress}',");
                    buffer.AppendLine($"   SubNet = '{device.SubNet}', GateWay = '{device.GateWay}', Port ={device.Port}, HardVersion = '{device.HardVersion}', SoftVersion = '{device.SoftVersion}'");
                    buffer.AppendLine($"Where DeviceId ={device.DeviceId}");
                    DbCommand cmd = dbHelper.GetSqlStringCommond(buffer.ToString());
                    dbHelper.ExecuteNonQuery(cmd);
                }
                msg = "Success!";
                return true;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return false;
            }
        }

        #endregion

        #region 删除设备
        public static void Del(int devId)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "DeleteDeviceInfo";
                DbCommand cmd = dbHelper.GetStoredProcCommond(sql);
                dbHelper.AddInParameter(cmd, "@DeviceId", DbType.Int32, devId);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }
        #endregion

        #region 获取同步任务列表
        public static List<DataSynTask> GetDataSynTasks(int deviceId)
        {
            List<DataSynTask> taskList = new List<DataSynTask>();
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = $"Select * From DevRightOfEmp Where UpdateFlag = 0 And DeviceId ={deviceId}";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                foreach (DataRow row in dt.Rows)
                {
                    DataSynTask task = new DataSynTask();
                    task.RecId = Convert.ToInt32(row["RecId"]);
                    task.EmpId = Convert.ToInt32(row["EmpId"]);
                    task.DeviceId = deviceId;
                    task.Rights = Convert.ToInt32(row["Rights"]);
                    task.UpdateFlag = Convert.ToInt32(row["UpdateFlag"]);
                    taskList.Add(task);
                }
            }
            return taskList;
        }
        #endregion

        #region 获取同步任务列表
        public static void ChangeDataSynTaskStatus(int recId)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = $"Update DevRightOfEmp Set UpdateFlag = 1 Where RecId ={recId}";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }
        #endregion
    }
}
