using System;

namespace HPT.Gate.Utils.Common
{
    /// <summary>
    /// 系统参数配置
    /// </summary>
    internal class AppSettings
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

        public static string ProviderName
        {
            get { return AppSettingsHelper.Get("ProviderName"); }
            set { AppSettingsHelper.Set("ProviderName", value); }
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
        #region 连接字符串

        /// <summary>
        /// 连接字符串
        /// </summary>
        public static string MssqlConnectString
        {
            get
            {
                string connectString = string.Empty;
                switch (LoginType)
                {
                    case "0":
                        connectString = @"Data Source = " + ServerName + "; Initial Catalog = " + AppSettings.DBName + ";Integrated Security=true;";
                        break;
                    case "1":
                        connectString = @"Data Source = " + ServerName + "; Initial Catalog = " + AppSettings.DBName + ";Integrated Security=false;Persist Security Info=False;User Id = " + UserName + "; Password = " + Password + ";";
                        break;
                }
                return connectString;
            }
        }

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

        #endregion

        #endregion


        public static int CardType
        {
            get { return Convert.ToInt32(AppSettingsHelper.Get("CardType")); }
            set { AppSettingsHelper.Set("CardType", value.ToString()); }
        }



    }
}
