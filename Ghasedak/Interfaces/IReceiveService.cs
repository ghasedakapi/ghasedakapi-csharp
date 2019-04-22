using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Ghasedak.Models.Results;

namespace Ghasedak.Interfaces
{
   public  interface IReceiveService
    {
        ReceiveMessageResult ReceiveList(string linenumber, int isRead);
        ReceiveMessageResult ReceiveListPaging(string linenumber, int isRead, DateTime fromdate, DateTime todate, int page = 0, int offset = 200);

    }
}
