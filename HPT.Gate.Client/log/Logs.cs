using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using System.IO;
using hpt.gate.DbTools;
using hpt.gate.DbTools.Service;
using HPT.Gate.DataAccess.Service;
using Joey.UserControls;

namespace HPT.Gate.Client.log
{
    class Logs
    {
        private static string logPath = Path.Combine(Environment.CurrentDirectory, "Log");
        /// <summary>
        /// 插入操作日志
        /// </summary>
        /// <param name="OperName"></param>
        /// <param name="recDatetime"></param>
        /// <param name="LogObject"></param>
        /// <param name="LogAction"></param>
        /// <param name="LogMessage"></param>
        public static void InsertOperLog(string OperName, string recDatetime, string LogObject, string LogAction, string LogMessage, int LogType)
        {
            try
            {
                OperLogService.Insert(OperName, recDatetime, LogObject, LogAction, LogMessage, LogType);
            }
            catch (Exception e)
            {
                Logs.WriteLog(e);
                MessageBoxHelper.Error("插入操作日志失败:" + e.Message);
            }
        }

        /// <summary>
        /// 将异常打印到LOG文件
        /// </summary>
        /// <param name="ex">异常</param>
        /// <param name="LogAddress">日志文件地址</param>
        public static void WriteLog(Exception ex, string LogAddress = "")
        {
            if (!Directory.Exists(logPath))
                Directory.CreateDirectory(logPath);
            //如果日志文件为空，则默认在Debug目录下新建 YYYY-mm-dd_Log.log文件
            if (LogAddress == "")
            {
                LogAddress = Environment.CurrentDirectory + "\\Log\\" +
                    DateTime.Now.Year + '-' +
                    DateTime.Now.Month + '-' +
                    DateTime.Now.Day + "_Log.log";
            }

            //把异常信息输出到文件
            StreamWriter sw = new StreamWriter(LogAddress, true);
            sw.WriteLine("记录时间：" + DateTime.Now.ToString());
            sw.WriteLine("异常信息：" + ex.Message);
            sw.WriteLine("异常对象：" + ex.Source);
            sw.WriteLine("调用堆栈：\n" + ex.StackTrace.Trim());
            sw.WriteLine("触发方法：" + ex.TargetSite);
            sw.WriteLine();
            sw.Close();
        }

        public static object _LockObj = new object();

        /// <summary>
        /// 将异常打印到LOG文件
        /// </summary>
        /// <param name="ex">异常</param>
        /// <param name="LogAddress">日志文件地址</param>
        public static void WriteLog(string log, string LogAddress = "")
        {
            lock (_LockObj)
            {
                //如果日志文件为空，则默认在Debug目录下新建 YYYY-mm-dd_Log.log文件
                if (LogAddress == "")
                {
                    LogAddress = Environment.CurrentDirectory + "\\Log\\" +
                        DateTime.Now.Year + '-' +
                        DateTime.Now.Month + '-' +
                        DateTime.Now.Day + "_Log.log";
                }

                //把异常信息输出到文件
                StreamWriter sw = new StreamWriter(LogAddress, true);
                sw.WriteLine(DateTime.Now.ToString() + " " + log);
                sw.WriteLine();
                sw.Close();
            }
        }
    }
}
