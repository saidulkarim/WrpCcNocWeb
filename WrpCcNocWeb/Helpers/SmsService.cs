using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Net;
using WrpCcNocWeb.Controllers;
using WrpCcNocWeb.DatabaseContext;
using WrpCcNocWeb.Models.Utility;

namespace WrpCcNocWeb.Helpers
{
    public class SmsService
    {
        #region Initialization    
        private readonly IWebHostEnvironment hostingEnvironment;
        private readonly WrpCcNocDbContext _db = new WrpCcNocDbContext();
        private readonly CommonHelper ch = new CommonHelper();
        private commonController cc = new commonController();
        private EmailService es = new EmailService();
        private Notification noti = new Notification();
        #endregion

        public SmsService()
        {

        }

        public bool SendSms(string cell_number, string message)
        {
            bool result = false;
            string strUrl = @"http://api.boom-cast.com/boomcast/WebFramework/boomCastWebService/externalApiSendTextMessage.php?masking=NOMASK&userName=CEGIS&password=a56c47b8954ee64f6b4db79bc60ba184&MsgType=TEXT&receiver=" + cell_number + "&message=" + message;
            WebRequest request = HttpWebRequest.Create(strUrl);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream s = (Stream)response.GetResponseStream();
            StreamReader readStream = new StreamReader(s);
            string responseString = readStream.ReadToEnd();
            response.Close();
            s.Close();
            readStream.Close();
            dynamic responseData = Newtonsoft.Json.JsonConvert.DeserializeObject(responseString);

            if (!string.IsNullOrEmpty(responseString))
            {

            }

            return result;
        }
    }
}
