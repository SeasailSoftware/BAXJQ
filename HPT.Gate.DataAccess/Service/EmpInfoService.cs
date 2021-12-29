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
    public class EmpInfoService
    {
        static EmpInfoService()
        {
            try
            {
                using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
                {
                    StringBuilder buffer = new StringBuilder();
                    buffer.AppendLine($"ALTER PROCEDURE [dbo].[Emp_Update](@EmpId int,@EmpCode varchar(30),@EmpName varchar(30),@EnglishName varchar(30),@sex varchar(5),@identityCard varchar(20),");
                    buffer.AppendLine($"            @DeptId int, @TelePhone varchar(15), @birthday varchar(20), @Nationality varchar(10), @bornEarth varchar(30), @marrige varchar(20), @joinDate varchar(30), @Photo image,");
                    buffer.AppendLine($"            @TicketType int, @BeginDate varchar(20), @EndDate varchar(20), @ICCardNo varchar(20), @IDSerial varchar(20), @IDCardNo varchar(20), @Duty varchar(30), @Rehire int, @HireTimes int, @Status int, @LeaveDate Varchar(20),");
                    buffer.AppendLine($"            @FingerData1 Image, @FingerData2 Image, @FaceData image)");
                    buffer.AppendLine($"AS");
                    buffer.AppendLine($" BEGIN");
                    buffer.AppendLine($"SET NOCOUNT ON;");
                    buffer.AppendLine($"            Update EmpInfo set EmpCode = @Empcode, EmpName = @EmpName, EnglishName = @EnglishName, Sex = @Sex, IdentityCard = @IdentityCard, DeptId = @DeptId, TelePhone = @TelePhone,");
                    buffer.AppendLine($"                    BirthDay = @BirthDay, Nationality = @Nationality, BornEarth = @BornEarth, Marrige = @Marrige, JoinDate = @JoinDate, Photo = @Photo, TicketType = @TicketType, BeginDate = @BeginDate,");
                    buffer.AppendLine($"                    EndDate = @EndDate, ICCardNo = @ICCardNo, IDSerial = @IDSerial, IDCardNo = @IDcardNo, Duty = @Duty, Rehire = @Rehire, HireTimes = @HireTimes, Status = @Status, LeaveDate = @LeaveDate, FingerData1 = @FingerData1, FingerData2 = @FingerData2, FaceData = @FaceData");
                    buffer.AppendLine($"            where EmpId = @EmpId");
                    buffer.AppendLine($"            Update DevRightOfEmp Set UpdateFlag = 0 Where EmpId = @EmpId");
                    buffer.AppendLine($"            Delete From FaceDataTask Where EmpId = @EmpId");
                    buffer.AppendLine($"            If(@Status = 0)");
                    buffer.AppendLine($"            Begin");
                    buffer.AppendLine($"                    Insert FingerPrintTask Select @EmpId,@EmpId,@FingerData1,0");
                    buffer.AppendLine($"                    Insert FaceDataTask Select 2, DeviceId,@EmpId,0 From FaceDevice");
                    buffer.AppendLine($"            End");
                    buffer.AppendLine($"            Else");
                    buffer.AppendLine($"            Begin");
                    buffer.AppendLine($"                    Insert FingerPrintTask Select @EmpId,@EmpId,0x00,0");
                    buffer.AppendLine($"                    Insert FaceDataTask Select 3, DeviceId,@EmpId,0 From FaceDevice");
                    buffer.AppendLine($"            End");
                    buffer.AppendLine($"END     ");
                    DbCommand cmd = dbHelper.GetSqlStringCommond(buffer.ToString());
                    dbHelper.ExecuteNonQuery(cmd);
                }
            }
            catch
            {

            }

            try
            {
                using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
                {
                    StringBuilder buffer = new StringBuilder();
                    buffer.AppendLine($"ALTER PROCEDURE[dbo].[Emp_Insert] (@EmpCode varchar(30),@EmpName varchar(30),@EnglishName varchar(30),@sex varchar(5),@identityCard varchar(20),");
                    buffer.AppendLine($"        @DeptId int,@TelePhone varchar(15),@birthday varchar(20),@Nationality varchar(10),@bornEarth varchar(30),@marrige varchar(20),@joinDate varchar(30),@Photo image,");
                    buffer.AppendLine($"        @TicketType int,@BeginDate varchar(20),@EndDate varchar(20),@ICCardNo varchar(20),@IDSerial varchar(20),@IDCardNo varchar(20),@Duty varchar(30),@Rehire int,@HireTimes int,@Status int,@LeaveDate Varchar(20),");
                    buffer.AppendLine($"        @FingerData1 Image, @FingerData2 Image,@FaceData image)");
                    buffer.AppendLine($"AS");
                    buffer.AppendLine($"BEGIN");
                    buffer.AppendLine($"SET NOCOUNT ON;");
                    buffer.AppendLine($"    Begin Tran");
                    buffer.AppendLine($"        --插入人员信息");
                    buffer.AppendLine($"        Insert into EmpInfo(EmpCode, EmpName, EnglishName, Sex, IdentityCard, DeptId, TelePhone, BirthDay, Nationality, BornEarth, Marrige, JoinDate, Photo,");
                    buffer.AppendLine($"            TicketType, BeginDate, EndDate, ICCardNo, IDSerial, IDCardNo, Duty, Rehire, HireTimes, Status, LeaveDate, FingerData1, FingerData2, FaceData)");
                    buffer.AppendLine($"        values(@Empcode, @EmpName, @EnglishName, @Sex, @IdentityCard, @DeptId, @TelePhone, @BirthDay, @Nationality, @BornEarth, @Marrige, @JoinDate, @Photo,");
                    buffer.AppendLine($"            @TicketType, @BeginDate, @EndDate, @ICCardNo, @IDSerial, @IDCardNo, @Duty, @Rehire, @HireTimes, @Status, @LeaveDate, @FingerData1, @FingerData2, @FaceData)");
                    buffer.AppendLine($"        Declare @EmpId int");
                    buffer.AppendLine($"        Select Top 1 @EmpId = EmpId From EmpInfo Where EmpCode =@EmpCode");
                    buffer.AppendLine($"        Delete From FaceDataTask Where EmpId = @EmpId");
                    buffer.AppendLine($"        If(Datalength(@Photo)>100)");
                    buffer.AppendLine($"                Insert FaceDataTask Select 1,DeviceId,@EmpId,0 From FaceDevice");
                    buffer.AppendLine($"         Insert FingerPrintTask Select @EmpId, @EmpId, @FingerData1,0");
                    buffer.AppendLine($"    COMMIT TRAN");
                    buffer.AppendLine($" END ");
                    DbCommand cmd = dbHelper.GetSqlStringCommond(buffer.ToString());
                    dbHelper.ExecuteNonQuery(cmd);
                }
            }
            catch
            {

            }

        }
        private static DBHelper helper = DBHelperFactory.CreateDBHelper();

        #region 获取所有人员信息
        public static DataTable GetAll()
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "Select a.*,b.DeptId,b.DeptName From EmpInfo a,DeptInfo b Where a.DeptId = b.DeptId ";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                return dt;
            }
        }
        #endregion

        #region 获取所有人员信息
        public static DataTable GetAllWithOutPhoto()
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "Select a.EmpId,a.EmpCode,a.EmpName,a.FingerId1,a.FingerData1,a.FingerId2,a.FingerData2,a.FingerId3,a.FingerData3,b.DeptId,b.DeptName From EmpInfo a,DeptInfo b Where a.DeptId = b.DeptId ";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                return dt;
            }
        }

        public static DataTable GetFaileFaces()
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "Select a.DeptId,a.DeptName,b.EmpId,b.Empcode,b.EmpName,b.FaceStatus From DeptInfo a,EmpInfo b Where a.DeptId = b.DeptId And b.FaceStatus='上传失败' Order By b.DeptId,b.EmpId ";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                return dt;
            }
        }
        #endregion

        #region 生成列表
        public static List<EmpInfo> ToList()
        {
            List<EmpInfo> emps = new List<EmpInfo>();
            DataTable dt = GetAll();
            return DataTableToEmpInfo(dt);
        }

        public static DataTable SummaryByDuty()
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "Select a.DeptName,Count(1) From DeptInfo a Left Join EmpInfo b On a.DeptId = b.DeptId Where b.IOFlag =1 Group By a.DeptName";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                return dt;
            }
        }

        #endregion

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

        #region 插入照片
        public static bool InsertPhoto(FaceToEmps face, out string msg)
        {
            try
            {
                string empCode = face.EmpCode;
                empCode = empCode.Substring(empCode.LastIndexOf(@"\") + 1);
                empCode = empCode.Substring(0, empCode.LastIndexOf('.'));
                EmpInfo emp = GetByEmpCode(empCode);
                if (emp == null)
                {
                    msg = "人员编号不存在!";
                    return false;
                }
                emp.Photo = (Bitmap)face.Photo;
                Update(emp);
                msg = "Success!";
                return true;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return false;
            }
        }
        #endregion


        #region 生成列表
        public static List<EmpInfo> ToListWithOutPhoto()
        {
            List<EmpInfo> emps = new List<EmpInfo>();
            DataTable dt = GetAllWithOutPhoto();
            foreach (DataRow row in dt.Rows)
            {
                EmpInfo emp = new EmpInfo();
                emp.EmpId = Convert.ToInt32(row["EmpId"]);
                emp.EmpCode = row["EmpCode"].ToString();
                emp.EmpName = row["EmpName"].ToString();
                emp.DeptId = Convert.ToInt32(row["DeptId"]);
                emp.DeptName = row["DeptName"].ToString();
                emp.FingerData1 = (byte[])row["FingerData1"];
                emp.FingerData2 = (byte[])row["FingerData2"];
                emp.FaceData = ArrayToFaceData((byte[])row["FaceData"]);
                emps.Add(emp);
            }
            return emps;
        }
        #endregion


        #region 删除人员以及所有信息
        public static void Del(int empid)
        {
            EmpInfo emp = GetByEmpId(empid);
            if (emp != null)
            {
                using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
                {
                    StringBuilder buffer = new StringBuilder();
                    buffer.AppendLine($"Delete From EmpInfo Where EmpId = {empid}");
                    buffer.AppendLine($"Update DevRightOfEmp Set UpdateFlag = 0 Where EmpId = {empid}");
                    buffer.AppendLine($" Insert FaceDataTask Select 3, DeviceId,{empid},0 From FaceDevice");
                    buffer.AppendLine($" Insert FingerPrintTask Select {empid}, {empid}, 0x00,0");
                    buffer.AppendLine($"Delete From Record Where EmpId ={empid}");
                    if (!string.IsNullOrEmpty(emp.ICCardNo))
                        buffer.AppendLine($"Delete From Record Where CardNo='{emp.ICCardNo}'");
                    if (!string.IsNullOrEmpty(emp.IDSerial))
                        buffer.AppendLine($"Delete From Record Where CardNo='{emp.IDSerial}'");
                    if (!string.IsNullOrEmpty(emp.IDCardNo))
                        buffer.AppendLine($"Delete From Record Where CardNo='{emp.IDCardNo}'");
                    DbCommand cmd = dbHelper.GetSqlStringCommond(buffer.ToString());
                    dbHelper.ExecuteNonQuery(cmd);
                }
            }
        }

        public static DataTable QueryTable(int deptId, int deptType, int status)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                StringBuilder buffer = new StringBuilder();
                buffer.Append($"{Environment.NewLine}Select a.[Empid],a.[empcode],a.[EmpName],a.[IOFlag],a.[EnglishName],a.[Sex],a.[IdentityCard],a.[DeptID],");
                buffer.Append($"a.[TelePhone],a.[BirthDay],a.[nationality],a.[BornEarth],a.[marrige],a.[JoinDate],a.[Duty],a.[Status],a.[Rehire],a.[HireTimes],a.[LeaveDate],");
                buffer.Append($"a.[TicketType],a.[BeginDate],a.[EndDate],a.[ICCardNo],a.[IDSerial],a.[IDCardNo],a.[CreateTime] ,a.[FaceStatus], b.DeptId, b.DeptName From EmpInfo a, DeptInfo b Where a.DeptId = b.DeptId And b.DeptId in (");
                if (deptType == 0)
                    buffer.Append($"{deptId},");
                else
                {
                    List<DeptInfo> depts = DeptInfoService.GetChildDepts(deptId);
                    foreach (DeptInfo dept in depts)
                    {
                        buffer.Append($"{dept.DeptId},");
                    }
                }
                buffer.Append("-1)");
                if (status != 2)
                    buffer.Append($"  And a.Status ={status}");
                DbCommand cmd = dbHelper.GetSqlStringCommond(buffer.ToString());
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                return dt;
            }
        }
        #endregion

        #region 获取部门人员列表
        public static List<EmpInfo> GetByDeptId(int deptId, bool flag = true)
        {
            List<int> deptIds = new List<int>();
            if (flag)
                deptIds = DeptInfoService.GetChildDepts(deptId).Select(p => p.DeptId).ToList();
            else
                deptIds.Add(deptId);
            string deptIdString = "(";
            foreach (int id in deptIds)
            {
                deptIdString += $"{id},";
            }
            deptIdString += "-1)";

            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = $"Select a.*,b.DeptId,b.DeptName From EmpInfo a,DeptInfo b Where a.DeptId = b.DeptId And b.DeptId in {deptIdString}";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                return DataTableToEmpInfoes(dt);
            }

        }

        #endregion

        #region 查找人员
        public static List<EmpInfo> Find(string empCode, string empName, int cardType, string cardNo)
        {
            List<EmpInfo> empList = ToList();
            if (!string.IsNullOrEmpty(empCode))
                empList = empList.Where(s => empCode.Equals(s.EmpCode)).ToList();
            if (!string.IsNullOrWhiteSpace(empName))
                empList = empList.Where(s => empName.Equals(s.EmpName)).ToList();
            if (!string.IsNullOrWhiteSpace(cardNo))
            {
                switch (cardType)
                {
                    case 1:
                        empList = empList.Where(s => cardNo.Equals(s.ICCardNo)).ToList();
                        break;
                }
            }
            return empList;
        }

        #endregion

        #region 获取人员信息
        public static DataTable GetEmpBaseInfo()
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "Select a.Empid, a.empcode, a.EmpName, a.IOFlag, a.EnglishName, a.Sex, a.IdentityCard, ";
                sql += "a.DeptID, a.TelePhone, a.BirthDay, a.nationality, a.BornEarth, a.marrige, a.JoinDate, ";
                sql += "a.Duty, a.Status, a.Rehire, a.HireTimes, a.LeaveDate, a.TicketType, a.BeginDate, ";
                sql += "a.EndDate, a.ICCardNo, a.IDSerial, a.IDCardNo ,b.DeptId,b.DeptName From EmpInfo a,DeptInfo b Where a.DeptId = b.DeptId ";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                return dt;
            }
        }
        #endregion

        #region 生成列表
        public static List<EmpInfo> BaseInfoToList()
        {
            List<EmpInfo> emps = new List<EmpInfo>();
            DataTable dt = GetEmpBaseInfo();
            foreach (DataRow row in dt.Rows)
            {
                EmpInfo emp = new EmpInfo();
                emp.EmpId = Convert.ToInt32(row["EmpId"]);
                emp.EmpCode = row["EmpCode"].ToString();
                emp.EmpName = row["EmpName"].ToString();
                emp.IOFlag = Convert.ToInt32(row["IOFlag"]);
                emp.EnglishName = row["EnglishName"].ToString();
                emp.Sex = row["Sex"].ToString();
                emp.IdentityCard = row["IdentityCard"].ToString();
                emp.DeptId = Convert.ToInt32(row["DeptId"]);
                emp.DeptName = row["DeptName"].ToString();
                emp.Telephone = row["TelePhone"].ToString();
                emp.BirthDay = Convert.ToDateTime(row["BirthDay"]).ToString("yyyy-MM-dd");
                emp.Nation = row["Nationality"].ToString();
                emp.BornEarth = row["BornEarth"].ToString();
                emp.Marrige = row["Marrige"].ToString();
                emp.JoinDate = Convert.ToDateTime(row["JoinDate"]).ToString("yyyy-MM-dd");
                emp.TicketType = Convert.ToInt32(row["TicketType"]);
                emp.BeginDate = Convert.ToDateTime(row["BeginDate"]).ToString("yyyy-MM-dd");
                emp.EndDate = Convert.ToDateTime(row["EndDate"]).ToString("yyyy-MM-dd");
                emp.ICCardNo = row["ICCardNo"].ToString();
                emp.IDSerial = row["IDSerial"].ToString();
                emp.IDCardNo = row["IDCardNo"].ToString();
                emp.Duty = row["Duty"].ToString();
                emp.Rehire = Convert.ToInt32(row["ReHire"]);
                emp.HireTimes = Convert.ToInt32(row["HireTimes"]);
                emp.Status = Convert.ToInt32(row["Status"]);
                emp.LeaveDate = Convert.ToDateTime(row["LeaveDate"]).ToString("yyyy-MM-dd");
                emps.Add(emp);
            }
            return emps;
        }

        #endregion



        #region 查找人员
        public static EmpInfo Find(string empCode, string empName)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "Select a.DeptId,a.DeptName,b.EmpId,b.Empcode,b.EmpName,b.FaceStatus From DeptInfo a,EmpInfo b Where a.DeptId = b.DeptId";
                if (!string.IsNullOrEmpty(empCode))
                    sql += $" And b.EmpCode='{empCode}'";
                if (!string.IsNullOrEmpty(empName))
                    sql += $" And b.EmpName='{empName}'";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                List<EmpInfo> emps = DataTableToEmpInfo(dt);
                if (emps.Count == 0) return null;
                return emps[0];
            }

        }

        #endregion

        #region 查找人员
        public static List<EmpInfo> Find(int deptId, int deptType, string empCode, string empName, string cardNo)
        {
            List<EmpInfo> empList = new List<EmpInfo>();
            List<DeptInfo> deptList = new List<DeptInfo>();
            if (deptType == 0)
                deptList.Add(DeptInfoService.GetByDeptId(deptId));
            else
                deptList = DeptInfoService.GetChildDepts(deptId);
            empList.AddRange(ToList().Where(p => deptList.Exists(s => s.DeptId == p.DeptId)));
            if (!string.IsNullOrEmpty(empCode))
                empList = empList.Where(s => empCode.Equals(s.EmpCode)).ToList();
            if (!string.IsNullOrWhiteSpace(empName))
                empList = empList.Where(s => empName.Equals(s.EmpName)).ToList();
            if (!string.IsNullOrWhiteSpace(cardNo))
                empList = empList.Where(s => cardNo.Equals(s.ICCardNo)).ToList();
            return empList;
        }

        #endregion


        #region 数据导入
        public static string DataImport(string deptName, string empCode, string empName, string icCardNo, string sex, string identityCard, string birthDay, string nation, string bornEarth, string marrige, string duty, string joinDate,
                    int deptImportType, int empImportType, int cardImportType, int ticketType, string beginDate, string endDate)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string result = string.Empty;
                try
                {
                    string sql = "DataImport";
                    DbCommand cmd = dbHelper.GetStoredProcCommond(sql);
                    dbHelper.AddInParameter(cmd, "@DeptName", DbType.String, deptName);
                    dbHelper.AddInParameter(cmd, "@EmpCode", DbType.String, empCode);
                    dbHelper.AddInParameter(cmd, "@EmpName", DbType.String, empName);
                    dbHelper.AddInParameter(cmd, "@CardNo", DbType.String, icCardNo);
                    dbHelper.AddInParameter(cmd, "@Sex", DbType.String, sex);
                    dbHelper.AddInParameter(cmd, "@IdentityCard", DbType.String, identityCard);
                    dbHelper.AddInParameter(cmd, "@Birthday", DbType.String, birthDay);
                    dbHelper.AddInParameter(cmd, "@Nation", DbType.String, nation);
                    dbHelper.AddInParameter(cmd, "@BornEarth", DbType.String, bornEarth);
                    dbHelper.AddInParameter(cmd, "@Marrige", DbType.String, marrige);
                    dbHelper.AddInParameter(cmd, "@Duty", DbType.String, duty);
                    dbHelper.AddInParameter(cmd, "@joinDate", DbType.String, joinDate);
                    dbHelper.AddInParameter(cmd, "@DeptImportType", DbType.Int32, deptImportType);
                    dbHelper.AddInParameter(cmd, "@EmpImportType", DbType.Int32, empImportType);
                    dbHelper.AddInParameter(cmd, "@CardImportType", DbType.Int32, cardImportType);
                    dbHelper.AddInParameter(cmd, "@TicketType", DbType.Int32, ticketType);
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

        public static List<FingerPrint> GetAllFingerPrints()
        {
            List<FingerPrint> fps = new List<FingerPrint>();
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "Select EmpId ,FingerData1 From EmpInfo ";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                foreach (DataRow row in dt.Rows)
                {
                    FingerPrint fp = new FingerPrint();
                    fp.EmpId = Convert.ToInt32(row["EmpId"]);
                    byte[] fData = (byte[])row["FingerData1"];
                    if (fData == null || fData.Length < 100)
                        fp.FingerData = null;
                    else
                        fp.FingerData = fData;
                    if (fp.FingerData != null)
                        fps.Add(fp);
                }
            }
            return fps;
        }

        #endregion

        #region 模糊查询
        public static DataTable Find(int deptId, int deptType, string empCode, string empName, string idCardNo, string duty, string telephone, string cardNo, int status)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                StringBuilder buffer = new StringBuilder();
                buffer.Append("Select a.*,b.DeptId,b.DeptName From EmpInfo a,DeptInfo b Where a.DeptId = b.DeptId And b.DeptId in(");
                if (deptType == 0)
                    buffer.Append($"{deptId},");
                else
                {
                    List<DeptInfo> depts = DeptInfoService.GetChildDepts(deptId);
                    foreach (DeptInfo dept in depts)
                    {
                        buffer.Append($"{dept.DeptId},");
                    }
                }
                buffer.Append("-1)");
                if (status != 2)
                    buffer.Append($"  And a.Status ={status}");
                if (!string.IsNullOrEmpty(empCode))
                    buffer.Append($" And a.EmpCode='{empCode}'");
                if (!string.IsNullOrEmpty(empName))
                    buffer.Append($" And a.EmpName='{empName}'");
                if (!string.IsNullOrEmpty(idCardNo))
                    buffer.Append($" And a.IdentityCard='{idCardNo}'");
                if (!string.IsNullOrEmpty(duty))
                    buffer.Append($" And a.Duty='{duty}'");
                if (!string.IsNullOrEmpty(telephone))
                    buffer.Append($" And a.Telephone='{telephone}'");
                if (!string.IsNullOrEmpty(cardNo))
                    buffer.Append($" And a.ICCardNo='{cardNo}'");

                DbCommand cmd = dbHelper.GetSqlStringCommond(buffer.ToString());
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                return dt;
            }
        }

        public static List<string> GetAllEmpCodes()
        {
            List<string> empCodes = new List<string>();
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "Select EmpCode From EmpInfo Order By EmpId";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                foreach (DataRow row in dt.Rows)
                {
                    empCodes.Add(row[0].ToString());
                }
            }
            return empCodes;
        }

        public static int GetInsideTotal()
        {
            int count = 0;
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "Select Count(1) From EmpInfo Where IOFlag = 1";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                foreach (DataRow row in dt.Rows)
                {
                    count = Convert.ToInt32(row[0].ToString());
                    break;
                }
            }
            return count;
        }

        public static List<EmpInfo> Find(int _DeptId, int _DeptType, string _EmpCode, string _EmpName, string _IDCardNo, string _Duty, string _Telephone, string cardNo, int _Status, string createDate)
        {
            List<EmpInfo> empList = new List<EmpInfo>();
            List<DeptInfo> deptList = new List<DeptInfo>();
            //部门
            if (_DeptType == 0)
                deptList.Add(DeptInfoService.GetByDeptId(_DeptId));
            else
                deptList = DeptInfoService.GetChildDepts(_DeptId);
            List<EmpInfo> list = GetByCreateDate(createDate, createDate + " 23:59");
            empList.AddRange(list.Where(p => deptList.Exists(s => s.DeptId == p.DeptId)));
            //人员编号
            if (!string.IsNullOrEmpty(_EmpCode))
                empList = empList.Where(s => s.EmpCode.Contains(_EmpCode)).ToList();
            //人员姓名
            if (!string.IsNullOrWhiteSpace(_EmpName))
                empList = empList.Where(s => s.EmpName.Contains(_EmpName)).ToList();
            //身份证号码
            if (!string.IsNullOrWhiteSpace(_IDCardNo))
                empList = empList.Where(s => s.IdentityCard.Contains(_IDCardNo)).ToList();
            //职务
            if (!string.IsNullOrWhiteSpace(_Duty))
                empList = empList.Where(s => s.Duty.Contains(_Duty)).ToList();
            //联系电话
            if (!string.IsNullOrWhiteSpace(_Telephone))
                empList = empList.Where(s => s.Telephone.Contains(_Telephone)).ToList();
            //卡号
            if (!string.IsNullOrWhiteSpace(cardNo))
                empList = empList.Where(s => s.ICCardNo.Contains(cardNo)).ToList();

            return empList;
        }

        private static List<EmpInfo> GetByCreateDate(string beginTime, string endTime)
        {
            List<EmpInfo> emps = new List<EmpInfo>();
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = $"Select a.*,b.DeptId,b.DeptName From EmpInfo a,DeptInfo b Where a.DeptId = b.DeptId And a.CreateTime >'{beginTime}' And a.CreateTime<='{endTime}' ";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                foreach (DataRow row in dt.Rows)
                {
                    EmpInfo emp = new EmpInfo();
                    emp.EmpId = Convert.ToInt32(row["EmpId"]);
                    emp.EmpCode = row["EmpCode"].ToString();
                    emp.EmpName = row["EmpName"].ToString();
                    emp.IOFlag = Convert.ToInt32(row["IOFlag"]);
                    emp.EnglishName = row["EnglishName"].ToString();
                    emp.Sex = row["Sex"].ToString();
                    emp.IdentityCard = row["IdentityCard"].ToString();
                    emp.DeptId = Convert.ToInt32(row["DeptId"]);
                    emp.DeptName = row["DeptName"].ToString();
                    emp.Telephone = row["TelePhone"].ToString();
                    emp.BirthDay = Convert.ToDateTime(row["BirthDay"]).ToString("yyyy-MM-dd");
                    emp.Nation = row["Nationality"].ToString();
                    emp.BornEarth = row["BornEarth"].ToString();
                    emp.Marrige = row["Marrige"].ToString();
                    emp.JoinDate = Convert.ToDateTime(row["JoinDate"]).ToString("yyyy-MM-dd");
                    emp.TicketType = Convert.ToInt32(row["TicketType"]);
                    emp.BeginDate = Convert.ToDateTime(row["BeginDate"]).ToString("yyyy-MM-dd");
                    emp.EndDate = Convert.ToDateTime(row["EndDate"]).ToString("yyyy-MM-dd");
                    emp.ICCardNo = row["ICCardNo"].ToString();
                    emp.IDSerial = row["IDSerial"].ToString();
                    emp.IDCardNo = row["IDCardNo"].ToString();
                    emp.Duty = row["Duty"].ToString();
                    emp.Rehire = Convert.ToInt32(row["ReHire"]);
                    emp.HireTimes = Convert.ToInt32(row["HireTimes"]);
                    emp.Status = Convert.ToInt32(row["Status"]);
                    emp.LeaveDate = Convert.ToDateTime(row["LeaveDate"]).ToString("yyyy-MM-dd");
                    emp.CreateTime = row["CreateTime"].ToString();
                    emps.Add(emp);
                }
                return emps;
            }
        }

        #endregion

        #region 查找人员
        public static List<EmpInfo> Find(int deptId, int deptType, int status)
        {
            List<EmpInfo> emps = new List<EmpInfo>();
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                StringBuilder buffer = new StringBuilder();
                buffer.Append("Select a.*,b.DeptId,b.DeptName From EmpInfo a,DeptInfo b Where a.DeptId = b.DeptId And b.DeptId in(");
                if (deptType == 0)
                    buffer.Append($"{deptId},");
                else
                {
                    List<DeptInfo> depts = DeptInfoService.GetChildDepts(deptId);
                    foreach (DeptInfo dept in depts)
                    {
                        buffer.Append($"{dept.DeptId},");
                    }
                }
                buffer.Append("-1)");
                if (status != 2)
                    buffer.Append($"  And a.Status ={status}");
                DbCommand cmd = dbHelper.GetSqlStringCommond(buffer.ToString());
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                foreach (DataRow row in dt.Rows)
                {
                    EmpInfo emp = new EmpInfo();
                    emp.EmpId = Convert.ToInt32(row["EmpId"]);
                    emp.EmpCode = row["EmpCode"].ToString();
                    emp.EmpName = row["EmpName"].ToString();
                    emp.IOFlag = Convert.ToInt32(row["IOFlag"]);
                    emp.EnglishName = row["EnglishName"].ToString();
                    emp.Sex = row["Sex"].ToString();
                    emp.IdentityCard = row["IdentityCard"].ToString();
                    emp.DeptId = Convert.ToInt32(row["DeptId"]);
                    emp.DeptName = row["DeptName"].ToString();
                    emp.Telephone = row["TelePhone"].ToString();
                    emp.BirthDay = Convert.ToDateTime(row["BirthDay"]).ToString("yyyy-MM-dd");
                    emp.Nation = row["Nationality"].ToString();
                    emp.BornEarth = row["BornEarth"].ToString();
                    emp.Marrige = row["Marrige"].ToString();
                    emp.JoinDate = Convert.ToDateTime(row["JoinDate"]).ToString("yyyy-MM-dd");
                    emp.TicketType = Convert.ToInt32(row["TicketType"]);
                    emp.BeginDate = Convert.ToDateTime(row["BeginDate"]).ToString("yyyy-MM-dd");
                    emp.EndDate = Convert.ToDateTime(row["EndDate"]).ToString("yyyy-MM-dd");
                    emp.ICCardNo = row["ICCardNo"].ToString();
                    emp.IDSerial = row["IDSerial"].ToString();
                    emp.IDCardNo = row["IDCardNo"].ToString();
                    emp.Duty = row["Duty"].ToString();
                    emp.Rehire = Convert.ToInt32(row["ReHire"]);
                    emp.HireTimes = Convert.ToInt32(row["HireTimes"]);
                    emp.Status = Convert.ToInt32(row["Status"]);
                    emp.LeaveDate = Convert.ToDateTime(row["LeaveDate"]).ToString("yyyy-MM-dd");
                    emp.CreateTime = row["CreateTime"].ToString();
                    emps.Add(emp);
                }
            }
            return emps;
        }
        #endregion

        #region 查找人员
        public static List<EmpInfo> FindBaseInfo(int deptId, int deptType, int status)
        {
            List<EmpInfo> empList = new List<EmpInfo>();
            List<DeptInfo> deptList = new List<DeptInfo>();
            //部门
            if (deptType == 0)
                deptList.Add(DeptInfoService.GetByDeptId(deptId));
            else
                deptList = DeptInfoService.GetChildDepts(deptId);
            empList.AddRange(BaseInfoToList().Where(p => deptList.Exists(s => s.DeptId == p.DeptId)));
            switch (status)
            {
                case 0:
                    empList = empList.Where(s => s.Status == 0).ToList();
                    break;
                case 1:
                    empList = empList.Where(s => s.Status == 1).ToList();
                    break;
                case 2:
                    break;
            }
            return empList;
        }
        #endregion


        #region 获取可用的人员编号
        public static string GetUseAbleEmpCode()
        {
            return string.Empty;
        }
        #endregion

        #region 检查IC卡是否已经存在
        public static bool CheckICCardExists(string cardNo)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = $"Select Top 1 * From EmpInfo Where ICCardNo='{cardNo}'";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                return dt.Rows.Count > 0;
            }
        }

        #endregion

        #region 检查I身份证序列号是否已经存在
        public static bool CheckIDSerialExists(string cardNo)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = $"Select Top 1 * From EmpInfo Where IDSerial='{cardNo}'";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                return dt.Rows.Count > 0;
            }
        }

        #endregion

        #region 检查I身份证序列号是否已经存在
        public static bool CheckIDCardNoExists(string cardNo)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = $"Select Top 1 * From EmpInfo Where IDCardNo='{cardNo}'";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                return dt.Rows.Count > 0;
            }
        }

        #endregion


        #region 添加人员
        public static void Insert(EmpInfo emp)
        {
            using (DBHelper helper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "Emp_Insert";
                DbCommand cmd = helper.GetStoredProcCommond(sql);
                helper.AddInParameter(cmd, "@EmpCode", DbType.String, emp.EmpCode);
                helper.AddInParameter(cmd, "@EmpName", DbType.String, emp.EmpName);
                helper.AddInParameter(cmd, "@EnglishName", DbType.String, emp.EnglishName);
                helper.AddInParameter(cmd, "@Sex", DbType.String, emp.Sex);
                helper.AddInParameter(cmd, "@IdentityCard", DbType.String, emp.IdentityCard);
                helper.AddInParameter(cmd, "@DeptId", DbType.Int32, emp.DeptId);
                helper.AddInParameter(cmd, "@TelePhone", DbType.String, emp.Telephone);
                helper.AddInParameter(cmd, "@BirthDay", DbType.String, emp.BirthDay);
                helper.AddInParameter(cmd, "@Nationality", DbType.String, emp.Nation);
                helper.AddInParameter(cmd, "@bornEarth", DbType.String, emp.BornEarth);
                helper.AddInParameter(cmd, "@marrige", DbType.String, emp.Marrige);
                helper.AddInParameter(cmd, "@joinDate", DbType.String, emp.JoinDate);
                helper.AddInParameter(cmd, "@Photo", DbType.Binary, emp.PhotoStream);
                helper.AddInParameter(cmd, "@TicketType", DbType.Int32, emp.TicketType);
                helper.AddInParameter(cmd, "@BeginDate", DbType.String, emp.BeginDate);
                helper.AddInParameter(cmd, "@EndDate", DbType.String, emp.EndDate);
                helper.AddInParameter(cmd, "@ICCardNo", DbType.String, emp.ICCardNo);
                helper.AddInParameter(cmd, "@IDSerial", DbType.String, emp.IDSerial);
                helper.AddInParameter(cmd, "@IDCardNo", DbType.String, emp.IDCardNo);
                helper.AddInParameter(cmd, "@Duty", DbType.String, emp.Duty);
                helper.AddInParameter(cmd, "@ReHire", DbType.Int32, emp.Rehire);
                helper.AddInParameter(cmd, "@HireTimes", DbType.Int32, emp.HireTimes);
                helper.AddInParameter(cmd, "@Status", DbType.Int32, emp.Status);
                helper.AddInParameter(cmd, "@LeaveDate", DbType.String, emp.LeaveDate);

                helper.AddInParameter(cmd, "@FingerData1", DbType.Binary, emp.FingerData1 == null ? new byte[1] : emp.FingerData1);
                helper.AddInParameter(cmd, "@FingerData2", DbType.Binary, emp.FingerData2 == null ? new byte[1] : emp.FingerData2);
                helper.AddInParameter(cmd, "@FaceData", DbType.Binary, FaceDataToArray(emp.FaceData));
                helper.ExecuteNonQuery(cmd);
            }



        }

        private static string BytesToHexString(byte[] data)
        {
            if (data == null || data.Length < 100) return "00";
            StringBuilder buffer = new StringBuilder();
            foreach (byte b in data)
            {
                buffer.Append(b.ToString("X2"));
            }
            return buffer.ToString();
        }

        public static void Insert(List<EmpInfo> emps)
        {
            List<EmpInfo> currents = new List<EmpInfo>();
            for (int i = 1; i <= emps.Count; i++)
            {
                currents.Add(emps[i - 1]);
                if (i % 500 == 0 || i == emps.Count)
                {
                    using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
                    {
                        StringBuilder buffer = new StringBuilder();
                        foreach (EmpInfo emp in currents)
                        {
                            buffer.AppendLine($"Exec Emp_Insert  '{emp.EmpCode}','{emp.EmpName}','{emp.EnglishName}','{emp.Sex}','{emp.IdentityCard}'," +
                                $"{emp.DeptId},'{emp.Telephone}','{emp.BirthDay}','{emp.Nation}','{emp.BornEarth}','{emp.Marrige}','{emp.JoinDate}',0x00,{emp.TicketType}," +
                                $"'{emp.BeginDate}','{emp.EndDate}','{emp.ICCardNo}','{emp.IDSerial}','{emp.IDCardNo}','{emp.Duty}',{emp.Rehire},{emp.HireTimes},{emp.Status}," +
                                $"'2000-01-01',0x00,0x00,0x00");
                        }
                        DbCommand cmd = helper.GetSqlStringCommond(buffer.ToString());
                        helper.ExecuteNonQuery(cmd);
                    }
                    currents.Clear();
                }
            }
        }


        #endregion

        #region 改变人员的场内外状态
        public static void ChangeIOStatus(int empId, int status)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = $"Update EmpInfo Set IOFlag ={status} Where EmpId ={empId}";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }
        #endregion


        #region 更新人员信息
        public static void Update(EmpInfo emp)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                if (emp.Status == 1)
                {
                    emp.ICCardNo = string.Empty;
                    emp.FingerData1 = new byte[] { 0x00 };
                    emp.FingerData2 = new byte[] { 0x00 };
                    emp.FaceData = ArrayToFaceData(new byte[] { 0x00 });
                }
                string sql = "Emp_Update";
                DbCommand cmd = dbHelper.GetStoredProcCommond(sql);
                dbHelper.AddInParameter(cmd, "@EmpId", DbType.String, emp.EmpId);
                dbHelper.AddInParameter(cmd, "@EmpCode", DbType.String, emp.EmpCode);
                dbHelper.AddInParameter(cmd, "@EmpName", DbType.String, emp.EmpName);
                dbHelper.AddInParameter(cmd, "@EnglishName", DbType.String, emp.EnglishName);
                dbHelper.AddInParameter(cmd, "@Sex", DbType.String, emp.Sex);
                dbHelper.AddInParameter(cmd, "@IdentityCard", DbType.String, emp.IdentityCard);
                dbHelper.AddInParameter(cmd, "@DeptId", DbType.Int32, emp.DeptId);
                dbHelper.AddInParameter(cmd, "@TelePhone", DbType.String, emp.Telephone);
                dbHelper.AddInParameter(cmd, "@BirthDay", DbType.String, emp.BirthDay);
                dbHelper.AddInParameter(cmd, "@Nationality", DbType.String, emp.Nation);
                dbHelper.AddInParameter(cmd, "@bornEarth", DbType.String, emp.BornEarth);
                dbHelper.AddInParameter(cmd, "@marrige", DbType.String, emp.Marrige);
                dbHelper.AddInParameter(cmd, "@joinDate", DbType.String, emp.JoinDate);
                dbHelper.AddInParameter(cmd, "@Photo", DbType.Binary, emp.PhotoStream);
                dbHelper.AddInParameter(cmd, "@TicketType", DbType.Int32, emp.TicketType);
                dbHelper.AddInParameter(cmd, "@BeginDate", DbType.String, emp.BeginDate);
                dbHelper.AddInParameter(cmd, "@EndDate", DbType.String, emp.EndDate);
                dbHelper.AddInParameter(cmd, "@ICCardNo", DbType.String, emp.ICCardNo);
                dbHelper.AddInParameter(cmd, "@IDSerial", DbType.String, emp.IDSerial);
                dbHelper.AddInParameter(cmd, "@IDCardNo", DbType.String, emp.IDCardNo);

                dbHelper.AddInParameter(cmd, "@Duty", DbType.String, emp.Duty);
                dbHelper.AddInParameter(cmd, "@ReHire", DbType.Int32, emp.Rehire);
                dbHelper.AddInParameter(cmd, "@HireTimes", DbType.Int32, emp.HireTimes);
                dbHelper.AddInParameter(cmd, "@Status", DbType.Int32, emp.Status);
                dbHelper.AddInParameter(cmd, "@LeaveDate", DbType.String, emp.LeaveDate);

                helper.AddInParameter(cmd, "@FingerData1", DbType.Binary, emp.FingerData1 == null ? new byte[1] : emp.FingerData1);
                helper.AddInParameter(cmd, "@FingerData2", DbType.Binary, emp.FingerData2 == null ? new byte[1] : emp.FingerData2);
                helper.AddInParameter(cmd, "@FaceData", DbType.Binary, FaceDataToArray(emp.FaceData));
                dbHelper.ExecuteNonQuery(cmd);
            }
        }

        public static void Update(List<EmpInfo> emps)
        {
            List<EmpInfo> currents = new List<EmpInfo>();
            for (int i = 1; i <= emps.Count; i++)
            {
                currents.Add(emps[i - 1]);
                if (i % 500 == 0 || i == emps.Count)
                {
                    using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
                    {
                        StringBuilder buffer = new StringBuilder();
                        foreach (EmpInfo emp in currents)
                        {
                            buffer.AppendLine($"Exec Emp_Update {emp.EmpId}, '{emp.EmpCode}','{emp.EmpName}','{emp.EnglishName}','{emp.Sex}','{emp.IdentityCard}'," +
                                $"{emp.DeptId},'{emp.Telephone}','{emp.BirthDay}','{emp.Nation}','{emp.BornEarth}','{emp.Marrige}','{emp.JoinDate}',0x00,{emp.TicketType}," +
                                $"'{emp.BeginDate}','{emp.EndDate}','{emp.ICCardNo}','{emp.IDSerial}','{emp.IDCardNo}','{emp.Duty}',{emp.Rehire},{emp.HireTimes},{emp.Status}," +
                                $"'{emp.LeaveDate}',0x00,0x00,0x00");
                        }
                        DbCommand cmd = helper.GetSqlStringCommond(buffer.ToString());
                        helper.ExecuteNonQuery(cmd);
                    }
                }
            }
        }

        #endregion


        #region 根据人员编号获取人员信息
        public static EmpInfo GetByEmpId(int empId)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = $"Select a.*,b.DeptId,b.DeptName From EmpInfo a,DeptInfo b Where a.DeptId = b.DeptId And EmpId ={empId}";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                List<EmpInfo> emps = DataTableToEmpInfo(dt);
                if (emps.Count == 0) return null;
                return emps[0];
            }
        }
        #endregion


        #region 注销IC卡
        public static void CancelICCard(int empId)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                DbCommand cmd = dbHelper.GetStoredProcCommond("Emp_CancelICCard");
                dbHelper.AddInParameter(cmd, "@EmpId", DbType.Int32, empId);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }
        #endregion

        #region 注销身份证序列号
        public static void CancelIDSerial(int empId)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                DbCommand cmd = dbHelper.GetStoredProcCommond("Emp_CancelIDSerial");
                dbHelper.AddInParameter(cmd, "@EmpId", DbType.Int32, empId);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }
        #endregion

        #region 注销身份证号码
        public static void CancelIDCardNo(int empId)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                DbCommand cmd = dbHelper.GetStoredProcCommond("Emp_CancelIDIDCardNo");
                dbHelper.AddInParameter(cmd, "@EmpId", DbType.Int32, empId);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }
        #endregion

        #region 数组转图片
        private static Bitmap ArrayToImage(byte[] arr)
        {
            try
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
                ms.Dispose();
                return bmp;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion

        #region 获取当前部门所有下级部门的人员
        public static List<EmpInfo> GetAllByDept(int deptId)
        {
            List<EmpInfo> emps = new List<EmpInfo>();
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                StringBuilder buffer = new StringBuilder();
                buffer.Append("Select a.*,b.DeptId,b.DeptName From EmpInfo a,DeptInfo b Where a.DeptId = b.DeptId And b.DeptId in(");
                List<DeptInfo> depts = DeptInfoService.GetChildDepts(deptId);
                foreach (DeptInfo dept in depts)
                {
                    buffer.Append($"{dept.DeptId},");
                }
                buffer.Append("-1)");
                DbCommand cmd = dbHelper.GetSqlStringCommond(buffer.ToString());
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                foreach (DataRow row in dt.Rows)
                {
                    EmpInfo emp = new EmpInfo();
                    emp.EmpId = Convert.ToInt32(row["EmpId"]);
                    emp.EmpCode = row["EmpCode"].ToString();
                    emp.EmpName = row["EmpName"].ToString();
                    emp.IOFlag = Convert.ToInt32(row["IOFlag"]);
                    emp.EnglishName = row["EnglishName"].ToString();
                    emp.Sex = row["Sex"].ToString();
                    emp.IdentityCard = row["IdentityCard"].ToString();
                    emp.DeptId = Convert.ToInt32(row["DeptId"]);
                    emp.DeptName = row["DeptName"].ToString();
                    emp.Telephone = row["TelePhone"].ToString();
                    emp.BirthDay = Convert.ToDateTime(row["BirthDay"]).ToString("yyyy-MM-dd");
                    emp.Nation = row["Nationality"].ToString();
                    emp.BornEarth = row["BornEarth"].ToString();
                    emp.Marrige = row["Marrige"].ToString();
                    emp.JoinDate = Convert.ToDateTime(row["JoinDate"]).ToString("yyyy-MM-dd");
                    emp.TicketType = Convert.ToInt32(row["TicketType"]);
                    emp.BeginDate = Convert.ToDateTime(row["BeginDate"]).ToString("yyyy-MM-dd");
                    emp.EndDate = Convert.ToDateTime(row["EndDate"]).ToString("yyyy-MM-dd");
                    emp.ICCardNo = row["ICCardNo"].ToString();
                    emp.IDSerial = row["IDSerial"].ToString();
                    emp.IDCardNo = row["IDCardNo"].ToString();
                    emp.Duty = row["Duty"].ToString();
                    emp.Rehire = Convert.ToInt32(row["ReHire"]);
                    emp.HireTimes = Convert.ToInt32(row["HireTimes"]);
                    emp.Status = Convert.ToInt32(row["Status"]);
                    emp.LeaveDate = Convert.ToDateTime(row["LeaveDate"]).ToString("yyyy-MM-dd");
                    emp.CreateTime = row["CreateTime"].ToString();
                    emps.Add(emp);
                }
            }
            return emps;
        }
        #endregion

        #region 获取设备权限列表
        public static List<EmpInfo> GetByDeviceId(int deviceId)
        {
            List<EmpInfo> empList = new List<EmpInfo>();
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                StringBuilder buffer = new StringBuilder();
                buffer.Append($"Select a.*,b.DeptId,b.DeptName From EmpInfo a,DeptInfo b,DevRightOfEmp c Where a.DeptId = b.DeptId And a.EmpId =c.EmpId And c.Rights=1 And c.DeviceId={deviceId}");
                DbCommand cmd = dbHelper.GetSqlStringCommond(buffer.ToString());
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                foreach (DataRow row in dt.Rows)
                {
                    EmpInfo emp = new EmpInfo();
                    emp.EmpId = Convert.ToInt32(row["EmpId"]);
                    emp.EmpCode = row["EmpCode"].ToString();
                    emp.EmpName = row["EmpName"].ToString();
                    emp.IOFlag = Convert.ToInt32(row["IOFlag"]);
                    emp.EnglishName = row["EnglishName"].ToString();
                    emp.Sex = row["Sex"].ToString();
                    emp.IdentityCard = row["IdentityCard"].ToString();
                    emp.DeptId = Convert.ToInt32(row["DeptId"]);
                    emp.DeptName = row["DeptName"].ToString();
                    emp.Telephone = row["TelePhone"].ToString();
                    emp.BirthDay = Convert.ToDateTime(row["BirthDay"]).ToString("yyyy-MM-dd");
                    emp.Nation = row["Nationality"].ToString();
                    emp.BornEarth = row["BornEarth"].ToString();
                    emp.Marrige = row["Marrige"].ToString();
                    emp.JoinDate = Convert.ToDateTime(row["JoinDate"]).ToString("yyyy-MM-dd");
                    emp.TicketType = Convert.ToInt32(row["TicketType"]);
                    emp.BeginDate = Convert.ToDateTime(row["BeginDate"]).ToString("yyyy-MM-dd");
                    emp.EndDate = Convert.ToDateTime(row["EndDate"]).ToString("yyyy-MM-dd");
                    emp.ICCardNo = row["ICCardNo"].ToString();
                    emp.IDSerial = row["IDSerial"].ToString();
                    emp.IDCardNo = row["IDCardNo"].ToString();
                    emp.Duty = row["Duty"].ToString();
                    emp.Rehire = Convert.ToInt32(row["ReHire"]);
                    emp.HireTimes = Convert.ToInt32(row["HireTimes"]);
                    emp.Status = Convert.ToInt32(row["Status"]);
                    emp.LeaveDate = Convert.ToDateTime(row["LeaveDate"]).ToString("yyyy-MM-dd");
                    emp.CreateTime = row["CreateTime"].ToString();
                    empList.Add(emp);
                }
            }
            return empList;
        }
        #endregion

        #region 获取人员同步卡列表
        public static List<CardUpdate> GetCardUpdateList(DataSynTask task)
        {

            List<CardUpdate> qdCard = new List<CardUpdate>();
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
                    CardUpdate card = new CardUpdate();
                    card.EmpId = Convert.ToInt32(row["EmpId"]);
                    card.Type = Convert.ToInt32(row["Type"]);
                    card.CardNo = row["CardNo"].ToString();
                    if (string.IsNullOrEmpty(card.CardNo))
                        card.CardNo = "FFFFFFFF";
                    card.CardId = Convert.ToInt32(row["CardId"]);
                    card.TotalNum = Convert.ToInt32(row["TotalNum"]);
                    card.BlackName = Convert.ToInt32(row["BlackName"]);
                    card.CardType = Convert.ToInt32(row["CardType"]);
                    card.CardCode = Convert.ToInt32(row["CardCode"]);
                    card.Row1 = row["Row1"].ToString();
                    card.Row2 = row["Row2"].ToString();
                    card.Row3 = row["Row3"].ToString();
                    card.InRight = Convert.ToInt32(row["InRight"]);
                    card.OutRight = Convert.ToInt32(row["OutRight"]);
                    card.VoiceNo = Convert.ToInt32(row["VoiceNo"]);
                    card.Photo = Convert.ToInt32(row["Photo"]);
                    card.VacationId = Convert.ToInt32(row["VacationId"]);
                    card.InTimeGroupNo = Convert.ToInt32(row["InTimeGroupNo"]);
                    card.OutTimeGroupNo = Convert.ToInt32(row["OutTimeGroupNo"]);
                    card.BeginDate = row["BeginDate"].ToString();
                    card.EndDate = row["EndDate"].ToString();
                    qdCard.Add(card);

                }
            }
            return qdCard;
        }
        #endregion

        #region 根据卡号获取人员信息
        public static EmpInfo GetByCardNo(int cardType, string cardNo)
        {
            EmpInfo emp = null;
            switch (cardType)
            {
                case 1:
                    emp = GetByICCardNo(cardNo);
                    break;
                case 2:
                    emp = GetByIDSerial(cardNo);
                    break;
                case 3:
                    cardNo = cardNo.ToUpper().Replace("A", "X");
                    emp = GetByIDCardNo(cardNo.ToUpper());
                    break;
                case 6:
                    byte[] array = HexToArray(cardNo);
                    int empId = BitConverter.ToInt32(array, 0);
                    emp = GetByEmpId(empId);
                    break;
            }
            return emp;
        }

        #endregion

        #region 根据IC卡查找人员
        public static EmpInfo GetByEmpCode(string empCode)
        {
            EmpInfo emp = null;
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = $"Select a.*,b.DeptId,b.DeptName From EmpInfo a,DeptInfo b Where a.DeptId = b.DeptId  And a.EmpCode ='{empCode}'";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                foreach (DataRow row in dt.Rows)
                {
                    emp = new EmpInfo();
                    emp.EmpId = Convert.ToInt32(row["EmpId"]);
                    emp.EmpCode = row["EmpCode"].ToString();
                    emp.EmpName = row["EmpName"].ToString();
                    emp.IOFlag = Convert.ToInt32(row["IOFlag"]);
                    emp.EnglishName = row["EnglishName"].ToString();
                    emp.Sex = row["Sex"].ToString();
                    emp.IdentityCard = row["IdentityCard"].ToString();
                    emp.DeptId = Convert.ToInt32(row["DeptId"]);
                    emp.DeptName = row["DeptName"].ToString();
                    emp.Telephone = row["TelePhone"].ToString();
                    emp.BirthDay = Convert.ToDateTime(row["BirthDay"]).ToString("yyyy-MM-dd");
                    emp.Nation = row["Nationality"].ToString();
                    emp.BornEarth = row["BornEarth"].ToString();
                    emp.Marrige = row["Marrige"].ToString();
                    emp.JoinDate = Convert.ToDateTime(row["JoinDate"]).ToString("yyyy-MM-dd");
                    byte[] array = (byte[])row["Photo"];
                    emp.Photo = ArrayToImage(array);
                    emp.TicketType = Convert.ToInt32(row["TicketType"]);
                    emp.BeginDate = Convert.ToDateTime(row["BeginDate"]).ToString("yyyy-MM-dd");
                    emp.EndDate = Convert.ToDateTime(row["EndDate"]).ToString("yyyy-MM-dd");
                    emp.ICCardNo = row["ICCardNo"].ToString();
                    emp.IDSerial = row["IDSerial"].ToString();
                    emp.IDCardNo = row["IDCardNo"].ToString();
                    emp.Duty = row["Duty"].ToString();
                    emp.Rehire = Convert.ToInt32(row["ReHire"]);
                    emp.HireTimes = Convert.ToInt32(row["HireTimes"]);
                    emp.Status = Convert.ToInt32(row["Status"]);
                    emp.LeaveDate = Convert.ToDateTime(row["LeaveDate"]).ToString("yyyy-MM-dd");
                    emp.FingerData1 = (byte[])row["FingerData1"];
                    emp.FingerData2 = (byte[])row["FingerData2"];
                    emp.FaceData = ArrayToFaceData((byte[])row["FaceData"]);
                }
            }
            return emp;
        }
        #endregion

        #region 根据IC卡查找人员
        private static EmpInfo GetByICCardNo(string cardNo)
        {
            EmpInfo emp = null;
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = $"Select a.*,b.DeptId,b.DeptName From EmpInfo a,DeptInfo b Where a.DeptId = b.DeptId  And a.ICCardNo ='{cardNo}'";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                foreach (DataRow row in dt.Rows)
                {
                    emp = new EmpInfo();
                    emp.EmpId = Convert.ToInt32(row["EmpId"]);
                    emp.EmpCode = row["EmpCode"].ToString();
                    emp.EmpName = row["EmpName"].ToString();
                    emp.IOFlag = Convert.ToInt32(row["IOFlag"]);
                    emp.EnglishName = row["EnglishName"].ToString();
                    emp.Sex = row["Sex"].ToString();
                    emp.IdentityCard = row["IdentityCard"].ToString();
                    emp.DeptId = Convert.ToInt32(row["DeptId"]);
                    emp.DeptName = row["DeptName"].ToString();
                    emp.Telephone = row["TelePhone"].ToString();
                    emp.BirthDay = Convert.ToDateTime(row["BirthDay"]).ToString("yyyy-MM-dd");
                    emp.Nation = row["Nationality"].ToString();
                    emp.BornEarth = row["BornEarth"].ToString();
                    emp.Marrige = row["Marrige"].ToString();
                    emp.JoinDate = Convert.ToDateTime(row["JoinDate"]).ToString("yyyy-MM-dd");
                    byte[] array = (byte[])row["Photo"];
                    emp.Photo = ArrayToImage(array);
                    emp.TicketType = Convert.ToInt32(row["TicketType"]);
                    emp.BeginDate = Convert.ToDateTime(row["BeginDate"]).ToString("yyyy-MM-dd");
                    emp.EndDate = Convert.ToDateTime(row["EndDate"]).ToString("yyyy-MM-dd");
                    emp.ICCardNo = row["ICCardNo"].ToString();
                    emp.IDSerial = row["IDSerial"].ToString();
                    emp.IDCardNo = row["IDCardNo"].ToString();
                    emp.Duty = row["Duty"].ToString();
                    emp.Rehire = Convert.ToInt32(row["ReHire"]);
                    emp.HireTimes = Convert.ToInt32(row["HireTimes"]);
                    emp.Status = Convert.ToInt32(row["Status"]);
                    emp.LeaveDate = Convert.ToDateTime(row["LeaveDate"]).ToString("yyyy-MM-dd");
                    emp.FingerData1 = (byte[])row["FingerData1"];
                    emp.FingerData2 = (byte[])row["FingerData2"];
                    emp.FaceData = ArrayToFaceData((byte[])row["FaceData"]);
                }
            }
            return emp;
        }
        #endregion

        #region 根据身份证序列号查找人员
        private static EmpInfo GetByIDSerial(string cardNo)
        {
            EmpInfo emp = null;
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = $"Select a.*,b.DeptId,b.DeptName From EmpInfo a,DeptInfo b Where a.DeptId = b.DeptId  And a.IDSerial ='{cardNo}'";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                foreach (DataRow row in dt.Rows)
                {
                    emp = new EmpInfo();
                    emp.EmpId = Convert.ToInt32(row["EmpId"]);
                    emp.EmpCode = row["EmpCode"].ToString();
                    emp.EmpName = row["EmpName"].ToString();
                    emp.IOFlag = Convert.ToInt32(row["IOFlag"]);
                    emp.EnglishName = row["EnglishName"].ToString();
                    emp.Sex = row["Sex"].ToString();
                    emp.IdentityCard = row["IdentityCard"].ToString();
                    emp.DeptId = Convert.ToInt32(row["DeptId"]);
                    emp.DeptName = row["DeptName"].ToString();
                    emp.Telephone = row["TelePhone"].ToString();
                    emp.BirthDay = Convert.ToDateTime(row["BirthDay"]).ToString("yyyy-MM-dd");
                    emp.Nation = row["Nationality"].ToString();
                    emp.BornEarth = row["BornEarth"].ToString();
                    emp.Marrige = row["Marrige"].ToString();
                    emp.JoinDate = Convert.ToDateTime(row["JoinDate"]).ToString("yyyy-MM-dd");
                    byte[] array = (byte[])row["Photo"];
                    emp.Photo = ArrayToImage(array);
                    emp.TicketType = Convert.ToInt32(row["TicketType"]);
                    emp.BeginDate = Convert.ToDateTime(row["BeginDate"]).ToString("yyyy-MM-dd");
                    emp.EndDate = Convert.ToDateTime(row["EndDate"]).ToString("yyyy-MM-dd");
                    emp.ICCardNo = row["ICCardNo"].ToString();
                    emp.IDSerial = row["IDSerial"].ToString();
                    emp.IDCardNo = row["IDCardNo"].ToString();
                    emp.Duty = row["Duty"].ToString();
                    emp.Rehire = Convert.ToInt32(row["ReHire"]);
                    emp.HireTimes = Convert.ToInt32(row["HireTimes"]);
                    emp.Status = Convert.ToInt32(row["Status"]);
                    emp.LeaveDate = Convert.ToDateTime(row["LeaveDate"]).ToString("yyyy-MM-dd");
                    emp.FingerData1 = (byte[])row["FingerData1"];
                    emp.FingerData2 = (byte[])row["FingerData2"];
                    emp.FaceData = ArrayToFaceData((byte[])row["FaceData"]);
                }
            }
            return emp;
        }
        #endregion

        #region 根据身份证号码查找人员
        private static EmpInfo GetByIDCardNo(string cardNo)
        {
            EmpInfo emp = null;
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = $"Select a.*,b.DeptId,b.DeptName From EmpInfo a,DeptInfo b Where a.DeptId = b.DeptId  And a.IDCardNo ='{cardNo}'";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                foreach (DataRow row in dt.Rows)
                {
                    emp = new EmpInfo();
                    emp.EmpId = Convert.ToInt32(row["EmpId"]);
                    emp.EmpCode = row["EmpCode"].ToString();
                    emp.EmpName = row["EmpName"].ToString();
                    emp.IOFlag = Convert.ToInt32(row["IOFlag"]);
                    emp.EnglishName = row["EnglishName"].ToString();
                    emp.Sex = row["Sex"].ToString();
                    emp.IdentityCard = row["IdentityCard"].ToString();
                    emp.DeptId = Convert.ToInt32(row["DeptId"]);
                    emp.DeptName = row["DeptName"].ToString();
                    emp.Telephone = row["TelePhone"].ToString();
                    emp.BirthDay = Convert.ToDateTime(row["BirthDay"]).ToString("yyyy-MM-dd");
                    emp.Nation = row["Nationality"].ToString();
                    emp.BornEarth = row["BornEarth"].ToString();
                    emp.Marrige = row["Marrige"].ToString();
                    emp.JoinDate = Convert.ToDateTime(row["JoinDate"]).ToString("yyyy-MM-dd");
                    byte[] array = (byte[])row["Photo"];
                    emp.Photo = ArrayToImage(array);
                    emp.TicketType = Convert.ToInt32(row["TicketType"]);
                    emp.BeginDate = Convert.ToDateTime(row["BeginDate"]).ToString("yyyy-MM-dd");
                    emp.EndDate = Convert.ToDateTime(row["EndDate"]).ToString("yyyy-MM-dd");
                    emp.ICCardNo = row["ICCardNo"].ToString();
                    emp.IDSerial = row["IDSerial"].ToString();
                    emp.IDCardNo = row["IDCardNo"].ToString();
                    emp.Duty = row["Duty"].ToString();
                    emp.Rehire = Convert.ToInt32(row["ReHire"]);
                    emp.HireTimes = Convert.ToInt32(row["HireTimes"]);
                    emp.Status = Convert.ToInt32(row["Status"]);
                    emp.LeaveDate = Convert.ToDateTime(row["LeaveDate"]).ToString("yyyy-MM-dd");
                    emp.FingerData1 = (byte[])row["FingerData1"];
                    emp.FingerData2 = (byte[])row["FingerData2"];
                    emp.FaceData = ArrayToFaceData((byte[])row["FaceData"]);
                }
            }
            return emp;
        }
        #endregion


        #region 通过指纹查找人员
        public static EmpInfo GetByFingerId(int fingerId)
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
                        byte[] array = (byte[])row["Photo"];
                        emp.Photo = ArrayToImage(array);
                        emp.TicketType = Convert.ToInt32(row["TicketType"]);
                        emp.BeginDate = row["BeginDate"].ToString();
                        emp.EndDate = row["EndDate"].ToString();
                        emp.ICCardNo = row["ICCardNo"].ToString();
                        emp.Duty = row["Duty"].ToString();
                        emp.Status = Convert.ToInt32(row["Status"]);
                        emp.LeaveDate = row["LeaveDate"].ToString();
                        emp.FingerData1 = (byte[])row["FingerData1"];
                        emp.FingerData2 = (byte[])row["FingerData2"];
                        emp.FaceData = ArrayToFaceData((byte[])row["FaceData"]);
                    }
                }
            }
            catch
            {

            }
            return emp;
        }
        #endregion

        #region 清除所有人员的在场状态
        public static void ClearCountOfInside()
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "Update EmpInfo Set IOFlag = 0";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }
        #endregion

        #region 查找人员
        public static List<EmpInfo> FindOnCondition(int deptId, int deptType, string empCode, string empName)
        {
            List<EmpInfo> empList = new List<EmpInfo>();
            List<DeptInfo> deptList = new List<DeptInfo>();
            if (deptType == 0)
                deptList.Add(DeptInfoService.GetByDeptId(deptId));
            else
                deptList = DeptInfoService.GetChildDepts(deptId);
            empList.AddRange(ToList().Where(p => deptList.Exists(s => s.DeptId == p.DeptId)));
            if (!string.IsNullOrEmpty(empCode))
                empList = empList.Where(s => empCode.Equals(s.EmpCode)).ToList();
            if (!string.IsNullOrWhiteSpace(empName))
                empList = empList.Where(s => empName.Equals(s.EmpName)).ToList();
            return empList;
        }
        #endregion

        #region 检查人员是否存在
        public static bool CheckExists(string empCode)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = $"Select top 1 * From EmpInfo Where EmpCode='{empCode}'";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                return dt.Rows.Count > 0;
            }
        }
        #endregion

        #region 获取下一个EmpId
        public static int GetNextEmpId()
        {
            int max = 0;
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "Select IsNull(Max(EmpId),0) From EmpInfo ";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                foreach (DataRow row in dt.Rows)
                {
                    max = Convert.ToInt32(row[0]);
                }
            }
            return max + 1;
        }
        #endregion


        #region 设置人员离职
        public static void SetEmpLeave(int empId, string leaveDate)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = $"Update EmpInfo Set Status = 1 ,LeaveDate ='{leaveDate}',ICCardNo='FFFFFFFF' Where EmpId ={empId} ";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }
        #endregion

        #region 注销指纹数据
        public static void CancelFingerPrints(int empId)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                DbCommand cmd = dbHelper.GetStoredProcCommond("CancelFingerPrints");
                dbHelper.AddInParameter(cmd, "@EmpId", DbType.Int32, empId);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }
        #endregion

        #region 人脸数据转字节数据
        private static byte[] FaceDataToArray(int[] faceData)
        {
            List<byte> array = new List<byte>();
            if (faceData == null)
                array.Add(0x00);
            else
            {
                foreach (int value in faceData)
                {
                    array.AddRange(BitConverter.GetBytes(value));
                }
            }
            return array.ToArray();
        }
        #endregion

        #region 字节数组转人脸数据
        private static int[] ArrayToFaceData(byte[] array)
        {
            if (array == null || array.Length < 100)
                return null;

            int[] list = new int[array.Length / 4];
            for (int i = 0; i < array.Length / 4; i++)
            {
                byte[] arr = new byte[4];
                if (i == array.Length / 4 - 1)
                {

                    int length = array.Length % 4;
                    switch (length)
                    {
                        case 0:
                            arr[0] = array[i * 4];
                            arr[1] = array[i * 4 + 1];
                            arr[2] = array[i * 4 + 2];
                            arr[3] = array[i * 4 + 3];
                            break;
                        case 1:
                            arr[0] = 0x00;
                            arr[1] = array[i * 4 + 0];
                            arr[2] = array[i * 4 + 1];
                            arr[3] = array[i * 4 + 2];
                            break;
                        case 2:
                            arr[0] = 0x00;
                            arr[1] = 0x00;
                            arr[2] = array[i * 4 + 0];
                            arr[3] = array[i * 4 + 1];
                            break;
                        case 3:
                            arr[0] = 0x00;
                            arr[1] = 0x00;
                            arr[2] = 0x00;
                            arr[3] = array[i * 4];
                            break;
                    }
                    if (length == 1)
                    {
                        arr[0] = 0x00;
                    }
                    list[i] = BitConverter.ToInt32(arr, 0);
                }
                else
                    arr = new byte[] { array[i * 4], array[i * 4 + 1], array[i * 4 + 2], array[i * 4 + 3] };

                list[i] = BitConverter.ToInt32(arr, 0);
            }
            return list;
        }
        #endregion

        #region 添加指纹人脸任务
        public static void AddFaceDataTask(EmpInfo emp, FaceDevice device, int type)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                int empId = emp == null ? 0 : emp.EmpId;
                string sql = $"Delete From FaceDataTask Where EmpId={empId} And DeviceId={device.DeviceId}";
                sql += $"{Environment.NewLine}Insert FaceDataTask(Type,DeviceId,EmpId,UpdateFlag) Values({type},{device.DeviceId},{empId},0)";
                DbCommand cmd = helper.GetSqlStringCommond(sql);
                helper.ExecuteNonQuery(cmd);
            }
        }
        #endregion

        #region 16进制转数组
        private static byte[] HexToArray(string s)
        {
            int length = 4;
            byte[] arr = new byte[length];
            s = s.Trim();
            if (s.Equals(string.Empty))
            {
                return null;
            }
            int len = s.Length;
            if (s.Length < (length * 2))
            {
                for (int i = 0; i < (length * 2 - len); i++)
                {
                    s = "0" + s;
                }
            }
            if (len > (length * 2))
            {
                s = s.Substring(0, length * 2);
            }

            for (int j = 0; j < length; j++)
            {
                arr[j] = Convert.ToByte(s.Substring((j * 2), 2), 16);
            }

            return arr;
        }
        #endregion


        #region DataTable To EmpInfoes
        public static List<EmpInfo> DataTableToEmpInfoes(DataTable dt)
        {
            List<EmpInfo> emps = new List<EmpInfo>();
            foreach (DataRow row in dt.Rows)
            {
                EmpInfo emp = new EmpInfo();
                emp.EmpId = Convert.ToInt32(row["EmpId"]);
                emp.EmpCode = row["EmpCode"].ToString();
                emp.EmpName = row["EmpName"].ToString();
                emp.IOFlag = Convert.ToInt32(row["IOFlag"]);
                emp.EnglishName = row["EnglishName"].ToString();
                emp.Sex = row["Sex"].ToString();
                emp.IdentityCard = row["IdentityCard"].ToString();
                emp.DeptId = Convert.ToInt32(row["DeptId"]);
                emp.DeptName = row["DeptName"].ToString();
                emp.Telephone = row["TelePhone"].ToString();
                emp.BirthDay = Convert.ToDateTime(row["BirthDay"]).ToString("yyyy-MM-dd");
                emp.Nation = row["Nationality"].ToString();
                emp.BornEarth = row["BornEarth"].ToString();
                emp.Marrige = row["Marrige"].ToString();
                emp.JoinDate = Convert.ToDateTime(row["JoinDate"]).ToString("yyyy-MM-dd");
                byte[] array = (byte[])row["Photo"];
                emp.Photo = ArrayToImage(array);
                emp.TicketType = Convert.ToInt32(row["TicketType"]);
                emp.BeginDate = Convert.ToDateTime(row["BeginDate"]).ToString("yyyy-MM-dd");
                emp.EndDate = Convert.ToDateTime(row["EndDate"]).ToString("yyyy-MM-dd");
                emp.ICCardNo = row["ICCardNo"].ToString();
                emp.IDSerial = row["IDSerial"].ToString();
                emp.IDCardNo = row["IDCardNo"].ToString();
                emp.Duty = row["Duty"].ToString();
                emp.Rehire = Convert.ToInt32(row["ReHire"]);
                emp.HireTimes = Convert.ToInt32(row["HireTimes"]);
                emp.Status = Convert.ToInt32(row["Status"]);
                emp.LeaveDate = Convert.ToDateTime(row["LeaveDate"]).ToString("yyyy-MM-dd");
                emp.FingerData1 = (byte[])row["FingerData1"];
                emp.FingerData2 = (byte[])row["FingerData2"];
                emp.FaceData = ArrayToFaceData((byte[])row["FaceData"]);
                emps.Add(emp);
            }
            return emps;
        }

        #endregion

        #region DataTable To EmpInfo
        public static List<EmpInfo> DataTableToEmpInfo(DataTable dt)
        {
            List<EmpInfo> emps = new List<EmpInfo>();
            foreach (DataRow row in dt.Rows)
            {
                EmpInfo emp = new EmpInfo();
                emp.EmpId = Convert.ToInt32(row["EmpId"]);
                emp.EmpCode = row["EmpCode"].ToString();
                emp.EmpName = row["EmpName"].ToString();
                emp.IOFlag = Convert.ToInt32(row["IOFlag"]);
                emp.EnglishName = row["EnglishName"].ToString();
                emp.Sex = row["Sex"].ToString();
                emp.IdentityCard = row["IdentityCard"].ToString();
                emp.DeptId = Convert.ToInt32(row["DeptId"]);
                emp.DeptName = row["DeptName"].ToString();
                emp.Telephone = row["TelePhone"].ToString();
                emp.BirthDay = Convert.ToDateTime(row["BirthDay"]).ToString("yyyy-MM-dd");
                emp.Nation = row["Nationality"].ToString();
                emp.BornEarth = row["BornEarth"].ToString();
                emp.Marrige = row["Marrige"].ToString();
                emp.JoinDate = Convert.ToDateTime(row["JoinDate"]).ToString("yyyy-MM-dd");
                byte[] array = (byte[])row["Photo"];
                emp.Photo = ArrayToImage(array);
                emp.TicketType = Convert.ToInt32(row["TicketType"]);
                emp.BeginDate = Convert.ToDateTime(row["BeginDate"]).ToString("yyyy-MM-dd");
                emp.EndDate = Convert.ToDateTime(row["EndDate"]).ToString("yyyy-MM-dd");
                emp.ICCardNo = row["ICCardNo"].ToString();
                emp.IDSerial = row["IDSerial"].ToString();
                emp.IDCardNo = row["IDCardNo"].ToString();
                emp.Duty = row["Duty"].ToString();
                emp.Rehire = Convert.ToInt32(row["ReHire"]);
                emp.HireTimes = Convert.ToInt32(row["HireTimes"]);
                emp.Status = Convert.ToInt32(row["Status"]);
                emp.LeaveDate = Convert.ToDateTime(row["LeaveDate"]).ToString("yyyy-MM-dd");
                byte[] finger1 = (byte[])row["FingerData1"];
                if (finger1 == null || finger1.Length < 100)
                    emp.FingerData1 = null;
                else
                    emp.FingerData1 = finger1;
                byte[] finger2 = (byte[])row["FingerData2"];
                if (finger2 == null || finger2.Length < 100)
                    emp.FingerData2 = null;
                else
                    emp.FingerData2 = finger2;
                emp.FaceData = ArrayToFaceData((byte[])row["FaceData"]);
                emp.FaceStatus = row["FaceStatus"].ToString();
                emps.Add(emp);
            }
            return emps;
        }

        #endregion
    }
}
