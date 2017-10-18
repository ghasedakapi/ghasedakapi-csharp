using GhasedakApi.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace GhasedakApi.Client
{
    public static class ApiClient
    {
        public static string Execute(string url, Dictionary<string, string> parameters, string method = "POST", string contentType = "application/x-www-form-urlencoded")
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = method;
            request.ContentType = contentType;
            byte[] data = new byte[0];
            string responseResult = "";
            HttpWebResponse _HttpWebResponse;
            string postdata = "";
            if (parameters != null)
            {
                postdata = parameters.Keys.Aggregate(postdata,
                    (current, key) => current + string.Format("{0}={1}&", key, parameters[key]));
                data = Encoding.UTF8.GetBytes(postdata);
            }
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
            }
            catch (ConnectionException ex)
            {
                throw new ConnectionException(
                     "Connection Error: " + request.Method,
                    ex
                );
            }
        }
    }
}