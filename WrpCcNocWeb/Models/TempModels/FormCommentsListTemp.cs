using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WrpCcNocWeb.Models
{
    public class FormCommentsListTemp
    {
        public long? AppProjDataAnalysisId { get; set; }
        public long UserId { get; set; }
        public string UserComments { get; set; }
        public string UserName { get; set; }
    }
}
