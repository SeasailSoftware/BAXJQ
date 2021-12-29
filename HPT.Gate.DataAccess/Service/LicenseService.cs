using hpt.gate.DataAccess.Entity;
using HPT.Face.Data.Service;
using HPT.Gate.Utils.Helper;
using System;
using System.Data;
using System.Data.Common;
using System.Linq;

namespace HPT.Face.Data.ServiceImpl
{
    public class LicenseService : ILicenseService
    {
        private DBHelper helper;
        public LicenseService()
        {
            helper = DBHelperFactory.CreateDBHelper();
        }
        public void Dispose()
        {
            helper.Dispose();
        }

        public License Get()
        {
            License license = null;
            string sql = "Select * from Cam_CameraInfo";
            DbCommand cmd = helper.GetSqlStringCommond(sql);
            DataTable dt = helper.ExecuteDataTable(cmd);
            foreach (DataRow row in dt.Rows)
            {
                license = new License();
                license.ClientCode = row["ClientCode"].ToString();
                license.CreateTime = Convert.ToDateTime(row["CreateTime"]);
                break;
            }
            return license;
        }

        public bool Set(License license, out string msg)
        {
            msg = "success!";
            return true;
        }

        public bool SetRegistCode(string registCode, out string msg)
        {
            msg = "success!";
            return true;
        }
    }
}
