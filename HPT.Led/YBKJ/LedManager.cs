using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
namespace HPT.Led.YBKJ
{
    public class LedManager
    {
        #region Var
        public List<Controller> Controllers { get; set; }
        private bool IsRunning = false;
        private ConcurrentQueue<CommandTask> CommandQueue = new ConcurrentQueue<CommandTask>();
        #endregion

        #region Instance

        private static LedManager instance;
        private static readonly object lockHelper = new object();

        public static LedManager Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (lockHelper)
                    {
                        if (instance == null)
                        {
                            instance = new LedManager();
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
                    Message($"[Led Service]{messgae}");
                }
            });
        }

        #endregion


        #region private

        public void AddTask(int type, int lid, int areaId, string content)
        {
            CommandTask task = new CommandTask()
            {
                Type = type,
                Lid = lid,
                AreaId = areaId,
                Content = content
            };
            CommandQueue.Enqueue(task);
        }

        #region 初始化动态库
        /// <summary>
        /// 初始化动态库
        /// </summary>
        /// <returns></returns>
        private bool InitDll()
        {
            try
            {
                string file1 = @"borlndmm.dll"; ;
                string file2 = @"LedDynamicArea.dll";
                string file3 = "TransNet.dll";
                if (!File.Exists(file1) || !File.Exists(file2) || !File.Exists(file3))
                {
                    OnMessage("初始化动态库失败:找不到相关dll文件!");
                    return false;
                }
                int initResult = BX_5EDynamicAreaSDK.Initialize();
                if (initResult != BX_5EDynamicAreaSDK.RETURN_NOERROR)
                {
                    OnMessage("[Led]初始化动态库成功!");
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                OnMessage($"初始化动态库失败:{ex.Message}");
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




        #region 初始化Led控制卡
        private void InitController(Controller controller)
        {
            //添加屏幕
            if (!BX_5EDynamicAreaSDK.AddScreen_Dynamic(controller))
            {
                OnMessage($"添加屏幕[屏号:{controller.Lid},IP:{controller.IPAddress}]失败!");
                return;
            }
            OnMessage($"添加屏幕[屏号:{controller.Lid},IP:{controller.IPAddress}]成功!");
            foreach (AreaInfo area in controller.DynAreas)
            {
                if (BX_5EDynamicAreaSDK.AddScreenDynamicArea(area))
                    OnMessage($"屏幕[屏号:{controller.Lid},IP:{controller.IPAddress}]添加区域[{area.AreaId}]成功!");
                else
                    OnMessage($"屏幕[屏号:{controller.Lid},IP:{controller.IPAddress}]添加区域[{area.AreaId}]失败!");
            }
        }
        #endregion

        #endregion

        #region public methods

        #region 开始服务
        public void Start()
        {
            if (IsRunning)
            {
                OnMessage("服务已启动...");
                return;
            }
            //初始化动态库
            if (!InitDll()) return;
            //初始化控制卡
            if (Controllers == null || Controllers.Count == 0)
            {
                OnMessage("尚未添加Led控制器,启动失败...");
                return;
            }
            Task.Factory.StartNew(() =>
            {
                foreach (Controller controller in Controllers)
                {
                    InitController(controller);
                }
                IsRunning = true;
                while (IsRunning)
                {
                    while (CommandQueue.Count > 0 && IsRunning)
                    {
                        CommandTask task = null;
                        if (CommandQueue.TryDequeue(out task))
                        {
                            try
                            {
                                int nResult = BX_5EDynamicAreaSDK.DeleteScreenDynamicAreaFile(task.Lid, task.AreaId, 0);
                                Controller controller = Controllers.FirstOrDefault(p => p.Lid == task.Lid);
                                if (controller == null)
                                {
                                    OnMessage($"找不到屏号为[{task.Lid}]的控制卡信息,无法更新区域内容...");
                                    return;
                                }
                                AreaInfo area = controller.DynAreas?.FirstOrDefault(o => o.AreaId == task.AreaId);
                                if (area == null)
                                {
                                    OnMessage($"找不到屏号为[{controller.IPAddress}],区域号为[{task.AreaId}]的区域信息,无法更新区域内容...");
                                    return;
                                }
                                switch (task.Type)
                                {
                                    case 0:
                                        if (BX_5EDynamicAreaSDK.AddScreenDynamicAreaText(area, task.Content))
                                        {
                                            if (BX_5EDynamicAreaSDK.RETURN_NOERROR == BX_5EDynamicAreaSDK.SendDynamicAreaInfoCommand(task.Lid, task.AreaId))
                                                OnMessage($"屏幕[{controller.IPAddress}]更新区域[{task.AreaId}]文本内容成功!Content=[{task.Content.Replace("\r", "").Replace("\n", "")}]");
                                            else
                                                OnMessage($"屏幕[{controller.IPAddress}]更新区域[{area}]文本内容失败!Content=[{task.Content.Replace("\r", "").Replace("\n", "")}]");

                                        }
                                        else
                                            OnMessage($"屏幕[{controller.IPAddress}]添加动态区域[{area}]文本失败!");
                                        break;
                                    case 1:
                                        if (BX_5EDynamicAreaSDK.AddScreenDynamicBmpFile(area, area.Content))
                                        {
                                            if (BX_5EDynamicAreaSDK.RETURN_NOERROR == BX_5EDynamicAreaSDK.SendDynamicAreaInfoCommand(task.Lid, task.AreaId))
                                                OnMessage($"屏幕[{controller.IPAddress}]更新区域[{area}]图片内容成功!图片路径=[{task.Content.Replace("\r", "").Replace("\n", "")}]");
                                            else
                                                OnMessage($"屏幕[{controller.IPAddress}]更新区域[{area}]图片内容成功!图片路径=[{task.Content.Replace("\r", "").Replace("\n", "")}]");
                                        }
                                        else
                                            OnMessage($"屏幕[{controller.IPAddress}]添加动态区域[{area}]图片失败!");
                                        break;
                                }
                            }
                            catch (Exception ex)
                            {
                                OnMessage($"更新区域[屏号:{task.Lid},区域号:{task.AreaId}]内容失败:{ex.Message}");
                            }
                        }
                    }
                    Thread.Sleep(1000);
                }
            });
        }

        #endregion

        #region 更新动态区域内容
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileType">0表示文本，1表示图片</param>
        /// <param name="lid">屏幕编号</param>
        /// <param name="areaId">区域编号0<=n<=3</param>
        /// <param name="content">fileType=0时,表示文本内容;当filetype=1时,表示图片路径</param>
        public void UpdateAreaContent(int fileType, int lid, int areaId, string content)
        {
            Task.Factory.StartNew(() =>
            {
                try
                {
                    int nResult = BX_5EDynamicAreaSDK.DeleteScreenDynamicAreaFile(lid, areaId, 0);
                    Controller controller = Controllers.FirstOrDefault(p => p.Lid == lid);
                    if (controller == null)
                    {
                        OnMessage($"找不到屏号为[{lid}]的控制卡信息,无法更新区域内容...");
                        return;
                    }
                    AreaInfo area = controller.DynAreas?.FirstOrDefault(o => o.AreaId == areaId);
                    if (area == null)
                    {
                        OnMessage($"找不到屏号为[{lid}],区域号为[{areaId}]的区域信息,无法更新区域内容...");
                        return;
                    }
                    switch (fileType)
                    {
                        case 0:

                            if (BX_5EDynamicAreaSDK.AddScreenDynamicAreaText(area, content))
                            {
                                if (BX_5EDynamicAreaSDK.RETURN_NOERROR == BX_5EDynamicAreaSDK.SendDynamicAreaInfoCommand(lid, areaId))
                                    OnMessage($"屏幕[{lid}]更新区域[{area}]文本内容成功!Content=[{Environment.NewLine}{content}]");
                                else
                                    OnMessage($"屏幕[{lid}]更新区域[{area}]文本内容失败!Content=[{content}]");
                            }

                            else
                                OnMessage($"屏幕[{lid}]添加动态区域[{area}]文本失败!");
                            break;
                        case 1:
                            if (BX_5EDynamicAreaSDK.AddScreenDynamicBmpFile(area, content))
                            {
                                if (BX_5EDynamicAreaSDK.RETURN_NOERROR == BX_5EDynamicAreaSDK.SendDynamicAreaInfoCommand(lid, areaId))
                                    OnMessage($"屏幕[{lid}]更新区域[{area}]图片内容成功!图片路径=[{content}]");
                                else
                                    OnMessage($"屏幕[{lid}]更新区域[{area}]图片内容成功!图片路径=[{content}]");
                            }
                            else
                                OnMessage($"屏幕[{lid}]添加动态区域[{area}]图片失败!");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    OnMessage($"更新区域[屏号:{lid},区域号:{areaId}]内容失败:{ex.Message}");
                }
            });
        }

        #endregion

        #region 停止服务
        public void Stop()
        {
            try
            {
                IsRunning = false;
                Controllers.Clear();
                Uninitialize();
                OnMessage("服务已停止...");
            }
            catch
            {

            }
        }
        #endregion

        #endregion

    }
}
