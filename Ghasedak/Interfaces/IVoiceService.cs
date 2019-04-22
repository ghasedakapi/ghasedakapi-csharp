using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Ghasedak.Models.Results;

namespace Ghasedak.Interfaces
{
  public interface IVoiceService
    {
        SendResult SendVoice(string message, string[] receptor, DateTime? senddate);
    }
}
