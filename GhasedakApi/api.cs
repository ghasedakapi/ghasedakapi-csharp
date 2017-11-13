using GhasedakApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using static GhasedakApi.Models.Results;
using GhasedakApi.Models;

namespace GhasedakApi
{
    public class Api : ISMSService, IAccountService, IVoiceService, IReceiveService
    {
        private readonly string _apikey;
        private static readonly JavaScriptSerializer _JavaScriptSerializer = new JavaScriptSerializer();
        public Api(string apikey)
        {
            _apikey = apikey;
        }

        #region sms
        public SendResult SendSMS(string message, string linenumber, string receptor)
        {
            var url = "api/v1/sms/send/simple";
            var param = new Dictionary<string, object>
            {
                {"apikey", _apikey},
                {"linenumber", linenumber},
                {"receptor",receptor },
                {"message", System.Web.HttpUtility.UrlEncodeUnicode(message)
            }
        };
            return MakeSendRequest(url, param);
        }
        public SendResult SendSMS(string message, string linenumber, string[] receptor)
        {
            var url = "api/v1/sms/send/bulk2";
            var param = new Dictionary<string, object>
             {
               {"apikey", _apikey},
               {"linenumber", linenumber},
               {"receptor",string.Join(",",receptor) },
               {"message", System.Web.HttpUtility.UrlEncodeUnicode(message)},
        };
            return MakeSendRequest(url, param);
        }
        public SendResult SendSMS(string[] message, string[] linenumber, string[] receptor)
        {
            var url = "api/v1/sms/send/bulk";
            var msg = new System.Text.StringBuilder();
            foreach (var item in message)
            {
                msg.Append(System.Web.HttpUtility.UrlEncodeUnicode(item)).Append(",");
            }
            var param = new Dictionary<string, object>
               {
                  {"apikey", _apikey},
                  {"linenumber", string.Join(",",linenumber)},
                  {"receptor",string.Join(",",receptor) },
                  {"message", msg},
                };
            return MakeSendRequest(url, param);
        }
        public SendResult SendSMS(string message, string linenumber, string receptor, DateTime senddate)
        {
            var url = "api/v1/sms/send/simple";
            var param = new Dictionary<string, object>
              {
                {"apikey", _apikey},
                {"linenumber", linenumber},
                {"receptor",receptor },
                {"message", System.Web.HttpUtility.UrlEncodeUnicode(message) },
                {"senddate",Utilities.Date_Time.DatetimeToUnixTimeStamp(senddate) }
        };
            return MakeSendRequest(url, param);
        }
        public SendResult SendSMS(string message, string linenumber, string[] receptor, DateTime senddate)
        {
            var url = "api/v1/sms/send/bulk2";
            var param = new Dictionary<string, object>
             {
               {"apikey", _apikey},
               {"linenumber", linenumber},
               {"receptor",string.Join(",",receptor) },
               {"message", System.Web.HttpUtility.UrlEncodeUnicode(message)},
               {"senddate",Utilities.Date_Time.DatetimeToUnixTimeStamp(senddate)
             }
        };
            return MakeSendRequest(url, param);
        }
        public SendResult SendSMS(string[] message, string[] linenumber, string[] receptor, DateTime[] senddate)
        {
            var url = "api/v1/sms/send/bulk";
            var msg = new System.Text.StringBuilder();
            var date = new System.Text.StringBuilder();
            for (int i = 0; i < message.Length; i++)
            {
                msg.Append(System.Web.HttpUtility.UrlEncodeUnicode(message[i])).Append(",");
                date.Append(Utilities.Date_Time.DatetimeToUnixTimeStamp(senddate[i])).Append(",");
            }
            var param = new Dictionary<string, object>
               {
                  {"apikey", _apikey},
                  {"linenumber", string.Join(",",linenumber)},
                  {"receptor",string.Join(",",receptor) },
                  {"message", msg},
                  {"senddate", date}
                };
            return MakeSendRequest(url, param);
        }
        public SendResult Verify(int type, string template, string[] receptor, string param1, string param2, string param3)
        {
            var url = "api/v1/sms/send/verify";
            var param = new Dictionary<string, object>
        {
            {"apikey", _apikey},
            {"type", type},
            {"receptor",string.Join(",",receptor) },
            {"param1", param1},
            {"param2", param2},
            {"param3", param3},
            {"template", template}
        };
            return MakeSendRequest(url, param);
        }
        public StatusResult GetStatus(string[] messageId)
        {
            var url = "api/v1/sms/status";
            var param = new Dictionary<string, object>
        {
            {"apikey", _apikey},
            {"messageid", string.Join(",",messageId)},
        };
            var response = Client.ApiClient.Execute(url, param);
            return _JavaScriptSerializer.Deserialize<StatusResult>(response);
        }
        #endregion
        public ApiResult AccountInfo()
        {
            var url = "api/v1/account/info";
            var param = new Dictionary<string, object>
        {
            {"apikey", _apikey}
        };
            var response = Client.ApiClient.Execute(url, param);
            return _JavaScriptSerializer.Deserialize<ApiResult>(response);
        }

        public ApiResult SendVoice(string message, string[] receptor, string senddate)
        {
            var url = "api/v1/voice/send";
            var param = new Dictionary<string, object>
                {
                 {"apikey", _apikey},
                 {"message", message},
                 {"senddate", senddate},
                };
            var response = Client.ApiClient.Execute(url, param);
            return _JavaScriptSerializer.Deserialize<ApiResult>(response);
        }

        private ApiResult MakeRequest(string url, Dictionary<string, object> param)
        {
            return _JavaScriptSerializer.Deserialize<ApiResult>(Client.ApiClient.Execute(url, param));
        }
        private SendResult MakeSendRequest(string url, Dictionary<string, object> param)
        {
            return _JavaScriptSerializer.Deserialize<SendResult>(Client.ApiClient.Execute(url, param));
        }
        public ReceiveMessageResult ReceiveList(string linenumber, int isRead)
        {
            var url = "api/v1/sms/receive";
            var param = new Dictionary<string, object>
                {
                 {"apikey", _apikey},
                 {"linenumber", linenumber},
                 {"isRead", isRead},
                };
            var response = Client.ApiClient.Execute(url, param);
            return _JavaScriptSerializer.Deserialize<ReceiveMessageResult>(response);
        }
    }
}
