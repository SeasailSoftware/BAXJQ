using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Data;
using System.Collections;
using System.IO;
using System.Data.SqlClient;
using System.Threading;
using System.Drawing;
using System.Data.Sql;
using hpt.gate.Entity;
using DbTools.Entity;
using hpt.gate.DbTools.Util;
using hpt.gate.Helper;
using hpt.gate.device.Data;
using hpt.gate.Tools;
using hpt.gate.Entity.Entity;

namespace hpt.gate.DbTools.Service
{
    public class MainService
    {

        #region 部门操作

        #region 删除用户对部门的权限
        public static void DeleteDeptOfOperByOperId(int currentOperId)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = $"Delete From DeptOfOper Where OperId ={currentOperId}";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }

        #endregion

        #region 修改部门根节点名称
        public static void ChangeDeptRootName(string deptName)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = $"Update DeptInfo Set DeptName ='{deptName}' Where DeptId = 1";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }
        #endregion



        #region 删除用户对部门的权限
        public static void AddDeptOfOper(int currentOperId, int deptId)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = $"{Environment.NewLine}If Not Exists(Select Top 1 * From DeptOfOper Where DeptId ={deptId} And OperId ={currentOperId})";
                sql += $"{Environment.NewLine}Insert Into DeptOfOper(OperId,DeptId) values({currentOperId},{deptId})";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }
        #endregion

        #region 根据用户Id获取部门列表
        public static List<DeptInfo> GetDeptListByOperId(int currentOperId)
        {
            List<DeptInfo> deptList = new List<DeptInfo>();
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = $"Select * From DeptInfo Where DeptId in (Select DeptId From DeptOfOper Where OperId ={currentOperId})";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                foreach (DataRow row in dt.Rows)
                {
                    DeptInfo dept = new DeptInfo();
                    dept.DeptId = Convert.ToInt32(row["DeptId"]);
                    dept.ParDeptId = Convert.ToInt32(row["ParDeptId"]);
                    dept.DeptName = row["DeptName"].ToString();
                    dept.ImageIndex = Convert.ToInt32(row["ImageIndex"]);
                    deptList.Add(dept);
                }
            }
            return deptList;
        }

        #endregion

        #region 根据用户Id获取部门列表
        public static DataTable GetDeptDatatableByOperId(int currentOperId)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = $"Select DeptId as Id,ParDeptId as ParId,DeptName As Name,ImageIndex From DeptInfo Where DeptId in (Select DeptId From DeptOfOper Where OperId ={currentOperId})";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                return dt;
            }
        }
        #endregion



        #region 根据上级部门编号以及部门名称查找部门编号
        public static int GetDeptId(int parDeptId, string deptName)
        {
            int deptId = 0;
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = $"Select top 1 DeptId From DeptInfo Where ParDeptId ={parDeptId} And DeptName ='{deptName}'";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                foreach (DataRow row in dt.Rows)
                {
                    deptId = Convert.ToInt32(row["DeptId"]);
                }
            }
            return deptId;
        }

        #endregion



        #endregion

        #region 票类操作

        #region 删除票类
        public static void DeleteTicketType(int recId)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = $"{Environment.NewLine}Delete From TicketType Where RecId = {recId}";
                sql += $"	{Environment.NewLine}Update DevRightOfEmp Set UpdateFlag = 0 Where EmpId in (Select EmpId From EmpInfo Where TicketType ={recId})";
                sql += $"{Environment.NewLine}Update EmpInfo Set TicketType = 1 Where TicketType = {recId}";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }
        #endregion

        #endregion


        /*
        #region 加载读写卡配置
        public static DataCardPass GetDataCardPass()
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                DataCardPass config = new DataCardPass();
                string sql = string.Format("Select * from Sys_WriteCardPara");
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                foreach (DataRow row in dt.Rows)
                {
                    config.WriteCard_Enable = Convert.ToInt32(row["Enabled"]) == 1;
                    config.CardPassword = row["CardPassword"].ToString();
                    config.SectorNo = Convert.ToInt32(row["SectorNo"]);
                    config.AntiSubmarineWarfare = Convert.ToInt32(row["AntiSubmarineWarfare"]) == 1;
                    config.LimitedTimes_Enabled = Convert.ToInt32(row["LimitedTimesEnabled"]) == 1;
                    config.LimitedTimesOfIn = Convert.ToInt32(row["LimitedTimesOfIn"]);
                    config.LimitedTimesOfOut = Convert.ToInt32(row["LimitedTimesOfOut"]);
                    config.LimitedMinutes_Enable = Convert.ToInt32(row["LimitedMinutesEnable"]) == 1;
                    config.LimitedMinutes = Convert.ToInt32(row["LimitedMinutes"]);
                    config.CardInterval = Convert.ToInt32(row["CardInterval"]);
                }
                return config;
            }
        }




#endregion
                    */

        #region 设置部门卡属性
        public static void SetCardProperityOfDept(EmpInfo emp, CardInfo card)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                DbCommand cmd = dbHelper.GetStoredProcCommond("UpdateCardOfDept");
                dbHelper.AddInParameter(cmd, "@EmpId", DbType.Int32, emp.EmpId);
                dbHelper.AddInParameter(cmd, "@BlackName", DbType.Int32, card.BlackName);
                dbHelper.AddInParameter(cmd, "@CardType", DbType.Int32, card.CardType);
                dbHelper.AddInParameter(cmd, "@InRight", DbType.Int32, card.InRight);
                dbHelper.AddInParameter(cmd, "@OutRight", DbType.Int32, card.OutRight);
                dbHelper.AddInParameter(cmd, "@VoiceNo", DbType.Int32, card.VoiceNo);
                dbHelper.AddInParameter(cmd, "@Photo", DbType.Int32, card.Photo);
                dbHelper.AddInParameter(cmd, "@VacationId", DbType.Int32, card.VacationNo);
                dbHelper.AddInParameter(cmd, "@InTimeGroupNo", DbType.Int32, card.InTimeGroupNo);
                dbHelper.AddInParameter(cmd, "@OutTimeGroupNo", DbType.Int32, card.OutTimeGroupNo);
                dbHelper.AddInParameter(cmd, "@BeginDate", DbType.String, card.BeginDate);
                dbHelper.AddInParameter(cmd, "@EndDate", DbType.String, card.EndDate);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }

        #endregion

        #region
        public static List<DeviceInfo> GetAllDevices()
        {
            List<DeviceInfo> devList = new List<DeviceInfo>();
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "Select * from deviceInfo";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                foreach (DataRow row in dt.Rows)
                {
                    /*
                    int devId = Convert.ToInt32(row["DeviceId"]);
                    string devName = row["DeviceName"].ToString();
                    string mac = row["MAC"].ToString();
                    string ip = row["IPAddress"].ToString();
                    string subNet = row["SubNet"].ToString();
                    string gateWay = row["GateWay"].ToString();
                    int port = Convert.ToInt32(row["Port"]);
                    string hardVersion = row["HardVersion"].ToString();
                    string softVersion = row["SoftVersion"].ToString();
                    UdpDevice device = new UdpDevice(devId, devName, mac, ip, subNet, gateWay, port, hardVersion, softVersion);
                    devList.Add(device);
                    */
                }
            }
            return devList;
        }

        #endregion

        #region
        public static DataTable GetAllDevicesTable()
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "Select DeviceId,DeviceName,Mac,IPAddress,SubNet,Gateway,Port,HardVersion,SoftVersion from deviceInfo";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                return dt;
            }
        }

        #endregion

        #region 获取人员列表
        public static DataTable GetEmpsOfDept(int deptId, int type)
        {
            List<EmpInfo> empList = new List<EmpInfo>();
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "GetEmpsOfDept";
                DbCommand cmd = dbHelper.GetStoredProcCommond(sql);
                dbHelper.AddInParameter(cmd, "@DeptId", DbType.Int32, deptId);
                dbHelper.AddInParameter(cmd, "@Type", DbType.Int32, type);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                return dt;
            }
        }

        #endregion


        #region 获取人员列表
        public static List<EmpInfo> GetEmpListByDeptId(int deptId, int type)
        {
            List<EmpInfo> empList = new List<EmpInfo>();
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "GetEmpAndCardListOfDeptId";
                DbCommand cmd = dbHelper.GetStoredProcCommond(sql);
                dbHelper.AddInParameter(cmd, "@DeptId", DbType.Int32, deptId);
                dbHelper.AddInParameter(cmd, "@Type", DbType.Int32, type);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                foreach (DataRow row in dt.Rows)
                {
                    EmpInfo emp = new EmpInfo();
                    emp.EmpId = Convert.ToInt32(row["EmpId"]);
                    emp.EmpCode = row["EmpCode"].ToString();
                    empList.Add(emp);
                }
            }
            return empList;
        }


        #endregion

        #region 获取读写卡参数配置
        public static WriteConfig GetWriteCardConfig()
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                WriteConfig config = new WriteConfig();
                string sql = string.Format("Select * from Sys_WriteCardPara");
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                foreach (DataRow row in dt.Rows)
                {
                    config.WriteCardEnable = Convert.ToInt32(row["Enabled"]) == 1;
                    config.CardPassword = row["CardPassword"].ToString();
                    config.SectorNo = Convert.ToInt32(row["SectorNo"]);
                    config.AntiSubmarineWarfare = Convert.ToInt32(row["AntiSubmarineWarfare"]) == 1;
                    config.LimitedTimes_Enabled = Convert.ToInt32(row["LimitedTimesEnabled"]) == 1;
                    config.LimitedTimesOfIn = Convert.ToInt32(row["LimitedTimesOfIn"]);
                    config.LimitedTimesOfOut = Convert.ToInt32(row["LimitedTimesOfOut"]);
                    config.LimitedMinutes_Enable = Convert.ToInt32(row["LimitedMinutesEnable"]) == 1;
                    config.LimitedMinutes = Convert.ToInt32(row["LimitedMinutes"]);
                    config.CardInterval = Convert.ToInt32(row["CardInterval"]);
                }
                return config;
            }
        }
        #endregion

        #region 快速添加人员与卡信息
        public static void InsertEmpAndCard(int deptId, string empCode, string empName, int cardType, string cardNo)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "FastAddEmpAndCard";
                DbCommand cmd = dbHelper.GetStoredProcCommond(sql);
                dbHelper.AddInParameter(cmd, "@DeptId", DbType.Int32, deptId);
                dbHelper.AddInParameter(cmd, "@EmpCode", DbType.String, empCode);
                dbHelper.AddInParameter(cmd, "@EmpName", DbType.String, empName);
                dbHelper.AddInParameter(cmd, "@CardType", DbType.Int32, cardType);
                dbHelper.AddInParameter(cmd, "@CardNo", DbType.String, cardNo);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }




        #endregion

        #region 获取读写卡参数配置
        public static void SetWriteCardConfig(WriteConfig config)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = $" UPDATE [Sys_WriteCardPara]  SET[Enabled] = {(config.WriteCardEnable ? 1 : 0) },[CardPassword] ='{config.CardPassword}',[SectorNo] = {config.SectorNo},[AntiSubmarineWarfare] = {(config.AntiSubmarineWarfare ? 1 : 0)},";
                sql += $"[LimitedTimesEnabled] ={(config.LimitedTimes_Enabled ? 1 : 0)} ,[LimitedTimesOfIn] = {config.LimitedTimesOfIn},[LimitedTimesOfOut] ={config.LimitedTimesOfOut},[LimitedMinutesEnable] ={(config.LimitedMinutes_Enable ? 1 : 0)},";
                sql += $"[LimitedMinutes] = {config.LimitedMinutes},[CardInterval] ={config.CardInterval} ";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }


        #endregion

        #region 获取部门信息
        public static DeptInfo GetDeptInfo(int deptid)
        {
            DeptInfo dept = null;
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = string.Format("Select * from DeptInfo where DeptId = {0} ", deptid);
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                foreach (DataRow row in dt.Rows)
                {
                    dept = new DeptInfo();
                    dept.DeptId = deptid;
                    dept.ParDeptId = Convert.ToInt32(row["ParDeptId"]);
                    dept.DeptName = row["DeptName"].ToString();
                    dept.ImageIndex = Convert.ToInt32(row["ImageIndex"]);
                }
            }
            return dept;
        }

        public static bool CheckEmpRights(int empId, int devId)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = string.Format("Select top 1 * from EmprightOfDevice where DeviceId ={0} and EmpId ={1}", devId, empId);
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                if (dt.Rows.Count > 0)
                    return true;
                return false;
            }
        }

        #endregion

        #region 检查数据库更新
        public static Versions GetVersion()
        {
            Versions version = null;
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                StringBuilder buffer = new StringBuilder();
                buffer.Append(Environment.NewLine + " if not exists(select * from sysobjects where name = 'Versions' and xtype = 'U')");
                buffer.Append(Environment.NewLine + " Begin");
                buffer.Append(Environment.NewLine + " Create table Versions(Vid int, MainVersion int, LedVersion int, AttendVersion int)");
                buffer.Append(Environment.NewLine + " Insert Versions(Vid, MainVersion, LedVersion, AttendVersion) values(1, 0, 0, 0)");
                buffer.Append(Environment.NewLine + " End ");
                buffer.Append(Environment.NewLine + " Declare @Vid int, @MainVersion int, @LedVersion int, @AttendVersion int");
                buffer.Append(Environment.NewLine + " Select top 1 @Vid = Vid,@MainVersion = MainVersion,@LedVersion = LedVersion,@AttendVersion = AttendVersion From Versions");
                buffer.Append(Environment.NewLine + " Select @Vid As Vid,@MainVersion As MainVersion,@LedVersion As LedVersion,@AttendVersion As AttendVersion ");
                DbCommand cmd = dbHelper.GetSqlStringCommond(buffer.ToString());
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                foreach (DataRow row in dt.Rows)
                {
                    version = new Versions();
                    version.Vid = Convert.ToInt32(row["Vid"]);
                    version.MainVersion = Convert.ToInt32(row["MainVersion"]);
                    version.LedVersion = Convert.ToInt32(row["LedVersion"]);
                    version.AttendVersion = Convert.ToInt32(row["AttendVersion"]);
                }
            }
            return version;
        }


        #endregion

        #region 执行数据库更新
        /// <summary>
        /// 执行数据库更新
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="progress"></param>
        /// <param name="label"></param>
        /// <returns></returns>
        public static bool ExecuteSqlFile(OleDbHelper dbHelper, string sql)
        {
            try
            {
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
                return true;
            }
            catch
            {

            }
            return false;
        }
        #endregion

        //public static DbHelper dbHelper = new DbHelper();



        #region 设备操作


        /// <summary>
        /// 设备撤权
        /// </summary>
        /// <param name="rightList"></param>
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

        public static List<int> GetEmpIds(string empCodeBegin, string empCodeEnd)
        {
            throw new NotImplementedException();
        }



        /// <summary>
        /// 获取部门的人员ID
        /// </summary>
        /// <param name="deptId"></param>
        /// <returns></returns>
        public static List<int> GetEmpIdList(int deptId)
        {
            List<int> empIdList = new List<int>();
            DataTable dt = GetEmpListByDeptId(deptId);
            foreach (DataRow row in dt.Rows)
            {
                int EmpId = Convert.ToInt32(row["EmpId"]);
                empIdList.Add(EmpId);
            }
            return empIdList;
        }

        #region 检查Led功能更新
        public static void CheckLedUpdate()
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "if Not exists(select * from syscolumns where id=object_id('AreaInfo') and name='Interval') ";
                sql += "  begin  ";
                sql += "  alter table AreaInfo  add Interval int not null   default 3";
                sql += "  end  ";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }
        #endregion


        #region 获取最近5条记录

        /// <summary>
        /// 获取最近5条记录
        /// </summary>
        /// <returns></returns>
        public static List<DisplayRecord> GetRecentRecords()
        {
            List<DisplayRecord> recordList = new List<DisplayRecord>();
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "Select * From (Select Top 4 b.DeviceName,a.* from Record a,DeviceInfo b where a.DeviceId = b.DeviceId Order By a.RecId Desc) d Left Join ";
                sql += "(Select a.DeptId, a.DeptName, b.EmpId, b.EmpName, b.EmpCode,b.Photo,c.CardId, c.CardNo from DeptInfo a, EmpInfo b, CardInfo c ";
                sql += "where a.DeptId = b.DeptId and b.EmpId = c.EmpId and c.CardStatus = 1) e On d.CardNo = e.CardNo ";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                foreach (DataRow row in dt.Rows)
                {
                    DisplayRecord record = new DisplayRecord();
                    record.DeviceId = Convert.ToInt32(row["DeviceId"]);
                    record.DeviceName = row["DeviceName"].ToString();
                    record.DeptId = Convert.ToInt32(row["DeptId"].ToString().Equals(string.Empty) ? "0" : row["deptId"]);
                    record.DeptName = row["DeptName"].ToString().Equals(string.Empty) ? "无" : row["DeptName"].ToString();
                    record.CardNo = row["CardNo"].ToString();
                    record.EmpId = Convert.ToInt32(row["EmpId"].ToString().Equals(string.Empty) ? "0" : row["EmpId"]);
                    record.EmpCode = row["EmpCode"].ToString().Equals(string.Empty) ? "无" : row["EmpCode"].ToString();
                    record.EmpName = row["EmpName"].ToString().Equals(string.Empty) ? "无" : row["EmpName"].ToString();
                    record.CardId = Convert.ToInt32(row["CardId"].ToString().Equals(string.Empty) ? "0" : row["CardId"]);
                    record.CurrentIndex = Convert.ToInt32(row["RecPointer"]);
                    record.IOFlag = row["IOFlag"].ToString();
                    record.RecDatetime = row["RecDateTime"].ToString();
                    record.RecordType = row["RecordType"].ToString();
                    byte[] array = row["Photo"].ToString().Equals(string.Empty) ? null : (byte[])row["Photo"];
                    record.PhotoArray = array == null ? null : array.Length <= 10 ? null : array;
                    recordList.Add(record);
                }
            }
            int count = recordList.Count;
            if (count < 5)
            {
                for (int i = 0; i < 5 - count; i++)
                {
                    DisplayRecord rec = null;
                    recordList.Add(rec);
                }
            }
            return recordList;
        }

        #region 检查部门名称是否存在
        /// <summary>
        /// 检查部门名称是否存在
        /// </summary>
        /// <param name="dbHelper"></param>
        /// <param name="deptName"></param>
        /// <returns></returns>
        public static bool CheckDeptName(OleDbHelper dbHelper, string deptName)
        {
            string sql = string.Format("Select top 1 DeptId From DeptInfo where DeptName ='{0}'", deptName);
            DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
            DataTable dt = dbHelper.ExecuteDataTable(cmd);
            if (dt.Rows.Count > 0) return true;
            return false;
        }
        #endregion

        #endregion
        /// <summary>
        /// 获取连续人员编号的人员ID
        /// </summary>
        /// <param name="empCodeBegin"></param>
        /// <param name="empCodeEnd"></param>
        /// <returns></returns>
        public static List<int> GetEmpIdList(string empCodeBegin, string empCodeEnd)
        {
            List<int> empIdList = new List<int>();
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                int begin = Convert.ToInt32(empCodeBegin);
                int end = Convert.ToInt32(empCodeEnd);
                for (int i = begin; i <= end; i++)
                {
                    string empCode = string.Format("{0:D8}", i);
                    int empId = GetEmpIdByEmpCode(empCode);
                    if (empId != 0)
                    {
                        empIdList.Add(empId);
                    }
                }
            }
            return empIdList;
        }






        /// <summary>
        /// 通过组号查找组名
        /// </summary>
        /// <param name="inTimeGroupNo"></param>
        /// <returns></returns>
        public static string GetGroupNameByGroupId(int inTimeGroupNo)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string name = string.Empty;
                string sql = "Select Name From TimeGroupOfDoor where Status = 1 and Id = " + inTimeGroupNo;
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                foreach (DataRow row in dt.Rows)
                {
                    name = row["Name"].ToString();
                }
                return name;
            }
        }

        #region 清除所有卡的在场状态
        public static void ClearAllCardStatus()
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "Update CardInfo Set IOFlag = 3 ";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }
        #endregion

        /// <summary>
        /// 增加，修改，删除 安装区域
        /// </summary>
        /// <param name="sql"></param>
        public static void AddDevicePlace(int parPlaceId, string placeName)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "insert into deviceplace(placeName,parPlaceid) values('" + placeName + "'," + parPlaceId + ")";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }
        /// <summary>
        /// 增加，修改，删除 安装区域
        /// </summary>
        /// <param name="sql"></param>
        public static void EditDevicePlace(int placeId, string placeName)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "update deviceplace set placeName ='" + placeName + "' where placeid =" + placeId;
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }
        /*
        /// <summary>
        /// 获取设备卡更新列表
        /// </summary>
        /// <param name="devId"></param>
        /// <returns></returns>
        public static Queue<DataCard> GetCardUpdateList(int devId)
        {
            try
            {
                Queue<DataCard> qdCard = new Queue<DataCard>();
                using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
                {
                    string sql = "GetCardListUpdate";
                    DbCommand cmd = dbHelper.GetStoredProcCommond(sql);
                    dbHelper.AddInParameter(cmd, "@DeviceId", DbType.Int32, devId);
                    DataTable dt = dbHelper.ExecuteDataTable(cmd);
                    foreach (DataRow row in dt.Rows)
                    {
                        int Type = Convert.ToInt32(row["Type"].ToString());
                        int empId = Convert.ToInt32(row["EmpId"]);
                        DataCard card = new DataCard();
                        /*
                        switch (Type)
                        {
                            case 1:
                                card.CardNo = ArrayHelper.HexToArray(row["CardNo"].ToString(), 4);
                                break;
                            case 2:
                                card.CardNo = ArrayHelper.IdentityToArray(row["CardNo"].ToString());
                                break;
                            case 3:
                                card.CardNo = ArrayHelper.IdentityToArray(row["CardNo"].ToString());
                                break;
                        }
                        card.CurCardSerial = ArrayHelper.IntToBytes(Convert.ToInt32(row["CardId"]), 2);
                        card.TatolCardSerial = ArrayHelper.IntToBytes(Convert.ToInt32(row["totalNum"]), 2);
                        card.CardStatus = ArrayHelper.IntToBytes(Convert.ToInt32(row["BlackName"]), 1);
                        card.CardType = ArrayHelper.IntToBytes(Convert.ToInt32(row["CardType"]), 1);
                        card.Row1 = ArrayHelper.GB2312ToArray(row["Row1"].ToString(), 16);
                        card.Row2 = ArrayHelper.GB2312ToArray(row["Row2"].ToString(), 16);
                        card.Row3 = ArrayHelper.GB2312ToArray(row["Row3"].ToString(), 16);
                        card.RightOfDoorIn = ArrayHelper.IntToBytes(Convert.ToInt32(row["InRight"]), 1);
                        card.RightOfDoorOut = ArrayHelper.IntToBytes(Convert.ToInt32(row["OutRight"]), 1);
                        card.VoiceNo = ArrayHelper.IntToBytes(Convert.ToInt32(row["VoiceNo"]), 1);
                        string photoName = row["CardCode"].ToString();
                        if (photoName.Equals("FFFF"))
                        {
                            card.PhotoName = new byte[] { 0xFF, 0xFF };
                        }
                        else
                        {
                            card.PhotoName = ArrayHelper.IntToBytes(empId, 2);
                        }

                        card.VacationGrop = ArrayHelper.IntToBytes(Convert.ToInt32(row["VacationId"]), 1);
                        card.TimeGroupOfNormalDoorIn = ArrayHelper.IntToBytes(Convert.ToInt32(row["InTimeGroupNo"]), 1);
                        card.TimeGroupOfNormalDoorOut = ArrayHelper.IntToBytes(Convert.ToInt32(row["OutTimeGroupNo"]), 1);
                        card.BeginDate = ArrayHelper.DateToArray1(Convert.ToDateTime(row["BeginDate"]));
                        card.EndDate = ArrayHelper.DateToArray1(Convert.ToDateTime(row["EndDate"]));
                        card.DeviceId = devId;
                        qdCard.Enqueue(card);

                    }
                }
                return qdCard;
            }
            catch
            {
                return null;
            }

        }


        /// <summary>
        /// 全部重新授权列表
        /// </summary>
        /// <param name="devId"></param>
        /// <returns></returns>
        public static Queue<DataCard> GetCardListAll(int devId)
        {
            try
            {
                Queue<DataCard> qdCard = new Queue<DataCard>();
                using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
                {
                    string sql = "GetCardListAll";
                    DbCommand cmd = dbHelper.GetStoredProcCommond(sql);
                    dbHelper.AddInParameter(cmd, "@DeviceId", DbType.Int32, devId);
                    DataTable dt = dbHelper.ExecuteDataTable(cmd);
                    foreach (DataRow row in dt.Rows)
                    {
                        int Type = Convert.ToInt32(row["Type"].ToString());
                        DataCard card = new DataCard();
                        /*
                        switch (Type)
                        {
                            case 1:
                                card.CardNo = ArrayHelper.HexToArray(row["CardNo"].ToString(), 4);
                                break;
                            case 2:
                                card.CardNo = ArrayHelper.IdentityToArray(row["CardNo"].ToString());
                                break;
                            case 3:
                                card.CardNo = ArrayHelper.IdentityToArray(row["CardNo"].ToString());
                                break;
                        }
                        card.CurCardSerial = ArrayHelper.IntToBytes(Convert.ToInt32(row["CardId"]), 2);
                        card.TatolCardSerial = ArrayHelper.IntToBytes(Convert.ToInt32(row["totalNum"]), 2);
                        card.CardStatus = ArrayHelper.IntToBytes(Convert.ToInt32(row["BlackName"]), 1);
                        card.CardType = ArrayHelper.IntToBytes(Convert.ToInt32(row["CardType"]), 1);
                        card.Row1 = ArrayHelper.GB2312ToArray(row["Row1"].ToString(), 16);
                        card.Row2 = ArrayHelper.GB2312ToArray(row["Row2"].ToString(), 16);
                        card.Row3 = ArrayHelper.GB2312ToArray(row["Row3"].ToString(), 16);
                        card.RightOfDoorIn = ArrayHelper.IntToBytes(Convert.ToInt32(row["InRight"]), 1);
                        card.RightOfDoorOut = ArrayHelper.IntToBytes(Convert.ToInt32(row["OutRight"]), 1);
                        card.VoiceNo = ArrayHelper.IntToBytes(Convert.ToInt32(row["VoiceNo"]), 1);
                        string photoName = row["CardCode"].ToString();
                        if (photoName.Equals("FFFF"))
                        {
                            card.PhotoName = new byte[] { 0xFF, 0xFF };
                        }
                        else
                        {
                            card.PhotoName = ArrayHelper.IntToBytes(Convert.ToInt32(photoName), 2);
                        }

                        card.VacationGrop = ArrayHelper.IntToBytes(Convert.ToInt32(row["VacationId"]), 1);
                        card.TimeGroupOfNormalDoorIn = ArrayHelper.IntToBytes(Convert.ToInt32(row["InTimeGroupNo"]), 1);
                        card.TimeGroupOfNormalDoorOut = ArrayHelper.IntToBytes(Convert.ToInt32(row["OutTimeGroupNo"]), 1);
                        card.BeginDate = ArrayHelper.DateToArray1(Convert.ToDateTime(row["BeginDate"]));
                        card.EndDate = ArrayHelper.DateToArray1(Convert.ToDateTime(row["EndDate"]));
                        card.DeviceId = devId;
                        qdCard.Enqueue(card);

                    }
                }
                return qdCard;
            }
            catch
            {
                return null;
            }

        }

                                */

        /// <summary>
        /// 修改设备信息
        /// </summary>
        /// <param name="dbDevice"></param>
        public static void EditDeviceInfo(DeviceInfo dbDevice)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                /*
                DbCommand cmd = dbHelper.GetStoredProcCommond("EditDeviceInfo");
                dbHelper.AddInParameter(cmd, "@DeviceId", DbType.Int32, dbDevice._MachineId);
                dbHelper.AddInParameter(cmd, "@DeviceName", DbType.String, dbDevice._DeviceName);
                dbHelper.AddInParameter(cmd, "@PlaceId", DbType.Int32, dbDevice.PlaceId);
                dbHelper.AddInParameter(cmd, "@MAC", DbType.String, dbDevice._Mac);
                dbHelper.AddInParameter(cmd, "@IPAddress", DbType.String, dbDevice._IPAddress);
                dbHelper.AddInParameter(cmd, "@SubNet", DbType.String, dbDevice._SubnetMark);
                dbHelper.AddInParameter(cmd, "@GateWay", DbType.String, dbDevice._Gateway);
                dbHelper.ExecuteNonQuery(cmd);
                */
            }
        }
        /// <summary>
        /// 获取可用的设备编号
        /// </summary>
        /// <returns></returns>
        public static int GetAvarilableDevId()
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                int devId = 0;
                string sql = "Select isnull(max(DeviceId),999)+1 As DeviceId  from DeviceInfo ";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                foreach (DataRow row in dt.Rows)
                {
                    devId = Convert.ToInt32(row["DeviceId"]);
                }
                return devId;
            }
        }

        /// <summary>
        /// 根据设备ID查找数据库设备信息
        /// </summary>
        /// <param name="deviceId"></param>
        /// <returns></returns>
        public static DBDevice GetDBDeviceByDevId(int deviceId)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                DBDevice device = null;
                string sql = "Select * from DeviceInfo   where deviceId = " + deviceId.ToString();
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                foreach (DataRow row in dt.Rows)
                {
                    device = new DBDevice();
                    device.DevId = Convert.ToInt32(row["DeviceId"]);
                    device.DevName = row["DeviceName"].ToString();
                    device.PlaceId = Convert.ToInt32(row["PlaceId"]);
                    device.MAC = row["mac"].ToString();
                    device.IPAddress = row["IPAddress"].ToString();
                    device.SubNet = row["SubNet"].ToString();
                    device.GateWay = row["GateWay"].ToString();
                    device.Port = Convert.ToInt32(row["Port"]);
                    device.HardVersion = row["HardVersion"].ToString();
                    device.SoftVersion = row["SoftVersion"].ToString();
                    device.P1 = Convert.ToInt32(row["P1"]);
                    device.V1 = Convert.ToInt32(row["V1"]);
                    device.P2 = Convert.ToInt32(row["P2"]);
                    device.V2 = Convert.ToInt32(row["V2"]);
                    device.WaitTime = Convert.ToInt32(row["WaitTime"]);
                    device.DelayTime = Convert.ToInt32(row["DelayTime"]);
                    device.RepeatTime = Convert.ToInt32(row["RepeatTime"]);
                    device.WelCome = row["WelCome"].ToString();
                }
                return device;
            }
        }


        /// <summary>
        /// 根据mac生成设备
        /// </summary>
        /// <param name="mac"></param>
        /// <returns></returns>
        public static DeviceInfo GetDeviceByMac(string mac)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                DeviceInfo device = null;
                mac = mac.Replace("-", "").Trim();
                string sql = "Select * from DeviceInfo   where Mac = '" + mac + "'";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                foreach (DataRow row in dt.Rows)
                {
                    int port = Convert.ToInt32(row["Port"]);
                    int devId = Convert.ToInt32(row["DeviceID"]);
                    string devName = row["DeviceName"].ToString();
                    device = new DeviceInfo();
                    device.DeviceId = devId;
                    device.DeviceName = devName;
                }
                return device;
            }
        }
        /// <summary>
        /// 获取数据库设备列表
        /// </summary>
        /// <returns></returns>
        public static DataTable GetDBDeviceList()
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                List<DeviceInfo> devList = new List<DeviceInfo>();
                string sql = "Select DeviceId,DeviceName,Mac,IPAddress,SubNet,GateWay,Port from DeviceInfo ";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                return dt;
            }
        }
        /// <summary>
        /// 删除指定ID的控制器
        /// </summary>
        /// <param name="deviceId"></param>
        public static void DeleteDeviceInfo(int deviceId)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                DbCommand cmd = dbHelper.GetStoredProcCommond("DeleteDeviceInfo");
                dbHelper.AddInParameter(cmd, "@DeviceId", DbType.Int32, deviceId);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }

        /// <summary>
        /// 获取安装区域列表
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static DataTable GetPlaceList()
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "select * from MJ_DevicePlace";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                return dt;
            }
        }
        /// <summary>
        /// 增加，修改，删除 安装区域
        /// </summary>
        /// <param name="sql"></param>
        public static void DevicePlaceEdit(string sql)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }
        /// <summary>
        /// 检查设备是否存在,,存在返回true,否则返回false
        /// </summary>
        /// <param name="mac"></param>
        /// <returns></returns>
        public static bool CheckDeviceExists(string mac)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                //先判断是否存在
                string sql = "select top 1 * from DeviceInfo  where Mac ='" + mac + "'";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataSet ds = dbHelper.ExecuteDataSet(cmd);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return true;
                }
                return false;
            }
        }

        #region 通过IP地址检查设备是否存在
        public static bool CheckDeviceExistsByIPAddress(string ipaddress)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                //先判断是否存在
                string sql = "select top 1 * from DeviceInfo  where IPAddress ='" + ipaddress + "'";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataSet ds = dbHelper.ExecuteDataSet(cmd);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return true;
                }
                return false;
            }
        }
        #endregion

        #region 通过机器号判断设备是否存在
        public static bool CheckDeviceExistsByMachineId(int machineId)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                //先判断是否存在
                string sql = $"select top 1 * from DeviceInfo  where DeviceId ={machineId}";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataSet ds = dbHelper.ExecuteDataSet(cmd);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return true;
                }
                return false;
            }
        }
        #endregion


        /// <summary>
        /// 插入设备
        /// </summary>
        /// <param name="device"></param>
        public static void InsertDeviceInfo(DeviceInfo device)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                /*
                DbCommand cmd = dbHelper.GetStoredProcCommond("InsertDeviceInfo");
                dbHelper.AddInParameter(cmd, "@PlaceId", DbType.String, device.PlaceId);
                dbHelper.AddInParameter(cmd, "@deviceName", DbType.String, device._DeviceName);
                dbHelper.AddInParameter(cmd, "@Mac", DbType.String, device._Mac);
                dbHelper.AddInParameter(cmd, "@DeviceId", DbType.Int32, device._MachineId);
                dbHelper.AddInParameter(cmd, "@IPAddress", DbType.String, device._IPAddress);
                dbHelper.AddInParameter(cmd, "@SubNet", DbType.String, device._SubnetMark);
                dbHelper.AddInParameter(cmd, "@GateWay", DbType.String, device._Gateway);
                dbHelper.AddInParameter(cmd, "@Port", DbType.String, device._Port);
                dbHelper.AddInParameter(cmd, "@HardVersion", DbType.String, device._HardVersion);
                dbHelper.AddInParameter(cmd, "@SoftVersion", DbType.String, device._SoftVersion);
                dbHelper.ExecuteNonQuery(cmd);
                */
            }
        }

        /*
        /// <summary>
        /// 根据设备ID获取设备详细信息
        /// </summary>
        /// <param name="devId"></param>
        /// <returns></returns>
        public static DBDevice GetDBDeviceByDevId(string devId)
        {
            string sql = "Select * from MJ_deviceInfo where deviceId =" + devId;
            DbHelper db = new DbHelper();
            DbCommand cmd = db.GetSqlStringCommond(sql);
            DataTable dt = db.ExecuteDataTable(cmd);
            DBDevice device = new DBDevice();
            string mac = string.Empty;
            foreach (DataRow row in dt.Rows)
            {
                device.DevId = Convert.ToInt32(row["DeviceId"]);
                device.DevName = row["DeviceName"].ToString();
                device.GateWay = row["GateWay"].ToString();
                device.IP = row["IPAdress"].ToString();
                mac = row["MAC"].ToString();
                device.PlaceId = Convert.ToInt32(row["PlaceId"]);
                device.SubNet = row["SubNet"].ToString();
            }
            for (int i = 0; i < mac.Length; i += 2)
            {
                device.MAC += mac.Substring(i, 2);
                if (i != 10)
                {
                    device.MAC += "-";
                }
            }
            return device;
        }
         * */
        #region 获取Udp设备信息
        /// <summary>
        /// 根据设备ID获取设备详细信息
        /// </summary>
        /// <param name="devId"></param>
        /// <returns></returns>
        public static DeviceInfo GetUdpDeviceInfoByDevId(int devId)
        {
            DeviceInfo device = null;
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "Select * from DeviceInfo  where deviceId =" + devId.ToString();
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                foreach (DataRow row in dt.Rows)
                {
                    /*
                    device = new DeviceInfo();
                    device._MachineId = Convert.ToInt32(row["DeviceID"]);
                    device._DeviceName = row["DeviceName"].ToString();
                    device._Mac = row["MAC"].ToString();
                    device._IPAddress = row["IPAddress"].ToString();
                    device._SubnetMark = row["Subnet"].ToString();
                    device._Gateway = row["GateWay"].ToString();
                    device._Port = Convert.ToInt32(row["Port"]);
                    device._HardVersion = row["HardVersion"].ToString();
                    device._SoftVersion = row["SoftVersion"].ToString();
                    device.Status = Convert.ToInt32(row["Enabled"]);
                    */
                }
            }
            return device;
        }

        /// <summary>
        /// 根据设备ID获取设备详细信息
        /// </summary>
        /// <param name="devId"></param>
        /// <returns></returns>
        public static DeviceInfo GetTcpDeviceInfoByDevId(int devId)
        {
            DeviceInfo device = null;
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "Select * from DeviceInfo  where deviceId =" + devId.ToString();
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                foreach (DataRow row in dt.Rows)
                {
                    /*
                    device = new DeviceInfo();
                    device._MachineId = Convert.ToInt32(row["DeviceID"]);
                    device._DeviceName = row["DeviceName"].ToString();
                    device._Mac = row["MAC"].ToString();
                    device._IPAddress = row["IPAddress"].ToString();
                    device._SubnetMark = row["Subnet"].ToString();
                    device._Gateway = row["GateWay"].ToString();
                    device._Port = Convert.ToInt32(row["Port"]);
                    device._HardVersion = row["HardVersion"].ToString();
                    device._SoftVersion = row["SoftVersion"].ToString();
                    */
                }
            }
            return device;
        }

        #endregion

        #region 获取所有udp设备
        /// <summary>
        /// 获取数据库所有设备信息
        /// </summary>
        /// <returns></returns>
        public static List<DeviceInfo> GetAllUdpDevices()
        {
            List<DeviceInfo> devList = new List<DeviceInfo>();
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "Select * from DeviceInfo ";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                try
                {
                    /*
                    DataTable dt = dbHelper.ExecuteDataTable(cmd);
                    foreach (DataRow row in dt.Rows)
                    {
                        int devId = Convert.ToInt32(row["DeviceId"]);
                        string mac = row["MAC"].ToString();
                        string ip = row["IPAddress"].ToString();
                        string subNet = row["SubNet"].ToString();
                        string gateWay = row["GateWay"].ToString();
                        int port = Convert.ToInt32(row["Port"]);
                        UdpDevice device = new UdpDevice();
                        device._Mac = mac;
                        device._MachineId = devId;
                        device._DeviceName = row["DeviceName"].ToString();
                        device._IPAddress = ip;
                        device._SubnetMark = subNet;
                        device._Gateway = gateWay;
                        device._Port = port;
                        device._HardVersion = row["HardVersion"].ToString();
                        device._SoftVersion = row["SoftVersion"].ToString();
                        devList.Add(device);
                    }
                    */
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return devList;
        }

        #endregion

        #region 获取所有tcp设备
        /// <summary>
        /// 获取数据库所有设备信息
        /// </summary>
        /// <returns></returns>
        public static List<DeviceInfo> GetAllTcpDevices()
        {
            List<DeviceInfo> devList = new List<DeviceInfo>();
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "Select * from DeviceInfo ";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                try
                {
                    DataTable dt = dbHelper.ExecuteDataTable(cmd);
                    foreach (DataRow row in dt.Rows)
                    {
                        /*
                        TcpDevice device = new TcpDevice();
                        int devId = Convert.ToInt32(row["DeviceId"]);
                        string mac = row["MAC"].ToString();
                        string ip = row["IPAddress"].ToString();
                        string subNet = row["SubNet"].ToString();
                        string gateWay = row["GateWay"].ToString();
                        int port = Convert.ToInt32(row["Port"]);
                        device._Mac = mac;
                        device._MachineId = devId;
                        device._IPAddress = ip;
                        device._DeviceName = row["DeviceName"].ToString();
                        device._SubnetMark = subNet;
                        device._Gateway = gateWay;
                        device._Port = port;
                        device._HardVersion = row["HardVersion"].ToString();
                        device._SoftVersion = row["SoftVersion"].ToString();
                        devList.Add(device);
                        */
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return devList;
        }
        #endregion

        /*
        /// <summary>
        /// 更新设备信息
        /// </summary>
        /// <param name="device"></param>
        public static void UpdateDeviceInfo(DBDevice device)
        {
            using (DbHelper dbHelper = new DbHelper())
            {
                DbCommand cmd = dbHelper.GetStoredProcCommond("MJ_UpdateDeviceInfo");
                dbHelper.AddInParameter(cmd, "@DeviceId", DbType.String, device.DevId);
                dbHelper.AddInParameter(cmd, "@PlaceId", DbType.String, device.PlaceId);
                dbHelper.AddInParameter(cmd, "@deviceName", DbType.String, device.DevName);
                dbHelper.AddInParameter(cmd, "@IP", DbType.String, device.IP);
                dbHelper.AddInParameter(cmd, "@SubNet", DbType.String, device.SubNet);
                dbHelper.AddInParameter(cmd, "@GateWay", DbType.String, device.GateWay);
                dbHelper.AddInParameter(cmd, "@Port", DbType.String, device.Port);
                dbHelper.AddInParameter(cmd, "@Mac", DbType.String, device.MAC.Replace("-", ""));

                dbHelper.ExecuteNonQuery(cmd);
            }
        }
        */
        /// <summary>
        /// 检查门是否存在,存在返回true,不存在返回false
        /// </summary>
        /// <param name="devId"></param>
        /// <param name="doorId"></param>
        /// <returns></returns>
        public static bool CheckDoorExists(int devId, int doorId)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                //先判断是否存在
                string sql = "select top 1 * from MJ_DoorInfo where DeviceId =" + devId + " and DoorId = " + doorId;
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataSet ds = dbHelper.ExecuteDataSet(cmd);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return true;
                }
                return false;
            }
        }
        /// <summary>
        /// 门增加，修改
        /// </summary>
        /// <param name="DevId"></param>
        /// <param name="doorId"></param>
        /// <param name="doorName"></param>
        public static void InsertDoorInfo(int DevId, int doorId, string doorName)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                DbCommand cmd = dbHelper.GetStoredProcCommond("MJ_DoorEdit");
                dbHelper.AddInParameter(cmd, "@DevId", DbType.Int32, DevId);
                dbHelper.AddInParameter(cmd, "@DoorId", DbType.Int32, doorId);
                dbHelper.AddInParameter(cmd, "@doorName", DbType.String, doorName);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }

        /// <summary>
        /// 删除门
        /// </summary>
        /// <param name="devId"></param>
        /// <param name="doorId"></param>
        public static void DeleteDoor(int devId, int doorId)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "Delete From MJ_DoorInfo where DeviceId = " + devId.ToString() + " and doorId = " + doorId.ToString();
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }


        #endregion

        #region 部门人员卡操作

        public static DataTable GetCardIOCount()
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "GetIOCount";
                DbCommand cmd = dbHelper.GetStoredProcCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                return dt;
            }
        }
        /// <summary>
        /// 通过EMpId查找员工
        /// </summary>
        /// <param name="empId"></param>
        /// <returns></returns>
        public static EmpInfo GetEmpInfoByCardNo(string cardNo)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = $@"Select b.DeptId,b.DeptName,a.* From EmpInfo a,DeptInfo b where a.DeptId = b.DeptId and a.ICCardNo ='{cardNo}' Or a.IDSerialNo ='{cardNo}' Or a.IDCardNo ='{cardNo}'";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                EmpInfo emp = null;
                foreach (DataRow row in dt.Rows)
                {
                    emp = new EmpInfo();
                    emp.DeptId = Convert.ToInt32(row["DeptId"]);
                    emp.DeptName = row["DeptName"].ToString();
                    emp.EmpId = Convert.ToInt32(row["EmpId"]);
                    emp.EmpCode = row["EmpCode"].ToString();
                    emp.EmpName = row["EmpName"].ToString();
                    emp.IOFlag = Convert.ToInt32(row["IOFlag"]);
                    emp.Photo = row["Photo"].ToString().Equals(string.Empty) ? new Bitmap(Environment.CurrentDirectory + @"/Image/DefaultPhoto.png") : GetPhotoFromArray((byte[])row["Photo"]);
                }
                return emp;
            }
        }

        #region 根据卡号获取人员信息
        public static EmpInfo GetEmpInfoByCardNo_BBC(string cardNo)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "Select b.DeptId,b.DeptName,a.EmpId,a.EmpCode,a.EmpName,a.Photo,c.CardId,c.CardNo,a.UserId from EmpInfo a,DeptInfo b,CardInfo c ";
                sql = sql + " where a.DeptId = b.DeptId and a.EmpId = c.EmpId and c.CardStatus = 1 and c.CardNo ='" + cardNo + "'";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                EmpInfo emp = null;
                foreach (DataRow row in dt.Rows)
                {
                    emp = new EmpInfo();
                    emp.DeptId = Convert.ToInt32(row["DeptId"]);
                    emp.EmpId = Convert.ToInt32(row["EmpId"]);
                    emp.EmpCode = row["EmpCode"].ToString();
                    emp.EmpName = row["EmpName"].ToString();
                    emp.UserId = row["UserId"].ToString();
                    emp.Photo = row["Photo"].ToString().Equals(string.Empty) ? new Bitmap(Environment.CurrentDirectory + @"/Image/DefaultPhoto.png") : GetPhotoFromArray((byte[])row["Photo"]);
                }
                return emp;
            }
        }
        #endregion


        /// <summary>
        /// 清除人员所有权限
        /// </summary>
        /// <param name="empId"></param>
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

        #region 清除卡所有权限
        public static void ClearAllRightsOfCard(int empId, int cardId)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "ClearAllRightsOfCard";
                DbCommand cmd = dbHelper.GetStoredProcCommond(sql);
                dbHelper.AddInParameter(cmd, "@EmpId", DbType.Int32, empId);
                dbHelper.AddInParameter(cmd, "@CardId", DbType.Int32, cardId);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }
        #endregion

        /// <summary>
        /// 获取人员对控制器权限
        /// </summary>
        /// <param name="deviceId"></param>
        /// <param name="doorId"></param>
        /// <returns></returns>
        public static DataTable GetRightsByEmpId(int empId)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                DbCommand cmd = dbHelper.GetStoredProcCommond("GetRightByEmpId");
                dbHelper.AddInParameter(cmd, "@EmpId", DbType.Int32, empId);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                return dt;
            }
        }
        /// <summary>
        /// 查找员工跟卡
        /// </summary>
        /// <param name="empcode"></param>
        /// <param name="empname"></param>
        /// <returns></returns>
        public static DataTable SearchEmpAndCard(string empcode, string empname, int type)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                DbCommand cmd = dbHelper.GetStoredProcCommond("FindEmpAndCardList");
                dbHelper.AddInParameter(cmd, "@EmpCode", DbType.String, empcode);
                dbHelper.AddInParameter(cmd, "@EmpName", DbType.String, empname);
                dbHelper.AddInParameter(cmd, "@Type", DbType.Int32, type);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                return dt;
            }
        }

        /// <summary>
        /// 根据部门编号获取人员列表
        /// </summary>
        /// <param name="deptId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static DataTable GetEmpAndCardListByDeptId(int deptId, int type)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "GetEmpAndCardListByDeptId";
                DbCommand cmd = dbHelper.GetStoredProcCommond(sql);
                dbHelper.AddInParameter(cmd, "@DeptId", DbType.Int32, deptId);
                dbHelper.AddInParameter(cmd, "@Type", DbType.Int32, type);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                return dt;
            }
        }
        /// <summary>
        /// 根据部门编号获取人员列表
        /// </summary>
        /// <param name="deptId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static DataTable GetEmpAndCardListOfDeptId(int deptId, int type)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "GetEmpAndCardListOfDeptId";
                DbCommand cmd = dbHelper.GetStoredProcCommond(sql);
                dbHelper.AddInParameter(cmd, "@DeptId", DbType.Int32, deptId);
                dbHelper.AddInParameter(cmd, "@Type", DbType.Int32, type);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                return dt;
            }
        }

        /// <summary>
        /// 根据部门编号获取人员列表
        /// </summary>
        /// <param name="deptId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static DataTable GetEmpAndCardListOfDeptId(int deptId, int type, int cardType)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "GetEmpAndCardListOfDeptIdByCardType";
                DbCommand cmd = dbHelper.GetStoredProcCommond(sql);
                dbHelper.AddInParameter(cmd, "@DeptId", DbType.Int32, deptId);
                dbHelper.AddInParameter(cmd, "@Type", DbType.Int32, type);
                dbHelper.AddInParameter(cmd, "@CardType", DbType.Int32, cardType);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                return dt;
            }
        }

        public static DataTable FindEmpList(string empCode, string empName, string cardNo)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "FindEmpInfo";
                DbCommand cmd = dbHelper.GetStoredProcCommond(sql);
                dbHelper.AddInParameter(cmd, "@EmpCode", DbType.String, empCode);
                dbHelper.AddInParameter(cmd, "@EmpName", DbType.String, empName);

                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                return dt;
            }
        }

        /// <summary>
        /// 查找员工和卡信息
        /// </summary>
        /// <param name="empCode"></param>
        /// <param name="empName"></param>
        /// <param name="cardNo"></param>
        /// <returns></returns>
        public static DataTable FindEmpAndCardList(string empCode, string empName, int cardType, string cardNo)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "FindEmpAndCard";
                DbCommand cmd = dbHelper.GetStoredProcCommond(sql);
                dbHelper.AddInParameter(cmd, "@EmpCode", DbType.String, empCode);
                dbHelper.AddInParameter(cmd, "@EmpName", DbType.String, empName);
                dbHelper.AddInParameter(cmd, "@CardType", DbType.Int32, cardType);
                dbHelper.AddInParameter(cmd, "@CardNo", DbType.String, cardNo);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                return dt;
            }
        }

        public static DataTable GetEmpInfoOnCondition(int deptId, int deptType, string empCode, string empName)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "GetEmpInfoBySearch";
                DbCommand cmd = dbHelper.GetStoredProcCommond(sql);
                dbHelper.AddInParameter(cmd, "@DeptId", DbType.Int32, deptId);
                dbHelper.AddInParameter(cmd, "@DeptType", DbType.String, deptType);
                dbHelper.AddInParameter(cmd, "@EmpCode", DbType.String, empCode);
                dbHelper.AddInParameter(cmd, "@EmpName", DbType.String, empName);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                return dt;

            }
        }

        /// <summary>
        /// 查找人员列表
        /// </summary>
        /// <param name="empCode"></param>
        /// <param name="empName"></param>
        /// <returns></returns>
        public static DataTable FindEmpList(string empCode, string empName)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "FindEmpInfo";
                DbCommand cmd = dbHelper.GetStoredProcCommond(sql);
                dbHelper.AddInParameter(cmd, "@EmpCode", DbType.String, empCode);
                dbHelper.AddInParameter(cmd, "@EmpName", DbType.String, empName);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                return dt;
            }
        }
        /// <summary>
        /// 获取部门有卡的人员
        /// </summary>
        /// <param name="deptId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static DataTable GetEmpListHasCardByDeptId(int deptId, int type)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "GetEmpListHasCardByDeptId";
                DbCommand cmd = dbHelper.GetStoredProcCommond(sql);
                dbHelper.AddInParameter(cmd, "@DeptId", DbType.Int32, deptId);
                dbHelper.AddInParameter(cmd, "@Type", DbType.Int32, type);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                return dt;
            }
        }
        /// <summary>
        /// 修改部门信息
        /// </summary>
        /// <param name="parDeptId"></param>
        /// <param name="_DeptId"></param>
        /// <param name="deptName"></param>
        public static void UpdateDept(int parDeptId, int _DeptId, string deptName, int deptCode, int deptCodeLength, int bindingEmpcode)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = string.Format("Update DeptInfo Set ParDeptId ={0}, DeptName = '{1}',DeptCode = {2},DeptCodeLength ={3},IsBindingEmpCode ={4} where DeptId = {5}", parDeptId, deptName, deptCode, deptCodeLength, bindingEmpcode, _DeptId);
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }
        /// <summary>
        /// 插入记录
        /// </summary>
        /// <param name="OperName"></param>
        /// <param name="recDatetime"></param>
        /// <param name="LogObject"></param>
        /// <param name="LogAction"></param>
        /// <param name="LogMessage"></param>
        /// <param name="LogType"></param>
        public static void InsertLog(string OperName, string recDatetime, string LogObject, string LogAction, string LogMessage, int LogType)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "InsertOperLog";
                DbCommand cmd = dbHelper.GetStoredProcCommond(sql);
                dbHelper.AddInParameter(cmd, "@OperName", DbType.String, OperName);
                dbHelper.AddInParameter(cmd, "@recDatetime", DbType.String, recDatetime);
                dbHelper.AddInParameter(cmd, "@Object", DbType.String, LogObject);
                dbHelper.AddInParameter(cmd, "@LogAction", DbType.String, LogAction);
                dbHelper.AddInParameter(cmd, "@LogMessage", DbType.String, LogMessage);
                dbHelper.AddInParameter(cmd, "@LogType", DbType.String, LogType);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }
        /// <summary>
        /// 获取部门没卡的人员
        /// </summary>
        /// <param name="deptId"></param>
        /// <returns></returns>
        public static DataTable GetEmpListHasNoCard(int deptId, int flag)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "GetEmpHasNoCard";
                DbCommand cmd = dbHelper.GetStoredProcCommond(sql);
                dbHelper.AddInParameter(cmd, "@DeptId", DbType.String, deptId);
                dbHelper.AddInParameter(cmd, "@Flag", DbType.String, flag);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                return dt;
            }
        }
        /// <summary>
        /// 查找本部门还没卡的人员
        /// </summary>
        /// <returns></returns>
        public static EmpInfo GetNetEmpIdOfDeptHasNoCard(string empCode)
        {
            EmpInfo emp = null;
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "GetNextEmpOfDeptHasNoCard";
                DbCommand cmd = dbHelper.GetStoredProcCommond(sql);
                dbHelper.AddInParameter(cmd, "@EmpCode", DbType.String, empCode);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                foreach (DataRow row in dt.Rows)
                {
                    emp = new EmpInfo();
                    emp.EmpId = Convert.ToInt32(row["EmpId"]);
                    emp.EmpCode = row["Empcode"].ToString();
                    emp.EmpName = row["EmpName"].ToString();
                }
            }
            return emp;
        }
        /// <summary>
        /// 根据字段查找内容
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public static string GetContentByColumn(int empId, int columnIndex)
        {
            string content = string.Empty;
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string columnName = string.Empty;
                string sql = "Select * from CardPara where CId =" + columnIndex;
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                foreach (DataRow row in dt.Rows)
                {
                    columnName = row["ColumnName"].ToString();
                }
                if (!columnName.Equals(string.Empty))
                {
                    string s = "Select " + columnName + " from EmpInfo a Left Join CardInfo b On a.EmpId = b.EmpId and b.cardstatus = 1 where a.EmpId =" + empId.ToString();
                    s = "exec " + "('" + s + "')";
                    DbCommand cmd1 = dbHelper.GetSqlStringCommond(s);
                    DataTable dt1 = dbHelper.ExecuteDataTable(cmd1);
                    foreach (DataRow row1 in dt1.Rows)
                    {
                        content = row1[0].ToString();
                    }
                }
            }
            return content;
        }

        public static Photo GetDataBmpListByEmpId(int empid, int _IOFlag)
        {
            Photo photo = null;
            try
            {


                using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
                {
                    string sql = "select EmpCode, Photo from EmpInfo where empId =" + empid.ToString();
                    DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                    DataTable dt = dbHelper.ExecuteDataTable(cmd);
                    foreach (DataRow row in dt.Rows)
                    {

                        byte[] bPhoto = row["Photo"].ToString().Equals(string.Empty) ? new byte[1] { 0x00 } : (byte[])row["Photo"];
                        photo = new Photo();
                        photo.ImageByte = bPhoto;
                        photo.EmpId = (UInt32)empid;

                    }
                }
            }
            catch
            {
                //MessageBox.Show(ex.Message);
            }
            return photo;
        }
        /// <summary>
        /// 根据卡号查找卡信息
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static CardInfo GetCardInfoByCardNo(string cardNo, int type)
        {
            CardInfo cardInfo = null;
            try
            {
                using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
                {
                    var sql = "select * from cardinfo a,TicketType b where a.TicketType = b.RecId  and a.Cardno ='" + cardNo + "' and a.Type =" + type.ToString();
                    DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                    DataTable dt = dbHelper.ExecuteDataTable(cmd);

                    foreach (DataRow row in dt.Rows)
                    {
                        cardInfo = new CardInfo();
                        cardInfo.CardId = Convert.ToInt32(row["CardId"]);
                        cardInfo.Type = Convert.ToInt32(row["Type"]);
                        cardInfo.CardNo = row["CardNo"].ToString();
                        cardInfo.EmpId = Convert.ToInt32(row["EmpId"]);
                        cardInfo.BlackName = Convert.ToInt32(row["BlackName"]);
                        cardInfo.CardType = Convert.ToInt32(row["CardType"]);
                        cardInfo.PhotoName = row["EmpId"].ToString();
                        cardInfo.InRight = Convert.ToInt32(row["InRight"]);
                        cardInfo.OutRight = Convert.ToInt32(row["OutRight"]);
                        cardInfo.VoiceNo = Convert.ToInt32(row["VoiceNo"]);
                        cardInfo.Photo = Convert.ToInt32(row["Photo"]);
                        cardInfo.VacationNo = Convert.ToInt32(row["VacationId"]);
                        cardInfo.InTimeGroupNo = Convert.ToInt32(row["IntimeGroupNo"]);
                        cardInfo.OutTimeGroupNo = Convert.ToInt32(row["OutTimeGroupNo"]);
                        cardInfo.BeginDate = row["BeginDate"].ToString();
                        cardInfo.EndDate = row["EndDate"].ToString();
                        cardInfo.DisplayType1 = Convert.ToInt32(row["DisplayType1"]);
                        cardInfo.Text1 = row["Text1"].ToString();
                        cardInfo.Column1 = Convert.ToInt32(row["Column1"]);
                        cardInfo.Content1 = row["Content1"].ToString();
                        cardInfo.DisplayType2 = Convert.ToInt32(row["DisplayType2"]);
                        cardInfo.Text2 = row["Text2"].ToString();
                        cardInfo.Column2 = Convert.ToInt32(row["Column2"]);
                        cardInfo.Content2 = row["Content2"].ToString();
                        cardInfo.DisplayType3 = Convert.ToInt32(row["DisplayType3"]);
                        cardInfo.Text3 = row["Text3"].ToString();
                        cardInfo.Column3 = Convert.ToInt32(row["Column3"]);
                        cardInfo.Content3 = row["Content3"].ToString();
                    }
                }
            }
            catch
            {
                //LogisTrac.WriteLog(ex);
            }
            return cardInfo;
        }

        #region 根据卡编号获取卡信息
        public static CardInfo GetCardInfoByCardId(int cardId)
        {
            CardInfo cardInfo = null;
            try
            {
                using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
                {
                    var sql = $"select * from cardinfo a,TicketType b where a.TicketType = b.RecId  and  a.CardId ={cardId}";
                    DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                    DataTable dt = dbHelper.ExecuteDataTable(cmd);

                    foreach (DataRow row in dt.Rows)
                    {
                        cardInfo = new CardInfo();
                        cardInfo.CardId = Convert.ToInt32(row["CardId"]);
                        cardInfo.Type = Convert.ToInt32(row["Type"]);
                        cardInfo.CardNo = row["CardNo"].ToString();
                        cardInfo.EmpId = Convert.ToInt32(row["EmpId"]);
                        cardInfo.BlackName = Convert.ToInt32(row["BlackName"]);
                        cardInfo.CardType = Convert.ToInt32(row["CardType"]);
                        cardInfo.PhotoName = row["CardId"].ToString();
                        cardInfo.InRight = Convert.ToInt32(row["InRight"]);
                        cardInfo.OutRight = Convert.ToInt32(row["OutRight"]);
                        cardInfo.VoiceNo = Convert.ToInt32(row["VoiceNo"]);
                        cardInfo.Photo = Convert.ToInt32(row["Photo"]);
                        cardInfo.VacationNo = Convert.ToInt32(row["VacationId"]);
                        cardInfo.InTimeGroupNo = Convert.ToInt32(row["IntimeGroupNo"]);
                        cardInfo.OutTimeGroupNo = Convert.ToInt32(row["OutTimeGroupNo"]);
                        cardInfo.BeginDate = row["BeginDate"].ToString();
                        cardInfo.EndDate = row["EndDate"].ToString();
                        cardInfo.DisplayType1 = Convert.ToInt32(row["DisplayType1"]);
                        cardInfo.Text1 = row["Text1"].ToString();
                        cardInfo.Column1 = Convert.ToInt32(row["Column1"]);
                        cardInfo.Content1 = row["Content1"].ToString();
                        cardInfo.DisplayType2 = Convert.ToInt32(row["DisplayType2"]);
                        cardInfo.Text2 = row["Text2"].ToString();
                        cardInfo.Column2 = Convert.ToInt32(row["Column2"]);
                        cardInfo.Content2 = row["Content2"].ToString();
                        cardInfo.DisplayType3 = Convert.ToInt32(row["DisplayType3"]);
                        cardInfo.Text3 = row["Text3"].ToString();
                        cardInfo.Column3 = Convert.ToInt32(row["Column3"]);
                        cardInfo.Content3 = row["Content3"].ToString();
                    }
                }
            }
            catch
            {
                //LogisTrac.WriteLog(ex);
            }
            return cardInfo;
        }
        #endregion

        /// <summary>
        /// 查找人员的卡信息
        /// </summary>
        /// <param name="empid"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static CardInfo GetCardInfoByEmpId(int empid, int type)
        {
            CardInfo cardInfo = null;
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "select * from cardinfo where cardstatus = 1 and  empid =" + empid.ToString() + " and Type =" + type.ToString();
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);

                foreach (DataRow row in dt.Rows)
                {
                    cardInfo = new CardInfo();
                    cardInfo.CardId = Convert.ToInt32(row["CardId"]);
                    cardInfo.Type = Convert.ToInt32(row["Type"]);
                    cardInfo.CardNo = row["CardNo"].ToString();
                    cardInfo.EmpId = Convert.ToInt32(row["EmpId"]);
                    cardInfo.BlackName = Convert.ToInt32(row["BlackName"]);
                    cardInfo.CardType = Convert.ToInt32(row["CardType"]);
                    cardInfo.PhotoName = row["CardCode"].ToString();
                    cardInfo.InRight = Convert.ToInt32(row["InRight"]);
                    cardInfo.OutRight = Convert.ToInt32(row["OutRight"]);
                    cardInfo.VoiceNo = Convert.ToInt32(row["VoiceNo"]);
                    cardInfo.Photo = Convert.ToInt32(row["Photo"]);
                    cardInfo.VacationNo = Convert.ToInt32(row["VacationId"]);
                    cardInfo.InTimeGroupNo = Convert.ToInt32(row["IntimeGroupNo"]);
                    cardInfo.OutTimeGroupNo = Convert.ToInt32(row["OutTimeGroupNo"]);
                    cardInfo.BeginDate = row["BeginDate"].ToString();
                    cardInfo.EndDate = row["EndDate"].ToString();
                    ///彩屏显示第一行
                    cardInfo.DisplayType1 = Convert.ToInt32(row["DisplayType1"]);
                    cardInfo.Text1 = row["Text1"].ToString();
                    cardInfo.Column1 = Convert.ToInt32(row["Column1"]);
                    cardInfo.Content1 = row["Content1"].ToString();
                    ///彩屏显示第二行
                    cardInfo.DisplayType2 = Convert.ToInt32(row["DisplayType2"]);
                    cardInfo.Text2 = row["Text2"].ToString();
                    cardInfo.Column2 = Convert.ToInt32(row["Column2"]);
                    cardInfo.Content2 = row["Content2"].ToString();
                    ///彩屏显示第三行
                    cardInfo.DisplayType3 = Convert.ToInt32(row["DisplayType3"]);
                    cardInfo.Text3 = row["Text3"].ToString();
                    cardInfo.Column3 = Convert.ToInt32(row["Column3"]);
                    cardInfo.Content3 = row["Content3"].ToString();
                }
            }
            return cardInfo;
        }

        #region 获取卡信息
        public static CardInfo GetCardInfoByEmpIdAndType(int empid, int type)
        {
            CardInfo cardInfo = null;
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "select * from cardinfo where cardstatus = 1 and  empid =" + empid.ToString() + " and Type =" + type.ToString();
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);

                foreach (DataRow row in dt.Rows)
                {
                    cardInfo = new CardInfo();
                    cardInfo.TicketType = Convert.ToInt32(row["TicketType"]);
                    cardInfo.Type = Convert.ToInt32(row["Type"]);
                    cardInfo.CardNo = row["CardNo"].ToString();
                    cardInfo.BeginDate = row["BeginDate"].ToString();
                    cardInfo.EndDate = row["EndDate"].ToString();
                }
            }
            return cardInfo;
        }
        #endregion

        /// <summary>
        /// 修改员工信息
        /// </summary>
        /// <param name="emp"></param>


        /// <summary>
        /// 根据部门ID查找部门名称
        /// </summary>
        /// <param name="deptId"></param>
        /// <returns></returns>
        public static string GetDeptNameByDeptId(int deptId)
        {
            string deptName = string.Empty;
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "Select DeptName from DeptInfo where DeptId =" + deptId.ToString();
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                foreach (DataRow row in dt.Rows)
                {
                    deptName = row["DeptName"].ToString();
                }
            }
            return deptName;
        }

        /// <summary>
        /// 根据部门ID查找部门名称
        /// </summary>
        /// <param name="deptId"></param>
        /// <returns></returns>
        public static string GetDeptNameByEmpId(int empId)
        {
            string deptName = string.Empty;
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "Select DeptName from DeptInfo where DeptId =(Select DeptId from EmpInfo where EmpId =" + empId.ToString() + ")";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                foreach (DataRow row in dt.Rows)
                {
                    deptName = row["DeptName"].ToString();
                }
            }
            return deptName;
        }


        /// <summary>
        /// 插入部门信息
        /// </summary>
        /// <param name="parDeptId"></param>
        /// <param name="deptName"></param>
        public static void AddDeptInfo(int parDeptId, string deptName, int deptCode, int deptCodeLength, int isBindingEmpcode)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = string.Format("Insert Into DeptInfo(DeptName,ParDeptId,DeptCode,DeptCodeLength,IsbindingEmpCode) values ('{0}',{1},{2},{3},{4})", deptName, parDeptId, deptCode, deptCodeLength, isBindingEmpcode);
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }



        /// <summary>
        /// 获取新的可用的工号
        /// </summary>
        /// <returns></returns>
        public static string GetNewEmpCode()
        {
            string empCode = string.Empty;
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "select isnull(max(Convert(int,EmpCode)),0) from empInfo ";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                foreach (DataRow row in dt.Rows)
                {
                    empCode = (Convert.ToInt32(row[0]) + 1).ToString();
                }
            }
            int length = empCode.Length;
            for (int i = 0; i < 8 - length; i++)
            {
                empCode = "0" + empCode;
            }
            return empCode;
        }




        /// <summary>
        /// 通过身份证号码检查员工是否已经存在
        /// </summary>
        /// <param name="idCard"></param>
        public static bool CheckEmpExists(string idCard)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "Select top 1 * from EmpInfo where IdentityCard ='" + idCard + "'";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                if (dt.Rows.Count == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// 检查身份证序列号是否已经存在
        /// </summary>
        /// <param name="cardNo"></param>
        /// <returns></returns>
        public static bool CheckCardExists(string cardNo)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "Select top 1 * from EmpInfo where ICCardNo ='" + cardNo + "'";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                if (dt.Rows.Count == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        #region 检查IC卡是否存在
        public static bool CheckICCardExists(string cardNo)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = $"Select top 1 * from EmpInfo where ICCardNo ='{cardNo}'";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                if (dt.Rows.Count == 1)
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

        #region 检查IC卡是否存在
        public static bool CheckIDSerialNoExists(string cardNo)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = $"Select top 1 * from EmpInfo where IDSerialNo ='{cardNo}'";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                if (dt.Rows.Count == 1)
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

        #region 检查IC卡是否存在
        public static bool CheckIDCardNoExists(string cardNo)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = $"Select top 1 * from EmpInfo where IDCardNo ='{cardNo}'";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                if (dt.Rows.Count == 1)
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

        /// <summary>
        /// 检查卡号是否存在
        /// </summary>
        /// <param name="flag"></param>
        /// <param name="cardNo"></param>
        /// <returns></returns>
        public static bool CheckCardExists(int flag, string cardNo)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = $"Select top 1 * from CardInfo where CardStatus = 1 and Type ={flag}  and  CardNo ='{cardNo}'";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                if (dt.Rows.Count == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// 删除员工以及卡信息
        /// </summary>
        /// <param name="empid"></param>
        public static void DeleteEmpAndCard(int empid)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "DeleteEmpInfoAndCardInfo";
                DbCommand cmd = dbHelper.GetStoredProcCommond(sql);
                dbHelper.AddInParameter(cmd, "@EmpId", DbType.Int32, empid);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }
        /// <summary>
        /// 获取操作员ID列表
        /// </summary>
        /// <param name="OperId"></param>
        /// <returns></returns>
        public static List<int> GetMenuIdListByOperId(int OperId)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                List<int> menuList = new List<int>();
                string sql = "select menuId from MJ_MenuOfOper where OperId =" + OperId.ToString() + "order by MenuId";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                foreach (DataRow row in dt.Rows)
                {
                    int MenuId = Convert.ToInt32(row["MenuId"]);
                    menuList.Add(MenuId);
                }
                return menuList;
            }
        }
        /// <summary>
        /// 获取操作员列表
        /// </summary>
        /// <returns></returns>
        public static DataTable GetOperList()
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "Select OperId,Op_Name,descr from OperInfo";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                return dt;
            }
        }
        /// <summary>
        /// 修改人员对门的权限
        /// </summary>
        /// <param name="deviceId"></param>
        /// <param name="rightOfDoor1"></param>
        /// <param name="rightOfDoor2"></param>
        /// <param name="empId"></param>
        public static void InsertDeviceRight(int empId, int deviceId, DBHelper dbHelper)
        {
            string sql = "InsertRight";
            DbCommand cmd = dbHelper.GetStoredProcCommond(sql);
            dbHelper.AddInParameter(cmd, "@EmpId", DbType.Int32, empId);
            dbHelper.AddInParameter(cmd, "@DeviceId", DbType.Int32, deviceId);
            dbHelper.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="empId"></param>
        /// <param name="deviceId"></param>
        /// <param name="dbHelper"></param>
        public static void InsertEmpRight(int empId, int deviceId)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "InsertEmpRight";
                DbCommand cmd = dbHelper.GetStoredProcCommond(sql);
                dbHelper.AddInParameter(cmd, "@EmpId", DbType.Int32, empId);
                dbHelper.AddInParameter(cmd, "@DeviceId", DbType.Int32, deviceId);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }
        public static void InsertEmpRight(int cardId, DevRightOfEmp right, int empId)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "InsertRightOfEmp";
                DbCommand cmd = dbHelper.GetStoredProcCommond(sql);
                dbHelper.AddInParameter(cmd, "@CardId", DbType.Int32, cardId);
                dbHelper.AddInParameter(cmd, "@EmpId", DbType.Int32, empId);
                dbHelper.AddInParameter(cmd, "@DeviceId", DbType.Int32, right.DeviceId);
                dbHelper.AddInParameter(cmd, "@Right", DbType.Int32, right.Right);
                dbHelper.ExecuteNonQuery(cmd);
            }

        }

        /// <summary>
        /// 通过EMpId查找员工
        /// </summary>
        /// <param name="empId"></param>
        /// <returns></returns>
        public static EmpInfo GetEmpInfoByEmpId(int empId)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {

                string sql = $"Select * From EmpInfo a,DeptInfo b Where a.DeptId = b.DeptId and a.EmpId = {empId}";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                EmpInfo emp = null;
                foreach (DataRow row in dt.Rows)
                {
                    emp = new EmpInfo();
                    emp.DeptId = Convert.ToInt32(row["DeptId"]);
                    emp.EmpId = empId;
                    emp.EmpCode = row["EmpCode"].ToString();
                    emp.EmpName = row["EmpName"].ToString();
                    emp.EnglishName = row["EnglishName"].ToString();
                    emp.Sex = row["Sex"].ToString();
                    emp.IdentityCard = row["IdentityCard"].ToString();
                    emp.Telephone = row["Telephone"].ToString();
                    emp.BirthDay = row["BirthDay"].ToString();
                    emp.Nation = row["Nationality"].ToString();
                    emp.BornEarth = row["BornEarth"].ToString();
                    emp.Marrige = row["Marrige"].ToString();
                    emp.JoinDate = row["JoinDate"].ToString();
                    emp.Photo = row["Photo"].ToString().Equals(string.Empty) ? null : GetPhotoFromArray((byte[])row["Photo"]);
                    emp.TicketType = Convert.ToInt32(row["TicketType"]);
                    emp.BeginDate = row["BeginDate"].ToString();
                    emp.EndDate = row["EndDate"].ToString();
                    emp.ICCardId = Convert.ToInt32(row["ICCardId"]);
                    emp.ICCardNo = row["ICCardNo"].ToString();
                    emp.IDSerialId = Convert.ToInt32(row["IDSerialId"]);
                    emp.IDSerialNo = row["IDSerialNo"].ToString();
                    emp.IDCardId = Convert.ToInt32(row["IDCardId"]);
                    emp.IDCardNo = row["IDCardNo"].ToString();
                    emp.FingerId1 = Convert.ToInt32(row["FingerId1"]);
                    emp.FingerData1 = (byte[])row["FingerData1"];
                    emp.FingerId2 = Convert.ToInt32(row["FingerId2"]);
                    emp.FingerData2 = (byte[])row["FingerData2"];
                    emp.FingerId3 = Convert.ToInt32(row["FingerId3"]);
                    emp.FingerData3 = (byte[])row["FingerData3"];
                }
                return emp;
            }
        }

        /// <summary>
        /// 通过EMpId查找员工
        /// </summary>
        /// <param name="empId"></param>
        /// <returns></returns>
        public static EmpInfo GetEmpInfoByEmpId(int empId, OleDbHelper dbHelper)
        {
            string sql = "Select * from EmpInfo where empid = " + empId;
            DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
            DataTable dt = dbHelper.ExecuteDataTable(cmd);
            EmpInfo emp = null;

            foreach (DataRow row in dt.Rows)
            {
                emp = new EmpInfo();
                emp.DeptId = Convert.ToInt32(row["DeptId"]);
                emp.EmpId = Convert.ToInt32(row["EmpId"]);
                emp.EmpCode = row["EmpCode"].ToString();
                emp.EmpName = row["EmpName"].ToString();
                emp.EnglishName = row["EnglishName"].ToString();
                emp.Sex = row["Sex"].ToString();
                emp.IDCardNo = row["IdentityCard"].ToString();
                emp.Telephone = row["Telephone"].ToString();
                emp.BirthDay = row["BirthDay"].ToString();
                emp.Nation = row["Nationality"].ToString();
                emp.BornEarth = row["BornEarth"].ToString();
                emp.Marrige = row["Marrige"].ToString();
                emp.JoinDate = row["JoinDate"].ToString();
                ///object obj = row["Photo"];
                emp.Photo = row["Photo"].ToString().Equals(string.Empty) ? null : GetPhotoFromArray((byte[])row["Photo"]);
            }
            return emp;
        }


        /// <summary>
        /// 根据部门ID查找员工
        /// </summary>
        /// <param name="deptId"></param>
        /// <returns></returns>
        public static DataTable GetEmpListByDeptId(int deptId)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "select EmpId,empcode,empname from empinfo where  deptid =" + deptId + "  order by EmpId";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                return dt;
            }
        }

        public static DataTable GetEmpListByDeptIdHasPhoto(int deptId)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "select EmpId,empcode,empname from empinfo where DataLength(Photo) >10 and  deptid =" + deptId + "  order by EmpId";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                return dt;
            }
        }

        /// <summary>
        /// 根据部门ID查找员工
        /// </summary>
        /// <param name="deptId"></param>
        /// <returns></returns>
        public static DataTable GetEmpListByDeptIdAll(int deptId)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                DbCommand cmd = dbHelper.GetStoredProcCommond("GetAllEmpByDeptId");
                dbHelper.AddInParameter(cmd, "@DeptId", DbType.Int32, Convert.ToInt32(deptId));
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                return dt;
            }
        }
        public static DataTable GetEmpListByDeptIdAllHasPhoto(int deptId)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                DbCommand cmd = dbHelper.GetStoredProcCommond("GetAllEmpByDeptIdHasPhoto");
                dbHelper.AddInParameter(cmd, "@DeptId", DbType.Int32, Convert.ToInt32(deptId));
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                return dt;
            }
        }
        /// <summary>
        /// 根据部门ID查找员工和卡
        /// </summary>
        /// <param name="deptId"></param>
        /// <returns></returns>
        public static DataTable GetEmpAndCardByDeptId(string deptId)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "select a.EmpId,a.empcode,a.empname, (case b.IoFlag when 3 then '场外' else '场内' end) as IOFlag ,'改变状态' as Operate from empinfo a,CardInfo b where a.empId = b.empId and b.cardstatus = 1 and  a.deptid =" + deptId + "order by a.EmpId";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                return dt;
            }
        }

        /// <summary>
        /// 根据部门ID查找员工和卡
        /// </summary>
        /// <param name="deptId"></param>
        /// <returns></returns>
        public static DataTable GetEmpAndCardListByDeptId(int deptId)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "GetEmpAndCardListByDeptId";
                DbCommand cmd = dbHelper.GetStoredProcCommond(sql);
                dbHelper.AddInParameter(cmd, "@DeptId", DbType.Int32, deptId);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                return dt;
            }
        }

        /// <summary>
        /// 根据部门ID查找员工和卡
        /// </summary>
        /// <param name="deptId"></param>
        /// <returns></returns>
        public static DataTable GetEmpAndCardListByTreeNode(int deptId)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {

                string sql = "GetEmpAndCardListByTreeNode";
                DbCommand cmd = dbHelper.GetStoredProcCommond(sql);
                dbHelper.AddInParameter(cmd, "@DeptId", DbType.Int32, deptId);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                return dt;
            }
        }
        /// <summary>
        /// 根据部门ID查找有卡的员工
        /// </summary>
        /// <param name="deptId"></param>
        /// <returns></returns>
        public static DataTable GetEmpHasCardListByDeptId(string deptId)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                DbCommand cmd = dbHelper.GetStoredProcCommond("MJ_GetEmpHasCardListByDeptId");
                dbHelper.AddInParameter(cmd, "@DeptId", DbType.Int32, Convert.ToInt32(deptId));
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                return dt;
            }
        }

        /*
        /// <summary>
        /// 通过员工工号查找卡信息
        /// </summary>
        /// <param name="empCode"></param>
        /// <returns></returns>
        public static CardInfo GetCardInfoByEmpCode(string empCode)
        {
            using (DbHelper dbHelper = new DbHelper())
            {
                string sql = "select * from MJ_cardinfo where cardstatus = 1 and  empid =(select top 1 Id from PersonInfo where Number ='" + empCode + "')";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                CardInfo cardInfo = null;
                /*
                foreach (DataRow row in dt.Rows)
                {
                    cardInfo = new CardInfo();
                    cardInfo.CardId = Convert.ToInt32(row["CardId"]);
                    cardInfo.CardNo = row["CardNo"].ToString();
                    cardInfo.BlackName = Convert.ToInt32(row["BlackName"]);
                    cardInfo.Door1GroupNo = Convert.ToInt32(row["Door1TimeGroup"]);
                    cardInfo.Door2GroupNo = Convert.ToInt32(row["Door2TimeGroup"]);
                    cardInfo.BeginDate = row["BeginDate"].ToString();
                    cardInfo.EndDate = row["EndDate"].ToString();
                }

                return cardInfo;
            }
        }

        /// <summary>
        /// 通过员工编号查找卡信息
        /// </summary>
        /// <param name="empId"></param>
        /// <returns></returns>
        public static DBCardInfo GetCardInfoByEmpId(string  empId)
        {
            using (DbHelper dbHelper = new DbHelper())
            {
                string sql = "select * from cardinfo where cardstatus = 1 and  empid =" + empId;
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                DBCardInfo cardInfo = null;
                foreach (DataRow row in dt.Rows)
                {
                    cardInfo = new DBCardInfo();
                    cardInfo.CardId = Convert.ToInt32(row["CardId"]);
                    cardInfo.Type = Convert.ToInt32(row["Type"]);
                    cardInfo.CardNo = row["CardNo"].ToString();
                    cardInfo.EmpId = Convert.ToInt32(row["Empid"]);
                    cardInfo.BlackName = Convert.ToInt32(row["BlackName"]);
                    cardInfo.CardType = Convert.ToInt32(row["CardType"]);
                    cardInfo.CardCode = row["CardCode"].ToString();
                    cardInfo.InRight = Convert.ToInt32(row["Inright"]);
                    cardInfo.OutRight = Convert.ToInt32(row["OutRight"]);
                    cardInfo.VoiceNo = Convert.ToInt32(row["VoiceNo"]);
                    cardInfo.Photo = Convert.ToInt32(row["Photo"]);
                    cardInfo.VacationId = Convert.ToInt32(row["VacationID"]);
                    cardInfo.InTimeGroupNo = Convert.ToInt32(row["InTimeGroupNo"]);
                    cardInfo.OutTimeGroupNo = Convert.ToInt32(row["OutTimeGroupNo"]);
                    cardInfo.BeginDate = row["BeginDate"].ToString();
                    cardInfo.EndDate = row["EndDate"].ToString();
                    cardInfo.DeptName = row["DeptName"].ToString();
                }
                return cardInfo;
            }
        }
        */
        /// <summary>
        /// 根据工号或者姓名查找员工信息
        /// </summary>
        /// <param name="empcode"></param>
        /// <param name="empname"></param>
        /// <returns></returns>
        public static DataTable SearchEmpInfo(string empcode, string empname)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                DbCommand cmd = dbHelper.GetStoredProcCommond("FindEmpInfo");
                dbHelper.AddInParameter(cmd, "@EmpCode", DbType.String, empcode);
                dbHelper.AddInParameter(cmd, "@EmpName", DbType.String, empname);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                return dt;
            }
        }

        /// <summary>
        /// 新增卡
        /// </summary>
        /// <param name="empCode"></param>
        /// <param name="card"></param>
        public static void InsertCard(int empId, CardInfo card)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                DbCommand cmd = dbHelper.GetStoredProcCommond("InsertCardInfo");
                dbHelper.AddInParameter(cmd, "@EmpId", DbType.String, empId);
                dbHelper.AddInParameter(cmd, "@Type", DbType.Int32, card.Type);
                dbHelper.AddInParameter(cmd, "@CardNo", DbType.String, card.CardNo);
                dbHelper.AddInParameter(cmd, "@BlackName", DbType.Int32, card.BlackName);
                dbHelper.AddInParameter(cmd, "@CardType", DbType.Int32, card.CardType);
                dbHelper.AddInParameter(cmd, "@CardCode", DbType.String, card.PhotoName);
                dbHelper.AddInParameter(cmd, "@InRight", DbType.Int32, card.InRight);
                dbHelper.AddInParameter(cmd, "@OutRight", DbType.Int32, card.OutRight);
                dbHelper.AddInParameter(cmd, "@VoiceNo", DbType.Int32, card.VoiceNo);
                dbHelper.AddInParameter(cmd, "@Photo", DbType.Int32, card.Photo);
                dbHelper.AddInParameter(cmd, "@VacationId", DbType.Int32, card.VacationNo);
                dbHelper.AddInParameter(cmd, "@InTimeGroupNo", DbType.Int32, card.InTimeGroupNo);
                dbHelper.AddInParameter(cmd, "@OutTimeGroupNo", DbType.Int32, card.OutTimeGroupNo);
                dbHelper.AddInParameter(cmd, "@BeginDate", DbType.String, card.BeginDate);
                dbHelper.AddInParameter(cmd, "@EndDate", DbType.String, card.EndDate);
                dbHelper.AddInParameter(cmd, "@DisplayType1", DbType.Int32, card.DisplayType1);
                dbHelper.AddInParameter(cmd, "@Text1", DbType.String, card.Text1);
                dbHelper.AddInParameter(cmd, "@Column1", DbType.Int32, card.Column1);
                dbHelper.AddInParameter(cmd, "@Content1", DbType.String, card.Content1);
                dbHelper.AddInParameter(cmd, "@DisplayType2", DbType.Int32, card.DisplayType2);
                dbHelper.AddInParameter(cmd, "@Text2", DbType.String, card.Text2);
                dbHelper.AddInParameter(cmd, "@Column2", DbType.Int32, card.Column2);
                dbHelper.AddInParameter(cmd, "@Content2", DbType.String, card.Content2);
                dbHelper.AddInParameter(cmd, "@DisplayType3", DbType.Int32, card.DisplayType3);
                dbHelper.AddInParameter(cmd, "@Text3", DbType.String, card.Text3);
                dbHelper.AddInParameter(cmd, "@Column3", DbType.Int32, card.Column3);
                dbHelper.AddInParameter(cmd, "@Content3", DbType.String, card.Content3);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }

        public static void BatchInsertCard(EmpInfo emp, CardInfo card)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                DbCommand cmd = dbHelper.GetStoredProcCommond("InsertCardInfo");
                dbHelper.AddInParameter(cmd, "@EmpId", DbType.String, emp.EmpId);
                dbHelper.AddInParameter(cmd, "@Type", DbType.Int32, card.Type);
                dbHelper.AddInParameter(cmd, "@CardNo", DbType.String, card.CardNo);
                dbHelper.AddInParameter(cmd, "@BlackName", DbType.Int32, card.BlackName);
                dbHelper.AddInParameter(cmd, "@CardType", DbType.Int32, card.CardType);
                dbHelper.AddInParameter(cmd, "@CardCode", DbType.String, card.PhotoName);
                dbHelper.AddInParameter(cmd, "@InRight", DbType.Int32, card.InRight);
                dbHelper.AddInParameter(cmd, "@OutRight", DbType.Int32, card.OutRight);
                dbHelper.AddInParameter(cmd, "@VoiceNo", DbType.Int32, card.VoiceNo);
                dbHelper.AddInParameter(cmd, "@Photo", DbType.Int32, card.Photo);
                dbHelper.AddInParameter(cmd, "@VacationId", DbType.Int32, card.VacationNo);
                dbHelper.AddInParameter(cmd, "@InTimeGroupNo", DbType.Int32, card.InTimeGroupNo);
                dbHelper.AddInParameter(cmd, "@OutTimeGroupNo", DbType.Int32, card.OutTimeGroupNo);
                dbHelper.AddInParameter(cmd, "@BeginDate", DbType.String, card.BeginDate);
                dbHelper.AddInParameter(cmd, "@EndDate", DbType.String, card.EndDate);
                dbHelper.AddInParameter(cmd, "@DisplayType1", DbType.Int32, 0);
                dbHelper.AddInParameter(cmd, "@Text1", DbType.String, "姓名:");
                dbHelper.AddInParameter(cmd, "@Column1", DbType.Int32, 2);
                dbHelper.AddInParameter(cmd, "@Content1", DbType.String, "姓名:" + emp.EmpName);
                dbHelper.AddInParameter(cmd, "@DisplayType2", DbType.Int32, 0);
                dbHelper.AddInParameter(cmd, "@Text2", DbType.String, "编号:");
                dbHelper.AddInParameter(cmd, "@Column2", DbType.Int32, 1);
                dbHelper.AddInParameter(cmd, "@Content2", DbType.String, "编号:" + emp.EmpCode);
                dbHelper.AddInParameter(cmd, "@DisplayType3", DbType.Int32, 0);
                dbHelper.AddInParameter(cmd, "@Text3", DbType.String, "部门:");
                dbHelper.AddInParameter(cmd, "@Column3", DbType.Int32, 0);
                dbHelper.AddInParameter(cmd, "@Content3", DbType.String, "部门:" + emp.DeptName);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }

        /// <summary>
        /// 根据工号插入卡
        /// </summary>
        /// <param name="empCode"></param>
        /// <param name="card"></param>
        public static void InsertCard(string empCode, CardInfo card)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                DbCommand cmd = dbHelper.GetStoredProcCommond("InsertCardInfoByEmpCode");
                dbHelper.AddInParameter(cmd, "@EmpCode", DbType.String, empCode);
                dbHelper.AddInParameter(cmd, "@Type", DbType.Int32, card.Type);
                dbHelper.AddInParameter(cmd, "@CardNo", DbType.String, card.CardNo);
                dbHelper.AddInParameter(cmd, "@BlackName", DbType.Int32, card.BlackName);
                dbHelper.AddInParameter(cmd, "@CardType", DbType.Int32, card.CardType);
                dbHelper.AddInParameter(cmd, "@CardCode", DbType.String, card.PhotoName);
                dbHelper.AddInParameter(cmd, "@InRight", DbType.Int32, card.InRight);
                dbHelper.AddInParameter(cmd, "@OutRight", DbType.Int32, card.OutRight);
                dbHelper.AddInParameter(cmd, "@VoiceNo", DbType.Int32, card.VoiceNo);
                dbHelper.AddInParameter(cmd, "@Photo", DbType.Int32, card.Photo);
                dbHelper.AddInParameter(cmd, "@VacationId", DbType.Int32, card.VacationNo);
                dbHelper.AddInParameter(cmd, "@InTimeGroupNo", DbType.Int32, card.InTimeGroupNo);
                dbHelper.AddInParameter(cmd, "@OutTimeGroupNo", DbType.Int32, card.OutTimeGroupNo);
                dbHelper.AddInParameter(cmd, "@BeginDate", DbType.String, card.BeginDate);
                dbHelper.AddInParameter(cmd, "@EndDate", DbType.String, card.EndDate);
                dbHelper.AddInParameter(cmd, "@DisplayType1", DbType.Int32, card.DisplayType1);
                dbHelper.AddInParameter(cmd, "@Text1", DbType.String, card.Text1);
                dbHelper.AddInParameter(cmd, "@Column1", DbType.Int32, card.Column1);
                dbHelper.AddInParameter(cmd, "@Content1", DbType.String, card.Content1);
                dbHelper.AddInParameter(cmd, "@DisplayType2", DbType.Int32, card.DisplayType2);
                dbHelper.AddInParameter(cmd, "@Text2", DbType.String, card.Text2);
                dbHelper.AddInParameter(cmd, "@Column2", DbType.Int32, card.Column2);
                dbHelper.AddInParameter(cmd, "@Content2", DbType.String, card.Content2);
                dbHelper.AddInParameter(cmd, "@DisplayType3", DbType.Int32, card.DisplayType3);
                dbHelper.AddInParameter(cmd, "@Text3", DbType.String, card.Text3);
                dbHelper.AddInParameter(cmd, "@Column3", DbType.Int32, card.Column3);
                dbHelper.AddInParameter(cmd, "@Content3", DbType.String, card.Content3);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }

        /// <summary>
        /// 更新卡信息
        /// </summary>
        /// <param name="empCode"></param>
        /// <param name="card"></param>
        public static void UpdateCard(int empId, CardInfo card)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                DbCommand cmd = dbHelper.GetStoredProcCommond("UpdateCardInfo");
                dbHelper.AddInParameter(cmd, "@EmpId", DbType.String, empId);
                dbHelper.AddInParameter(cmd, "@Type", DbType.Int32, card.Type);
                dbHelper.AddInParameter(cmd, "@CardNo", DbType.String, card.CardNo);
                dbHelper.AddInParameter(cmd, "@BlackName", DbType.Int32, card.BlackName);
                dbHelper.AddInParameter(cmd, "@CardType", DbType.Int32, card.CardType);
                dbHelper.AddInParameter(cmd, "@CardCode", DbType.String, card.PhotoName);
                dbHelper.AddInParameter(cmd, "@InRight", DbType.Int32, card.InRight);
                dbHelper.AddInParameter(cmd, "@OutRight", DbType.Int32, card.OutRight);
                dbHelper.AddInParameter(cmd, "@VoiceNo", DbType.Int32, card.VoiceNo);
                dbHelper.AddInParameter(cmd, "@Photo", DbType.Int32, card.Photo);
                dbHelper.AddInParameter(cmd, "@VacationId", DbType.Int32, card.VacationNo);
                dbHelper.AddInParameter(cmd, "@InTimeGroupNo", DbType.Int32, card.InTimeGroupNo);
                dbHelper.AddInParameter(cmd, "@OutTimeGroupNo", DbType.Int32, card.OutTimeGroupNo);
                dbHelper.AddInParameter(cmd, "@BeginDate", DbType.String, card.BeginDate);
                dbHelper.AddInParameter(cmd, "@EndDate", DbType.String, card.EndDate);
                dbHelper.AddInParameter(cmd, "@DisplayType1", DbType.Int32, card.DisplayType1);
                dbHelper.AddInParameter(cmd, "@Text1", DbType.String, card.Text1);
                dbHelper.AddInParameter(cmd, "@Column1", DbType.Int32, card.Column1);
                dbHelper.AddInParameter(cmd, "@Content1", DbType.String, card.Content1);
                dbHelper.AddInParameter(cmd, "@DisplayType2", DbType.Int32, card.DisplayType2);
                dbHelper.AddInParameter(cmd, "@Text2", DbType.String, card.Text2);
                dbHelper.AddInParameter(cmd, "@Column2", DbType.Int32, card.Column2);
                dbHelper.AddInParameter(cmd, "@Content2", DbType.String, card.Content2);
                dbHelper.AddInParameter(cmd, "@DisplayType3", DbType.Int32, card.DisplayType3);
                dbHelper.AddInParameter(cmd, "@Text3", DbType.String, card.Text3);
                dbHelper.AddInParameter(cmd, "@Column3", DbType.Int32, card.Column3);
                dbHelper.AddInParameter(cmd, "@Content3", DbType.String, card.Content3);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }

        #region  更新卡信息
        public static void UpdateCardInfo(int empId, CardInfo card)
        {
            TicketType attribute = GetAttributeOfTypeById(card.TicketType);
            if (attribute == null) throw new Exception("票类不能为空!");
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                DbCommand cmd = dbHelper.GetStoredProcCommond("UpdateCard");
                dbHelper.AddInParameter(cmd, "@EmpId", DbType.Int32, empId);
                dbHelper.AddInParameter(cmd, "@TicketType", DbType.Int32, attribute.RecId);
                dbHelper.AddInParameter(cmd, "@Type", DbType.Int32, card.Type);
                dbHelper.AddInParameter(cmd, "@CardNo", DbType.String, card.CardNo);
                dbHelper.AddInParameter(cmd, "@BlackName", DbType.Int32, card.BlackName);
                dbHelper.AddInParameter(cmd, "@CardType", DbType.Int32, attribute.CardType);
                dbHelper.AddInParameter(cmd, "@CardCode", DbType.String, empId.ToString());
                dbHelper.AddInParameter(cmd, "@InRight", DbType.Int32, attribute.InRight);
                dbHelper.AddInParameter(cmd, "@OutRight", DbType.Int32, attribute.OutRight);
                dbHelper.AddInParameter(cmd, "@VoiceNo", DbType.Int32, attribute.VoiceNo);
                dbHelper.AddInParameter(cmd, "@Photo", DbType.Int32, attribute.Photo);
                dbHelper.AddInParameter(cmd, "@VacationId", DbType.Int32, attribute.VacationId);
                dbHelper.AddInParameter(cmd, "@InTimeGroupNo", DbType.Int32, attribute.IntimeGroupNo);
                dbHelper.AddInParameter(cmd, "@OutTimeGroupNo", DbType.Int32, attribute.OutTimeGroupNo);
                dbHelper.AddInParameter(cmd, "@BeginDate", DbType.String, card.BeginDate);
                dbHelper.AddInParameter(cmd, "@EndDate", DbType.String, card.EndDate);
                dbHelper.AddInParameter(cmd, "@DisplayType1", DbType.Int32, attribute.DisplayType1);
                dbHelper.AddInParameter(cmd, "@Text1", DbType.String, attribute.Text1);
                dbHelper.AddInParameter(cmd, "@Column1", DbType.Int32, attribute.Column1);
                dbHelper.AddInParameter(cmd, "@Content1", DbType.String, attribute.Content1);
                dbHelper.AddInParameter(cmd, "@DisplayType2", DbType.Int32, attribute.DisplayType2);
                dbHelper.AddInParameter(cmd, "@Text2", DbType.String, attribute.Text2);
                dbHelper.AddInParameter(cmd, "@Column2", DbType.Int32, attribute.Column2);
                dbHelper.AddInParameter(cmd, "@Content2", DbType.String, attribute.Content2);
                dbHelper.AddInParameter(cmd, "@DisplayType3", DbType.Int32, attribute.DisplayType3);
                dbHelper.AddInParameter(cmd, "@Text3", DbType.String, attribute.Text3);
                dbHelper.AddInParameter(cmd, "@Column3", DbType.Int32, attribute.Column3);
                dbHelper.AddInParameter(cmd, "@Content3", DbType.String, attribute.Content3);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }
        #endregion

        #region  更新卡信息
        public static void UpdateCardInfoNew(int empId, CardInfo card)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                DbCommand cmd = dbHelper.GetStoredProcCommond("UpdateCardNew");
                dbHelper.AddInParameter(cmd, "@CardId", DbType.String, card.CardId);
                dbHelper.AddInParameter(cmd, "@EmpId", DbType.String, empId);
                dbHelper.AddInParameter(cmd, "@TicketType", DbType.Int32, card.TicketType);
                dbHelper.AddInParameter(cmd, "@Type", DbType.Int32, card.Type);
                dbHelper.AddInParameter(cmd, "@CardNo", DbType.String, card.CardNo);
                dbHelper.AddInParameter(cmd, "@BeginDate", DbType.String, card.BeginDate);
                dbHelper.AddInParameter(cmd, "@EndDate", DbType.String, card.EndDate);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }
        #endregion




        /// <summary>
        /// 注销卡
        /// </summary>
        /// <param name="empCode"></param>
        /// <param name="cardNo"></param>
        public static void CancelCard(string empId, int type)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                DbCommand cmd = dbHelper.GetStoredProcCommond("CancleEmpCard");
                dbHelper.AddInParameter(cmd, "@EmpId", DbType.Int32, Convert.ToInt16(empId));
                dbHelper.AddInParameter(cmd, "@Type", DbType.Int32, type);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }

        /// <summary>
        /// 重新启用卡
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="empId"></param>
        public static void ReUseCard(string cardNo, int empId)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                DbCommand cmd = dbHelper.GetStoredProcCommond("MJ_ReUseCard");
                dbHelper.AddInParameter(cmd, "@EmpId", DbType.Int32, empId);
                dbHelper.AddInParameter(cmd, "@CardNo", DbType.String, cardNo);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }

        /// <summary>
        /// 检查卡号是否已经存在,存在返回true,不存在返回false
        /// </summary>
        /// <param name="cardNo"></param>
        /// <returns></returns>
        public static bool CheckCardExists(int flag, string empCode, string cardNo)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                DbCommand cmd = dbHelper.GetStoredProcCommond("CheckCardNo");
                dbHelper.AddInParameter(cmd, "@Flag", DbType.Int32, flag);
                dbHelper.AddInParameter(cmd, "@EmpCode", DbType.String, empCode);
                dbHelper.AddInParameter(cmd, "@CardNo", DbType.String, cardNo);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                foreach (DataRow row in dt.Rows)
                {
                    if (Convert.ToInt32(row["Status"]) == 0)
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        /// <summary>
        /// 检查卡号是否存在
        /// </summary>
        /// <param name="Type1"></param>
        /// <param name="empId"></param>
        /// <param name="cardNo"></param>
        /// <returns></returns>
        public static bool CheckCardExists(int _Type, int empId, string cardNo)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                DbCommand cmd = dbHelper.GetStoredProcCommond("CheckCardNo");
                dbHelper.AddInParameter(cmd, "@Type", DbType.Int32, _Type);
                dbHelper.AddInParameter(cmd, "@EmpId", DbType.Int32, empId);
                dbHelper.AddInParameter(cmd, "@CardNo", DbType.String, cardNo);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                foreach (DataRow row in dt.Rows)
                {
                    if (Convert.ToInt32(row["Status"]) == 0)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        /// <summary>
        /// 检查卡号是否存在
        /// </summary>
        /// <param name="Type1"></param>
        /// <param name="Flag"></param>
        /// <param name="empCode"></param>
        /// <param name="cardNo"></param>
        /// <returns></returns>
        public static bool CheckCardExists(int _Type, int Flag, string empCode, string cardNo)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                DbCommand cmd = dbHelper.GetStoredProcCommond("CheckCardNo");
                dbHelper.AddInParameter(cmd, "@Type", DbType.Int32, _Type);
                dbHelper.AddInParameter(cmd, "@Flag", DbType.Int32, Flag);
                dbHelper.AddInParameter(cmd, "@EmpCode", DbType.String, empCode);
                dbHelper.AddInParameter(cmd, "@CardNo", DbType.String, cardNo);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                foreach (DataRow row in dt.Rows)
                {
                    if (Convert.ToInt32(row["Status"]) == 0)
                    {
                        return true;
                    }
                }

                return false;
            }
        }


        /*
        /// <summary>
        /// 获取DataCard列表
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static Queue<data.DataCard> GetDataCardQueue(string sql, int deviceId)
        {
            using (DbHelper dbHelper = new DbHelper())
            {
                Queue<DataCard> dCardList = new Queue<DataCard>();
                DbCommand cmd = dbHelper.GetStoredProcCommond(sql);
                cmd.CommandTimeout = 120;
                dbHelper.AddInParameter(cmd, "@DeviceId", DbType.Int32, deviceId);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                foreach (DataRow row in dt.Rows)
                {
                    DataCard dCard = new DataCard();
                    dCard.Total = ArrayHelper.IntToBytes(Convert.ToInt32(row["Total"]), 2);
                    dCard.CardId = ArrayHelper.IntToBytes(Convert.ToInt32(row["CardId"]), 2);
                    dCard.CardNo = ArrayHelper.HexToArray(row["CardNo"].ToString(), 4);
                    dCard.CardStatus = ArrayHelper.IntToBytes(Convert.ToInt32(row["BlackName"]), 1);
                    int rightOfDoor1 = Convert.ToInt32(row["Door1"]);
                    int rightOfDoor2 = Convert.ToInt32(row["Door2"]);
                    dCard.Rights = new byte[] { (byte)((byte)rightOfDoor2 * 2 + (byte)rightOfDoor1) };
                    int timeGroup1 = Convert.ToInt32(row["Group1"]);
                    int timeGroup2 = Convert.ToInt32(row["Group2"]);
                    dCard.TimeGroupOfDoor = new byte[8];
                    dCard.TimeGroupOfDoor[6] = (byte)timeGroup1;
                    dCard.TimeGroupOfDoor[7] = (byte)timeGroup2;
                    dCard.BeginDate = ArrayHelper.DateToArray(Convert.ToDateTime(row["BeginDate"].ToString()));
                    dCard.EndDate = ArrayHelper.DateToArray(Convert.ToDateTime(row["EndDate"].ToString()));
                    for (int i = 0; i < dCard.Other.Length; i++)
                    {
                        dCard.Other[i] = 0x00;
                    }
                    dCardList.Enqueue(dCard);
                }
                return dCardList;

            }
        }
        */
        /// <summary>
        /// 根据设备ID 获取设备有权限的人员
        /// </summary>
        /// <param name="temp"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 撤销人员对控制器门的权限
        /// </summary>
        /// <param name="deviceId"></param>
        /// <param name="doorId"></param>
        /// <param name="empId"></param>
        public static void CancelRight(int deviceId, int doorId, int empId)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string door = doorId == 0 ? "RightOfDoor1" : "RightOfDoor2";
                string sql = "Update MJ_EmpRightOfDevice set  " + door + "=0,UpdateFlag = 0 where deviceId =" + deviceId.ToString() + " and EmpId =" + empId.ToString();
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }

        /// <summary>
        /// 与设备同步之后改变更新状态
        /// </summary>
        /// <param name="devId"></param>
        /// <param name="cardId"></param>
        /// <param name="cardNo"></param>
        public static void ChangeUpdateFlag(int devId, int cardId, string cardNo)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                DbCommand cmd = dbHelper.GetStoredProcCommond("UpdateCarInfoAfterSynchronous");
                dbHelper.AddInParameter(cmd, "@CardId", DbType.Int32, cardId);
                dbHelper.AddInParameter(cmd, "@CardNo", DbType.String, cardNo);
                dbHelper.AddInParameter(cmd, "@DeviceId", DbType.Int32, devId);
                dbHelper.ExecuteNonQuery(cmd);
            }

        }


        #endregion
        #region ---------------------控件--------------------






        /// <summary>
        /// 附加数据库
        /// </summary>
        /// <param name="conString"></param>
        /// <param name="dbName"></param>
        /// <param name="dataPath"></param>
        /// <returns></returns>
        public static bool AttachDataBase(string conString, string dbName, string dataPath, string logPath)
        {

            bool flag = false;
            using (OleDbHelper dbHelper = new OleDbHelper(conString))
            {
                try
                {
                    string sqlAttach = " EXEC  sp_attach_db  @dbname='" + dbName + "',@filename1='" + dataPath + "',@filename2 = '" + logPath + "'";
                    DbCommand cmd = dbHelper.GetSqlStringCommond(sqlAttach);
                    dbHelper.ExecuteNonQuery(cmd);
                    flag = true;
                }
                catch
                {
                    //MessageBox.Show("附加数据库失败:" + ex.Message, "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    flag = false;
                }
            }
            return flag;
        }

        /// <summary>
        /// 得到数据库表列表
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        public static DataTable GetServerTableList(string connection)
        {
            DataTable dt = null;
            string cmdStirng = "select name from sysdatabases where dbid > 4";
            SqlConnection connect = new SqlConnection(connection);
            SqlCommand cmd = new SqlCommand(cmdStirng, connect);
            try
            {
                if (connect.State == ConnectionState.Closed)
                {
                    connect.Open();
                    DataSet ds = new DataSet();
                    SqlDataAdapter sda = new SqlDataAdapter(cmdStirng, connect);
                    sda.Fill(ds, "TableList");
                    dt = ds.Tables["TableList"];
                }
            }
            catch
            {
                //MessageBox.Show("加载服务器上的数据表失败:" + e.Message, "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                if (connect != null && connect.State == ConnectionState.Open)
                {
                    connect.Dispose();
                }
            }
            return dt;
        }
        /// <summary>
        /// 获取局域网内的sql服务器列表
        /// </summary>
        /// <returns></returns>
        public static DataTable GetSqlServerNames()
        {
            DataTable dataSources = SqlClientFactory.Instance.CreateDataSourceEnumerator().GetDataSources();
            DataColumn column = dataSources.Columns["InstanceName"];
            DataColumn column2 = dataSources.Columns["ServerName"];
            DataRowCollection rows = dataSources.Rows;
            DataTable dt = new DataTable();
            dt.Columns.Add("ServerName", System.Type.GetType("System.String"));
            string array = string.Empty;
            for (int i = 0; i < rows.Count; i++)
            {
                string str2 = rows[i][column2] as string;
                string str = rows[i][column] as string;
                if (((str == null) || (str.Length == 0)) || ("MSSQLSERVER" == str))
                {
                    array = str2;
                }
                else
                {
                    array = str2 + @"\" + str;
                }
                dt.Rows.Add(array);
            }
            return dt;
        }




        /*
        /// <summary>
        /// 显示树
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="tv"></param>
        /// <param name="imageList1"></param>
        public static void DisplayTree(DataTable dt, TreeView tv, int father, ImageList imageList1)
        {
            tv.Nodes.Clear();
            tv.ImageList = imageList1;

            TreeHelper.Init_Tree(father, (TreeNode)null, tv, dt);
            tv.ExpandAll();
            dt.Clear();
        }
         * */
        /// <summary>
        /// 获取树的数据表
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static DataTable GetTreeTable(string sql, CommandType type)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                DbCommand cmd = null;
                switch (type)
                {
                    case CommandType.Text:
                        cmd = dbHelper.GetSqlStringCommond(sql);
                        break;
                    case CommandType.StoredProcedure:
                        cmd = dbHelper.GetStoredProcCommond(sql);
                        break;
                }
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                return dt;
            }
        }

        /// <summary>
        /// 更新操作员的菜单权限
        /// </summary>
        /// <param name="operId"></param>
        /// <param name="menuList"></param>
        public static void UpdateMenuRight(int operId, List<int> menuList)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                ///先删除对应的权限
                string sql1 = "Delete from MJ_MenuOfOper where OperId =" + operId.ToString();
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql1);
                dbHelper.ExecuteNonQuery(cmd);

                ///再插入对应权限
                foreach (int menuId in menuList)
                {
                    string sql2 = "Insert into MJ_MenuOfOper(OperId,MenuId) values(" + operId.ToString() + "," + menuId.ToString() + ")";
                    DbCommand cmd2 = dbHelper.GetSqlStringCommond(sql2);
                    dbHelper.ExecuteNonQuery(cmd2);
                }
            }
        }
        #endregion

        #region  ----------------时间段----------




        /// <summary>
        /// 获取节假日时间组列表
        /// </summary>
        /// <returns></returns>
        public static DataTable GetTimegroupOfVacation()
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "Select Gid,Gname,Gmark from MJ_DateGroupOfVacation where Status = 1";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                return dt;
            }
        }

        /// <summary>
        /// 获取节假日时间组列表
        /// </summary>
        /// <returns></returns>
        public static DataTable GetTimeGroupOfVacationList()
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                var sql = "select Gid,Gname,Gmark from DateGroupOfVacation where status = 1";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                return dt;
            }
        }
        /*
        /// <summary>
        /// 获取节假日时间组列表
        /// </summary>
        /// <returns></returns>
        public static DataTimeGroupOfVacation GetTimeGroupListOfVacation()
        {
            try
            {

                using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
                {
                    List<TimeGroup> tgList = new List<TimeGroup>();
                    DataTimeGroupOfVacation dTimegroupOfVacation = new DataTimeGroupOfVacation();
                    string sql = "Select a.Gid,a.Tid as TID,a.TbeginTime as TBeginTime,a.TEndTime as TEndTime from TimeGroupOfVacation a,DateGroupOfVacation b where a.Gid = b.Gid and b.status = 1 ";
                    DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                    DataTable dt = dbHelper.ExecuteDataTable(cmd);
                    foreach (DataRow row in dt.Rows)
                    {
                        int index = Convert.ToInt32(row["Gid"]);
                        int tId = (Convert.ToInt32(row["TID"]) - 1) % 5;
                        string beginTime = row["TBeginTime"].ToString();
                        string endTime = row["TEndtime"].ToString();
                        TimeGroup timeGroup = new TimeGroup(beginTime, endTime, true);
                        dTimegroupOfVacation.TimeGroupList[index][tId] = timeGroup;
                    }
                    return dTimegroupOfVacation;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        */
        /*
        /// <summary>
        /// 新增门禁时间段
        /// </summary>
        /// <param name="gName"></param>
        /// <param name="gMark"></param>
        /// <param name="timeGroup1"></param>
        /// <param name="timeGroup2"></param>
        /// <param name="timeGroup3"></param>
        /// <param name="timeGroup4"></param>
        /// <param name="timeGroup5"></param>
        public static void InsertTimeGroupOfVacation(string gName, string gMark, TimeGroup timeGroup1, TimeGroup timeGroup2, TimeGroup timeGroup3, TimeGroup timeGroup4, TimeGroup timeGroup5)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                DbCommand cmd = dbHelper.GetStoredProcCommond("InsertDateGroupOfVacation");
                dbHelper.AddInParameter(cmd, "@gName", DbType.String, gName);
                dbHelper.AddInParameter(cmd, "@gMark", DbType.String, gMark);
                dbHelper.AddInParameter(cmd, "@TimeBegin1", DbType.String, timeGroup1.SBeginTime);
                dbHelper.AddInParameter(cmd, "@TimeEnd1", DbType.String, timeGroup1.SEndTime);
                dbHelper.AddInParameter(cmd, "@TimeBegin2", DbType.String, timeGroup2.SBeginTime);
                dbHelper.AddInParameter(cmd, "@TimeEnd2", DbType.String, timeGroup2.SEndTime);
                dbHelper.AddInParameter(cmd, "@TimeBegin3", DbType.String, timeGroup3.SBeginTime);
                dbHelper.AddInParameter(cmd, "@TimeEnd3", DbType.String, timeGroup3.SEndTime);
                dbHelper.AddInParameter(cmd, "@TimeBegin4", DbType.String, timeGroup4.SBeginTime);
                dbHelper.AddInParameter(cmd, "@TimeEnd4", DbType.String, timeGroup4.SEndTime);
                dbHelper.AddInParameter(cmd, "@TimeBegin5", DbType.String, timeGroup5.SBeginTime);
                dbHelper.AddInParameter(cmd, "@TimeEnd5", DbType.String, timeGroup5.SEndTime);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }

        /// <summary>
        /// 更新节假日时间组时间段
        /// </summary>
        /// <param name="Gid"></param>
        /// <param name="gName"></param>
        /// <param name="gMark"></param>
        /// <param name="timeGroup1"></param>
        /// <param name="timeGroup2"></param>
        /// <param name="timeGroup3"></param>
        /// <param name="timeGroup4"></param>
        /// <param name="timeGroup5"></param>
        public static void UpdateTimeGroupOfVacation(int Gid, string gName, string gMark, TimeGroup timeGroup1, TimeGroup timeGroup2, TimeGroup timeGroup3, TimeGroup timeGroup4, TimeGroup timeGroup5)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                DbCommand cmd = dbHelper.GetStoredProcCommond("UpdateDateGroupOfVacation");
                dbHelper.AddInParameter(cmd, "@Gid", DbType.Int32, Gid);
                dbHelper.AddInParameter(cmd, "@gName", DbType.String, gName);
                dbHelper.AddInParameter(cmd, "@gMark", DbType.String, gMark);
                dbHelper.AddInParameter(cmd, "@TimeBegin1", DbType.String, timeGroup1.SBeginTime);
                dbHelper.AddInParameter(cmd, "@TimeEnd1", DbType.String, timeGroup1.SEndTime);
                dbHelper.AddInParameter(cmd, "@TimeBegin2", DbType.String, timeGroup2.SBeginTime);
                dbHelper.AddInParameter(cmd, "@TimeEnd2", DbType.String, timeGroup2.SEndTime);
                dbHelper.AddInParameter(cmd, "@TimeBegin3", DbType.String, timeGroup3.SBeginTime);
                dbHelper.AddInParameter(cmd, "@TimeEnd3", DbType.String, timeGroup3.SEndTime);
                dbHelper.AddInParameter(cmd, "@TimeBegin4", DbType.String, timeGroup4.SBeginTime);
                dbHelper.AddInParameter(cmd, "@TimeEnd4", DbType.String, timeGroup4.SEndTime);
                dbHelper.AddInParameter(cmd, "@TimeBegin5", DbType.String, timeGroup5.SBeginTime);
                dbHelper.AddInParameter(cmd, "@TimeEnd5", DbType.String, timeGroup5.SEndTime);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }
        /// <summary>
        /// 删除节假日时间组
        /// </summary>
        /// <param name="gId"></param>
        public static void DeleteTimeGroupOfVacation(string gId)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                DbCommand cmd = dbHelper.GetStoredProcCommond("DeleteTimeGroupOfVacation");
                dbHelper.AddInParameter(cmd, "@Gid", DbType.Int32, Convert.ToInt32(gId));
                dbHelper.ExecuteNonQuery(cmd);
            }
        }

        /// <summary>
        /// 获取节假日列表
        /// </summary>
        /// <returns></returns>
        public static DataTable GetVacationList()
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "select VID, VName,VbeginDate,VEndDate,Vdesc from vacation where status = 1";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                return dt;
            }
        }
        */
        /// <summary>
        /// 增加节假日
        /// </summary>
        /// <param name="name"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="desc"></param>
        public static void AddVacation(string name, string beginDate, string endDate, string desc)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                DbCommand cmd = dbHelper.GetStoredProcCommond("InsertVacation");
                dbHelper.AddInParameter(cmd, "@VName", DbType.String, name);
                dbHelper.AddInParameter(cmd, "@VBeginDate", DbType.String, beginDate);
                dbHelper.AddInParameter(cmd, "@VEndDate", DbType.String, endDate);
                dbHelper.AddInParameter(cmd, "@VDesc", DbType.String, desc);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }

        /// <summary>
        /// 修改节假日
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="desc"></param>
        public static void UpdateVacation(string id, string name, string beginDate, string endDate, string desc)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "update  vacation set vname ='" + name + "',VBeginDate ='" + beginDate + "',VEndDate ='" + endDate + "',Vdesc ='" + desc + "'  where vid =" + id;
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }

        /// <summary>
        /// 删除节假日
        /// </summary>
        /// <param name="id"></param>
        public static void DeleteVacation(int id)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = " Update vacation set status = 0 where vid =" + id;
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }

        /*
        /// <summary>
        /// 获取全部节假日用于上传控制器
        /// </summary>
        /// <returns></returns>
        public static DataVacation GetDataVacation()
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                DataVacation dVacation = new DataVacation();
                try
                {
                    string sql = "select * from Vacation  where Status = 1";
                    DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                    DataTable dt = dbHelper.ExecuteDataTable(cmd);
                    foreach (DataRow row in dt.Rows)
                    {
                        int index = Convert.ToInt32(row["Vid"]);
                        string beginDate = row["VBeginDate"].ToString().Substring(0, 10);
                        string endDate = row["VEndDate"].ToString().Substring(0, 10);
                        DateGroup dateGroup = new DateGroup(beginDate, endDate);
                        dVacation.VacationList[index] = dateGroup;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return dVacation;
            }
        }

        /// <summary>
        /// 获取门禁星期时间组
        /// </summary>
        /// <returns></returns>
        public static DataTimeGroupOfDoor GetDataTimeGroupOfDoor(int groupId)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                DataTimeGroupOfDoor dDoor = new DataTimeGroupOfDoor();
                dDoor.GroupNo = ArrayHelper.IntToBytes(groupId, 1);
                string sql = "Select * from TimeOfGroup where GroupId = " + groupId.ToString();
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                foreach (DataRow row in dt.Rows)
                {
                    int week = Convert.ToInt32(row["weekNo"]) - 1;
                    string beginTime = row["BeginTime"].ToString();
                    string endTime = row["EndTime"].ToString();
                    TimeGroup dtGroup = new TimeGroup(beginTime, endTime, true);
                    dDoor.TimeGroupList[week].Add(dtGroup);
                }
                return dDoor;
            }
        }


        #region 获取星期时间组组列表
        public static List<DataTimeGroupOfDoor> GetListOfTimeGroupOfDoor()
        {
            List<DataTimeGroupOfDoor> dtgdList = new List<DataTimeGroupOfDoor>();
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "Select GroupId from TimeOfGroup  Group By GroupId";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                foreach (DataRow row in dt.Rows)
                {
                    int groupId = Convert.ToInt32(row["GroupId"]);
                    DataTimeGroupOfDoor dtgd = GetDataTimeGroupOfDoor(groupId);
                    dtgdList.Add(dtgd);
                }
            }
            return dtgdList;
        }
        #endregion
                */
        /// <summary>
        /// 获取星期时间组列表
        /// </summary>
        /// <returns></returns>
        public static DataTable GettimeGroupListOfDoor()
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "select Id,Name,DDEsc from TimeGroupOfDoor where status = 1";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                return dt;
            }
        }
        /// <summary>
        /// 增加门禁时间组
        /// </summary>
        /// <param name="GroupName"></param>
        /// <param name="GDesc"></param>
        /// <param name="time0begin0"></param>
        /// <param name="time0end0"></param>
        /// <param name="time0begin1"></param>
        /// <param name="time0end1"></param>
        /// <param name="time0begin2"></param>
        /// <param name="time0end2"></param>
        /// <param name="time0begin3"></param>
        /// <param name="time0end3"></param>
        /// <param name="time1begin0"></param>
        /// <param name="time1end0"></param>
        /// <param name="time1begin1"></param>
        /// <param name="time1end1"></param>
        /// <param name="time1begin2"></param>
        /// <param name="time1end2"></param>
        /// <param name="time1begin3"></param>
        /// <param name="time1end3"></param>
        /// <param name="time2begin0"></param>
        /// <param name="time2end0"></param>
        /// <param name="time2begin1"></param>
        /// <param name="time2end1"></param>
        /// <param name="time2begin2"></param>
        /// <param name="time2end2"></param>
        /// <param name="time2begin3"></param>
        /// <param name="time2end3"></param>
        /// <param name="time3begin0"></param>
        /// <param name="time3end0"></param>
        /// <param name="time3begin1"></param>
        /// <param name="time3end1"></param>
        /// <param name="time3begin2"></param>
        /// <param name="time3end2"></param>
        /// <param name="time3begin3"></param>
        /// <param name="time3end3"></param>
        /// <param name="time4begin0"></param>
        /// <param name="time4end0"></param>
        /// <param name="time4begin1"></param>
        /// <param name="time4end1"></param>
        /// <param name="time4begin2"></param>
        /// <param name="time4end2"></param>
        /// <param name="time4begin3"></param>
        /// <param name="time4end3"></param>
        /// <param name="time5begin0"></param>
        /// <param name="time5end0"></param>
        /// <param name="time5begin1"></param>
        /// <param name="time5end1"></param>
        /// <param name="time5begin2"></param>
        /// <param name="time5end2"></param>
        /// <param name="time5begin3"></param>
        /// <param name="time5end3"></param>
        /// <param name="time6begin0"></param>
        /// <param name="time6end0"></param>
        /// <param name="time6begin1"></param>
        /// <param name="time6end1"></param>
        /// <param name="time6begin2"></param>
        /// <param name="time6end2"></param>
        /// <param name="time6begin3"></param>
        /// <param name="time6end3"></param>
        public static void AddTimeGroupOfDoor(string GroupName, string GDesc,
            string time0begin0, string time0end0, string time0begin1, string time0end1, string time0begin2, string time0end2, string time0begin3, string time0end3,
            string time1begin0, string time1end0, string time1begin1, string time1end1, string time1begin2, string time1end2, string time1begin3, string time1end3,
            string time2begin0, string time2end0, string time2begin1, string time2end1, string time2begin2, string time2end2, string time2begin3, string time2end3,
            string time3begin0, string time3end0, string time3begin1, string time3end1, string time3begin2, string time3end2, string time3begin3, string time3end3,
            string time4begin0, string time4end0, string time4begin1, string time4end1, string time4begin2, string time4end2, string time4begin3, string time4end3,
            string time5begin0, string time5end0, string time5begin1, string time5end1, string time5begin2, string time5end2, string time5begin3, string time5end3,
            string time6begin0, string time6end0, string time6begin1, string time6end1, string time6begin2, string time6end2, string time6begin3, string time6end3)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                DbCommand cmd = dbHelper.GetStoredProcCommond("InsertTimeOfGroup");
                dbHelper.AddInParameter(cmd, "@GroupName", DbType.String, GroupName);
                dbHelper.AddInParameter(cmd, "@groupDesc", DbType.String, GDesc);

                dbHelper.AddInParameter(cmd, "@time0begin0", DbType.String, time0begin0);
                dbHelper.AddInParameter(cmd, "@time0end0", DbType.String, time0end0);
                dbHelper.AddInParameter(cmd, "@time0begin1", DbType.String, time0begin1);
                dbHelper.AddInParameter(cmd, "@time0end1", DbType.String, time0end1);
                dbHelper.AddInParameter(cmd, "@time0begin2", DbType.String, time0begin2);
                dbHelper.AddInParameter(cmd, "@time0end2", DbType.String, time0end2);
                dbHelper.AddInParameter(cmd, "@time0begin3", DbType.String, time0begin3);
                dbHelper.AddInParameter(cmd, "@time0end3", DbType.String, time0end3);

                dbHelper.AddInParameter(cmd, "@time1begin0", DbType.String, time1begin0);
                dbHelper.AddInParameter(cmd, "@time1end0", DbType.String, time1end0);
                dbHelper.AddInParameter(cmd, "@time1begin1", DbType.String, time1begin1);
                dbHelper.AddInParameter(cmd, "@time1end1", DbType.String, time1end1);
                dbHelper.AddInParameter(cmd, "@time1begin2", DbType.String, time1begin2);
                dbHelper.AddInParameter(cmd, "@time1end2", DbType.String, time1end2);
                dbHelper.AddInParameter(cmd, "@time1begin3", DbType.String, time1begin3);
                dbHelper.AddInParameter(cmd, "@time1end3", DbType.String, time1end3);

                dbHelper.AddInParameter(cmd, "@time2begin0", DbType.String, time2begin0);
                dbHelper.AddInParameter(cmd, "@time2end0", DbType.String, time2end0);
                dbHelper.AddInParameter(cmd, "@time2begin1", DbType.String, time2begin1);
                dbHelper.AddInParameter(cmd, "@time2end1", DbType.String, time2end1);
                dbHelper.AddInParameter(cmd, "@time2begin2", DbType.String, time2begin2);
                dbHelper.AddInParameter(cmd, "@time2end2", DbType.String, time2end2);
                dbHelper.AddInParameter(cmd, "@time2begin3", DbType.String, time2begin3);
                dbHelper.AddInParameter(cmd, "@time2end3", DbType.String, time2end3);

                dbHelper.AddInParameter(cmd, "@time3begin0", DbType.String, time3begin0);
                dbHelper.AddInParameter(cmd, "@time3end0", DbType.String, time3end0);
                dbHelper.AddInParameter(cmd, "@time3begin1", DbType.String, time3begin1);
                dbHelper.AddInParameter(cmd, "@time3end1", DbType.String, time3end1);
                dbHelper.AddInParameter(cmd, "@time3begin2", DbType.String, time3begin2);
                dbHelper.AddInParameter(cmd, "@time3end2", DbType.String, time3end2);
                dbHelper.AddInParameter(cmd, "@time3begin3", DbType.String, time3begin3);
                dbHelper.AddInParameter(cmd, "@time3end3", DbType.String, time3end3);

                dbHelper.AddInParameter(cmd, "@time4begin0", DbType.String, time4begin0);
                dbHelper.AddInParameter(cmd, "@time4end0", DbType.String, time4end0);
                dbHelper.AddInParameter(cmd, "@time4begin1", DbType.String, time4begin1);
                dbHelper.AddInParameter(cmd, "@time4end1", DbType.String, time4end1);
                dbHelper.AddInParameter(cmd, "@time4begin2", DbType.String, time4begin2);
                dbHelper.AddInParameter(cmd, "@time4end2", DbType.String, time4end2);
                dbHelper.AddInParameter(cmd, "@time4begin3", DbType.String, time4begin3);
                dbHelper.AddInParameter(cmd, "@time4end3", DbType.String, time4end3);

                dbHelper.AddInParameter(cmd, "@time5begin0", DbType.String, time5begin0);
                dbHelper.AddInParameter(cmd, "@time5end0", DbType.String, time5end0);
                dbHelper.AddInParameter(cmd, "@time5begin1", DbType.String, time5begin1);
                dbHelper.AddInParameter(cmd, "@time5end1", DbType.String, time5end1);
                dbHelper.AddInParameter(cmd, "@time5begin2", DbType.String, time5begin2);
                dbHelper.AddInParameter(cmd, "@time5end2", DbType.String, time5end2);
                dbHelper.AddInParameter(cmd, "@time5begin3", DbType.String, time5begin3);
                dbHelper.AddInParameter(cmd, "@time5end3", DbType.String, time5end3);

                dbHelper.AddInParameter(cmd, "@time6begin0", DbType.String, time6begin0);
                dbHelper.AddInParameter(cmd, "@time6end0", DbType.String, time6end0);
                dbHelper.AddInParameter(cmd, "@time6begin1", DbType.String, time6begin1);
                dbHelper.AddInParameter(cmd, "@time6end1", DbType.String, time6end1);
                dbHelper.AddInParameter(cmd, "@time6begin2", DbType.String, time6begin2);
                dbHelper.AddInParameter(cmd, "@time6end2", DbType.String, time6end2);
                dbHelper.AddInParameter(cmd, "@time6begin3", DbType.String, time6begin3);
                dbHelper.AddInParameter(cmd, "@time6end3", DbType.String, time6end3);

                dbHelper.ExecuteNonQuery(cmd);
            }
        }

        /// <summary>
        /// 修改星期时间组
        /// </summary>
        /// <param name="GroupId"></param>
        /// <param name="time0begin0"></param>
        /// <param name="time0end0"></param>
        /// <param name="time0begin1"></param>
        /// <param name="time0end1"></param>
        /// <param name="time0begin2"></param>
        /// <param name="time0end2"></param>
        /// <param name="time0begin3"></param>
        /// <param name="time0end3"></param>
        /// <param name="time1begin0"></param>
        /// <param name="time1end0"></param>
        /// <param name="time1begin1"></param>
        /// <param name="time1end1"></param>
        /// <param name="time1begin2"></param>
        /// <param name="time1end2"></param>
        /// <param name="time1begin3"></param>
        /// <param name="time1end3"></param>
        /// <param name="time2begin0"></param>
        /// <param name="time2end0"></param>
        /// <param name="time2begin1"></param>
        /// <param name="time2end1"></param>
        /// <param name="time2begin2"></param>
        /// <param name="time2end2"></param>
        /// <param name="time2begin3"></param>
        /// <param name="time2end3"></param>
        /// <param name="time3begin0"></param>
        /// <param name="time3end0"></param>
        /// <param name="time3begin1"></param>
        /// <param name="time3end1"></param>
        /// <param name="time3begin2"></param>
        /// <param name="time3end2"></param>
        /// <param name="time3begin3"></param>
        /// <param name="time3end3"></param>
        /// <param name="time4begin0"></param>
        /// <param name="time4end0"></param>
        /// <param name="time4begin1"></param>
        /// <param name="time4end1"></param>
        /// <param name="time4begin2"></param>
        /// <param name="time4end2"></param>
        /// <param name="time4begin3"></param>
        /// <param name="time4end3"></param>
        /// <param name="time5begin0"></param>
        /// <param name="time5end0"></param>
        /// <param name="time5begin1"></param>
        /// <param name="time5end1"></param>
        /// <param name="time5begin2"></param>
        /// <param name="time5end2"></param>
        /// <param name="time5begin3"></param>
        /// <param name="time5end3"></param>
        /// <param name="time6begin0"></param>
        /// <param name="time6end0"></param>
        /// <param name="time6begin1"></param>
        /// <param name="time6end1"></param>
        /// <param name="time6begin2"></param>
        /// <param name="time6end2"></param>
        /// <param name="time6begin3"></param>
        /// <param name="time6end3"></param>
        public static void UpdateTimeGroupOfDoor(int GroupId,
            string time0begin0, string time0end0, string time0begin1, string time0end1, string time0begin2, string time0end2, string time0begin3, string time0end3,
            string time1begin0, string time1end0, string time1begin1, string time1end1, string time1begin2, string time1end2, string time1begin3, string time1end3,
            string time2begin0, string time2end0, string time2begin1, string time2end1, string time2begin2, string time2end2, string time2begin3, string time2end3,
            string time3begin0, string time3end0, string time3begin1, string time3end1, string time3begin2, string time3end2, string time3begin3, string time3end3,
            string time4begin0, string time4end0, string time4begin1, string time4end1, string time4begin2, string time4end2, string time4begin3, string time4end3,
            string time5begin0, string time5end0, string time5begin1, string time5end1, string time5begin2, string time5end2, string time5begin3, string time5end3,
            string time6begin0, string time6end0, string time6begin1, string time6end1, string time6begin2, string time6end2, string time6begin3, string time6end3)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                DbCommand cmd = dbHelper.GetStoredProcCommond("UpdateTimeOfGroup");
                dbHelper.AddInParameter(cmd, "@ID", DbType.Int32, GroupId);

                dbHelper.AddInParameter(cmd, "@time0begin0", DbType.String, time0begin0);
                dbHelper.AddInParameter(cmd, "@time0end0", DbType.String, time0end0);
                dbHelper.AddInParameter(cmd, "@time0begin1", DbType.String, time0begin1);
                dbHelper.AddInParameter(cmd, "@time0end1", DbType.String, time0end1);
                dbHelper.AddInParameter(cmd, "@time0begin2", DbType.String, time0begin2);
                dbHelper.AddInParameter(cmd, "@time0end2", DbType.String, time0end2);
                dbHelper.AddInParameter(cmd, "@time0begin3", DbType.String, time0begin3);
                dbHelper.AddInParameter(cmd, "@time0end3", DbType.String, time0end3);

                dbHelper.AddInParameter(cmd, "@time1begin0", DbType.String, time1begin0);
                dbHelper.AddInParameter(cmd, "@time1end0", DbType.String, time1end0);
                dbHelper.AddInParameter(cmd, "@time1begin1", DbType.String, time1begin1);
                dbHelper.AddInParameter(cmd, "@time1end1", DbType.String, time1end1);
                dbHelper.AddInParameter(cmd, "@time1begin2", DbType.String, time1begin2);
                dbHelper.AddInParameter(cmd, "@time1end2", DbType.String, time1end2);
                dbHelper.AddInParameter(cmd, "@time1begin3", DbType.String, time1begin3);
                dbHelper.AddInParameter(cmd, "@time1end3", DbType.String, time1end3);

                dbHelper.AddInParameter(cmd, "@time2begin0", DbType.String, time2begin0);
                dbHelper.AddInParameter(cmd, "@time2end0", DbType.String, time2end0);
                dbHelper.AddInParameter(cmd, "@time2begin1", DbType.String, time2begin1);
                dbHelper.AddInParameter(cmd, "@time2end1", DbType.String, time2end1);
                dbHelper.AddInParameter(cmd, "@time2begin2", DbType.String, time2begin2);
                dbHelper.AddInParameter(cmd, "@time2end2", DbType.String, time2end2);
                dbHelper.AddInParameter(cmd, "@time2begin3", DbType.String, time2begin3);
                dbHelper.AddInParameter(cmd, "@time2end3", DbType.String, time2end3);

                dbHelper.AddInParameter(cmd, "@time3begin0", DbType.String, time3begin0);
                dbHelper.AddInParameter(cmd, "@time3end0", DbType.String, time3end0);
                dbHelper.AddInParameter(cmd, "@time3begin1", DbType.String, time3begin1);
                dbHelper.AddInParameter(cmd, "@time3end1", DbType.String, time3end1);
                dbHelper.AddInParameter(cmd, "@time3begin2", DbType.String, time3begin2);
                dbHelper.AddInParameter(cmd, "@time3end2", DbType.String, time3end2);
                dbHelper.AddInParameter(cmd, "@time3begin3", DbType.String, time3begin3);
                dbHelper.AddInParameter(cmd, "@time3end3", DbType.String, time3end3);

                dbHelper.AddInParameter(cmd, "@time4begin0", DbType.String, time4begin0);
                dbHelper.AddInParameter(cmd, "@time4end0", DbType.String, time4end0);
                dbHelper.AddInParameter(cmd, "@time4begin1", DbType.String, time4begin1);
                dbHelper.AddInParameter(cmd, "@time4end1", DbType.String, time4end1);
                dbHelper.AddInParameter(cmd, "@time4begin2", DbType.String, time4begin2);
                dbHelper.AddInParameter(cmd, "@time4end2", DbType.String, time4end2);
                dbHelper.AddInParameter(cmd, "@time4begin3", DbType.String, time4begin3);
                dbHelper.AddInParameter(cmd, "@time4end3", DbType.String, time4end3);

                dbHelper.AddInParameter(cmd, "@time5begin0", DbType.String, time5begin0);
                dbHelper.AddInParameter(cmd, "@time5end0", DbType.String, time5end0);
                dbHelper.AddInParameter(cmd, "@time5begin1", DbType.String, time5begin1);
                dbHelper.AddInParameter(cmd, "@time5end1", DbType.String, time5end1);
                dbHelper.AddInParameter(cmd, "@time5begin2", DbType.String, time5begin2);
                dbHelper.AddInParameter(cmd, "@time5end2", DbType.String, time5end2);
                dbHelper.AddInParameter(cmd, "@time5begin3", DbType.String, time5begin3);
                dbHelper.AddInParameter(cmd, "@time5end3", DbType.String, time5end3);

                dbHelper.AddInParameter(cmd, "@time6begin0", DbType.String, time6begin0);
                dbHelper.AddInParameter(cmd, "@time6end0", DbType.String, time6end0);
                dbHelper.AddInParameter(cmd, "@time6begin1", DbType.String, time6begin1);
                dbHelper.AddInParameter(cmd, "@time6end1", DbType.String, time6end1);
                dbHelper.AddInParameter(cmd, "@time6begin2", DbType.String, time6begin2);
                dbHelper.AddInParameter(cmd, "@time6end2", DbType.String, time6end2);
                dbHelper.AddInParameter(cmd, "@time6begin3", DbType.String, time6begin3);
                dbHelper.AddInParameter(cmd, "@time6end3", DbType.String, time6end3);

                dbHelper.ExecuteNonQuery(cmd);
            }
        }


        /// <summary>
        /// 获取门禁时间组列表
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public static List<List<TimeGroup>> GetTimeGroupList(int groupId)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                List<List<TimeGroup>> timeGroupList = new List<List<TimeGroup>>();
                for (int i = 0; i < 7; i++)
                {
                    List<TimeGroup> list = new List<TimeGroup>();
                    timeGroupList.Add(list);
                }
                string sql = "select * from timeofgroup where groupid =" + groupId;
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                foreach (DataRow row in dt.Rows)
                {
                    int week = Convert.ToInt32(row["weekNo"]);
                    string beginTime = row["BeginTime"].ToString();
                    string endTime = row["EndTime"].ToString();
                    TimeGroup timeGroup = new TimeGroup(beginTime, endTime);
                    timeGroupList[week - 1].Add(timeGroup);
                }
                for (int k = 0; k < timeGroupList.Count; k++)
                {
                    List<TimeGroup> tList = timeGroupList[k];
                    if (tList.Count == 0)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            TimeGroup tg = new TimeGroup("00:00", "00:00");
                            tList.Add(tg);
                        }
                    }
                }
                return timeGroupList;
            }
        }

        /// <summary>
        /// 删除星期时间段
        /// </summary>
        /// <param name="groupId"></param>
        public static void DeleteTimeGroupOfDoor(int groupId)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "DeleteTimeGroupOfDoor";
                DbCommand cmd = dbHelper.GetStoredProcCommond(sql);
                dbHelper.AddInParameter(cmd, "@GroupId", DbType.Int32, groupId);
                dbHelper.ExecuteNonQuery(cmd);
            }

        }
        #endregion

        #region -----------------Record(记录)--------------------


        /// <summary>
        /// 获取记录表
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static DataTable GetRecords(string sql)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                DbCommand cmd = dbHelper.GetStoredProcCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                return dt;
            }
        }


        #endregion

        #region ------------------系统设置--------------

        /// <summary>
        /// 检查密码是否正确
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static bool CheckPassword(string operName, string password)
        {
            bool flag = false;
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "Select * from OperInfo where Op_Name ='" + operName + "'";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                foreach (DataRow row in dt.Rows)
                {
                    if (password.Equals(row["OP_Pass"].ToString()))
                    {
                        flag = true;
                    }
                }

            }
            return flag;
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="operId"></param>
        public static void DeleteOper(int operId)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "DeleteOper";
                DbCommand cmd = dbHelper.GetStoredProcCommond(sql);
                dbHelper.AddInParameter(cmd, "@OperId", DbType.Int32, operId);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }

        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="operName"></param>
        /// <param name="newPassword"></param>
        public static void ModifyPassword(string operName, string newPassword)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "Update OperInfo set OP_Pass = '" + newPassword + "' where Op_Name = '" + operName + "'";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="operId"></param>
        /// <returns></returns>
        public static OperInfo GetOperInfoByOperId(int operId)
        {
            OperInfo oper = null;
            try
            {
                using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
                {
                    string sql = "Select * from OperInfo where OperId =" + operId.ToString();
                    DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                    DataTable dt = dbHelper.ExecuteDataTable(cmd);
                    foreach (DataRow row in dt.Rows)
                    {
                        oper = new OperInfo();
                        oper.OperId = operId;
                        oper.OperName = row["Op_Name"].ToString();
                        oper.OperPass = row["OperPassword"].ToString();
                        oper.Descr = row["Descr"].ToString();
                    }
                }
            }
            catch
            {
            }
            return oper;
        }

        /// <summary>
        /// 插入操作员
        /// </summary>
        /// <param name="operName"></param>
        /// <param name="operPass"></param>
        /// <param name="descr"></param>
        public static void InsertOperInfo(string operName, string operPass, string descr)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "Insert into OperInfo(Op_Name,OperPassword,Descr) values ('" + operName + "','" + operPass + "','" + descr + "')";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }

        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="operId"></param>
        /// <param name="operName"></param>
        /// <param name="operPass"></param>
        /// <param name="descr"></param>
        public static void UpdateOperInfo(int operId, string operName, string operPass, string descr)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "Update OperInfo Set OP_Name = '" + operName + "', OP_Pass = '" + operPass + "',Descr = '" + descr + "'  where  Op_ID = " + operId.ToString();
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }
        /// <summary>
        /// 检查操作员是否存在
        /// </summary>
        /// <param name="operName"></param>
        /// <returns></returns>
        public static bool CheckOperName(string operName)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "Select * from OperInfo where Op_Name ='" + operName + "'";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                int operId = 0;
                foreach (DataRow row in dt.Rows)
                {
                    operId = Convert.ToInt32(row["OperId"]);
                }
                if (operId == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }


        /// <summary>
        /// 根据操作员生成菜单列表
        /// </summary>
        /// <param name="OperName"></param>
        /// <returns></returns>
        public static List<Menus> GetMenuList(string OperName)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                List<Menus> menuList = new List<Menus>();
                string sql = "MJ_GetMenuListByOperName";
                DbCommand cmd = dbHelper.GetStoredProcCommond(sql);
                dbHelper.AddInParameter(cmd, "@OperName", DbType.String, OperName);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                foreach (DataRow row in dt.Rows)
                {
                    Menus menu = new Menus();
                    menu.MenuId = Convert.ToInt32(row["MenuId"]);
                    menu.MenuName = row["MenuName"].ToString();
                    menu.MenuText = row["MenuText"].ToString();
                    menuList.Add(menu);
                }
                return menuList;
            }
        }
        /// <summary>
        /// 获取菜单权限树形菜单
        /// </summary>
        /// <returns></returns>
        public static DataTable GetMenuTree()
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "select MenuId as id,MenuText as name,ParMenuId as parid,4 as ImageIndex  from Menus Where EnableFlag = 1 ";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                dt.TableName = "TableName";
                return dt;
            }
        }

        #endregion


        #region ----------------------报表-----------------

        /// <summary>
        /// 员工报表
        /// </summary>
        /// <param name="deptName"></param>
        /// <param name="EmpCode"></param>
        /// <param name="EmpName"></param>
        /// <returns></returns>
        public static DataTable GetEmpReport(int deptId, int deptType, string EmpCode, string EmpName, string cardNo)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "ReportOfEmpAndCard";
                DbCommand cmd = dbHelper.GetStoredProcCommond(sql);
                dbHelper.AddInParameter(cmd, "@DeptId", DbType.Int32, deptId);
                dbHelper.AddInParameter(cmd, "@DeptType", DbType.Int32, deptType);
                dbHelper.AddInParameter(cmd, "@EmpCode", DbType.String, EmpCode);
                dbHelper.AddInParameter(cmd, "@EmpName", DbType.String, EmpName);
                dbHelper.AddInParameter(cmd, "@CardNo", DbType.String, cardNo);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                return dt;
            }
        }
        /// <summary>
        /// 正常刷卡记录报表
        /// </summary>
        /// <returns></returns>
        public static DataTable GetNormalRecords()
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "GetNormalRecords";
                DbCommand cmd = dbHelper.GetStoredProcCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                return dt;
            }
        }
        /// <summary>
        /// 异常刷卡记录报表
        /// </summary>
        /// <returns></returns>
        public static DataTable GetExceptionRecords()
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "GetExceptionRecords";
                DbCommand cmd = dbHelper.GetStoredProcCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                return dt;
            }
        }
        /// <summary>
        /// 权限报表
        /// </summary>
        /// <param name="deviceId"></param>
        /// <param name="doorId"></param>
        /// <param name="Empcode"></param>
        /// <param name="EmpName"></param>
        /// <param name="CardNo"></param>
        /// <returns></returns>
        public static DataTable GetRightsReport(int deptId, int deptType, int deviceId, string Empcode, string EmpName, string CardNo)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "ReportOfRights";
                DbCommand cmd = dbHelper.GetStoredProcCommond(sql);
                dbHelper.AddInParameter(cmd, "@DeptId", DbType.Int32, deptId);
                dbHelper.AddInParameter(cmd, "@DeptType", DbType.Int32, deptType);
                dbHelper.AddInParameter(cmd, "@DeviceId", DbType.Int32, deviceId);
                dbHelper.AddInParameter(cmd, "@EmpCode", DbType.String, Empcode);
                dbHelper.AddInParameter(cmd, "@EmpName", DbType.String, EmpName);
                dbHelper.AddInParameter(cmd, "@CardNo", DbType.String, CardNo);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                return dt;
            }
        }

        /// <summary>
        /// 记录报表
        /// </summary>
        /// <param name="ReportType"></param>
        /// <param name="DeviceId"></param>
        /// <param name="CardNo"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        public static DataTable GetRecordReport(int deptId, int deptType, int deviceId, string empCode, string empName, string CardNo, string BeginDate, string EndDate)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "ReportOfRecord";
                DbCommand cmd = dbHelper.GetStoredProcCommond(sql);
                dbHelper.AddInParameter(cmd, "@DeptId", DbType.Int32, deptId);
                dbHelper.AddInParameter(cmd, "@DeptType", DbType.Int32, deptType);
                dbHelper.AddInParameter(cmd, "@DeviceId", DbType.Int32, deviceId);
                dbHelper.AddInParameter(cmd, "@EmpCode", DbType.String, empCode);
                dbHelper.AddInParameter(cmd, "@EmpName", DbType.String, empName);
                dbHelper.AddInParameter(cmd, "@CardNo", DbType.String, CardNo);
                dbHelper.AddInParameter(cmd, "@BeginDate", DbType.String, BeginDate);
                dbHelper.AddInParameter(cmd, "@EndDate", DbType.String, EndDate);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                return dt;
            }
        }
        #endregion
        #region -------------操作日志--------

        /// <summary>
        /// 根据sql 语句 返回DataTable
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static DataTable GetDataTableFromSql(string sql)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                return dt;
            }
        }

        /// <summary>
        /// 查找操作日志
        /// </summary>
        /// <param name="operName"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static DataTable FindOperLog(string operName, string beginDate, string endDate)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "FindOperLog";
                DbCommand cmd = dbHelper.GetStoredProcCommond(sql);
                dbHelper.AddInParameter(cmd, "@OperName", DbType.String, operName);
                dbHelper.AddInParameter(cmd, "@BeginDate", DbType.String, beginDate);
                dbHelper.AddInParameter(cmd, "@EndDate", DbType.String, endDate);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                return dt;
            }
        }
        #endregion

        #region  ------------其他------------

        /*
        /// <summary>
        /// 执行sql文件安装数据库
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static bool ExecuteSqlFile(string fileName, ProgressBar progress, Label label)
        {
            ///获取sql列表
            ArrayList listSql = new ArrayList();
            if (!File.Exists(fileName))
            {
                return false;
            }
            StreamReader rs = new StreamReader(fileName, System.Text.Encoding.Default);//注意编码
            string commandText = "";
            string varLine = "";
            while (rs.Peek() > -1)
            {
                varLine = rs.ReadLine();
                if (varLine == "")
                {
                    continue;
                }
                if (!varLine.ToUpper().Equals("GO"))
                {
                    commandText += varLine;
                    //commandText = commandText.Replace("@database_name=N'dbhr'", string.Format("@database_name=N'{0}'", dbname));
                    commandText += "\r\n";
                }
                else
                {
                    if (!commandText.Equals(""))
                    {
                        listSql.Add(commandText);
                        commandText = "";
                    }
                }
            }
            rs.Close();
            int count = listSql.Count;
            if (count > 0)
            {
                SqlConnection conn = new SqlConnection(AppSettings.MssqlConnectString);
                try
                {
                    conn.Open();
                }
                catch (Exception exx)
                {
                    MessageBox.Show(exx.Message);
                    return false;
                }
                SqlTransaction trans = conn.BeginTransaction();
                SqlCommand sc = new SqlCommand();
                sc.Connection = conn;
                sc.Transaction = trans;
                string error = string.Empty;
                try
                {
                    int index = 1;
                    foreach (string sql in listSql)
                    {
                        sc.CommandText = sql;
                        error = sql;
                        sc.ExecuteNonQuery();
                        progress.Value = index * 100 / count;
                        label.Text = progress.Value.ToString() + @"%";
                        index++;
                        Thread.Sleep(50);
                        Application.DoEvents();
                    }
                    trans.Commit();
                    return true;
                }
                catch (Exception e)
                {
                    trans.Rollback();
                    MessageBox.Show("执行" + error + "时发生错误，错误信息:" + e.Message);
                    return false;

                }
                finally
                {
                    conn.Close();
                }
            }
            else
            {
                return false;
            }
        }
        */
        /// <summary>
        /// 执行sql文件安装数据库
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static void ExecuteSqlFile(string fileName)
        {
            ///获取sql列表
            ArrayList listSql = new ArrayList();
            if (!File.Exists(fileName))
            {
                return;
            }
            StreamReader rs = new StreamReader(fileName, System.Text.Encoding.Default);//注意编码
            string commandText = "";
            string varLine = "";
            while (rs.Peek() > -1)
            {
                varLine = rs.ReadLine();
                if (varLine == "")
                {
                    continue;
                }
                if (!varLine.ToUpper().Equals("GO"))
                {
                    commandText += varLine;
                    //commandText = commandText.Replace("@database_name=N'dbhr'", string.Format("@database_name=N'{0}'", dbname));
                    commandText += "\r\n";
                }
                else
                {
                    if (!commandText.Equals(""))
                    {
                        listSql.Add(commandText);
                        commandText = "";
                    }
                }
            }
            rs.Close();
            int count = listSql.Count;
            if (count > 0)
            {
                SqlConnection conn = new SqlConnection(AppSettings.MssqlConnectString);
                try
                {
                    conn.Open();
                }
                catch
                {
                    return;
                }
                SqlTransaction trans = conn.BeginTransaction();
                SqlCommand sc = new SqlCommand();
                sc.Connection = conn;
                sc.Transaction = trans;
                string error = string.Empty;
                try
                {
                    foreach (string sql in listSql)
                    {
                        sc.CommandText = sql;
                        error = sql;
                        sc.ExecuteNonQuery();
                    }
                    trans.Commit();
                    return;
                }
                catch
                {
                    trans.Rollback();

                    return;

                }
                finally
                {
                    conn.Close();
                }
            }
            else
            {
                return;
            }
        }


        #endregion



        #region  -----------------LED-----------------

        /// <summary>
        /// 添加静态区域
        /// </summary>
        /// <param name="areaName"></param>
        /// <param name="point_X"></param>
        /// <param name="point_Y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="rows"></param>
        /// <param name="changeRow"></param>
        /// <param name="spacing"></param>
        /// <param name="content"></param>
        /// <param name="displayEffect"></param>
        /// <param name="speed"></param>
        /// <param name="stayTime"></param>
        public static void AddStaticArea(string areaName, int point_X, int point_Y, int width, int height, int rows, int changeRow, int spacing, string content, int displayEffect, int speed, int stayTime)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "InsertArea";
                DbCommand cmd = dbHelper.GetStoredProcCommond(sql);
                dbHelper.AddInParameter(cmd, "@AreaId", DbType.String, areaName);
                dbHelper.AddInParameter(cmd, "@AreaType", DbType.Int32, 0);
                dbHelper.AddInParameter(cmd, "@Point_X", DbType.Int32, point_X);
                dbHelper.AddInParameter(cmd, "@Point_Y", DbType.Int32, point_Y);
                dbHelper.AddInParameter(cmd, "@Width", DbType.Int32, width);
                dbHelper.AddInParameter(cmd, "@Height", DbType.Int32, height);
                dbHelper.AddInParameter(cmd, "@Rows", DbType.Int32, rows);
                dbHelper.AddInParameter(cmd, "@ChangeRow", DbType.Int32, changeRow);
                dbHelper.AddInParameter(cmd, "@Spacing", DbType.Int32, spacing);
                dbHelper.AddInParameter(cmd, "@Content", DbType.String, content);
                dbHelper.AddInParameter(cmd, "@DisplayEffect", DbType.Int32, displayEffect);
                dbHelper.AddInParameter(cmd, "@Speed", DbType.Int32, speed);
                dbHelper.AddInParameter(cmd, "@StayTime", DbType.Int32, stayTime);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }
        /// <summary>
        /// 添加动态区域
        /// </summary>
        /// <param name="areaName"></param>
        /// <param name="point_X"></param>
        /// <param name="point_Y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="rows"></param>
        /// <param name="changeRow"></param>
        /// <param name="spacing"></param>
        /// <param name="content"></param>
        /// <param name="displayEffect"></param>
        /// <param name="speed"></param>
        /// <param name="stayTime"></param>
        public static void AddDynamicArea(AreaInfo area)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "InsertArea";
                DbCommand cmd = dbHelper.GetStoredProcCommond(sql);
                dbHelper.AddInParameter(cmd, "@AreaId", DbType.Int32, area.AreaId);
                dbHelper.AddInParameter(cmd, "@BorderEffect", DbType.Int32, area.BorderEffect);
                dbHelper.AddInParameter(cmd, "@BorderLength", DbType.Int32, area.BorderLength);
                dbHelper.AddInParameter(cmd, "@BorderNo", DbType.Int32, area.BorderNo);
                dbHelper.AddInParameter(cmd, "@BorderSpeed", DbType.Int32, area.BorderSpeed);
                dbHelper.AddInParameter(cmd, "@BordreType", DbType.Int32, area.BordreType);
                dbHelper.AddInParameter(cmd, "@DisplayEffect", DbType.Int32, area.DisplayEffect);
                dbHelper.AddInParameter(cmd, "@Height", DbType.Int32, area.Height);
                dbHelper.AddInParameter(cmd, "@LID", DbType.Int32, area.LID);
                dbHelper.AddInParameter(cmd, "@Point_X", DbType.Int32, area.Point_X);
                dbHelper.AddInParameter(cmd, "@Point_Y", DbType.Int32, area.Point_Y);
                dbHelper.AddInParameter(cmd, "@SingleLine", DbType.Int32, area.SingleLine);
                dbHelper.AddInParameter(cmd, "@Speed", DbType.Int32, area.Speed);
                dbHelper.AddInParameter(cmd, "@Stay", DbType.Int32, area.Stay);
                dbHelper.AddInParameter(cmd, "@Text", DbType.String, area.Text);
                dbHelper.AddInParameter(cmd, "@TextBold", DbType.Int32, area.TextBold);
                dbHelper.AddInParameter(cmd, "@TextFont", DbType.String, area.TextFont);
                dbHelper.AddInParameter(cmd, "@TextFontSize", DbType.Int32, area.TextFontSize);
                dbHelper.AddInParameter(cmd, "@Width", DbType.Int32, area.Width);
                dbHelper.AddInParameter(cmd, "@Interval", DbType.Int32, area.Interval);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }


        /// <summary>
        /// 获取静态区域列表
        /// </summary>
        /// <returns></returns>
        public static List<AreaInfo> GetAreaList(int lid)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "select * from AreaInfo where Lid = " + lid;
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                List<AreaInfo> areaList = new List<AreaInfo>();
                foreach (DataRow row in dt.Rows)
                {
                    AreaInfo area = new AreaInfo();
                    area.LID = lid;
                    area.AreaId = Convert.ToInt32(row["AreaId"]);
                    area.BorderEffect = Convert.ToInt32(row["BorderEffect"]);
                    area.BorderLength = Convert.ToInt32(row["BorderLength"]);
                    area.BorderNo = Convert.ToInt32(row["BorderNo"]);
                    area.BorderSpeed = Convert.ToInt32(row["BorderSpeed"]);
                    area.BordreType = Convert.ToInt32(row["BordreType"]);
                    area.DisplayEffect = Convert.ToInt32(row["DisplayEffect"]);
                    area.Height = Convert.ToInt32(row["Height"]);
                    area.Point_X = Convert.ToInt32(row["Point_X"]);
                    area.Point_Y = Convert.ToInt32(row["Point_Y"]);
                    area.RecId = Convert.ToInt32(row["RecId"]);
                    area.SingleLine = Convert.ToInt32(row["SingleLine"]);
                    area.Speed = Convert.ToInt32(row["Speed"]);
                    area.Stay = Convert.ToInt32(row["Stay"]);
                    area.Text = row["Text"].ToString();
                    area.TextBold = Convert.ToInt32(row["TextBold"]);
                    area.TextFont = row["TextFont"].ToString();
                    area.TextFontSize = Convert.ToInt32(row["TextFontSize"]);
                    area.Width = Convert.ToInt32(row["Width"]);
                    area.Interval = Convert.ToInt32(row["Interval"]);
                    areaList.Add(area);
                }
                return areaList;
            }
        }

        /// <summary>
        /// 删除区域
        /// </summary>
        /// <param name="areaId"></param>
        public static void DeleteArea(int lId, int areaId)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = " Delete From AreaInfo where LID =" + lId + "  and  AreaId =" + areaId.ToString();
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }
        #endregion

        /// <summary>
        /// 通过工号，姓名，卡状态查找员工与卡信息
        /// </summary>
        /// <param name="empCode"></param>
        /// <param name="empName"></param>
        /// <param name="ioFlag"></param>
        /// <returns></returns>
        public static DataTable GetEmpAndCardListFind(int deptId, string empCode, string empName, int ioFlag)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string s_empCode = empCode.Equals(string.Empty) ? "" : " and a.empcode like '%" + empCode + "%'  ";
                string s_empName = empName.Equals(string.Empty) ? "" : " and a.empName like '%" + empName + "%' ";
                string s_IOFlag = " and b.IOFlag = " + ioFlag.ToString();
                string sql = "select a.EmpId,a.empcode,a.empname, (case b.IoFlag when 3 then '场外' else '场内' end) as IOFlag ,'改变状态' as Operate from empinfo a,CardInfo b "
                + "where a.empId = b.empId and b.cardstatus = 1  and a.DeptId = " + deptId.ToString() + s_IOFlag + s_empCode + s_empName + "  order by a.EmpId";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                return dt;
            }
        }

        /// <summary>
        /// 修改卡的进出状态
        /// </summary>
        /// <param name="empId"></param>
        public static void UpdateCardIOFlag(int empId, int ioFlag)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "Update CardInfo set IOFlag =" + ioFlag.ToString() + " Where CardStatus = 1 and EmpId =" + empId.ToString();
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }

        /// <summary>
        /// 查找部门人数
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string GetContent(string content, int lId)
        {
            string _Content = string.Empty;
            List<int> deptList = new List<int>();
            if (content.IndexOf("【") < 0 || content.IndexOf("】") < 0)
            {
                return content;
            }
            while (content.Length > 0)
            {
                if (content.IndexOf("【") < 0 || content.IndexOf("】") < 0)
                {
                    _Content += content;
                    break;
                }
                else
                {
                    int index1 = content.IndexOf("【");
                    int index2 = content.IndexOf("】");
                    if (index2 - index1 > 0)
                    {
                        string deptId = content.Substring(index1 + 1, index2 - index1 - 1);
                        //校验是否为数字
                        if (!System.Text.RegularExpressions.Regex.IsMatch(deptId, "^\\d+$"))
                        {
                            _Content = content.Substring(0, index2 + 1);
                            content = content.Substring(index2 + 1);
                            continue;
                        }
                        else
                        {
                            int curIndex = Convert.ToInt32(deptId);
                            string instead = string.Empty;
                            ///大于1000的为记录参数
                            if (curIndex >= 1000)
                            {
                                int curIndex1 = curIndex % 1000;
                                int recordIndex = curIndex / 1000;
                                List<string> list = MainService.GetLastIndexRecord(recordIndex);
                                switch (curIndex1)
                                {
                                    case 998:
                                        instead = list[1];
                                        break;
                                    case 997:
                                        instead = list[2];
                                        break;
                                    case 996:
                                        instead = Convert.ToDateTime(list[3]).ToString("HH:mm");
                                        break;
                                    case 995:
                                        instead = list[0];
                                        break;
                                }
                                /*
                                int recordIndex = curIndex / 1000;
                                LedRecordQueue ledRecordQueue = null;
                                foreach (LedRecordQueue ledQueue in GlobalVariables.LedRecord)
                                {
                                    if (ledQueue.LID == lId)
                                    {
                                        ledRecordQueue = ledQueue;
                                        break;
                                    }
                                }
                                if (ledRecordQueue != null)
                                {
                                    if (ledRecordQueue.RecordQueue.Count >= recordIndex)
                                    {
                                        List<Record> recordList = new List<Record>();
                                        foreach (Record record in ledRecordQueue.RecordQueue)
                                        {
                                            recordList.Add(record);
                                        }
                                        Record curRecord = recordList[recordIndex - 1];

                                        int curIndex1 = curIndex % 1000;
                                        switch (curIndex1)
                                        {
                                            case 998:
                                                instead = curRecord.EmpName;
                                                break;
                                            case 997:
                                                instead = curRecord.SIOFlag;
                                                break;
                                            case 996:
                                                instead = Convert.ToDateTime(curRecord.RecDateTime).ToString("HH:mm");
                                                break;
                                            case 995:
                                                instead = curRecord.DeptName;
                                                break;
                                        }
                                    }
                                }
                                */
                            }
                            else
                            {
                                switch (curIndex)
                                {
                                    ///场内总人数
                                    case 999:
                                        instead = MainService.GetIOTotal();
                                        instead = IOTotalFormat(instead);
                                        break;
                                    case 994:
                                        instead = GetRecordsOfIn();
                                        break;
                                    case 993:
                                        instead = GetRecordOfOut();
                                        break;
                                    default:
                                        using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
                                        {
                                            string sql = "Select Count(*) from EmpInfo a,CardInfo b where a.EmpId = b.Empid and b.CardStatus = 1 and b.IOFlag = 4 and a.DeptId =" + deptId;
                                            DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                                            DataTable dt = dbHelper.ExecuteDataTable(cmd);
                                            foreach (DataRow row in dt.Rows)
                                            {
                                                instead = row[0].ToString();
                                            }
                                        }
                                        instead = IOTotalFormat(instead);
                                        break;
                                }
                            }
                            _Content += content.Substring(0, index1) + instead;
                            content = content.Substring(index2 + 1);
                            continue;
                        }
                    }
                }
            }
            return _Content;
        }

        #region 获取最后第N条记录
        private static List<string> GetLastIndexRecord(int recordIndex)
        {
            List<string> list = new List<string>();
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "Declare @CardNo varchar(30),@EmpName varchar(30),@IOFlag varchar(10),@RecDateTime varchar(30),@DeptName varchar(30),@EmpId int  ";
                sql += "Create Table #Record(RecId int,CardNo varchar(20),IOFlag varchar(10),RecDateTime varchar(30))  ";
                sql += string.Format("Insert  #Record Select Top {0} RecId,CardNo,IOFlag,RecDateTime From Record Where RecordType = '有效票' Order By RecId Desc  ", recordIndex);
                sql += "Select top 1 @CardNo = CardNo,@IOFlag = IOFlag,@RecDateTime = RecDateTime from #Record Order By RecId asc  ";
                sql += "Select @EmpId = EmpId From CardInfo where CardStatus = 1 and CardNo = @CardNo  ";
                sql += "Select @EmpName = EmpName From EmpInfo where EmpId = @EmpId  ";
                sql += "Select @DeptName = DeptName From DeptInfo where DeptId =(Select DeptId From EmpInfo where EmpId = @EmpId)  ";
                sql += "Select @DeptName As DeptName,@EmpName as EmpName,@IOFlag as IOFlag,@RecDateTime AS RecDateTime  ";
                sql += "Drop table #record  ";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                foreach (DataRow row in dt.Rows)
                {
                    list.Add(row["DeptName"].ToString());
                    list.Add(row["EmpName"].ToString());
                    list.Add(row["IOFlag"].ToString());
                    list.Add(row["RecDateTime"].ToString());
                }
            }
            return list;
        }
        #endregion


        #region 获取当天出场人数

        private static string GetRecordOfOut()
        {
            int total = 0;
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = string.Format("Select Count(*) As Total from Record where IOFlag = '出' and RecordType = '有效票' and recDatetime >= '{0}'", DateTime.Now.ToString("yyyy-MM-dd"));
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                foreach (DataRow row in dt.Rows)
                {
                    total = Convert.ToInt32(row["Total"]);
                }
            }
            return total.ToString("0000");
        }

        #endregion

        #region 获取进场总人数
        private static string GetRecordsOfIn()
        {
            int total = 0;
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = string.Format("Select Count(*) As Total from Record where IOFlag = '进' and RecordType = '有效票' and recDatetime >= '{0}'", DateTime.Now.ToString("yyyy-MM-dd"));
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                foreach (DataRow row in dt.Rows)
                {
                    total = Convert.ToInt32(row["Total"]);
                }
            }
            return total.ToString("0000");
        }
        #endregion


        /// <summary>
        /// 统计场内总人数
        /// </summary>
        /// <returns></returns>
        public static string GetIOTotal()
        {
            string total = "0";
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "Select Count(*) as Total from EmpInfo a,CardInfo b where a.EmpId = b.EmpId and b.Cardstatus = 1 and  b.IOFlag = 4";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                foreach (DataRow row in dt.Rows)
                {
                    total = row["Total"].ToString();
                }
            }
            return total;
        }

        /// <summary>
        /// 字符串格式化
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public static string IOTotalFormat(string count)
        {
            int length = count.Length;
            if (length < 4)
            {
                for (int i = 0; i < 4 - length; i++)
                {
                    count = "0" + count;
                }
            }
            if (length > 4)
            {
                count = count.Substring(length - 4, 4);
            }
            return count;
        }

        /// <summary>
        /// 检查登录是否成功
        /// </summary>
        /// <param name="OperName"></param>
        /// <param name="OpPass"></param>
        /// <returns></returns>
        public static bool CheckLogin(string OperName, string OpPass)
        {
            try
            {
                using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
                {
                    string sql = "select  *  from operinfo where OperName ='" + OperName + "' and OperPassword = '" + OpPass + "'";
                    DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                    DataTable dt = dbHelper.ExecuteDataTable(cmd);
                    if (dt.Rows.Count >= 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch
            {
                return false;
            }

        }
        /// <summary>
        /// 根据登录名查找登录ID
        /// </summary>
        /// <param name="OperName"></param>
        /// <returns></returns>
        public static int GetOperIdByOperName(string OperName, string OpPass)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "select  OperId  from operinfo where OperName ='" + OperName + "' and OperPassword = '" + OpPass + "'";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                int operId = 0;
                foreach (DataRow row in dt.Rows)
                {
                    operId = Convert.ToInt32(row["OperId"]);
                }
                return operId;
            }
        }

        /// <summary>
        /// 获取用户菜单
        /// </summary>
        /// <param name="OperId"></param>
        /// <returns></returns>


        /// <summary>
        /// 删除操作员信息
        /// </summary>
        /// <param name="operId"></param>
        public static void DeleteOperInfo(int operId)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                DbCommand cmd = dbHelper.GetStoredProcCommond("DeleteOper");
                dbHelper.AddInParameter(cmd, "@operId", DbType.Int32, operId);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }


        /// <summary>
        /// 获取日志列表
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static DataTable GetOperLog(int type)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "Select LogId as 日志编号,OperName as 操作员,RecDateTime as 记录时间,Object as 操作对象,LogAction as 动作,LogMessage 内容 from OperLog where LogType = " + type.ToString(); ;
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                return dt;
            }

        }

        /// <summary>
        /// 获取数据库服务器列表
        /// </summary>
        /// <returns></returns>
        public static DataTable GetServerList()
        {
            SqlDataSourceEnumerator instance = SqlDataSourceEnumerator.Instance;
            System.Data.DataTable table = instance.GetDataSources();
            return table;
        }
        /// <summary>
        /// 删除操作员权限
        /// </summary>
        /// <param name="OperId"></param>
        public static void DeleteMenusOfOper(int OperId)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "Delete from MenuOfOper where OperId =" + OperId.ToString();
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
            }

        }

        /// <summary>
        /// 插入菜单项
        /// </summary>
        /// <param name="OperId"></param>
        /// <param name="menuId"></param>
        public static void InsertMenusOfOper(int OperId, int menuId)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "Insert into MenuOfOper(OperId,MenuId) values(" + OperId.ToString() + "," + menuId.ToString() + ")";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }

        /// <summary>
        /// 初始化数据库
        /// </summary>
        public static void InitSystem()
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "InitSystem";
                DbCommand cmd = dbHelper.GetStoredProcCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }



        /// <summary>
        /// 数据库还原
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static bool RestoreDatabase(string fileName, string dbName, string connectString)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper(connectString))
            {
                ///<---------先找出所有连接数----------->
                string sqlKillSid = "Use master SELECT spid FROM sysprocesses ,sysdatabases WHERE sysprocesses.dbid=sysdatabases.dbid AND sysdatabases.Name='" + dbName + "'";

                DbCommand cmdKillSid = dbHelper.GetSqlStringCommond(sqlKillSid);
                DataTable dtKillSid = dbHelper.ExecuteDataTable(cmdKillSid);
                ///<---------删除所有连接数----------->
                foreach (DataRow row in dtKillSid.Rows)
                {
                    string sqlKill = "Kill " + row[0].ToString();
                    DbCommand cmdKill = dbHelper.GetSqlStringCommond(sqlKill);
                    dbHelper.ExecuteNonQuery(cmdKill);
                }
                ///<---------数据库还原----------->
                // SqlConnection constring = new SqlConnection(Data Source=(local);Initial Catalog=master;User ID=sa;Password=123);
                string sql = "Use Master  RESTORE DATABASE   " + dbName + "   FROM DISK ='" + fileName + "'   WITH REPLACE";//数据库名称和路径 WITH REPLACE是去除日志文件
                ///string sql = string.Format("use master;exec p_killspid '{0}';restore database {0} From disk = '{1}' with replace;", GlobalVariables.SpecifiedDBName, fileName);
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                int index = dbHelper.ExecuteNonQuery(cmd);
                return index >= 0 ? false : true
                    ;
            }
        }

        /// <summary>
        /// 导入人员资料
        /// </summary>
        /// <param name="deptName"></param>
        /// <param name="empCode"></param>
        /// <param name="empName"></param>
        public static void ImportEmpInfo(OleDbHelper dbHelper, string deptName, string empCode, string empName)
        {
            DbCommand cmd = dbHelper.GetStoredProcCommond("ImportEmpInfo");
            dbHelper.AddInParameter(cmd, "@DeptName", DbType.String, deptName);
            dbHelper.AddInParameter(cmd, "@EmpCode", DbType.String, empCode);
            dbHelper.AddInParameter(cmd, "@EmpName", DbType.String, empName);
            dbHelper.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 批量导入员工信息
        /// </summary>
        /// <param name="p"></param>
        /// <param name="deptName"></param>
        /// <param name="empCode"></param>
        /// <param name="empName"></param>
        /// <param name="CardCode"></param>
        /// <param name="telePhone"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        public static void BachImport(int type, string deptName, string empCode, string empName, string cardNo, string telePhone, string beginDate, string endDate)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                DbCommand cmd = dbHelper.GetStoredProcCommond("BatchImport");
                dbHelper.AddInParameter(cmd, "@Type", DbType.Int32, type);
                dbHelper.AddInParameter(cmd, "@DeptName", DbType.String, deptName);
                dbHelper.AddInParameter(cmd, "@EmpCode", DbType.String, empCode);
                dbHelper.AddInParameter(cmd, "@EmpName", DbType.String, empName);
                dbHelper.AddInParameter(cmd, "@CardNo", DbType.String, cardNo);
                dbHelper.AddInParameter(cmd, "@Telephone", DbType.String, telePhone);
                dbHelper.AddInParameter(cmd, "@BeginDate", DbType.String, beginDate);
                dbHelper.AddInParameter(cmd, "@EndDate", DbType.String, endDate);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }

        /// <summary>
        /// 批量导入身份证信息
        /// </summary>
        /// <param name="deptName"></param>
        /// <param name="empCode"></param>
        /// <param name="empName"></param>
        /// <param name="cardNo"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        public static void ImportIDCardInfo(string deptName, string empCode, string empName, string cardNo, string beginDate, string endDate)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                DbCommand cmd = dbHelper.GetStoredProcCommond("ImportIDCardInfo");
                dbHelper.AddInParameter(cmd, "@DeptName", DbType.String, deptName);
                dbHelper.AddInParameter(cmd, "@EmpCode", DbType.String, empCode);
                dbHelper.AddInParameter(cmd, "@EmpName", DbType.String, empName);
                dbHelper.AddInParameter(cmd, "@CardNo", DbType.String, cardNo);
                dbHelper.AddInParameter(cmd, "@BeginDate", DbType.String, beginDate);
                dbHelper.AddInParameter(cmd, "@EndDate", DbType.String, endDate);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }


        /// <summary>
        /// 通过导入Excel的方式删除人员与卡信息
        /// </summary>
        /// <param name="deptName"></param>
        /// <param name="empCode"></param>
        /// <param name="empName"></param>
        /// <param name="cardNo"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static bool DeleteIDCardInfo(string deptName, string empCode, string empName, string cardNo, string beginDate, string endDate)
        {
            bool flag = false;
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                DbCommand cmd = dbHelper.GetStoredProcCommond("DeleteIDCardInfo");
                dbHelper.AddInParameter(cmd, "@DeptName", DbType.String, deptName);
                dbHelper.AddInParameter(cmd, "@EmpCode", DbType.String, empCode);
                dbHelper.AddInParameter(cmd, "@EmpName", DbType.String, empName);
                dbHelper.AddInParameter(cmd, "@CardNo", DbType.String, cardNo);
                dbHelper.AddInParameter(cmd, "@BeginDate", DbType.String, beginDate);
                dbHelper.AddInParameter(cmd, "@EndDate", DbType.String, endDate);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                foreach (DataRow row in dt.Rows)
                {
                    flag = Convert.ToInt32(row[0]) == 1 ? true : false;
                }
            }
            return flag;
        }
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="OperName"></param>
        /// <param name="OperPass"></param>
        public static void AddOperInfo(string OperName, string OperPass)
        {

            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "insert into OperInfo(OperName,OperPassword) values ('" + OperName + "','" + OperPass + "')";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }

        /// <summary>
        /// 获取用户编号
        /// </summary>
        /// <param name="operName"></param>
        /// <returns></returns>
        public static int GetOperIdByOperName(string operName)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "select  OperId  from operinfo where OperName ='" + operName + "'";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                int operId = 0;
                foreach (DataRow row in dt.Rows)
                {
                    operId = Convert.ToInt32(row["OperId"]);
                }
                return operId;
            }
        }

        public static string GetOperPass(int OperId)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string pass = string.Empty;
                string sql = "select OperPassword from operinfo where op_id =" + OperId.ToString();
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                foreach (DataRow row in dt.Rows)
                {
                    pass = row["OperPassword"].ToString();
                }
                return pass;
            }
        }

        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="OperId"></param>
        /// <param name="OperPassNew"></param>
        public static void UpdateUserPass(int OperId, string OperPassNew)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "update operInfo set OperPassword = '" + OperPassNew + "'  where OperId =" + OperId.ToString();
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }



        #region LED
        /// <summary>
        /// 更新动态区域
        /// </summary>
        /// <param name="area"></param>
        public static void UpdateStaticArea(Area area)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "UpdateStaticArea";
                DbCommand cmd = dbHelper.GetStoredProcCommond(sql);
                dbHelper.AddInParameter(cmd, "@AreaId", DbType.Int32, area.AreaId);
                dbHelper.AddInParameter(cmd, "@AreaName", DbType.String, area.AreaName);
                dbHelper.AddInParameter(cmd, "@Point_X", DbType.Int32, area.Point_X);
                dbHelper.AddInParameter(cmd, "@Point_Y", DbType.Int32, area.Point_Y);
                dbHelper.AddInParameter(cmd, "@Width", DbType.Int32, area.Width);
                dbHelper.AddInParameter(cmd, "@Height", DbType.Int32, area.Height);
                dbHelper.AddInParameter(cmd, "@Rows", DbType.Int32, area.Rows);
                dbHelper.AddInParameter(cmd, "@ChangeRow", DbType.Int32, area.ChangeRow);
                dbHelper.AddInParameter(cmd, "@Spacing", DbType.Int32, area.Spacing);
                dbHelper.AddInParameter(cmd, "@Content", DbType.String, area.Content);
                dbHelper.AddInParameter(cmd, "@DisplayEffect", DbType.Int32, area.DisplayEffect);
                dbHelper.AddInParameter(cmd, "@Speed", DbType.Int32, area.Speed);
                dbHelper.AddInParameter(cmd, "@StayTime", DbType.Int32, area.StayTime);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }



        /*
        /// <summary>
        /// 获取动态区域列表
        /// </summary>
        /// <returns></returns>
        public static List<DynamicArea> GetDynamicAreaList()
        {
            /*
            List<DynamicArea> dAreaList = new List<DynamicArea>();
            using (DbHelper dbHelper = new DbHelper())
            {
                string sql = "Select * from AreaInfo where AreaType = 1";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                for (int index = 0; index < dt.Rows.Count; index++)
                {
                    DataRow row = dt.Rows[index];
                    DynamicArea dArea = new DynamicArea();
                    Area area = new Area();
                    area.AreaId = Convert.ToInt32(row["AreaId"]);
                    area.AreaName = row["AreaName"].ToString();
                    area.AreaType = Convert.ToInt32(row["AreaType"]);
                    area.Point_X = Convert.ToInt32(row["Point_X"]);
                    area.Point_Y = Convert.ToInt32(row["Point_Y"]);
                    area.Width = Convert.ToInt32(row["Width"]);
                    area.Height = Convert.ToInt32(row["Height"]);
                    area.Rows = Convert.ToInt32(row["Rows"]);
                    area.ChangeRow = Convert.ToInt32(row["ChangeRow"]);
                    area.Spacing = Convert.ToInt32(row["Spacing"]);
                    area.Content = row["Content"].ToString();
                    area.DisplayEffect = Convert.ToInt32(row["DisplayEffect"]);
                    area.Speed = Convert.ToInt32(row["Speed"]);
                    area.StayTime = Convert.ToInt32(row["StayTime"]);
                    dArea.InitHeader(area);
                    dArea.bx_5k.DynamicAreaLoc = (byte)index;
                    dAreaList.Add(dArea);
                }
            }
            return dAreaList;

        }
    */
        #endregion

        /// <summary>
        /// 根据工号得到EmpId
        /// </summary>
        /// <param name="empCode"></param>
        /// <returns></returns>
        public static int GetEmpIdByEmpCode(string empCode)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                int empId = 0;
                string sql = "select EmpId From EmpInfo where EmpCode ='" + empCode + "'";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                foreach (DataRow row in dt.Rows)
                {
                    empId = Convert.ToInt32(row["EmpId"].ToString());
                }
                return empId;
            }
        }

        /// <summary>
        /// 检测LED控制卡是否已经存在
        /// </summary>
        /// <param name="ipaddress"></param>
        /// <returns></returns>
        public static bool CheckLEDControllerExists(string ipaddress)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "Select top 1 * from LEDController where IPAddress ='" + ipaddress + "'";
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

        /// <summary>
        /// 添加LED控制器
        /// </summary>
        /// <param name="protocol"></param>
        /// <param name="width"></param>
        /// <param name="heigth"></param>
        /// <param name="ipaddress"></param>
        /// <param name="port"></param>
        /// <param name="interval"></param>
        public static void AddLEDController(int nControlType, int protocol, int width, int heigth, string ipaddress, int port, int interval, List<int> devList, int totalRecord)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string devString = string.Empty;
                foreach (int index in devList)
                {
                    devString += index + ",";
                }
                devString = devString.Substring(0, devString.LastIndexOf(","));
                string sql = "Insert Into LedController(ControlType,Protocol,Width,Heigth,IPAddress,Port,Interval,DevList,TotalRecord) values(" + nControlType + "," + protocol + "," + width + "," + heigth + ",'" + ipaddress + "'," + port + "," + interval + ",'" + devString + "'," + totalRecord + ")";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }


        /// <summary>
        /// 修改LED控制卡信息
        /// </summary>
        /// <param name="lId"></param>
        /// <param name="nControlType"></param>
        /// <param name="protocol"></param>
        /// <param name="width"></param>
        /// <param name="heigth"></param>
        /// <param name="ipaddress"></param>
        /// <param name="port"></param>
        /// <param name="interval"></param>
        /// <param name="devList"></param>
        /// <param name="totalRecord"></param>
        public static void UpdateLEDController(int lId, int nControlType, int protocol, int width, int heigth, string ipaddress, int port, int interval, List<int> devList, int totalRecord)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string devString = string.Empty;
                foreach (int index in devList)
                {
                    devString += index + ",";
                }
                devString = devString.Substring(0, devString.LastIndexOf(","));
                string sql = "Update LedController Set ";
                sql += " ControlType = " + nControlType + ",";
                sql += " protocol = " + protocol + ",";
                sql += " width = " + width + ",";
                sql += " heigth = " + heigth + ",";
                sql += " ipaddress = '" + ipaddress + "',";
                sql += " port = " + port + ",";
                sql += " interval = " + interval + ",";
                sql += " devList = '" + devString + "',";
                sql += " totalRecord = " + totalRecord;
                sql += "  where Lid = " + lId;
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }

        /// <summary>
        /// 加载LED控制卡列表
        /// </summary>
        public static DataTable LoadLEDController()
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "Select * from LEDController";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                return dt;
            }
        }

        /// <summary>
        /// 获取LED控制卡的编号
        /// </summary>
        /// <param name="ipaddress"></param>
        /// <returns></returns>
        public static int GetLID(string ipaddress)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "Select LID from LEDController where IPAddress = '" + ipaddress + "'";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                int lId = 0;
                foreach (DataRow row in dt.Rows)
                {
                    lId = Convert.ToInt32(row["LID"]);
                }
                return lId;
            }
        }

        /// <summary>
        /// 获取LED控制卡信息
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static LedController GetLedController(int lId)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "Select * from LEDController where LID = " + lId;
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                LedController ledController = null;
                foreach (DataRow row in dt.Rows)
                {
                    ledController = new LedController();
                    ledController.ControlType = Convert.ToInt32(row["ControlType"]);
                    ledController.DevList = row["DevList"].ToString();
                    ledController.Lid = Convert.ToInt32(row["LID"]);
                    ledController.Heigth = Convert.ToInt32(row["Heigth"]);
                    ledController.Interval = Convert.ToInt32(row["Interval"]);
                    ledController.IPaddress = row["IPAddress"].ToString();
                    ledController.Port = Convert.ToInt32(row["Port"]);
                    ledController.Protocol = Convert.ToInt32(row["Protocol"]);
                    ledController.Width = Convert.ToInt32(row["Width"]);
                    ledController.TotalRecord = Convert.ToInt32(row["TotalRecord"]);
                }
                return ledController;
            }
        }

        /// <summary>
        /// 获取LED控制卡列表
        /// </summary>
        /// <returns></returns>
        public static List<LedController> GetLedControllerList()
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "Select * from LEDController";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                List<LedController> ledControllerList = new List<LedController>();
                foreach (DataRow row in dt.Rows)
                {
                    LedController ledController = new LedController();
                    ledController = new LedController();
                    ledController.ControlType = Convert.ToInt32(row["ControlType"]);
                    ledController.DevList = row["DevList"].ToString();
                    ledController.Lid = Convert.ToInt32(row["LID"]);
                    ledController.Heigth = Convert.ToInt32(row["Heigth"]);
                    ledController.Interval = Convert.ToInt32(row["Interval"]);
                    ledController.IPaddress = row["IPAddress"].ToString();
                    ledController.Port = Convert.ToInt32(row["Port"]);
                    ledController.Protocol = Convert.ToInt32(row["Protocol"]);
                    ledController.Width = Convert.ToInt32(row["Width"]);
                    ledController.TotalRecord = Convert.ToInt32(row["TotalRecord"]);
                    ledControllerList.Add(ledController);
                }
                return ledControllerList;
            }
        }

        /// <summary>
        /// 获取可用的动态区域编号
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static int GetAreaId(int lId)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "select  Isnull(Min(t.AreaId),0)  as AreaId from (Select Vid as AreaId from Voice where Vid >=0 and Vid <=3)  t where t.AreaId not in (select AreaId  from AreaInfo where LId =" + lId + ")";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                int areaId = 0;
                foreach (DataRow row in dt.Rows)
                {
                    areaId = Convert.ToInt32(row["AreaId"]);
                }
                return areaId;
            }
        }

        /// <summary>
        /// 加载动态区域列表
        /// </summary>
        /// <returns></returns>
        public static DataTable LoadDynamicAreaList(int lId)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "Select AreaId,Point_X,Point_Y,Width,Height,Text  From AreaInfo where LID =  " + lId;
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                return dt;
            }
        }

        /// <summary>
        /// 获取动态区域信息
        /// </summary>
        /// <param name="p"></param>
        /// <param name="p_2"></param>
        /// <returns></returns>
        public static AreaInfo GetAreaInfo(int lId, int areaId)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "Select  *  From AreaInfo where LID =  " + lId + "  and  AreaId = " + areaId;
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                AreaInfo area = null;
                foreach (DataRow row in dt.Rows)
                {
                    area = new AreaInfo();
                    area.LID = lId;
                    area.AreaId = areaId;
                    area.BorderEffect = Convert.ToInt32(row["BorderEffect"]);
                    area.BorderLength = Convert.ToInt32(row["BorderLength"]);
                    area.BorderNo = Convert.ToInt32(row["BorderNo"]);
                    area.BorderSpeed = Convert.ToInt32(row["BorderSpeed"]);
                    area.BordreType = Convert.ToInt32(row["BordreType"]);
                    area.DisplayEffect = Convert.ToInt32(row["DisplayEffect"]);
                    area.Height = Convert.ToInt32(row["Height"]);
                    area.Point_X = Convert.ToInt32(row["Point_X"]);
                    area.Point_Y = Convert.ToInt32(row["Point_Y"]);
                    area.RecId = Convert.ToInt32(row["RecId"]);
                    area.SingleLine = Convert.ToInt32(row["SingleLine"]);
                    area.Speed = Convert.ToInt32(row["Speed"]);
                    area.Stay = Convert.ToInt32(row["Stay"]);
                    area.Text = row["Text"].ToString();
                    area.TextBold = Convert.ToInt32(row["TextBold"]);
                    area.TextFont = row["TextFont"].ToString();
                    area.TextFontSize = Convert.ToInt32(row["TextFontSize"]);
                    area.Width = Convert.ToInt32(row["Width"]);
                    area.Interval = Convert.ToInt32(row["Interval"]);
                }
                return area;
            }
        }


        public static void UpdateDynamicArea(AreaInfo area)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "Update AreaInfo set ";
                sql += "  BorderEffect =  " + area.BorderEffect + " , ";
                sql += "  BorderLength =  " + area.BorderLength + " , ";
                sql += "  BorderNo  = " + area.BorderNo + " , ";
                sql += "  BorderSpeed  = " + area.BorderSpeed + " , ";
                sql += "  BordreType  = " + area.BordreType + " , ";
                sql += "  DisplayEffect  = " + area.DisplayEffect + " , ";
                sql += "  Height =  " + area.Height + " , ";
                sql += "  Point_X  = " + area.Point_X + " , ";
                sql += "  Point_Y  = " + area.Point_Y + " , ";
                sql += "  SingleLine  = " + area.SingleLine + " , ";
                sql += "  Speed  = " + area.Speed + " , ";
                sql += "  Stay  = " + area.Stay + " , ";
                sql += "  Text  = '" + area.Text + "' , ";
                sql += "  TextBold  = " + area.TextBold + " , ";
                sql += "  TextFont  = '" + area.TextFont + "' , ";
                sql += "  TextFontSize  =  " + area.TextFontSize + " ,";
                sql += "  Width = " + area.Width + " , ";
                sql += "  Interval = " + area.Interval;
                sql += "  Where  LID = " + area.LID + "  and AreaId = " + area.AreaId;
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }

        /// <summary>
        /// 得到LED显示记录条数
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static int GetRecordCount(int lId)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "Select TotalRecord  From LedController where LID =  " + lId;
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                int count = 0;
                foreach (DataRow row in dt.Rows)
                {
                    count = Convert.ToInt32(row["TotalRecord"]);
                }
                return count;
            }
        }



        /// <summary>
        /// 获取LED监控的设备列表
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static List<int> GetLedDevList(int lId)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "Select DevList  From LedController  where  Lid = " + lId;
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                List<int> devList = new List<int>();
                string devListString = string.Empty;
                foreach (DataRow row in dt.Rows)
                {
                    devListString = row["DevList"].ToString();
                }
                string[] devstring = devListString.Split(',');
                foreach (string s in devstring)
                {
                    devList.Add(Convert.ToInt32(s));
                }

                return devList;
            }
        }

        /// <summary>
        /// 删除控制卡信息
        /// </summary>
        /// <param name="lId"></param>
        public static void DeleteLedController(int lId)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "Delete From AreaInfo where Lid = " + lId;
                sql += "   Delete From LedController where Lid = " + lId;
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }

        /// <summary>
        /// 取消加载考勤功能
        /// </summary>
        public static void RemoveAttendModule()
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "Update Menus Set EnableFlag = 0  where  MenuId = 7 Or (MenuId >70 and MenuId < 80)";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }

        /// <summary>
        /// 启用考勤功能
        /// </summary>
        public static void EnableAttendModule()
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "Update Menus Set EnableFlag = 1  where  MenuId = 7 Or (MenuId >70 and MenuId < 80)";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }

        /// <summary>
        /// 身份证记录报表
        /// </summary>
        /// <param name="deviceId"></param>
        /// <param name="cardNo"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static DataTable GetIDCardReport(int deviceId, string cardNo, string beginDate, string endDate)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                List<Menus> menuList = new List<Menus>();
                string sql = "ReportOfIDCard";
                DbCommand cmd = dbHelper.GetStoredProcCommond(sql);
                dbHelper.AddInParameter(cmd, "@DeviceId", DbType.Int32, deviceId);
                dbHelper.AddInParameter(cmd, "@CardNo", DbType.String, cardNo);
                dbHelper.AddInParameter(cmd, "@BeginDate", DbType.String, beginDate);
                dbHelper.AddInParameter(cmd, "@EndDate", DbType.String, endDate);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                return dt;
            }
        }

        /// <summary>
        /// 通过身份证号码检查员工是否已经存在
        /// </summary>
        /// <param name="empCode"></param>
        public static bool CheckEmpCodeExists(string empCode)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "Select top 1 * from EmpInfo where EmpCode ='" + empCode + "'";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                if (dt.Rows.Count == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// 启用身份证记录报表
        /// </summary>
        public static void EnableIDCardReport()
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "Update Menus Set EnableFlag =1  where  MenuId = 44";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }

        /// <summary>
        /// 不启用身份证刷卡记录报表
        /// </summary>
        public static void DisableIDCardReport()
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "Update Menus Set EnableFlag =0  where  MenuId = 44";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }

        #region 检查是否存在卡号XXX在当前时间的请假范围内
        public static bool CheckOnLeave(string cardNo)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string now = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string sql = string.Format("Select top 1 * from AskForLeave where BeginDate<='{0}' and EndDate>='{1}' and CardNo ='{2}'", now, now, cardNo);
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                if (dt.Rows.Count == 1)
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

        #region 更新请假是否已经出门的信息
        public static void UpdateOnLeave(string cardNo)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string now = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string sql = string.Format("Update AskForLeave Set Active = 1 where BeginDate<='{0}' and EndDate>='{1}' and CardNo ='{2}' and Active = 0", now, now, cardNo);
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }
        #endregion

        #region 获取显示记录参数
        public static DisplayRecord GetDisplayRecord(int deviceId, int cardType, string sCardNo)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                DisplayRecord record = new DisplayRecord();
                record.DeviceName = GetUdpDeviceInfoByDevId(deviceId).DeviceName;
                string sql = "Declare @EmpId int,@DeptName varchar(20),@EmpCode varchar(20),@EmpName varchar(20)";
                sql += string.Format(" Select @EmpId = EmpId From CardInfo where CardNo ='{0}' and Type = {1} ", sCardNo, cardType);
                sql += " If(@EmpId > 0) ";
                sql += " Begin ";
                sql += " Select @EmpCode = EmpCode,@EmpName = EmpName From EmpInfo where EmpId = @EmpId ";
                sql += " Select @DeptName = DeptName From DeptInfo where DeptId =(Select DeptId From EmpInfo where EmpId = @EmpId) ";
                sql += " End ";
                sql += " Select @DeptName As DeptName,@EmpCode As EmpCode,@EmpName As EmpName";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                foreach (DataRow row in dt.Rows)
                {
                    record.DeptName = row["DeptName"].ToString();
                    record.EmpCode = row["EmpCode"].ToString();
                    record.EmpName = row["EmpName"].ToString();
                }
                return record;
            }
        }
        #endregion

        #region 进出人数统计
        public static DataTable SumaryOfIORecord(string beginTime, string endTime)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "SumaryOfIORecord";
                DbCommand cmd = dbHelper.GetStoredProcCommond(sql);
                dbHelper.AddInParameter(cmd, "@BeginTime", DbType.String, beginTime);
                dbHelper.AddInParameter(cmd, "@EndTime", DbType.String, endTime);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                return dt;
            }
        }

        #endregion

        #region 插入抓拍的图像

        public static void InsertCapture(int deviceId, int camId, string iOFlag, string recDatetime, byte[] image)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "InsertCapture ";
                DbCommand cmd = dbHelper.GetStoredProcCommond(sql);
                dbHelper.AddInParameter(cmd, "@DeviceId", DbType.Int32, deviceId);
                dbHelper.AddInParameter(cmd, "@CamId", DbType.Int32, camId);
                dbHelper.AddInParameter(cmd, "@IOFlag", DbType.String, iOFlag);
                dbHelper.AddInParameter(cmd, "@RecDatetime", DbType.String, recDatetime);
                dbHelper.AddInParameter(cmd, "@ImageString", DbType.Binary, image);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }
        #endregion

        #region 获取记录对应的抓拍图像
        public static byte[] GetImageOfRecord(int deviceId, string ioFlag, string recTime)
        {
            byte[] arr = null;
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = string.Format("Select ImageString From ImageOfRecord where deviceid ={0} and IOFlag ='{1}' and RecDatetime ='{2}'", deviceId, ioFlag, recTime);
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                foreach (DataRow row in dt.Rows)
                {
                    arr = (byte[])row[0];
                }
            }
            return arr;
        }
        #endregion

        #region 获取异常记录报表
        public static DataTable GetReportOfExceptionRecord(int deviceId, string beginDate, string endDate)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "ReportOfExceptionRecord";
                DbCommand cmd = dbHelper.GetStoredProcCommond(sql);
                dbHelper.AddInParameter(cmd, "@DeviceId", DbType.Int32, deviceId);
                dbHelper.AddInParameter(cmd, "@BeginDate", DbType.String, beginDate);
                dbHelper.AddInParameter(cmd, "@EndDate", DbType.String, endDate);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                return dt;
            }
        }
        #endregion



        #region 数据导入
        public static string DataImportWithIDCard(string deptName, string empCode, string empName, string idCard, string cardNo,
            int deptImportType, int empImportType, int cardImportType, int cardType, int cardProperityType, int inRight, int outRight,
            int showPhoto, int timeGroupOfIn, int timeGroupOfOut, int timeGroupOfVacation, int voice, string beginDate, string endDate)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string result = string.Empty;
                try
                {
                    string sql = "DataImportWithIDCard";
                    DbCommand cmd = dbHelper.GetStoredProcCommond(sql);
                    dbHelper.AddInParameter(cmd, "@DeptName", DbType.String, deptName);
                    dbHelper.AddInParameter(cmd, "@EmpCode", DbType.String, empCode);
                    dbHelper.AddInParameter(cmd, "@EmpName", DbType.String, empName);
                    dbHelper.AddInParameter(cmd, "@IDCard", DbType.String, idCard);
                    dbHelper.AddInParameter(cmd, "@CardNo", DbType.String, cardNo);
                    dbHelper.AddInParameter(cmd, "@DeptImportType", DbType.Int32, deptImportType);
                    dbHelper.AddInParameter(cmd, "@EmpImportType", DbType.Int32, empImportType);
                    dbHelper.AddInParameter(cmd, "@CardImportType", DbType.Int32, cardImportType);
                    dbHelper.AddInParameter(cmd, "@CardType", DbType.Int32, cardType);
                    dbHelper.AddInParameter(cmd, "@CardProperityType", DbType.Int32, cardProperityType);
                    dbHelper.AddInParameter(cmd, "@InRight", DbType.Int32, inRight);
                    dbHelper.AddInParameter(cmd, "@OutRight", DbType.Int32, outRight);
                    dbHelper.AddInParameter(cmd, "@ShowPhoto", DbType.Int32, showPhoto);
                    dbHelper.AddInParameter(cmd, "@TimeGroupOfIn", DbType.Int32, timeGroupOfIn);
                    dbHelper.AddInParameter(cmd, "@TimeGroupOfOut", DbType.Int32, timeGroupOfOut);
                    dbHelper.AddInParameter(cmd, "@TimeGroupOfVacation", DbType.Int32, timeGroupOfVacation);
                    dbHelper.AddInParameter(cmd, "@Voice", DbType.Int32, voice);
                    dbHelper.AddInParameter(cmd, "@BeginDate", DbType.String, beginDate);
                    dbHelper.AddInParameter(cmd, "@EndDate", DbType.String, endDate);
                    dbHelper.AddOutParameter(cmd, "@Result", DbType.String, 100);
                    dbHelper.ExecuteNonQuery(cmd);
                    result = cmd.Parameters["@Result"].Value.ToString();
                }
                catch (Exception ex)
                {
                    result = ex.Message;
                }
                return result;
            }
        }
        #endregion

        #region 检查表格是否存在
        public static void CheckTableExists()
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "if Not exists (select * from sysobjects where id = object_id(N'[dbo].[Student]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) ";
                sql += " Begin ";
                sql += " CREATE TABLE[dbo].[Student]( ";
                sql += " [RecId][int] IDENTITY(1, 1) NOT NULL,";
                sql += " [MainKey] [int] NOT NULL,";
                sql += " [SchoolName] [varchar](50) NULL,";
                sql += " [SchoolType] [varchar](50) NULL,";
                sql += " [JoinYear] [varchar](50) NULL,";
                sql += " [ClassName] [varchar](50) NULL,";
                sql += " [StudentCode]  [varchar](30) NULL,";
                sql += " [StudentName] [varchar](50) NULL,";
                sql += " [StudentType] [varchar](30) NULL,";
                sql += " [IDCardNo]  [varchar](20) NULL,";
                sql += " [SerialNo] [varchar](20) NOT NULL,";
                sql += " [Telephone] [varchar](20) NULL,";
                sql += " [PerantPhone1] [varchar](20) NULL,";
                sql += " [PerantPhone2] [varchar](20) NULL";
                sql += " ) ON[PRIMARY]";
                sql += " End";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }
        #endregion

        #region 插入学生表
        public static void ImportStudent(int mainKey, string schoolName, string schoolType, string joinYear, string className, string studentCode, string studentName, string studentType, string idCardNo, string serialNo, string telephone, string perantPhone1, string perantPhone2)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "InsertStudent";
                DbCommand cmd = dbHelper.GetStoredProcCommond(sql);
                dbHelper.AddInParameter(cmd, "@MainKey", DbType.String, mainKey);
                dbHelper.AddInParameter(cmd, "@SchoolName", DbType.String, schoolName);
                dbHelper.AddInParameter(cmd, "@SchoolType", DbType.String, schoolType);
                dbHelper.AddInParameter(cmd, "@JoinYear", DbType.String, joinYear);
                dbHelper.AddInParameter(cmd, "@ClassName", DbType.String, className);
                dbHelper.AddInParameter(cmd, "@StudentCode", DbType.String, studentCode);
                dbHelper.AddInParameter(cmd, "@StudentName", DbType.String, studentName);
                dbHelper.AddInParameter(cmd, "@StudentType", DbType.String, studentType);
                dbHelper.AddInParameter(cmd, "@IDCardNo", DbType.String, idCardNo);
                dbHelper.AddInParameter(cmd, "@SerialNo", DbType.String, serialNo);
                dbHelper.AddInParameter(cmd, "@Telephone", DbType.String, telephone);
                dbHelper.AddInParameter(cmd, "@PerantPhone1", DbType.String, perantPhone1);
                dbHelper.AddInParameter(cmd, "@PerantPhone2", DbType.String, perantPhone2);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }

        #endregion

        #region 查找合适的控制卡屏号
        public static decimal GetSuitableLid()
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                int lid = 1;
                string sql = "Select Min(Vid) As Lid from Voice Where Vid Not In( Select Lid From Led_LedController ) and Vid >0";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                foreach (DataRow row in dt.Rows)
                {
                    lid = Convert.ToInt32(row["Lid"]);
                }
                return lid;
            }
        }
        #endregion

        #region 获取未同步的消费记录
        public static List<DataOfConst> GetConstListUnSyned(string schoolId)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                List<DataOfConst> recordList = new List<DataOfConst>();
                try
                {
                    string sql = "GetUnSynedRecords";
                    DbCommand cmd = dbHelper.GetStoredProcCommond(sql);
                    DataTable dt = dbHelper.ExecuteDataTable(cmd);
                    foreach (DataRow row in dt.Rows)
                    {
                        DataOfConst record = new DataOfConst();
                        record.RecId = Convert.ToInt32(row["RecId"]);
                        record.SchoolId = schoolId;
                        record.StudentId = row["StudentId"].ToString();
                        record.Account = CardNoConvert(row["Account"].ToString());
                        record.PayType = Convert.ToInt32(row["PayType"]);
                        record.PayMoney = row["PayMoney"].ToString();
                        record.DevId = Convert.ToInt32(row["DevNo"]);
                        record.RecDatetime = row["RecDateTime"].ToString();
                        record.Mark = string.Empty;
                        recordList.Add(record);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return recordList;
            }
        }
        #endregion

        #region 卡号转换
        private static string CardNoConvert(string cardNo)
        {
            string newCardNo = string.Empty;
            try
            {
                byte[] arr = ArrayHelper.HexToArray(cardNo, 4);
                Array.Reverse(arr);
                UInt32 value = 4294967295 - BitConverter.ToUInt32(arr, 0);
                newCardNo = value.ToString();
            }
            catch
            {

            }
            return newCardNo;
        }
        #endregion


        #region 保存到已同步记录
        public static void InsertRecordOfSyned(DataOfConst record, string resultCode, string resultMessage)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                StringBuilder buffer = new StringBuilder();
                buffer.Append($"{Environment.NewLine}If Not Exists(Select top 1 * From RecordOfSyned Where SynedRecId ={record.RecId}) ");
                buffer.Append($"{Environment.NewLine}Insert RecordOfSyned(SynedRecId,SchoolId,StudentId,PayType,PayMoney,Mark,RecDateTime,ResultCode,ResultMessage)  values ({record.RecId},'{record.SchoolId}','{record.StudentId}',{record.PayType},'{record.PayMoney}','{record.Mark}','{record.RecDatetime}','{resultCode}','{resultMessage}')");
                //string sql = $"If Not Exists(Select top 1 * From RecordOfSyned Where SynedRecId ={record.RecId})  ";
                //sql += "Insert RecordOfSyned(SynedRecId,SchoolId,StudentId,PayType,PayMoney,Mark,RecDateTime,ResultCode,ResultMessage)  values (@SynedRecId,@SchoolId,@StudentId,@PayType,@PayMoney,@Mark,@RecDateTime,@ResultCode,@ResultMessage)";
                DbCommand cmd = dbHelper.GetSqlStringCommond(buffer.ToString());
                //dbHelper.AddInParameter(cmd, "@SynedRecId", DbType.Int32, record.RecId);
                //dbHelper.AddInParameter(cmd, "@SchoolId", DbType.String, record.SchoolId);
                //dbHelper.AddInParameter(cmd, "@PayType", DbType.Int32, record.PayType);
                //dbHelper.AddInParameter(cmd, "@StudentId", DbType.Int32, record.StudentId);
                //dbHelper.AddInParameter(cmd, "@PayMoney", DbType.Decimal, record.PayMoney);
                //dbHelper.AddInParameter(cmd, "@Mark", DbType.String, record.Mark);
                //dbHelper.AddInParameter(cmd, "@RecDatetime", DbType.String, record.RecDatetime);
                //dbHelper.AddInParameter(cmd, "@ResultCode", DbType.String, resultCode);
                //dbHelper.AddInParameter(cmd, "@ResultMessage", DbType.String, resultMessage);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }
        #endregion

        #region 根据人员编号查找对应的人员照片
        public static PhotoOfEmp GetPhotoOfEmpByEmpId(int empid)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                PhotoOfEmp photo = new PhotoOfEmp();
                photo.EmpId = empid;
                string sql = string.Format("Select Photo From EmpInfo where EmpId ={0}", empid);
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                foreach (DataRow row in dt.Rows)
                {
                    photo.ArrayOfPhoto = row["Photo"].ToString().Length >= 10 ? null : (byte[])row["Photo"];
                }
                return photo;
            }
        }
        #endregion

        #region 获取抓拍记录报表
        public static DataTable GetRecordReport_Capture(int deptId, int deptType, int deviceId, string empCode, string empName, string CardNo, string BeginDate, string EndDate)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "ReportOfRecord_Capture";
                DbCommand cmd = dbHelper.GetStoredProcCommond(sql);
                dbHelper.AddInParameter(cmd, "@DeptId", DbType.Int32, deptId);
                dbHelper.AddInParameter(cmd, "@DeptType", DbType.Int32, deptType);
                dbHelper.AddInParameter(cmd, "@DeviceId", DbType.Int32, deviceId);
                dbHelper.AddInParameter(cmd, "@EmpCode", DbType.String, empCode);
                dbHelper.AddInParameter(cmd, "@EmpName", DbType.String, empName);
                dbHelper.AddInParameter(cmd, "@CardNo", DbType.String, CardNo);
                dbHelper.AddInParameter(cmd, "@BeginDate", DbType.String, BeginDate);
                dbHelper.AddInParameter(cmd, "@EndDate", DbType.String, EndDate);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                return dt;
            }
        }
        #endregion

        #region 获取当前记录最大ID
        public static UInt64 GetCurrentRecordIndex()
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                UInt64 index = 0;
                string sql = "Select IsNull(Max(RecId),1) as RecordIndex From Record";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                foreach (DataRow row in dt.Rows)
                {
                    index = Convert.ToUInt64(row["RecordIndex"]);
                }
                return index;
            }
        }
        #endregion

        #region 获取实时采集记录
        public static List<DisplayRecord> GetRealtimeRecords(UInt64 currentRecordIndex)
        {
            List<DisplayRecord> recordList = new List<DisplayRecord>();
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = string.Format("Select * From (Select b.DeviceName,a.* from Record a,DeviceInfo b where a.DeviceId = b.DeviceId and RecId >{0} ) d Left Join ", currentRecordIndex);
                sql += "(Select a.DeptId, a.DeptName, b.EmpId, b.EmpName, b.EmpCode,b.Photo,c.CardId, c.CardNo from DeptInfo a, EmpInfo b, CardInfo c ";
                sql += "where a.DeptId = b.DeptId and b.EmpId = c.EmpId and c.CardStatus = 1) e On d.CardNo = e.CardNo Order by d.RecId asc";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                foreach (DataRow row in dt.Rows)
                {
                    DisplayRecord record = new DisplayRecord();
                    record.RecId = Convert.ToUInt64(row["RecId"]);
                    record.DeviceId = Convert.ToInt32(row["DeviceId"]);
                    record.DeviceName = row["DeviceName"].ToString();
                    record.DeptId = Convert.ToInt32(row["DeptId"].ToString().Equals(string.Empty) ? "0" : row["deptId"]);
                    record.DeptName = row["DeptName"].ToString().Equals(string.Empty) ? "无" : row["DeptName"].ToString();
                    record.CardNo = row["CardNo"].ToString();
                    record.EmpId = Convert.ToInt32(row["EmpId"].ToString().Equals(string.Empty) ? "0" : row["EmpId"]);
                    record.EmpCode = row["EmpCode"].ToString().Equals(string.Empty) ? "无" : row["EmpCode"].ToString();
                    record.EmpName = row["EmpName"].ToString().Equals(string.Empty) ? "无" : row["EmpName"].ToString();
                    record.CardId = Convert.ToInt32(row["CardId"].ToString().Equals(string.Empty) ? "0" : row["CardId"]);
                    record.CurrentIndex = Convert.ToInt32(row["RecPointer"]);
                    record.IOFlag = row["IOFlag"].ToString();
                    record.RecDatetime = row["RecDateTime"].ToString();
                    record.RecordType = row["RecordType"].ToString();
                    byte[] array = row["Photo"].ToString().Equals(string.Empty) ? null : (byte[])row["Photo"];
                    record.PhotoArray = array == null ? null : array.Length <= 10 ? null : array;
                    recordList.Add(record);
                }
            }
            return recordList;
        }
        #endregion





        #region 加载卡类列表
        public static DataTable GetTicketType()
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "Select a.RecId,a.Name from TicketType a";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                return dt;
            }
        }
        #endregion



        #region 添加卡类信息

        public static void AddCardType(TicketType cardType)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "INSERT INTO TicketType(Name,TypeId,BlackName,CardType,InRight,OutRight,VoiceNo,Photo,VacationId,";
                sql += "IntimeGroupNo,OutTimeGroupNo,AntiSubmarine,LimitEnabled,TimegroupLimitEnabled,";
                sql += "LimitTypeOfTimeGroupLimit,TimesOfTimeGroupLimit,EffectDateLimitEnabled,LimitTypeOfEffectDateLimit,TimesOfEffectDateLimit,LimitTimeEnabled,MinutesOfLimitTime,";
                sql += "DisplayType1,Text1,Column1,Content1,DisplayType2,Text2,Column2,Content2,DisplayType3,Text3,Column3,Content3)";
                sql += $" VALUES ('{cardType.Name}',{cardType.TypeId},{cardType.BlackName},{cardType.CardType},{cardType.InRight},{cardType.OutRight},{cardType.VoiceNo},{cardType.Photo},{cardType.VacationId},";
                sql += $"{cardType.IntimeGroupNo},{cardType.OutTimeGroupNo},{cardType.AntiSubmarine},{cardType.LimitEnabled},{cardType.TimegroupLimitEnabled},";
                sql += $"{cardType.LimitTypeOfTimeGroupLimit},{cardType.TimesOfTimeGroupLimit},{cardType.EffectDateLimitEnabled},{cardType.LimitTypeOfEffectDateLimit},{cardType.TimesOfEffectDateLimit},{cardType.LimitTimeEnabled},{cardType.MinutesOfLimitTime},";
                sql += $"{cardType.DisplayType1},'{cardType.Text1}',{cardType.Column1},'{cardType.Content1}',{cardType.DisplayType2},'{cardType.Text2}',{cardType.Column2},'{cardType.Content2}',{cardType.DisplayType3},'{cardType.Text3}',{cardType.Column3},'{cardType.Content3}')";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }

        #endregion


        #region 检查卡号在闸机上是否有权限
        public static bool CheckRight(int empId, int devId)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = $"Select top 1 * From DevRightOfEmp Where EmpId ={empId} and DeviceId ={devId} And Rights = 1";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                if (dt.Rows.Count > 0)
                    return true;
                return false;
            }
        }
        #endregion



        #region 添加卡
        public static void AddCard(int empId, CardInfo card)
        {
            TicketType attribute = GetAttributeOfTypeById(card.TicketType);
            if (attribute == null) throw new Exception("票类不能为空!");
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {

                DbCommand cmd = dbHelper.GetStoredProcCommond("AddCardInfo");
                dbHelper.AddInParameter(cmd, "@EmpId", DbType.String, empId);
                dbHelper.AddInParameter(cmd, "@TicketType", DbType.Int32, attribute.RecId);
                dbHelper.AddInParameter(cmd, "@Type", DbType.Int32, card.Type);
                dbHelper.AddInParameter(cmd, "@CardNo", DbType.String, card.CardNo);
                dbHelper.AddInParameter(cmd, "@BlackName", DbType.Int32, card.BlackName);
                dbHelper.AddInParameter(cmd, "@CardType", DbType.Int32, attribute.CardType);
                dbHelper.AddInParameter(cmd, "@CardCode", DbType.String, empId.ToString());
                dbHelper.AddInParameter(cmd, "@InRight", DbType.Int32, attribute.InRight);
                dbHelper.AddInParameter(cmd, "@OutRight", DbType.Int32, attribute.OutRight);
                dbHelper.AddInParameter(cmd, "@VoiceNo", DbType.Int32, attribute.VoiceNo);
                dbHelper.AddInParameter(cmd, "@Photo", DbType.Int32, attribute.Photo);
                dbHelper.AddInParameter(cmd, "@VacationId", DbType.Int32, attribute.VacationId);
                dbHelper.AddInParameter(cmd, "@InTimeGroupNo", DbType.Int32, attribute.IntimeGroupNo);
                dbHelper.AddInParameter(cmd, "@OutTimeGroupNo", DbType.Int32, attribute.OutTimeGroupNo);
                dbHelper.AddInParameter(cmd, "@BeginDate", DbType.String, card.BeginDate);
                dbHelper.AddInParameter(cmd, "@EndDate", DbType.String, card.EndDate);
                dbHelper.AddInParameter(cmd, "@DisplayType1", DbType.Int32, attribute.DisplayType1);
                dbHelper.AddInParameter(cmd, "@Text1", DbType.String, attribute.Text1);
                dbHelper.AddInParameter(cmd, "@Column1", DbType.Int32, attribute.Column1);
                dbHelper.AddInParameter(cmd, "@Content1", DbType.String, attribute.Content1);
                dbHelper.AddInParameter(cmd, "@DisplayType2", DbType.Int32, attribute.DisplayType2);
                dbHelper.AddInParameter(cmd, "@Text2", DbType.String, attribute.Text2);
                dbHelper.AddInParameter(cmd, "@Column2", DbType.Int32, attribute.Column2);
                dbHelper.AddInParameter(cmd, "@Content2", DbType.String, attribute.Content2);
                dbHelper.AddInParameter(cmd, "@DisplayType3", DbType.Int32, attribute.DisplayType3);
                dbHelper.AddInParameter(cmd, "@Text3", DbType.String, attribute.Text3);
                dbHelper.AddInParameter(cmd, "@Column3", DbType.Int32, attribute.Column3);
                dbHelper.AddInParameter(cmd, "@Content3", DbType.String, attribute.Content3);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }

        #endregion

        #region 添加卡
        public static void AddCardNew(int empId, CardInfo card)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {

                DbCommand cmd = dbHelper.GetStoredProcCommond("AddCardNew");
                dbHelper.AddInParameter(cmd, "@EmpId", DbType.String, empId);
                dbHelper.AddInParameter(cmd, "@TicketType", DbType.Int32, card.TicketType);
                dbHelper.AddInParameter(cmd, "@Type", DbType.Int32, card.Type);
                dbHelper.AddInParameter(cmd, "@CardNo", DbType.String, card.CardNo);
                dbHelper.AddInParameter(cmd, "@BeginDate", DbType.String, card.BeginDate);
                dbHelper.AddInParameter(cmd, "@EndDate", DbType.String, card.EndDate);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }

        #endregion

        #region 根据编号获取票类信息
        private static TicketType GetAttributeOfTypeById(int recId)
        {
            TicketType cardType = null;
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = $"Select * from TicketType where RecId ={recId}";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                foreach (DataRow row in dt.Rows)
                {
                    cardType = new TicketType();
                    cardType.RecId = recId;
                    cardType.Name = row["Name"].ToString();
                    cardType.TypeId = Convert.ToInt32(row["TypeId"]);
                    cardType.InRight = Convert.ToInt32(row["InRight"]);
                    cardType.OutRight = Convert.ToInt32(row["OutRight"]);
                    cardType.Photo = Convert.ToInt32(row["Photo"]);
                    cardType.VacationId = Convert.ToInt32(row["VacationId"]);
                    cardType.VoiceNo = Convert.ToInt32(row["VoiceNo"]);
                    cardType.IntimeGroupNo = Convert.ToInt32(row["IntimeGroupNo"]);
                    cardType.OutTimeGroupNo = Convert.ToInt32(row["OutTimeGroupNo"]);
                    cardType.AntiSubmarine = Convert.ToInt32(row["AntiSubmarine"]);
                    cardType.LimitEnabled = Convert.ToInt32(row["LimitEnabled"]);
                    cardType.TimegroupLimitEnabled = Convert.ToInt32(row["TimegroupLimitEnabled"]);
                    cardType.LimitTypeOfTimeGroupLimit = Convert.ToInt32(row["LimitTypeOfTimeGroupLimit"]);
                    cardType.TimesOfTimeGroupLimit = Convert.ToInt32(row["TimesOfTimeGroupLimit"]);
                    cardType.EffectDateLimitEnabled = Convert.ToInt32(row["EffectDateLimitEnabled"]);
                    cardType.LimitTypeOfEffectDateLimit = Convert.ToInt32(row["LimitTypeOfEffectDateLimit"]);
                    cardType.TimesOfEffectDateLimit = Convert.ToInt32(row["TimesOfEffectDateLimit"]);
                    cardType.LimitTimeEnabled = Convert.ToInt32(row["LimitTimeEnabled"]);
                    cardType.MinutesOfLimitTime = Convert.ToInt32(row["MinutesOfLimitTime"]);
                    cardType.DisplayType1 = Convert.ToInt32(row["DisplayType1"]);
                    cardType.Text1 = row["Text1"].ToString();
                    cardType.Column1 = Convert.ToInt32(row["Column1"]);
                    cardType.Content1 = row["Content1"].ToString();
                    cardType.DisplayType2 = Convert.ToInt32(row["DisplayType2"]);
                    cardType.Text2 = row["Text2"].ToString();
                    cardType.Column2 = Convert.ToInt32(row["Column2"]);
                    cardType.Content2 = row["Content2"].ToString();
                    cardType.DisplayType3 = Convert.ToInt32(row["DisplayType3"]);
                    cardType.Text3 = row["Text3"].ToString();
                    cardType.Column3 = Convert.ToInt32(row["Column3"]);
                    cardType.Content3 = row["Content3"].ToString();
                }
            }
            return cardType;
        }
        #endregion

        #region 添加指纹
        public static void AddFingerPrint(int currentEmpId, FingerPrint finger)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "AddFingerPrint";
                DbCommand cmd = dbHelper.GetStoredProcCommond(sql);
                dbHelper.AddInParameter(cmd, "@EmpId", DbType.Int32, currentEmpId);
                dbHelper.AddInParameter(cmd, "@FingerId", DbType.Int32, finger.FingerId);
                dbHelper.AddInParameter(cmd, "@PositionId", DbType.Int32, finger.PositionId);
                dbHelper.AddInParameter(cmd, "@FingerData", DbType.Binary, finger.FingerData);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }
        #endregion



        #region 获取人员指纹信息
        public static List<FingerPrint> GetAllFingerPrintsByEmpId(int empId)
        {
            List<FingerPrint> fingerList = new List<FingerPrint>();
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = $"Select * from FingerPrint Where EmpId ={empId}";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        FingerPrint finger = new FingerPrint();
                        finger.EmpId = Convert.ToInt32(row["EmpId"]);
                        finger.FingerId = Convert.ToInt32(row["FingerId"]);
                        finger.PositionId = Convert.ToInt32(row["PositionId"]);
                        finger.FingerData = (byte[])(row["FingerData"]);
                        fingerList.Add(finger);
                    }
                }
            }
            return fingerList;
        }
        #endregion




        #region 检查指纹是否已经存在
        public static bool CheckFingerPrintExists(int fingerId)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "CheckFingerPrintExists";
                DbCommand cmd = dbHelper.GetStoredProcCommond(sql);
                dbHelper.AddInParameter(cmd, "@FingerId", DbType.Int32, fingerId);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                if (dt.Rows.Count == 0) return false;
                return true;
            }
        }
        #endregion

        #region 通过指纹查找人员
        public static EmpInfo GetEmpInfoByFingerId(int fingerId)
        {
            EmpInfo emp = null;
            try
            {
                using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
                {
                    string sql = "GetEmpInfoByFingerId";
                    DbCommand cmd = dbHelper.GetStoredProcCommond(sql);
                    dbHelper.AddInParameter(cmd, "@FingerId", DbType.Int32, fingerId);
                    DataTable dt = dbHelper.ExecuteDataTable(cmd);
                    foreach (DataRow row in dt.Rows)
                    {
                        emp = new EmpInfo();
                        emp.DeptId = Convert.ToInt32(row["DeptId"]);
                        emp.EmpId = Convert.ToInt32(row["EmpId"]);
                        emp.EmpCode = row["EmpCode"].ToString();
                        emp.EmpName = row["EmpName"].ToString();
                        emp.EnglishName = row["EnglishName"].ToString();
                        emp.Sex = row["Sex"].ToString();
                        emp.IdentityCard = row["IdentityCard"].ToString();
                        emp.Telephone = row["Telephone"].ToString();
                        emp.BirthDay = row["BirthDay"].ToString();
                        emp.Nation = row["Nationality"].ToString();
                        emp.BornEarth = row["BornEarth"].ToString();
                        emp.Marrige = row["Marrige"].ToString();
                        emp.JoinDate = row["JoinDate"].ToString();
                        emp.Photo = row["Photo"].ToString().Equals(string.Empty) ? null : GetPhotoFromArray((byte[])row["Photo"]);
                        emp.TicketType = Convert.ToInt32(row["TicketType"]);
                        emp.BeginDate = row["BeginDate"].ToString();
                        emp.EndDate = row["EndDate"].ToString();
                        emp.ICCardId = Convert.ToInt32(row["ICCardId"]);
                        emp.ICCardNo = row["ICCardNo"].ToString();
                        emp.IDSerialId = Convert.ToInt32(row["IDSerialId"]);
                        emp.IDSerialNo = row["IDSerialNo"].ToString();
                        emp.IDCardId = Convert.ToInt32(row["IDCardId"]);
                        emp.IDCardNo = row["IDCardNo"].ToString();
                        emp.FingerId1 = Convert.ToInt32(row["FingerId1"]);
                        emp.FingerData1 = (byte[])row["FingerData1"];
                        emp.FingerId2 = Convert.ToInt32(row["FingerId2"]);
                        emp.FingerData2 = (byte[])row["FingerData2"];
                        emp.FingerId3 = Convert.ToInt32(row["FingerId3"]);
                        emp.FingerData3 = (byte[])row["FingerData3"];
                    }
                }
            }
            catch
            {

            }
            return emp;
        }
        #endregion



        #region   数组转图片
        public static Bitmap GetPhotoFromArray(byte[] arr)
        {
            if (arr.Length < 100)
            {
                return null;
            }
            MemoryStream ms = new MemoryStream(arr);
            //Image img = Image.FromStream(ms, true);//在这里出错  
            Image img = Image.FromStream(ms);//在这里出错  
                                             //流用完要及时关闭  
            ms.Close();
            Bitmap bmp = new Bitmap(img);
            return bmp;
        }
        #endregion

        #region 设备树
        public static DataTable GetDeviceTreeTable()
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = $"select placeID as id,placeName as name,parPlaceid as parid,imageindex as imageindex from deviceplace Union select deviceid as id,devicename as name,placeid as parid,imageindex as imageindex from deviceinfo";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                return dt;
            }
        }
        #endregion

        #region 更改考勤模式
        public static void ChangeAttendModel(int attendModel)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = string.Empty;
                switch (attendModel)
                {
                    case 0:
                        sql += $"{Environment.NewLine}Update Menus Set EnableFlag = 1 where MenuId >=61 and MenuId <=66";
                        sql += $"{Environment.NewLine}Update Menus Set EnableFlag = 0 where MenuId = 67 or MenuId = 68 or MenuId = 69 or MenuId = 610";
                        break;
                    case 1:
                        sql += $"{Environment.NewLine}Update Menus Set EnableFlag = 0 where MenuId >=61 and MenuId <=66";
                        sql += $"{Environment.NewLine}Update Menus Set EnableFlag = 1 where MenuId = 67 or MenuId = 68 or MenuId = 69 or MenuId = 610";
                        break;
                }
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }
        #endregion

        #region 加载当天二维码
        public static DataTable GetBarcodesToday()
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = $"Select RecId,BarcodeNo,EffectTime,CreateTime from Barcode Where CreateTime >='{DateTime.Now.ToString("yyyy-MM-dd")}'";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                return dt;
            }
        }
        #endregion

        #region 检查二维码是否存在
        public static bool CheckBarcodeExists(string barcode)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = $"Select * from Barcode where BarcodeNo ='{barcode}'";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                if (dt.Rows.Count == 1)
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

        #region 添加二维码
        public static void AddBarcode(string _CurrentBarcode, List<int> devList, int barcodeEffectTime, int timesOfIn, int timesOfOut)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                StringBuilder buffer = new StringBuilder();
                for (int i = 0; i < devList.Count; i++)
                {
                    buffer.Append(devList[i].ToString());
                    if (i < devList.Count - 1)
                        buffer.Append(",");
                }
                string recTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string outOfTime = DateTime.Now.AddMinutes(barcodeEffectTime).ToString("yyyy-MM-dd HH:mm:ss");
                string sql = $"{Environment.NewLine}If Not Exists(Select top 1 * From Barcode Where BarcodeNo ='{_CurrentBarcode}' )";
                sql += $"{Environment.NewLine}Insert Barcode(BarcodeNo,DevList,EffectTime,TimesOfIn,TimesOfInLeft,TimesOfOut,TimesOfOutLeft,CreateTime,OutOfTime)";
                sql += $"{Environment.NewLine}  Values('{_CurrentBarcode}','{buffer.ToString()}',{barcodeEffectTime},{timesOfIn},{0},{timesOfOut},{0},'{recTime}','{outOfTime}')";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }
        #endregion

        #region 获取条码信息
        public static Barcode GetBarcodeByBarcodeNo(string barcodeNo)
        {
            Barcode barcode = null;
            try
            {
                using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
                {
                    string sql = $"Select Top 1 * From Barcode Where BarcodeNo ='{barcodeNo}'";
                    DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                    DataTable dt = dbHelper.ExecuteDataTable(cmd);
                    foreach (DataRow row in dt.Rows)
                    {
                        barcode = new Barcode();
                        barcode.RecId = Convert.ToInt32(row["RecId"]);
                        barcode.BarcodeNo = row["BarcodeNo"].ToString();
                        barcode.EffectTime = Convert.ToInt32(row["EffectTime"]);
                        barcode.TimesOfIn = Convert.ToInt32(row["TimesOfIn"]);
                        barcode.TimesOfInLeft = Convert.ToInt32(row["TimesOfInLeft"]);
                        barcode.TimesOfOut = Convert.ToInt32(row["TimesOfOut"]);
                        barcode.TimesOfOutLeft = Convert.ToInt32(row["TimesOfOutLeft"]);
                        barcode.CreateTime = row["CreateTime"].ToString();
                        barcode.OutOfTime = row["OutOfTime"].ToString();
                    }
                }
            }
            catch
            {

            }
            return barcode;
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


        #region 对接镇远生活

        #region 添加数据上传任务
        public static void AddDataUploadTask(string oper, int taskType, string empCode, string empName, string _ICCardNo, string iDCardNo, string telephone, string englishName, string deptName)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = $"{Environment.NewLine}Insert Into DataUploadTask(Oper,TaskType,EmpCode,EmpName,CardNo,IDCard,Phone,SchoolCode,DeptName,CreateTime)";
                sql += $"{Environment.NewLine}Values ('{oper}',{taskType},'{empCode}','{empName}','{_ICCardNo}','{iDCardNo}','{telephone}','{englishName}','{deptName}','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}')";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }
        #endregion

        #region 获取未同步的任务列表
        public static List<ZYTDataUpload> GetUnSynedZYTDataUploadList()
        {
            List<ZYTDataUpload> taskList = new List<ZYTDataUpload>();
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = $"{Environment.NewLine} Select * From DataUploadTask Where UpdateFlag = 0";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                foreach (DataRow row in dt.Rows)
                {
                    ZYTDataUpload data = new ZYTDataUpload();
                    data.RecId = Convert.ToInt32(row["RecId"]);
                    data.Oper = row["Oper"].ToString();
                    data.TaskType = Convert.ToInt32(row["TaskType"]);
                    data.EmpCode = row["EmpCode"].ToString();
                    data.EmpName = row["EmpName"].ToString();
                    data.CardNo = row["CardNo"].ToString();
                    data.IDCard = row["IDCard"].ToString();
                    data.Phone = row["Phone"].ToString();
                    data.SchoolCode = row["SchoolCode"].ToString();
                    data.DeptName = row["DeptName"].ToString();
                    data.UpdateFlag = Convert.ToInt32(row["UpdateFlag"]);
                    data.UploadTime = row["UploadTime"].ToString();
                    taskList.Add(data);
                }
            }
            return taskList;
        }
        #endregion

        #region 任务完成后改变任务的状态
        public static void ChangeZYTTaskStatus(int recId)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = $"{Environment.NewLine}Update DataUploadTask Set UpdateFlag = 1,UploadTime ='{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}'  where RecId ={recId}";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }

        #endregion

        #region 添加图片上传任务
        public static void AddUploadImageTask(string empCode, string recTime, string sJpegPicFileName)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = $"{Environment.NewLine}Insert Into UploadImageTask(EmpCode,RecDatetime,FilePath) values('{empCode}','{recTime}','{sJpegPicFileName}')";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }
        #endregion

        #region 获取未同步的图片上传任务
        public static List<ZYTImageUpload> GetUnSynedZYTImageUploadList()
        {
            List<ZYTImageUpload> taskList = new List<ZYTImageUpload>();
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = $"{Environment.NewLine} Select * From UploadImageTask Where UpdateFlag = 0";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                foreach (DataRow row in dt.Rows)
                {
                    ZYTImageUpload data = new ZYTImageUpload();
                    data.RecId = Convert.ToInt32(row["RecId"]);
                    data.EmpCode = row["EmpCode"].ToString();
                    data.RecDatetime = row["RecDatetime"].ToString();
                    data.FilePath = row["FilePath"].ToString();
                    taskList.Add(data);
                }
            }
            return taskList;
        }
        #endregion

        #region 改变图片上传任务的状态
        public static void ChangeZYTImageTaskStatus(int recId)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = $"{Environment.NewLine}Update UploadImageTask Set UpdateFlag = 1  where RecId ={recId}";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }
        #endregion

        #endregion

        #region 获取卡类信息
        public static TicketType GetTicketTypeByCardNo(string cardNo)
        {
            TicketType ticketTyp = null;
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = $"Select * from TicketType Where RecId = (Select top 1 TicketType From CardInfo where CardStatus = 1 and CardNo ='{cardNo}')";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                foreach (DataRow row in dt.Rows)
                {
                    ticketTyp = new TicketType();
                    ticketTyp.RecId = Convert.ToInt32(row["RecId"]);
                    ticketTyp.Name = row["Name"].ToString();
                    ticketTyp.Text1 = row["Text1"].ToString();
                    ticketTyp.Column1 = Convert.ToInt32(row["Column1"]);
                    ticketTyp.Content1 = row["Content1"].ToString();
                    ticketTyp.DisplayType2 = Convert.ToInt32(row["DisplayType2"]);
                    ticketTyp.Text2 = row["Text2"].ToString();
                    ticketTyp.Column2 = Convert.ToInt32(row["Column2"]);
                    ticketTyp.Content2 = row["Content2"].ToString();
                    ticketTyp.DisplayType3 = Convert.ToInt32(row["DisplayType3"]);
                    ticketTyp.Text3 = row["Text3"].ToString();
                    ticketTyp.Column3 = Convert.ToInt32(row["Column3"]);
                    ticketTyp.Content3 = row["Content3"].ToString();
                    ticketTyp.TypeId = Convert.ToInt32(row["TypeId"]);
                    ticketTyp.BlackName = Convert.ToInt32(row["BlackName"]);
                    ticketTyp.CardType = Convert.ToInt32(row["CardType"]);
                    ticketTyp.InRight = Convert.ToInt32(row["InRight"]);
                    ticketTyp.OutRight = Convert.ToInt32(row["OutRight"]);
                    ticketTyp.VoiceNo = Convert.ToInt32(row["VoiceNo"]);
                    ticketTyp.Photo = Convert.ToInt32(row["Photo"]);
                    ticketTyp.VacationId = Convert.ToInt32(row["VacationId"]);
                    ticketTyp.IntimeGroupNo = Convert.ToInt32(row["IntimeGroupNo"]);
                    ticketTyp.OutTimeGroupNo = Convert.ToInt32(row["OutTimeGroupNo"]);
                    ticketTyp.LimitEnabled = Convert.ToInt32(row["LimitEnabled"]);
                    ticketTyp.TimegroupLimitEnabled = Convert.ToInt32(row["TimegroupLimitEnabled"]);
                    ticketTyp.LimitTypeOfTimeGroupLimit = Convert.ToInt32(row["LimitTypeOfTimeGroupLimit"]);
                    ticketTyp.TimesOfTimeGroupLimit = Convert.ToInt32(row["TimesOfTimeGroupLimit"]);
                    ticketTyp.EffectDateLimitEnabled = Convert.ToInt32(row["EffectDateLimitEnabled"]);
                    ticketTyp.LimitTypeOfEffectDateLimit = Convert.ToInt32(row["LimitTypeOfEffectDateLimit"]);
                    ticketTyp.TimesOfEffectDateLimit = Convert.ToInt32(row["TimesOfEffectDateLimit"]);
                    ticketTyp.LimitTimeEnabled = Convert.ToInt32(row["LimitTimeEnabled"]);
                    ticketTyp.MinutesOfLimitTime = Convert.ToInt32(row["MinutesOfLimitTime"]);
                    ticketTyp.DisplayType1 = Convert.ToInt32(row["DisplayType1"]);
                    ticketTyp.AntiSubmarine = Convert.ToInt32(row["AntiSubmarine"]);
                }
            }
            return ticketTyp;
        }
        #endregion

        #region 获取卡类信息
        public static TicketType GetTicketTypeByRecId(int recId)
        {
            TicketType ticketTyp = null;
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = $"Select * from TicketType Where RecId ={recId}";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                foreach (DataRow row in dt.Rows)
                {
                    ticketTyp = new TicketType();
                    ticketTyp.RecId = Convert.ToInt32(row["RecId"]);
                    ticketTyp.Name = row["Name"].ToString();
                    ticketTyp.Text1 = row["Text1"].ToString();
                    ticketTyp.Column1 = Convert.ToInt32(row["Column1"]);
                    ticketTyp.Content1 = row["Content1"].ToString();
                    ticketTyp.DisplayType2 = Convert.ToInt32(row["DisplayType2"]);
                    ticketTyp.Text2 = row["Text2"].ToString();
                    ticketTyp.Column2 = Convert.ToInt32(row["Column2"]);
                    ticketTyp.Content2 = row["Content2"].ToString();
                    ticketTyp.DisplayType3 = Convert.ToInt32(row["DisplayType3"]);
                    ticketTyp.Text3 = row["Text3"].ToString();
                    ticketTyp.Column3 = Convert.ToInt32(row["Column3"]);
                    ticketTyp.Content3 = row["Content3"].ToString();
                    ticketTyp.TypeId = Convert.ToInt32(row["TypeId"]);
                    ticketTyp.BlackName = Convert.ToInt32(row["BlackName"]);
                    ticketTyp.CardType = Convert.ToInt32(row["CardType"]);
                    ticketTyp.InRight = Convert.ToInt32(row["InRight"]);
                    ticketTyp.OutRight = Convert.ToInt32(row["OutRight"]);
                    ticketTyp.VoiceNo = Convert.ToInt32(row["VoiceNo"]);
                    ticketTyp.Photo = Convert.ToInt32(row["Photo"]);
                    ticketTyp.VacationId = Convert.ToInt32(row["VacationId"]);
                    ticketTyp.IntimeGroupNo = Convert.ToInt32(row["IntimeGroupNo"]);
                    ticketTyp.OutTimeGroupNo = Convert.ToInt32(row["OutTimeGroupNo"]);
                    ticketTyp.LimitEnabled = Convert.ToInt32(row["LimitEnabled"]);
                    ticketTyp.TimegroupLimitEnabled = Convert.ToInt32(row["TimegroupLimitEnabled"]);
                    ticketTyp.LimitTypeOfTimeGroupLimit = Convert.ToInt32(row["LimitTypeOfTimeGroupLimit"]);
                    ticketTyp.TimesOfTimeGroupLimit = Convert.ToInt32(row["TimesOfTimeGroupLimit"]);
                    ticketTyp.EffectDateLimitEnabled = Convert.ToInt32(row["EffectDateLimitEnabled"]);
                    ticketTyp.LimitTypeOfEffectDateLimit = Convert.ToInt32(row["LimitTypeOfEffectDateLimit"]);
                    ticketTyp.TimesOfEffectDateLimit = Convert.ToInt32(row["TimesOfEffectDateLimit"]);
                    ticketTyp.LimitTimeEnabled = Convert.ToInt32(row["LimitTimeEnabled"]);
                    ticketTyp.MinutesOfLimitTime = Convert.ToInt32(row["MinutesOfLimitTime"]);
                    ticketTyp.DisplayType1 = Convert.ToInt32(row["DisplayType1"]);
                    ticketTyp.AntiSubmarine = Convert.ToInt32(row["AntiSubmarine"]);
                }
            }
            return ticketTyp;
        }
        #endregion

        /*

        #region 获取时间段内通行次数
        public static int GetTimesOfTimegroup(int ioFlag, int timegroupNo, string cardNo)
        {
            int times = 10000;
            TimeGroup timeGroup = GetTimegroupDuring(timegroupNo);
            if (timeGroup == null) return times;
            string beginDate = $"{DateTime.Now.ToString("yyyy-MM-dd")} {timeGroup.SBeginTime}";
            string endDate = $"{DateTime.Now.ToString("yyyy-MM-dd")} {timeGroup.SEndTime}";
            string io = ioFlag == 3 ? "进" : "出";
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = $"Select Count(*) As Times from Records Where IOFlag ='{io}' And RecordType ='有效票' And  CardNo ='{cardNo}' and RecDateTime >='{beginDate}' And RecDatetime <='{endDate}' ";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                foreach (DataRow row in dt.Rows)
                {
                    times = Convert.ToInt32(row["Times"]);
                }
            }
            return times;
        }

        #endregion

    */

        #region 获取有效期内通行次数
        public static int GetTimesOfEffectDate(int ioFlag, string cardNo, string beginDate, string endDate)
        {
            int times = 0;
            string io = ioFlag == 3 ? "进" : "出";
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = $"Select Count(*) As Times from Records Where IOFlag ='{io}' And RecordType ='有效票' And  CardNo ='{cardNo}' and RecDateTime >='{beginDate}' And RecDatetime <='{endDate}' ";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                foreach (DataRow row in dt.Rows)
                {
                    times = Convert.ToInt32(row["Times"]);
                }
            }
            return times;
        }

        #endregion

        /*
        #region 获取时段内上次通行时间
        public static string GetLastTimeOfTimegroup(int timeGroupNo, string cardNo)
        {
            string lastTime = string.Empty;
            TimeGroup timeGroup = GetTimegroupDuring(timeGroupNo);
            if (timeGroup == null) return lastTime;
            string beginDate = $"{DateTime.Now.ToString("yyyy-MM-dd")} {timeGroup.SBeginTime}";
            string endDate = $"{DateTime.Now.ToString("yyyy-MM-dd")} {timeGroup.SEndTime}";
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = $"Select top 1 RecDateTime As LastTime from Records Where IOFlag='进' And  RecordType ='有效票' And  CardNo ='{cardNo}' and RecDateTime >='{beginDate}' And RecDatetime <='{endDate}' order By RecDatetime ";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                foreach (DataRow row in dt.Rows)
                {
                    lastTime = row["LastTime"].ToString();
                }
            }
            return lastTime;
        }

        #endregion
            */
        /*
        #region 获取当前时间包含的的时间段
        private static TimeGroup GetTimegroupDuring(int timegroupNo)
        {
            List<List<TimeGroup>> timeGroup = GetTimeGroupList(timegroupNo);
            if (timeGroup == null) return null;
            int week = (int)DateTime.Now.DayOfWeek;
            List<TimeGroup> groupList = timeGroup[week];
            foreach (TimeGroup group in groupList)
            {
                Array.Reverse(group.BeginTime);
                Array.Reverse(group.EndTime);
                UInt16 begin = BitConverter.ToUInt16(group.BeginTime, 0);
                UInt16 end = BitConverter.ToUInt16(group.EndTime, 0);
                if (begin == 0 && end == 0)
                    continue;
                byte[] array = ArrayHelper.HexToArray(DateTime.Now.ToString("HHmm"), 2);
                Array.Reverse(array);
                UInt16 now = BitConverter.ToUInt16(array, 0);
                if (begin <= now && now <= end)
                    return group;
            }
            return null;
        }
        #endregion
    */
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

        #region 根据IC卡号获取人员信息
        public static EmpInfo GetEmpInfoByICCardNo(string cardNo)
        {
            EmpInfo emp = null;
            try
            {
                using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
                {
                    string sql = $"Select a.*,b.DeptId,b.DeptName From EmpInfo a,DeptInfo b Where a.DeptId = b.DeptId and a.ICCardNo='{cardNo}'";
                    DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                    DataTable dt = dbHelper.ExecuteDataTable(cmd);
                    foreach (DataRow row in dt.Rows)
                    {
                        emp = new EmpInfo();
                        emp.DeptId = Convert.ToInt32(row["DeptId"]);
                        emp.EmpId = Convert.ToInt32(row["EmpId"]);
                        emp.EmpCode = row["EmpCode"].ToString();
                        emp.EmpName = row["EmpName"].ToString();
                        emp.IOFlag = Convert.ToInt32(row["IOFlag"]);
                        emp.EnglishName = row["EnglishName"].ToString();
                        emp.Sex = row["Sex"].ToString();
                        emp.IdentityCard = row["IdentityCard"].ToString();
                        emp.Telephone = row["Telephone"].ToString();
                        emp.BirthDay = row["BirthDay"].ToString();
                        emp.Nation = row["Nationality"].ToString();
                        emp.BornEarth = row["BornEarth"].ToString();
                        emp.Marrige = row["Marrige"].ToString();
                        emp.JoinDate = row["JoinDate"].ToString();
                        emp.Photo = row["Photo"].ToString().Equals(string.Empty) ? null : GetPhotoFromArray((byte[])row["Photo"]);
                        emp.TicketType = Convert.ToInt32(row["TicketType"]);
                        emp.BeginDate = row["BeginDate"].ToString();
                        emp.EndDate = row["EndDate"].ToString();
                        emp.ICCardId = Convert.ToInt32(row["ICCardId"]);
                        emp.ICCardNo = row["ICCardNo"].ToString();
                        emp.IDSerialId = Convert.ToInt32(row["IDSerialId"]);
                        emp.IDSerialNo = row["IDSerialNo"].ToString();
                        emp.IDCardId = Convert.ToInt32(row["IDCardId"]);
                        emp.IDCardNo = row["IDCardNo"].ToString();
                        emp.FingerId1 = Convert.ToInt32(row["FingerId1"]);
                        emp.FingerData1 = (byte[])row["FingerData1"];
                        emp.FingerId2 = Convert.ToInt32(row["FingerId2"]);
                        emp.FingerData2 = (byte[])row["FingerData2"];
                        emp.FingerId3 = Convert.ToInt32(row["FingerId3"]);
                        emp.FingerData3 = (byte[])row["FingerData3"];
                    }
                }
            }
            catch
            {

            }
            return emp;
        }
        #endregion

        #region 根据身份证序列号获取人员信息
        public static EmpInfo GetEmpInfoByIDSerialNo(string cardNo)
        {
            EmpInfo emp = null;
            try
            {
                using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
                {
                    string sql = $"Select a.*,b.DeptId,b.DeptName From EmpInfo a,DeptInfo b Where a.DeptId = b.DeptId and a.IDSerialNo='{cardNo}'";
                    DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                    DataTable dt = dbHelper.ExecuteDataTable(cmd);
                    foreach (DataRow row in dt.Rows)
                    {
                        emp = new EmpInfo();
                        emp.DeptId = Convert.ToInt32(row["DeptId"]);
                        emp.EmpId = Convert.ToInt32(row["EmpId"]);
                        emp.EmpCode = row["EmpCode"].ToString();
                        emp.EmpName = row["EmpName"].ToString();
                        emp.EnglishName = row["EnglishName"].ToString();
                        emp.Sex = row["Sex"].ToString();
                        emp.IdentityCard = row["IdentityCard"].ToString();
                        emp.Telephone = row["Telephone"].ToString();
                        emp.BirthDay = row["BirthDay"].ToString();
                        emp.Nation = row["Nationality"].ToString();
                        emp.BornEarth = row["BornEarth"].ToString();
                        emp.Marrige = row["Marrige"].ToString();
                        emp.JoinDate = row["JoinDate"].ToString();
                        emp.Photo = row["Photo"].ToString().Equals(string.Empty) ? null : GetPhotoFromArray((byte[])row["Photo"]);
                        emp.TicketType = Convert.ToInt32(row["TicketType"]);
                        emp.BeginDate = row["BeginDate"].ToString();
                        emp.EndDate = row["EndDate"].ToString();
                        emp.ICCardId = Convert.ToInt32(row["ICCardId"]);
                        emp.ICCardNo = row["ICCardNo"].ToString();
                        emp.IDSerialId = Convert.ToInt32(row["IDSerialId"]);
                        emp.IDSerialNo = row["IDSerialNo"].ToString();
                        emp.IDCardId = Convert.ToInt32(row["IDCardId"]);
                        emp.IDCardNo = row["IDCardNo"].ToString();
                        emp.FingerId1 = Convert.ToInt32(row["FingerId1"]);
                        emp.FingerData1 = (byte[])row["FingerData1"];
                        emp.FingerId2 = Convert.ToInt32(row["FingerId2"]);
                        emp.FingerData2 = (byte[])row["FingerData2"];
                        emp.FingerId3 = Convert.ToInt32(row["FingerId3"]);
                        emp.FingerData3 = (byte[])row["FingerData3"];
                    }
                }
            }
            catch
            {

            }
            return emp;
        }
        #endregion

        #region 根据身份证号码获取人员信息
        public static EmpInfo GetEmpInfoByIDCardNo(string cardNo)
        {
            EmpInfo emp = null;
            try
            {
                using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
                {
                    string sql = $"Select a.*,b.DeptId,b.DeptName From EmpInfo a,DeptInfo b Where a.DeptId = b.DeptId and a.IDCardNo='{cardNo}'";
                    DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                    DataTable dt = dbHelper.ExecuteDataTable(cmd);
                    foreach (DataRow row in dt.Rows)
                    {
                        emp = new EmpInfo();
                        emp.DeptId = Convert.ToInt32(row["DeptId"]);
                        emp.EmpId = Convert.ToInt32(row["EmpId"]);
                        emp.EmpCode = row["EmpCode"].ToString();
                        emp.EmpName = row["EmpName"].ToString();
                        emp.EnglishName = row["EnglishName"].ToString();
                        emp.Sex = row["Sex"].ToString();
                        emp.IdentityCard = row["IdentityCard"].ToString();
                        emp.Telephone = row["Telephone"].ToString();
                        emp.BirthDay = row["BirthDay"].ToString();
                        emp.Nation = row["Nationality"].ToString();
                        emp.BornEarth = row["BornEarth"].ToString();
                        emp.Marrige = row["Marrige"].ToString();
                        emp.JoinDate = row["JoinDate"].ToString();
                        emp.Photo = row["Photo"].ToString().Equals(string.Empty) ? null : GetPhotoFromArray((byte[])row["Photo"]);
                        emp.TicketType = Convert.ToInt32(row["TicketType"]);
                        emp.BeginDate = row["BeginDate"].ToString();
                        emp.EndDate = row["EndDate"].ToString();
                        emp.ICCardId = Convert.ToInt32(row["ICCardId"]);
                        emp.ICCardNo = row["ICCardNo"].ToString();
                        emp.IDSerialId = Convert.ToInt32(row["IDSerialId"]);
                        emp.IDSerialNo = row["IDSerialNo"].ToString();
                        emp.IDCardId = Convert.ToInt32(row["IDCardId"]);
                        emp.IDCardNo = row["IDCardNo"].ToString();
                        emp.FingerId1 = Convert.ToInt32(row["FingerId1"]);
                        emp.FingerData1 = (byte[])row["FingerData1"];
                        emp.FingerId2 = Convert.ToInt32(row["FingerId2"]);
                        emp.FingerData2 = (byte[])row["FingerData2"];
                        emp.FingerId3 = Convert.ToInt32(row["FingerId3"]);
                        emp.FingerData3 = (byte[])row["FingerData3"];
                    }
                }
            }
            catch
            {

            }
            return emp;
        }
        #endregion


        #region 获取规定时间内的人数
        public static int GetTotalOfInside(string beginTime, string endTime)
        {
            int count = 0;
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "GetTotalOfInside";
                DbCommand cmd = dbHelper.GetStoredProcCommond(sql);
                dbHelper.AddInParameter(cmd, "@BeginTime", DbType.String, beginTime);
                dbHelper.AddInParameter(cmd, "@EndTime", DbType.String, endTime);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                foreach (DataRow row in dt.Rows)
                {
                    count = Convert.ToInt32(row[0]);
                }
            }
            return count;
        }
        #endregion

        #region 清除所有人员的在场状态
        public static void ClearCountOfInside()
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "ClearCountOfInside";
                DbCommand cmd = dbHelper.GetStoredProcCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }
        #endregion

        #region 获取任意身份证号码当天刷卡记录
        public static int GetCountOfIdCardRecord(string cardNo)
        {
            int count = -1;
            try
            {
                using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
                {

                    string sql = $"Select Count(*)  From Record Where Type = 3 And CardNo ='{cardNo}' And RecDateTime >='{DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00"}'  And RecDateTime <='{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}'";
                    DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                    DataTable dt = dbHelper.ExecuteDataTable(cmd);
                    foreach (DataRow row in dt.Rows)
                    {
                        count = Convert.ToInt32(row[0]);
                    }
                }
            }
            catch
            {

            }
            return count;
        }

        #endregion

        #region 插入实时记录
        public static void AddRealtimeRecord(int cardType, int camId, string cardNo, int deviceId, string ioFlag, string recTime, byte[] image)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "InsertRealtimeRecord ";
                DbCommand cmd = dbHelper.GetStoredProcCommond(sql);
                dbHelper.AddInParameter(cmd, "@CardType", DbType.Int32, cardType);
                dbHelper.AddInParameter(cmd, "@CamId", DbType.Int32, camId);
                dbHelper.AddInParameter(cmd, "@CardNo", DbType.String, cardNo);
                dbHelper.AddInParameter(cmd, "@DeviceId", DbType.String, deviceId);
                dbHelper.AddInParameter(cmd, "@IOFlag", DbType.String, ioFlag);
                dbHelper.AddInParameter(cmd, "@RecDatetime", DbType.String, recTime);
                dbHelper.AddInParameter(cmd, "@Image", DbType.Binary, image);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }
        #endregion

        #region 改变人员场内场外状态
        public static void ChangeEmpIOStatus(string cardNo, int iOFlag)
        {
            int status = (iOFlag == 3 ? 1 : 0);
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "ChangeEmpIOStatus ";
                DbCommand cmd = dbHelper.GetStoredProcCommond(sql);
                dbHelper.AddInParameter(cmd, "@CardNo", DbType.String, cardNo);
                dbHelper.AddInParameter(cmd, "@Status", DbType.Int32, status);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }

        #endregion

        #region 获取指纹更新状态
        public static List<FingerPrintTask> GetFingerPrintTasks()
        {
            List<FingerPrintTask> taskList = new List<FingerPrintTask>();
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "Select * From FingerPrintTask Where UpdateFlag = 0 ";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                foreach (DataRow row in dt.Rows)
                {
                    FingerPrintTask task = new FingerPrintTask();
                    task.RecId = Convert.ToInt32(row["RecId"]);
                    task.EmpId = Convert.ToInt32(row["EmpId"]);
                    task.FingerId = Convert.ToInt32(row["FingerId"]);
                    task.FingerData = (byte[])row["FingerData"];
                    taskList.Add(task);
                }
            }
            return taskList;
        }
        #endregion

        #region 更新指纹任务状态
        public static void UpdateFingerPrintTaskStatus(int recId)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = $"Update FingerPrintTask Set UpdateFlag = 1 Where RecId={recId} ";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }
        #endregion


    }
}
