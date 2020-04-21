using Microsoft.AspNetCore.Http;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace WrpCcNocWeb.Models.Utility
{
    public class EmailModel
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public IFormFile Attachment { get; set; }        
    }
}
