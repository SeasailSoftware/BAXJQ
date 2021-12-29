using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Data.Sql;
using HPT.Gate.DataAccess.Entity;
using HPT.Gate.Utils.Helper;
using System.Drawing;
using System.IO;

namespace hpt.gate.DbTools.Service
{
    public class CollectService
    {
        #region 检查登录是否成功
        /// <summary>
        /// 检查登录是否成功
        /// </summary>
        /// <param name="OperName"></param>
        /// <param name="OpPass"></param>
        /// <returns></returns>
        public static bool CheckLogin(string OperName, string OpPass)
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

        public static DataTable LoadLEDController()
        {
            throw new NotImplementedException();
        }
        #endregion


        #region 获取数据库服务器列表
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

        #endregion

        #region 附加数据库

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

        #endregion

        #region 获取所有Tcp设备
        /// <summary>
        /// 获取数据库所有设备信息
        /// </summary>
        /// <returns></returns>
        public static List<DeviceInfo> GetAllTcpDevices()
        {
            List<DeviceInfo> devList = new List<DeviceInfo>();
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "Select * from deviceInfo";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                foreach (DataRow row in dt.Rows)
                {
                    DeviceInfo device = new DeviceInfo();
                    device.DeviceId = Convert.ToInt32(row["DeviceId"]);
                    device.DeviceName = row["DeviceName"].ToString();
                    device.Mac = row["MAC"].ToString();
                    device.IPAddress = row["IPAddress"].ToString();
                    device.SubNet = row["SubNet"].ToString();
                    device.GateWay = row["GateWay"].ToString();
                    device.Port = Convert.ToInt32(row["Port"]);
                    device.HardVersion = row["HardVersion"].ToString();
                    device.SoftVersion = row["SoftVersion"].ToString();
                    devList.Add(device);
                }
            }
            return devList;
        }
        #endregion

        #region 获取所有设备
        /// <summary>
        /// 获取数据库所有设备信息
        /// </summary>
        /// <returns></returns>
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
                    DeviceInfo device = new DeviceInfo();
                    device.DeviceId = Convert.ToInt32(row["DeviceId"]);
                    device.DeviceName = row["DeviceName"].ToString();
                    device.Mac = row["MAC"].ToString();
                    device.IPAddress = row["IPAddress"].ToString();
                    device.SubNet = row["SubNet"].ToString();
                    device.GateWay = row["GateWay"].ToString();
                    device.Port = Convert.ToInt32(row["Port"]);
                    device.HardVersion = row["HardVersion"].ToString();
                    device.SoftVersion = row["SoftVersion"].ToString();
                    devList.Add(device);
                }
            }
            return devList;
        }

        public static List<PhotoTask> GetPhotoTasks(int _MachineId)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region 根据机器号查找设备信息
        /// <summary>
        /// 根据机器号查找设备信息
        /// </summary>
        /// <param name="deviceId"></param>
        /// <returns></returns>
        public static DeviceInfo GetUdpDeviceInfoByDevId(int deviceId)
        {
            DeviceInfo device = null;
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = string.Format("Select * from DeviceInfo where DeviceId ={0}", deviceId);
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                foreach (DataRow row in dt.Rows)
                {
                    device.DeviceId = Convert.ToInt32(row["DeviceId"]);
                    device.DeviceName = row["DeviceName"].ToString();
                    device.Mac = row["MAC"].ToString();
                    device.IPAddress = row["IPAddress"].ToString();
                    device.SubNet = row["SubNet"].ToString();
                    device.GateWay = row["GateWay"].ToString();
                    device.Port = Convert.ToInt32(row["Port"]);
                    device.HardVersion = row["HardVersion"].ToString();
                    device.SoftVersion = row["SoftVersion"].ToString();

                }
            }
            return device;
        }

        /// <summary>
        /// 根据机器号查找设备信息
        /// </summary>
        /// <param name="deviceId"></param>
        /// <returns></returns>
        public static DeviceInfo GetTcpDeviceInfoByDevId(int deviceId)
        {
            DeviceInfo device = null;
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = string.Format("Select * from DeviceInfo where DeviceId ={0}", deviceId);
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                foreach (DataRow row in dt.Rows)
                {
                    device.DeviceId = Convert.ToInt32(row["DeviceId"]);
                    device.DeviceName = row["DeviceName"].ToString();
                    device.Mac = row["MAC"].ToString();
                    device.IPAddress = row["IPAddress"].ToString();
                    device.SubNet = row["SubNet"].ToString();
                    device.GateWay = row["GateWay"].ToString();
                    device.Port = Convert.ToInt32(row["Port"]);
                    device.HardVersion = row["HardVersion"].ToString();
                    device.SoftVersion = row["SoftVersion"].ToString();
                }
            }
            return device;
        }


        #endregion

        #region 获取可用机器编号

        public static void UpdateLEDController(int _LID, int nControlType, int protocol, int width, int heigth, string ipaddress, int port, int interval, List<int> devList, int totalRecord)
        {
            throw new NotImplementedException();
        }

        #endregion



        #region 检查设备是否已经存在
        /// <summary>
        /// 检查设备是否存在,,存在返回true,否则返回false
        /// </summary>
        /// <param name="mac"></param>
        /// <returns></returns>
        public static bool CheckDeviceExistsByIPAddress(string ipAddress)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                //先判断是否存在
                string sql = "select top 1 * from DeviceInfo where IPAddress ='" + ipAddress + "'";
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

        #region 检查设备是否已经存在
        /// <summary>
        /// 检查设备是否存在,,存在返回true,否则返回false
        /// </summary>
        /// <param name="mac"></param>
        /// <returns></returns>
        public static bool CheckDeviceExistsByIPAddressAndPort(string ipAddress, int port)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                //先判断是否存在
                string sql = $"select top 1 * from DeviceInfo where IPAddress ='{ipAddress}' and Port ={port}";
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

        #region 添加设备
        /// <summary>
        /// 插入设备
        /// </summary>
        /// <param name="device"></param>
        public static void InsertUdpDevice(DeviceInfo device)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                DbCommand cmd = dbHelper.GetStoredProcCommond("InsertDeviceInfo");
                dbHelper.AddInParameter(cmd, "@DeviceId", DbType.Int32, device.DeviceId);
                dbHelper.AddInParameter(cmd, "@deviceName", DbType.String, device.DeviceName);
                dbHelper.AddInParameter(cmd, "@PlaceId", DbType.Int32, device.PlaceId);
                dbHelper.AddInParameter(cmd, "@Mac", DbType.String, device.Mac);
                dbHelper.AddInParameter(cmd, "@IPAddress", DbType.String, device.IPAddress);
                dbHelper.AddInParameter(cmd, "@SubNet", DbType.String, device.SubNet);
                dbHelper.AddInParameter(cmd, "@GateWay", DbType.String, device.GateWay);
                dbHelper.AddInParameter(cmd, "@Port", DbType.Int32, device.Port);
                dbHelper.AddInParameter(cmd, "@HardVersion", DbType.String, device.HardVersion);
                dbHelper.AddInParameter(cmd, "@SoftVersion", DbType.String, device.SoftVersion);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }



        #endregion

        #region 更新设备信息
        /// <summary>
        /// 更新设备信息
        /// </summary>
        /// <param name="_CurDevice"></param>
        public static void UpdateDevice(DeviceInfo device)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = string.Empty;/* string.Format("Update DeviceInfo Set DeviceName ='{0}',IPAddress ='{1}',Subnet ='{2}',Gateway ='{3}',PlaceId ={4},Enabled = {5} Where DeviceId = {6}",
                    device._DeviceName, device._IPAddress, device._SubnetMark, device._Gateway, device.PlaceId, device.Status, device._MachineId);*/
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }

        /// <summary>
        /// 更新设备信息
        /// </summary>
        /// <param name="_CurDevice"></param>
        public static void UpdateTcpDevice(DeviceInfo device)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = string.Empty;/* string.Format("Update DeviceInfo Set DeviceName ='{0}',IPAddress ='{1}',Subnet ='{2}',Gateway ='{3}',PlaceId ={4},Enabled = {5} Where DeviceId = {6}",
                    device._DeviceName, device._IPAddress, device._SubnetMark, device._Gateway, device.PlaceId, device.Status, device._MachineId);*/
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }

        #endregion

        #region 删除设备信息
        /// <summary>
        /// 删除设备信息
        /// </summary>
        public static void DeleteDevice(int devId)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = string.Format("Delete From DeviceInfo where DeviceId ={0} Delete From EmpRightOfDevice where DeviceId = {1} if exists (select * from sysobjects where id = object_id(N'[dbo].[Cam_CameraOfDevice]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)  Delete From Cam_CameraOfDevice Where DeviceId = {2}", devId, devId, devId);
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }
        #endregion

        /*
        #region 获取更新卡信息
        /// <summary>
        /// 获取更新卡列表
        /// </summary>
        /// <param name="devId"></param>
        /// <returns></returns>
        public static List<DataCard> GetCardUpdateList(DataSynTask task)
        {

            List<DataCard> qdCard = new List<DataCard>();
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "GetCardListUpdate";
                DbCommand cmd = dbHelper.GetStoredProcCommond(sql);
                dbHelper.AddInParameter(cmd, "@DeviceId", DbType.Int32, task.DeviceId);
                dbHelper.AddInParameter(cmd, "@EmpId", DbType.Int32, task.EmpId);
                dbHelper.AddInParameter(cmd, "@Rights", DbType.Int32, task.Rights);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                foreach (DataRow row in dt.Rows)
                {
                    try
                    {
                        int Type = Convert.ToInt32(row["Type"].ToString());
                        DataCard card = new DataCard();
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
                        int empId = Convert.ToInt32(row["EmpId"]);
                        int cardId = Convert.ToInt32(row["CardId"]);
                        card.CurCardSerial = ArrayHelper.IntToBytes(cardId, 2);
                        card.TatolCardSerial = ArrayHelper.IntToBytes(Convert.ToInt32(row["totalNum"]), 2);
                        card.CardStatus = ArrayHelper.IntToBytes(Convert.ToInt32(row["BlackName"]), 1);
                        card.CardType = ArrayHelper.IntToBytes(Convert.ToInt32(row["CardType"]), 1);
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
                        card.Row1 = ArrayHelper.GB2312ToArray(row["Row1"].ToString(), 16);
                        card.Row2 = ArrayHelper.GB2312ToArray(row["Row2"].ToString(), 16);
                        card.Row3 = ArrayHelper.GB2312ToArray(row["Row3"].ToString(), 16);
                        card.DeviceId = task.DeviceId;
                        qdCard.Add(card);
                    }
                    catch
                    {

                    }
                }
            }
            return qdCard;


        }

*/

        #region 根据配置获取对应的显示内容
        public static string[] GetDisplayContent(int empId, int ticketType)
        {
            string[] content = new string[3];
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                try
                {
                    string sql = "GetDisplayContent";
                    DbCommand cmd = dbHelper.GetStoredProcCommond(sql);
                    dbHelper.AddInParameter(cmd, "@EmpId", DbType.Int32, empId);
                    dbHelper.AddInParameter(cmd, "@TicketType", DbType.Int32, ticketType);
                    DataTable dt = dbHelper.ExecuteDataTable(cmd);
                    foreach (DataRow row in dt.Rows)
                    {
                        content[0] = row["Content1"].ToString();
                        content[1] = row["Content2"].ToString();
                        content[2] = row["Content3"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return content;
        }
        #endregion



        #region 获取图片上传任务
        /// <summary>
        /// 获取未完成的任务列表
        /// </summary>
        /// <returns></returns>
        public static List<PhotoTask> GetUnfinishedTask(int deviceId)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                List<PhotoTask> taskList = new List<PhotoTask>();
                try
                {
                    string sql = string.Format("Select b.TaskId,b.EmpId,b.IOFlag,a.Photo from EmpInfo a,PhotoTask b Where a.EmpId = b.EmpId and b.Status = 0 and b.DeviceId ={0}", deviceId);
                    DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                    DataTable dt = dbHelper.ExecuteDataTable(cmd);
                    foreach (DataRow row in dt.Rows)
                    {
                        PhotoTask task = new PhotoTask();
                        task.TaskId = Convert.ToInt32(row["TaskId"]);
                        task.EmpId = Convert.ToUInt64(row["EmpId"]);
                        task.IOFlag = Convert.ToInt32(row["IOFlag"]);
                        task.Photo = row["Photo"].ToString().Equals(string.Empty) ? null : BytesToImage((byte[])row["Photo"]);
                        taskList.Add(task);
                    }
                }
                catch
                {

                }
                return taskList;
            }
        }
        #endregion

        #region 数组转图片
        public static Bitmap BytesToImage(byte[] arr)
        {
            if (arr == null || arr.Length < 100) return null;
            try
            {
                using (MemoryStream ms = new MemoryStream(arr))
                {
                    Bitmap img = (Bitmap)Bitmap.FromStream(ms);//在这里出错  
                    return img;
                }
            }
            catch
            {
                return null;
            }

        }
        #endregion


        #region 删除图片上传任务
        public static void DeletePhotoTask(int taskId)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = string.Format("Update PhotoTask Set Status = 1 where TaskId ={0}", taskId);
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }
        #endregion

        #region 添加人员信息
        /// <summary>
        /// 添加人员信息
        /// </summary>
        /// <param name="empCode"></param>
        /// <param name="empName"></param>
        /// <param name="cardNo"></param>
        /// <param name="photo"></param>
        public static void InsertEmpInfo(string empCode, string empName, string cardNo, byte[] photo, string row1, string row2, string row3)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "HPT_AddEmpAndCard";
                DbCommand cmd = dbHelper.GetStoredProcCommond(sql);
                dbHelper.AddInParameter(cmd, "@EmpCode", DbType.String, empCode);
                dbHelper.AddInParameter(cmd, "@EmpName", DbType.String, empName);
                dbHelper.AddInParameter(cmd, "@CardNo", DbType.String, cardNo);
                dbHelper.AddInParameter(cmd, "@Photo", DbType.Binary, photo);
                dbHelper.AddInParameter(cmd, "@Row1", DbType.String, row1);
                dbHelper.AddInParameter(cmd, "@Row2", DbType.String, row2);
                dbHelper.AddInParameter(cmd, "@Row3", DbType.String, row3);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }
        #endregion

        #region 添加人员设备权限
        public static void InsertRights(string empCode, int deviceId)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "HPT_AddEmpRights";
                DbCommand cmd = dbHelper.GetStoredProcCommond(sql);
                dbHelper.AddInParameter(cmd, "@EmpCode", DbType.String, empCode);
                dbHelper.AddInParameter(cmd, "@DeviceId", DbType.Int32, deviceId);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }

        public static List<string> GetLastIndexRecord(int recordIndex)
        {
            throw new NotImplementedException();
        }
        #endregion

        /*
        #region 插入记录
        public static void InsertRecord(Record record)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "InsertRecord";
                DbCommand cmd = dbHelper.GetStoredProcCommond(sql);
                dbHelper.AddInParameter(cmd, "@Type", DbType.Int32, record.Type);
                dbHelper.AddInParameter(cmd, "@CardNo", DbType.String, record.CardNo);
                dbHelper.AddInParameter(cmd, "@RecordType", DbType.String, record.SRecordType);
                dbHelper.AddInParameter(cmd, "@RecDateTime", DbType.String, record.RecDatetime);
                dbHelper.AddInParameter(cmd, "@IOFlag", DbType.String, record.SIOFlag);
                dbHelper.AddInParameter(cmd, "@PlaceId", DbType.Int32, 0);
                dbHelper.AddInParameter(cmd, "@DeviceId", DbType.Int32, record.MachineId);
                dbHelper.AddInParameter(cmd, "@RecPointer", DbType.Int32, record.CurrentIndex);
                dbHelper.AddInParameter(cmd, "@Name", DbType.String, record.Name == null ? " " : record.Name);
                dbHelper.AddInParameter(cmd, "@Sex", DbType.String, record.Sex == null ? " " : record.Sex);
                dbHelper.AddInParameter(cmd, "@Nation", DbType.String, record.Nation == null ? " " : record.Nation);
                dbHelper.AddInParameter(cmd, "@Address", DbType.String, record.Address == null ? " " : record.Address);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }

        #endregion

        #region 改变卡的同步状态
        /// <summary>
        /// 改变卡的同步状态
        /// </summary>
        /// <param name="card"></param>
        public static void UpdateCardStatus(DataCard card)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "UpdateCarInfoAfterSynchronous";
                DbCommand cmd = dbHelper.GetStoredProcCommond(sql);
                dbHelper.AddInParameter(cmd, "@CardId", DbType.Int32, card.CardId);
                dbHelper.AddInParameter(cmd, "@CardNo", DbType.String, card.SCardNo);
                dbHelper.AddInParameter(cmd, "@DeviceId", DbType.String, card.DeviceId);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }

        #endregion
        */
        #region 检查数据库版本
        public static int CheckVersion()
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                int version = 0;
                try
                {
                    string sql = "if Not exists(select 1 from sysobjects where name = 'DBVersion')";
                    sql += " Begin ";
                    sql += " CREATE TABLE[dbo].[DBVersion]([RecId][int] IDENTITY(1, 1) NOT NULL,[Version] [int] NOT NULL) ON[PRIMARY] ";
                    sql += " Insert DBVersion(Version) Values(0) ";
                    sql += " End ";
                    sql += " Select Top 1 Version From DBVersion ";
                    DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                    DataTable dt = dbHelper.ExecuteDataTable(cmd);
                    foreach (DataRow row in dt.Rows)
                    {
                        version = Convert.ToInt32(row["Version"]);
                    }
                }
                catch
                {

                }
                return version;
            }
        }
        #endregion

        #region 更新数据库版本号
        /// <summary>
        /// 更新数据库版本号
        /// </summary>
        /// <param name="version"></param>
        public static void UpdateVersion(int version)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = " If Not Exists(Select top 1 * From DBVersion) ";
                sql += string.Format(" Insert Into DBVersion(Version) values({0}) ", version);
                sql += " Else ";
                sql += string.Format(" Update DBVersion Set Version = {0} ", version);
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
            }
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

        #region 测试数据库连接
        public static bool CheckConnect(string connectString = "")
        {
            string sql = "Select top 1 * from EmpInfo";
            OleDbHelper dbHelper = null;
            try
            {
                if (connectString.Equals(string.Empty))
                {
                    dbHelper = new OleDbHelper();
                }
                else
                {
                    dbHelper = new OleDbHelper(connectString);
                }
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
            }
            catch
            {
                return false;
            }
            return true;
        }
        #endregion

        #region 获取人员照片
        #region 获取人员的照片
        public static byte[] GetEmpPhotoByEmpId(UInt64 empid)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = string.Format("Select Photo From EmpInfo where EmpId ={0}", empid);
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                byte[] arr = null;
                foreach (DataRow row in dt.Rows)
                {
                    arr = row["Photo"].ToString().Length < 10 ? null : (byte[])row["Photo"];
                }
                return arr;
            }
        }
        #endregion
        #endregion

        #region 更新卡的进出状态
        public static void UpdateCardIOStatus(int cardType, string cardNo, int ioFlag)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                int index = ioFlag == 3 ? 4 : 3;
                string sql = string.Format("Update CardInfo Set IOFlag ={0} where Type ={1} and CardNo ='{2}'", index, cardType, cardNo);
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }
        #endregion

        #region 删除Led控制卡
        public static void DeleteLedController(int lId)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = string.Format("Delete From Led_LedController where Lid ={0}", lId);
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }

        public static void DeleteLedController(string ipAddress)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = $"Declare @Lid int";
                sql += $"{Environment.NewLine}Select top 1 @Lid = Lid From Led_LedController Where IPAddress ='{ipAddress}'";
                sql +=$"{Environment.NewLine}Delete From Led_LedController where Lid =@Lid";
                sql += $"{Environment.NewLine}Delete From Led_AreaInfo where Lid =@Lid";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }
        #endregion

    }
}
