using HPT.Gate.DataAccess.Entity.Entity;
using HPT.Gate.DataAccess.Service;
using HPT.Gate.Utils.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace HPT.Gate.DataAccess.Entity.Service
{
    public class RecordService
    {
        static RecordService()
        {
            try
            {
                using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
                {
                    StringBuilder buffer = new StringBuilder();

                    buffer.AppendLine($"ALTER PROCEDURE[dbo].[InsertRecord] (@Type int,@CardNo varchar(20),@RecordType varchar(30),@RecDateTime varchar(30),@IOFlag varchar(5),@DeviceId int,@Passed int,@Name varchar(30),@Sex varchar(10),@Nation varchar(10),@Address varchar(100),@Capture Image)");
                    buffer.AppendLine($"AS");
                    buffer.AppendLine($"BEGIN");
                    buffer.AppendLine($"SET NOCOUNT ON;");
                    buffer.AppendLine($"Declare @DeptId int,@DeptName varchar(30),@EmpId int,@EmpCode varchar(20),@EmpName varchar(30),@DeviceName varchar(30)");
                    buffer.AppendLine($"Select top 1 @DeviceName = DeviceName From DeviceInfo Where DeviceId = @DeviceId");
                    buffer.AppendLine($"If(@Type = 1)");
                    buffer.AppendLine($"Select Top 1 @EmpId = EmpId From EmpInfo where ICCardNo =@CardNo");
                    buffer.AppendLine($"If(@Type = 2)");
                    buffer.AppendLine($"Select Top 1 @EmpId = EmpId From EmpInfo where IDserial =@CardNo");
                    buffer.AppendLine($"If(@Type = 3)");
                    buffer.AppendLine($"Select Top 1 @EmpId = EmpId From EmpInfo where IDCardNo =@CardNo");
                    buffer.AppendLine($" If(@Type = 5)");
                    buffer.AppendLine($" Select @EmpId = Convert(int, @CardNo)");
                    buffer.AppendLine($" If(@Type = 6)");
                    buffer.AppendLine($"Select @EmpId = Convert(int, @CardNo)");
                    buffer.AppendLine($" If(@EmpId>0)");
                    buffer.AppendLine($"Begin");
                    buffer.AppendLine($"Select top 1 @DeptId = b.DeptId,@DeptName = b.DeptName,@EmpCode = a.EmpCode ,@EmpName = a.EmpName From EmpInfo a, DeptInfo b");
                    buffer.AppendLine($"where a.DeptId = b.DeptId and a.EmpId = @EmpId");
                    buffer.AppendLine($" If(@RecordType = '有效票')");
                    buffer.AppendLine($"Update EmpInfo Set IOFlag = case @IOFlag when '进' then 1 else 0 end Where EmpId = @EmpId");
                    buffer.AppendLine($"Insert Into AttendData(EmpId, DeviceId, CardNo, RecDateTime, IOFlag, Passed)");
                    buffer.AppendLine($"Values(@EmpId, @DeviceId, @CardNo, @RecDatetime, @IOFlag, @Passed)");
                    buffer.AppendLine($"End");
                    buffer.AppendLine($" Insert Into Record(DeptId, DeptName, EmpId, EmpCode, EmpName, Type, CardNo, DeviceId, DeviceName, IOFlag, RecDateTime, RecordType, Name, Sex, Nation, Address, Capture)");
                    buffer.AppendLine($"Values(@DeptId, @DeptName, @EmpId, @EmpCode, @EmpName, @Type, @CardNo, @DeviceId, @DeviceName, @IOFlag, @RecDateTime, @RecordType, @Name, @Sex, @Nation, @Address, @Capture)");
                    buffer.AppendLine($"END");
                    DbCommand cmd = dbHelper.GetSqlStringCommond(buffer.ToString());
                    dbHelper.ExecuteNonQuery(cmd);
                }
            }
            catch
            {

            }
        }
        public static void Insert(Record record)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                DbCommand cmd = dbHelper.GetStoredProcCommond("InsertRecord");
                dbHelper.AddInParameter(cmd, "@Type", DbType.Int32, record.Type);
                dbHelper.AddInParameter(cmd, "@CardNo", DbType.String, record.CardNo);
                dbHelper.AddInParameter(cmd, "@RecordType", DbType.String, record.RecordType);
                dbHelper.AddInParameter(cmd, "@RecDateTime", DbType.String, record.RecDatetime);
                dbHelper.AddInParameter(cmd, "@IOFlag", DbType.String, record.IOFlag);
                dbHelper.AddInParameter(cmd, "@DeviceId", DbType.Int32, record.DeviceId);
                dbHelper.AddInParameter(cmd, "@Passed", DbType.Int32, record.Passed);
                dbHelper.AddInParameter(cmd, "@Name", DbType.String, record.Name == null ? string.Empty : record.Name);
                dbHelper.AddInParameter(cmd, "@Sex", DbType.String, record.Sex == null ? string.Empty : record.Sex);
                dbHelper.AddInParameter(cmd, "@Nation", DbType.String, record.Nation == null ? string.Empty : record.Nation);
                dbHelper.AddInParameter(cmd, "@Address", DbType.String, record.Address == null ? string.Empty : record.Address);
                dbHelper.AddInParameter(cmd, "@Capture", DbType.Binary, new byte[] { 0x00 });
                dbHelper.ExecuteNonQuery(cmd);
            }
        }

        public static int GetTimesOfTimegroup(string ioFlag, int timegroupNo, string cardNo)
        {
            int times = 10000;
            TimeGroup timeGroup = TimegroupOfDoorService.GetTimegroupDuring(timegroupNo);
            if (timeGroup == null) return times;
            string beginDate = $"{DateTime.Now.ToString("yyyy-MM-dd")} {timeGroup.BeginTime}";
            string endDate = $"{DateTime.Now.ToString("yyyy-MM-dd")} {timeGroup.EndTime}";
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = $"Select Count(*) As Times from Record Where IOFlag ='{ioFlag}' And RecordType ='有效票' And  CardNo ='{cardNo}' and RecDateTime >='{beginDate}' And RecDatetime <='{endDate}' ";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                foreach (DataRow row in dt.Rows)
                {
                    times = Convert.ToInt32(row["Times"]);
                }
            }
            return times;
        }

        public static string GetLastTimeOfTimegroup(int intimeGroupNo, int empId)
        {
            string lastTime = string.Empty;
            TimeGroup timeGroup = TimegroupOfDoorService.GetTimegroupDuring(intimeGroupNo);
            if (timeGroup == null) return lastTime;
            string beginDate = $"{DateTime.Now.ToString("yyyy-MM-dd")} {timeGroup.BeginTime}";
            string endDate = $"{DateTime.Now.ToString("yyyy-MM-dd")} {timeGroup.EndTime}";
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = $"Select top 1 RecDateTime As LastTime from Record Where IOFlag='进' And  RecordType ='有效票' And  EmpId ={empId} and RecDateTime >='{beginDate}' And RecDatetime <='{endDate}' order By RecDatetime ";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                foreach (DataRow row in dt.Rows)
                {
                    lastTime = row["LastTime"].ToString();
                }
            }
            return lastTime;
        }

        #region 记录报表
        public static List<Record> Find(int _DeviceId, int _DeptId, int _DeptType, string _EmpCode, string _EmpName, int _CardType, string _CardNo, int _IOFlag, string _BeginTime, string _EndTime, int _RecordType)
        {
            List<Record> recordList = ToList(_BeginTime, _EndTime);
            if (_DeviceId != 0)
                recordList = recordList.Where(p => p.DeviceId == _DeviceId).ToList();
            if (_DeptId != 0)
            {
                if (_DeptType == 0)
                    recordList = recordList.Where(p => p.DeptId == _DeptId).ToList();
                else
                {
                    List<DeptInfo> deptList = DeptInfoService.GetChildDepts(_DeptId);
                    recordList = recordList.Where(p => deptList.Exists(s => s.DeptId == p.DeptId)).ToList();
                }
            }
            if (!string.IsNullOrWhiteSpace(_EmpCode))
                recordList = recordList.Where(s => _EmpCode.Equals(s.EmpCode)).ToList();
            if (!string.IsNullOrWhiteSpace(_EmpName))
                recordList = recordList.Where(s => _EmpName.Equals(s.EmpName)).ToList();
            if (_CardType != 0)
                recordList = recordList.Where(s => s.Type == _CardType).ToList();

            if (!string.IsNullOrWhiteSpace(_CardNo))
                recordList = recordList.Where(s => _CardNo.Equals(s.CardNo)).ToList();
            switch (_IOFlag)
            {
                case 0:
                    break;
                case 1:
                    recordList = recordList.Where(s => "进".Equals(s.IOFlag)).ToList();
                    break;
                case 2:
                    recordList = recordList.Where(s => "出".Equals(s.IOFlag)).ToList();
                    break;
            }
            if (_RecordType == 1)
                recordList = recordList.Where(s => "有效票".Equals(s.RecordType)).ToList();
            recordList.OrderBy(s => s.DeptId).OrderBy(s => s.EmpId).OrderBy(s => s.RecDatetime);
            return recordList;
        }
        #endregion

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

        #region 获取时间段内所有记录
        public static List<Record> ToList(string beginDate, string endDate)
        {
            List<Record> recordList = new List<Record>();
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = $"Select * From Record Where RecDatetime >='{beginDate}' And RecDatetime <='{endDate}'";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                foreach (DataRow row in dt.Rows)
                {
                    Record record = new Record();
                    record.RecId = Convert.ToInt32(row["RecId"]);
                    string deptId = row["DeptId"].ToString();
                    record.DeptId = Convert.ToInt32(deptId.Equals(string.Empty) ? "0" : deptId);
                    record.DeptName = row["DeptName"].ToString();
                    string empId = row["EmpId"].ToString();
                    record.EmpId = Convert.ToInt32(empId.Equals(string.Empty) ? "0" : row["EmpId"]);
                    record.EmpCode = row["EmpCode"].ToString();
                    record.EmpName = row["EmpName"].ToString();
                    record.Type = Convert.ToInt32(row["Type"]);
                    record.CardNo = row["CardNo"].ToString();
                    record.DeviceId = Convert.ToInt32(row["DeviceId"]);
                    record.DeviceName = row["DeviceName"].ToString();
                    record.IOFlag = row["IOFlag"].ToString();
                    record.RecDatetime = row["RecDatetime"].ToString();
                    record.RecordType = row["RecordType"].ToString();
                    record.Name = row["Name"].ToString();
                    record.Sex = row["Sex"].ToString();
                    record.Nation = row["Nation"].ToString();
                    record.Address = row["Address"].ToString();
                    byte[] array = (byte[])row["Capture"];
                    record.Capture = ArrayToImage(array);
                    recordList.Add(record);
                }
            }
            return recordList;
        }

        #endregion

        #region 数组转图片
        private static Bitmap ArrayToImage(byte[] arr)
        {
            if (arr == null || arr.Length < 100)
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

        #region 根据记录统计场内人数
        public static int GetTotalNumOfInside()
        {
            int total = 0;
            try
            {
                using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
                {
                    string beginTime = DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00";
                    string endTime = DateTime.Now.AddMinutes(15).ToString("yyyy-MM-dd HH:mm:ss");
                    StringBuilder buffer = new StringBuilder();
                    buffer.AppendLine("Declare @In int, @Out int, @Total int");
                    buffer.AppendLine($"Select @In = COUNT(1) From Record Where RecDateTime>= '{beginTime}' And RecDateTime<= '{endTime}' And IOFlag = '进' And RecordType ='有效票'");
                    buffer.AppendLine($"Select @Out = COUNT(1) From Record Where RecDateTime>= '{beginTime}' And RecDateTime<= '{endTime}' And IOFlag = '出' And RecordType ='有效票' ");
                    buffer.AppendLine("Select @Total = @In - @Out");
                    buffer.AppendLine("If(@Total < 0) Select @Total = 0");
                    buffer.AppendLine("Select @Total");
                    DbCommand cmd = dbHelper.GetSqlStringCommond(buffer.ToString());
                    DataTable dt = dbHelper.ExecuteDataTable(cmd);
                    foreach (DataRow row in dt.Rows)
                    {
                        total = Convert.ToInt32(row[0]);
                    }
                }
            }
            catch
            {

            }
            return total;
        }
        #endregion

    }
}
