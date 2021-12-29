using HPT.Gate.Utils.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using HPT.Gate.DataAccess.Entity;

namespace HPT.Gate.DataAccess.Entity.Service
{
    public class DevRightOfEmpService
    {
        #region 生成列表
        public static List<DevRightOfEmp> ToList()
        {
            List<DevRightOfEmp> rights = new List<DevRightOfEmp>();
            DataTable dt = GetAll();
            foreach (DataRow row in dt.Rows)
            {
                DevRightOfEmp right = new DevRightOfEmp();
                right.RecId = Convert.ToInt32(row["RecId"]);
                right.EmpId = Convert.ToInt32(row["EmpId"]);
                right.DeviceId = Convert.ToInt32(row["DeviceId"]);
                right.Right = Convert.ToInt32(row["Rights"]);
                right.UpdateFlag = Convert.ToInt32(row["UpdateFlag"]);
                rights.Add(right);
            }
            return rights;
        }
        #endregion

        #region 获取所有部门
        public static DataTable GetAll()
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "Select * From DevRightOfEmp";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                return dt;
            }
        }

        #endregion

        #region 获取设备对应权限
        internal static List<DevRightOfEmp> GetByDeviceId(int deviceId)
        {
            return ToList().Where(s => s.DeviceId == deviceId).ToList();
        }

        #endregion

        #region 获取设备对应的人的权限
        public static DataTable GetRightsOfDevice(int deviceId)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                DbCommand cmd = dbHelper.GetStoredProcCommond("GetRightsOfDevId");
                dbHelper.AddInParameter(cmd, "@DeviceId", DbType.Int32, deviceId);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                return dt;
            }
        }
        #endregion

        #region 检查是否有权限
        public static bool CheckRight(int empId, ushort deviceId)
        {
            return (ToList().Where(p => p.DeviceId == deviceId).ToList()).Exists(p => p.EmpId == empId && p.Right == 1);
        }

        public static void Insert(int empId, int deviceId)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "InsertRight";
                DbCommand cmd = dbHelper.GetStoredProcCommond(sql);
                dbHelper.AddInParameter(cmd, "@EmpId", DbType.Int32, empId);
                dbHelper.AddInParameter(cmd, "@DeviceId", DbType.Int32, deviceId);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }

        #endregion

        #region 人员权限报表
        public static List<Rights> Find(int _DeviceId, int _DeptId, int _DeptType, string _EmpCode, string _EmpName, string _CardNo)
        {
            List<Rights> rights = new List<Rights>();
            List<EmpInfo> empList = new List<EmpInfo>();
            if (_DeptType == 0)
                empList = EmpInfoService.GetByDeptId(_DeptId);
            else
                empList = EmpInfoService.GetAllByDept(_DeptId);
            if (!string.IsNullOrWhiteSpace(_EmpCode))
                empList = empList.Where(s => _EmpCode.Equals(s.EmpCode)).ToList();
            if (!string.IsNullOrWhiteSpace(_EmpName))
                empList = empList.Where(s => _EmpName.Equals(s.EmpName)).ToList();
            if (!string.IsNullOrWhiteSpace(_CardNo))
                empList = empList.Where(s => _CardNo.Equals(s.ICCardNo)).ToList();

            foreach (EmpInfo emp in empList)
            {
                foreach (DevRightOfEmp devRight in DevRightOfEmpService.ToList())
                {
                    if (_DeviceId != 0)
                    {
                        if (devRight.DeviceId != _DeviceId)
                            continue;
                    }
                    if (devRight.EmpId != emp.EmpId)
                        continue;
                    Rights right = new Rights();
                    right.DeviceId = devRight.DeviceId;
                    right.DeviceName = DeviceInfoService.GetByDeviceId(devRight.DeviceId).DeviceName;
                    right.DeptId = emp.DeptId;
                    right.DeptName = emp.DeptName;
                    right.EmpCode = emp.EmpCode;
                    right.EmpName = emp.EmpName;
                    TicketType ticketType = TicketTypeService.GetByRecId(emp.TicketType);
                    right.RightOfIn = ticketType.InRight == 1 ? "是" : "否";
                    right.RightOfOut = ticketType.OutRight == 1 ? "是" : "否";
                    right.BeginDate = emp.BeginDate;
                    right.EndDate = emp.EndDate;
                    rights.Add(right);
                }
            }
            return rights;
        }

        #endregion

        #region 撤掉权限
        public static void UndoRights(List<DevRightOfEmp> rightList)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                foreach (DevRightOfEmp right in rightList)
                {
                    DbCommand cmd = dbHelper.GetStoredProcCommond("SaveRights");
                    dbHelper.AddInParameter(cmd, "@DeviceId", DbType.Int32, right.DeviceId);
                    dbHelper.AddInParameter(cmd, "@EmpId", DbType.Int32, right.EmpId);
                    dbHelper.AddInParameter(cmd, "@Rights", DbType.Int32, right.Right);
                    dbHelper.ExecuteNonQuery(cmd);
                }
            }
        }
        #endregion


        #region 获取人员对应权限的设备
        public static List<DeviceInfo> GetDeviceByEmpid(int empId)
        {
            List<DeviceInfo> devList = new List<DeviceInfo>();
            List<DevRightOfEmp> rights = ToList().Where(s => s.EmpId == empId && s.Right == 1).ToList();
            foreach (DeviceInfo device in DeviceInfoService.ToList())
            {
                if (rights.Exists(p => p.DeviceId == device.DeviceId))
                    devList.Add(device);
            }
            return devList;
        }
        #endregion

        #region 清除人员所有权限
        public static void ClearAllRightsOfEmpId(int empId)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "ClearAllRightsOfEmpId";
                DbCommand cmd = dbHelper.GetStoredProcCommond(sql);
                dbHelper.AddInParameter(cmd, "@EmpId", DbType.Int32, empId);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }
        #endregion


    }
}
