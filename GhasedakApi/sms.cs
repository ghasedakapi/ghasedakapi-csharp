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
    public class SMS : ISendService
    {
        private readonly string _apikey;
        private static readonly JavaScriptSerializer _JavaScriptSerializer = new JavaScriptSerializer();
        public SMS(string apikey)
        {
            _apikey = apikey;
        }
        public ApiResult Send(string message, string linenumber, string receptor, string senddate)
        {
            var url = "api/v1/sms/send/simple";
            var param = new Dictionary<string, object>
        {
            {"apikey", _apikey},
            {"linenumber", linenumber},
            {"receptor",receptor },
            {"message", System.Web.HttpUtility.UrlEncodeUnicode(message)},
            {"date", senddate}
        };
            var response = Client.ApiClient.Execute(url, param);
            return _JavaScriptSerializer.Deserialize<ApiResult>(response);
        }
        public ApiResult Send(string message, string linenumber, string[] receptor, string senddate)
        {
            var url = "api/v1/sms/send/bulk2";
            var param = new Dictionary<string, object>
        {
            {"apikey", _apikey},
            {"linenumber", linenumber},
            {"receptor",string.Join(",",receptor) },
            {"message", System.Web.HttpUtility.UrlEncodeUnicode(message)},
            {"date", senddate}
        };
            var response = Client.ApiClient.Execute(url, param);
            return _JavaScriptSerializer.Deserialize<ApiResult>(response);
        }
        public ApiResult Send(string[] message, string[] linenumber, string[] receptor, string[] senddate)
        {
            var url = "api/v1/sms/send/bulk";
            var builder = new System.Text.StringBuilder();
            foreach (var item in message)
            {
                builder.Append(System.Web.HttpUtility.UrlEncodeUnicode(item)).Append(",");
            }
            var param = new Dictionary<string, object>
        {
            {"apikey", _apikey},
            {"linenumber", string.Join(",",linenumber)},
            {"receptor",string.Join(",",receptor) },
            {"message", builder},
            {"date", senddate}
        };
            var response = Client.ApiClient.Execute(url, param);
            return _JavaScriptSerializer.Deserialize<ApiResult>(response);
        }
        public ApiResult Verify(int type, string template, string[] receptor, string param1, string param2, string param3)
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
            var response = Client.ApiClient.Execute(url, param);
            return _JavaScriptSerializer.Deserialize<ApiResult>(response);
        }
    }
}
