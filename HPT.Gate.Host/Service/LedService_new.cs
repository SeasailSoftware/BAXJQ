using hpt.gate.Led;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Drawing;
using System.Xml;
using System.Drawing.Imaging;
using hpt.gate.DataAccess.Entity;
using hpt.gate.DbTools.Service;
using System.Data.Common;
using System.Data;
using hpt.gate.collect.Config;
using hpt.gate.Helper;

namespace hpt.gate.collect.Led
{
    public class LedService
    {
        #region Var
        /// <summary>
        /// Led控制卡列表
        /// </summary>
        private List<LedController> _LedControllers = new List<LedController>();

        private bool IsStart = false;
        #endregion

        #region Instance

        private static LedService instance;
        private static readonly object lockHelper = new object();

        public static LedService Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (lockHelper)
                    {
                        if (instance == null)
                        {
                            instance = new LedService();
                        }
                    }
                }
                return instance;
            }
        }

        #endregion

        #region Events
        /// <summary>
        /// 消息提示事件
        /// </summary>
        public event Action<string> Message;

        /// <summary>
        /// 触发消息提示事件
        /// </summary>
        /// <param name="messgae"></param>
        private void OnMessage(string messgae)
        {
            if (Message == null) return;
            Task.Factory.StartNew(() =>
            {
                if (Message != null)
                {
                    Message(messgae);
                }
            });
        }

        #endregion


        #region private

        #region 初始化动态库
        /// <summary>
        /// 初始化动态库
        /// </summary>
        /// <returns></returns>
        private bool Init()
        {
            try
            {
                string file1 = Path.Combine(Environment.CurrentDirectory, @"lib/borlndmm.dll");
                string file2 = Path.Combine(Environment.CurrentDirectory, @"lib/LedDynamicArea.dll");
                string file3 = Path.Combine(Environment.CurrentDirectory, @"lib/TransNet.dll");
                if (!File.Exists(file1) || !File.Exists(file2) || !File.Exists(file3))
                {
                    OnMessage("初始化动态库失败:找不到相关dll文件!");
                    return false;
                }
                int initResult = BX_5EDynamicAreaSDK.Initialize();
                if (initResult != BX_5EDynamicAreaSDK.RETURN_NOERROR)
                {
                    //Logs.WriteLog("初始化动态库失败!");
                    OnMessage("[Led]初始化动态库成功!");
                    return false;
                }
                //Logs.WriteLog("初始化动态库成功!");
                return true;
            }
            catch (Exception ex)
            {
                //Logs.WriteLog(ex);
                OnMessage("[Led]初始化动态库失败:" + ex.Message);
                return false;
            }
        }
        #endregion

        #region 释放动态库
        private void Uninitialize()
        {
            int nResult = BX_5EDynamicAreaSDK.Uninitialize();
        }
        #endregion


        #region 初始化屏幕
        /// <summary>
        /// 初始化屏幕
        /// </summary>
        /// <param name="controller"></param>
        private void InitLedMonitor(LedController controller)
        {
            //创建文件夹
            string path = Path.Combine(Environment.CurrentDirectory, "Led");
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            //删除屏幕
            if (!DeleteScreen(controller.Lid))
                return;
            //添加屏幕
            if (!BX_5EDynamicAreaSDK.AddScreen_Dynamic(controller))
            {
                OnMessage(string.Format("添加显示屏[{0}]失败!", controller.Lid));
                return;
            }

            OnMessage(string.Format("添加显示屏[{0}]成功!", controller.Lid));
            foreach (AreaInfo area in controller.DynAreas)
            {
                if (BX_5EDynamicAreaSDK.AddScreenDynamicArea(area))
                {
                    OnMessage(string.Format("显示屏[{0}]添加动态区域[{1}]成功!", controller.Lid, area.AreaId));
                }
                else
                {
                    OnMessage(string.Format("显示屏[{0}]添加动态区域[{1}]失败!", controller.Lid, area.AreaId));
                }
            }

        }
        #endregion

        #region 删除屏幕
        /// <summary>
        /// 删除屏幕
        /// </summary>
        /// <param name="lid"></param>
        /// <returns></returns>
        private bool DeleteScreen(int lid)
        {
            int result = BX_5EDynamicAreaSDK.DeleteScreen_Dynamic(lid);
            if (result == BX_5EDynamicAreaSDK.RETURN_NOERROR || result == BX_5EDynamicAreaSDK.RETURN_ERROR_NOFIND_SCREENNO)
            {
                OnMessage(string.Format("[Led]删除屏幕[{0}]成功!", lid));
                return true;
            }
            OnMessage(string.Format("[Led]删除屏幕[{0}]失败!", lid));
            return false;
        }
        #endregion

        #region 读取文本文件内容
        /// <summary>
        /// 读取txt文件
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private string ReadTxtFile(string path)
        {
            path = Environment.CurrentDirectory + @"\led\" + path;
            if (!File.Exists(path))
            {
                return string.Empty;
            }
            using (StreamReader sr = new StreamReader(path, Encoding.Default))
            {
                string str = string.Empty;
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    str += line;
                    str += "\r\n";
                }
                return str;
            }
        }
        #endregion

        #region 清除所有发送过的Bmp图像
        private void ClearAllBmpSended(string temp)
        {
            string derictory = string.Format("{0}\\led", Environment.CurrentDirectory);
            string[] files = Directory.GetFiles(derictory);
            foreach (string file in files)
            {
                string fileName = file.Replace(derictory + "\\", "");
                int last = fileName.LastIndexOf(".");
                if (fileName.Substring(last).ToUpper().Equals(".BMP"))
                {
                    int index = fileName.IndexOf("_");
                    if (index == -1) continue;
                    string current = fileName.Substring(0, index);
                    if (current.Equals(temp.ToUpper()))
                    {
                        try
                        {
                            File.Delete(file);
                        }
                        catch
                        {

                        }
                    }
                }

            }
        }
        #endregion

        #region 生成Bmp图像
        private void HandleBmpFile(AreaInfo area, string sendFileName)
        {
            if (!File.Exists(sendFileName))
            {
                File.Create(sendFileName).Close();
            }

            int length = area.Width;
            int height = area.Height;
            Bitmap bmp = new Bitmap(length, height);//新建一个图片对象

            Graphics g = Graphics.FromImage(bmp);//利用该图片对象生成“画板”

            Font font = new Font(area.TextFont, area.TextFontSize);//设置字体颜色
            SolidBrush brush = new SolidBrush(Color.Red);//新建一个画刷,到这里为止,我们已经准备好了画板、画刷、和数据

            Pen pen = new Pen(Color.Red, 1);//定义了一个红色,宽度为的画笔
            g.Clear(Color.Black); //设置黑色背景
            //一排数据
            ///<----------------开始读取配置文件------------------>
            XmlDocument doc = new XmlDocument();
            string path = Environment.CurrentDirectory + @"/led/" + area.Text;
            doc.Load(path);    //加载Xml文件  
            XmlElement root = doc.DocumentElement;   //获取根节点  
            XmlNodeList rows = root.SelectNodes("//Table");
            ///root.GetElementsByTagName("person"); //获取person子节点集合 
            int rowIndex = 0;
            foreach (XmlNode node in rows)
            {
                foreach (XmlNode child in node.ChildNodes)
                {
                    string rowHeights = ((XmlElement)child).GetAttribute("Height");
                    ///高度根据字体的大小自动设置
                    ///int rowHeitht = Convert.ToInt32(((XmlElement)child).GetAttribute("Height")) ;
                    ///SizeF rowSize = g.MeasureString("测试", font);
                    ///int rowHeitht = (int)rowSize.Height;
                    int rowHeitht = GetHeightByFontSize(font);
                    int totalWidth = Convert.ToInt32(((XmlElement)child).GetAttribute("Width"));
                    int cellIndex = 0;
                    foreach (XmlNode childNode in child)
                    {
                        int cellWidth = Convert.ToInt32(((XmlElement)childNode).GetAttribute("Width"));
                        cellWidth = length * cellWidth / totalWidth;
                        string value = ((XmlElement)childNode).GetAttribute("Value");
                        value = GetContent(value, area.LID);
                        SizeF size = g.MeasureString(value, font);
                        /*
                        if(size.Width>cellWidth+5)
                        {
                            value = GetSuitableValue(g,cellWidth,value,font);
                        }
                         * */
                        g.DrawRectangle(pen, cellIndex, rowIndex, cellWidth, rowHeitht);
                        g.DrawString(value, font, brush, cellIndex + 1, rowIndex + 1);//
                        cellIndex += cellWidth;
                    }
                    rowIndex += rowHeitht;
                }
            }
            bmp.Save(sendFileName, ImageFormat.Bmp);//保存为输出流，否则页面上显示不出来
            g.Dispose();//释放掉该资源

            ///<--------------------------------------->
        }

        #endregion

        #region 计算行高
        /// <summary>
        /// 根据字体算出行高
        /// </summary>
        /// <param name="font"></param>
        /// <returns></returns>
        public static int GetHeightByFontSize(Font font)
        {
            int height = 16;
            switch ((int)font.Size)
            {
                case 8:
                    height = 11;
                    break;
                case 9:
                    height = 12;
                    break;
                case 10:
                    height = 13;
                    break;
                case 11:
                    height = 15;
                    break;
                case 12:
                    height = 16;
                    break;
                case 13:
                    height = 17;
                    break;
                case 14:
                    height = 19;
                    break;
                case 16:
                    height = 22;
                    break;
                case 18:
                    height = 24;
                    break;
                case 20:
                    height = 26;
                    break;
                case 28:
                    height = 37;
                    break;
            }
            return height + 1;
        }
        #endregion

        #region 更新动态区域内容
        private void UpdateDynArea(AreaInfo area)
        {
            int nResult = BX_5EDynamicAreaSDK.DeleteScreenDynamicAreaFile(area.LID, area.AreaId, 0);
            ///添加文件到动态区域
            string fileType = area.Text.Substring(area.Text.LastIndexOf(".")).ToUpper();
            switch (fileType)
            {
                case ".TXT":
                    string content = ReadTxtFile(area.Text);
                    string pText = GetContent(content, area.LID);
                    if (BX_5EDynamicAreaSDK.AddScreenDynamicAreaText(area, pText))
                    {
                        int updateResult = BX_5EDynamicAreaSDK.SendDynamicAreaInfoCommand(area.LID, area.AreaId);
                        if (updateResult == BX_5EDynamicAreaSDK.RETURN_NOERROR)
                        {
                            OnMessage(string.Format("显示屏[{2}]更新动态区域{0}内容【{1}】成功!", area.AreaId, pText.Replace("\r\n", ""), area.LID));
                        }
                        else
                        {
                            OnMessage(string.Format("显示屏[{2}]更新动态区域{0}内容【{1}】失败!", area.AreaId, pText.Replace("\r\n", ""), area.LID));
                            //Logs.WriteLog("发送文本【" + area.LID + area.AreaId + "】失败!");
                        }
                    }
                    break;
                case ".XML":
                    //清除所有发送的bmp文件
                    string temp = string.Format("{0}{1}", area.LID, area.AreaId);
                    //ClearAllBmpSended(temp);
                    //生成新的发送Bmp图像
                    string fileName = string.Format("{0}\\led\\{1}.bmp", Environment.CurrentDirectory, temp);
                    try
                    {
                        HandleBmpFile(area, fileName);
                    }
                    catch (Exception ex)
                    {
                        OnMessage($"显示屏[{area.LID}]动态区域[{area.AreaId}]生成表格失败:{ex.Message}");
                    }
                    if (BX_5EDynamicAreaSDK.AddScreenDynamicBmpFile(area, fileName))
                    {
                        int updateResult = BX_5EDynamicAreaSDK.SendDynamicAreaInfoCommand(area.LID, area.AreaId);
                        if (updateResult == BX_5EDynamicAreaSDK.RETURN_NOERROR)
                            OnMessage(string.Format("显示屏[{2}]更新动态区域{0}内容【{1}】成功!", area.AreaId, fileName, area.LID));
                        else
                            OnMessage(string.Format("显示屏[{2}]更新动态区域{0}内容【{1}】失败!", area.AreaId, fileName, area.LID));
                    }
                    break;
            }
        }

        #endregion

        #region 替换Led参数内容
        private string GetContent(string content, int lId)
        {
            string _Content = string.Empty;
            List<int> deptList = new List<int>();
            if (content.IndexOf("【") < 0 || content.IndexOf("】") < 0)
            {
                return content;
            }
            while (content.Length > 0)
            {
                if (content.IndexOf("【") < 0 || content.IndexOf("】") < 0)
                {
                    _Content += content;
                    break;
                }
                else
                {
                    int index1 = content.IndexOf("【");
                    int index2 = content.IndexOf("】");
                    if (index2 - index1 > 0)
                    {
                        string deptId = content.Substring(index1 + 1, index2 - index1 - 1);
                        //校验是否为数字
                        if (!System.Text.RegularExpressions.Regex.IsMatch(deptId, "^\\d+$"))
                        {
                            _Content = content.Substring(0, index2 + 1);
                            content = content.Substring(index2 + 1);
                            continue;
                        }
                        else
                        {
                            int curIndex = Convert.ToInt32(deptId);
                            string instead = string.Empty;
                            instead = LedDbService.GetValueOfDynPara(curIndex, AppSettings.LedNumberLength);
                            _Content += content.Substring(0, index1) + instead;
                            content = content.Substring(index2 + 1);
                            continue;
                        }
                    }
                }
            }
            return _Content;
        }
        #endregion


        #endregion

        #region public methods

        #region 添加led控制卡到管理器
        /// <summary>
        /// 添加led控制卡到管理器
        /// </summary>
        /// <param name="controller"></param>
        public void AddController(LedController controller)
        {
            bool flag = false;
            foreach (LedController led in _LedControllers)
            {
                if (led.Lid == controller.Lid)
                {
                    flag = true;
                    break;
                }
            }
            if (!flag)
                _LedControllers.Add(controller);
        }
        #endregion

        #region 开始服务
        public void Start()
        {
            if (IsStart) return;
            //初始化动态库
            if (!Init())
            {
                IsStart = false;
                return;
            }
            IsStart = true;
            _LedControllers = LedDbService.GetAllLedControllers();
            Task.Factory.StartNew(() =>
            {
                foreach (LedController controller in _LedControllers)
                {
                    controller.DynAreas = LedDbService.GetAreaList(controller.Lid);
                    InitLedMonitor(controller);
                }
            });
            DBService.Instance.DataInsertEvent += UpdateLedContentEvent;
        }

        #endregion

        #region 停止服务
        public void Stop()
        {
            if (!IsStart) return;
            DBService.Instance.DataInsertEvent -= UpdateLedContentEvent;
            Uninitialize();
            IsStart = false;
        }
        #endregion

        #endregion

        #region 查找合适的控制卡屏号
        public static decimal GetSuitableLid()
        {
            using (DBHelper dbHelper = DBHelperFactory.CreateDBHelper())
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


        #region 数据插入事件
        private void UpdateLedContentEvent(object sender, Service.DataInsertArgs e)
        {
            foreach (LedController controller in _LedControllers)
            {
                foreach (AreaInfo area in controller.DynAreas)
                {
                    Task.Factory.StartNew(() => { UpdateDynArea(area); });
                }
            }
        }
        #endregion

    }
}
