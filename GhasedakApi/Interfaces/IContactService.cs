using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static GhasedakApi.Models.Results;

namespace GhasedakApi.Interfaces
{
    public interface IContactService
    {
        GroupResult AddGroup(string name, int parent);
        ApiResult RemoveGroup(int groupid);
        ApiResult EditGroup(int groupid,string name);
        ApiResult AddNumberToGroup(int groupid, string[] number,string [] firstname,string []lastname,string[] email);
        GroupListResult GroupList(int parent);
        GroupNumbersResult GroupNumbersList(int groupid, int page, int offset);
    }
}
