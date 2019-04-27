using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Ghasedak.Models.Results;

namespace Ghasedak.Interfaces
{
    public interface ISMSService
    {
        SendResult SendSMS(string message, string receptor, string linenumber = null, DateTime? senddate = null, String checkid = null);
        SendResult SendSMS(string[] message, string[] linenumber, string[] receptor, DateTime[] senddate = null, string[] checkid = null);
        SendResult SendSMS(string message, string[] receptor, string linenumber = null, DateTime? senddate = null, string[] checkid = null);
        SendResult Verify(int type, string template, string[] receptor, string param1, string param2 = null, string param3 = null, string param4 = null, string param5 = null, string param6 = null, string param7 = null, string param8 = null, string param9 = null, string param10 = null);
        StatusResult GetStatus(int type, long[] id);
        SendResult CancelSMS(long[] messageid);
    }
}
