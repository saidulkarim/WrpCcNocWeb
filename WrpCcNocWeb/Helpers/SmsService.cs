using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using WrpCcNocWeb.Controllers;
using WrpCcNocWeb.DatabaseContext;
using WrpCcNocWeb.Models;
using WrpCcNocWeb.Models.AdminModule;
using WrpCcNocWeb.Models.TempModels;
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

        public bool Send(int SmsFormatId, long ReceiverUserId, List<string> vars)
        {
            bool result = false;

            try
            {
                AdminModUsersDetail ud = _db.AdminModUsersDetail.Find(ReceiverUserId);
                if (ud == null)
                {
                    result = false;
                    throw new NullReferenceException("User information not found by this receiver id: " + ReceiverUserId.ToString());
                }

                AdminModUserRegistrationDetail rd = _db.AdminModUserRegistrationDetail.Find(ud.UserRegistrationId);
                if (rd == null)
                {
                    result = false;
                    throw new NullReferenceException("User registration information not found by this receiver id: " + ReceiverUserId.ToString());
                }

                string user_mobile = rd.UserMobile;
                string sms_format = _db.LookUpAdminModSmsFormat.Find(SmsFormatId).SmsBody;

                if (vars.Count > 0)
                {
                    for (int i = 0; i < vars.Count; i++)
                    {
                        int varCount = i + 1;
                        string findString = "{{var_" + varCount + "}}";

                        if (sms_format.Contains(findString))
                        {
                            sms_format = sms_format.Replace(findString, vars[i]);
                        }
                    }
                }

                if (user_mobile.Length != 11)
                {
                    result = false;
                    throw new IOException("User mobile number is not valid. Mobile number must be in 11 digit.");
                }

                if (string.IsNullOrEmpty(sms_format))
                {
                    result = false;
                    throw new NullReferenceException("Sorry, SMS format not found!");
                }

                if (user_mobile.Length == 11 && !string.IsNullOrEmpty(sms_format))
                {
                    SmsSentInfo ssi = new SmsSentInfo()
                    {
                        SmsFormatId = SmsFormatId,
                        ReceiverUserId = ReceiverUserId.ToString().ToInt32(),
                        UserMobile = "88" + user_mobile,
                        SmsBody = sms_format
                    };

                    result = SendSms(ssi);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

        /// <summary>
        /// Writer: <b>A.T.M. Saidul Karim</b><br />
        /// Written on: 11 Oct, 2020<br />
        /// Input: SmsSentInfo contains SmsFormatId, ReceiverUserId, UserMobile, SmsBody, SmsSentOn.<br />
        /// Output: Boolean data
        /// </summary>
        /// <param name="ssi">SmsSentInfo contains: SmsFormatId, ReceiverUserId, UserMobile, SmsBody, SmsSentOn</param>
        /// <returns>Boolean data</returns>
        public bool SendSms(SmsSentInfo ssi)
        {
            bool result = false;

            try
            {
                string strUrl = @"http://api.boom-cast.com/boomcast/WebFramework/boomCastWebService/externalApiSendTextMessage.php?masking=NOMASK&userName=CEGIS&password=a56c47b8954ee64f6b4db79bc60ba184&MsgType=TEXT&receiver=" + ssi.UserMobile + "&message=" + ssi.SmsBody;
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
                    foreach (var data in responseData)
                    {

                    }
                }

                //LookUpAdminModSmsHistory sh = new LookUpAdminModSmsHistory()
                //{
                //    SmsFormatId = ssi.SmsFormatId,
                //    ReceiverUserId = ssi.ReceiverUserId,
                //    UserMobile = ssi.UserMobile,
                //    SmsBody = ssi.SmsBody,
                //    SmsSentOn = DateTime.Now
                //};

                //_db.LookUpAdminModSmsHistory.Add(sh);
                //if (_db.SaveChanges() > 0)
                //{
                //    result = true;
                //}

                result = true;
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

        /// <summary>
        /// Writer: <b>A.T.M. Saidul Karim</b><br />
        /// Written on: 11 Oct, 2020<br />
        /// Input: user_id = Application logged in user id. mobile_number = 11 digits mobile number. 
        /// message = Message string should not more than 150 chars.<br />
        /// Output: Boolean data
        /// </summary>
        /// <param name="user_id">Application logged in user id</param>
        /// <param name="mobile_number">11 digits mobile number</param>
        /// <param name="message">Message string should not more than 150 chars</param>
        /// <returns></returns>
        public bool SendSms(int user_id, string mobile_number, string message)
        {
            bool result = false;

            try
            {
                string strUrl = @"http://api.boom-cast.com/boomcast/WebFramework/boomCastWebService/externalApiSendTextMessage.php?masking=NOMASK&userName=CEGIS&password=a56c47b8954ee64f6b4db79bc60ba184&MsgType=TEXT&receiver=" + mobile_number + "&message=" + message;
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
                    foreach (var data in responseData)
                    {
                        LookUpAdminModSmsHistory sh = new LookUpAdminModSmsHistory()
                        {
                            ReceiverUserId = user_id,
                            UserMobile = mobile_number,
                            SmsBody = message,
                            SmsSentOn = DateTime.Now
                        };

                        _db.LookUpAdminModSmsHistory.Add(sh);
                    }

                    if (_db.SaveChanges() > 0)
                    {
                        result = true;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }
    }
}
