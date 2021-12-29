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
    public class CameraService
    {
        #region 获取摄像头信息
        public static DataTable GetCameraList()
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "Select * from Cam_CameraInfo";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                return dt;
            }
        }
        #endregion

        #region 获取摄像头与设备的绑定关系
        public static DataTable GetCamOfDeviceList()
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "Select * from Cam_CameraOfDevice";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                return dt;
            }
        }
        #endregion

        #region 检查摄像头是否已经存在
        public static bool CheckCameraExists(string ipAddress)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "Select * from Cam_CameraInfo where IPAddress = '" + ipAddress + "'";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                if (dt.Rows.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        #endregion

        #region 添加摄像头信息
        public static void InsertCamera(string camName, string mark, string ipAddress, int port, string userName, string password)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "Insert Into Cam_CameraInfo(CamName,IPAddress,Port,UserName,PassWord,Mark) values ";
                sql += $"('{camName}', '{ipAddress}', {port}, '{userName}', '{password}', '{mark}')";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }
        #endregion

        #region 获取所有摄像头列表
        public static List<CameraInfo> GetAllCameras()
        {
            List<CameraInfo> camList = new List<CameraInfo>();
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "Select  *  from CameraInfo ";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                foreach (DataRow row in dt.Rows)
                {
                    CameraInfo camera = new CameraInfo();
                    camera.CamId = Convert.ToInt32(row["CamId"]);
                    camera.CamName = row["CamName"].ToString();
                    camera.IPAddress = row["IPAddress"].ToString();
                    camera.Port = Convert.ToInt32(row["Port"]);
                    camera.UserName = row["UserName"].ToString();
                    camera.Password = row["Password"].ToString();
                    camera.Mark = row["Mark"].ToString();
                    camList.Add(camera);
                }
            }
            return camList;
        }
        #endregion

        #region 获取摄像头信息
        public static CameraInfo GetCameraInfoByCamId(int camId)
        {
            CameraInfo camera = null;
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "Select Top 1 *  from Cam_CameraInfo where CamId = " + camId;
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                foreach (DataRow row in dt.Rows)
                {
                    camera = new CameraInfo();
                    camera.CamId = camId;
                    camera.CamName = row["CamName"].ToString();
                    camera.IPAddress = row["IPAddress"].ToString();
                    camera.Port = Convert.ToInt32(row["Port"]);
                    camera.UserName = row["UserName"].ToString();
                    camera.Password = row["Password"].ToString();
                    camera.Mark = row["Mark"].ToString();
                }
            }
            return camera;
        }

        #region 根据设备编号，出入口找到匹配的摄像头

        public static int GetCamId(int devId, int ioFlag)
        {
            int camId = 0;
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "Select *  from Cam_CameraOfDevice where DeviceId = " + devId;
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                foreach (DataRow row in dt.Rows)
                {
                    switch (ioFlag)
                    {
                        case 3:
                            camId = Convert.ToInt32(row["InCamId"]);
                            break;
                        case 4:
                            camId = Convert.ToInt32(row["OutCamId"]);
                            break;
                    }
                }
            }
            return camId;
        }

        #endregion

        #endregion

        #region 更新摄像头信息
        public static void UpdateCameraInfo(CameraInfo camera)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = $"Update Cam_CameraInfo Set CamName ='{camera.CamName}',Mark ='{camera.Mark}',IPAddress ='{camera.IPAddress}',Port = {camera.Port},UserName ='{camera.UserName}',Password ='{camera.Password}' where CamId ={camera.CamId}";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }
        #endregion

        #region 删除摄像头信息
        public static void DeleteCamera(int camId)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "Delete From Cam_CameraInfo where CamId = " + camId;
                sql += "Delete From Cam_CameraOfDevice where InCamId = " + camId + " Or OutCamId = " + camId;
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }
        #endregion


        #region 检查设备是否已经绑定摄像头
        public static bool CheckCamOfDevice(int devId)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = string.Format("Select top 1 * From Cam_CameraOfDevice where DeviceId =  {0}", devId);
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                if (dt.Rows.Count > 0)
                {
                    return true;
                }
                return false;
            }
        }
        #endregion

        #region 添加设备与摄像头绑定关系
        public static void AddCamOfDevice(int devId, string devName, int inCamId, string inCamName, int outCamId, string outCamName)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = string.Format("Insert Into Cam_CameraOfDevice(DeviceId,DeviceName,InCamId,InCamName,OutCamId,OutCamName) Values({0},'{1}',{2},'{3}',{4},'{5}')", devId, devName, inCamId, inCamName, outCamId, outCamName);
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }

        #endregion

        #region 删除摄像头绑定关系
        public static void DeleteCamOfDevice(int devId)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = string.Format("Delete From Cam_CameraOfDevice Where DeviceId = {0}", devId);
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }

        #endregion

        #region 更新摄像头绑定关系
        public static void EditCamOfDevice(int devId, string devName, int inCamId, string inCamName, int outCamId, string outCamName)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = string.Format("Update Cam_CameraOfDevice Set InCamId = {0},InCamName ='{1}',OutCamId = {2},OutCamName ='{3}' where DeviceId ={4}", inCamId, inCamName, outCamId, outCamName, devId);
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }
        #endregion

    }
}
