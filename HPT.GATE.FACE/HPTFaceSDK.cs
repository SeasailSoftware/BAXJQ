using HPT.Face.Request;
using HPT.Face.Response;
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

namespace HPT.Face
{
    public class HPTFaceSDK
    {
        private const int Port = 8090;
        #region 设备接口

        #region 设置网络参数
        public static bool SetNetInfo(string ip, string pass, int isDHCPMod, string newIp, string gateway, string subNetMark, string dns, out string msg)
        {
            var client = new RestClient($"http://{ip}:{Port}/setNetInfo");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            RequestSetNetInfo requestSetNetInfo = new RequestSetNetInfo()
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
                msg = "无法连接设备!";
                return false;
            }
            ResultInfo<ResponseSetNetInfo> result = GetResult<ResponseSetNetInfo>(response);
            msg = result.Msg;
            return result.Result == 1 && result.Success;
        }
        #endregion

        #region 设备重启
        public static bool Restart(string ip, string pass, out string msg)
        {
            var client = new RestClient($"http://{ip}:{Port}/restartDevice");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", $"pass={pass}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                msg = "无法连接设备!";
                return false;
            }
            ResultInfo<string> result = GetResult<string>(response);
            msg = result.Data;
            return result.Result == 1 && result.Success;
        }
        #endregion

        #region 设置时间
        public static bool SetTime(string ip, string pass, out string msg)
        {
            var client = new RestClient($"http://{ip}:{Port}/setTime");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            TimeSpan ts = DateTime.Now.ToUniversalTime() - new DateTime(1970, 1, 1);
            request.AddParameter("application/x-www-form-urlencoded", $"pass={pass}&timestamp={(long)ts.TotalMilliseconds}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                msg = "无法连接设备!";
                return false;
            }
            ResultInfo<string> result = GetResult<string>(response);
            msg = result.Data;
            return result.Result == 1 && result.Success;
        }
        public static bool SetTime(string ip, string pass, DateTime dt, out string msg)
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
                msg = "无法连接设备!";
                return false;
            }
            ResultInfo<string> result = GetResult<string>(response);
            msg = result.Data;
            return result.Result == 1 && result.Success;
        }

        #endregion


        #region 获取序列号
        public static string GetSerialNumber(string ip, out string msg)
        {
            var client = new RestClient($"http://{ip}:{Port}/getDeviceKey");
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                msg = "无法连接设备!";
                return string.Empty;
            }
            ResultInfo<string> result = GetResult<string>(response);
            msg = result.Msg;
            return result.Data;
        }
        #endregion

        #region 修改设备密码
        public static bool SetPassword(string ip, string oldPass, string newPass, out string msg)
        {
            var client = new RestClient($"http://{ip}:{Port}/setPassWord");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            RequestSetPassword setPassword = new RequestSetPassword() { OldPass = oldPass, NewPass = newPass };
            request.AddParameter("application/x-www-form-urlencoded", setPassword.Serialize(), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                msg = "无法连接设备!";
                return false;
            }
            ResultInfo<string> result = GetResult<string>(response);
            msg = result.Data;
            return result.Result == 1 && result.Success;
        }
        #endregion

        #region 修改Logo

        public static bool ChangeLogo(string ip, string pass, Image image, out string msg)
        {
            var client = new RestClient($"http://{ip}:{Port}/changeLogo");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            RequestChangeLogo changeLogo = new RequestChangeLogo() { Pass = pass, Logo = image };
            request.AddParameter("application/x-www-form-urlencoded", changeLogo.Serialize(), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                msg = "无法连接设备!";
                return false;
            }
            ResultInfo<string> result = GetResult<string>(response);
            msg = result.Msg;
            return result.Result == 1 && result.Success;
        }
        #endregion

        #region 设备初始化
        public static bool Reset(string ip, string pass, bool delete, out string msg)
        {
            var client = new RestClient($"http://{ip}:{Port}/device/reset");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", $"delete={delete}&pass={pass}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                msg = "无法连接设备!";
                return false;
            }
            ResultInfo<string> result = GetResult<string>(response);
            msg = result.Msg;
            return result.Result == 1 && result.Success;
        }
        #endregion

        #region 远程开门
        public static bool RemoteOpenDoor(string ip, string pass, out string msg)
        {
            var client = new RestClient($"http://{ip}:{Port}/device/openDoorControl");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", $"pass={pass}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                msg = "无法连接设备!";
                return false;
            }
            ResultInfo<string> result = GetResult<string>(response);
            msg = result.Msg;
            return result.Result == 1 && result.Success;
        }
        #endregion

        #region 获取设备参数
        public static Config GetConfig(string ip, string pass, out string msg)
        {

            var client = new RestClient($"http://{ip}:{Port}/getConfig");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            try
            {
                request.AddParameter("application/x-www-form-urlencoded", $"pass={pass}", ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    msg = "无法连接设备!";
                    return null;
                }
                ResultInfo<Config> result = GetResult<Config>(response);
                msg = result.Msg;
                return result.Data;

            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return null;
            }
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
        public static bool SetConfig(string ip, string pass, string companyName, int identifyDistance, int identifyScores, int saveIdentifyTime, bool autoRestart,
            int multiplayerDetection, int ttsModType, string tsModContent, int displayModType, string displayModContent, string wg, int wgType,
            int comModType, string comModContent, string slogan, string intro, int recRank, int recStrangerType, int recStrangerTimesThreshold, int ttsModStrangerType,
            string ttsModStrangerContent, int relayRate, int lightPattern, int volume, int workMode, out string msg)
        {

            var client = new RestClient($"http://{ip}:{Port}/setConfig");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            RequestSetConfig setConfig = new RequestSetConfig()
            {
                Pass = pass,
                Data = new Config()
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
                    LightPattern = lightPattern,
                    Volume = volume,
                    WorkMode = workMode
                }
            };
            try
            {
                request.AddParameter("application/x-www-form-urlencoded", setConfig.Serialize(), ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    msg = "无法连接设备!";
                    return false;
                }
                ResultInfo<Config> result = GetResult<Config>(response);
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

        #region 设置待机参数
        public static bool SetStandbyParas(string ip, string password, int waitTime, int interval, out string msg)
        {
            var client = new RestClient($@"http://{ip}:{Port}/standy/param");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", $"pass={password}&time={waitTime}&&interval={interval}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                msg = "无法连接设备!";
                return false;
            }
            ResultInfo<string> result = GetResult<string>(response);
            msg = result.Msg;
            return result.Result == 1 && result.Success;
        }
        #endregion


        #region 设置识别回调函数
        public static bool SetIdentifyCallBack(string ip, string pass, string url, out string msg)
        {
            var client = new RestClient($"http://{ip}:{Port}/setIdentifyCallBack");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", $"pass={pass}&callbackUrl={url}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                msg = "无法连接设备!";
                return false;
            }
            ResultInfo<string> result = GetResult<string>(response);
            msg = result.Msg;
            return result.Result == 1 && result.Success;
        }
        #endregion

        #region 设置设备心跳URL
        public static bool SetDeviceHeartBeat(string ip, string pass, string url, out string msg)
        {
            var client = new RestClient($"http://{ip}:{Port}/setDeviceHeartBeat");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", $"pass={pass}&url={url}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                msg = "无法连接设备!";
                return false;
            }
            ResultInfo<string> result = GetResult<string>(response);
            msg = result.Msg;
            return result.Result == 1 && result.Success;
        }
        #endregion

        #region 设置拍照注册回调URL
        public static bool SetImgRegCallBack(string ip, string pass, string url, out string msg)
        {
            var client = new RestClient($"http://{ip}:{Port}/setImgRegCallBack");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", $"pass={pass}&url={url}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                msg = "无法连接设备!";
                return false;
            }
            ResultInfo<string> result = GetResult<string>(response);
            msg = result.Msg;
            return result.Result == 1 && result.Success;
        }
        #endregion

        #region 获取广告轮播图
        public static bool GetAdvertismen(string ip, string pass, int index, out Advertismen advertismen, out string msg)
        {
            var client = new RestClient($"http://{ip}:{Port}//advertisment/show");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", $"pass={pass}&index={index}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                msg = "无法连接设备!";
                advertismen = null;
                return false;
            }
            ResultInfo<Advertismen> result = GetResult<Advertismen>(response);
            msg = result.Msg;
            advertismen = result.Data;
            return result.Result == 1 && result.Success;
        }

        public static bool GetAdvertismens(string ip, string pass, out List<Advertismen> advertismens, out string msg)
        {
            advertismens = new List<Advertismen>();
            var client = new RestClient($"http://{ip}:{Port}/advertisment/show");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", $"pass={pass}&index=-1", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                msg = "无法连接设备!";
                return false;
            }
            ResultInfo<List<Advertismen>> result = GetResult<List<Advertismen>>(response);
            msg = result.Msg;
            if (result.Data != null)
                advertismens.AddRange(result.Data);
            return result.Result == 1 && result.Success;
        }
        #endregion

        #region 设置广告轮播图
        public static bool SetAdvertismens(string ip, string pass, int index, Image image, out string msg)
        {
            var client = new RestClient($"http://{ip}:{Port}/advertisment/set");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", $"pass={pass}&index={index}&imgBase64={ImageHelper.ImageToBase64String(image)}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                msg = "无法连接设备!";
                return false;
            }
            ResultInfo<string> result = GetResult<string>(response);
            msg = result.Msg;
            return result.Result == 1 && result.Success;
        }

        #endregion

        #region 清空广告轮播图
        public static bool DeleteAdvertismens(string ip, string pass, out string msg, int index = -1)
        {
            var client = new RestClient($"http://{ip}:{Port}/advertisment/delete");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", $"pass={pass}&&index={index}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                msg = "无法连接设备!";
                return false;
            }
            ResultInfo<string> result = GetResult<string>(response);
            msg = result.Msg;
            return result.Result == 1 && result.Success;
        }
        #endregion


        #endregion

        #region 人员接口

        #region 查询人员信息
        public static bool FindPersons(string ip, string pass, out List<Person> persons, string personId = "-1")
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
            ResultInfo<List<Person>> result = GetResult<List<Person>>(response);
            persons = new List<Person>();
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
        public static bool CreatePerson(string ip, string password, string id, string idCardNum, string name, out string msg)
        {
            var client = new RestClient($@"http://{ip}:{Port}/person/create");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            RequestPerson requestPerson = new RequestPerson() { Pass = password, Person = new Person() { Id = id, IdcardNum = idCardNum, Name = name } };
            request.AddParameter("application/x-www-form-urlencoded", requestPerson.Serialize(), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                msg = "无法连接设备!";
                return false;
            }
            ResultInfo<Person> result = GetResult<Person>(response);
            msg = result.Msg;
            return result.Result == 1 && result.Success;
        }
        #endregion

        #region 更新人员信息
        public static bool UpdatePerson(string ip, string password, string id, string idCardNum, string name, out string msg)
        {
            var client = new RestClient($@"http://{ip}:{Port}/person/update");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            RequestPerson requestPerson = new RequestPerson() { Pass = password, Person = new Person() { Id = id, IdcardNum = idCardNum, Name = name } };
            request.AddParameter("application/x-www-form-urlencoded", requestPerson.Serialize(), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                msg = "无法连接设备!";
                return false;
            }
            ResultInfo<Person> result = GetResult<Person>(response);
            msg = result.Msg;
            return result.Result == 1 && result.Success;
        }
        #endregion

        #region 删除人员信息
        public static bool DeletePerson(string ip, string password, string id, out string msg)
        {
            var client = new RestClient($@"http://{ip}:{Port}/person/delete");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", $"pass={password}&id={id}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                msg = "无法连接设备!";
                return false;
            }
            ResultInfo<ResponseDeletePerson> result = GetResult<ResponseDeletePerson>(response);
            msg = result.Msg;
            return result.Result == 1 && result.Success;
        }
        #endregion

        #region 添加通行时间
        public static bool CreatePasstime(string ip, string password, string personId, string passtime, out string msg)
        {
            var client = new RestClient($@"http://{ip}:{Port}/person/createPasstime");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            RequestPasstime reqPasstime = new RequestPasstime()
            {
                Pass = password,
                Passtime = new PassTime()
                {
                    personId = personId,
                    passtime = passtime
                }
            };
            request.AddParameter("application/x-www-form-urlencoded", reqPasstime.Serialize(), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                msg = "无法连接设备!";
                return false;
            }
            ResultInfo<string> result = GetResult<string>(response);
            msg = result.Msg;
            return result.Result == 1 && result.Success;
        }
        #endregion

        #region 删除通行时间
        public static bool DeletePasstime(string ip, string password, string personId, out string msg)
        {
            var client = new RestClient($@"http://{ip}:{Port}/person/deletePasstime");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", $"pass={password}&personId={personId}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                msg = "无法连接设备!";
                return false;
            }
            ResultInfo<string> result = GetResult<string>(response);
            msg = result.Msg;
            return result.Result == 1 && result.Success;
        }
        #endregion

        #region 添加有效期
        public static bool CreatePermissions(string ip, string password, string personId, string time, out string msg)
        {
            var client = new RestClient($@"http://{ip}:{Port}/person/permissionsCreate");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", $"pass={password}&personId={personId}&time={time}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                msg = "无法连接设备!";
                return false;
            }
            ResultInfo<string> result = GetResult<string>(response);
            msg = result.Msg;
            return result.Result == 1 && result.Success;
        }
        #endregion

        #region 删除有效期
        public static bool DeletePermissions(string ip, string password, string personId, out string msg)
        {
            var client = new RestClient($@"http://{ip}:{Port}/person/permissionsDelete");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", $"pass={password}&personId={personId}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                msg = "无法连接设备!";
                return false;
            }
            ResultInfo<string> result = GetResult<string>(response);
            msg = result.Msg;
            return result.Result == 1 && result.Success;
        }
        #endregion

        #region 查询记录
        public static List<Record> FindRecords(string ip, string password, string personId, int length, int index, string startTime, string endTime, out string msg)
        {
            var client = new RestClient($@"http://{ip}:{Port}/findRecords ");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            RequestFindRecords findRecords = new RequestFindRecords()
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
                msg = "无法连接设备!";
                return null;
            }
            ResultInfo<ResponseRecords> result = GetResult<ResponseRecords>(response);
            msg = result.Msg;
            List<Record> records = new List<Record>();
            if (result.Result == 1 && result.Success)
                records.AddRange(result.Data.records);
            return records;
        }
        #endregion

        #region 删除记录(时间字符串)
        public static bool DeleteRecords(string ip, string password, string personId, DateTime time, out string msg)
        {
            var client = new RestClient($@"http://{ip}:{Port}/deleteRecords");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", $"pass={password}&time={time.ToString("yyyy-MM-dd HH:mm:ss")}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                msg = "无法连接设备!";
                return false;
            }
            ResultInfo<string> result = GetResult<string>(response);
            msg = result.Msg;
            return result.Result == 1 && result.Success;
        }
        #endregion

        #region 删除记录(时间戳)
        public static bool DeleteRecordsByUnixTime(string ip, string password, string personId, DateTime time, out string msg)
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
                msg = "无法连接设备!";
                return false;
            }
            ResultInfo<string> result = GetResult<string>(response);
            msg = result.Msg;
            return result.Result == 1 && result.Success;
        }
        #endregion


        #endregion

        #region 人脸接口


        #region  添加人脸
        public static bool CreateFace(string iP, string pass, string personId, string faceId, Image face, out string msg)
        {
            var client = new RestClient($@"http://{iP}:{Port}/face/create");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            RequestFace faceAdd = new RequestFace() { Pass = pass, PersonId = personId, FaceId = faceId, FaceImage = face };
            request.AddParameter("application/x-www-form-urlencoded", faceAdd.Serialize(), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                msg = "无法连接设备!";
                return false;
            }
            ResultInfo<string> result = GetResult<string>(response);
            msg = result.Msg;
            return result.Result == 1 && result.Success;
        }
        #endregion

        #region  更新人脸
        public static bool UpdateFace(string iP, string pass, string personId, string faceId, Image face, out string msg)
        {
            var client = new RestClient($@"http://{iP}:{Port}/face/update");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            RequestFace faceUpdate = new RequestFace() { Pass = pass, PersonId = personId, FaceId = faceId, FaceImage = face };
            request.AddParameter("application/x-www-form-urlencoded", faceUpdate.Serialize(), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                msg = "无法连接设备!";
                return false;
            }
            ResultInfo<string> result = GetResult<string>(response);
            msg = result.Msg;
            return result.Result == 1 && result.Success;
        }
        #endregion

        #region  删除单张人脸
        public static bool DeleteFace(string iP, string pass, string personId, string faceId, out string msg)
        {
            var client = new RestClient($@"http://{iP}:{Port}/face/delete");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", $"pass={pass}&personId={personId}&faceId={faceId}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                msg = "无法连接设备!";
                return false;
            }
            ResultInfo<string> result = GetResult<string>(response);
            msg = result.Msg;
            return result.Result == 1 && result.Success;
        }
        #endregion

        #region  删除人员所有人脸
        public static bool ClearFaces(string iP, string pass, string personId, out string msg)
        {
            var client = new RestClient($@"http://{iP}:{Port}/face/deletePerson");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", $"pass={pass}&personId={personId}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                msg = "无法连接设备!";
                return false;
            }
            ResultInfo<string> result = GetResult<string>(response);
            msg = result.Msg;
            return result.Result == 1;
        }
        #endregion

        #region 查询人脸
        public static List<FaceData> FindFaces(string iP, string pass, string personId, out string msg)
        {
            var client = new RestClient($@"http://{iP}:{Port}/face/find");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", $"pass={pass}&personId={personId}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                msg = "无法连接设备!";
                return null;
            }
            ResultInfo<List<FaceData>> result = GetResult<List<FaceData>>(response);
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

        #region 检查人脸是否符合要求
        public static bool CheckFace(string iP, string pass, Image face, out string msg)
        {
            if (!Ping(iP))
            {
                msg = "设备离线!";
                return false;
            }
            var client = new RestClient($@"http://{iP}:{Port}/face/check");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", $"pass={pass}&imgBase64={ImageHelper.ImageToBase64String(face)}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                msg = "无法连接设备!";
                return false;
            }
            ResultInfo<string> result = GetResult<string>(response);
            msg = result.Msg;
            return result.Result == 1 && result.Success;
        }
        #endregion

        #endregion


        #region 数据解析

        private static ResultInfo<T> GetResult<T>(IRestResponse response) where T : class
        {

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                ResultInfo<T> result = new ResultInfo<T>();
                result.Success = false;
                result.Data = null;
                result.Msg = "无法连接服务器";
                result.Result = 0;
                return result;
            }
            if (response.ErrorException != null)
            {
                ResultInfo<T> result = new ResultInfo<T>();
                result.Success = false;
                result.Data = null;
                result.Msg = response.ErrorException.Message;
                result.Result = 0;
                return result;
            }
            return Deserialize<T>(response);
        }

        private static ResultInfo<T> Deserialize<T>(IRestResponse response) where T : class
        {


            MemoryStream memoryStream = null;
            try
            {
                memoryStream = new MemoryStream(response.RawBytes);

                using (StreamReader sr = new StreamReader(memoryStream, Encoding.UTF8))
                {
                    return JsonConvert.DeserializeObject(sr.ReadToEnd(), typeof(ResultInfo<T>), new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }) as ResultInfo<T>;
                }
            }
            catch (Exception ex)
            {
                ResultInfo<T> result = new ResultInfo<T>();
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

        #region 综合接口

        #region  添加人脸
        public static bool CreateEmp(string ip, string pass, string personId, string cardNo, string personName, string timeGroup, string endTime, byte[] face1, byte[] face2, byte[] face3, out string msg)
        {
            var client = new RestClient($@"http://{ip}:{Port}/person/createface");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            RequestCreateEmp create = new RequestCreateEmp()
            {
                pass = pass,
                person = new Person() { Id = personId, Name = personName, IdcardNum = cardNo },
                passtime = new PassTime() { personId = personId, passtime = timeGroup },
                time = endTime,
                imgBase64First = face1 == null ? "" : Convert.ToBase64String(face1, Base64FormattingOptions.InsertLineBreaks).Replace("+", "%2B"),
                imgBase64Second = face2 == null ? "" : Convert.ToBase64String(face2, Base64FormattingOptions.InsertLineBreaks).Replace("+", "%2B"),
                imgBase64Third = face3 == null ? "" : Convert.ToBase64String(face3, Base64FormattingOptions.InsertLineBreaks).Replace("+", "%2B")
            };
            request.AddParameter("application/x-www-form-urlencoded", create.Serialize(), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                msg = "无法连接设备!";
                return false;
            }
            ResultInfo<string> result = GetResult<string>(response);
            msg = result.Msg;
            return result.Result == 1 && result.Success;
            /*
            List<Person> persons;
            if (FindPersons(ip, pass, out persons, personId))
            {
                Image image = ImageHelper.BytesToImage(face1);
                if (persons.Count == 0)
                {
                    if (image == null)
                        return CreatePerson(ip, pass, personId, personId, personName, out msg);
                    else
                        return CreatePerson(ip, pass, personId, personId, personName, out msg) && CreateFace(ip, pass, personId, personId, image, out msg);
                }
                else
                {
                    if (image == null)
                        return UpdatePerson(ip, pass, personId, personId, personName, out msg) && DeleteFace(ip, pass, personId, personId, out msg);
                    else
                        return UpdatePerson(ip, pass, personId, personId, personName, out msg) && DeleteFace(ip, pass, personId, personId, out msg)
                            && CreateFace(ip, pass, personId, personId, image, out msg);
                }
            }
            msg = $"创建人员失败:无法查找人员!";
            return false;
            DeletePerson(ip, pass, personId, out msg);
            if (!CreatePerson(ip, pass, personId, cardNo, personName, out msg)) return false;

            Image img1 = ImageHelper.BytesToImage(face1);
            List<FaceData> faces = new List<FaceData>();
            List<FaceData> fs = FindFaces(ip, pass, personId, out msg);
            if (fs != null)
                faces.AddRange(fs);
            if (faces == null) return false;
            if (img1 == null)
            {
                if (faces.Exists(p => p.faceId.Equals($"{personId}1")))
                    DeleteFace(ip, pass, personId, $"{personId}1", out msg);
            }
            else
            {
                if (!faces.Exists(p => p.faceId.Equals($"{personId}1")))
                {
                    if (!CreateFace(ip, pass, personId, $"{personId}1", img1, out msg)) return false;
                }
                else
                {
                    if (!UpdateFace(ip, pass, personId, $"{personId}1", img1, out msg)) return false;
                }
            }
            Image img2 = ImageHelper.BytesToImage(face2);
            if (img2 == null)
            {
                if (faces.Exists(p => p.faceId.Equals($"{personId}2")))
                    DeleteFace(ip, pass, personId, $"{personId}2", out msg);
            }
            else
            {
                if (!faces.Exists(p => p.faceId.Equals($"{personId}2")))
                {
                    if (!CreateFace(ip, pass, personId, $"{personId}2", img2, out msg)) return false;
                }
                else
                {
                    if (!UpdateFace(ip, pass, personId, $"{personId}2", img2, out msg)) return false;
                }
            }
            Image img3 = ImageHelper.BytesToImage(face3);
            if (img3 == null)
            {
                if (faces.Exists(p => p.faceId.Equals($"{personId}3")))
                    DeleteFace(ip, pass, personId, $"{personId}3", out msg);
            }
            else
            {
                if (!faces.Exists(p => p.faceId.Equals($"{personId}3")))
                {
                    if (!CreateFace(ip, pass, personId, $"{personId}3", img3, out msg)) return false;
                }
                else
                {
                    if (!UpdateFace(ip, pass, personId, $"{personId}3", img3, out msg)) return false;
                }
            }
            return true;
            */
        }
        #endregion

        #region 删除人员信息
        public static bool DeleteEmp(string ipAddress, string pass, string empCode, out string message)
        {
            return DeletePerson(ipAddress, pass, empCode, out message);
        }
        #endregion

        #endregion

        #region 采集记录
        public static bool CollectRecords(string ip, string pass, long index, int length, out List<Record> records, out string msg)
        {
            records = new List<Record>();
            var client = new RestClient($"http://{ip}:{Port}/query/record");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", $"pass={pass}&personId=-1&index={(index == 0 ? 0 : index + 1)}&length=100", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                msg = "无法连接设备!";
                return false;
            }
            ResultInfo<ResponseRecords> result = GetResult<ResponseRecords>(response);
            msg = result.Msg;
            if (result.Result == 1 && result.Success)
            {
                records.AddRange(result.Data.records);
            }
            msg = "success!";
            return true;
        }
        #endregion

        #region Ping
        public static bool Ping(string host)
        {
            Ping ping = new Ping();
            PingReply reply = ping.Send(host, 200);
            return reply.Status == IPStatus.Success;
        }
        #endregion
    }
}
