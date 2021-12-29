using HPT.Gate.Utils.Common;
using System;

namespace HPT.Gate.Host.Config
{
    /// <summary>
    /// 系统参数配置
    /// </summary>
    public class AppSettings
    {

        #region 数据库配置


        public static bool AutoBackupData
        {
            get => AppSettingsHelper.Get("AutoBackupData").ToUpper().Equals("TRUE") ? true : false;
            set => AppSettingsHelper.Set("AutoBackupData", value ? "true" : "false");
        }

        public static string ProviderName
        {
            get => AppSettingsHelper.Get("ProviderName");
            set => AppSettingsHelper.Set("ProviderName", value);
        }

        /// <summary>
        /// 是否已经安装数据库
        /// </summary>
        public static bool IsInstall
        {
            get => AppSettingsHelper.Get("IsInstall").ToUpper().Equals("TRUE") ? true : false;
            set => AppSettingsHelper.Set("IsInstall", value ? "true" : "false");
        }
        /// <summary>
        /// 数据库登录类型
        /// </summary>
        public static string LoginType
        {
            get => AppSettingsHelper.Get("LoginType");
            set => AppSettingsHelper.Set("LoginType", value);
        }

        /// <summary>
        /// 数据库服务器名称
        /// </summary>
        public static string ServerName
        {
            get => AppSettingsHelper.Get("ServerName");
            set => AppSettingsHelper.Set("ServerName", value);
        }

        public static string DBPath
        {
            get => AppSettingsHelper.Get("DBPath");
            set => AppSettingsHelper.Set("DBPath", value);
        }

        /// <summary>
        /// 数据库名称
        /// </summary>
        public static string DBName
        {
            get => AppSettingsHelper.Get("DBName");
            set => AppSettingsHelper.Set("DBName", value);
        }

        /// <summary>
        /// 登录用户名
        /// </summary>
        public static string UserName
        {
            get => AppSettingsHelper.Get("UserName");
            set => AppSettingsHelper.Set("UserName", value);
        }
        /// <summary>
        /// 登录密码
        /// </summary>
        public static string Password
        {
            get => AppSettingsHelper.Get("Password");
            set => AppSettingsHelper.Set("Password", value);
        }

        public static string DbBackupPath
        {
            get => AppSettingsHelper.Get("DbBackupPath");
            set => AppSettingsHelper.Set("DbBackupPath", value);
        }

        public static string UpdateFile => AppSettingsHelper.Get("UpdateFile");


        public static string ConnectString
        {
            get
            {
                string connectString = string.Empty;
                switch (ProviderName)
                {
                    case "System.Data.SqlClient":
                        switch (LoginType)
                        {
                            case "0":
                                connectString = @"Data Source = " + ServerName + "; Initial Catalog = " + AppSettings.DBName + ";Integrated Security=true;";
                                break;
                            case "1":
                                connectString = @"Data Source = " + ServerName + "; Initial Catalog = " + AppSettings.DBName + ";Integrated Security=false;Persist Security Info=False;User Id = " + UserName + "; Password = " + Password + ";";
                                break;
                        }
                        break;
                    case "System.Data.OleDb":
                        switch (LoginType)
                        {
                            case "0":
                                connectString = $@"Provider = SQLOLEDB.1; Integrated Security = SSPI; Persist Security Info = False; User ID ={UserName}; Initial Catalog ={DBName}; Data Source ={ServerName}";
                                break;
                            case "1":
                                connectString = $@"Provider = SQLOLEDB.1; Password ={Password}; Persist Security Info = True; User ID = {UserName}; Initial Catalog ={DBName}; Data Source ={ServerName}";
                                break;
                        }
                        break;
                }
                return connectString;
            }
        }

        #region 连接字符串
        public static string MasterConnectString
        {
            get
            {
                string connectString = string.Empty;
                switch (ProviderName)
                {
                    case "System.Data.SqlClient":
                        connectString = $@"Data Source ={ServerName}; Initial Catalog =Master;Integrated Security=true;";
                        break;
                    case "System.Data.OleDb":
                        connectString = $@"Provider = SQLOLEDB.1; Integrated Security = SSPI; Persist Security Info = False; User ID ={UserName}; Initial Catalog =Master; Data Source ={ServerName}";
                        break;
                }
                return connectString;
            }
        }

        #endregion



        #endregion

        #region 功能配置

        public static bool SynCardEnabled
        {
            get => Convert.ToBoolean(AppSettingsHelper.Get("SynCardEnabled"));
            set => AppSettingsHelper.Set("SynCardEnabled", value.ToString());
        }
        public static bool LedEnabled
        {
            get => Convert.ToBoolean(AppSettingsHelper.Get("LedEnabled"));
            set => AppSettingsHelper.Set("LedEnabled", value.ToString());
        }
        public static bool FingerPrintEnabled
        {
            get => Convert.ToBoolean(AppSettingsHelper.Get("FingerPrintEnabled"));
            set => AppSettingsHelper.Set("FingerPrintEnabled", value.ToString());
        }
        public static int FingerPrintType
        {
            get => Convert.ToInt32(AppSettingsHelper.Get("FingerPrintType"));
            set => AppSettingsHelper.Set("FingerPrintType", value.ToString());
        }

        public static bool FaceEnabled
        {
            get => Convert.ToBoolean(AppSettingsHelper.Get("FaceEnabled"));
            set => AppSettingsHelper.Set("FaceEnabled", value.ToString());
        }
        public static int FaceMachine
        {
            get => Convert.ToInt32(AppSettingsHelper.Get("FaceMachine"));
            set => AppSettingsHelper.Set("FaceMachine", value.ToString());
        }
        public static int LedNumberLength
        {
            get => Convert.ToInt32(AppSettingsHelper.Get("LedNumberLength"));
            set => AppSettingsHelper.Set("LedNumberLength", value.ToString());
        }

        public static bool CameraEnabled
        {
            get => Convert.ToBoolean(AppSettingsHelper.Get("CameraEnabled"));
            set => AppSettingsHelper.Set("CameraEnabled", value.ToString());
        }

        public static int NetCamType
        {
            get => Convert.ToInt32(AppSettingsHelper.Get("NetCamType"));
            set => AppSettingsHelper.Set("NetCamType", value.ToString());
        }

        public static string LocalIPAddress
        {
            get => AppSettingsHelper.Get("LocalIPAddress");
            set => AppSettingsHelper.Set("LocalIPAddress", value);
        }

        /// <summary>
        /// 本地服务器端口
        /// </summary>
        public static int LocalPort
        {
            get => Convert.ToInt32(AppSettingsHelper.Get("LocalPort"));
            set => AppSettingsHelper.Set("LocalPort", value.ToString());
        }

        public static int RightsType
        {
            get => Convert.ToInt32(AppSettingsHelper.Get("RightsType"));
            set => AppSettingsHelper.Set("RightsType", value.ToString());
        }



        #region 启动场内人数自动清零
        public static bool AutoClearEnabled
        {
            get => AppSettingsHelper.Get("AutoClearEnabled").ToUpper().Equals("TRUE") ? true : false;
            set => AppSettingsHelper.Set("AutoClearEnabled", value ? "true" : "false");
        }

        public static string AutoClearTime
        {
            get => AppSettingsHelper.Get("AutoClearTime");
            set => AppSettingsHelper.Set("AutoClearTime", value);
        }

        public static bool LimitedTotalEnabled
        {
            get => AppSettingsHelper.Get("LimitedTotalEnabled").ToUpper().Equals("TRUE") ? true : false;
            set => AppSettingsHelper.Set("LimitedTotalEnabled", value ? "true" : "false");
        }

        public static int LimitedTotalOfInside
        {
            get => Convert.ToInt32(AppSettingsHelper.Get("LimitedTotalOfInside"));
            set => AppSettingsHelper.Set("LimitedTotalOfInside", value.ToString());
        }

        #endregion


        #endregion


        #region 其他配置

        public static string LastPath
        {
            get => AppSettingsHelper.Get("LastPath");
            set => AppSettingsHelper.Set("LastPath", value);
        }
        #endregion

        #region Lcd设置
        public static string LcdTitle
        {
            get => AppSettingsHelper.Get("LcdTitle");
            set => AppSettingsHelper.Set("LcdTitle", value);
        }

        public static int LcdCamOfIn
        {
            get => Convert.ToInt32(AppSettingsHelper.Get("LcdCamOfIn"));
            set => AppSettingsHelper.Set("LcdCamOfIn", value.ToString());
        }

        public static int LcdCamOfOut
        {
            get => Convert.ToInt32(AppSettingsHelper.Get("LcdCamOfOut"));
            set => AppSettingsHelper.Set("LcdCamOfOut", value.ToString());
        }
        #endregion

        #region 考勤设置
        public static int AttendModel
        {
            get => Convert.ToInt32(AppSettingsHelper.Get("AttendModel"));
            set => AppSettingsHelper.Set("AttendModel", value.ToString());
        }

        #endregion


        public static string JMSServer
        {
            get => AppSettingsHelper.Get("JMSServer");
            set => AppSettingsHelper.Set("JMSServer", value);
        }
        public static string JMSFilter
        {
            get => AppSettingsHelper.Get("JMSFilter");
            set => AppSettingsHelper.Set("JMSFilter", value);
        }

        public static string JMSClient
        {
            get => AppSettingsHelper.Get("JMSClient");
            set => AppSettingsHelper.Set("JMSClient", value);
        }

        public static string JMSAccount
        {
            get => AppSettingsHelper.Get("JMSAccount");
            set => AppSettingsHelper.Set("JMSAccount", value);
        }
        public static string JMSPassword
        {
            get => AppSettingsHelper.Get("JMSPassword");
            set => AppSettingsHelper.Set("JMSPassword", value);
        }

        public static string ServerURL
        {
            get => AppSettingsHelper.Get("ServerURL");
            set => AppSettingsHelper.Set("ServerURL", value);
        }

    }
}
