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
        private readonly static string _baseUrl = "https://api.ghasedak.io/";


        private static readonly JavaScriptSerializer _JavaScriptSerializer = new JavaScriptSerializer();
        public static string Execute(string url, string apikey, Dictionary<string, object> parameters, string method = "POST", string contentType = "application/x-www-form-urlencoded")
        {
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

            HttpWebRequest request = null;

            if (! string.IsNullOrEmpty(method) &&  method.Trim().ToLower() == "get")
            {
                url += "?"+ postdata;
                if (url.Contains("dep"))
                    url += "agent=csharp";
                else
                    url += "agent=csharp";
                request = (HttpWebRequest)WebRequest.Create(string.Format("{0}{1}", _baseUrl, url));
                request.Method = method;
                request.ContentType = contentType;
                request.Headers.Add("apikey", apikey);
            }
            else
            {
                if (url.Contains("dep"))
                    url += "&agent=csharp";
                else
                    url += "?agent=csharp";

                request = (HttpWebRequest)WebRequest.Create(string.Format("{0}{1}", _baseUrl, url));
                request.Method = method;
                request.ContentType = contentType;
                request.Headers.Add("apikey", apikey);
                using (Stream webpageStream = request.GetRequestStream())
                {
                    webpageStream.Write(data, 0, data.Length);
                }
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



        private static String GetQueryStringData(Dictionary<string, object> parameters)
        {
            String queryString = String.Empty;
            if (parameters != null && parameters.Count > 0)
            {
                for (int i = 0; i < parameters.Count; i++)
                {
                    if (i == 0)
                        queryString += ("?" + parameters.Keys.ElementAt(i) + "=" + parameters.Values.ElementAt(i));
                    else
                        queryString += ("&" + parameters.Keys.ElementAt(i) + "=" + parameters.Values.ElementAt(i));
                }
            }
            return queryString;

        }
    }
}