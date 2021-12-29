using System;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;
using System.Data.SqlClient;
using Joey.UserControls;
using HPT.Gate.Client.config;

namespace HPT.Gate.Client
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

            ///登录
            if (new Login().ShowDialog() != DialogResult.OK) return false;
            return true;
        }

        #region 登录系统
        internal static bool AppRunLogin()
        {
            if (new Login().ShowDialog() != DialogResult.OK) return false;
            return true;
        }
        #endregion

        private static bool TestConnect()
        {
            SqlConnection conn = new SqlConnection(AppSettings.OLEConnectString);
            try
            {
                conn.Open();
                return true;
            }
            catch (Exception ex)
            {
                MessageBoxHelper.Error("数据库连接失败!错误信息:" + ex.Message);
                return false;
            }
        }

        #region event method

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            if (e == null)
            {
                return;
            }
            if (e.Exception.InnerException != null)
            {
                ///NLogHelper.GetLogger(typeof(Program).Namespace).Error(e.Exception.InnerException);
            }
            ///NLogHelper.GetLogger(typeof(Program).Namespace).Error(e);
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e == null)
            {
                return;
            }
            ////NLogHelper.GetLogger(typeof(Program).Namespace).Error(e);
            var ex = e.ExceptionObject as Exception;
            if (ex == null)
            {
                return;
            }
            ///NLogHelper.GetLogger(typeof(Program).Namespace).Error(ex);
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
                    Application.Run(new Main());
                }
                else
                {
                    MessageBoxHelper.Info("应用程序正在运行中...");
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
            System.Windows.Forms.Application.Run(new Main());
        }
        #endregion

        public static void AppExitRelease()
        {
            Environment.Exit(Environment.ExitCode);
        }
    }
}
