using HPT.Face.YF.Model;
using HPT.Face.YF.Request;
using HPT.Face.YF.Response;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;

namespace HPT.Face.YF
{
    public class YFFace : HFace
    {
        private const int Port = 8090;
        #region 设备接口

        #region 设置网络参数
        public bool SetNetInfo(string ip, string pass, int isDHCPMod, string newIp, string gateway, string subNetMark, string dns, out string msg)
        {
            var client = new RestClient($"http://{ip}:{Port}/setNetInfo");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            YFRequestSetNetInfo requestSetNetInfo = new YFRequestSetNetInfo()
            {
                Pass = pass,
                IsDHCPMod = isDHCPMod,
                IP = newIp,
                SubnetMask = subNetMark,
                GateWay = gateway,
                DNS = dns
            };
            request.AddParameter("application/x-www-form-urlencoded", requestSetNetInfo.Serialize(), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                msg = "设备无响应!";
                return false;
            }
            YFResultInfo<YFResponseSetNetInfo> result = GetResult<YFResponseSetNetInfo>(response);
            msg = result.Msg;
            return result.Result == 1 && result.Success;
        }
        #endregion

        #region 设备重启
        public bool Restart(string ip, string pass, out string msg)
        {
            var client = new RestClient($"http://{ip}:{Port}/restartDevice");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", $"pass={pass}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                msg = "设备无响应!";
                return false;
            }
            YFResultInfo<string> result = GetResult<string>(response);
            msg = result.Data;
            return result.Result == 1 && result.Success;
        }
        #endregion

        #region 设置时间
        public bool SetTime(string ip, string pass, DateTime dt, out string msg)
        {
            var client = new RestClient($"http://{ip}:{Port}/setTime");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            TimeSpan ts = dt.ToUniversalTime() - new DateTime(1970, 1, 1);
            request.AddParameter("application/x-www-form-urlencoded", $"pass={pass}&timestamp={(long)ts.TotalMilliseconds}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                msg = "设备无响应!";
                return false;
            }
            YFResultInfo<string> result = GetResult<string>(response);
            msg = result.Data;
            return result.Result == 1 && result.Success;
        }
        #endregion


        #region 获取序列号
        public string GetSerialNumber(string ip, out string msg)
        {
            var client = new RestClient($"http://{ip}:{Port}/getDeviceKey");
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                msg = "设备无响应!";
                return string.Empty;
            }
            YFResultInfo<string> result = GetResult<string>(response);
            msg = result.Msg;
            return result.Data;
        }
        #endregion

        #region 修改设备密码
        public bool SetPassword(string ip, string oldPass, string newPass, out string msg)
        {
            var client = new RestClient($"http://{ip}:{Port}/setPassWord");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            YFRequestSetPassword setPassword = new YFRequestSetPassword() { OldPass = oldPass, NewPass = newPass };
            request.AddParameter("application/x-www-form-urlencoded", setPassword.Serialize(), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                msg = "设备无响应!";
                return false;
            }
            YFResultInfo<string> result = GetResult<string>(response);
            msg = result.Data;
            return result.Result == 1 && result.Success;
        }
        #endregion

        #region 修改Logo

        public bool ChangeLogo(string ip, string pass, Image image, out string msg)
        {
            var client = new RestClient($"http://{ip}:{Port}/changeLogo");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            YFRequestChangeLogo changeLogo = new YFRequestChangeLogo() { Pass = pass, Logo = image };
            request.AddParameter("application/x-www-form-urlencoded", changeLogo.Serialize(), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                msg = "设备无响应!";
                return false;
            }
            YFResultInfo<string> result = GetResult<string>(response);
            msg = result.Msg;
            return result.Result == 1 && result.Success;
        }
        #endregion

        #region 设备初始化
        public bool ReSet(string ip, string pass, bool delete, out string msg)
        {
            var client = new RestClient($"http://{ip}:{Port}/device/reset");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", $"delete={delete}&pass={pass}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                msg = "设备无响应!";
                return false;
            }
            YFResultInfo<string> result = GetResult<string>(response);
            msg = result.Msg;
            return result.Result == 1 && result.Success;
        }
        #endregion

        #region 远程开门
        public bool RemoteOpenDoor(string ip, string pass, out string msg)
        {
            var client = new RestClient($"http://{ip}:{Port}/device/openDoorControl");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", $"pass={pass}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                msg = "设备无响应!";
                return false;
            }
            YFResultInfo<string> result = GetResult<string>(response);
            msg = result.Msg;
            return result.Result == 1 && result.Success;
        }
        #endregion

        #region 设备参数设置
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ip">设备IP</param>
        /// <param name="pass">设备密码</param>
        /// <param name="companyName">公司名称</param>
        /// <param name="identifyDistance">识别距离</param>
        /// <param name="identifyScores">识别分数</param>
        /// <param name="saveIdentifyTime">识别间隔</param>
        /// <param name="autoRestart">是否自动重启</param>
        /// <param name="multiplayerDetection">多人脸识别模式</param>
        /// <param name="ttsModType">语音输出模式</param>
        /// <param name="tsModContent">自定义语音输出内容</param>
        /// <param name="displayModType">显示模式</param>
        /// <param name="displayModContent">自定义显示内容</param>
        /// <param name="wg">维根输出内容</param>
        /// <param name="wgType">维根格式</param>
        /// <param name="comModType">串口输出模式</param>
        /// <param name="comModContent">自定义串口输出内容</param>
        /// <param name="slogan">标语</param>
        /// <param name="intro">公司简介</param>
        /// <param name="recRank">识别等级</param>
        /// <param name="recStrangerType">陌生人识别开关</param>
        /// <param name="recStrangerTimesThreshold">陌生人时间判定等级</param>
        /// <param name="ttsModStrangerType">陌生人语音输出模式</param>
        /// <param name="ttsModStrangerContent">自定义陌生人语音输出内容</param>
        /// <param name="relayRate">继电器执行时间</param>
        /// <param name="lightPattern">灯光模式</param>
        /// <param name="msg">返回消息</param>
        /// <returns></returns>
        public bool SetConfig(string ip, string pass, string companyName, int identifyDistance, int identifyScores, int saveIdentifyTime, bool autoRestart,
            int multiplayerDetection, int ttsModType, string tsModContent, int displayModType, string displayModContent, string wg, int wgType,
            int comModType, string comModContent, string slogan, string intro, int recRank, int recStrangerType, int recStrangerTimesThreshold, int ttsModStrangerType,
            string ttsModStrangerContent, int relayRate, int lightPattern, out string msg)
        {

            var client = new RestClient($"http://{ip}:{Port}/setConfig");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            YFRequestSetConfig setConfig = new YFRequestSetConfig()
            {
                Pass = pass,
                Data = new YFConfig()
                {
                    CompanyName = companyName,
                    IdentifyDistance = identifyDistance,
                    IdentifyScores = identifyScores,
                    SaveIdentifyTime = saveIdentifyTime,
                    MultiplayerDetection = multiplayerDetection,
                    TtsModType = ttsModType,
                    TtsModContent = tsModContent,
                    DisplayModType = displayModType,
                    DisplayModContent = displayModContent,
                    Wg = wg,
                    WgType = wgType,
                    ComModType = comModType,
                    ComModContent = comModContent,
                    Slogan = slogan,
                    Intro = intro,
                    RecRank = recRank,
                    RecStrangerType = recStrangerType,
                    RecStrangerTimesThreshold = recStrangerTimesThreshold,
                    TtsModStrangerType = ttsModStrangerType,
                    TtsModStrangerContent = ttsModStrangerContent,
                    RelayRate = relayRate,
                    LightPattern = lightPattern
                }
            };
            try
            {
                request.AddParameter("application/x-www-form-urlencoded", setConfig.Serialize(), ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    msg = "设备无响应!";
                    return false;
                }
                YFResultInfo<YFConfig> result = GetResult<YFConfig>(response);
                msg = result.Msg;
                return result.Result == 1 && result.Success;

            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return false;
            }
        }
        #endregion

        #region 设置识别回调函数
        public bool SetIdentifyCallBack(string ip, string pass, string url, out string msg)
        {
            var client = new RestClient($"http://{ip}:{Port}/setIdentifyCallBack");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", $"pass={pass}&callbackUrl={url}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                msg = "设备无响应!";
                return false;
            }
            YFResultInfo<string> result = GetResult<string>(response);
            msg = result.Msg;
            return result.Result == 1 && result.Success;
        }
        #endregion

        #region 设置设备心跳URL
        public bool SetDeviceHeartBeat(string ip, string pass, string url, out string msg)
        {
            var client = new RestClient($"http://{ip}:{Port}/setDeviceHeartBeat");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", $"pass={pass}&url={url}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                msg = "设备无响应!";
                return false;
            }
            YFResultInfo<string> result = GetResult<string>(response);
            msg = result.Msg;
            return result.Result == 1 && result.Success;
        }
        #endregion

        #region 设置拍照注册回调URL
        public bool SetImgRegCallBack(string ip, string pass, string url, out string msg)
        {
            var client = new RestClient($"http://{ip}:{Port}/setImgRegCallBack");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", $"pass={pass}&url={url}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                msg = "设备无响应!";
                return false;
            }
            YFResultInfo<string> result = GetResult<string>(response);
            msg = result.Msg;
            return result.Result == 1 && result.Success;
        }
        #endregion

        #endregion

        #region 人员接口

        #region 查询人员信息
        public bool FindPersons(string ip, string pass, out List<YFPerson> persons, string personId = "-1")
        {
            var client = new RestClient($"http://{ip}:{Port}/person/find");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", $"pass=12345678&id={personId}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                persons = null;
                return false;
            }
            YFResultInfo<List<YFPerson>> result = GetResult<List<YFPerson>>(response);
            persons = new List<YFPerson>();
            if (result.Result == 1)
            {
                if (result.Data != null)
                    persons.AddRange(result.Data);
                return true;
            }
            else
            {
                persons = null;
                return false;
            }
        }
        #endregion


        #region 创建人员
        public bool CreatePerson(string ip, string password, string id, string idCardNum, string name, out string msg)
        {
            var client = new RestClient($@"http://{ip}:{Port}/person/create");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            YFRequestPerson requestPerson = new YFRequestPerson() { Pass = password, Person = new YFPerson() { Id = id, IdcardNum = idCardNum, Name = name } };
            request.AddParameter("application/x-www-form-urlencoded", requestPerson.Serialize(), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                msg = "设备无响应!";
                return false;
            }
            YFResultInfo<YFPerson> result = GetResult<YFPerson>(response);
            msg = result.Msg;
            return result.Result == 1 && result.Success;
        }
        #endregion

        #region 更新人员信息
        public bool UpdatePerson(string ip, string password, string id, string idCardNum, string name, out string msg)
        {
            var client = new RestClient($@"http://{ip}:{Port}/person/update");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            YFRequestPerson requestPerson = new YFRequestPerson() { Pass = password, Person = new YFPerson() { Id = id, IdcardNum = idCardNum, Name = name } };
            request.AddParameter("application/x-www-form-urlencoded", requestPerson.Serialize(), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                msg = "设备无响应!";
                return false;
            }
            YFResultInfo<YFPerson> result = GetResult<YFPerson>(response);
            msg = result.Msg;
            return result.Result == 1 && result.Success;
        }
        #endregion

        #region 删除人员信息
        public bool DeletePerson(string ip, string password, string id, out string msg)
        {
            var client = new RestClient($@"http://{ip}:{Port}/person/delete");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", $"pass={password}&id={id}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                msg = "设备无响应!";
                return false;
            }
            YFResultInfo<YFResponseDeletePerson> result = GetResult<YFResponseDeletePerson>(response);
            msg = result.Msg;
            return result.Result == 1 && result.Success;
        }
        #endregion

        #region 添加通行时间
        public bool CreatePasstime(string ip, string password, string personId, string passtime, out string msg)
        {
            var client = new RestClient($@"http://{ip}:{Port}/person/createPasstime");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            YFRequestPasstime reqPasstime = new YFRequestPasstime()
            {
                Pass = password,
                Passtime = new YFPassTime()
                {
                    personId = personId,
                    passtime = passtime
                }
            };
            request.AddParameter("application/x-www-form-urlencoded", reqPasstime.Serialize(), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                msg = "设备无响应!";
                return false;
            }
            YFResultInfo<string> result = GetResult<string>(response);
            msg = result.Msg;
            return result.Result == 1 && result.Success;
        }
        #endregion

        #region 删除通行时间
        public bool DeletePasstime(string ip, string password, string personId, out string msg)
        {
            var client = new RestClient($@"http://{ip}:{Port}/person/deletePasstime");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", $"pass={password}&personId={personId}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                msg = "设备无响应!";
                return false;
            }
            YFResultInfo<string> result = GetResult<string>(response);
            msg = result.Msg;
            return result.Result == 1 && result.Success;
        }
        #endregion

        #region 添加有效期
        public bool CreatePermissions(string ip, string password, string personId, string time, out string msg)
        {
            var client = new RestClient($@"http://{ip}:{Port}/person/permissionsCreate");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", $"pass={password}&personId={personId}&time={time}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                msg = "设备无响应!";
                return false;
            }
            YFResultInfo<string> result = GetResult<string>(response);
            msg = result.Msg;
            return result.Result == 1 && result.Success;
        }
        #endregion

        #region 删除有效期
        public bool DeletePermissions(string ip, string password, string personId, out string msg)
        {
            var client = new RestClient($@"http://{ip}:{Port}/person/permissionsDelete");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", $"pass={password}&personId={personId}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                msg = "设备无响应!";
                return false;
            }
            YFResultInfo<string> result = GetResult<string>(response);
            msg = result.Msg;
            return result.Result == 1 && result.Success;
        }
        #endregion

        #region 查询记录
        public List<YFRecord> FindRecords(string ip, string password, string personId, int length, int index, string startTime, string endTime, out string msg)
        {
            var client = new RestClient($@"http://{ip}:{Port}/findRecords ");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            YFRequestFindRecords findRecords = new YFRequestFindRecords()
            {
                Pass = password,
                PersonId = personId,
                PageSize = length,
                PageIndex = index,
                BeginTime = startTime,
                EndTime = endTime
            };
            request.AddParameter("application/x-www-form-urlencoded", findRecords.Serialize(), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                msg = "设备无响应!";
                return null;
            }
            YFResultInfo<YFResponseRecords> result = GetResult<YFResponseRecords>(response);
            msg = result.Msg;
            List<YFRecord> records = new List<YFRecord>();
            if (result.Result == 1 && result.Success)
                records.AddRange(result.Data.records);
            return records;
        }
        #endregion

        #region 删除记录(时间字符串)
        public bool DeleteRecords(string ip, string password, string personId, DateTime time, out string msg)
        {
            var client = new RestClient($@"http://{ip}:{Port}/deleteRecords");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", $"pass={password}&time={time.ToString("yyyy-MM-dd HH:mm:ss")}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                msg = "设备无响应!";
                return false;
            }
            YFResultInfo<string> result = GetResult<string>(response);
            msg = result.Msg;
            return result.Result == 1 && result.Success;
        }
        #endregion

        #region 删除记录(时间戳)
        public bool DeleteRecordsByUnixTime(string ip, string password, string personId, DateTime time, out string msg)
        {
            var client = new RestClient($@"http://{ip}:{Port}/deleteRecordsByUnixTime");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0, 0));
            long unixTime = (time.Ticks - startTime.Ticks) / 10000;   //除10000调整为13位      
            request.AddParameter("application/x-www-form-urlencoded", $"pass={password}&unixTime={unixTime}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                msg = "设备无响应!";
                return false;
            }
            YFResultInfo<string> result = GetResult<string>(response);
            msg = result.Msg;
            return result.Result == 1 && result.Success;
        }
        #endregion


        #endregion

        #region 人脸接口

        #region 检查人脸是否及格
        public bool CheckFace(string iP, string password, Image image, out string msg)
        {
            byte[] arr = Encoding.GetEncoding("gb2312").GetBytes("李梦洁");
            string personId = string.Empty;
            foreach (byte b in arr)
            {
                personId += b.ToString("X2");
            }
            List<YFPerson> persons;
            if (FindPersons(iP, password, out persons, personId))
            {
                if (persons.Count != 0)
                    DeleteEmp(iP, password, personId, out msg);
                return CreatePerson(iP, password, personId, "", "李梦洁", out msg)//添加人员
                            && CreateFace(iP, password, personId, personId, image, out msg)//添加人脸
                            && DeletePerson(iP, password, personId, out msg);//删除人员
            }
            else
            {
                msg = "设备无响应!";
                return false;
            }
        }
        #endregion

        #region  添加人脸
        public bool CreateFace(string iP, string pass, string personId, string faceId, Image face, out string msg)
        {
            var client = new RestClient($@"http://{iP}:{Port}/face/create");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            RequestFace faceAdd = new RequestFace() { Pass = pass, PersonId = personId, FaceId = faceId, FaceImage = face };
            request.AddParameter("application/x-www-form-urlencoded", faceAdd.Serialize(), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                msg = "设备无响应!";
                return false;
            }
            YFResultInfo<string> result = GetResult<string>(response);
            msg = result.Msg;
            return result.Result == 1 && result.Success;
        }
        #endregion

        #region  更新人脸
        public bool UpdateFace(string iP, string pass, string personId, string faceId, Image face, out string msg)
        {
            var client = new RestClient($@"http://{iP}:{Port}/face/update");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            RequestFace faceUpdate = new RequestFace() { Pass = pass, PersonId = personId, FaceId = faceId, FaceImage = face };
            request.AddParameter("application/x-www-form-urlencoded", faceUpdate.Serialize(), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                msg = "设备无响应!";
                return false;
            }
            YFResultInfo<string> result = GetResult<string>(response);
            msg = result.Msg;
            return result.Result == 1 && result.Success;
        }
        #endregion

        #region  删除单张人脸
        public bool DeleteFace(string iP, string pass, string personId, string faceId, out string msg)
        {
            var client = new RestClient($@"http://{iP}:{Port}/face/delete");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", $"pass={pass}&personId={personId}&faceId={faceId}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                msg = "设备无响应!";
                return false;
            }
            YFResultInfo<string> result = GetResult<string>(response);
            msg = result.Msg;
            return result.Result == 1 && result.Success;
        }
        #endregion

        #region  删除人员所有人脸
        public bool ClearFaces(string iP, string pass, string personId, out string msg)
        {
            var client = new RestClient($@"http://{iP}:{Port}/face/deletePerson");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", $"pass={pass}&personId={personId}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                msg = "设备无响应!";
                return false;
            }
            YFResultInfo<string> result = GetResult<string>(response);
            msg = result.Msg;
            return result.Result == 1;
        }
        #endregion

        #region 查询人脸
        public List<YFFaceData> FindFaces(string iP, string pass, string personId, out string msg)
        {
            var client = new RestClient($@"http://{iP}:{Port}/face/find");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", $"pass={pass}&personId={personId}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                msg = "设备无响应!";
                return null;
            }
            YFResultInfo<List<YFFaceData>> result = GetResult<List<YFFaceData>>(response);
            msg = result.Msg;
            if (result.Result == 1)
            {
                if (result.Success)
                {
                    return result.Data;
                }
            }
            return null;
        }
        #endregion


        #endregion


        #region 数据解析

        private YFResultInfo<T> GetResult<T>(IRestResponse response) where T : class
        {

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                YFResultInfo<T> result = new YFResultInfo<T>();
                result.Success = false;
                result.Data = null;
                result.Msg = "设备无响应";
                result.Result = 0;
                return result;
            }
            if (response.ErrorException != null)
            {
                YFResultInfo<T> result = new YFResultInfo<T>();
                result.Success = false;
                result.Data = null;
                result.Msg = response.ErrorException.Message;
                result.Result = 0;
                return result;
            }
            return Deserialize<T>(response);
        }

        private YFResultInfo<T> Deserialize<T>(IRestResponse response) where T : class
        {


            MemoryStream memoryStream = null;
            try
            {
                memoryStream = new MemoryStream(response.RawBytes);

                using (StreamReader sr = new StreamReader(memoryStream, Encoding.UTF8))
                {
                    return JsonConvert.DeserializeObject(sr.ReadToEnd(), typeof(YFResultInfo<T>), new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }) as YFResultInfo<T>;
                }
            }
            catch (Exception ex)
            {
                YFResultInfo<T> result = new YFResultInfo<T>();
                result.Result = 1;
                result.Success = false;
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

        #region 通用接口
        public bool CreateEmp(string ip, string password, string empCode, string empName, string endDate, Image photo, out string msg)
        {
            List<YFPerson> persons;
            if (!FindPersons(ip, password, out persons, empCode))
            {
                msg = "设备无响应!";
                return false;
            }
            if (persons == null)
            {
                msg = "数据解析错误";
                return false;
            }
            if (persons.Count == 0)
                return CreatePerson(ip, password, empCode, "", empName, out msg)
                    && CreateFace(ip, password, empCode, empCode, photo, out msg);
            else
                return UpdatePerson(ip, password, empCode, "", empName, out msg)
                    && UpdateFace(ip, password, empCode, empCode, photo, out msg);
        }

        public bool UpdateEmp(string ip, string password, string empCode, string empName, string endDate, Image photo, out string msg)
        {
            List<YFPerson> persons;
            if (!FindPersons(ip, password, out persons, empCode))
            {
                msg = "设备无响应!";
                return false;
            }
            if (persons == null)
            {
                msg = "数据解析错误";
                return false;
            }
            if (persons.Count == 0)
                return CreatePerson(ip, password, empCode, empCode, empName, out msg)
                    && CreateFace(ip, password, empCode, empCode, photo, out msg);
            else
                return UpdatePerson(ip, password, empCode, empCode, empName, out msg) && ClearFaces(ip, password, empCode, out msg)
                    && CreateFace(ip, password, empCode, empCode, photo, out msg);
        }
        public bool UpdateEmp(string ip, string password, int empId, string empName, string endDate, byte[] photo, out string msg)
        {
            throw new NotImplementedException();
        }
        public bool DeleteEmp(string ip, string password, string empCode, out string msg)
        {
            return DeletePerson(ip, password, empCode, out msg);
        }

        public bool UpdateEmp(string ip, string password, string empCode, string empName, string endDate, byte[] photo, out string msg)
        {
            throw new NotImplementedException();
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

        #endregion


    }
}
