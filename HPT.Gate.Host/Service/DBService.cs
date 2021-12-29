using HPT.Gate.DataAccess.Entity;
using HPT.Gate.DataAccess.Entity.Entity;
using HPT.Gate.DataAccess.Entity.Service;
using HPT.Gate.DataAccess.Service;
using HPT.Gate.Device.Data;
using HPT.Gate.Host.Service;
using HPT.Gate.Host.Util;
using System;
using System.Collections.Concurrent;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace HPT.Gate.Host
{
    public class DBService
    {
        #region private 

        private bool IsRunning = false;
        private static object _RecordLocker = new object();
        private static object _CardLocker = new object();
        private static readonly object _TaskLocker = new object();
        private static DBService instance;
        private static readonly object lockHelper = new object();

        private ConcurrentQueue<DataRecord> RecordQueue = new ConcurrentQueue<DataRecord>();
        private ConcurrentQueue<FaceTask> FaceTaskQueue = new ConcurrentQueue<FaceTask>();
        private static string ErrorImageDirectory = $@"{Environment.CurrentDirectory}\ErrorImage";
        #endregion


        #region Instance
        public static DBService Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (lockHelper)
                    {
                        if (instance == null)
                        {
                            instance = new DBService();
                            if (!Directory.Exists(ErrorImageDirectory)) Directory.CreateDirectory(ErrorImageDirectory);
                        }
                    }
                }
                return instance;
            }
        }
        #endregion

        #region Event
        public event EventHandler<DataInsertArgs> DataInsertEvent;

        /// <summary>
        /// 触发数据接收后事件
        /// </summary>
        /// <param name="client"></param>
        /// <param name="data"></param>
        /// <param name="message"></param>
        public void OnDataInsertEvent(int cardType, int camId, string cardNo, int deviceId, int ioFlag, string recTime, Bitmap image, string recordEvent)
        {
            try
            {
                if (DataInsertEvent == null) return;
                DataInsertArgs args = new DataInsertArgs();
                args.CardType = cardType;
                args.CamId = camId;
                args.CardNo = cardNo;
                args.DeviceId = deviceId;
                args.IOFlag = ioFlag;
                args.RecDatetime = recTime;
                args.Photo = image;
                args.RecordType = recordEvent;
                DataInsertEvent(this, args);
            }
            catch
            {
            }
        }

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
                    Message($"[数据库服务]{messgae}");
                }
            });
        }

        #endregion


        #region public 

        #region 开启数据库同步服务
        /// <summary>
        /// 开启数据库同步服务
        /// </summary>
        public void Start()
        {
            if (IsRunning) return;
            IsRunning = true;
            Task.Factory.StartNew(() => { SaveRecord(); });
            Task.Factory.StartNew(() => { HandleFaceTask(); });
        }
        #endregion

        #region 停止数据库同步服务
        /// <summary>
        /// 停止数据库同步服务
        /// </summary>
        public void Stop()
        {
            IsRunning = false;
        }
        #endregion

        #region 添加记录
        /// <summary>
        /// 添加记录
        /// </summary>
        /// <param name="record"></param>
        public void AddRecord(DataRecord record)
        {
            RecordQueue.Enqueue(record);
        }
        #endregion

        #region 添加任务
        public void AddTask(FaceDataTask task, bool success, Image image)
        {
            FaceTask faceTask = new FaceTask() { Task = task, Success = success, Photo = image };
            FaceTaskQueue.Enqueue(faceTask);
        }
        #endregion


        #region 获取当前尚未插入数据库的记录数
        public int GetRecordCount()
        {
            return RecordQueue.Count;
        }
        #endregion

        #region 获取当前尚未插入数据库的记录数
        public int GetTasksCount()
        {
            return FaceTaskQueue.Count;
        }
        #endregion


        #endregion

        #region 处理人脸任务
        private void HandleFaceTask()
        {
            while (IsRunning)
            {

                while (FaceTaskQueue.Count > 0)
                {
                    FaceTask task = null;
                    if (FaceTaskQueue.TryDequeue(out task))
                    {
                        if (task != null && task.Task != null)
                        {
                            try
                            {
                                if (FaceDataTaskService.FinishedTask(task.Task, task.Success))
                                {
                                    if (task.Photo != null)
                                    {
                                        string path = $@"{ErrorImageDirectory}\{Guid.NewGuid().ToString()}_{task.Task.EmpId}.jpg";
                                        task.Photo.Save(path);
                                    }
                                }
                                OnMessage($"人脸任务处理完毕!TaskId={task.Task.RecId}");
                            }
                            catch (Exception ex)
                            {
                                OnMessage($"处理人脸任务失败:{ex.Message}");
                            }
                        }
                    }
                }
                Thread.Sleep(1000);
            }
        }

        #endregion


        #region 保存记录
        private void SaveRecord()
        {
            while (IsRunning)
            {

                while (RecordQueue.Count > 0)
                {
                    DataRecord record = null;
                    if (RecordQueue.TryDequeue(out record))
                    {
                        if (record != null && record.CardNo != null)
                        {
                            try
                            {
                                Record dbRecord = DataConverter.GetRecord(record);
                                RecordService.Insert(dbRecord);
                                OnDataInsertEvent(dbRecord.Type, 0, record.CardNo, record.MachineId, record.IOFlag, record.RecDatetime, null, record.SRecordType);
                            }
                            catch (Exception ex)
                            {

                            }

                        }
                    }
                }
                Thread.Sleep(1000);
            }
        }

        #endregion


    }
    public class FaceTask
    {
        public FaceDataTask Task { get; set; }

        public bool Success { get; set; }

        public Image Photo { get; set; }
    }
}
