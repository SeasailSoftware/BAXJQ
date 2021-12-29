using HPT.Gate.DataAccess.Entity.Entity;
using HPT.Gate.Utils.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace HPT.Gate.DataAccess.Service
{
    public class ImageOfRecordService
    {
        #region 插入抓拍的图像

        public static void Insert(ImageOfRecord capture)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "InsertCapture ";
                DbCommand cmd = dbHelper.GetStoredProcCommond(sql);
                dbHelper.AddInParameter(cmd, "@DeviceId", DbType.Int32, capture.DeviceId);
                dbHelper.AddInParameter(cmd, "@CamId", DbType.Int32, capture.CamId);
                dbHelper.AddInParameter(cmd, "@IOFlag", DbType.String, capture.IOFlag);
                dbHelper.AddInParameter(cmd, "@RecDatetime", DbType.String, capture.RecDatetime);
                dbHelper.AddInParameter(cmd, "@ImageString", DbType.Binary, capture.ImageString);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }
        #endregion
    }
}
