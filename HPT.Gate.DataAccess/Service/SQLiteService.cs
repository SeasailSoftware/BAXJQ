using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Data.Common;
using HPT.Gate.DataAccess.Entity;

namespace hpt.gate.DbTools.Service
{
    public class SQLiteService
    {

        #region 加载Led控制器列表
        /// <summary>
        /// 加载LED控制卡列表
        /// </summary>
        public static DataTable LoadLEDController()
        {
            using (SQLiteHelper SQLiteHelper = new SQLiteHelper())
            {
                string sql = "Select Lid,Width,Heigth,IPaddress,Port from Led_LEDController";
                DbCommand cmd = SQLiteHelper.GetSqlStringCommond(sql);
                DataTable dt = SQLiteHelper.ExecuteDataTable(cmd);
                return dt;
            }
        }

        public static int GetUseableParaId()
        {
            using (SQLiteHelper SQLiteHelper = new SQLiteHelper())
            {
                int paraId = 0;
                string sql = "Select IfNull(Max(ParaId)+1,1) As ParaId from DynPara";
                DbCommand cmd = SQLiteHelper.GetSqlStringCommond(sql);
                DataTable dt = SQLiteHelper.ExecuteDataTable(cmd);
                foreach (DataRow row in dt.Rows)
                {
                    paraId = Convert.ToInt32(row["ParaId"]);
                }
                return paraId;
            }
        }

        #endregion

        #region 检测Led控制器是否已经存在
        /// <summary>
        /// 检测Led控制器是否已经存在
        /// </summary>
        /// <param name="ipaddress"></param>
        /// <returns></returns>
        public static bool CheckLEDControllerExists(string ipaddress)
        {
            using (SQLiteHelper SQLiteHelper = new SQLiteHelper())
            {
                string sql = "Select top 1 * from Led_LEDController where IPAddress ='" + ipaddress + "'";
                DbCommand cmd = SQLiteHelper.GetSqlStringCommond(sql);
                DataTable dt = SQLiteHelper.ExecuteDataTable(cmd);
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

        public static string ExcuteSqlString(string connectString, string sql)
        {
            using (SQLiteHelper SQLiteHelper = new SQLiteHelper(connectString))
            {
                string result = string.Empty;
                DbCommand cmd = SQLiteHelper.GetSqlStringCommond(sql);
                DataTable dt = SQLiteHelper.ExecuteDataTable(cmd);
                foreach (DataRow row in dt.Rows)
                {
                    result = row[0].ToString();
                }
                return result;
            }
        }
        #endregion

        #region 添加Led控制器
        /// <summary>
        /// 添加Led控制器
        /// </summary>
        /// <param name="nControlType"></param>
        /// <param name="protocol"></param>
        /// <param name="width"></param>
        /// <param name="heigth"></param>
        /// <param name="ipaddress"></param>
        /// <param name="port"></param>
        public static void AddLEDController(int nControlType, int protocol, int width, int heigth, string ipaddress, int port)
        {
            using (SQLiteHelper SQLiteHelper = new SQLiteHelper())
            {
                string sql = "Insert Into Led_LedController(ControlType,Protocol,Width,Heigth,IPAddress,Port) values(" + nControlType + "," + protocol + "," + width + "," + heigth + ",'" + ipaddress + "'," + port + ")";
                DbCommand cmd = SQLiteHelper.GetSqlStringCommond(sql);
                SQLiteHelper.ExecuteNonQuery(cmd);
            }
        }



        public static void AddDynPara(DynPara _DynPara)
        {
            using (SQLiteHelper SQLiteHelper = new SQLiteHelper())
            {
                string sql = string.Format(@"Insert Into DynPara(ParaId,ParaName,Server,DBName,UserName,Password,ParaValue) values({0},'{1}','{2}','{3}','{4}','{5}','{6}')", _DynPara.ParaId, _DynPara.ParaName, _DynPara.Server, _DynPara.DBName, _DynPara.UserName, _DynPara.Password, _DynPara.ParaValue.Replace("'", "''"));
                DbCommand cmd = SQLiteHelper.GetSqlStringCommond(sql);
                SQLiteHelper.ExecuteNonQuery(cmd);
            }
        }
        #endregion

        #region 获取Led控制器编号
        /// <summary>
        /// 获取Led控制器编号
        /// </summary>
        /// <param name="ipaddress"></param>
        /// <returns></returns>
        public static int GetLID(string ipaddress)
        {
            using (SQLiteHelper SQLiteHelper = new SQLiteHelper())
            {
                string sql = "Select LID from Led_LEDController where IPAddress = '" + ipaddress + "'";
                DbCommand cmd = SQLiteHelper.GetSqlStringCommond(sql);
                DataTable dt = SQLiteHelper.ExecuteDataTable(cmd);
                int lId = 0;
                foreach (DataRow row in dt.Rows)
                {
                    lId = Convert.ToInt32(row["LID"]);
                }
                return lId;
            }
        }




        #endregion

        #region 修改Led控制器信息
        public static void UpdateLEDController(int _LID, int nControlType, int protocol, int width, int heigth, string ipaddress, int port)
        {
            using (SQLiteHelper SQLiteHelper = new SQLiteHelper())
            {
                string sql = "Update Led_LedController Set ";
                sql += " ControlType = " + nControlType + ",";
                sql += " protocol = " + protocol + ",";
                sql += " width = " + width + ",";
                sql += " heigth = " + heigth + ",";
                sql += " ipaddress = '" + ipaddress + "',";
                sql += " port = " + port;
                sql += "  where Lid = " + _LID;
                DbCommand cmd = SQLiteHelper.GetSqlStringCommond(sql);
                SQLiteHelper.ExecuteNonQuery(cmd);
            }
        }
        #endregion

        #region 删除动态区域
        /// <summary>
        /// 删除动态区域
        /// </summary>
        /// <param name="lId"></param>
        /// <param name="areaId"></param>
        public static void DeleteArea(int lId, int areaId)
        {
            using (SQLiteHelper SQLiteHelper = new SQLiteHelper())
            {
                string sql = " Delete From Led_AreaInfo where LID =" + lId + "  and  AreaId =" + areaId.ToString();
                DbCommand cmd = SQLiteHelper.GetSqlStringCommond(sql);
                SQLiteHelper.ExecuteNonQuery(cmd);
            }
        }
        #endregion

        #region 获取动态区域信息
        /// <summary>
        /// 获取动态区域信息
        /// </summary>
        /// <param name="_LID"></param>
        /// <param name="_AreaId"></param>
        /// <returns></returns>
        public static AreaInfo GetAreaInfo(int lId, int areaId)
        {
            using (SQLiteHelper SQLiteHelper = new SQLiteHelper())
            {
                string sql = "Select  *  From Led_AreaInfo where LID =  " + lId + "  and  AreaId = " + areaId;
                DbCommand cmd = SQLiteHelper.GetSqlStringCommond(sql);
                DataTable dt = SQLiteHelper.ExecuteDataTable(cmd);
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
        #endregion

        #region 获取所有led控制器
        /// <summary>
        /// 获取所有led控制器
        /// </summary>
        /// <returns></returns>
        public static List<LedController> GetAllLedControllers()
        {
            using (SQLiteHelper SQLiteHelper = new SQLiteHelper())
            {
                List<LedController> controllers = new List<LedController>();
                string sql = "Select * from Led_LEDController ";
                DbCommand cmd = SQLiteHelper.GetSqlStringCommond(sql);
                DataTable dt = SQLiteHelper.ExecuteDataTable(cmd);
                LedController ledController = null;
                foreach (DataRow row in dt.Rows)
                {
                    ledController = new LedController();
                    ledController.ControlType = Convert.ToInt32(row["ControlType"]);
                    ledController.Lid = Convert.ToInt32(row["LID"]);
                    ledController.Heigth = Convert.ToInt32(row["Heigth"]);
                    ledController.IPaddress = row["IPAddress"].ToString();
                    ledController.Port = Convert.ToInt32(row["Port"]);
                    ledController.Protocol = Convert.ToInt32(row["Protocol"]);
                    ledController.Width = Convert.ToInt32(row["Width"]);
                    controllers.Add(ledController);
                }
                return controllers;
            }
        }
        #endregion

        #region 获取Led控制器信息
        /// <summary>
        /// 获取Led控制器信息
        /// </summary>
        /// <param name="_LID"></param>
        /// <returns></returns>
        public static LedController GetLedController(int lId)
        {
            using (SQLiteHelper SQLiteHelper = new SQLiteHelper())
            {
                string sql = "Select * from Led_LEDController where LID = " + lId;
                DbCommand cmd = SQLiteHelper.GetSqlStringCommond(sql);
                DataTable dt = SQLiteHelper.ExecuteDataTable(cmd);
                LedController ledController = null;
                foreach (DataRow row in dt.Rows)
                {
                    ledController = new LedController();
                    ledController.ControlType = Convert.ToInt32(row["ControlType"]);
                    ledController.Lid = Convert.ToInt32(row["LID"]);
                    ledController.Heigth = Convert.ToInt32(row["Heigth"]);
                    ledController.IPaddress = row["IPAddress"].ToString();
                    ledController.Port = Convert.ToInt32(row["Port"]);
                    ledController.Protocol = Convert.ToInt32(row["Protocol"]);
                    ledController.Width = Convert.ToInt32(row["Width"]);
                }
                return ledController;
            }
        }

        #endregion

        #region 加载动态区域列表
        /// <summary>
        /// 加载动态区域列表
        /// </summary>
        /// <param name="_LID"></param>
        /// <returns></returns>
        public static DataTable LoadDynamicAreaList(int lId)
        {
            using (SQLiteHelper SQLiteHelper = new SQLiteHelper())
            {
                string sql = "Select AreaId,Point_X,Point_Y,Width,Height,Text  From Led_AreaInfo where LID =  " + lId;
                DbCommand cmd = SQLiteHelper.GetSqlStringCommond(sql);
                DataTable dt = SQLiteHelper.ExecuteDataTable(cmd);
                return dt;
            }
        }

        #endregion

        #region 获取可用动态区域编号
        public static int GetAreaId(int lid)
        {
            using (SQLiteHelper SQLiteHelper = new SQLiteHelper())
            {
                string sql = "select  IfNull(Min(t.AreaId),0)  as AreaId from (Select RecId as AreaId from HPT where RecId >=0 and RecId <=3)  t where t.AreaId not in (select AreaId  from Led_AreaInfo where LId =" + lid + ")";
                DbCommand cmd = SQLiteHelper.GetSqlStringCommond(sql);
                DataTable dt = SQLiteHelper.ExecuteDataTable(cmd);
                int areaId = 0;
                foreach (DataRow row in dt.Rows)
                {
                    areaId = Convert.ToInt32(row["AreaId"]);
                }
                return areaId;
            }
        }
        #endregion


        #region 添加动态区域
        public static void AddDynamicArea(AreaInfo area)
        {
            using (SQLiteHelper SQLiteHelper = new SQLiteHelper())
            {
                string sql = "Insert Into Led_AreaInfo(AreaId,BorderEffect,BorderLength,BorderNo,BorderSpeed,BordreType,DisplayEffect,Height,LID,Point_X,Point_Y,SingleLine,Speed,Stay,Text,TextBold,TextFont,TextFontSize,Width,Interval)";
                sql += " Values (@AreaId,@BorderEffect,@BorderLength,@BorderNo,@BorderSpeed,@BordreType,@DisplayEffect,@Height,@LID,@Point_X,@Point_Y,@SingleLine,@Speed,@Stay,@Text,@TextBold,@TextFont,@TextFontSize,@Width,@Interval)";
                DbCommand cmd = SQLiteHelper.GetSqlStringCommond(sql);
                SQLiteHelper.AddInParameter(cmd, "@AreaId", DbType.Int32, area.AreaId);
                SQLiteHelper.AddInParameter(cmd, "@BorderEffect", DbType.Int32, area.BorderEffect);
                SQLiteHelper.AddInParameter(cmd, "@BorderLength", DbType.Int32, area.BorderLength);
                SQLiteHelper.AddInParameter(cmd, "@BorderNo", DbType.Int32, area.BorderNo);
                SQLiteHelper.AddInParameter(cmd, "@BorderSpeed", DbType.Int32, area.BorderSpeed);
                SQLiteHelper.AddInParameter(cmd, "@BordreType", DbType.Int32, area.BordreType);
                SQLiteHelper.AddInParameter(cmd, "@DisplayEffect", DbType.Int32, area.DisplayEffect);
                SQLiteHelper.AddInParameter(cmd, "@Height", DbType.Int32, area.Height);
                SQLiteHelper.AddInParameter(cmd, "@LID", DbType.Int32, area.LID);
                SQLiteHelper.AddInParameter(cmd, "@Point_X", DbType.Int32, area.Point_X);
                SQLiteHelper.AddInParameter(cmd, "@Point_Y", DbType.Int32, area.Point_Y);
                SQLiteHelper.AddInParameter(cmd, "@SingleLine", DbType.Int32, area.SingleLine);
                SQLiteHelper.AddInParameter(cmd, "@Speed", DbType.Int32, area.Speed);
                SQLiteHelper.AddInParameter(cmd, "@Stay", DbType.Int32, area.Stay);
                SQLiteHelper.AddInParameter(cmd, "@Text", DbType.String, area.Text);
                SQLiteHelper.AddInParameter(cmd, "@TextBold", DbType.Int32, area.TextBold);
                SQLiteHelper.AddInParameter(cmd, "@TextFont", DbType.String, area.TextFont);
                SQLiteHelper.AddInParameter(cmd, "@TextFontSize", DbType.Int32, area.TextFontSize);
                SQLiteHelper.AddInParameter(cmd, "@Width", DbType.Int32, area.Width);
                SQLiteHelper.AddInParameter(cmd, "@Interval", DbType.Int32, area.Interval);
                SQLiteHelper.ExecuteNonQuery(cmd);
            }
        }
        #endregion

        #region 更新动态区域信息
        /// <summary>
        /// 更新动态区域信息
        /// </summary>
        /// <param name="area"></param>
        public static void UpdateDynamicArea(AreaInfo area)
        {
            using (SQLiteHelper SQLiteHelper = new SQLiteHelper())
            {
                string sql = "Update Led_AreaInfo set ";
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
                DbCommand cmd = SQLiteHelper.GetSqlStringCommond(sql);
                SQLiteHelper.ExecuteNonQuery(cmd);
            }
        }

        #endregion

        #region 获取动态区域列表
        /// <summary>
        /// 获取动态区域列表
        /// </summary>
        /// <param name="lid"></param>
        /// <returns></returns>
        public static List<AreaInfo> GetAreaList(int lid)
        {
            using (SQLiteHelper SQLiteHelper = new SQLiteHelper())
            {
                string sql = "select * from Led_AreaInfo where Lid = " + lid;
                DbCommand cmd = SQLiteHelper.GetSqlStringCommond(sql);
                DataTable dt = SQLiteHelper.ExecuteDataTable(cmd);
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
        #endregion

        #region 获取最后第N条记录
        public static List<string> GetLastIndexRecord(int recordIndex)
        {
            List<string> list = new List<string>();
            using (SQLiteHelper SQLiteHelper = new SQLiteHelper())
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
                DbCommand cmd = SQLiteHelper.GetSqlStringCommond(sql);
                DataTable dt = SQLiteHelper.ExecuteDataTable(cmd);
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

        #region 获取场内人数
        /// <summary>
        /// 统计场内总人数
        /// </summary>
        /// <returns></returns>
        public static string GetIOTotal()
        {
            string total = "0";
            using (SQLiteHelper SQLiteHelper = new SQLiteHelper())
            {
                string sql = "Select Count(*) as Total from EmpInfo a,CardInfo b where a.EmpId = b.EmpId and b.Cardstatus = 1 and  b.IOFlag = 4";
                DbCommand cmd = SQLiteHelper.GetSqlStringCommond(sql);
                DataTable dt = SQLiteHelper.ExecuteDataTable(cmd);
                foreach (DataRow row in dt.Rows)
                {
                    total = Convert.ToInt32(row["Total"]).ToString("0000");
                }
            }
            return total;
        }
        #endregion

        #region 获取进场总人数
        public static string GetRecordsOfIn()
        {
            int total = 0;
            using (SQLiteHelper SQLiteHelper = new SQLiteHelper())
            {
                string sql = string.Format("Select Count(*) As Total from Record where IOFlag = '进' and RecordType = '有效票' and recDatetime >= '{0}'", DateTime.Now.ToString("yyyy-MM-dd"));
                DbCommand cmd = SQLiteHelper.GetSqlStringCommond(sql);
                DataTable dt = SQLiteHelper.ExecuteDataTable(cmd);
                foreach (DataRow row in dt.Rows)
                {
                    total = Convert.ToInt32(row["Total"]);
                }
            }
            return total.ToString("0000");
        }

        #endregion

        #region 获取部门场内人数
        public static string GetCountOfInByDeptId(string deptId)
        {
            using (SQLiteHelper SQLiteHelper = new SQLiteHelper())
            {
                string count = string.Empty;
                string sql = "Select Count(*) from EmpInfo a,CardInfo b where a.EmpId = b.Empid and b.CardStatus = 1 and b.IOFlag = 4 and a.DeptId =" + deptId;
                DbCommand cmd = SQLiteHelper.GetSqlStringCommond(sql);
                DataTable dt = SQLiteHelper.ExecuteDataTable(cmd);
                foreach (DataRow row in dt.Rows)
                {
                    count = Convert.ToInt32(row[0]).ToString("0000");
                }
                return count;
            }
        }
        #endregion


        #region 获取当天出场人数

        public static string GetRecordOfOut()
        {
            int total = 0;
            using (SQLiteHelper SQLiteHelper = new SQLiteHelper())
            {
                string sql = string.Format("Select Count(*) As Total from Record where IOFlag = '出' and RecordType = '有效票' and recDatetime >= '{0}'", DateTime.Now.ToString("yyyy-MM-dd"));
                DbCommand cmd = SQLiteHelper.GetSqlStringCommond(sql);
                DataTable dt = SQLiteHelper.ExecuteDataTable(cmd);
                foreach (DataRow row in dt.Rows)
                {
                    total = Convert.ToInt32(row["Total"]);
                }
            }
            return total.ToString("0000");
        }

        #endregion

        #region 检查Led更新
        public static void CheckLedUpdate()
        {

        }
        #endregion

        #region 获取动态参数列表
        public static DataTable GetDynParaList()
        {
            using (SQLiteHelper SQLiteHelper = new SQLiteHelper())
            {
                string sql = "Select * from DynPara";
                DbCommand cmd = SQLiteHelper.GetSqlStringCommond(sql);
                DataTable dt = SQLiteHelper.ExecuteDataTable(cmd);
                return dt;
            }
        }
        #endregion

        #region 删除Led控制卡
        /// <summary>
        /// 删除动态区域
        /// </summary>
        /// <param name="lId"></param>
        /// <param name="areaId"></param>
        public static void DeleteLedController(string ipAddress)
        {
            using (SQLiteHelper SQLiteHelper = new SQLiteHelper())
            {
                string sql = string.Format(" Delete From Led_LedController where IPAddress ='{0}'", ipAddress);
                DbCommand cmd = SQLiteHelper.GetSqlStringCommond(sql);
                SQLiteHelper.ExecuteNonQuery(cmd);
            }
        }
        #endregion

        public static void AddLEDController(int nControlType, int protocol, int width, int heigth, string ipaddress, int port, List<int> devList, int totalRecord)
        {
            throw new NotImplementedException();
        }

        #region SQLite

        #region 检查控制卡是否已经存在
        public static bool SQLiteCheckLEDControllerExists(string ipaddress)
        {
            using (SQLiteHelper SQLiteHelper = new SQLiteHelper())
            {
                string sql = string.Format("Select * from Led_LEDController where IPAddress ='{0}' Order By Lid Limit 0,1", ipaddress);
                DbCommand cmd = SQLiteHelper.GetSqlStringCommond(sql);
                DataTable dt = SQLiteHelper.ExecuteDataTable(cmd);
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

        #region 添加Led控制器
        /// <summary>
        /// 添加Led控制器
        /// </summary>
        /// <param name="nControlType"></param>
        /// <param name="protocol"></param>
        /// <param name="width"></param>
        /// <param name="heigth"></param>
        /// <param name="ipaddress"></param>
        /// <param name="port"></param>
        public static void SQLiteAddLEDController(int lid, int nControlType, int protocol, int width, int heigth, string ipaddress, int port)
        {
            using (SQLiteHelper SQLiteHelper = new SQLiteHelper())
            {
                string sql = "Insert Into Led_LedController(Lid,ControlType,Protocol,Width,Heigth,IPAddress,Port) values(" + lid + "," + nControlType + "," + protocol + "," + width + "," + heigth + ",'" + ipaddress + "'," + port + ")";
                DbCommand cmd = SQLiteHelper.GetSqlStringCommond(sql);
                SQLiteHelper.ExecuteNonQuery(cmd);
            }
        }
        #endregion

        #region 获取可用动态区域编号
        public static int SQLiteGetAreaId(int lid)
        {
            using (SQLiteHelper SQLiteHelper = new SQLiteHelper())
            {
                string sql = "select  Min(t.AreaId)  as AreaId from (Select RecId as AreaId from HPT where RecId >=0 and RecId <=3) t where t.AreaId not in (select AreaId  from Led_AreaInfo where LId =" + lid + ")";
                DbCommand cmd = SQLiteHelper.GetSqlStringCommond(sql);
                DataTable dt = SQLiteHelper.ExecuteDataTable(cmd);
                int areaId = 0;
                foreach (DataRow row in dt.Rows)
                {
                    areaId = Convert.ToInt32(row["AreaId"]);
                }
                return areaId;
            }
        }
        #endregion

        #region 获取Led控制器信息
        /// <summary>
        /// 获取Led控制器信息
        /// </summary>
        /// <param name="_LID"></param>
        /// <returns></returns>
        public static LedController GetLedController(string ipAddress)
        {
            using (SQLiteHelper SQLiteHelper = new SQLiteHelper())
            {
                string sql = string.Format("Select * from Led_LEDController where IPAddress = '{0}'", ipAddress);
                DbCommand cmd = SQLiteHelper.GetSqlStringCommond(sql);
                DataTable dt = SQLiteHelper.ExecuteDataTable(cmd);
                LedController ledController = null;
                foreach (DataRow row in dt.Rows)
                {
                    ledController = new LedController();
                    ledController.ControlType = Convert.ToInt32(row["ControlType"]);
                    ledController.Lid = Convert.ToInt32(row["LID"]);
                    ledController.Heigth = Convert.ToInt32(row["Heigth"]);
                    ledController.IPaddress = row["IPAddress"].ToString();
                    ledController.Port = Convert.ToInt32(row["Port"]);
                    ledController.Protocol = Convert.ToInt32(row["Protocol"]);
                    ledController.Width = Convert.ToInt32(row["Width"]);
                }
                return ledController;
            }
        }

        #endregion

        #region 获取动态参数信息
        public static DynPara GetDynParaByRecId(int recId)
        {
            using (SQLiteHelper SQLiteHelper = new SQLiteHelper())
            {
                string sql = string.Format("Select * from DynPara where RecId = {0}", recId);
                DbCommand cmd = SQLiteHelper.GetSqlStringCommond(sql);
                DataTable dt = SQLiteHelper.ExecuteDataTable(cmd);
                DynPara para = null;
                foreach (DataRow row in dt.Rows)
                {
                    para = new DynPara();
                    para.ParaId = Convert.ToInt32(row["ParaId"]);
                    para.ParaName = row["ParaName"].ToString();
                    para.ParaValue = row["ParaValue"].ToString();
                    para.Server = row["Server"].ToString();
                    para.DBName = row["DBName"].ToString();
                    para.UserName = row["UserName"].ToString();
                    para.Password = row["Password"].ToString();
                }
                return para;
            }
        }
        #endregion

        #region 修改动态参数信息
        public static void EditDynPara(DynPara _DynPara)
        {
            using (SQLiteHelper SQLiteHelper = new SQLiteHelper())
            {
                string sql = string.Format(@"Update DynPara Set ParaName ='{0}',Server='{1}',DBName ='{2}',UserName ='{3}',Password ='{4}',ParaValue ='{5}' Where RecId = {6}", _DynPara.ParaName, _DynPara.Server, _DynPara.DBName, _DynPara.UserName, _DynPara.Password, _DynPara.ParaValue.Replace("'", "''"), _DynPara.RecId);
                DbCommand cmd = SQLiteHelper.GetSqlStringCommond(sql);
                SQLiteHelper.ExecuteNonQuery(cmd);
            }
        }
        #endregion

        #region 删除动态参数
        public static void DeleteDynPara(int recId)
        {
            using (SQLiteHelper SQLiteHelper = new SQLiteHelper())
            {
                string sql = string.Format("Delete From DynPara where RecId ={0}", recId);
                DbCommand cmd = SQLiteHelper.GetSqlStringCommond(sql);
                SQLiteHelper.ExecuteNonQuery(cmd);
            }
        }
        #endregion

        #region 根据动态参数编号得到对应的内容
        public static string GetContentByParaId(int curIndex)
        {
            string result = string.Empty;
            DynPara para = null;
            using (SQLiteHelper SQLiteHelper = new SQLiteHelper())
            {
                string sql = string.Format("Select * from DynPara where ParaId = {0}", curIndex);
                DbCommand cmd = SQLiteHelper.GetSqlStringCommond(sql);
                DataTable dt = SQLiteHelper.ExecuteDataTable(cmd);
                para = null;
                foreach (DataRow row in dt.Rows)
                {
                    para = new DynPara();
                    para.ParaId = Convert.ToInt32(row["ParaId"]);
                    para.ParaName = row["ParaName"].ToString();
                    para.ParaValue = row["ParaValue"].ToString();
                    para.Server = row["Server"].ToString();
                    para.DBName = row["DBName"].ToString();
                    para.UserName = row["UserName"].ToString();
                    para.Password = row["Password"].ToString();
                }
            }
            if (para != null)
            {
                if (!para.ParaValue.Equals(string.Empty))
                {
                    using (SQLiteHelper dbHelper = new SQLiteHelper(para.ConnectString))
                    {
                        try
                        {
                            DbCommand cmd = dbHelper.GetSqlStringCommond(para.ParaValue);
                            DataTable dt = dbHelper.ExecuteDataTable(cmd);
                            foreach (DataRow row in dt.Rows)
                            {
                                result = row[0].ToString();
                            }
                        }
                        catch
                        {
                        }
                    }
                }
            }
            return result;
        }
        #endregion

        #endregion

    }
}
