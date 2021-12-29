using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace HPT.Joey.Lib.Log
{
    public static class LogHelper
    {
        private static readonly string BASE_DIR = $"Log";
        private static readonly string FILENAME = System.DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
        private static readonly string ExceptionFile = $"{BASE_DIR}\\Exception\\{FILENAME}"; 
        private static readonly string InfoFile = $"{BASE_DIR}\\Info\\{FILENAME}";
        private static readonly string CommunicationFile = $"{BASE_DIR}\\Comm\\{FILENAME}"; 
        private const string LOG4NET_CONFIG = "Log\\log4net_config.xml";
        private static readonly log4net.ILog CurrentLoger = log4net.LogManager.GetLogger(typeof(LogHelper));
        static LogHelper()
        {

        }

        public static void WriteLog(string fileName,string sInfo)
        {
            ConfigureLoad(fileName);
            CurrentLoger.Info(sInfo);
        }
        /// <summary>  
        /// 输出日志  
        /// </summary>  
        /// <param name="sInfo"></param>  
        public static void WriteLog(string sInfo)
        {
            ConfigureLoad(InfoFile);
            CurrentLoger.Info(sInfo);
        }


        public static void WriteCommLog(string info)
        {
            ConfigureLoad(CommunicationFile);
            CurrentLoger.Info(info);
        }

        /// <summary>  
        /// 记录debug信息  
        /// </summary>  
        /// <param name="e"></param>  
        public static void WriteLog(Exception e)
        {
            ConfigureLoad(ExceptionFile);
            StringBuilder buffer = new StringBuilder();
            buffer.AppendLine("-------[本次异常开始]-------------");
            buffer.AppendLine("Message : " + e.Message);
            buffer.AppendLine("Source : " + e.Source);
            buffer.AppendLine("StackTrace : " + e.StackTrace);
            buffer.AppendLine("TargetSite : " + e.TargetSite);
            buffer.AppendLine("- -----[本次异常结束]--------------\r\n");
            CurrentLoger.Info(buffer.ToString());
        }

        /// <summary>  
        /// 配置log4net环境  
        /// </summary>  
        private static void ConfigureLoad(string dir)
        {
            XmlDocument doc = new XmlDocument();
            //使用当前dll路径  
            string sPath = FilesOperate.GetAssemblyPath();

            if (!sPath.EndsWith("\\"))
            {
                sPath += "\\";
            }
            //查看Log文件夹是否存在，如果不存在，则创建  
            string sLogDir = sPath + BASE_DIR;
            if (!Directory.Exists(sLogDir))
            {
                Directory.CreateDirectory(sLogDir);
            }
            string sLogFile = sPath + dir;
            sPath += LOG4NET_CONFIG;
            doc.Load(@sPath);
            XmlElement myElement = doc.DocumentElement;

            //修改log.txt的路径  
            XmlNode pLogFileAppenderNode = myElement.SelectSingleNode("descendant::appender[@name='LogFileAppender']/file");
            // Create an attribute collection from the element.  
            XmlAttributeCollection attrColl = pLogFileAppenderNode.Attributes;
            attrColl[0].Value = sLogFile;

            log4net.Config.XmlConfigurator.Configure(myElement);
        }

    }
}
