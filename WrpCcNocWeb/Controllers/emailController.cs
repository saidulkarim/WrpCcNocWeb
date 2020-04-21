using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WrpCcNocWeb.DatabaseContext;
using WrpCcNocWeb.Helpers;
using WrpCcNocWeb.Models.Utility;

namespace WrpCcNocWeb.Controllers
{
    public class emailController : Controller
    {
        public emailController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        #region Initialization
        private readonly LoggedUserInfo iLoggedUser;
        private readonly WrpCcNocDbContext _db = new WrpCcNocDbContext();
        private readonly CommonHelper ch = new CommonHelper();
        private Notification noti = new Notification();
        private readonly string rootDirOfProjFile = "../images";
        private readonly string rootDirOfDocs = "../docs";
        public IConfiguration Configuration { get; }
        #endregion

        // GET: Home
        public IActionResult Index()
        {
            ViewBag.Message = "";
            return View();
        }

        public string SendEmail(EmailModel model)
        {
            string result = string.Empty;

            try
            {
                var emailConfig = Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>();

                using (MailMessage mm = new MailMessage(emailConfig.UserName, model.To))
                {
                    mm.Subject = model.Subject;
                    mm.Body = model.Body;

                    if (model.Attachment.Length > 0)
                    {
                        string fileName = Path.GetFileName(model.Attachment.FileName);
                        mm.Attachments.Add(new Attachment(model.Attachment.OpenReadStream(), fileName));
                    }

                    mm.IsBodyHtml = false;

                    using (SmtpClient smtp = new SmtpClient())
                    {
                        smtp.Host = "smtp.gmail.com";
                        smtp.EnableSsl = true;
                        NetworkCredential NetworkCred = new NetworkCredential(emailConfig.UserName, emailConfig.Password);
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = NetworkCred;
                        smtp.Port = 587;
                        smtp.Send(mm);

                        result = "Email has been sent to the destination.";
                    }
                }
            }
            catch (Exception ex)
            {
                result = ch.ExtractInnerException(ex);
            }

            return result;
        }
    }
}