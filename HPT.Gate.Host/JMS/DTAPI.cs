#region << 版 本 注 释 >>

/*----------------------------------------------------------------

* 项目名称 ：HPT.Gate.Host.JMS

* 项目描述 ：

* 类 名 称 ：DTAPI

* 类 描 述 ：

* 所在的域 ：7OANK7GDNCIPE0X

* 命名空间 ：HPT.Gate.Host.JMS

* 机器名称 ：7OANK7GDNCIPE0X 

* CLR 版本 ：4.0.30319.42000

* 作    者 ：Administrator

* 创建时间 ：2019-07-08 09:34:18

* 更新时间 ：2019-07-08 09:34:18

* 版 本 号 ：v1.0.0.0

*******************************************************************

* Copyright @ Administrator 2019. All rights reserved.

*******************************************************************

//----------------------------------------------------------------
*/

#endregion

using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Net;

namespace HPT.Gate.Host.JMS
{
    /// <summary>
    /// 功能描述    ：DTAPI  
    /// 创 建 者    ：Administrator
    /// 创建日期    ：2019-07-08 09:34:18 
    /// 最后修改者  ：Administrator
    /// 最后修改日期：2019-07-08 09:34:18 
    /// </summary>
    public class DTAPI
    {
        public static bool HandleAddCard(string url, List<int> ids, string schoolNo, out string msg)
        {
            var client = new RestClient($"{url}/cardapi/admin/door/handleAddCardAuthResult");
            var request = new RestRequest(Method.POST);
            client.Timeout = 5000;
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            var cardRequest = new CardRequest() { ids = ids, schoolNo = schoolNo };
            request.AddParameter("application/json", JsonConvert.SerializeObject(cardRequest), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                msg = $"远程服务器响应:{response.StatusCode}";
                return false;
            }
            DTResponse result = JsonConvert.DeserializeObject<DTResponse>(response.Content);
            if (result == null)
            {
                msg = "远程服务器返回空!";
                return false;
            }
            msg = result.message;
            return result.code == 0;
        }

        public static bool HandleDeleteCard(string url, List<int> ids, string schoolNo, out string msg)
        {
            var client = new RestClient($"{url}/cardapi/admin/door/handleDeleteCardAuthResult");
            var request = new RestRequest(Method.POST);
            client.Timeout = 5000;
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            var cardRequest = new CardRequest() { ids = ids, schoolNo = schoolNo };
            request.AddParameter("application/json", JsonConvert.SerializeObject(cardRequest), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                msg = $"远程服务器响应:{response.StatusCode}";
                return false;
            }
            DTResponse result = JsonConvert.DeserializeObject<DTResponse>(response.Content);
            if (result == null)
            {
                msg = "远程服务器返回空!";
                return false;
            }
            msg = result.message;
            return result.code == 0;
        }


        public static bool HandleDeleteTimegroup(string url, int id, int type, bool success, out string msg)
        {
            var client = new RestClient($"{url}/cardapi/admin/door/handleDeleteTimeRuleResult");
            var request = new RestRequest(Method.POST);
            client.Timeout = 5000;
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            var timegroupRequest = new TimegroupRequest() { id = id, type = type, flag = success ? 1 : 0 };
            request.AddParameter("application/json", JsonConvert.SerializeObject(timegroupRequest), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                msg = $"远程服务器响应:{response.StatusCode}";
                return false;
            }
            DTResponse result = JsonConvert.DeserializeObject<DTResponse>(response.Content);
            if (result == null)
            {
                msg = "远程服务器返回空!";
                return false;
            }
            msg = result.message;
            return result.code == 0;
        }

        public static bool HandleAddTimegroup(string url, int id, int type, bool success, out string msg)
        {
            var client = new RestClient($"{url}/cardapi/admin/door/handleAddTimeRuleResult");
            var request = new RestRequest(Method.POST);
            client.Timeout = 5000;
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            var timegroupRequest = new TimegroupRequest() { id = id, type = type, flag = success ? 1 : 0 };
            request.AddParameter("application/json", JsonConvert.SerializeObject(timegroupRequest), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                msg = $"远程服务器响应:{response.StatusCode}";
                return false;
            }
            DTResponse result = JsonConvert.DeserializeObject<DTResponse>(response.Content);
            if (result == null)
            {
                msg = "远程服务器返回空!";
                return false;
            }
            msg = result.message;
            return result.code == 0;
        }


        public static bool HandleVacation(string url, string cardNo, out string msg)
        {
            var client = new RestClient($"{url}/cardapi/admin/door/handleVacationPassResule");
            var request = new RestRequest(Method.POST);
            client.Timeout = 5000;
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            var vacationRequest = new VacationRequest() { cardNo = cardNo };
            request.AddParameter("application/json", JsonConvert.SerializeObject(vacationRequest), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                msg = $"远程服务器响应:{response.StatusCode}";
                return false;
            }
            DTResponse result = JsonConvert.DeserializeObject<DTResponse>(response.Content);
            if (result == null)
            {
                msg = "远程服务器返回空!";
                return false;
            }
            msg = result.message;
            return result.code == 0;
        }

        public static bool UploadDoorRecord(string url, string termSN, string cardNo, string recDatetime, int iOFlag, string capture, out string msg)
        {
            var client = new RestClient($"{url}/doorapi/hardware/door/addRecord");
            var request = new RestRequest(Method.POST);
            client.Timeout = 5000;
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            var recordRequest = new RecordRequest() { termSn = termSN, cardNo = cardNo, cardTime = recDatetime, capture = capture, type = iOFlag };
            request.AddParameter("application/json", JsonConvert.SerializeObject(recordRequest), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                msg = $"远程服务器响应:{response.StatusCode}";
                return false;
            }
            DTResponse result = JsonConvert.DeserializeObject<DTResponse>(response.Content);
            if (result == null)
            {
                msg = "远程服务器返回空!";
                return true;
            }
            msg = result.message;
            return true;
        }

        public static bool UploadAttendanceRecord(string url, string termSN, string cardNo, string recDatetime, out string msg)
        {
            var client = new RestClient($"{url}/oaapi/admin/attendance/record/report");
            var request = new RestRequest(Method.POST);
            client.Timeout = 5000;
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            var recordRequest = new RecordRequest() { termSn = termSN, cardNo = cardNo, cardTime = recDatetime };
            request.AddParameter("application/json", JsonConvert.SerializeObject(recordRequest), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                msg = $"远程服务器响应:{response.StatusCode}";
                return false;
            }
            DTResponse result = JsonConvert.DeserializeObject<DTResponse>(response.Content);
            if (result == null)
            {
                msg = "远程服务器返回空!";
                return true;
            }
            msg = result.message;
            return true;
        }

        public static bool UploadSchoolCardAttendanceRecord(string url, string termSN, string cardNo, string recDatetime, out string msg)
        {
            var client = new RestClient($"{url}/schoolapi/admin/shlCar/carRecordReport");
            var request = new RestRequest(Method.POST);
            client.Timeout = 5000;
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            var recordRequest = new RecordRequest() { termSn = termSN, cardNo = cardNo, cardTime = recDatetime };
            request.AddParameter("application/json", JsonConvert.SerializeObject(recordRequest), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                msg = $"远程服务器响应:{response.StatusCode}";
                return false;
            }
            DTResponse result = JsonConvert.DeserializeObject<DTResponse>(response.Content);
            if (result == null)
            {
                msg = "远程服务器返回空!";
                return true;
            }
            msg = result.message;
            return true;
        }


        public static bool UploadDoorDeviceStatus(string url, string termSn, int type, out string msg)
        {
            var client = new RestClient($"{url}/cardapi/admin/door/monitor");
            var request = new RestRequest(Method.POST);
            client.Timeout = 5000;
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            var heartbeatRequest = new DoorHeartbeatRequest() { termSn = termSn, type = type };
            request.AddParameter("application/json", JsonConvert.SerializeObject(heartbeatRequest), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                msg = $"远程服务器响应:{response.StatusCode}";
                return false;
            }
            DTResponse result = JsonConvert.DeserializeObject<DTResponse>(response.Content);
            if (result == null)
            {
                msg = "远程服务器返回空!";
                return true;
            }
            msg = result.message;
            return true;
        }

        public static bool UploadAttendanceDeviceStatus(string url, string termSn, out string msg)
        {
            var client = new RestClient($"{url}/oaapi/admin/attendance/monitor");
            var request = new RestRequest(Method.POST);
            client.Timeout = 5000;
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            var heartbeatRequest = new AttendanceHeartbeatRequest() { termCode = termSn };
            request.AddParameter("application/json", JsonConvert.SerializeObject(heartbeatRequest), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                msg = $"远程服务器响应:{response.StatusCode}";
                return false;
            }
            DTResponse result = JsonConvert.DeserializeObject<DTResponse>(response.Content);
            if (result == null)
            {
                msg = "远程服务器返回空!";
                return true;
            }
            msg = result.message;
            return true;
        }

    }
}
