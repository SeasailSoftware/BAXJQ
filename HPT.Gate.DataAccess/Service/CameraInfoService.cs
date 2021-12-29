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
    public class CameraInfoService
    {
        #region 生成列表
        public static List<CameraInfo> ToList()
        {
            List<CameraInfo> cameraList = new List<CameraInfo>();
            DataTable dt = GetAll();
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
                cameraList.Add(camera);
            }
            return cameraList;
        }
        #endregion

        #region 获取所有部门
        public static DataTable GetAll()
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "Select * From CameraInfo";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                return dt;
            }
        }

        #endregion

        #region 检查摄像头是否已经存在
        public static bool CheckCameraExists(string ipAddress)
        {
            return ToList().Exists(s => ipAddress.Equals(s.IPAddress));
        }
        #endregion

        #region 添加摄像头
        public static void Insert(CameraInfo camera)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "Insert Into CameraInfo(CamName,IPAddress,Port,UserName,PassWord,Mark) values ";
                sql += $"('{camera.CamName}', '{camera.IPAddress}', {camera.Port}, '{camera.UserName}', '{camera.Password}', '{camera.Mark}')";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }
        #endregion

        #region 获取摄像头信息
        public static CameraInfo GetByCamId(int camId)
        {
            return ToList().FirstOrDefault(p => p.CamId == camId);
        }

        #endregion

        #region 更新摄像头信息
        public static void Update(CameraInfo camera)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = $"Update CameraInfo Set CamName ='{camera.CamName}',Mark ='{camera.Mark}',IPAddress ='{camera.IPAddress}',Port = {camera.Port},UserName ='{camera.UserName}',Password ='{camera.Password}' where CamId ={camera.CamId}";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }
        #endregion

        #region 删除摄像头
        public static void Del(int camId)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "Delete From CameraInfo where CamId = " + camId;
                sql += "Delete From CameraOfDevice where InCamId = " + camId + " Or OutCamId = " + camId;
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }
        #endregion

        #region 获取绑定的摄像头
        public static CameraInfo GetByDevId(int devId, int ioFlag)
        {
            CameraOfDevice binding = CameraOfDeviceService.ToList().FirstOrDefault(p => p.DeviceId == devId);
            if (binding == null) return null;
            if (ioFlag == 3)
                return ToList().FirstOrDefault(p => p.CamId == binding.InCamId);
            else
                return ToList().FirstOrDefault(p => p.CamId == binding.OutCamId);
        }
        #endregion


    }
}
