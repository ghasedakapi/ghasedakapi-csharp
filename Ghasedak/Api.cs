using Ghasedak.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using static Ghasedak.Models.Results;
using Ghasedak.Models;

namespace Ghasedak
{
    public class Api : ISMSService, IAccountService, IVoiceService, IReceiveService, IContactService
    {
        private readonly string _apikey;
        private static readonly JavaScriptSerializer _JavaScriptSerializer = new JavaScriptSerializer();
        public Api(string apikey)
        {
            _apikey = apikey;
        }

        #region sms
        public SendResult SendSMS(string message,  string receptor,string linenumber = null, DateTime? senddate=null, String checkid=null)
        {
            var url = "v2/sms/send/simple";
            var param = new Dictionary<string, object>();
            param.Add("apikey", _apikey);
            param.Add("message", System.Web.HttpUtility.UrlEncodeUnicode(message));

             if(!string.IsNullOrEmpty(linenumber))
                param.Add("linenumber", linenumber);
            if (senddate.HasValue)
                param.Add("senddate", Utilities.Date_Time.DatetimeToUnixTimeStamp(Convert.ToDateTime(senddate)));
            if(!string.IsNullOrEmpty(checkid))
                param.Add("checkid", checkid);

            return MakeSendRequest(url, param);
        }

       

        public SendResult SendSMS(string[] message, string[] linenumber, string[] receptor, DateTime[] senddate =null, string[] checkid=null)
        {
            var url = "v2/sms/send/bulk";
            var msg = new System.Text.StringBuilder();
            var date = new System.Text.StringBuilder();
            var check = new System.Text.StringBuilder();
            var param = new Dictionary<string, object>();

            foreach (var item in message)
            {
                msg.Append(System.Web.HttpUtility.UrlEncodeUnicode(item)).Append(",");
            }
            param.Add("apikey", _apikey);
            param.Add("linenumber", linenumber);
            param.Add("message", msg);
            param.Add("receptor", string.Join(",", receptor));
            if (senddate.Length > 0)
            {
                foreach (var item in senddate)
                {
                    date.Append(Utilities.Date_Time.DatetimeToUnixTimeStamp(Convert.ToDateTime(item))).Append(",");
                }
                param.Add("senddate", date);
            }

            if (checkid.Length > 0)
                param.Add("checkid", string.Join(",", checkid));
            
            return MakeSendRequest(url, param);
        }

        public SendResult SendSMS(string message, string[] receptor, string linenumber=null, DateTime? senddate=null, string[] checkid=null)
        {
            var url = "v2/sms/send/pair";
            var param = new Dictionary<string, object>();

            param.Add("apikey", _apikey);
            param.Add("message", System.Web.HttpUtility.UrlEncodeUnicode(message));
            param.Add("receptor", string.Join(",", receptor));

            if (!string.IsNullOrEmpty(linenumber))
              param.Add("linenumber", linenumber);

            if (senddate.HasValue)
                param.Add("senddate", Utilities.Date_Time.DatetimeToUnixTimeStamp(Convert.ToDateTime(senddate)));
            if (checkid.Length > 0)
                param.Add("checkid", string.Join(",", checkid));
          
            return MakeSendRequest(url, param);
        }
        

        public SendResult Verify(int type, string template, string[] receptor, string param1, string param2=null, string param3=null, string param4=null, string param5=null, string param6=null, string param7=null, string param8=null, string param9=null, string param10=null)
        {
            var url = "v2/verification/send/simple";
            var param = new Dictionary<string, object>
        {
            {"apikey", _apikey},
            {"type", type},
            {"receptor",string.Join(",",receptor) },
            {"template", template},
            {"param1", param1},
            {"param2", param2},
            {"param3", param3},
            {"param4", param4},
            {"param5", param5},
            {"param6", param6},
            {"param7", param7},
            {"param8", param8},
            {"param9", param9},
            {"param10", param10},
        };
            return MakeSendRequest(url, param);
        }

        public StatusResult GetStatus(int type,long[] id )
        {
            var url = "v2/sms/status";
            var param = new Dictionary<string, object>
               {
                   {"apikey", _apikey},
                   {"type", type},
                   {"id", string.Join(",",id)},
               };
            return _JavaScriptSerializer.Deserialize<StatusResult>(Client.ApiClient.Execute(url, param));
        }
        public SendResult CancelSMS(long[] messageid)
        {
            var url = "v2/sms/cancel";
            var param = new Dictionary<string, object>
             {
                {"apikey", _apikey},
                {"messageid", string.Join(",",messageid)},
             };
            return MakeSendRequest(url, param);
        }

        #endregion

        #region Contact

        public GroupResult AddGroup(string name, int parent)
        {
            var url = "v2/contact/group/new";
            var param = new Dictionary<string, object>
             {
                {"apikey", _apikey},
                {"name", name},
                {"parent", parent},
             };
            return _JavaScriptSerializer.Deserialize<GroupResult>(Client.ApiClient.Execute(url, param));
        }

        public ApiResult RemoveGroup(int groupid)
        {
            var url = "v2/contact/group/remove";
            var param = new Dictionary<string, object>
             {
                {"apikey", _apikey},
                {"groupid", groupid},
             };
            return MakeRequest(url, param);
        }

        public ApiResult EditGroup(int groupid, string name)
        {
            var url = "v2/contact/group/edit";
            var param = new Dictionary<string, object>
             {
                {"apikey", _apikey},
                {"groupid", groupid},
                {"name", name},
             };
            return MakeRequest(url, param);
        }

        public ApiResult AddNumberToGroup(int groupid, string[] number, string[] firstname, string[] lastname, string[] email)
        {
            var url = "v2/contact/group/addnumber";
            var param = new Dictionary<string, object>
             {
                {"apikey", _apikey},
                {"groupid", groupid},
                {"number", string.Join(",",number)},
                {"firstname", string.Join(",",firstname)},
                {"lastname", string.Join(",",lastname)},
                {"email", string.Join(",",email)},
             };
            return MakeRequest(url, param);
        }

        public GroupListResult GroupList(int parent)
        {
            var url = "v2/contact/group/list";
            var param = new Dictionary<string, object>
             {
                {"apikey", _apikey},
                {"parent", parent},
             };
            return _JavaScriptSerializer.Deserialize<GroupListResult>(Client.ApiClient.Execute(url, param));
        }

        public GroupNumbersResult GroupNumbersList(int groupid, int page, int offset)
        {
            var url = "v2/contact/group/listnumber ";
            var param = new Dictionary<string, object>
             {
                {"apikey", _apikey},
                {"groupid", groupid},
                {"page", page},
                {"offset", offset},
             };
            return _JavaScriptSerializer.Deserialize<GroupNumbersResult>(Client.ApiClient.Execute(url, param));
        }

        #endregion

        public ApiResult AccountInfo()
        {
            var url = "v2/account/info";
            var param = new Dictionary<string, object>
        {
            {"apikey", _apikey}
        };
            var response = Client.ApiClient.Execute(url, param);
            return _JavaScriptSerializer.Deserialize<ApiResult>(response);
        }

        public SendResult SendVoice(string message, string[] receptor, DateTime? senddate)
        {
            var url = "v2/voice/send/simple";
            var param = new Dictionary<string, object>();

            param.Add("apikey", _apikey);
            param.Add("message", message);
            if (senddate.HasValue)
                param.Add("senddate", Utilities.Date_Time.DatetimeToUnixTimeStamp(Convert.ToDateTime(senddate)));

            return MakeSendRequest(url, param);
        }
        public ReceiveMessageResult ReceiveList(string linenumber, int isRead)
        {
            var url = "v2/sms/receive/last";
            var param = new Dictionary<string, object>
                {
                 {"apikey", _apikey},
                 {"linenumber", linenumber},
                 {"isRead", isRead},
                };
            var response = Client.ApiClient.Execute(url, param);
            return _JavaScriptSerializer.Deserialize<ReceiveMessageResult>(response);
        }

         public ReceiveMessageResult ReceiveListPaging(string linenumber, int isRead , DateTime fromdate , DateTime todate , int page=0 , int offset=200)
        {
            var url = "v2/sms/receive/paging";
            var param = new Dictionary<string, object>
                {
                 {"apikey", _apikey},
                 {"linenumber", linenumber},
                 {"isRead", isRead},
                 {"fromdate", Utilities.Date_Time.DatetimeToUnixTimeStamp(Convert.ToDateTime(fromdate))},
                 {"todate", Utilities.Date_Time.DatetimeToUnixTimeStamp(Convert.ToDateTime(todate))},
                 {"page", page},
                 {"offset", offset},
                };
            var response = Client.ApiClient.Execute(url, param);
            return _JavaScriptSerializer.Deserialize<ReceiveMessageResult>(response);
        }


        private ApiResult MakeRequest(string url, Dictionary<string, object> param)
        {
            return _JavaScriptSerializer.Deserialize<ApiResult>(Client.ApiClient.Execute(url, param));
        }
        private SendResult MakeSendRequest(string url, Dictionary<string, object> param)
        {
            return _JavaScriptSerializer.Deserialize<SendResult>(Client.ApiClient.Execute(url, param));
        }

       
    }
}
