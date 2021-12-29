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
    public class DevicePlaceService
    {
        #region 生成列表
        public static List<DevicePlace> ToList()
        {
            List<DevicePlace> placeList = new List<DevicePlace>();
            DataTable dt = GetAll();
            foreach (DataRow row in dt.Rows)
            {
                DevicePlace place = new DevicePlace();
                place.PlaceId = Convert.ToInt32(row["PlaceId"]);
                place.PlaceName = row["PlaceName"].ToString();
                place.ParPlaceId = Convert.ToInt32(row["ParPlaceId"]);
                place.ImageIndex = Convert.ToInt32(row["ImageIndex"]);
                placeList.Add(place);
            }
            return placeList;
        }
        #endregion

        #region 获取所有部门
        public static DataTable GetAll()
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "Select * From DevicePlace";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                return dt;
            }
        }
        #endregion
    }
}
