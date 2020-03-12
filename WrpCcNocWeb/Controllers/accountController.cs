﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WrpCcNocWeb.Models;
using WrpCcNocWeb.Helpers;
using static WrpCcNocWeb.Helpers.CommonHelper;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using Microsoft.AspNetCore.Http;
using WrpCcNocWeb.Models.Utility;
using WrpCcNocWeb.Models.UserManagement;
using WrpCcNocWeb.DatabaseContext;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WrpCcNocWeb.Controllers
{
    public class accountController : Controller
    {
        private readonly WrpCcNocDbContext _db = new WrpCcNocDbContext();
        private readonly CommonHelper ch = new CommonHelper();

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
                                     .Where(r => r.UserName.ToString().Equals(UserName) && r.UserPassword.ToString().Equals(UserPass))
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
                                if (userRegInfo.UserPassword.Equals(UserPass))
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
                                        return RedirectToActionPermanent("index", "home");
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
                           UserMobile = ri.UserMobile.ToString()
                       }).FirstOrDefault();
            }
            else
            {
                _ui = (from ri in _db.AdminModUserRegistrationDetail
                       where ri.UserName.Equals(logged_user)
                       select new UserInfo
                       {
                           UserID = user_id,
                           UserRegistrationID = ri.UserRegistrationId,
                           UserName = ri.UserName.ToString(),
                           UserEmail = ri.UserEmail.ToString(),
                           UserMobile = ri.UserMobile.ToString()
                       }).FirstOrDefault();
            }

            if (_ui != null && _ui.UserRegistrationID != 0)
            {
                HttpContext.Session.SetComplexData("LoggerUserInfo", _ui);

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
            //if (userID > 0)
            //{
            //    AdminModUserLogHistoryDetail _log = new AdminModUserLogHistoryDetail
            //    {
            //        UserId = userID,
            //        LoginDateTime = DateTime.Now,
            //        MachineIPOrUrl = "130.180.3.215"
            //    };

            //    _db.AdminModUserLogHistoryDetail.Add(_log);
            //    _db.SaveChanges();
            //}
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
            try
            {
                userReg.UserActivationStatus = 0;
                userReg.DateOfCreation = DateTime.Now;
                userReg.EmailVerificationCode = Guid.NewGuid().ToString();
                userReg.IsEmailVerified = 0;

                _db.AdminModUserRegistrationDetail.Add(userReg);
                int x = _db.SaveChanges();

                if (x > 0)
                {
                    ViewBag.UserVerificationCode = userReg.EmailVerificationCode;
                    TempData["Message"] = ch.ShowMessage(Sign.Success, Sign.Success.ToString(), OperationMessage.Success.ToDescription());

                    return RedirectToAction("verify", new { id = ViewBag.UserVerificationCode });
                }
                else
                {
                    ViewBag.UserVerificationCode = string.Empty;
                    TempData["Message"] = ch.ShowMessage(Sign.Error, Sign.Error.ToString(), OperationMessage.NotSuccess.ToDescription());
                }
            }
            catch (Exception ex)
            {
                ViewBag.UserVerificationCode = string.Empty;

                var message = ch.ExtractInnerException(ex);
                TempData["Message"] = ch.ShowMessage(Sign.Danger, Sign.Danger.ToString(), message);
            }

            return View();
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
                AdminModUserRegistrationDetail userRegVerify = _db.AdminModUserRegistrationDetail.Where(w => w.EmailVerificationCode.ToString().Equals(id)).FirstOrDefault();

                if (userRegVerify != null)
                {
                    try
                    {
                        userRegVerify.UserActivationStatus = 1;
                        userRegVerify.LastModifiedDate = DateTime.Now;
                        userRegVerify.IsEmailVerified = 1;

                        _db.Entry(userRegVerify).State = EntityState.Modified;
                        int x = _db.SaveChanges();

                        if (x > 0)
                        {
                            return RedirectToAction("login");
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
            ViewBag.UserRegistrationID = ui.UserRegistrationID;
            ViewBag.SecurityQuestionId = new SelectList(_db.LookUpAdminModSecurityQuestion.ToList(), "SecurityQuestionId", "SecurityQuestion");

            return View();
        }

        //account/profile
        [HttpPost]
        public IActionResult profile(AdminModUsersDetail userDetail)
        {
            int result = 0;

            if (userDetail != null)
            {
                var _user = _db.AdminModUsersDetail
                    .Where(u => u.UserId != null && u.UserId == userDetail.UserId)
                    .Select(x => new
                    {
                        UserId = x.UserId,
                    }).FirstOrDefault();

                if (_user == null)
                {
                    try
                    {
                        userDetail.UserDateOfBirth = userDetail.UserDateOfBirth.ToString().ToDatabaseDateFormat();
                        userDetail.IsProfileSubmitted = 1;

                        _db.AdminModUsersDetail.Add(userDetail);

                        result = _db.SaveChanges();

                        if (result > 0)
                        {
                            UserInfo _ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
                            _ui.UserID = userDetail.UserId;
                            HttpContext.Session.SetComplexData("LoggerUserInfo", _ui);

                            return RedirectToAction("index", "home");
                        }
                        else
                        {
                            TempData["Message"] = ch.ShowMessage(Sign.Error, "Sorry, your profile information not submitted!");
                        }
                    }
                    catch (Exception ex)
                    {
                        var message = ch.ExtractInnerException(ex);
                        TempData["Message"] = ch.ShowMessage(Sign.Danger, message);
                    }
                }

                UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
                ViewBag.UserRegistrationID = ui.UserRegistrationID;
            }

            ViewBag.SecurityQuestionId = new SelectList(_db.LookUpAdminModSecurityQuestion.ToList(), "SecurityQuestionId", "SecurityQuestion", userDetail.SecurityQuestionId);
            return View(userDetail);
        }

        //account/recover
        public IActionResult recover()
        {
            return View();
        }

        //account/logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult logout()
        {
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
            List<UserInfo> userDetails = _db.AdminModUsersDetail.Join(_db.AdminModUserRegistrationDetail, u => u.UserRegistrationId, r => r.UserRegistrationId, (u, r) => new { R = r, U = u })
                                         .Select(s => new UserInfo
                                         {
                                             UserID = s.U.UserId,
                                             UserName = s.R.UserName,
                                             UserFullName = s.U.UserFullName,
                                             UserDesignation = s.U.UserDesignation,
                                             UserEmail = s.R.UserEmail,
                                             UserMobile = s.R.UserMobile,
                                             UserActivationStatus = s.R.UserActivationStatus

                                         }).ToList();
            ViewData["UserDetails"] = userDetails;
            return View();
        }

        public IActionResult createUser()
        {
            return View();
        }

        //POST: /account/createUser
        [HttpPost]
        public IActionResult createUser(AdminModUserRegistrationDetail userReg)
        {
            try
            {
                userReg.UserActivationStatus = 0;
                userReg.DateOfCreation = DateTime.Now;
                userReg.EmailVerificationCode = Guid.NewGuid().ToString();
                userReg.IsEmailVerified = 0;

                _db.AdminModUserRegistrationDetail.Add(userReg);
                int x = _db.SaveChanges();

                if (x > 0)
                {
                    ViewBag.UserVerificationCode = userReg.EmailVerificationCode;
                    TempData["Message"] = ch.ShowMessage(Sign.Success, Sign.Success.ToString(), OperationMessage.Success.ToDescription());

                    return RedirectToAction("verify", new { id = ViewBag.UserVerificationCode });
                }
                else
                {
                    ViewBag.UserVerificationCode = string.Empty;
                    TempData["Message"] = ch.ShowMessage(Sign.Error, Sign.Error.ToString(), OperationMessage.NotSuccess.ToDescription());
                }
            }
            catch (Exception ex)
            {
                ViewBag.UserVerificationCode = string.Empty;

                var message = ch.ExtractInnerException(ex);
                TempData["Message"] = ch.ShowMessage(Sign.Danger, Sign.Danger.ToString(), message);
            }

            return View();
        }

        public IActionResult userDetails(long userId)
        {
            UserInfo userDetails = _db.AdminModUsersDetail.Join(_db.AdminModUserRegistrationDetail, u => u.UserRegistrationId, r => r.UserRegistrationId, (u, r) => new { R = r, U = u })
                                         .Where(w => w.U.UserId == userId)
                                         .Select(s => new UserInfo
                                         {
                                             UserID = s.U.UserId,
                                             UserName = s.R.UserName,
                                             UserFullName = s.U.UserFullName,
                                             UserDesignation = s.U.UserDesignation,
                                             UserEmail = s.R.UserEmail,
                                             UserMobile = s.R.UserMobile,
                                             UserAddress = s.U.UserAddress,
                                             UserActivationStatus = s.R.UserActivationStatus,
                                             DateOfCreation = s.R.DateOfCreation,
                                             LastModifiedDate = s.R.LastModifiedDate
                                         }).FirstOrDefault();

            ViewData["UserDetails"] = userDetails;
            return View();
        }

        public IActionResult editUserProfile()
        {
            return View();
        }

        public IActionResult resetPassword()
        {
            return View();
        }

        public IActionResult makeActiveInactive()
        {
            return View();
        }

        public IActionResult deleteUser()
        {
            return View();
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
            try
            {
                //amug.UserActivationStatus = 0;
                //amug.DateOfCreation = DateTime.Now;
                //amug.EmailVerificationCode = Guid.NewGuid().ToString();
                //amug.IsEmailVerified = 0;

                //_db.AdminModUserRegistrationDetail.Add(amug);
                //int x = _db.SaveChanges();

                //if (x > 0)
                //{
                //    ViewBag.UserVerificationCode = amug.EmailVerificationCode;
                TempData["Message"] = ch.ShowMessage(Sign.Success, Sign.Success.ToString(), OperationMessage.Success.ToDescription());

                //    return RedirectToAction("verify", new { id = ViewBag.UserVerificationCode });
                //}
                //else
                //{
                //    ViewBag.UserVerificationCode = string.Empty;
                //    TempData["Message"] = ch.ShowMessage(Sign.Error, Sign.Error.ToString(), OperationMessage.NotSuccess.ToDescription());
                //}
            }
            catch (Exception ex)
            {
                ViewBag.UserVerificationCode = string.Empty;

                var message = ch.ExtractInnerException(ex);
                TempData["Message"] = ch.ShowMessage(Sign.Danger, Sign.Danger.ToString(), message);
            }

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