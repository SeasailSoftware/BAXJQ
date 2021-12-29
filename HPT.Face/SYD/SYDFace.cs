using HPT.Face.SYD.Response;
using HPT.Face.Utils;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;

namespace HPT.Face.SYD
{
    public class SYDFace : HFace
    {
        private const int Port = 8088;
        #region 设备接口

        #region 抓拍
        public bool Capture(string ip, string password, out Image img)
        {
            var client = new RestClient($"http://{ip}:{Port}/photo");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", $"key={password}&tipsBefore=正在抓拍&tipsAfter抓拍成功&count=1", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                img = null;
                return false;
            }
            TResult<SYDResponseCapture> result = GetResult<SYDResponseCapture>(response);
            if (result.Status == 0)
            {
                if (result.Data != null && result.Data.photos != null && result.Data.photos.Count > 0)
                {
                    img = FaceUtil.Base64StringToImage(result.Data.photos[0]);
                    return true;
                }
            }
            img = null;
            return false;
        }
        #endregion


        #region 设备重启
        public bool Reboot(string ip, string password, out string msg)
        {
            var client = new RestClient($"http://{ip}:{Port}/reboot");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", $"key={password}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                msg = "设备无响应";
                return false;
            }
            TResult<string> result = GetResult<string>(response);
            msg = result.Msg;
            return result.Status == 0;
        }
        #endregion

        #region 设备开闸
        public bool Open(string ip, string password, out string msg)
        {
            var client = new RestClient($"http://{ip}:{Port}/open");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", $"key={password}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                msg = "设备无响应";
                return false;
            }
            TResult<string> result = GetResult<string>(response);
            msg = result.Msg;
            return result.Status == 0;
        }
        #endregion


        #region 修改设备密码
        public bool SetPassword(string ip, string key, string newKey, out string msg)
        {
            var client = new RestClient($"http://{ip}:{Port}/setDeviceKey");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", $"key={key}&newKey={newKey}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                msg = "设备无响应!";
                return false;
            }
            TResult<string> result = GetResult<string>(response);
            msg = result.Data;
            return result.Status == 0;
        }
        #endregion

        #region 获取基础参数
        public SYDDeviceInfo GetDeviceInfo(string ip, string password, out string msg)
        {
            var client = new RestClient($"http://{ip}:{Port}/getDeviceInfo?key={password}");
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                msg = "设备无响应!";
                return null;
            }
            TResult<SYDDeviceInfo> result = GetResult<SYDDeviceInfo>(response);
            msg = result.Msg;
            return result.Data;
        }
        #endregion

        #region 设置基础参数
        public bool SetDeviceInfo(string ip, string password, SYDDeviceInfo device, out string msg)
        {
            var client = new RestClient($"http://{ip}:{Port}/setDeviceInfo");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", $"key={password}&{device.Serialize()}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                msg = "设备无响应!";
                return false;
            }
            TResult<string> result = GetResult<string>(response);
            msg = result.Data;
            return result.Status == 0;
        }
        #endregion

        #region 获取时间

        public bool GetTime(string ip, string password, out DateTime dt)
        {
            var client = new RestClient($"http://{ip}:{Port}/getTime?key={password}");
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                dt = DateTime.MinValue;
                return false;
            }
            TResult<SYDResponseGetTime> result = GetResult<SYDResponseGetTime>(response);
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            if (result.Data != null)
            {
                var val = Convert.ToDecimal(Decimal.Parse(result.Data.ts, System.Globalization.NumberStyles.Float));
                var lTime = (long)(val * 10000000);
                TimeSpan toNow = new TimeSpan(lTime);
                dt = dtStart.Add(toNow);
                return true;
            }
            dt = DateTime.MinValue;
            return false;
        }

        #endregion

        #region 设置时间

        public bool SetTime(string ip, string password, DateTime dt, out string msg)
        {
            var client = new RestClient($"http://{ip}:{Port}/setTime");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1)); // 当地时区
            double timeStamp = (dt - startTime).TotalMilliseconds; // 相差毫秒数
            request.AddParameter("application/x-www-form-urlencoded", $"key={password}&ts={timeStamp}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                msg = "设备无响应";
                return false;
            }
            TResult<string> result = GetResult<string>(response);
            msg = result.Msg;
            return result.Status == 0;
        }

        #endregion

        #endregion

        #region 人员接口

        #region 获取所有人员
        public List<SYDPerson> PersonList(string ip, string password, out string msg)
        {
            var client = new RestClient($"http://{ip}:{Port}/listPerson?key={password}");
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                msg = "设备无响应";
                return null;
            }
            TResult<SYDResponseListPerson> result = GetResult<SYDResponseListPerson>(response);
            msg = result.Msg;
            if (result.Status == 0)
            {
                return result.Data.person;
            }
            return null;
        }
        #endregion

        #region 获取所有人员
        public SYDPerson GetPerson(string ip, string password, string personId, out string msg)
        {

            var client = new RestClient($"http://{ip}:{Port}/getPerson?key={password}&id={personId}");
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                msg = "设备无响应";
                return null;
            }
            TResult<SYDPerson> result = GetResult<SYDPerson>(response);
            msg = result.Msg;
            if (result.Status == 0)
            {
                return result.Data;
            }
            return null;
        }
        #endregion

        #region 检查人脸是否可用

        public bool CheckFace(string ip, string password, string imgBase64String, out string msg)
        {
            var client = new RestClient($"http://{ip}:{Port}/check");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", $"key={password}&photo={imgBase64String}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                msg = "设备无响应";
                return false;
            }
            TResult<SYDPhotoValidater> result = GetResult<SYDPhotoValidater>(response);
            bool flag = false;
            if (result.Status == 0)
            {
                switch (result.Data.isValid)
                {
                    case 0:
                        /*
                        if (result.Data.isExist)
                            msg = "人脸已经存在!";
                        else
                        {
                            msg = "成功!";
                            flag = true;
                        }
                        */
                        msg = "成功!";
                        flag = true;
                        break;
                    case 1:
                        msg = "图片尺寸错误!";
                        break;
                    case 2:
                        msg = "图片格式错误!";
                        break;
                    case 3:
                        msg = "找不到人脸!";
                        break;
                    case 4:
                        msg = "特征值提取失败!";
                        break;
                    default:
                        msg = "未知错误!";
                        break;
                }
            }
            else
                msg = result.Msg;
            return flag;
        }

        #endregion

        #region 添加人员
        public bool SetPerson(string ip, string password, SYDPerson person, out string msg)
        {
            DeletePerson(ip, password, new List<string>() { person.id }, out msg);
            //if (!CheckFace(ip, password, person.photo, out msg)) return false;
            var client = new RestClient($"http://{ip}:{Port}/setPerson");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            StringBuilder buffer = new StringBuilder();
            buffer.Append($"key={password}")
                .Append($"&id={person.id}")
                .Append($"&name={person.name}")
                .Append($"&IC_NO={person.IC_NO}")
                .Append($"&ID_NO={person.ID_NO}")
                .Append($"&photo={person.photo}")
                .Append($"&passCount={person.passCount}")
                .Append($"&startTs={person.startTs}")
                .Append($"&endTs={person.endTs}");
            request.AddParameter("application/x-www-form-urlencoded", buffer.ToString(), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                msg = "设备无响应";
                return false;
            }
            TResult<string> result = GetResult<string>(response);
            msg = result.Msg;
            if (msg.Contains("人脸已经存在")) return true;
            return result.Status == 0;
        }

        #endregion

        #region 批量删除人员
        public bool DeletePerson(string ip, string password, List<string> ids, out string msg)
        {
            var client = new RestClient($"http://{ip}:{Port}/removePerson");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            StringBuilder buffer = new StringBuilder();
            buffer.Append("[");
            foreach (string id in ids)
            {
                buffer.Append($"{id},");
            }
            buffer.Append("-1]");
            request.AddParameter("application/x-www-form-urlencoded", $"key={password}&id={buffer.ToString()}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                msg = "设备无响应";
                return false;
            }
            TResult<string> result = GetResult<string>(response);
            msg = result.Msg;
            return result.Status == 0;
        }
        #endregion

        #region 获取记录
        public List<SYDRecord> GetRecords(string ip, string password, DateTime time, int count, out string msg)
        {
            var client = new RestClient($"http://{ip}:{Port}/listRecord?key={password}&ts={ConvertHelper.DateTimeToTimeStamp(time) / 1000}&count={count}");
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                msg = "设备无响应";
                return null;
            }
            TResult<SYDResponseRecords> result = GetResult<SYDResponseRecords>(response);
            msg = result.Msg;
            if (result.Status == 0)
                return result.Data.record;
            return null;
        }
        #endregion

        #region 删除记录
        public bool DeleteRecords(string ip, string password, DateTime time, out string msg)
        {
            var client = new RestClient($"http://{ip}:{Port}/removeRecord");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", $"key={password}&ts={ConvertHelper.DateTimeToTimeStamp(time) / 1000}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                msg = "设备无响应";
                return false;
            }
            TResult<string> result = GetResult<string>(response);
            msg = result.Msg;
            return result.Status == 0;
        }
        #endregion
        #endregion

        #region 数据解析

        private TResult<T> GetResult<T>(IRestResponse response) where T : class
        {

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                TResult<T> result = new TResult<T>();
                result.Data = null;
                result.Msg = "设备无响应";
                result.Status = 1;
                return result;
            }
            if (response.ErrorException != null)
            {
                TResult<T> result = new TResult<T>();
                result.Status = 1;
                result.Data = null;
                result.Msg = response.ErrorException.Message;
                return result;
            }
            return Deserialize<T>(response);
        }

        private TResult<T> Deserialize<T>(IRestResponse response) where T : class
        {


            MemoryStream memoryStream = null;
            try
            {
                memoryStream = new MemoryStream(response.RawBytes);

                using (StreamReader sr = new StreamReader(memoryStream, Encoding.UTF8))
                {
                    return JsonConvert.DeserializeObject(sr.ReadToEnd(), typeof(TResult<T>), new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }) as TResult<T>;
                }
            }
            catch (Exception ex)
            {
                TResult<T> result = new TResult<T>();
                result.Status = 1;
                result.Msg = ex.Message;
                return result;
            }
            finally
            {
                if (memoryStream != null)
                {
                    memoryStream.Dispose();
                }
            }

        }


        #endregion



        #region 添加人员信息
        public bool CreateEmp(string iPAddress, string password, string empId, string empName, string endDate, Image photo, out string msg)
        {
            SYDPerson person = new SYDPerson();
            person.id = empId;
            person.name = empName;
            person.IC_NO = empId;
            person.passCount = 1000;
            person.startTs = ConvertHelper.DateTimeToTimeStamp(DateTime.Now) / 1000;
            person.endTs = ConvertHelper.DateTimeToTimeStamp(DateTime.Parse(endDate)) / 1000;
            person.photo = ConvertHelper.ImageToBase64String(photo);
            return SetPerson(iPAddress, password, person, out msg);
        }
        #endregion

        #region 删除人员
        public bool DeleteEmp(string iPAddress, string password, string empId, out string msg)
        {
            return DeletePerson(iPAddress, password, new List<string>() { empId.ToString() }, out msg);
        }
        #endregion

        #region 更新人员信息
        public bool UpdateEmp(string ip, string password, string empCode, string empName, string endDate, Image photo, out string msg)
        {
            SYDPerson person = new SYDPerson();
            person.id = empCode;
            person.name = empName;
            UInt32 cardNo = Convert.ToUInt32(empCode);
            person.IC_NO = UInt32ToHexString(cardNo);
            person.passCount = 1000;
            person.startTs = ConvertHelper.DateTimeToTimeStamp(DateTime.Now) / 1000;
            person.endTs = ConvertHelper.DateTimeToTimeStamp(DateTime.Parse(endDate)) / 1000;
            person.photo = ConvertHelper.ImageToBase64String(photo);
            return SetPerson(ip, password, person, out msg);
        }

        private string UInt32ToHexString(UInt32 temp)
        {
            string cardNo = string.Empty;
            byte[] arr = BitConverter.GetBytes(temp);
            foreach (byte b in arr)
            {
                cardNo += b.ToString("X2");
            }
            return cardNo;
        }

        public bool UpdateEmp(string ip, string password, int empId, string empName, string endDate, byte[] photo, out string msg)
        {
            SYDPerson person = new SYDPerson();
            person.id = empId.ToString("00000000");
            person.name = empName;
            person.IC_NO = empId.ToString("00000000");
            person.passCount = 1000;
            person.startTs = ConvertHelper.DateTimeToTimeStamp(DateTime.Now) / 1000;
            person.endTs = ConvertHelper.DateTimeToTimeStamp(DateTime.Parse(endDate)) / 1000;
            person.photo = ConvertHelper.BytesToBase64String(photo);
            return SetPerson(ip, password, person, out msg);
        }


        #endregion

        public bool ReSet(string iPAddress, string password, bool v, out string msg)
        {
            var client = new RestClient($"http://{iPAddress}:{Port}/removePerson");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", $"key={password}&id=[]", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                msg = "设备无响应";
                return false;
            }
            TResult<string> result = GetResult<string>(response);
            msg = result.Msg;
            return result.Status == 0;
        }

        public bool CheckFace(string iPAddress, string password, Image image, out string msg)
        {
            string imgBase64String = ConvertHelper.ImageToBase64String(image);
            return CheckFace(iPAddress, password, imgBase64String, out msg);
        }

        public bool IsOnline(string iPAddress)
        {
            Ping sender = new Ping();
            PingReply reply = sender.Send(iPAddress, 200);
            return reply.Status == IPStatus.Success;
        }

        public Image Capture(string iPaddress, string fileName, out string msg)
        {
            throw new NotImplementedException();
        }
    }
}
