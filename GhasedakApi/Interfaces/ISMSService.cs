using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static GhasedakApi.Models.Results;

namespace GhasedakApi.Interfaces
{
    public interface ISMSService
    {
        SendResult SendSMS(string message, string linenumber, string receptor);
        SendResult SendSMS(string message, string linenumber, string receptor, DateTime senddate);
        SendResult SendSMS(string message, string linenumber, string[] receptor);
        SendResult SendSMS(string message, string linenumber, string[] receptor, DateTime senddate);
        SendResult SendSMS(string[] message, string[] linenumber, string[] receptor);
        SendResult SendSMS(string[] message, string[] linenumber, string[] receptor, DateTime[] senddate);
        SendResult Verify(int type, string template, string[] receptor, string param1, string param2, string param3);
        StatusResult GetStatus(long[] messageid);
        SendResult CancelSMS(long[] messageid);
        SelectMessageResult SelectSMS(long[] messageid);
    }
}
