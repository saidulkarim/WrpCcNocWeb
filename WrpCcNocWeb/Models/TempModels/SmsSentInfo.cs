using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WrpCcNocWeb.Models.TempModels
{
    public class SmsSentInfo
    {       
        public int SmsFormatId { get; set; }
        public int ReceiverUserId { get; set; }
        public string UserMobile { get; set; }
        public string SmsBody { get; set; }
        public DateTime? SmsSentOn { get; set; }
    }
}
