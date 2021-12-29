using HPT.Face.Utils;
using System;
using System.Drawing;
using System.Net.NetworkInformation;

namespace HPT.Face.AXD
{
    public class AXDFace : HFace
    {
        static AXDFace()
        {
            try
            {
                if (AXDEnvironment.IsInited) return;
                HaCamera.InitEnvironment();
                AXDEnvironment.IsInited = true;
            }
            catch (Exception ex)
            {
                var val = ex.Message;
            }
        }
        public bool CheckFace(string iPAddress, string password, Image image, out string msg)
        {
            byte[] bytes = FaceUtil.ImageToBytes(image);
            bool success = HaCamera.ValidImage(bytes);
            if (success)
                msg = "照片符合要求!";
            else
                msg = "照片不符合要求!";
            return success;
        }

        public bool CreateEmp(string ip, string password, string empCode, string empName, string endDate, Image photo, out string msg)
        {
            HaCamera cam = new HaCamera() { Ip = ip };
            try
            {
                if (!cam.Connect())
                {
                    msg = "连接失败!";
                    return false;
                }
                if (cam.DeleteFace(empCode))
                {
                    if (photo != null)
                    {
                        if (cam.AddFace(empCode, empName, 1, FaceUtil.ImageToBytes(photo), Convert.ToUInt32(empCode), UInt32.MaxValue))
                        {
                            msg = "添加成功!";
                            return true;
                        }
                    }
                }
                msg = "删除人员失败!";
                return false;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return false;
            }
            finally
            {
                cam.DisConnect();
            }
        }

        public bool DeleteEmp(string ip, string password, string empCode, out string msg)
        {
            HaCamera cam = new HaCamera() { Ip = ip };
            try
            {
                if (!cam.Connect())
                {
                    msg = "连接失败!";
                    return false;
                }
                if (cam.DeleteFace(empCode))
                {
                    msg = "删除人脸成功!";
                    return true;
                }
                else
                {
                    msg = "删除人脸失败!";
                    return false;
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return false;
            }
            finally
            {
                cam.DisConnect();
            }
        }

        public bool IsOnline(string iPAddress)
        {
            try
            {
                Ping sender = new Ping();
                PingReply reply = sender.Send(iPAddress, 200);
                return reply.Status == IPStatus.Success;
            }
            catch
            {
                return false;
            }
        }

        public bool ReSet(string iPAddress, string password, bool v, out string msg)
        {
            HaCamera cam = new HaCamera() { Ip = iPAddress };

            try
            {
                if (!cam.Connect())
                {
                    msg = "连接失败!";
                    return false;
                }
                if (cam.DeleteAllFace())
                {
                    msg = "清空人脸成功!";
                    return true;
                }
                else
                {
                    msg = "清空人脸失败!";
                    return false;
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return false;
            }
            finally
            {
                cam.DisConnect();
            }
        }

        public bool SetTime(string iPAddress, string password, DateTime dt, out string msg)
        {
            HaCamera cam = new HaCamera() { Ip = iPAddress };
            try
            {
                if (!cam.Connect())
                {
                    msg = "连接失败!";
                    return false;
                }
                if (cam.SetTime(dt.ToString("yyyy-MM-dd HH:mm:ss")))
                {
                    msg = "设置时间成功!";
                    return true;
                }
                else
                {
                    msg = "设置时间失败!";
                    return false;
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return false;
            }
            finally
            {
                cam.DisConnect();
            }
        }

        public bool UpdateEmp(string ip, string password, string empCode, string empName, string endDate, Image photo, out string msg)
        {
            HaCamera cam = new HaCamera() { Ip = ip };
            try
            {
                if (!cam.Connect())
                {
                    msg = "连接失败!";
                    return false;
                }
                if (cam.DeleteFace(empCode))
                {
                    if (photo != null)
                    {
                        UInt32 temp = Convert.ToUInt32(empCode);
                        byte[] array = BitConverter.GetBytes(temp);
                        Array.Reverse(array);
                        UInt32 cardNo = BitConverter.ToUInt32(array, 0);
                        return cam.AddFace(empCode, empName, 1, FaceUtil.ImageToBytes(photo), cardNo, UInt32.MaxValue, out msg);
                    }
                }
                msg = "添加人员失败!";
                return false;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return false;
            }
            finally
            {
                cam.DisConnect();
            }
        }

        public bool UpdateEmp(string ip, string password, int empId, string empName, string endDate, byte[] photo, out string msg)
        {
            throw new NotImplementedException();
        }

        public Image Capture(string ip, string fileName, out string msg)
        {
            HaCamera cam = new HaCamera() { Ip = ip };
            try
            {
                if (!cam.Connect())
                {
                    msg = "连接失败!";
                    return null;
                }
                return cam.Snapshot(300, out msg);
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return null;
            }
            finally
            {
                cam.DisConnect();
            }
        }
    }
}
