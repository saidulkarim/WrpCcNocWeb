using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WrpCcNocWeb.DatabaseContext;
using WrpCcNocWeb.Helpers;
using WrpCcNocWeb.Models;
using WrpCcNocWeb.Models.UserManagement;
using WrpCcNocWeb.Models.Utility;
using static WrpCcNocWeb.Helpers.CommonHelper;
using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WrpCcNocWeb.Controllers
{
    public class queryController : Controller
    {
        #region Initialization
        private readonly LoggedUserInfo iLoggedUser;
        private readonly WrpCcNocDbContext _db = new WrpCcNocDbContext();
        private commonController cc = new commonController();
        private EmailService es = new EmailService();
        private readonly CommonHelper ch = new CommonHelper();
        private Notification noti = new Notification();
        private static readonly IWebHostEnvironment hostingEnvironment;
        private static formController fc = new formController(hostingEnvironment);
        private readonly string rootDirOfProjFile = "../images";
        private readonly string rootDirOfDocs = "../docs";
        #endregion

        public IActionResult index()
        {
            List<ProjectQueryViewList> pqvl = new List<ProjectQueryViewList>();

            try
            {
                UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
                if (ui == null)
                {
                    return RedirectToAction("login", "account");
                }

                pqvl = (from qd in _db.CcModProjectQueryDetail
                        join qst in _db.LookUpCcModQueryState on qd.QueryStateId equals qst.QueryStateId into qstGroup
                        from qs in qstGroup.DefaultIfEmpty()

                        join sdi in _db.AdminModUsersDetail on qd.SenderUserId equals sdi.UserId //sender user info
                        join sri in _db.AdminModUserRegistrationDetail on sdi.UserRegistrationId equals sri.UserRegistrationId //sender reg info
                        join rei in _db.AdminModUsersDetail on qd.ReceiverUserId equals rei.UserId //receiver user info
                        join rri in _db.AdminModUserRegistrationDetail on rei.UserRegistrationId equals rri.UserRegistrationId //receiver reg info

                        where qd.ReceiverUserId == ui.UserID

                        select new ProjectQueryViewList
                        {
                            ProjectQueryId = qd.ProjectQueryId,
                            ProjectId = qd.ProjectId,
                            SenderUserId = qd.SenderUserId,
                            SenderUserName = sri.UserName,
                            SenderFullName = sdi.UserFullName,
                            SenderDesignation = sdi.UserDesignation,
                            QuerySubject = qd.QuerySubject,
                            QueryBody = qd.QueryBody.Substring(0, 100),
                            ReceiverUserId = qd.ReceiverUserId,
                            ReceiverUserName = rri.UserName,
                            ReceiverFullName = rei.UserFullName,
                            ReceiverDesignation = rei.UserDesignation,
                            QueryStateId = qd.QueryStateId,
                            QueryState = qs.QueryStateName,
                            SentOn = qd.QuerySentOn.ToString("dd MMM, yyyy HH:mm")
                        }).OrderByDescending(o => o.ProjectQueryId).ToList();
            }
            catch (Exception ex)
            {
                var message = ch.ExtractInnerException(ex);
                pqvl = new List<ProjectQueryViewList>();
            }

            ViewBag.PQVL = pqvl; //Project Query View List
            return View();
        }

        public IActionResult query(long? id, long? qid)
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");

            if (ui == null)
            {
                return RedirectToAction("login", "account");
            }

            UserLevelInfo uli = HttpContext.Session.GetComplexData<UserLevelInfo>("UserLevelInfo");

            if (id != null)
            {
                CcModAppProjectCommonDetail pcd = _db.CcModAppProjectCommonDetail.Find(id);

                if (pcd == null)
                {
                    ViewBag.ReceiverUserId = 0;
                    ViewBag.ReceiverName = string.Empty;

                    ViewBag.ProjectId = 0;
                    ViewBag.QuerySubject = string.Empty;
                }
                else
                {
                    UserInfo userInfoOfReceiver = fc.GetHigherAuthorityByProjectId(pcd);

                    if (userInfoOfReceiver != null)
                    {
                        ViewBag.ReceiverUserId = userInfoOfReceiver.UserID;
                        ViewBag.ReceiverName = userInfoOfReceiver.UserFullName;
                    }
                    else
                    {
                        ViewBag.ReceiverUserId = 0;
                        ViewBag.ReceiverName = string.Empty;
                    }

                    ViewBag.ProjectId = id;
                    ViewBag.QuerySubject = "Query based on " + pcd.ProjectName;
                }
            }
            else
            {
                CcModProjectQueryDetail qd = _db.CcModProjectQueryDetail.Where(w => w.ProjectId == id).FirstOrDefault();
                if (qd != null)
                {
                    UserInfo userInfoOfReceiver = fc.GetUserInfoById(qd.SenderUserId.ToString().ToInt());

                    ViewBag.ReceiverUserId = qd.SenderUserId;
                    ViewBag.ReceiverName = userInfoOfReceiver.UserFullName;

                    ViewBag.ProjectQueryId = qd.ProjectQueryId;
                    ViewBag.ProjectId = qd.ProjectId;
                    ViewBag.QuerySubject = qd.QuerySubject;
                    ViewBag.OldQueryBody = qd.QueryBody;
                }
                else
                {
                    ViewBag.ReceiverUserId = 0;
                    ViewBag.ReceiverName = string.Empty;

                    ViewBag.ProjectQueryId = 0;
                    ViewBag.ProjectId = 0;
                    ViewBag.QuerySubject = string.Empty;
                    ViewBag.OldQueryBody = string.Empty;
                }
            }

            if (qid != null)
            {
                ViewBag.ProjectQueryId = qid;

                CcModProjectQueryDetail qd = _db.CcModProjectQueryDetail.Find(qid);
                if (qd != null)
                {
                    #region Updating Read State
                    qd.QueryStateId = 1;
                    _db.Entry(qd).State = EntityState.Modified;
                    _db.SaveChanges();
                    #endregion

                    ViewBag.QuerySubject = qd.QuerySubject;
                    ViewBag.OldQueryBody = qd.QueryBody;
                }
                else
                {
                    ViewBag.QuerySubject = "";
                    ViewBag.OldQueryBody = "";
                }
            }

            ViewBag.SenderUserId = ui.UserID;
            ViewBag.SenderName = ui.UserFullName;

            return View();
        }

        [HttpPost]
        public IActionResult query(CcModProjectQueryDetail _query)
        {
            int result = 0;
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");

            if (ui == null)
            {
                return RedirectToAction("login", "account");
            }

            UserLevelInfo uli = HttpContext.Session.GetComplexData<UserLevelInfo>("UserLevelInfo");

            try
            {
                //Query based on Project
                if (_query.ProjectId > 0)
                {
                    if (string.IsNullOrEmpty(_query.QueryBody))
                    {
                        TempData["Message"] = ch.ShowMessage(Sign.Warning, "Empty Message", "Message can not be left empty!");
                        return RedirectToAction("query");
                    }

                    DateTime queryDateTime = DateTime.Now;
                    string sender_info = "<b>Sender: " + ui.UserFullName + "," + ui.UserDesignation + "</b><br />";
                    sender_info += "<b>Date: " + queryDateTime.ToString("dd MMM, yyyy HH:mm") + "</b><br />";
                    sender_info += "<hr />";
                    string query_body = sender_info + _query.QueryBody;

                    if (_query.ProjectQueryId == 0)
                    {
                        CcModProjectQueryDetail qd = new CcModProjectQueryDetail()
                        {
                            ProjectId = _query.ProjectId,
                            SenderUserId = _query.SenderUserId,
                            QuerySubject = _query.QuerySubject,
                            QueryBody = query_body,
                            ReceiverUserId = _query.ReceiverUserId,
                            QueryStateId = 2, //not read
                            QuerySentOn = queryDateTime
                        };

                        _db.CcModProjectQueryDetail.Add(qd);
                        result = _db.SaveChanges();

                        if (result > 0)
                        {
                            TempData["Message"] = ch.ShowMessage(Sign.Success, "Success", "Your query has been submitted successfully.");
                            return RedirectToAction("index");
                        }
                        else
                        {
                            TempData["Message"] = ch.ShowMessage(Sign.Error, "Not Submit", "Your query not submitted!");
                        }
                    }
                    else
                    {
                        CcModProjectQueryDetail qd = _db.CcModProjectQueryDetail.Find(_query.ProjectQueryId);

                        if (qd != null)
                        {
                            qd.ReceiverUserId = qd.SenderUserId;
                            qd.SenderUserId = ui.UserID;
                            qd.QueryStateId = 2;
                            qd.QueryBody = query_body + "<br /><hr class='double' />" + qd.QueryBody;
                        }

                        _db.Entry(qd).State = EntityState.Modified;
                        result = _db.SaveChanges();

                        if (result > 0)
                        {
                            TempData["Message"] = ch.ShowMessage(Sign.Success, "Success", "Your query has been submitted successfully.");
                            return RedirectToAction("index");
                        }
                        else
                        {
                            TempData["Message"] = ch.ShowMessage(Sign.Error, "Not Submit", "Your query not submitted!");
                        }
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(_query.QueryBody))
                    {
                        TempData["Message"] = ch.ShowMessage(Sign.Warning, "Empty Message", "Message can not be left empty!");
                        return RedirectToAction("query");
                    }

                    DateTime queryDateTime = DateTime.Now;
                    string sender_info = "<b>Sender: " + ui.UserFullName + "," + ui.UserDesignation + "</b><br />";
                    sender_info += "<b>Date: " + queryDateTime.ToString("dd MMM, yyyy HH:mm") + "</b><br />";
                    sender_info += "<hr />";
                    string query_body = sender_info + _query.QueryBody;

                    if (_query.ProjectQueryId == 0)
                    {
                        CcModProjectQueryDetail qd = new CcModProjectQueryDetail()
                        {
                            //ProjectId = _query.ProjectId,
                            SenderUserId = _query.SenderUserId,
                            QuerySubject = _query.QuerySubject,
                            QueryBody = query_body,
                            ReceiverUserId = _query.ReceiverUserId,
                            QueryStateId = 2, //not read
                            QuerySentOn = queryDateTime
                        };

                        _db.CcModProjectQueryDetail.Add(qd);
                        result = _db.SaveChanges();

                        if (result > 0)
                        {
                            TempData["Message"] = ch.ShowMessage(Sign.Success, "Success", "Your query has been submitted successfully.");
                            return RedirectToAction("index");
                        }
                        else
                        {
                            TempData["Message"] = ch.ShowMessage(Sign.Error, "Not Submit", "Your query not submitted!");
                        }
                    }
                    else
                    {
                        CcModProjectQueryDetail qd = _db.CcModProjectQueryDetail.Find(_query.ProjectQueryId);

                        if (qd != null)
                        {
                            qd.ReceiverUserId = qd.SenderUserId;
                            qd.SenderUserId = ui.UserID;
                            qd.QueryStateId = 2;
                            qd.QueryBody = query_body + "<br /><hr class='double' />" + qd.QueryBody;
                        }

                        _db.Entry(qd).State = EntityState.Modified;
                        result = _db.SaveChanges();

                        if (result > 0)
                        {
                            TempData["Message"] = ch.ShowMessage(Sign.Success, "Success", "Your query has been submitted successfully.");
                            return RedirectToAction("index");
                        }
                        else
                        {
                            TempData["Message"] = ch.ShowMessage(Sign.Error, "Not Submit", "Your query not submitted!");
                        }
                    }
                }

                UserInfo userInfoOfReceiver = fc.GetUserInfoById(_query.ReceiverUserId.ToString().ToInt());

                if (userInfoOfReceiver != null)
                {
                    ViewBag.ReceiverUserId = userInfoOfReceiver.UserID;
                    ViewBag.ReceiverName = userInfoOfReceiver.UserFullName;
                }
                else
                {
                    ViewBag.ReceiverUserId = 0;
                    ViewBag.ReceiverName = string.Empty;
                }

                ViewBag.ProjectId = _query.ProjectId;
                ViewBag.QuerySubject = _query.QuerySubject;

                ViewBag.SenderUserId = ui.UserID;
                ViewBag.SenderName = ui.UserFullName;
            }
            catch (Exception ex)
            {
                TempData["Message"] = ch.ShowMessage(Sign.Error, "An Error Occured", ex.Message.ToString());
                return RedirectToAction("query");
            }

            return View();
        }
    }
}