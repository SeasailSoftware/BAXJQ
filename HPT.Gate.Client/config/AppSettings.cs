using HPT.Gate.Utils.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPT.Gate.Client.config
{
    /// <summary>
    /// 系统参数配置
    /// </summary>
    public class AppSettings
    {
        #region 数据库配置

        /// <summary>
        /// 是否已经安装数据库
        /// </summary>
        public static bool IsInstall
        {
            get { return AppSettingsHelper.Get("IsInstall").ToUpper().Equals("TRUE") ? true : false; }
            set { AppSettingsHelper.Set("IsInstall", value ? "true" : "false"); }
        }
        /// <summary>
        /// 数据库登录类型
        /// </summary>
        public static string LoginType
        {
            get { return AppSettingsHelper.Get("LoginType"); }
            set { AppSettingsHelper.Set("LoginType", value); }
        }

        /// <summary>
        /// 数据库服务器名称
        /// </summary>
        public static string ServerName
        {
            get { return AppSettingsHelper.Get("ServerName"); }
            set { AppSettingsHelper.Set("ServerName", value); }
        }

        /// <summary>
        /// 数据库名称
        /// </summary>
        public static string DBName
        {
            get { return AppSettingsHelper.Get("DBName"); }
            set { AppSettingsHelper.Set("DBName", value); }
        }

        /// <summary>
        /// 登录用户名
        /// </summary>
        public static string UserName
        {
            get { return AppSettingsHelper.Get("UserName"); }
            set { AppSettingsHelper.Set("UserName", value); }
        }
        /// <summary>
        /// 登录密码
        /// </summary>
        public static string Password
        {
            get { return AppSettingsHelper.Get("Password"); }
            set { AppSettingsHelper.Set("Password", value); }
        }

        /// <summary>
        /// 连接字符串
        /// </summary>
        public static string OLEConnectString
        {
            get
            {
                string connectString = string.Empty;
                switch (LoginType)
                {
                    case "0":
                        connectString = $@"Provider = SQLOLEDB.1; Integrated Security = SSPI; Persist Security Info = False; User ID ={UserName}; Initial Catalog ={DBName}; Data Source ={ServerName}";
                        break;
                    case "1":
                        connectString = $@"Provider = SQLOLEDB.1; Password ={Password}; Persist Security Info = True; User ID = {UserName}; Initial Catalog ={DBName}; Data Source ={ServerName}";
                        break;
                }
                return connectString;
            }
        }
        /// <summary>
        /// 连接字符串
        /// </summary>
        public static string ConnectString1
        {
            get
            {
                string connectString = string.Empty;
                switch (LoginType)
                {
                    case "0":
                        connectString = @"Data Source = " + ServerName + "; Initial Catalog = master;Integrated Security=true;";
                        break;
                    case "1":
                        connectString = @"Data Source = " + ServerName + "; Initial Catalog = master;Integrated Security=false;Persist Security Info=False;User Id = " + UserName + "; Password = " + Password + ";";
                        break;
                }
                return connectString;
            }
        }

        #region 连接字符串


        #endregion

        #region

        public static bool EnableWriteCard
        {
            get { return AppSettingsHelper.Get("EnableWriteCard").ToUpper().Equals("TRUE") ? true : false; }
            set { AppSettingsHelper.Set("EnableWriteCard", value ? "true" : "false"); }
        }

        /// <summary>
        /// 读写卡密码
        /// </summary>
        public static string CardPass
        {
            get { return AppSettingsHelper.Get("CardPass"); }
            set { AppSettingsHelper.Set("CardPass", value); }
        }

        /// <summary>
        /// 读写卡密码
        /// </summary>
        public static int SectorNo
        {
            get { return Convert.ToInt32(AppSettingsHelper.Get("SectorNo")); }
            set { AppSettingsHelper.Set("SectorNo", value.ToString()); }
        }

        /// <summary>
        /// 是否启用反潜
        /// </summary>
        public static bool AntiSubmarineWarfare
        {
            get { return AppSettingsHelper.Get("AntiSubmarineWarfare").ToUpper().Equals("TRUE") ? true : false; }
            set { AppSettingsHelper.Set("AntiSubmarineWarfare", value ? "true" : "false"); }
        }

        /// <summary>
        /// 是否启用时间段内计次
        /// </summary>
        public static bool EnableLimitedTimes
        {
            get { return AppSettingsHelper.Get("EnableLimitedTimes").ToUpper().Equals("TRUE") ? true : false; }
            set { AppSettingsHelper.Set("EnableLimitedTimes", value ? "true" : "false"); }
        }

        /// <summary>
        /// 时间段内限次的次数
        /// </summary>
        public static int LimitedTimes
        {
            get { return Convert.ToInt32(AppSettingsHelper.Get("LimitedTimes")); }
            set { AppSettingsHelper.Set("LimitedTimes", value.ToString()); }
        }

        public static string AttendBeginTime
        {
            get { return AppSettingsHelper.Get("AttendBeginTime"); }
            set { AppSettingsHelper.Set("AttendBeginTime", value); }
        }

        public static string AttendEndTime
        {
            get { return AppSettingsHelper.Get("AttendEndTime"); }
            set { AppSettingsHelper.Set("AttendEndTime", value); }
        }

        public static int AttendEndDay
        {
            get { return Convert.ToInt32(AppSettingsHelper.Get("AttendEndDay")); }
            set { AppSettingsHelper.Set("AttendEndDay", value.ToString()); }
        }

        #endregion

        #endregion

        #region 二维码配置

        public static int BarcodeTimesOfIn
        {
            get { return Convert.ToInt32(AppSettingsHelper.Get("BarcodeTimesOfIn")); }
            set { AppSettingsHelper.Set("BarcodeTimesOfIn", value.ToString()); }
        }

        public static int BarcodeTimesOfOut
        {
            get { return Convert.ToInt32(AppSettingsHelper.Get("BarcodeTimesOfOut")); }
            set { AppSettingsHelper.Set("BarcodeTimesOfOut", value.ToString()); }
        }
        public static int BarcodeEffectTime
        {
            get { return Convert.ToInt32(AppSettingsHelper.Get("BarcodeEffectTime")); }
            set { AppSettingsHelper.Set("BarcodeEffectTime", value.ToString()); }
        }

        public static string BarcodeContent
        {
            get { return AppSettingsHelper.Get("BarcodeContent"); }
            set { AppSettingsHelper.Set("BarcodeContent", value); }
        }

        public static List<int> BarcodeDeviceList
        {
            get
            {
                List<int> devList = new List<int>();
                string buffer = AppSettingsHelper.Get("BarcodeDeviceList");
                if (buffer == null) return devList;
                string[] devString = buffer.Split(',');
                foreach (string s in devString)
                {
                    int temp = 0;
                    try
                    {
                        temp = Convert.ToInt32(s);
                    }
                    catch
                    {

                    }
                    if (temp != 0)
                        devList.Add(temp);
                }
                return devList;
            }
            set
            {
                StringBuilder buffer = new StringBuilder();
                if (value == null)
                    AppSettingsHelper.Set("BarcodeDeviceList", string.Empty);
                for (int i = 0; i < value.Count; i++)
                {
                    buffer.Append(value[i].ToString());
                    if (i < value.Count - 1)
                        buffer.Append(",");
                }
                AppSettingsHelper.Set("BarcodeDeviceList", buffer.ToString());
            }
        }

        #endregion

        #region 导入设置

        public static int DeptImportType
        {
            get { return Convert.ToInt32(AppSettingsHelper.Get("DeptImportType")); }
            set { AppSettingsHelper.Set("DeptImportType", value.ToString()); }
        }

        public static int EmpImportType
        {
            get { return Convert.ToInt32(AppSettingsHelper.Get("EmpImportType")); }
            set { AppSettingsHelper.Set("EmpImportType", value.ToString()); }
        }

        public static int CardImportType
        {
            get { return Convert.ToInt32(AppSettingsHelper.Get("CardImportType")); }
            set { AppSettingsHelper.Set("CardImportType", value.ToString()); }
        }

        public static int CardType
        {
            get { return Convert.ToInt32(AppSettingsHelper.Get("CardType")); }
            set { AppSettingsHelper.Set("CardType", value.ToString()); }
        }

        #endregion

        #region 卡属性

        public static int TicketType
        {
            get { return Convert.ToInt32(AppSettingsHelper.Get("TicketType")); }
            set { AppSettingsHelper.Set("TicketType", value.ToString()); }
        }

        public static string BeginDate
        {
            get { return AppSettingsHelper.Get("BeginDate"); }
            set { AppSettingsHelper.Set("BeginDate", value); }
        }

        public static string EndDate
        {
            get { return AppSettingsHelper.Get("EndDate"); }
            set { AppSettingsHelper.Set("EndDate", value); }
        }

        public static string LastImportPath
        {
            get { return AppSettingsHelper.Get("LastImportPath"); }
            set { AppSettingsHelper.Set("LastImportPath", value); }
        }



        #endregion

        #region  软件属性
        public static string SoftwareName
        {
            get { return AppSettingsHelper.Get("SoftwareName"); }
            set { AppSettingsHelper.Set("SoftwareName", value); }
        }
        #endregion

        #region 其他配置

        public static string LastPath
        {
            get { return AppSettingsHelper.Get("LastPath"); }
            set { AppSettingsHelper.Set("LastPath", value); }
        }
        #endregion

    }
}
