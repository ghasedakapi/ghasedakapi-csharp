using GhasedakApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GhasedakApi
{
    public class SMS:ISendService
    {
        private readonly string _apikey;
        public SMS(string apikey)
        {
            _apikey = apikey;
        }
    }
}