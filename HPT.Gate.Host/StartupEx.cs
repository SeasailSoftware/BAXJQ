using hpt.gate;
using HPT.Gate.DataAccess.Entity.Service;
using HPT.Gate.Host.Config;
using HPT.Joey.Lib.Log;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace HPT.Gate.Host
{
    /// <summary>
    /// 系统启动辅助类
    /// </summary>
    public class StartupEx
    {
        /// <summary>
        /// 应用程序启动初始化，系统启动后立即运行
        /// </summary>
        public static bool AppStartupInit()
        {
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            Application.EnableVisualStyles(); // Application
            Application.SetCompatibleTextRenderingDefault(false);
            Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.RealTime;

            return true;
        }

        internal static bool CheckDBInstalled()
        {
            ///检查数据库是否已经安装
            if (!AppSettings.IsInstall)
            {
                if (new FrmDBInstall().ShowDialog() != DialogResult.OK) return false;
            }
            else
            {
                if (!TestConnect())
                {
                    AppSettings.IsInstall = true;
                    if (new FrmDBInstall().ShowDialog() != DialogResult.OK) return false;
                }
            }
            return true;
        }

        public static bool CheckLicense()
        {
            /*
            using (ILicenseService service = new LicenseService())
            {
                bool success = false;
                License license = service.Get();
                if (license != null)
                {
                    if (!license.Expired)
                    {
                        //有注册码则检测注册码的客户名称以及有效期
                        if (!string.IsNullOrEmpty(license.RegistCode))
                        {
                            CustMsg cust = SoftKeyConverter.RegisterCodeToCustMsg(license.RegistCode);
                            if (cust != null || cust.CustName.Equals(license.ClientCode))
                            {
                                if (DateTime.TryParse(cust.BeginDate, out DateTime dtBegin) && DateTime.TryParse(cust.EndDate, out DateTime dtEnd))
                                {
                                    if (dtBegin <= DateTime.Now && dtEnd.AddDays(1) > DateTime.Now)
                                        success = true;
                                }
                            }
                        }
                        else
                        {
                            ///没有注册码则检测是否已经过试用期
                            string msg;
                            if (DateTime.Now > license.LastLogin)
                            {
                                if (license.CreateTime.AddDays(90) >= DateTime.Now)
                                {
                                    license.LastLogin = DateTime.Now;
                                    service.Set(license, out msg);
                                    success = true;
                                }
                            }
                        }
                    }
                }
                if (success) return true;
                HPTSoftKey softKey = new HPTSoftKey();
                if (softKey.ReadCustomMessage(out CustMsg custMsg))
                {
                    if (custMsg != null || custMsg.CustName.Equals(license.ClientCode))
                    {
                        if (DateTime.TryParse(custMsg.BeginDate, out DateTime dtBegin) && DateTime.TryParse(custMsg.EndDate, out DateTime dtEnd))
                        {
                            if (dtBegin <= DateTime.Now && dtEnd.AddDays(1) > DateTime.Now)
                                return true;
                        }
                    }
                }
                return false;
            }
            */
            return true;
        }

        #region 软件注册
        public static bool Regist()
        {
            /*
            FrmRegist regist = new FrmRegist();
            return regist.ShowDialog() == DialogResult.OK;
            */
            return true;
        }
        #endregion



        #region 登录系统
        internal static bool AppRunLogin()
        {
            /*
            if (new Login().ShowDialog() != DialogResult.OK) return false;
            return true;
            */
            return true;
        }
        #endregion

        private static bool TestConnect()
        {
            return SystemService.TestConnect(AppSettings.ConnectString);
        }

        internal static bool CheckUpdate()
        {
            return FingerPrintService.CheckTableExists();
        }

        #region event method

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            LogHelper.WriteLog(ExceptionMsg(e.Exception, e.ToString()));
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            LogHelper.WriteLog(ExceptionMsg(e.ExceptionObject as Exception, e.ToString()));
        }

        #endregion

        /// <summary>
        /// 应用程序正式运行初始化，登录成功后启动主窗体前调用
        /// </summary>
        public static void AppRunInit()
        {
            ///MainTest.Logined();
            // 启动本地数据加载进程
            ///Task.Factory.StartNew(LocalCache.Load);
            // 启动主窗体
            bool createNew;
            using (System.Threading.Mutex mutex = new System.Threading.Mutex(true, Application.ProductName, out createNew))
            {
                if (createNew)
                {
                    Application.Run(new FrmMain());
                }
                else
                {
                    MessageBox.Show("应用程序正在运行中", "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Thread.Sleep(1000);
                    System.Environment.Exit(1);
                }
            }
        }

        #region 运行采集服务器
        public static void AppRunServer()
        {
            ///MainTest.Logined();
            // 启动本地数据加载进程
            ///Task.Factory.StartNew(LocalCache.Load);
            // 启动主窗体
            /*
            System.Windows.Forms.Application.Run(new FrmServer());
            */
        }
        #endregion

        public static void AppExitRelease()
        {
            try
            {
                string bakPath = $@"{Environment.CurrentDirectory}\Bak";
                if (!Directory.Exists(bakPath)) Directory.CreateDirectory(bakPath);
                bakPath += $@"\HPTGMS_{DateTime.Now.ToString("yyyyMMdd_HHmm")}.bak";
                SystemService.BackUpData(bakPath, AppSettings.DBName);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
            }
            Environment.Exit(Environment.ExitCode);
        }

        public static string ExceptionMsg(Exception ex, string bak)
        {
            StringBuilder buffer = new StringBuilder();
            buffer.AppendLine("<--------------------------------异常开始---------------------------->");
            buffer.AppendLine("【出现时间】：" + DateTime.Now.ToString());
            if (ex != null)
            {
                buffer.AppendLine("【异常类型】：" + ex.GetType().Name);
                buffer.AppendLine("【异常信息】：" + ex.Message);
                buffer.AppendLine("【堆栈调用】：" + ex.StackTrace);
            }
            buffer.AppendLine("【未处理异常】：" + bak);
            buffer.AppendLine("<--------------------------------异常结束---------------------------->");
            return buffer.ToString();
        }
    }
}
