using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static GhasedakApi.Models.Results;

namespace GhasedakApi.Interfaces
{
    public interface ISendService
    {
        ApiResult Send(string message, string linenumber, string receptor, string senddate);
        ApiResult Send(string message, string linenumber, string[] receptor, string senddate);
        ApiResult Send(string[] message, string[] linenumber, string[] receptor, string[] senddate);
        ApiResult Verify(int type, string template, string[] receptor, string param1, string param2, string param3);

    }
}
