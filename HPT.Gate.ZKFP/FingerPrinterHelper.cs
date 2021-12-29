using libzkfpcsharp;
using System;

namespace hpt.gate.FingerPrintHelper
{
    public class FingerPrinterHelper
    {
        private FingerPrinterHelper()
        {
        }

        private static readonly object DBLocker = new object();
        private static readonly object InstanceLock = new object();
        private static FingerPrinterHelper _instance;

        public static FingerPrinterHelper Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (InstanceLock)
                    {
                        if (_instance == null)
                        {
                            _instance = new FingerPrinterHelper();
                        }
                    }
                }

                return _instance;
            }
        }

        #region 指纹变量

        //private IntPtr _mDevHandle = IntPtr.Zero;
        private IntPtr _dbHandle = IntPtr.Zero;

        #endregion 指纹变量

        private bool _inited;

        /// <summary>
        /// 初始化指纹设备
        /// </summary>
        /// <returns></returns>
        public bool Init()
        {
            try
            {
                if (zkfp2.Init() == zkfperrdef.ZKFP_ERR_OK)
                {
                    _inited = true;
                    _dbHandle = zkfp2.DBInit();
                    if (IntPtr.Zero == _dbHandle)
                    {
                        zkfp2.Terminate();
                        return false;
                    }
                    return true;
                }
            }
            catch (DllNotFoundException exception)
            {

                return false;
            }
            catch (Exception e)
            {

            }
            return false;
        }


        /// <summary>
        /// 释放指纹仪
        /// </summary>
        public void Free()
        {
            try
            {
                if (!_inited)
                    return;
                //Thread.Sleep(1000);
                if (_dbHandle != IntPtr.Zero)
                {
                    zkfp2.DBFree(_dbHandle);
                    _dbHandle = IntPtr.Zero;
                }

                zkfp2.Terminate();
            }
            catch (Exception e)
            {

            }
        }

        public bool Clear()
        {
            var ret = zkfp2.DBClear(_dbHandle);
            if (zkfp.ZKFP_ERR_OK == ret)
            {
                return true;
            }

            return false;
        }

        public bool Add(int fingerID, byte[] data)
        {
            lock (DBLocker)
            {
                Remove(fingerID);
                if (data == null || data.Length < 200) return true;
                var ret = zkfp2.DBAdd(_dbHandle, fingerID, data);
                if (zkfp.ZKFP_ERR_OK == ret) return true;
                return false;
            }
        }

        public bool Remove(int fingerID)
        {
            var ret = zkfp2.DBDel(_dbHandle, fingerID);
            if (zkfp.ZKFP_ERR_OK == ret)
            {
                return true;
            }

            return false;
        }

        public int Count()
        {
            return zkfp2.DBCount(_dbHandle);
        }

        private const int MaxCount = 10;

        public bool Identity(byte[] data, out int fingerID, int minScorce = 55)
        {
            fingerID = 0;
            int count = 0;
            while (true)
            {
                var scorce = minScorce;
                var ret = zkfp2.DBIdentify(_dbHandle, data, ref fingerID, ref scorce);
                if (zkfp.ZKFP_ERR_OK == ret)
                {
                    if (scorce >= minScorce)
                    {
                        return true;
                    }
                    else
                    {
                        if (count > MaxCount)
                        {

                            break;
                        }
                        else
                        {

                            count++;
                        }
                    }
                }
                else
                {
                    break;
                }
            }
            return false;
        }

        public static int ZKFP_ERR_ALREADY_INIT = 1; /**< 已经初始化 */
        public static int ZKFP_ERR_OK = 0; /**< 操作成功 */
        public static int ZKFP_ERR_INITLIB = -1; /**< 初始化算法库失败 */
        public static int ZKFP_ERR_INIT = -2; /**< 初始化采集库失败 */
        public static int ZKFP_ERR_NO_DEVICE = -3; /**< 无设备连接 */
        public static int ZKFP_ERR_NOT_SUPPORT = -4; /**< 接口暂不支持 */
        public static int ZKFP_ERR_INVALID_PARAM = -5; /**< 无效参数 */
        public static int ZKFP_ERR_OPEN = -6; /**< 打开设备失败 */
        public static int ZKFP_ERR_INVALID_HANDLE = -7; /**< 无效句柄 */
        public static int ZKFP_ERR_CAPTURE = -8; /**< 取像失败 */
        public static int ZKFP_ERR_EXTRACT_FP = -9; /**< 提取指纹模板失败 */
        public static int ZKFP_ERR_ABSORT = -10; /**< 中断 */
        public static int ZKFP_ERR_MEMORY_NOT_ENOUGH = -11; /**< 内存不足 */
        public static int ZKFP_ERR_BUSY = -12; /**< 当前正在采集 */
        public static int ZKFP_ERR_ADD_FINGER = -13; /**< 添加指纹模板失败 */
        public static int ZKFP_ERR_DEL_FINGER = -14; /**< 删除指纹失败 */
        public static int ZKFP_ERR_FAIL = -17; /**< 操作失败 */
        public static int ZKFP_ERR_CANCEL = -18; /**< 取消采集 */
        public static int ZKFP_ERR_VERIFY_FP = -20; /**< 比对指纹失败 */
        public static int ZKFP_ERR_MERGE = -22; /**< 合并登记指纹模板失败 */
        public static int ZKFP_ERR_NOT_OPENED = -23; /**< 设备未打开 */
        public static int ZKFP_ERR_NOT_INIT = -24; /**< 未初始化 */
        public static int ZKFP_ERR_ALREADY_OPENED = -25; /**< 设备已打开 */
    }
}