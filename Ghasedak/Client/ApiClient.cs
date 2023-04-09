using Ghasedak.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using static Ghasedak.Models.Results;

namespace Ghasedak.Client
{
    public static class ApiClient
    {
        private readonly static string _baseUrl = "https://api.ghasedak.me/";
        private static readonly JavaScriptSerializer _JavaScriptSerializer = new JavaScriptSerializer();
        public static string Execute(string url, string apikey, Dictionary<string, object> parameters, string method = "POST", string contentType = "application/x-www-form-urlencoded")
        {
            var request = (HttpWebRequest)WebRequest.Create(string.Format("{0}{1}",_baseUrl,url));
            request.Method = method;
            request.ContentType = contentType;
            byte[] data = new byte[0];
            string responseResult = "";
            HttpWebResponse _HttpWebResponse;
            string postdata = "";
            if (parameters != null)
            {
                if(parameters.Where(x => x.Key == "receptor").Any())
                parameters["receptor"] = string.Join(",", parameters.Where(x => x.Key == "receptor").Select(x => x.Value.ToString().Replace("+", "00")));

                postdata = parameters.Keys.Aggregate(postdata,
                    (current, key) => current + string.Format("{0}={1}&", key, parameters[key]));
                data = Encoding.UTF8.GetBytes(postdata);
            }
            request.Headers.Add("apikey", apikey);
            using (Stream webpageStream = request.GetRequestStream())
            {
                webpageStream.Write(data, 0, data.Length);
            }
            try
            {
                using (_HttpWebResponse = (HttpWebResponse)request.GetResponse())
                {
                    using (var reader = new StreamReader(_HttpWebResponse.GetResponseStream()))
                    {
                        responseResult = reader.ReadToEnd();
                    }
                }
                return responseResult;
            }
            catch (WebException Apiex)
            {
                _HttpWebResponse = (HttpWebResponse)Apiex.Response;
                using (var reader = new StreamReader(_HttpWebResponse.GetResponseStream()))
                {
                    responseResult = reader.ReadToEnd();
                }
                try
                {
                    var res = _JavaScriptSerializer.Deserialize<ApiResult>(responseResult);
                    throw new ApiException(res.Result.Code,(int)_HttpWebResponse.StatusCode,res.Result.Message);
                }
                catch (ConnectionException ex)
                {
                    throw new ConnectionException(ex.Message,ex);
                }
            }
        }
    }
}