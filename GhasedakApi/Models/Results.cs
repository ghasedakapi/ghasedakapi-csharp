using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GhasedakApi.Models
{
    public class Results
    {
        public class ApiResult
        {
            public ResultItems Result { get; set; }
            public object Items { get; set; }

        }
        public class ResultItems
        {
            public int Code { get; set; }
            public string Message { get; set; }
        }
        public class ReceiveMessageResult
        {
            public ResultItems Result { get; set; }
            public IList<ReceiveMessageItems> Items { get; set; }
        }

        public class ReceiveMessageItems
        {
            public long messageid { get; set; }
            public string message { get; set; }
            public string linenumber { get; set; }
            public DateTime receivedate { get; set; }
        }
        public class SendResult
        {
            public ResultItems Result { get; set; }
            public IList<long> Items { get; set; }
        }
        public class StatusResult
        {
            public ResultItems Result { get; set; }
            public IList<StatusItems> Items { get; set; }
        }
        public class StatusItems
        {
            public long messageId { get; set; }
            public int status { get; set; }
        }
        public class GroupResult
        {
            public ResultItems Result { get; set; }
            public AddGroupItem Items { get; set; }

        }
        public class AddGroupItem
        {
            public int GroupId { get; set; }
        }

        public class GroupNumbersResult
        {
            public ResultItems Result { get; set; }

        }
        public class GroupNumbersItem
        {
            public string firstName { get; set; }
            public string lastName { get; set; }
            public string number { get; set; }
            public string email { get; set; }
        }
        public class GroupListResult
        {
            public ResultItems Result { get; set; }

        }

        public class GroupListItem
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int Parent { get; set; }
        }
    }
}