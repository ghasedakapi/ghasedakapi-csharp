using GhasedakApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using static GhasedakApi.Models.Results;

namespace GhasedakApi
{
    public class SMS : ISendService
    {
        private readonly string _apikey;
        private readonly string _baseUrl = "http://ghasedakapi.com/";
        private static readonly JavaScriptSerializer _JavaScriptSerializer = new JavaScriptSerializer();
        public SMS(string apikey)
        {
            _apikey = apikey;
        }

        public ApiResult Send(string message, string linenumber, string receptor, string senddate)
        {
            var url = string.Format("{0}{1}",_baseUrl, "api/v1/sms/send/simple");
            var param = new Dictionary<string, string>
        {
            {"linenumber", linenumber},
            {"receptor", string.Join(",", receptor.ToArray())},
            {"message", System.Web.HttpUtility.UrlEncodeUnicode(message)},
            {"date", senddate}
        };
            var response = Client.ApiClient.Execute(url, param);
            return _JavaScriptSerializer.Deserialize<ApiResult>(response);
        }
    }
}