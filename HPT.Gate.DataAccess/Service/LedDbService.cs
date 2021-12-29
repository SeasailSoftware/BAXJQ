using HPT.Gate.DataAccess.Entity;
using HPT.Gate.Utils.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Linq;

namespace hpt.gate.DbTools.Service
{
    public class LedDbService
    {
        #region 添加Led控制卡
        public static bool AddController(LedController controller, out string msg)
        {
            List<LedController> controllers = GetAllLedControllers();
            if (controllers.Exists(p => p.Lid == controller.Lid))
            {
                msg = $"屏号[{controller.Lid}]已重复!";
                return false;
            }
            if (controllers.Exists(p => p.IPaddress.Equals(controller.IPaddress)))
            {
                msg = $"IP地址[{controller.IPaddress}]已重复!";
                return false;
            }
            try
            {
                string devices = string.Empty;
                foreach (int value in controller.Devices)
                {
                    devices += $"{value},";
                }
                using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
                {
                    StringBuilder buffer = new StringBuilder();
                    buffer.Append($"Insert Into Led_LedController(Lid,ControlType,Protocol,Width,Heigth,IPAddress,Port,Devices) values({controller.Lid},{controller.ControlType},{controller.Protocol},{controller.Width},{controller.Heigth},'{controller.IPaddress}',{controller.Port},'{devices}')");
                    buffer.AppendLine($"Delete From Led_AreaInfo Where Lid={controller.Lid} ");
                    foreach (AreaInfo area in controller.DynAreas)
                    {
                        buffer.AppendLine($"Insert  Led_AreaInfo(AreaId,BorderEffect,BorderLength,BorderNo,BorderSpeed,BordreType,DisplayEffect,Height,LID,Point_X,Point_Y,SingleLine,Speed,Stay,Text,TextBold,TextFont,TextFontSize,Width,Interval)");
                        buffer.Append($" Values ({area.AreaId},{area.BorderEffect},{area.BorderLength},{area.BorderNo},{area.BorderSpeed},{area.BordreType},");
                        buffer.Append($"{area.DisplayEffect},{area.Height},{area.LID},{area.Point_X},{area.Point_Y},{area.SingleLine},{area.Speed},{area.Stay},'{area.Text}',");
                        buffer.Append($"{area.TextBold},'{area.TextFont}',{area.TextFontSize},{area.Width},{area.Interval})");
                    }
                    DbCommand cmd = dbHelper.GetSqlStringCommond(buffer.ToString());
                    dbHelper.ExecuteNonQuery(cmd);
                }
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


        #region 查找合适的控制卡屏号
        public static int GetSuitableLid()
        {
            using (SQLiteHelper dbHelper = new SQLiteHelper())
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

        #region 加载Led控制器列表
        /// <summary>
        /// 加载LED控制卡列表
        /// </summary>
        public static DataTable LoadLEDController()
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "Select Lid,Width,Heigth,IPaddress,Port from Led_LEDController";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                return dt;
            }
        }

        public static decimal GetUseableParaId()
        {
            throw new NotImplementedException();
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
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "Select top 1 * from Led_LEDController where IPAddress ='" + ipaddress + "'";
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

        public static bool CheckLEDControllerExists(int lid)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = string.Format("Select top 1 * from Led_LEDController where Lid ={0}", lid); ;
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

        public static List<LedController> GetLedControllerByDeviceId(int deviceId)
        {
            return GetAllLedControllers().Where(p => p.Devices.Contains(deviceId)).ToList();
        }

        public static string ExcuteSqlString(string sql)
        {
            throw new NotImplementedException();
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
        public static void AddLEDController(int lid, int nControlType, int protocol, int width, int heigth, string ipaddress, int port)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "Insert Into Led_LedController(Lid,ControlType,Protocol,Width,Heigth,IPAddress,Port) values(" + lid + "," + nControlType + "," + protocol + "," + width + "," + heigth + ",'" + ipaddress + "'," + port + ")";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }



        public static void AddDynPara(DynPara _DynPara)
        {
            throw new NotImplementedException();
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
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "Select LID from Led_LEDController where IPAddress = '" + ipaddress + "'";
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


        public static void EditDynPara(DynPara _DynPara)
        {
            throw new NotImplementedException();
        }

        public static void DeleteDynPara(int recId)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region 修改Led控制器信息
        public static void UpdateLEDController(int _LID, int nControlType, int protocol, int width, int heigth, string ipaddress, int port)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "Update Led_LedController Set ";
                sql += " ControlType = " + nControlType + ",";
                sql += " protocol = " + protocol + ",";
                sql += " width = " + width + ",";
                sql += " heigth = " + heigth + ",";
                sql += " ipaddress = '" + ipaddress + "',";
                sql += " port = " + port;
                sql += "  where Lid = " + _LID;
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
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
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = " Delete From Led_AreaInfo where LID =" + lId + "  and  AreaId =" + areaId.ToString();
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
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
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "Select  *  From Led_AreaInfo where LID =  " + lId + "  and  AreaId = " + areaId;
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

        #endregion

        public static List<LedController> GetByDeviceId(int machineId)
        {
            return GetAllLedControllers().Where(p => p.Devices.Contains(machineId)).ToList();
        }


        #region 获取所有led控制器
        /// <summary>
        /// 获取所有led控制器
        /// </summary>
        /// <returns></returns>
        public static List<LedController> GetAllLedControllers()
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                List<LedController> controllers = new List<LedController>();
                string sql = "Select * from Led_LEDController ";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                return DataTableToControllers(dt);

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
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "Select * from Led_LEDController where LID = " + lId;
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                List<LedController> controllers = DataTableToControllers(dt);
                if (controllers.Count == 0) return null;
                return controllers[0];
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
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "Select AreaId,Point_X,Point_Y,Width,Height,Text  From Led_AreaInfo where LID =  " + lId;
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                return dt;
            }
        }

        #endregion

        #region 获取可用动态区域编号
        public static int GetAreaId(int lid)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "select  Isnull(Min(t.AreaId),0)  as AreaId from (Select Vid as AreaId from Voice where Vid >=0 and Vid <=3)  t where t.AreaId not in (select AreaId  from Led_AreaInfo where LId =" + lid + ")";
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
        #endregion

        #region 添加动态区域
        public static void AddDynamicArea(AreaInfo area)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = $"{Environment.NewLine}Insert  Led_AreaInfo(AreaId,BorderEffect,BorderLength,BorderNo,BorderSpeed,BordreType,DisplayEffect,Height,LID,Point_X,Point_Y,SingleLine,Speed,Stay,Text,TextBold,TextFont,TextFontSize,Width,Interval)";
                sql += $" Values ({area.AreaId},{area.BorderEffect},{area.BorderLength},{area.BorderNo},{area.BorderSpeed},{area.BordreType},";
                sql += $"{area.DisplayEffect},{area.Height},{area.LID},{area.Point_X},{area.Point_Y},{area.SingleLine},{area.Speed},{area.Stay},'{area.Text}',";
                sql += $"{area.TextBold},'{area.TextFont}',{area.TextFontSize},{area.Width},{area.Interval})";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
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
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
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
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
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
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "select * from Led_AreaInfo where Lid = " + lid;
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

        public static void UpdateLEDController(LedController led)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                StringBuilder buffer = new StringBuilder();
                buffer.AppendLine($"Update Led_LedController Set  ControlType ={led.ControlType},protocol ={led.Protocol},width ={led.Width},heigth ={led.Heigth},ipaddress = '{led.IPaddress}',port ={led.Port} where Lid ={led.Lid}");
                buffer.AppendLine($"Delete From Led_AreaInfo Where Lid={led.Lid}");
                foreach (AreaInfo area in led.DynAreas)
                {
                    buffer.AppendLine($"Insert  Led_AreaInfo(AreaId,BorderEffect,BorderLength,BorderNo,BorderSpeed,BordreType,DisplayEffect,Height,LID,Point_X,Point_Y,SingleLine,Speed,Stay,Text,TextBold,TextFont,TextFontSize,Width,Interval)" +
                     $" Values ({area.AreaId},{area.BorderEffect},{area.BorderLength},{area.BorderNo},{area.BorderSpeed},{area.BordreType}," +
                     $"{area.DisplayEffect},{area.Height},{area.LID},{area.Point_X},{area.Point_Y},{area.SingleLine},{area.Speed},{area.Stay},'{area.Text}'," +
                    $"{area.TextBold},'{area.TextFont}',{area.TextFontSize},{area.Width},{area.Interval})");
                }
                DbCommand cmd = dbHelper.GetSqlStringCommond(buffer.ToString());
                dbHelper.ExecuteNonQuery(cmd);
            }
        }

        public static string GetValueOfDynPara(int curIndex, object ledNumberLength)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region 获取最后第N条记录
        public static List<string> GetLastIndexRecord(int recordIndex)
        {
            List<string> list = new List<string>();
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                StringBuilder buffer = new StringBuilder();
                /*
                buffer.Append($"{Environment.NewLine}Declare @CardNo varchar(30),@EmpName varchar(30),@IOFlag varchar(10),@RecDateTime varchar(30),@DeptName varchar(30),@EmpId int  ");
                buffer.Append($"{Environment.NewLine}Create Table #Record(RecId int,CardNo varchar(20),IOFlag varchar(10),RecDateTime varchar(30))  ");
                buffer.Append($"{Environment.NewLine}Insert  #Record Select Top {recordIndex} RecId,CardNo,IOFlag,RecDateTime From Record Where RecordType = '有效票' Order By RecId Desc  ");
                buffer.Append($"{Environment.NewLine}Select top 1 @CardNo = CardNo,@IOFlag = IOFlag,@RecDateTime = RecDateTime from #Record Order By RecId asc  ");
                buffer.Append($"{Environment.NewLine}Select @EmpId = EmpId From CardInfo where CardStatus = 1 and CardNo = @CardNo  ");
                buffer.Append($"{Environment.NewLine}Select @EmpName = EmpName From EmpInfo where EmpId = @EmpId  ");
                buffer.Append($"{Environment.NewLine}Select @DeptName = DeptName From DeptInfo where DeptId =(Select DeptId From EmpInfo where EmpId = @EmpId)  ");
                buffer.Append($"{Environment.NewLine}Select @DeptName As DeptName,@EmpName as EmpName,@IOFlag as IOFlag,@RecDateTime AS RecDateTime  ");
                buffer.Append($"{Environment.NewLine}Drop table #record  ");
                */
                buffer.Append($"{Environment.NewLine}Select top {recordIndex} d.DeptName,c.EmpName,a.RecDatetime,a.IOFlag from Record a,CardInfo b,EmpInfo c,DeptInfo d where a.CardNo = b.CardNo  and b.EmpId = c.EmpId and b.CardStatus = 1 and c.DeptId = d.Deptid order By a.RecId Desc");
                DbCommand cmd = dbHelper.GetSqlStringCommond(buffer.ToString());
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

        #region 获取场内人数
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

        #region 获取部门场内人数
        public static string GetCountOfInByDeptId(string deptId)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string count = string.Empty;
                string sql = "Select Count(*) from EmpInfo Where IOFlag = 1 and DeptId =" + deptId;
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
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

        #region 检查Led更新
        public static void CheckLedUpdate()
        {

        }
        #endregion

        #region 获取动态参数列表
        public static DataTable GetDynParaList()
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "Select * from DynPara";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                return dt;
            }
        }
        #endregion

        #region 添加部门Led动态参数
        public static void AddLedDynPara(int deptId, string deptName)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = $"If Not Exists(Select top 1 * From Led_DynPara Where ParaId ={deptId})";
                sql += $"{Environment.NewLine}Insert Led_DynPara(ParaId,ParaName,ParaSql) Values({deptId},'{deptName}','{"Select Count(1) From EmpInfo Where IOFlag = 1 And DeptId =" + deptId}')";
                sql += $"{Environment.NewLine}Else";
                sql += $"{Environment.NewLine}Update Led_DynPara Set ParaName ='{deptName}',ParaSql ='{"Select Count(1) From EmpInfo Where IOFlag = 1 And DeptId =" + deptId}' Where ParaId = {deptId}";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }
        #endregion

        #region 删除部门Led动态参数
        public static void DeleteLedDynPara(int deptId)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = $"Delete From Led_DynPara Where ParaId ={deptId}";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }
        #endregion

        #region 获取动态参数实际值
        public static string GetValueOfDynPara(int curIndex, int length)
        {
            string value = string.Empty;
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                LedDynPara para = GetLedDynPara(curIndex);
                if (para != null)
                {
                    DbCommand cmd = dbHelper.GetSqlStringCommond(para.ParaSql);
                    DataTable dt = dbHelper.ExecuteDataTable(cmd);
                    foreach (DataRow row in dt.Rows)
                    {
                        value = row[0].ToString();
                        DateTime dateTime;
                        if (DateTime.TryParse(value, out dateTime))
                        {
                            value = dateTime.ToString("HH:mm");
                        }
                        if (isNumberic(value))
                        {
                            string format = string.Empty;
                            for (int i = 0; i < length; i++)
                            {
                                format += "0";
                            }
                            value = Convert.ToInt32(value).ToString(format);
                        }
                    }
                }

            }
            return value;
        }
        #endregion

        #region 获取动态参数信息
        public static LedDynPara GetLedDynPara(int paraId)
        {
            LedDynPara para = null;
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = $"Select Top 1 *  from Led_DynPara Where ParaId ={paraId}";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
                foreach (DataRow row in dt.Rows)
                {
                    para = new LedDynPara();
                    para.RecId = Convert.ToInt32(row["RecId"]);
                    para.ParaId = Convert.ToInt32(row["ParaId"]);
                    para.ParaName = row["ParaName"].ToString();
                    para.ParaSql = row["ParaSql"].ToString();
                }
            }
            return para;
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
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = string.Format("Select * from Led_LEDController where IPAddress ='{0}' Order By Lid Limit 0,1", ipaddress);
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
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "Insert Into Led_LedController(Lid,ControlType,Protocol,Width,Heigth,IPAddress,Port) values(" + lid + "," + nControlType + "," + protocol + "," + width + "," + heigth + ",'" + ipaddress + "'," + port + ")";
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                dbHelper.ExecuteNonQuery(cmd);
            }
        }
        #endregion

        #region 获取可用动态区域编号
        public static int SQLiteGetAreaId(int lid)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = "select  Min(t.AreaId)  as AreaId from (Select RecId as AreaId from HPT where RecId >=0 and RecId <=3) t where t.AreaId not in (select AreaId  from Led_AreaInfo where LId =" + lid + ")";
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
        #endregion

        #region 获取Led控制器信息
        /// <summary>
        /// 获取Led控制器信息
        /// </summary>
        /// <param name="_LID"></param>
        /// <returns></returns>
        public static LedController GetLedController(string ipAddress)
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
            {
                string sql = string.Format("Select * from Led_LEDController where IPAddress = '{0}'", ipAddress);
                DbCommand cmd = dbHelper.GetSqlStringCommond(sql);
                DataTable dt = dbHelper.ExecuteDataTable(cmd);
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

        #endregion

        private static bool isNumberic(string message)
        {
            System.Text.RegularExpressions.Regex rex = new System.Text.RegularExpressions.Regex(@"^\d+$");
            if (rex.IsMatch(message))
                return true;
            return false;
        }

        #region DataTableToControllers
        public static List<LedController> DataTableToControllers(DataTable dt)
        {
            List<LedController> controllers = new List<LedController>();
            foreach (DataRow row in dt.Rows)
            {
                LedController ledController = new LedController();
                ledController.ControlType = Convert.ToInt32(row["ControlType"]);
                ledController.Lid = Convert.ToInt32(row["LID"]);
                ledController.Heigth = Convert.ToInt32(row["Heigth"]);
                ledController.IPaddress = row["IPAddress"].ToString();
                ledController.Port = Convert.ToInt32(row["Port"]);
                ledController.Protocol = Convert.ToInt32(row["Protocol"]);
                ledController.Width = Convert.ToInt32(row["Width"]);
                ledController.Devices = new List<int>();
                string[] devices = row["Devices"].ToString().Split(',');
                foreach (string device in devices)
                {
                    int devId;
                    if (int.TryParse(device, out devId))
                        ledController.Devices.Add(devId);
                }
                ledController.DynAreas = GetAreaList(ledController.Lid);
                controllers.Add(ledController);
            }
            return controllers;
        }
        #endregion

    }
}
