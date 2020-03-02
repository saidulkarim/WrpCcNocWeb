using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WrpCcNocWeb.Models.Utility
{
    public class Notification
    {
        public Notification()
        {

        }

        public string id { get; set; }
        public string status { get; set; }
        public string message { get; set; }

        public Notification Success { set; get; }
    }
}
