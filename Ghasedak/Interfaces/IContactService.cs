using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Ghasedak.Models.Results;

namespace Ghasedak.Interfaces
{
    public interface IContactService
    {
        GroupResult AddGroup(string name, int parent, string dep = null);
        ApiResult RemoveGroup(int groupid, string dep = null);
        ApiResult EditGroup(int groupid,string name, string dep = null);
        ApiResult AddNumberToGroup(int groupid, string[] number,string [] firstname,string []lastname,string[] email, string dep = null);
        GroupListResult GroupList(int parent, string dep = null);
        GroupNumbersResult GroupNumbersList(int groupid, int page, int offset, string dep = null);
    }
}
