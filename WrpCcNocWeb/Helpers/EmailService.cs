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
using WrpCcNocWeb.Models;
using WrpCcNocWeb.Models.AdminModule;
using WrpCcNocWeb.Models.Utility;

namespace WrpCcNocWeb.Helpers
{
    public class EmailService
    {
        public EmailService()
        {

        }

        #region Initialization
        private readonly LoggedUserInfo iLoggedUser;
        private readonly WrpCcNocDbContext _db = new WrpCcNocDbContext();
        private readonly CommonHelper ch = new CommonHelper();
        private Notification noti = new Notification();
        private readonly string rootDirOfProjFile = "../images";
        private readonly string rootDirOfDocs = "../docs";
        #endregion

        /// <summary>
        /// Send Email Feature
        /// </summary>
        /// <param name="toEmail">To email address.</param>
        /// <param name="emailFormatId">Email format Id from LookUpAdminModEmailFormat table.</param>
        /// <param name="vars">List array string parameters</param>
        /// <returns></returns>
        public string SendEmail(string toEmail, int emailFormatId, List<string> vars)
        {
            string result = string.Empty, mailSubject = string.Empty, mailBody = string.Empty;
            LookUpAdminModEmailFormat eFormat = new LookUpAdminModEmailFormat();

            eFormat = _db.LookUpAdminModEmailFormat.Where(w => w.EmailFormatId == emailFormatId).FirstOrDefault();

            if (eFormat != null)
            {
                mailSubject = eFormat.EmailSubject;
                mailBody = eFormat.EmailSalutation +
                           eFormat.EmailBody1stPara +
                           eFormat.EmailBody2ndPara +
                           eFormat.EmailBody3rdPara +
                           eFormat.EmailBodyClosing1stLine +
                           eFormat.EmailBodyClosing2ndLine +
                           eFormat.ValedictoryGreetings +
                           eFormat.EmailSenderInfo1stLine +
                           eFormat.EmailSenderInfo2ndLine +
                           eFormat.EmailSenderInfo3rdLine +
                           eFormat.EmailSenderInfo4thLine;

                if (vars.Count > 0)
                {
                    for (int i = 0; i < vars.Count; i++)
                    {
                        int varCount = i + 1;
                        string findString = "{{var_" + varCount + "}}";

                        if (mailBody.Contains(findString))
                        {
                            mailBody = mailBody.Replace(findString, vars[i]);
                        }
                    }

                    EmailModel em = new EmailModel
                    {
                        To = toEmail,
                        Subject = mailSubject,
                        Body = mailBody,
                        Attachment = null
                    };

                    if (em != null)
                    {
                        result = Send(em);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Fianl Email Sending Method
        /// </summary>
        /// <param name="model">EmailModel parameter</param>
        /// <returns></returns>
        public string Send(EmailModel model)
        {
            string result = string.Empty;
            EmailConfiguration emailConfig = new EmailConfiguration();
            try
            {
                LookUpCcModGeneralSetting generalSetting = ch.GetAppGenInfo();
                if (generalSetting == null)
                {
                    throw new NullReferenceException("Email configuration object is null. Could not get configuration data from database.");
                }

                if (generalSetting.IsEmailServiceActive == 1)
                {
                    emailConfig.From = generalSetting.MailSendFrom;
                    emailConfig.SmtpServer = generalSetting.SmtpServer;
                    emailConfig.Port = generalSetting.SmtpOutGoingPort.ToInt();
                    emailConfig.UserName = generalSetting.MailSenderUserName;
                    emailConfig.Password = generalSetting.MailSenderPassword;

                    //var emailConfig = GetEmailConfiguration.GetConfig().GetSection("EmailConfiguration").Get<EmailConfiguration>();
                    //var connectionstring = setting["EmailConfiguration"];
                    //var emailConfig = Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>();

                    using MailMessage mm = new MailMessage(emailConfig.UserName, model.To)
                    {
                        Subject = model.Subject,
                        Body = model.Body
                    };
                   
                    if (model.Attachment != null)
                    {
                        if (model.Attachment.Length > 0)
                        {
                            string fileName = Path.GetFileName(model.Attachment.FileName);
                            mm.Attachments.Add(new Attachment(model.Attachment.OpenReadStream(), fileName));
                        }
                    }

                    mm.IsBodyHtml = true;

                    using SmtpClient smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        EnableSsl = true
                    };

                    NetworkCredential NetworkCred = new NetworkCredential(emailConfig.UserName, emailConfig.Password);
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = NetworkCred;
                    smtp.Port = 587;
                    smtp.Send(mm);
                }

                result = "success";
            }
            catch (Exception ex)
            {
                result = ch.ExtractInnerException(ex);
            }

            return result;
        }
    }
}
