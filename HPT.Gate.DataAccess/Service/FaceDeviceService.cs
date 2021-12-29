using HPT.Gate.DataAccess.Entity;
using HPT.Gate.Utils.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Net;

namespace HPT.Gate.DataAccess.Service
{
    public class FaceDeviceService
    {
        #region 生成列表
        public static List<FaceDevice> ToList()
        {
            List<FaceDevice> list = new List<FaceDevice>();
            DataTable dt = GetAll();
            foreach (DataRow row in dt.Rows)
            {
                FaceDevice obj = new FaceDevice();
                obj.DeviceId = Convert.ToInt32(row["DeviceId"]);
                obj.IPAddress = row["IPAddress"].ToString();
                obj.Mac = row["Mac"].ToString();
                obj.Port = Convert.ToInt32(row["Port"]);
                obj.SN = row["SN"].ToString();
                obj.Password = row["Password"].ToString();
                list.Add(obj);
            }
            return list;
        }
        #endregion

        #region 获取所有人脸识别机器
        public static DataTable GetAll()
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "Select * From FaceDevice";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                return dt;
            }
        }

        public static bool CheckIPAddressExists(string iPAddress)
        {
            return ToList().Exists(p => p.IPAddress.Equals(iPAddress));
        }

        public static FaceDevice FirstOrDefaultOnline()
        {
            return ToList().FirstOrDefault(p => CheckOnline(p.IPAddress));
        }

        private static bool CheckOnline(string iPAddress)
        {
            System.Net.NetworkInformation.Ping ping = new System.Net.NetworkInformation.Ping();

            System.Net.NetworkInformation.PingReply pingStatus =
                ping.Send(IPAddress.Parse(iPAddress), 200);
            return pingStatus.Status == System.Net.NetworkInformation.IPStatus.Success;
        }

        public static bool CheckMachineIdExists(int devId)
        {
            return ToList().Exists(p => p.DeviceId == devId);
        }

        #endregion


        #region 添加设备
        public static void Insert(FaceDevice device)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = $"Insert Into FaceDevice(IPAddress,Port,SN,Mac,Password) Values('{device.IPAddress}',{device.Port},'{device.SN}','{device.Mac}','{device.Password}')";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }

        #endregion

        #region 更新设备
        public static void Update(FaceDevice device)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = $"Update FaceDevice Set IPAddress ='{device.IPAddress}',Port ={device.Port},Mac='{device.Mac}',SN='{device.SN}',Password='{device.Password}' Where DeviceId ={device.DeviceId}";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }
        #endregion

        #region 获取注册人脸机器
        public static FaceDevice GetEnrollDevice()
        {
            return ToList().FirstOrDefault();
        }
        #endregion

        #region 删除人脸设备
        public static void Delete(int machineNumber)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = $"Delete From FaceDevice Where DeviceId ={machineNumber}";
                sql += $"{Environment.NewLine} Delete From FaceDataTask Where DeviceId ={machineNumber} And UpdateFlag = 0";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }
        #endregion


    }
}
