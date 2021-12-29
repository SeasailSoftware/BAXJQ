using hpt.gate.DataAccess.Entity;
using hpt.gate.DataAccess.Service;
using HPT.Face;
using HPT.Face.AXD;
using HPT.Face.HPT;
using HPT.Face.SYD;
using HPT.Face.YF;
using HPT.Gate.DataAccess.Entity;
using HPT.Gate.DataAccess.Entity.Service;
using HPT.Gate.DataAccess.Service;
using HPT.Gate.Device.Data;
using HPT.Gate.Device.Dev;
using HPT.Gate.Host.Config;
using HPT.Gate.Host.Util;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HPT.Gate.Host
{
    public class DataService
    {

        #region private

        private bool IsRunning = false;
        #endregion


        #region Event
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
                    Message($"[数据同步服务]{messgae}");
                }
            });
        }
        #endregion

        #region Instance

        private static DataService instance;
        private static readonly object lockHelper = new object();

        public static DataService Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (lockHelper)
                    {
                        if (instance == null)
                        {
                            instance = new DataService();
                        }
                    }
                }
                return instance;
            }
        }

        #endregion

        #region public mechods


        #region 启动服务
        public void Start()
        {
            if (IsRunning) return;
            IsRunning = true;
            List<DeviceInfo> devList = DeviceInfoService.ToList();
            foreach (DeviceInfo device in devList)
            {
                TcpDevice dev = DataConverter.GetTcpDevice(device);
                bool success = AppSettings.SynCardEnabled;
                Task.Factory.StartNew(() => { SynCardDatas(dev, success); });
            }
            SystemConfig config = SystemConfigService.Get();
            if (config != null && config.FaceEnabled)
            {
                List<FaceDevice> devices = FaceDeviceService.ToList();
                foreach (FaceDevice device in devices)
                {
                    Task.Factory.StartNew(() => { SynFaceDatas(device); });
                }
            }

            OnMessage("服务启动成功!");
        }

        #endregion

        #region 停止服务
        public void Stop()
        {
            IsRunning = false;
            OnMessage("服务已停止!");
        }
        #endregion


        #endregion

        #region private methods

        #region 数据同步卡数据
        private void SynCardDatas(TcpDevice device, bool synFlag)
        {
            int indexSetTime = 0;
            while (IsRunning)
            {
                //判断设备是否离线
                if (!device.IsOnline)
                {
                    OnMessage($"设备[{device._IPAddress}]离线...");
                    int index = 60;
                    while (!device.IsOnline)
                    {
                        if (!IsRunning) break;
                        Thread.Sleep(1000);
                        index--;
                    }
                    continue;
                }
                if (synFlag)
                {
                    //获取同步任务列表
                    List<DataSynTask> taskList = DeviceInfoService.GetDataSynTasks(device._MachineId);
                    foreach (DataSynTask task in taskList)
                    {
                        if (!IsRunning) break;
                        if (!device.IsOnline) break;
                        DoTask(device, task);
                    }
                }

                //采集记录
                List<DataRecord> recordList = device.GetRecords();
                if (recordList != null && recordList.Count > 0)
                {
                    foreach (DataRecord record in recordList)
                    {
                        DBService.Instance.AddRecord(record);
                        OnMessage($"设备[{device._IPAddress}]采集记录:{record.ToString()}");
                    }
                    continue;
                }
                Thread.Sleep(1000);
                if (!IsRunning) break;
                //设置设备时间
                indexSetTime++;
                if (indexSetTime >= 3000)
                {
                    if (device.SetTime())
                        OnMessage($"设备[{device._IPAddress}]设置时间成功!");
                    else
                        OnMessage($"设备[{device._IPAddress}]设置时间失败!");
                    indexSetTime = 0;
                }
            }
        }

        #endregion

        #region 同步人脸数据
        private void SynFaceDatas(FaceDevice device)
        {
            SystemConfig config = SystemConfigService.Get();
            if (!config.FaceEnabled) return;
            HFace service = null;

            while (IsRunning)
            {
                if (CheckOnline(device.IPAddress))
                {
                    List<FaceDataTask> tasks = new List<FaceDataTask>();
                    try
                    {
                        tasks = FaceDataTaskService.GetUnSynTask(device.DeviceId);
                    }
                    catch (Exception ex)
                    {
                        OnMessage($"获取人脸设备[{device.IPAddress}]更新任务失败:{ex.Message}");

                    }
                    foreach (FaceDataTask task in tasks)
                    {
                        string personId = task.EmpId.ToString("00000000");
                        UInt32 temp = (UInt32)((task.EmpId - 1) * 5 + 4);
                        byte[] arr = BitConverter.GetBytes(temp);
                        Array.Reverse(arr);
                        UInt32 temp1 = BitConverter.ToUInt32(arr, 0);
                        switch (config.FaceMachineType)
                        {
                            case 0:
                                service = new HPTFace();
                                personId = temp.ToString("00000000");
                                break;
                            case 1:
                                service = new YFFace();
                                personId = temp1.ToString("00000000");
                                break;
                            case 2:
                                service = new SYDFace();
                                personId = ((task.EmpId - 1) * 5 + 4).ToString("00000000");
                                break;
                            case 3:
                                service = new AXDFace();
                                personId = ((task.EmpId - 1) * 5 + 4).ToString("00000000");
                                break;
                        }

                        if (!IsRunning) break;
                        if (!service.IsOnline(device.IPAddress)) break;
                        EmpInfo emp = null;
                        string msg;
                        switch (task.Type)
                        {
                            //删除
                            case 3:
                                emp = EmpInfoService.GetByEmpId(task.EmpId);
                                if (emp == null)
                                    emp = new EmpInfo() { EmpId = task.EmpId, EmpCode = "00000000", EmpName = "已删除" };
                                if (service.DeleteEmp(device.IPAddress, device.Password, personId, out msg))
                                {
                                    OnMessage($"人脸机[{ device.IPAddress}]删除人员数据[{ emp.EmpCode},{ emp.EmpName}]成功!");
                                    DBService.Instance.AddTask(task, true, null);
                                    TaskResultService.Insert(task.RecId, emp.EmpId, task.DeviceId, true, "Success!");
                                }
                                else
                                {
                                    OnMessage($"人脸机[{device.IPAddress}]删除人员数据[{emp.EmpCode},{emp.EmpName}]失败:{msg}");
                                    DBService.Instance.AddTask(task, false, null);
                                    TaskResultService.Insert(task.RecId, emp.EmpId, task.DeviceId, false, msg);
                                }
                                break;
                            case 4:
                                if (service.SetTime(device.IPAddress, device.Password, DateTime.Now, out msg))
                                    OnMessage($"人脸机[{device.IPAddress}]设置时间成功!");
                                else
                                    OnMessage($"人脸机[{device.IPAddress}]设置时间失败:{msg}");
                                DBService.Instance.AddTask(task, true, null);
                                break;
                            case 5:
                                if (service.ReSet(device.IPAddress, device.Password, false, out msg))
                                    OnMessage($"人脸机[{device.IPAddress}]初始化成功!");
                                else
                                    OnMessage($"人脸机[{device.IPAddress}]初始化失败:{msg}");
                                DBService.Instance.AddTask(task, true, null);
                                break;
                            //新增/更新
                            default:
                                emp = EmpInfoService.GetByEmpId(task.EmpId);
                                if (emp == null)
                                    DBService.Instance.AddTask(task, true, null);
                                else
                                {
                                    //同步人员信息
                                    if (service.UpdateEmp(device.IPAddress, device.Password, personId, emp.EmpName, emp.EndDate, (Bitmap)emp.Photo, out msg))
                                    {
                                        OnMessage($"人脸机[{ device.IPAddress}]添加人员数据[{ emp.EmpCode},{ emp.EmpName}]成功!");
                                        DBService.Instance.AddTask(task, true, emp.Photo);
                                        TaskResultService.Insert(task.RecId, emp.EmpId, task.DeviceId, false, "Success!");
                                    }
                                    else
                                    {
                                        if (!msg.Contains("设备无响应"))
                                        {
                                            OnMessage($"人脸机[{device.IPAddress}]添加人员数据[{emp.EmpCode},{emp.EmpName}]失败:{msg}");
                                            DBService.Instance.AddTask(task, false, emp.Photo);
                                            TaskResultService.Insert(task.RecId, emp.EmpId, task.DeviceId, true, msg);
                                        }

                                    }
                                }
                                break;
                        }

                    }

                }
                else
                    OnMessage($"人脸设备[{device.IPAddress}]离线,30秒后再试...");
                int index = 30;
                while (index > 0 && IsRunning)
                {
                    Thread.Sleep(1000);
                    index--;
                }
            }
        }

        #endregion


        #region 同步完成后改变任务的状态
        private void ChangeDataSynTaskStatus(int recId)
        {
            try
            {
                DeviceInfoService.ChangeDataSynTaskStatus(recId);
            }
            catch (Exception ex)
            {
                OnMessage($"改变同步任务的同步状态失败:{ex.Message},RecId ={recId}");
            }
        }
        #endregion

        #region 
        private bool CheckOnline(string iPAddress)
        {
            Ping sender = new Ping();
            PingReply reply = sender.Send(iPAddress, 1000);
            return reply.Status == IPStatus.Success;
        }
        #endregion

        #region 启动同步任务
        private void DoTask(TcpDevice device, DataSynTask task)
        {
            StringBuilder buffer = new StringBuilder();
            buffer.AppendLine($"开始执行同步卡任务[任务编号:{task.RecId},人员编号:{task.EmpId}]...");
            try
            {
                List<DataCard> dataCards = new List<DataCard>();
                int max = EmpInfoService.GetNextEmpId() * 5;
                int current = task.EmpId;
                int blackName = task.Rights == 0 ? 1 : 0;
                EmpInfo emp = EmpInfoService.GetByEmpId(task.EmpId);
                if (emp == null)
                {
                    dataCards.Add(DataConverter.GetDataCard(max, current, null, 1, null, blackName, null));
                    dataCards.Add(DataConverter.GetDataCard(max, current, null, 2, null, blackName, null));
                    dataCards.Add(DataConverter.GetDataCard(max, current, null, 3, null, blackName, null));
                    dataCards.Add(DataConverter.GetDataCard(max, current, null, 5, null, blackName, null));
                    dataCards.Add(DataConverter.GetDataCard(max, current, null, 6, null, blackName, null));
                }
                else
                {
                    TicketType ticket = TicketTypeService.GetByRecId(emp.TicketType);
                    string[] displayContents = EmpInfoService.GetDisplayContent(emp.EmpId, emp.TicketType);
                    dataCards.Add(DataConverter.GetDataCard(max, current, emp, 1, ticket, blackName, displayContents));
                    dataCards.Add(DataConverter.GetDataCard(max, current, emp, 2, ticket, blackName, displayContents));
                    dataCards.Add(DataConverter.GetDataCard(max, current, emp, 3, ticket, blackName, displayContents));
                    dataCards.Add(DataConverter.GetDataCard(max, current, emp, 5, ticket, blackName, displayContents));
                    dataCards.Add(DataConverter.GetDataCard(max, current, emp, 6, ticket, blackName, displayContents));
                }
                bool success = true;
                foreach (DataCard card in dataCards)
                {
                    switch (card.Type)
                    {
                        case 1:
                            if (device.SynCardData(card))
                            {
                                success = success && true;
                                buffer.AppendLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} [{device._DeviceName}]同步IC/ID卡[{card.SCardNo}]成功!");
                            }
                            else
                            {
                                success = success && false;
                                buffer.AppendLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} [{device._DeviceName}]同步IC/ID卡[{card.SCardNo}]失败!");
                            }
                            break;
                        case 2:
                            if (device.SynCardData(card))
                            {
                                success = success && true;
                                buffer.AppendLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} [{device._DeviceName}]同步身份证序列号[{card.SCardNo}]成功!");
                            }
                            else
                            {
                                success = success && false;
                                buffer.AppendLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} {device._DeviceName}同步身份证序列号[{card.SCardNo}]失败!");
                            }
                            break;
                        case 3:
                            if (device.SynCardData(card))
                            {
                                success = success && true;
                                buffer.AppendLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} [{device._DeviceName}]同步身份证号码[{card.SCardNo}]成功!");
                            }
                            else
                            {
                                success = success && false;
                                buffer.AppendLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} {device._DeviceName}同步身份证号码[{card.SCardNo}]失败!");
                            }
                            break;
                        case 5:
                            if (AppSettings.FingerPrintEnabled && AppSettings.FingerPrintType == 1)
                            {
                                if (device.SynCardData(card))
                                {
                                    success = success && true;
                                    buffer.AppendLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} [{device._DeviceName}]同步指纹编号[{card.SCardNo}]成功!");
                                }
                                else
                                {
                                    success = success && false;
                                    buffer.AppendLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} {device._DeviceName}同步指纹编号[{card.SCardNo}]失败!");
                                }
                            }
                            else
                                success = success && true;
                            break;
                        case 6:
                            if (device.SynCardData(card))
                            {
                                success = success && true;
                                buffer.AppendLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} [{device._DeviceName}]同步人脸编号[{card.SCardNo}]成功!");
                            }
                            else
                            {
                                success = success && false;
                                buffer.AppendLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} {device._DeviceName}同步人脸编号[{card.SCardNo}]失败!");
                            }
                            break;
                    }
                }
                if (success)
                {
                    buffer.AppendLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} [{device._DeviceName}]同步任务[{task.RecId}]成功,EmpId={task.EmpId}");
                    ChangeDataSynTaskStatus(task.RecId);
                }
                else
                    buffer.AppendLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} [{device._DeviceName}]同步任务[{task.RecId}]失败,EmpId={task.EmpId}");
            }
            catch (Exception ex)
            {
                buffer.AppendLine($"执行同步卡任务失败:{ex.Message},EmpId={task.EmpId},DeviceId={task.DeviceId}");
            }
            OnMessage(buffer.ToString());
        }
        #endregion


        #endregion

    }
}
