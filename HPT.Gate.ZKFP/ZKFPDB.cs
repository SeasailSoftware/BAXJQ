using libzkfpcsharp;
using System;

namespace HPT.Gate.ZKFP
{
    public class ZKFPDB
    {

        private IntPtr mDBHandle = IntPtr.Zero;
        private int score = 50;
        private static object dbLock = new object();

        public int Count => (mDBHandle == IntPtr.Zero ? 0 : zkfp2.DBCount(mDBHandle));

        #region Public Methods


        #region 清空指纹
        public bool Clear()
        {
            if (mDBHandle == IntPtr.Zero) return false;
            return zkfp2.DBClear(mDBHandle) == 0;

        }
        #endregion

        #region 数据库初始化
        public bool Init()
        {
            return (mDBHandle = zkfp2.DBInit()) != IntPtr.Zero;
        }
        #endregion

        #region 指纹验证

        public bool DBIdentify(byte[] captureTmp)
        {
            if (mDBHandle == IntPtr.Zero) return false;
            int fid = 0;
            int ret = zkfp2.DBIdentify(mDBHandle, captureTmp, ref fid, ref score);
            return ret == zkfp.ZKFP_ERR_OK;
        }

        #endregion

        #region 指纹验证

        public bool DBIdentify(byte[] captureTmp, out int fpId, out int nScore)
        {
            fpId = 0;
            nScore = 0;
            int ret = zkfp2.DBIdentify(mDBHandle, captureTmp, ref fpId, ref nScore);
            if (ret == zkfp.ZKFP_ERR_OK) return true;
            return false;
        }

        #endregion

        #region 模版比对
        public int DBMatch(byte[] temp1, byte[] temp2)
        {
            return zkfp2.DBMatch(mDBHandle, temp1, temp2);
        }
        #endregion


        #endregion

        #region 数据合并
        public byte[] DBMerge(byte[] temp1, byte[] temp2, byte[] temp3)
        {
            int cbRegTmp = 0;
            byte[] regTemp = new byte[2048];
            if (zkfp.ZKFP_ERR_OK == zkfp2.DBMerge(mDBHandle, temp1, temp2, temp3, regTemp, ref cbRegTmp))
            {
                byte[] array = new byte[cbRegTmp];
                Array.Copy(regTemp, array, cbRegTmp);
                return array;
            }
            return null;
        }

        #endregion

        #region 添加到数据库
        public bool DBAdd(int fid, byte[] data)
        {
            if (fid <= 0 || data == null || data.Length < 100) return false;
            try
            {
                lock (dbLock)
                {
                    zkfp2.DBDel(mDBHandle, fid);
                    return zkfp2.DBAdd(mDBHandle, fid, data) == zkfp.ZKFP_ERR_OK;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public bool DBAdd(int fid, byte[] data, out string msg)
        {
            if (fid <= 0 || data == null || data.Length < 100)
            {
                msg = "指纹数据不符合规定!";
                return false;
            }
            try
            {
                lock (dbLock)
                {
                    zkfp2.DBDel(mDBHandle, fid);
                    msg = "";
                    return zkfp2.DBAdd(mDBHandle, fid, data) == zkfp.ZKFP_ERR_OK;
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return false;
            }

        }

        #endregion

        #region 添加到数据库
        public bool Delete(int fid, out string msg)
        {
            try
            {
                lock (dbLock)
                {
                    int result = zkfp2.DBDel(mDBHandle, fid);
                    if (result != zkfp.ZKFP_ERR_OK)
                    {
                        msg = $"删除失败!返回代码{result}";
                        return false;
                    }
                    msg = "success";
                    return true;
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return false;
            }
        }
        #endregion



    }

}
