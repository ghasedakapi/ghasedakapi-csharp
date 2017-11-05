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
            public string  Message { get; set; }
        }

    }
}