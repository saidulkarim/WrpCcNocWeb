﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using WrpCcNocWeb.Models;
using WrpCcNocWeb.Helpers;
using static WrpCcNocWeb.Helpers.CommonHelper;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Microsoft.AspNetCore.Http;
using WrpCcNocWeb.Models.Utility;
using WrpCcNocWeb.Models.UserManagement;
using WrpCcNocWeb.DatabaseContext;
using Microsoft.AspNetCore.Mvc.Rendering;
using WrpCcNocWeb.ViewModels;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Hosting;
using System.Net.Mail;
using System.Text.RegularExpressions;
using WrpCcNocWeb.Models.TempModels;

namespace WrpCcNocWeb.Controllers
{
    public class accountController : Controller
    {
        private readonly WrpCcNocDbContext _db = new WrpCcNocDbContext();
        private readonly CommonHelper ch = new CommonHelper();
        private commonController cc = new commonController();
        private EmailService es = new EmailService();
        private string msg = string.Empty;
        private Notification noti = new Notification();

        private readonly IWebHostEnvironment hostingEnvironment;

        public accountController(IWebHostEnvironment hostingEnvironment)
        {
            this.hostingEnvironment = hostingEnvironment;
        }

        public IActionResult index()
        {
            return View();
        }

        //account/login
        public IActionResult login()
        {
            return View();
        }

        //account/login
        [HttpPost]
        public IActionResult login(LoginTemp _loginTemp)
        {
            if (ModelState.IsValid)
            {
                string UserName = _loginTemp.UserName.ToString().Trim();
                string UserPass = _loginTemp.UserPass.ToString().Trim();

                var userRegInfo = _db.AdminModUserRegistrationDetail
                                     .Where(r => r.UserName.ToString().Equals(UserName) && r.UserPassword.ToString().Equals(UserPass.EncryptString()))
                                     .FirstOrDefault();

                if (!Captcha.ValidateCaptchaCode(_loginTemp.CaptchaCode, HttpContext))
                {
                    TempData["Message"] = ch.ShowMessage(Sign.Error, Sign.Error.ToString(), "Invalid captcha code entered!");
                    return View();
                }
                else
                {
                    if (userRegInfo != null)
                    {
                        if (userRegInfo.IsEmailVerified == 1)
                        {
                            if (userRegInfo.UserActivationStatus == 1)
                            {
                                if (userRegInfo.UserPassword.Equals(UserPass.EncryptString()))
                                {
                                    var user_id_temp = _db.AdminModUsersDetail
                                                        .Where(p => p.UserRegistrationId != null && p.UserRegistrationId == userRegInfo.UserRegistrationId)
                                                        .Select(x => new
                                                        {
                                                            UserId = x.UserId,
                                                        }).FirstOrDefault();

                                    long UserId = (user_id_temp == null) ? 0 : user_id_temp.UserId.ToString().ToLong();
                                    UserInfoToSession(UserName, UserId);

                                    if (UserId != 0)
                                    {
                                        if (UserPass != "1234")
                                        {
                                            return RedirectToActionPermanent("index", "home");
                                        }
                                        else
                                        {
                                            return RedirectToActionPermanent("profile", "account");
                                        }
                                    }
                                    else
                                    {
                                        return RedirectToActionPermanent("profile", "account");
                                    }
                                }
                                else
                                {
                                    TempData["Message"] = ch.ShowMessage(Sign.Error, Sign.Error.ToString(), "Wrong password.");
                                    return View();
                                }
                            }
                            else
                            {
                                TempData["Message"] = ch.ShowMessage(Sign.Error, Sign.Error.ToString(), "Inactive user.");
                                return View();
                            }
                        }
                        else
                        {
                            TempData["Message"] = ch.ShowMessage(Sign.Error, Sign.Error.ToString(), "Please verify your account first.");
                            return View();
                        }
                    }
                    else
                    {
                        TempData["Message"] = ch.ShowMessage(Sign.Error, Sign.Error.ToString(), "Invalid login attempt.");
                        return View();
                    }
                }
            }

            return View();
        }

        private void UserInfoToSession(string logged_user, long user_id = 0)
        {
            UserInfo _ui = new UserInfo();

            if (user_id == 0)
            {
                _ui = (from ri in _db.AdminModUserRegistrationDetail
                       where ri.UserName.Equals(logged_user)
                       select new UserInfo
                       {
                           UserID = 0,
                           UserRegistrationID = ri.UserRegistrationId,
                           UserName = ri.UserName.ToString(),
                           UserEmail = ri.UserEmail.ToString(),
                           UserMobile = ri.UserMobile.ToString(),
                           UserActivationStatus = ri.UserActivationStatus,
                           DateOfCreation = ri.DateOfCreation
                       }).FirstOrDefault();
            }
            else
            {
                _ui = (from ri in _db.AdminModUserRegistrationDetail
                       join ud in _db.AdminModUsersDetail on ri.UserRegistrationId equals ud.UserRegistrationId into udGroup
                       from ui in udGroup.DefaultIfEmpty()
                       where ri.UserName.Equals(logged_user)
                       select new UserInfo
                       {
                           UserID = user_id,
                           UserRegistrationID = ri.UserRegistrationId,
                           UserName = ri.UserName.ToString(),
                           UserFullName = ui.ApplicantTypeId == 1 ? ui.ApplicantName : ui.OrganizationName, //ui.UserFullName,
                           UserDesignation = ui.UserDesignation,
                           UserMobile = ri.UserMobile.ToString(),
                           UserEmail = ri.UserEmail.ToString(),
                           UserAddress = ui.PostalAddress,
                           UserActivationStatus = ri.UserActivationStatus,
                           DateOfCreation = ri.DateOfCreation,
                           LastModifiedDate = ri.LastModifiedDate,
                           IsDeleted = ri.IsDeleted
                       }).FirstOrDefault();
            }

            if (_ui != null && _ui.UserRegistrationID != 0)
            {
                HttpContext.Session.SetComplexData("LoggerUserInfo", _ui);

                HttpContext.Response.Cookies.Append("UserName", string.Empty, new CookieOptions()
                {
                    Expires = DateTime.Now.AddDays(-1)
                });

                HttpContext.Response.Cookies.Append("UserName", _ui.UserName, new CookieOptions()
                {
                    Expires = DateTime.Now.AddDays(1)
                });

                //HttpContext.Session.SetString("UserID", _ui.UserID.ToString());
                //HttpContext.Session.SetString("UserRegistrationID", _ui.UserRegistrationID.ToString());
                //HttpContext.Session.SetString("UserName", _ui.UserName);
                //HttpContext.Session.SetString("UserEmail", _ui.UserEmail);
                //HttpContext.Session.SetString("UserMobile", _ui.UserMobile);

                userloghist(_ui.UserID);
            }
        }

        private void userloghist(long userID)
        {
            if (userID > 0)
            {
                AdminModUserLogHistoryDetail _log = new AdminModUserLogHistoryDetail
                {
                    UserId = userID,
                    LoginDateTime = DateTime.Now,
                    MachineIPOrUrl = HttpContext.Connection.RemoteIpAddress.ToString()
                };

                _db.AdminModUserLogHistoryDetail.Add(_log);
                _db.SaveChanges();
            }
        }

        //account/register
        public IActionResult register()
        {
            return View();
        }

        //POST: /account/register
        [HttpPost]
        public IActionResult register(AdminModUserRegistrationDetail userReg)
        {
            string result = string.Empty;
            using var dbContextTransaction = _db.Database.BeginTransaction();
            int count = 0;

            if (!string.IsNullOrEmpty(userReg.UserEmail))
            {
                if (!IsValidEmail(userReg.UserEmail.ToLower()))
                {
                    TempData["Message"] = ch.ShowMessage(Sign.Error, "Invalid Email Address", "Invalid email address provided!");
                    return View();
                }

                count = _db.AdminModUserRegistrationDetail.Where(w => w.UserEmail.ToLower() == userReg.UserEmail.ToLower()).Count();
                if (count > 0)
                {
                    dbContextTransaction.Rollback();
                    ViewBag.UserVerificationCode = string.Empty;
                    TempData["Message"] = ch.ShowMessage(Sign.Error, "Duplicate Email", "Email addresss: " + userReg.UserEmail + " already used in this system. Please try another email address.");
                    return View();
                }
            }

            if (!string.IsNullOrEmpty(userReg.UserMobile))
            {
                if (!IsValidMobile(userReg.UserMobile.ToLower()))
                {
                    TempData["Message"] = ch.ShowMessage(Sign.Error, "Invalid Mobile Number", "Invalid mobile number provided!");
                    return View();
                }

                count = _db.AdminModUserRegistrationDetail.Where(w => w.UserMobile.ToLower() == userReg.UserMobile.ToLower()).Count();
                if (count > 0)
                {
                    dbContextTransaction.Rollback();
                    ViewBag.UserVerificationCode = string.Empty;
                    TempData["Message"] = ch.ShowMessage(Sign.Error, "Duplicate Mobile Number", "Mobile number: " + userReg.UserMobile + " already used in this system. Please try another mobile number.");
                    return View();
                }
            }

            try
            {
                userReg.UserPassword = userReg.UserPassword.EncryptString();
                userReg.UserActivationStatus = 0;
                userReg.DateOfCreation = DateTime.Now;
                userReg.EmailVerificationCode = Guid.NewGuid().ToString();
                userReg.IsEmailVerified = 0;
                userReg.IsDeleted = 0;

                _db.AdminModUserRegistrationDetail.Add(userReg);
                int x = _db.SaveChanges();

                if (x > 0)
                {
                    ViewBag.UserVerificationCode = userReg.EmailVerificationCode;
                    TempData["Message"] = ch.ShowMessage(Sign.Success, Sign.Success.ToString(), OperationMessage.Success.ToDescription());

                    result = NewRegSendEmailConfirmation(userReg);

                    if (result == "success")
                    {
                        dbContextTransaction.Commit();
                        msg = "Your have successfully registered. Please check your mail to verify account.";
                        TempData["Message"] = ch.ShowMessage(Sign.Success, Sign.Success.ToString(), msg);

                        //return View();
                        return RedirectToAction("login");
                        //return RedirectToAction("verify", new { id = ViewBag.UserVerificationCode });
                    }
                    else
                    {
                        TempData["Message"] = ch.ShowMessage(Sign.Error, "An Error Occured", result);
                        return View();
                    }
                }
                else
                {
                    dbContextTransaction.Rollback();
                    ViewBag.UserVerificationCode = string.Empty;
                    TempData["Message"] = ch.ShowMessage(Sign.Error, "An Error Occured", OperationMessage.NotSuccess.ToDescription());
                }
            }
            catch (Exception ex)
            {
                dbContextTransaction.Rollback();
                ViewBag.UserVerificationCode = string.Empty;
                var message = ch.ExtractInnerException(ex);
                TempData["Message"] = ch.ShowMessage(Sign.Danger, "An Error Occured", message);
            }

            return View();
        }

        public string NewRegSendEmailConfirmation(AdminModUserRegistrationDetail userReg)
        {
            string result = string.Empty;
            string verifyUrl = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
            List<string> vars = new List<string>();

            if (userReg != null)
            {
                string callAt = string.Empty;
                LookUpCcModGeneralSetting generalSetting = _db.LookUpCcModGeneralSetting.Find(1);

                if (!string.IsNullOrEmpty(generalSetting.CallCenterNumber))
                {
                    callAt = generalSetting.CallCenterNumber;
                }

                if (!string.IsNullOrEmpty(generalSetting.TnTNumber))
                {
                    callAt += (string.IsNullOrEmpty(callAt)) ? generalSetting.TnTNumber : " or " + generalSetting.TnTNumber;
                }

                if (!string.IsNullOrEmpty(generalSetting.MobileNumber))
                {
                    callAt += (string.IsNullOrEmpty(callAt)) ? generalSetting.MobileNumber : " or " + generalSetting.MobileNumber;
                }

                verifyUrl = verifyUrl + "/account/verify/" + ViewBag.UserVerificationCode;
                vars.Add(userReg.UserName);
                vars.Add(userReg.UserPassword.DecryptString());
                vars.Add(verifyUrl);
                vars.Add(callAt);

                //1 => New Requester Registration in LookUpAdminModEmailFormat table
                result = es.SendEmail(userReg.UserEmail, 1, vars);
            }

            return result;
        }

        //account/verify
        public IActionResult verify()
        {
            return View();
        }

        //account/verify
        [HttpPost]
        public IActionResult verify(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                AdminModUserRegistrationDetail userRegVerify = _db.AdminModUserRegistrationDetail.Where(w => w.EmailVerificationCode.Equals(id)).FirstOrDefault();

                if (userRegVerify != null)
                {
                    try
                    {
                        userRegVerify.UserActivationStatus = 1;
                        userRegVerify.LastModifiedDate = DateTime.Now;
                        userRegVerify.IsEmailVerified = 1;
                        userRegVerify.IsDeleted = 0;

                        _db.Entry(userRegVerify).State = EntityState.Modified;
                        int x = _db.SaveChanges();

                        if (x > 0)
                        {
                            var user_id_temp = _db.AdminModUsersDetail
                                                .Where(p => p.UserRegistrationId == userRegVerify.UserRegistrationId)
                                                .Select(x => new
                                                {
                                                    UserId = x.UserId,
                                                }).FirstOrDefault();

                            long UserId = (user_id_temp == null) ? 0 : user_id_temp.UserId.ToString().ToLong();
                            UserInfoToSession(userRegVerify.UserName, UserId);

                            if (UserId != 0)
                            {
                                return RedirectToActionPermanent("index", "home");
                            }
                            else
                            {
                                return RedirectToActionPermanent("profile", "account");
                            }
                            //return RedirectToAction("login");
                            //return RedirectToAction("profile"); //rony
                        }
                        else
                        {
                            TempData["Message"] = ch.ShowMessage(Sign.Error, "Sorry, verification failed!");
                        }
                    }
                    catch (Exception ex)
                    {
                        ViewBag.UserVerificationCode = string.Empty;

                        var message = ch.ExtractInnerException(ex);
                        TempData["Message"] = ch.ShowMessage(Sign.Danger, message);
                    }
                }
            }

            ViewBag.UserVerificationCode = id;
            return View();
        }

        //account/profile
        public IActionResult profile()
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
            if (ui == null)
            {
                return RedirectToAction("login", "account");
            }

            ViewBag.UserName = ui.UserName;
            ViewBag.UserHasDefPwd = HasDefPwd(ui.UserName);
            ViewBag.UserRegistrationID = ui.UserRegistrationID;
            ViewBag.ApplicantTypeId = new SelectList(_db.LookUpCcModApplicantType.ToList(), "ApplicantTypeId", "ApplicantType");
            ViewBag.SecurityQuestionId = new SelectList(_db.LookUpAdminModSecurityQuestion.ToList(), "SecurityQuestionId", "SecurityQuestion");

            return View();
        }

        //account/saveprofile
        [HttpPost]
        [Obsolete]
        public JsonResult saveprofile(long UserId, long UserRegistrationId, int ApplicantTypeId,
            string ApplicantName, string ApplicantNameBn, string OrganizationName, string OrganizationNameBn,
            string OrganizationAddress, string OrganizationAddressBn, string UserProfession, string UserDesignation,
            string UserNID, string PostalAddress, string PostalAddressBn, int SecurityQuestionId,
            string SecurityQuestionAnswer, IFormFile file, IFormFile nidFile)
        {
            int result = 0;
            AdminModUsersDetail _user = new AdminModUsersDetail();

            #region New Applicant User Registration
            if (UserRegistrationId > 0)
            {
                var _userExistCheck = _db.AdminModUsersDetail.Where(u => u.UserRegistrationId == UserRegistrationId).FirstOrDefault();
                using var dbContextTransaction = _db.Database.BeginTransaction();

                if (_userExistCheck == null)
                {
                    try
                    {
                        _user.UserRegistrationId = UserRegistrationId;
                        _user.ApplicantTypeId = ApplicantTypeId;
                        _user.ApplicantName = ApplicantName;
                        _user.ApplicantNameBn = ApplicantNameBn;
                        _user.OrganizationName = OrganizationName;
                        _user.OrganizationNameBn = OrganizationNameBn;
                        _user.OrganizationAddress = OrganizationAddress;
                        _user.OrganizationAddressBn = OrganizationAddressBn;
                        _user.UserProfession = UserProfession;
                        _user.UserDesignation = UserDesignation;
                        _user.UserNID = UserNID;
                        _user.PostalAddress = PostalAddress;
                        _user.PostalAddressBn = PostalAddressBn;
                        _user.SecurityQuestionId = SecurityQuestionId;
                        _user.SecurityQuestionAnswer = SecurityQuestionAnswer;
                        _user.IsProfileSubmitted = 1;

                        _db.AdminModUsersDetail.Add(_user);
                        result = _db.SaveChanges();

                        if (result > 0)
                        {
                            AdminModUserGrpDistDetail ugdd = new AdminModUserGrpDistDetail
                            {
                                UserId = _user.UserId,
                                UserGroupId = 1000000001
                            };

                            _db.AdminModUserGrpDistDetail.Add(ugdd);
                            result = _db.SaveChanges();

                            if (result > 0)
                            {
                                noti = uunc(_user.UserId, "NidFileName", nidFile);

                                if (noti.status == "success")
                                {
                                    noti = uusf(_user.UserId, "SignatureFileName", file);

                                    if (noti.status == "success")
                                    {
                                        dbContextTransaction.Commit();

                                        AdminModUserRegistrationDetail urd = _db.AdminModUserRegistrationDetail.Find(UserRegistrationId);
                                        UserInfoToSession(urd.UserName, _user.UserId);

                                        //UserInfo _ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
                                        //_ui.UserID = _user.UserId;
                                        //HttpContext.Session.SetComplexData("LoggerUserInfo", _ui);

                                        noti = new Notification
                                        {
                                            id = _user.UserId.ToString(),
                                            status = "success",
                                            title = "Success",
                                            message = "Profile information has been successfully submitted. Your signature also uploaded."
                                        };

                                        //return RedirectToAction("index", "home");
                                    }
                                }
                            }
                            else
                            {
                                dbContextTransaction.Rollback();

                                noti = new Notification
                                {
                                    id = "0",
                                    status = "error",
                                    title = "Submission Error",
                                    message = "Sorry, your profile information not submitted!"
                                };
                            }
                        }
                        else
                        {
                            dbContextTransaction.Rollback();

                            noti = new Notification
                            {
                                id = "0",
                                status = "error",
                                title = "Submission Error",
                                message = "Sorry, your profile information not submitted!"
                            };
                        }
                    }
                    catch (Exception ex)
                    {
                        dbContextTransaction.Rollback();
                        var message = ch.ExtractInnerException(ex);

                        noti = new Notification
                        {
                            id = "0",
                            status = "error",
                            title = "Exception Error",
                            message = message
                        };
                    }
                }
                else
                {
                    try
                    {
                        //_userExistCheck.UserRegistrationId = _userExistCheck.UserRegistrationId;                        
                        _userExistCheck.ApplicantTypeId = ApplicantTypeId;
                        _userExistCheck.ApplicantName = ApplicantName;
                        _userExistCheck.ApplicantNameBn = ApplicantNameBn;
                        _userExistCheck.OrganizationName = OrganizationName;
                        _userExistCheck.OrganizationNameBn = OrganizationNameBn;
                        _userExistCheck.OrganizationAddress = OrganizationAddress;
                        _userExistCheck.OrganizationAddressBn = OrganizationAddressBn;
                        _userExistCheck.UserProfession = UserProfession;
                        _userExistCheck.UserDesignation = UserDesignation;
                        _userExistCheck.UserNID = UserNID;
                        _userExistCheck.PostalAddress = PostalAddress;
                        _userExistCheck.PostalAddressBn = PostalAddressBn;
                        _userExistCheck.SecurityQuestionId = SecurityQuestionId;
                        _userExistCheck.SecurityQuestionAnswer = SecurityQuestionAnswer;
                        _userExistCheck.IsProfileSubmitted = _userExistCheck.IsProfileSubmitted;

                        _db.Entry(_userExistCheck).State = EntityState.Modified;
                        result = _db.SaveChanges();

                        if (result > 0)
                        {
                            if (file != null)
                            {
                                noti = uunc(_userExistCheck.UserId, "NidFileName", nidFile);

                                if (noti.status == "success")
                                {
                                    noti = uusf(_userExistCheck, "SignatureFileName", file);

                                    if (noti.status == "success")
                                    {
                                        dbContextTransaction.Commit();
                                        //UserInfo _ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
                                        //_ui.UserID = _userExistCheck.UserId;
                                        //HttpContext.Session.SetComplexData("LoggerUserInfo", _ui);

                                        noti = new Notification
                                        {
                                            id = _userExistCheck.UserId.ToString(),
                                            status = "success",
                                            title = "Success",
                                            message = "Profile information has been successfully updated and signature also uploaded."
                                        };
                                    }
                                }
                            }
                            else
                            {
                                dbContextTransaction.Commit();
                                //UserInfo _ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
                                //_ui.UserID = _userExistCheck.UserId;
                                //HttpContext.Session.SetComplexData("LoggerUserInfo", _ui);

                                noti = new Notification
                                {
                                    id = _userExistCheck.UserId.ToString(),
                                    status = "success",
                                    title = "Success",
                                    message = "Profile information has been successfully updated."
                                };
                            }
                        }
                        else
                        {
                            dbContextTransaction.Rollback();

                            noti = new Notification
                            {
                                id = "0",
                                status = "error",
                                title = "Submission Error",
                                message = "Sorry, profile information not update!"
                            };
                        }
                    }
                    catch (Exception ex)
                    {
                        dbContextTransaction.Rollback();
                        var message = ch.ExtractInnerException(ex);

                        noti = new Notification
                        {
                            id = "0",
                            status = "error",
                            title = "Exception Error",
                            message = message
                        };
                    }
                }
            }
            #endregion

            #region User Info Update by Admin
            if (UserId > 0)
            {
                var _userExistCheck = _db.AdminModUsersDetail.Find(UserId);

                if (_userExistCheck != null)
                {
                    using var dbContextTransaction = _db.Database.BeginTransaction();

                    try
                    {
                        //_userExistCheck.UserRegistrationId = _userExistCheck.UserRegistrationId;                        
                        _userExistCheck.ApplicantTypeId = ApplicantTypeId;
                        _userExistCheck.ApplicantName = ApplicantName;
                        _userExistCheck.ApplicantNameBn = ApplicantNameBn;
                        _userExistCheck.OrganizationName = OrganizationName;
                        _userExistCheck.OrganizationNameBn = OrganizationNameBn;
                        _userExistCheck.OrganizationAddress = OrganizationAddress;
                        _userExistCheck.OrganizationAddressBn = OrganizationAddressBn;
                        _userExistCheck.UserProfession = UserProfession;
                        _userExistCheck.UserDesignation = UserDesignation;
                        _userExistCheck.UserNID = UserNID;
                        _userExistCheck.PostalAddress = PostalAddress;
                        _userExistCheck.PostalAddressBn = PostalAddressBn;
                        _userExistCheck.SecurityQuestionId = SecurityQuestionId;
                        _userExistCheck.SecurityQuestionAnswer = SecurityQuestionAnswer;
                        _userExistCheck.IsProfileSubmitted = _userExistCheck.IsProfileSubmitted;

                        _db.Entry(_userExistCheck).State = EntityState.Modified;
                        result = _db.SaveChanges();

                        if (result > 0)
                        {
                            if (result > 0)
                            {
                                if (file != null)
                                {
                                    noti = uunc(_user.UserId, "NidFileName", nidFile);

                                    if (noti.status == "success")
                                    {
                                        noti = uusf(_userExistCheck, "SignatureFileName", file);

                                        if (noti.status == "success")
                                        {
                                            dbContextTransaction.Commit();
                                            //UserInfo _ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
                                            //_ui.UserID = _userExistCheck.UserId;
                                            //HttpContext.Session.SetComplexData("LoggerUserInfo", _ui);

                                            noti = new Notification
                                            {
                                                id = _userExistCheck.UserId.ToString(),
                                                status = "success",
                                                title = "Success",
                                                message = "Profile information has been successfully updated and signature also uploaded."
                                            };
                                        }
                                    }
                                }
                                else
                                {
                                    dbContextTransaction.Commit();
                                    //UserInfo _ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
                                    //_ui.UserID = _userExistCheck.UserId;
                                    //HttpContext.Session.SetComplexData("LoggerUserInfo", _ui);

                                    noti = new Notification
                                    {
                                        id = _userExistCheck.UserId.ToString(),
                                        status = "success",
                                        title = "Success",
                                        message = "Profile information has been successfully updated."
                                    };
                                }
                            }
                            else
                            {
                                dbContextTransaction.Rollback();

                                noti = new Notification
                                {
                                    id = "0",
                                    status = "error",
                                    title = "Submission Error",
                                    message = "Sorry, profile information not update!"
                                };
                            }
                        }
                        else
                        {
                            dbContextTransaction.Rollback();

                            noti = new Notification
                            {
                                id = "0",
                                status = "error",
                                title = "Submission Error",
                                message = "Sorry, your profile information not submitted!"
                            };
                        }
                    }
                    catch (Exception ex)
                    {
                        dbContextTransaction.Rollback();
                        var message = ch.ExtractInnerException(ex);

                        noti = new Notification
                        {
                            id = "0",
                            status = "error",
                            title = "Exception Error",
                            message = message
                        };
                    }
                }
            }
            #endregion

            return Json(noti);
        }

        //account/UploadUserNidCard
        [Obsolete]
        public Notification uunc(long urid, string controltitle, IFormFile nid_file)
        {
            int result = 0;
            string filename = "", extension = "", foldername = "images/nid";
            AdminModUsersDetail ud = new AdminModUsersDetail();

            if (urid != 0)
            {
                ud = _db.AdminModUsersDetail.Find(urid);
            }

            try
            {
                if (nid_file != null)
                {
                    if (ud != null)
                    {
                        filename = ContentDispositionHeaderValue.Parse(nid_file.ContentDisposition).FileName.Trim('"');
                        extension = filename.Substring(filename.LastIndexOf('.'));
                        filename = EnsureCorrectFilename(filename);
                        filename = String.Format("{0}_{1}{2}", ud.UserId, "nid", extension);

                        ud.UserNIDFile = filename;
                        _db.Entry(ud).State = EntityState.Modified;
                        result = _db.SaveChanges();

                        if (result > 0)
                        {
                            using FileStream output = System.IO.File.Create(GetPathAndFilename(filename, foldername));
                            nid_file.CopyTo(output);

                            noti = new Notification
                            {
                                id = ud.UserId.ToString(),
                                status = "success",
                                title = "Success",
                                message = "Your NID has been successfully uploaded."
                            };
                        }
                        else
                        {
                            noti = new Notification
                            {
                                id = ud.UserId.ToString(),
                                status = "error",
                                title = "NID Upload Error",
                                message = "NID file has not uploaded."
                            };
                        }
                    }
                    else
                    {
                        noti = new Notification
                        {
                            id = "0",
                            status = "warning",
                            title = "Profile Error",
                            message = "Your profile has not submit yet!"
                        };
                    }
                }
                else
                {
                    noti = new Notification
                    {
                        id = "0",
                        status = "warning",
                        title = "Select File",
                        message = "No file(s) selected!"
                    };
                }
            }
            catch (Exception ex)
            {
                var message = ch.ExtractInnerException(ex);

                noti = new Notification
                {
                    id = "0",
                    status = "error",
                    title = "An Exception Error Occured",
                    message = message
                };
            }

            return noti;
        }

        [HttpPost]
        public JsonResult rupw(long urid, string uopw, string ucpw)
        {
            int result = 0;

            if (urid > 0)
            {
                var _userRegDetail = _db.AdminModUserRegistrationDetail.Find(urid);

                if (_userRegDetail != null)
                {
                    using var dbContextTransaction = _db.Database.BeginTransaction();

                    try
                    {
                        _userRegDetail.UserPassword = ucpw.EncryptString();

                        _db.Entry(_userRegDetail).State = EntityState.Modified;
                        result = _db.SaveChanges();

                        if (result > 0)
                        {
                            dbContextTransaction.Commit();

                            noti = new Notification
                            {
                                id = _userRegDetail.UserRegistrationId.ToString(),
                                status = "success",
                                title = "Success",
                                message = "User password has been changed successfully."
                            };
                        }
                        else
                        {
                            dbContextTransaction.Rollback();

                            noti = new Notification
                            {
                                id = "0",
                                status = "error",
                                title = "Password Change Error",
                                message = "Sorry, User password not changed!"
                            };
                        }
                    }
                    catch (Exception ex)
                    {
                        dbContextTransaction.Rollback();
                        var message = ch.ExtractInnerException(ex);

                        noti = new Notification
                        {
                            id = "0",
                            status = "error",
                            title = "Password Change Exception Error",
                            message = message
                        };
                    }
                }
            }

            return Json(noti);
        }

        [Obsolete]
        public Notification uusf(long urid, string controltitle, IFormFile file)
        {
            int result = 0;
            string filename = "", extension = "", foldername = "images/signature";
            AdminModUsersDetail ud = new AdminModUsersDetail();

            if (urid != 0)
            {
                ud = _db.AdminModUsersDetail.Find(urid);
            }

            try
            {
                if (file != null)
                {
                    if (ud != null)
                    {
                        filename = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                        extension = filename.Substring(filename.IndexOf('.'));
                        filename = EnsureCorrectFilename(filename);
                        filename = String.Format("{0}_{1}{2}", ud.UserId, "signature", extension);

                        ud.ApplicantSignature = filename;

                        _db.Entry(ud).State = EntityState.Modified;
                        result = _db.SaveChanges();

                        if (result > 0)
                        {
                            using FileStream output = System.IO.File.Create(GetPathAndFilename(filename, foldername));
                            file.CopyTo(output);

                            noti = new Notification
                            {
                                id = ud.UserId.ToString(),
                                status = "success",
                                title = "Success",
                                message = "Your signature has been successfully uploaded."
                            };
                        }
                        else
                        {
                            noti = new Notification
                            {
                                id = ud.UserId.ToString(),
                                status = "error",
                                title = "Signature Upload Error",
                                message = "Your selected file has not uploaded."
                            };
                        }
                    }
                    else
                    {
                        noti = new Notification
                        {
                            id = "0",
                            status = "warning",
                            title = "Profile Error",
                            message = "Your profile has not submit yet!"
                        };
                    }
                }
                else
                {
                    noti = new Notification
                    {
                        id = "0",
                        status = "warning",
                        title = "Select File",
                        message = "No file(s) selected!"
                    };
                }
            }
            catch (Exception ex)
            {
                var message = ch.ExtractInnerException(ex);

                noti = new Notification
                {
                    id = "0",
                    status = "error",
                    title = "An Exception Error Occured",
                    message = message
                };
            }

            return noti;
        }

        [Obsolete]
        public Notification uusf(AdminModUsersDetail ud, string controltitle, IFormFile file)
        {
            int result = 0;
            string filename = "", extension = "", foldername = "images/signature";

            try
            {
                if (file != null)
                {
                    if (ud != null)
                    {
                        filename = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                        extension = filename.Substring(filename.IndexOf('.'));
                        filename = EnsureCorrectFilename(filename);
                        filename = String.Format("{0}_{1}{2}", ud.UserId, "signature", extension);

                        ud.ApplicantSignature = filename;

                        _db.Entry(ud).State = EntityState.Modified;
                        result = _db.SaveChanges();

                        if (result > 0)
                        {
                            using FileStream output = System.IO.File.Create(GetPathAndFilename(filename, foldername));
                            file.CopyTo(output);

                            noti = new Notification
                            {
                                id = ud.UserId.ToString(),
                                status = "success",
                                title = "Success",
                                message = "Your signature has been successfully uploaded."
                            };
                        }
                        else
                        {
                            noti = new Notification
                            {
                                id = ud.UserId.ToString(),
                                status = "error",
                                title = "Signature Upload Error",
                                message = "Your selected file has not uploaded."
                            };
                        }
                    }
                    else
                    {
                        noti = new Notification
                        {
                            id = "0",
                            status = "warning",
                            title = "Profile Error",
                            message = "Your profile has not submit yet!"
                        };
                    }
                }
                else
                {
                    noti = new Notification
                    {
                        id = "0",
                        status = "warning",
                        title = "Select File",
                        message = "No file(s) selected!"
                    };
                }
            }
            catch (Exception ex)
            {
                var message = ch.ExtractInnerException(ex);

                noti = new Notification
                {
                    id = "0",
                    status = "error",
                    title = "An Exception Error Occured",
                    message = message
                };
            }

            return noti;
        }

        private string EnsureCorrectFilename(string filename)
        {
            if (filename.Contains("\\"))
                filename = filename.Substring(filename.LastIndexOf("\\") + 1);

            return filename;
        }

        private string GetCommonDetailFileName(string userId, string control_title)
        {
            string result = string.Empty;

            switch (control_title)
            {
                case "Higher_Auth_Signature":
                    result = userId + "_SIG_" + DateTime.Now.ToString("yyMMddHHmmssfff");
                    break;

                case "Higher_Auth_Seal":
                    result = userId + "_SEL_" + DateTime.Now.ToString("yyMMddHHmmssfff");
                    break;
            }

            return result;
        }

        private string GetPathAndFilename(string fileName, string folderName)
        {
            var path = Path.Combine(hostingEnvironment.WebRootPath, folderName, fileName);
            return path;
        }

        //account/recover
        public IActionResult recover()
        {
            return View();
        }

        //POST: /account/recover
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult recover([Bind("RecoverType,UserEmail")] RecoverUserIdPassword recover)
        {
            string UserName = string.Empty, UserPassword = string.Empty, recoverMessage = string.Empty, result = string.Empty;
            List<string> vars = new List<string>();

            if (ModelState.IsValid)
            {
                AdminModUserRegistrationDetail registrationDetail = _db.AdminModUserRegistrationDetail.Where(w => w.UserEmail == recover.UserEmail).FirstOrDefault();

                if (registrationDetail == null)
                {
                    TempData["Message"] = ch.ShowMessage(Sign.Danger, "Wrong Information", "Sorry, you have entered a wrong email address!");
                    return View(recover);
                }

                if (recover.RecoverType == "RecoverUserID")
                {
                    UserName = registrationDetail.UserName;
                    recoverMessage = @"user name. <br /><h2>User Name: <b>" + UserName + "<b></h2>";
                }
                else if (recover.RecoverType == "RecoverPassword")
                {
                    UserPassword = registrationDetail.UserPassword.DecryptString();
                    recoverMessage = @"password. <br /><h2>User Password: " + UserPassword + "<b></h2>";
                }
                else
                {
                    UserName = registrationDetail.UserName;
                    UserPassword = registrationDetail.UserPassword.DecryptString();

                    recoverMessage = @"user name and password. <br /><br /><h2>User Name: <b>" + UserName + "<b></h2>";
                    recoverMessage += @"<br /><h2>Password: " + UserPassword + "<b></h2>";
                }

                string callAt = cc.GetCallCenterInfo();

                vars.Add(recoverMessage);
                vars.Add(callAt);

                //3 => User ID Password Recovery in LookUpAdminModEmailFormat table
                result = es.SendEmail(recover.UserEmail, 3, vars);

                if (result == "success")
                {
                    TempData["Message"] = ch.ShowMessage(Sign.Success, "Success", "An email has been sent to your email address.");
                }
                else
                {
                    TempData["Message"] = ch.ShowMessage(Sign.Error, "An Error Occured", "Email not sent to your email address." + result);
                }
            }
            else
            {
                TempData["Message"] = ch.ShowMessage(Sign.Danger, "Required Fields", "Please select a recovery option and enter your registered email address!");
                return View(recover);
            }

            return View(recover);
        }

        //account/logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult logout()
        {
            HttpContext.Response.Cookies.Append("FormLanguage", string.Empty, new CookieOptions()
            {
                Expires = DateTime.Now.AddDays(-1)
            });

            HttpContext.Response.Cookies.Append("UserName", string.Empty, new CookieOptions()
            {
                Expires = DateTime.Now.AddDays(-1)
            });

            return RedirectToAction("login");
        }

        //account/help
        public IActionResult help()
        {
            return View();
        }

        //account/howtoapply
        public IActionResult howtoapply()
        {
            return View();
        }

        //account/howtoapply
        public IActionResult termcondition()
        {
            return View();
        }

        #region Signup Info Validation
        //account/dunc :: duplicate user name check
        [HttpGet]
        public ActionResult dunc(string un)
        {
            bool result = false;

            if (string.IsNullOrEmpty(un))
            {
                result = true;
                return Json(new { success = result, responseText = "Empty user name provided!" });
            }
            else
            {
                int count = _db.AdminModUserRegistrationDetail.Where(w => w.UserName.ToLower() == un.ToLower()).Count();

                if (count > 0)
                {
                    result = true;
                    return Json(new { result = result, response = "User name already used!" });
                }
                else
                {
                    result = false;
                    return Json(new { result = result, response = "User name is okay!" });
                }
            }
        }

        //account/duec :: duplicate user email check
        [HttpGet]
        public ActionResult duec(string ue)
        {
            bool result = false;

            if (string.IsNullOrEmpty(ue))
            {
                result = true;
                return Json(new { success = result, responseText = "Empty email provided!" });
            }
            if (IsValidEmail(ue))
            {
                result = true;
                return Json(new { success = result, responseText = "Invalid email address provided!" });
            }

            int count = _db.AdminModUserRegistrationDetail.Where(w => w.UserEmail.ToLower() == ue.ToLower()).Count();

            if (count > 0)
            {
                result = true;
                return Json(new { result = result, response = "User email already in used!" });
            }
            else
            {
                result = false;
                return Json(new { result = result, response = "User email is okay!" });
            }
        }

        //account/dumc :: duplicate user mobile check
        [HttpGet]
        public ActionResult dumc(string um)
        {
            bool result = false;

            if (string.IsNullOrEmpty(um))
            {
                result = true;
                return Json(new { success = result, responseText = "Empty mobile number provided!" });
            }
            if (IsValidMobile(um))
            {
                result = true;
                return Json(new { success = result, responseText = "Invalid mobile number provided!" });
            }

            int count = _db.AdminModUserRegistrationDetail.Where(w => w.UserMobile.ToLower() == um.ToLower()).Count();

            if (count > 0)
            {
                result = true;
                return Json(new { result = result, response = "User mobile number already in used!" });
            }
            else
            {
                result = false;
                return Json(new { result = result, response = "User mobile number is okay!" });
            }
        }

        public bool IsValidEmail(string emailaddress)
        {
            try
            {
                Regex regex = new Regex(@"^[\w-]+(\.[\w-]+)*@([a-z0-9-]+(\.[a-z0-9-]+)*?\.[a-z]{2,6}|(\d{1,3}\.){3}\d{1,3})(:\d{4})?$");
                Match match = regex.Match(emailaddress);
                if (match.Success)
                    return true;
                else
                    return false;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public bool IsValidMobile(string mobile)
        {
            try
            {
                Regex regex = new Regex(@"(^([+]{1}[8]{2}|0088)?(01){1}[3-9]{1}\d{8})$");
                Match match = regex.Match(mobile);
                if (match.Success)
                    return true;
                else
                    return false;
            }
            catch (FormatException)
            {
                return false;
            }
        }
        #endregion

        //[Route("get-captcha-image")]
        public IActionResult GetCaptchaImage()
        {
            int width = 120;
            int height = 36;
            var captchaCode = Captcha.GenerateCaptchaCode();
            var result = Captcha.GenerateCaptchaImage(width, height, captchaCode);
            HttpContext.Session.SetString("CaptchaCode", result.CaptchaCode);
            Stream s = new MemoryStream(result.CaptchaByteData);
            return new FileStreamResult(s, "image/png");
        }

        public IActionResult viewUsers()
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
            UserLevelInfo uli = HttpContext.Session.GetComplexData<UserLevelInfo>("UserLevelInfo");
            List<UserInfo> userDetails = new List<UserInfo>();

            userDetails = _db.AdminModUsersDetail.Join(_db.AdminModUserRegistrationDetail, u => u.UserRegistrationId, r => r.UserRegistrationId, (u, r) => new { R = r, U = u })
                             .Select(s => new UserInfo
                             {
                                 UserID = s.U.UserId,
                                 UserName = s.R.UserName,
                                 UserFullName = s.U.ApplicantTypeId == 1 ? s.U.ApplicantName : s.U.OrganizationName,
                                 UserDesignation = s.U.UserDesignation,
                                 UserEmail = s.R.UserEmail,
                                 UserMobile = s.R.UserMobile,
                                 UserActivationStatus = s.R.UserActivationStatus,
                                 IsDeleted = s.R.IsDeleted
                             }).ToList();

            if (!string.IsNullOrEmpty(uli.UnionGeoCode))
            {
                //userDetails = (from ud in userDetails.ToList()
                //               join ugdd in _db.AdminModUserGrpDistDetail on ud.UserID equals ugdd.UserId into userGrpDistDetailGroup
                //               from ugd in userGrpDistDetailGroup.DefaultIfEmpty()
                //               join uGroup in _db.LookUpAdminModUserGroup on ugd.UserGroupId equals uGroup.UserGroupId into userGroup
                //               from ug in userGroup.DefaultIfEmpty()

                //               where ug.UnionGeoCode == uli.UnionGeoCode

                //               select new UserInfo
                //               {
                //                   UserID = ud.UserID,
                //                   UserName = ud.UserName,
                //                   UserFullName = ud.UserFullName,
                //                   UserDesignation = ud.UserDesignation,
                //                   UserEmail = ud.UserEmail,
                //                   UserMobile = ud.UserMobile,
                //                   UserActivationStatus = ud.UserActivationStatus,
                //                   IsDeleted = ud.IsDeleted
                //               }).ToList();
                userDetails = (from ugdd in _db.AdminModUserGrpDistDetail.ToList()
                               join udtl in _db.AdminModUsersDetail on ugdd.UserId equals udtl.UserId into userDetailGroup
                               from ud in userDetailGroup.DefaultIfEmpty()
                               join uGroup in _db.LookUpAdminModUserGroup on ugdd.UserGroupId equals uGroup.UserGroupId into userGroup
                               from ug in userGroup.DefaultIfEmpty()
                               join uReg in _db.AdminModUserRegistrationDetail on ud.UserRegistrationId equals uReg.UserRegistrationId into userReg
                               from ur in userReg.DefaultIfEmpty()

                               where ug.UnionGeoCode == uli.UnionGeoCode &&
                               (ug.AuthorityLevelId == 5 || ug.AuthorityLevelId == 6 || ug.AuthorityLevelId == 9 || ug.AuthorityLevelId == 10)

                               select new UserInfo
                               {
                                   UserID = ud.UserId,
                                   UserName = ur.UserName,
                                   //UserFullName = ud.UserFullName,
                                   UserFullName = ud.ApplicantTypeId == 1 ? ud.ApplicantName : ud.OrganizationName,
                                   UserDesignation = ud.UserDesignation,
                                   UserEmail = ur.UserEmail,
                                   UserMobile = ur.UserMobile,
                                   UserActivationStatus = ur.UserActivationStatus,
                                   IsDeleted = ur.IsDeleted
                               }).ToList();
            }

            if (!string.IsNullOrEmpty(uli.UpazilaGeoCode) && string.IsNullOrEmpty(uli.UnionGeoCode))
            {
                //userDetails = (from ud in userDetails.ToList()
                //               join ugdd in _db.AdminModUserGrpDistDetail on ud.UserID equals ugdd.UserId into userGrpDistDetailGroup
                //               from ugd in userGrpDistDetailGroup.DefaultIfEmpty()
                //               join uGroup in _db.LookUpAdminModUserGroup on ugd.UserGroupId equals uGroup.UserGroupId into userGroup
                //               from ug in userGroup.DefaultIfEmpty()

                //               where ug.UpazilaGeoCode == uli.UpazilaGeoCode && ug.UnionGeoCode == string.Empty

                //               select new UserInfo
                //               {
                //                   UserID = ud.UserID,
                //                   UserName = ud.UserName,
                //                   UserFullName = ud.UserFullName,
                //                   UserDesignation = ud.UserDesignation,
                //                   UserEmail = ud.UserEmail,
                //                   UserMobile = ud.UserMobile,
                //                   UserActivationStatus = ud.UserActivationStatus,
                //                   IsDeleted = ud.IsDeleted
                //               }).ToList();
                userDetails = (from ugdd in _db.AdminModUserGrpDistDetail.ToList()
                               join udtl in _db.AdminModUsersDetail on ugdd.UserId equals udtl.UserId into userDetailGroup
                               from ud in userDetailGroup.DefaultIfEmpty()
                               join uGroup in _db.LookUpAdminModUserGroup on ugdd.UserGroupId equals uGroup.UserGroupId into userGroup
                               from ug in userGroup.DefaultIfEmpty()
                               join uReg in _db.AdminModUserRegistrationDetail on ud.UserRegistrationId equals uReg.UserRegistrationId into userReg
                               from ur in userReg.DefaultIfEmpty()

                               where ug.UpazilaGeoCode == uli.UpazilaGeoCode &&
                               (ug.AuthorityLevelId == 3 || ug.AuthorityLevelId == 4 || ug.AuthorityLevelId == 8 || ug.AuthorityLevelId == 11)

                               select new UserInfo
                               {
                                   UserID = ud.UserId,
                                   UserName = ur.UserName,
                                   //UserFullName = ud.UserFullName,
                                   UserFullName = ud.ApplicantTypeId == 1 ? ud.ApplicantName : ud.OrganizationName,
                                   UserDesignation = ud.UserDesignation,
                                   UserEmail = ur.UserEmail,
                                   UserMobile = ur.UserMobile,
                                   UserActivationStatus = ur.UserActivationStatus,
                                   IsDeleted = ur.IsDeleted
                               }).ToList();
            }

            if (!string.IsNullOrEmpty(uli.DistrictGeoCode) && string.IsNullOrEmpty(uli.UpazilaGeoCode) && string.IsNullOrEmpty(uli.UnionGeoCode))
            {
                //userDetails = (from ud in userDetails.ToList()
                //               join ugdd in _db.AdminModUserGrpDistDetail on ud.UserID equals ugdd.UserId into userGrpDistDetailGroup
                //               from ugd in userGrpDistDetailGroup.DefaultIfEmpty()
                //               join uGroup in _db.LookUpAdminModUserGroup on ugd.UserGroupId equals uGroup.UserGroupId into userGroup
                //               from ug in userGroup.DefaultIfEmpty()

                //               where ug.DistrictGeoCode == uli.DistrictGeoCode &&
                //               (ug.AuthorityLevelId <= uli.AuthorityLevelId || ug.AuthorityLevelId == 12)

                //               select new UserInfo
                //               {
                //                   UserID = ud.UserID,
                //                   UserName = ud.UserName,
                //                   UserFullName = ud.UserFullName,
                //                   UserDesignation = ud.UserDesignation,
                //                   UserEmail = ud.UserEmail,
                //                   UserMobile = ud.UserMobile,
                //                   UserActivationStatus = ud.UserActivationStatus,
                //                   IsDeleted = ud.IsDeleted
                //               }).ToList();

                userDetails = (from ugdd in _db.AdminModUserGrpDistDetail.ToList()
                               join udtl in _db.AdminModUsersDetail on ugdd.UserId equals udtl.UserId into userDetailGroup
                               from ud in userDetailGroup.DefaultIfEmpty()
                               join uGroup in _db.LookUpAdminModUserGroup on ugdd.UserGroupId equals uGroup.UserGroupId into userGroup
                               from ug in userGroup.DefaultIfEmpty()
                               join uReg in _db.AdminModUserRegistrationDetail on ud.UserRegistrationId equals uReg.UserRegistrationId into userReg
                               from ur in userReg.DefaultIfEmpty()

                               where ug.DistrictGeoCode == uli.DistrictGeoCode &&
                               (ug.AuthorityLevelId == 1 || ug.AuthorityLevelId == 2 || ug.AuthorityLevelId == 7 || ug.AuthorityLevelId == 12)

                               select new UserInfo
                               {
                                   UserID = ud.UserId,
                                   UserName = ur.UserName,
                                   //UserFullName = ud.UserFullName,
                                   UserFullName = ud.ApplicantTypeId == 1 ? ud.ApplicantName : ud.OrganizationName,
                                   UserDesignation = ud.UserDesignation,
                                   UserEmail = ur.UserEmail,
                                   UserMobile = ur.UserMobile,
                                   UserActivationStatus = ur.UserActivationStatus,
                                   IsDeleted = ur.IsDeleted
                               }).ToList();
            }

            ViewData["UserDetails"] = userDetails.Where(w => w.IsDeleted == 0).ToList();
            return View();
        }

        public IActionResult createuser()
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
            UserLevelInfo uli = HttpContext.Session.GetComplexData<UserLevelInfo>("UserLevelInfo");

            ViewBag.IsWarpoUser = uli.UserGroupName.Contains("WARPO") ? "warpo" : "non-warpo";

            //where ug.DistrictGeoCode == uli.DistrictGeoCode &&  (ug.AuthorityLevelId <= uli.AuthorityLevelId || ug.AuthorityLevelId == 12)
            if (!string.IsNullOrEmpty(uli.UnionGeoCode))
            {
                ViewBag.LookUpAdminModUserGroup = _db.LookUpAdminModUserGroup
                                                    .Where(w => w.UnionGeoCode == uli.UnionGeoCode &&
                                                     ((w.AuthorityLevelId == 5 || w.AuthorityLevelId == 6 || w.AuthorityLevelId == 9 || w.AuthorityLevelId == 10)))
                                                    .OrderBy(o => o.UserGroupId).ToList();
            }

            if (!string.IsNullOrEmpty(uli.UpazilaGeoCode) && string.IsNullOrEmpty(uli.UnionGeoCode))
            {
                ViewBag.LookUpAdminModUserGroup = _db.LookUpAdminModUserGroup
                                                    .Where(w => w.UpazilaGeoCode == uli.UpazilaGeoCode &&
                                                     ((w.AuthorityLevelId == 3 || w.AuthorityLevelId == 4 || w.AuthorityLevelId == 8 || w.AuthorityLevelId == 11)))
                                                    .OrderBy(o => o.UserGroupId).ToList();
            }

            if (!string.IsNullOrEmpty(uli.DistrictGeoCode) && string.IsNullOrEmpty(uli.UpazilaGeoCode) && string.IsNullOrEmpty(uli.UnionGeoCode))
            {
                ViewBag.LookUpAdminModUserGroup = _db.LookUpAdminModUserGroup
                                                    .Where(w => w.DistrictGeoCode == uli.DistrictGeoCode &&
                                                     ((w.AuthorityLevelId == 1 || w.AuthorityLevelId == 2 || w.AuthorityLevelId == 7 || w.AuthorityLevelId == 12)))
                                                    .OrderBy(o => o.UserGroupId).ToList();
            }

            if (uli.UserGroupId == 1000000002 || uli.UserGroupId == 1000000003 || uli.UserGroupId == 1000000004 || uli.UserGroupId == 1000000005 || uli.UserGroupId == 1000000006)
            {
                ViewBag.LookUpAdminModUserGroup = _db.LookUpAdminModUserGroup
                                                     .OrderBy(o => o.UserGroupId).ToList();
            }

            ViewBag.SecurityQuestionId = new SelectList(_db.LookUpAdminModSecurityQuestion.ToList(), "SecurityQuestionId", "SecurityQuestion");
            return View();
        }

        //POST: /account/createUser
        [HttpPost]
        public IActionResult createuser(AdminNewUserCreation anuc)
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
            UserLevelInfo uli = HttpContext.Session.GetComplexData<UserLevelInfo>("UserLevelInfo");

            var files = Request.Form.Files;
            string filename = "", extension = "", signaturefoldername = "images/higher_auth_signature", sealfoldername = "images/higher_auth_seal";

            using var dbContextTransaction = _db.Database.BeginTransaction();
            try
            {
                AdminModUserRegistrationDetail userReg = new AdminModUserRegistrationDetail
                {
                    UserName = anuc.UserName,
                    UserPassword = anuc.UserPassword.EncryptString(),
                    UserActivationStatus = 1,
                    UserEmail = anuc.UserEmail,
                    UserMobile = anuc.UserMobile,
                    DateOfCreation = DateTime.Now,
                    EmailVerificationCode = Guid.NewGuid().ToString(),
                    IsEmailVerified = 1,
                    IsDeleted = 0
                };

                _db.AdminModUserRegistrationDetail.Add(userReg);
                int x = _db.SaveChanges();

                if (x > 0)
                {
                    AdminModUsersDetail amud = new AdminModUsersDetail
                    {
                        UserRegistrationId = userReg.UserRegistrationId,

                        //rony :: need to work here
                        //UserFullName = anuc.FullName,
                        //UserFullName = ud.ApplicantTypeId == 1 ? ud.ApplicantName : ud.OrganizationName,
                        //UserFatherName = "empty",
                        //UserDateOfBirth = DateTime.Now,
                        //UserAddress = anuc.Address,

                        ApplicantTypeId = 1,
                        ApplicantName = anuc.FullName,
                        ApplicantNameBn = anuc.FullNameBn,
                        UserDesignation = anuc.Designation,
                        PostalAddress = anuc.Address,
                        PostalAddressBn = anuc.AddressBn,
                        SecurityQuestionId = anuc.SecurityQuestionId.ToInt(),
                        SecurityQuestionAnswer = anuc.SecurityQuestionAnswer,
                        IsProfileSubmitted = 1
                    };

                    x = 0;
                    _db.AdminModUsersDetail.Add(amud);
                    x = _db.SaveChanges();

                    if (x > 0)
                    {
                        AdminModUserGrpDistDetail amugdd = new AdminModUserGrpDistDetail
                        {
                            UserId = amud.UserId,
                            UserGroupId = anuc.UserGroup.ToLong()
                        };

                        x = 0;
                        _db.AdminModUserGrpDistDetail.Add(amugdd);
                        x = _db.SaveChanges();

                        if (x > 0)
                        {
                            if (files.Count > 0)
                            {
                                foreach (var file in files)
                                {
                                    filename = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                                    extension = filename.Substring(filename.IndexOf('.'));
                                    filename = EnsureCorrectFilename(filename);

                                    if (file.Name == "SignatureFileName")
                                    {
                                        filename = GetCommonDetailFileName(amud.UserId.ToString(), "Higher_Auth_Signature") + extension;
                                        amud.HigherAuthSignature = filename;
                                    }

                                    if (file.Name == "SealFileName")
                                    {
                                        filename = GetCommonDetailFileName(amud.UserId.ToString(), "Higher_Auth_Seal") + extension;
                                        amud.HigherAuthSeal = filename;
                                    }
                                    //size += file.Length;

                                    _db.Entry(amud).State = EntityState.Modified;
                                    x = _db.SaveChanges();

                                    if (x > 0)
                                    {
                                        if (file.Name == "SignatureFileName")
                                        {
                                            using FileStream outputSignature = System.IO.File.Create(GetPathAndFilename(filename, signaturefoldername));
                                            file.CopyTo(outputSignature);
                                        }

                                        if (file.Name == "SealFileName")
                                        {
                                            using FileStream outputSeal = System.IO.File.Create(GetPathAndFilename(filename, sealfoldername));
                                            file.CopyTo(outputSeal);
                                        }
                                    }
                                }
                            }

                            dbContextTransaction.Commit();

                            string successMsg = "User Name: " + anuc.UserName + ". User Registration ID: " + userReg.UserRegistrationId + ". User Group ID: " + amugdd.UserGroupId;
                            TempData["Message"] = ch.ShowMessage(Sign.Success, Sign.Success.ToString(), "Successfully created new user and user information is given below.<br />" + successMsg);
                            return RedirectToAction("viewusers", "account");
                        }
                        else
                        {
                            dbContextTransaction.Rollback();
                            TempData["Message"] = ch.ShowMessage(Sign.Error, Sign.Error.ToString(), "There was an error occured to give user group distribution!");
                        }
                    }
                    else
                    {
                        dbContextTransaction.Rollback();
                        TempData["Message"] = ch.ShowMessage(Sign.Error, Sign.Error.ToString(), "There was an error occured to auto create new user profile!");
                    }
                }
                else
                {
                    dbContextTransaction.Rollback();
                    TempData["Message"] = ch.ShowMessage(Sign.Error, Sign.Error.ToString(), "There was an error occured to create new user registration!");
                }
            }
            catch (Exception ex)
            {
                dbContextTransaction.Rollback();

                var message = ch.ExtractInnerException(ex);
                TempData["Message"] = ch.ShowMessage(Sign.Danger, Sign.Danger.ToString(), message);
            }

            ViewBag.IsWarpoUser = uli.UserGroupName.Contains("WARPO") ? "warpo" : "non-warpo";
            ViewBag.LookUpAdminModUserGroup = _db.LookUpAdminModUserGroup.OrderBy(o => o.UserGroupId).ToList();
            ViewBag.SecurityQuestionId = new SelectList(_db.LookUpAdminModSecurityQuestion.ToList(), "SecurityQuestionId", "SecurityQuestion", anuc.SecurityQuestionId);
            return View();
        }

        public IActionResult userDetails(long id)
        {
            UserInfo userDetails = _db.AdminModUsersDetail.Join(_db.AdminModUserRegistrationDetail, u => u.UserRegistrationId, r => r.UserRegistrationId, (u, r) => new { R = r, U = u })
                                         .Where(w => w.U.UserId == id)
                                         .Select(s => new UserInfo
                                         {
                                             UserID = s.U.UserId,
                                             UserName = s.R.UserName,
                                             //UserFullName = s.U.UserFullName,
                                             UserFullName = s.U.ApplicantTypeId == 1 ? s.U.ApplicantName : s.U.OrganizationName,
                                             UserDesignation = s.U.UserDesignation,
                                             UserEmail = s.R.UserEmail,
                                             UserMobile = s.R.UserMobile,
                                             //UserAddress = s.U.UserAddress,
                                             UserAddress = s.U.PostalAddress,
                                             UserActivationStatus = s.R.UserActivationStatus,
                                             DateOfCreation = s.R.DateOfCreation,
                                             LastModifiedDate = s.R.LastModifiedDate
                                         }).FirstOrDefault();

            ViewData["UserDetails"] = userDetails;
            return View();
        }

        public IActionResult editUserProfile(long id)
        {
            AdminModUsersDetail userDetails = _db.AdminModUsersDetail.Where(w => w.UserId == id).FirstOrDefault();

            ViewBag.ApplicantTypeId = new SelectList(_db.LookUpCcModApplicantType.ToList(), "ApplicantTypeId", "ApplicantType", userDetails.ApplicantTypeId);
            ViewBag.SecurityQuestionId = new SelectList(_db.LookUpAdminModSecurityQuestion.ToList(), "SecurityQuestionId", "SecurityQuestion", userDetails.SecurityQuestionId);
            //ViewBag.UpdateUserInfo = userDetails;
            return View(userDetails);
        }

        public IActionResult resetPassword(long id)
        {
            AdminModUsersDetail userDetails = _db.AdminModUsersDetail.Find(id);
            ViewBag.UpdateUserInfo = userDetails;

            return View();
        }

        public IActionResult makeActiveInactive(long id)
        {
            int result = 0;
            string msg = string.Empty;
            AdminModUsersDetail userDetails = _db.AdminModUsersDetail.Find(id);
            AdminModUserRegistrationDetail userRegDetails = _db.AdminModUserRegistrationDetail.Find(userDetails.UserRegistrationId);

            //ViewBag.UserRegDetails = userRegDetails;
            using var dbContextTransaction = _db.Database.BeginTransaction();

            try
            {
                if (userRegDetails.UserActivationStatus == 1)
                {
                    userRegDetails.UserActivationStatus = 0;
                    msg = "User " + userRegDetails.UserName + " has been inactivated successfully!";
                }
                else
                {
                    userRegDetails.UserActivationStatus = 1;
                    msg = "User " + userRegDetails.UserName + " has been activated successfully!";
                }

                _db.Entry(userRegDetails).State = EntityState.Modified;
                result = _db.SaveChanges();

                if (result > 0)
                {
                    dbContextTransaction.Commit();
                    TempData["Message"] = ch.ShowMessage(Sign.Success, "Active/ Inactive Status", msg);
                }
                else
                {
                    dbContextTransaction.Rollback();
                    TempData["Message"] = ch.ShowMessage(Sign.Error, "Active/ Inactive Error", "An error occured!");
                }
            }
            catch (Exception ex)
            {
                dbContextTransaction.Rollback();
                TempData["Message"] = ch.ShowMessage(Sign.Error, "Active/ Inactive Error", ch.ExtractInnerException(ex));
            }

            return RedirectToAction("viewusers", "account");
        }

        public IActionResult deleteUser(long id)
        {
            int result = 0;
            string msg = string.Empty;
            AdminModUsersDetail userDetails = _db.AdminModUsersDetail.Find(id);
            AdminModUserRegistrationDetail userRegDetails = _db.AdminModUserRegistrationDetail.Find(userDetails.UserRegistrationId);

            using var dbContextTransaction = _db.Database.BeginTransaction();

            try
            {
                userRegDetails.IsDeleted = 1;

                _db.Entry(userRegDetails).State = EntityState.Modified;
                result = _db.SaveChanges();

                if (result > 0)
                {
                    dbContextTransaction.Commit();
                    TempData["Message"] = ch.ShowMessage(Sign.Success, "Deletion Status", "User " + userRegDetails.UserName + " has been deleted!");
                }
                else
                {
                    dbContextTransaction.Rollback();
                    TempData["Message"] = ch.ShowMessage(Sign.Error, "Deletion Status", "An error occured, user not deleted");
                }
            }
            catch (Exception ex)
            {
                dbContextTransaction.Rollback();
                TempData["Message"] = ch.ShowMessage(Sign.Error, "User Deletion Exception Error", ch.ExtractInnerException(ex));
            }

            return RedirectToAction("viewusers", "account");
        }

        public IActionResult viewUserGroups()
        {
            /* 
              SELECT  a."UserGroupId", a."UserGroupName", 
                    a."AuthorityLevelId", a."DistrictGeoCode", 
                    a."UpazilaGeoCode", a."UnionGeoCode", 
                    b."AuthorityLevel", c."DistrictName", 
                    d."UpazilaName", e."UnionName"
            FROM (((ARH."LookUpAdminModUserGroup" a
                    LEFT JOIN ARH."LookUpAdminModAuthorityLevel" b
                        ON a."AuthorityLevelId"= b."AuthorityLevelId") 
                    LEFT JOIN ARH."LookUpAdminBndDistrict" c
                        ON a."DistrictGeoCode"	 = c."DistrictGeoCode") 
                    LEFT JOIN ARH."LookUpAdminBndUpazila" d
                        ON a."UpazilaGeoCode" = d."UpazilaGeoCode") 
                    LEFT JOIN ARH."LookUpAdminBndUnion" e
                        ON a."UnionGeoCode" = e."UnionGeoCode"; */

            List<UserGroupInfo> userGroupDetails = (from g in _db.LookUpAdminModUserGroup
                                                    join auth_level in _db.LookUpAdminModAuthorityLevel on g.AuthorityLevelId equals auth_level.AuthorityLevelId into auth
                                                    from al in auth.DefaultIfEmpty()
                                                    join district in _db.LookUpAdminBndDistrict on g.DistrictGeoCode equals district.DistrictGeoCode into dist
                                                    from ds in dist.DefaultIfEmpty()
                                                    join upazila in _db.LookUpAdminBndUpazila on g.UpazilaGeoCode equals upazila.UpazilaGeoCode into upaz
                                                    from up in upaz.DefaultIfEmpty()
                                                    join union in _db.LookUpAdminBndUnion on g.UnionGeoCode equals union.UnionGeoCode into unio
                                                    from un in unio.DefaultIfEmpty()
                                                    select new UserGroupInfo
                                                    {
                                                        UserGroupId = g.UserGroupId,
                                                        UserGroupName = g.UserGroupName,
                                                        AuthorityLevel = al.AuthorityLevel,
                                                        DistrictName = ds.DistrictName,
                                                        UpazilaName = up.UpazilaName,
                                                        UnionName = un.UnionName
                                                    }).OrderBy(o => o.UserGroupId).ToList();

            ViewData["UserGroupDetails"] = userGroupDetails;
            return View();
        }

        public IActionResult createusergroup()
        {
            ViewBag.AdminModAuthorityLevel = _db.LookUpAdminModAuthorityLevel.OrderBy(o => o.AuthorityLevelId).ToList();
            ViewBag.LookUpAdminBndDistrict = _db.LookUpAdminBndDistrict.OrderBy(o => o.DistrictName).ToList();
            return View();
        }

        //POST: /account/createUser
        [HttpPost]
        public IActionResult createusergroup(LookUpAdminModUserGroup amug)
        {
            int count = 0;
            string UserGroupId = string.Empty,
                   //highestGeoCode = string.Empty,
                   DistrictGeoCode = amug.DistrictGeoCode,
                   UpazilaGeoCode = amug.UpazilaGeoCode,
                   UnionGeoCode = amug.UnionGeoCode;

            //string highestGeoCode = !string.IsNullOrEmpty(UnionGeoCode) ? UnionGeoCode : !string.IsNullOrEmpty(UpazilaGeoCode) ? UpazilaGeoCode : !string.IsNullOrEmpty(DistrictGeoCode) ? DistrictGeoCode : string.Empty;

            #region checking duplicate user group
            if (!string.IsNullOrEmpty(UnionGeoCode))
            {
                var exitsGroupUnion = _db.LookUpAdminModUserGroup.Where(w => w.UnionGeoCode == UnionGeoCode).ToList();

                //if (exitsGroupUnion.Count == 2)
                //{
                //    TempData["Message"] = ch.ShowMessage(Sign.Warning, Sign.Warning.ToString(), "All authority level user group already created under this union.");
                //    goto Final;
                //}
                //else
                //{
                count = exitsGroupUnion.Where(w => w.AuthorityLevelId == amug.AuthorityLevelId).Count();

                if (count > 0)
                {
                    TempData["Message"] = ch.ShowMessage(Sign.Warning, Sign.Warning.ToString(), "User group of this authority level already exists.");
                    goto Final;
                }
                else
                {
                    UserGroupId = UnionGeoCode + (exitsGroupUnion.Count() + 1).ToString().PadLeft(2, '0');
                    goto DatabaseOperation;
                }
                //}
            }

            if (!string.IsNullOrEmpty(UpazilaGeoCode))
            {
                var exitsGroupUpazila = _db.LookUpAdminModUserGroup.Where(w => w.UpazilaGeoCode == UpazilaGeoCode).ToList();

                //if (exitsGroupUpazila.Count == 2)
                //{
                //    TempData["Message"] = ch.ShowMessage(Sign.Warning, Sign.Warning.ToString(), "All authority level user group already created under this upazila.");
                //    goto Final;
                //}
                //else
                //{
                count = exitsGroupUpazila.Where(w => w.AuthorityLevelId == amug.AuthorityLevelId).Count();

                if (count > 0)
                {
                    TempData["Message"] = ch.ShowMessage(Sign.Warning, Sign.Warning.ToString(), "User group of this authority level already exists.");
                    goto Final;
                }
                else
                {
                    UserGroupId = UpazilaGeoCode + (exitsGroupUpazila.Count() + 1).ToString().PadLeft(4, '0');
                    goto DatabaseOperation;
                }
                //}
            }

            if (!string.IsNullOrEmpty(DistrictGeoCode))
            {
                var exitsGroupDsitrict = _db.LookUpAdminModUserGroup.Where(w => w.DistrictGeoCode == DistrictGeoCode).ToList();

                //if (exitsGroupDsitrict.Count == 2)
                //{
                //    TempData["Message"] = ch.ShowMessage(Sign.Warning, Sign.Warning.ToString(), "All authority level user group already created under this district.");
                //    goto Final;
                //}
                //else
                //{
                count = exitsGroupDsitrict.Where(w => w.AuthorityLevelId == amug.AuthorityLevelId).Count();

                if (count > 0)
                {
                    TempData["Message"] = ch.ShowMessage(Sign.Warning, Sign.Warning.ToString(), "User group of this authority level already exists.");
                    goto Final;
                }
                else
                {
                    UserGroupId = DistrictGeoCode + (exitsGroupDsitrict.Count() + 1).ToString().PadLeft(6, '0');
                    goto DatabaseOperation;
                }
                //}
            }
        #endregion

        DatabaseOperation:
            try
            {
                amug.UserGroupId = UserGroupId.ToLong();
                _db.LookUpAdminModUserGroup.Add(amug);
                int x = _db.SaveChanges();

                if (x > 0)
                {
                    TempData["Message"] = ch.ShowMessage(Sign.Success, Sign.Success.ToString(), "User group has been created successfully.");
                    return RedirectToAction("viewusergroups", "account");
                }
                else
                {
                    TempData["Message"] = ch.ShowMessage(Sign.Error, Sign.Error.ToString(), "Something went wrong. Sorry, User group not created.");
                    goto Final;
                }

            }
            catch (Exception ex)
            {
                ViewBag.UserVerificationCode = string.Empty;

                var message = ch.ExtractInnerException(ex);
                TempData["Message"] = ch.ShowMessage(Sign.Danger, Sign.Danger.ToString(), message);
            }

        Final:
            ViewBag.AdminModAuthorityLevel = _db.LookUpAdminModAuthorityLevel.OrderBy(o => o.AuthorityLevelId).ToList();
            ViewBag.LookUpAdminBndDistrict = _db.LookUpAdminBndDistrict.OrderBy(o => o.DistrictName).ToList();
            return View();
        }

        public IActionResult userGroupDetails(long userGroupId)
        {
            UserGroupInfo userGroupDetails = (from g in _db.LookUpAdminModUserGroup
                                              join auth_level in _db.LookUpAdminModAuthorityLevel on g.AuthorityLevelId equals auth_level.AuthorityLevelId into auth
                                              from al in auth.DefaultIfEmpty()
                                              join district in _db.LookUpAdminBndDistrict on g.DistrictGeoCode equals district.DistrictGeoCode into dist
                                              from ds in dist.DefaultIfEmpty()
                                              join upazila in _db.LookUpAdminBndUpazila on g.UpazilaGeoCode equals upazila.UpazilaGeoCode into upaz
                                              from up in upaz.DefaultIfEmpty()
                                              join union in _db.LookUpAdminBndUnion on g.UnionGeoCode equals union.UnionGeoCode into unio
                                              from un in unio.DefaultIfEmpty()
                                              where g.UserGroupId != null && g.UserGroupId == userGroupId
                                              select new UserGroupInfo
                                              {
                                                  UserGroupId = g.UserGroupId,
                                                  UserGroupName = g.UserGroupName,
                                                  AuthorityLevel = al.AuthorityLevel,
                                                  DistrictName = ds.DistrictName,
                                                  UpazilaName = up.UpazilaName,
                                                  UnionName = un.UnionName,
                                                  DistrictGeoCode = ds.DistrictGeoCode,
                                                  UpazilaGeoCode = up.UpazilaGeoCode,
                                                  UnionGeoCode = un.UnionGeoCode,
                                                  CanViewOneList = g.CanViewOneList,
                                                  CanViewMultipleList = g.CanViewMultipleList,
                                                  CanInsertOne = g.CanInsertOne,
                                                  CanInsertMultiple = g.CanInsertMultiple,
                                                  CanUpdateOne = g.CanUpdateOne,
                                                  CanUpdateMultiple = g.CanUpdateMultiple,
                                                  CanViewAsDetails = g.CanViewAsDetails,
                                                  CanDeleteOne = g.CanDeleteOne,
                                                  CanDeleteMultiple = g.CanDeleteMultiple
                                              }).FirstOrDefault();

            ViewData["UserGroupDetails"] = userGroupDetails;
            return View();
        }

        public IActionResult editUserGroup()
        {
            return View();
        }

        public IActionResult deleteUserGroup()
        {
            return View();
        }

        //POST: /account/changepassword
        public IActionResult changepassword()
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
            ViewBag.UserName = ui.UserName;
            ViewBag.UserRegistrationID = ui.UserRegistrationID;
            //ViewBag.SecurityQuestionId = new SelectList(_db.LookUpAdminModSecurityQuestion.ToList(), "SecurityQuestionId", "SecurityQuestion");

            return View();
        }

        //account/login
        [HttpPost]
        public IActionResult changepassword(ChangePassword _cp)
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
            if (ui == null)
            {
                TempData["Message"] = ch.ShowMessage(Sign.Error, "Unauthorized User", "Unauthorized user found!");
                return RedirectToAction("login");
            }

            AdminModUserRegistrationDetail urd = _db.AdminModUserRegistrationDetail.Find(ui.UserRegistrationID);
            if (urd == null)
            {
                TempData["Message"] = ch.ShowMessage(Sign.Error, "Unauthorized User", "Unauthorized user found!");
                return RedirectToAction("login");
            }

            try
            {
                if (ModelState.IsValid)
                {
                    string currentUserDbPassword = _cp.CurrentPassword.EncryptString();

                    if (currentUserDbPassword != urd.UserPassword)
                    {
                        TempData["Message"] = ch.ShowMessage(Sign.Error, "Mismatched Password", "Current password not matched!");
                        goto EndMethod;
                    }

                    if (_cp.NewPassword != _cp.ConfirmNewPassword)
                    {
                        TempData["Message"] = ch.ShowMessage(Sign.Error, "Mismatched Password", "New password and confirm password not matched!");
                        goto EndMethod;
                    }

                    urd.UserPassword = _cp.ConfirmNewPassword.EncryptString();

                    _db.Entry(urd).State = EntityState.Modified;
                    int x = _db.SaveChanges();

                    if (x > 0)
                    {
                        TempData["Message"] = ch.ShowMessage(Sign.Success, "Password Changed", "Password has been changed successfully.");
                    }
                    else
                    {
                        TempData["Message"] = ch.ShowMessage(Sign.Error, "Password Not Changed", "Password not changed!");
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.UserVerificationCode = string.Empty;

                var message = ch.ExtractInnerException(ex);
                TempData["Message"] = ch.ShowMessage(Sign.Danger, message);
            }

        EndMethod:
            ViewBag.UserName = ui.UserName;
            ViewBag.UserRegistrationID = ui.UserRegistrationID;
            //ViewBag.SecurityQuestionId = new SelectList(_db.LookUpAdminModSecurityQuestion.ToList(), "SecurityQuestionId", "SecurityQuestion");

            return View();
        }

        //POST: /account/userlog
        public IActionResult userlog()
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
            ViewBag.UserName = ui.UserName;
            ViewBag.UserRegistrationID = ui.UserRegistrationID;

            List<UserLoginInfo> uli = new List<UserLoginInfo>();

            try
            {
                uli = (from ulhd in _db.AdminModUserLogHistoryDetail
                       join uDetail in _db.AdminModUsersDetail on ulhd.UserId equals uDetail.UserId into userDetail
                       from ud in userDetail.DefaultIfEmpty()
                       join urDetail in _db.AdminModUserRegistrationDetail on ud.UserRegistrationId equals urDetail.UserRegistrationId into userRegDetail
                       from ur in userRegDetail.DefaultIfEmpty()

                       select new UserLoginInfo
                       {
                           UserID = ud.UserId,
                           UserRegistrationID = ud.UserRegistrationId,
                           UserName = ur.UserName,
                           //UserFullName = ud.UserFullName,
                           UserFullName = ud.ApplicantTypeId == 1 ? ud.ApplicantName : ud.OrganizationName,
                           UserDesignation = ud.UserDesignation,
                           UserMobile = ur.UserMobile,
                           UserEmail = ur.UserEmail,
                           //UserAddress = ud.UserAddress,
                           UserAddress = ud.PostalAddress,
                           LoginDateTime = ulhd.LoginDateTime.ToString("dd MMM, yyyy HH:mm:ss"),
                           MachineIpOrUrl = ulhd.MachineIPOrUrl
                       }).ToList();
            }
            catch (Exception ex)
            {
                uli = new List<UserLoginInfo>();
            }

            ViewBag.UserLoginInfo = uli;
            return View();
        }

        #region Send Email Via Local Mail
        //account/SendEmailViaLocalMail
        public IActionResult SendEmailViaLocalMail()
        {
            SmtpClient sc = new SmtpClient()
            {
                Host = "mail.cegisbd.com",
                Credentials = new NetworkCredential("administrator@cegisbd.com", "cegis@2017"),
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false
            };

            try
            {
                MailMessage msg = new MailMessage();
                msg.From = new MailAddress("administrator@cegisbd.com", "Administrator, CEGIS");

                // In case the mail system doesn't like no to recipients. This could be removed
                //msg.To.Add("pssp@companyDomainName.com");

                //msg.To.Add("saidulkarim@cegisbd.com");
                msg.To.Add("atmskrony@gmail.com");
                msg.Subject = "Test Mail";
                msg.Body = "This is test.";
                msg.IsBodyHtml = true;
                //Response.Write(msg);
                sc.Send(msg);

                TempData["Message"] = ch.ShowMessage(Sign.Success, Sign.Success.ToString(), "Success");
            }
            catch (Exception ex)
            {
                TempData["Message"] = ch.ShowMessage(Sign.Error, Sign.Error.ToString(), ex.Message);
            }

            //try
            //{
            //    SmtpClient client = new SmtpClient("mail.cegisbd.com", 465);
            //    client.EnableSsl = true;
            //    client.Timeout = 10000;
            //    client.DeliveryMethod = SmtpDeliveryMethod.Network;
            //    client.UseDefaultCredentials = false;
            //    client.Credentials = new NetworkCredential("administrator@cegisbd.com", "cegis@2017");
            //    MailMessage msg = new MailMessage();
            //    msg.To.Add("atmskrony@gmail.com");
            //    msg.From = new MailAddress("administrator@cegisbd.com");
            //    msg.Subject = "Test Mail";
            //    msg.Body = "This is test.";
            //    client.Send(msg);
            //    TempData["Message"] = ch.ShowMessage(Sign.Success, Sign.Success.ToString(), "Success");
            //}
            //catch (Exception ex)
            //{
            //    TempData["Message"] = ch.ShowMessage(Sign.Error, Sign.Error.ToString(), ex.Message);
            //}
            //try
            //{
            //    EmailModel em = new EmailModel
            //    {
            //        To = "atmskrony@gmail.com",
            //        Subject = "Test Mail",
            //        Body = "This is test.",
            //        Attachment = null
            //    };
            //    if (em != null)
            //    {
            //        es.Send(em);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    TempData["Message"] = ch.ShowMessage(Sign.Error, Sign.Error.ToString(), ex.Message);
            //    //throw;
            //}

            return View();
        }
        #endregion

        //account/UserHasDefPwd :: User Has Default Password
        private int HasDefPwd(string uid)
        {
            int result = 0;
            string UserName = uid;
            string UserPass = "1234";

            var userRegInfo = _db.AdminModUserRegistrationDetail
                                 .Where(r => r.UserName.ToString().Equals(UserName) && r.UserPassword.ToString().Equals(UserPass.EncryptString()))
                                 .FirstOrDefault();

            if (userRegInfo != null)
            {
                result = 1; // still using default password
            }
            else
            {
                result = 0; // not default password
            }

            return result;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}