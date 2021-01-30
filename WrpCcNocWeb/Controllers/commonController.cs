using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WrpCcNocWeb.DatabaseContext;
using WrpCcNocWeb.Helpers;
using WrpCcNocWeb.Models;
using WrpCcNocWeb.Models.TempModels;
using WrpCcNocWeb.Models.UserManagement;
using WrpCcNocWeb.Models.Utility;

namespace WrpCcNocWeb.Controllers
{
    public class commonController : Controller
    {
        #region Initialization        
        private readonly WrpCcNocDbContext _db = new WrpCcNocDbContext();
        private readonly CommonHelper ch = new CommonHelper();
        private Notification noti = new Notification();
        #endregion

        //common/GetProjectTypeData
        [HttpGet]
        public JsonResult gptd()
        {
            List<LookUpCcModProjectType> _items = new List<LookUpCcModProjectType>();

            try
            {
                _items = _db.LookUpCcModProjectType.ToList();
            }
            catch (Exception ex)
            {
                var message = ch.ExtractInnerException(ex);
                _items = new List<LookUpCcModProjectType>();
            }

            return Json(_items);
        }

        //common/GetAdminBoundary :: Upazila, Union
        [HttpGet]
        public JsonResult gab(string code, string type)
        {
            List<ListItems> _items = new List<ListItems>();

            try
            {
                if (type == "upz")
                {
                    _items = (from u in _db.LookUpAdminBndUpazila
                              where u.DistrictGeoCode == code
                              select new ListItems
                              {
                                  code = u.UpazilaGeoCode,
                                  ab_name = u.UpazilaName,
                                  ab_name_bn = u.UpazilaNameBn
                              }).ToList();
                }
                else if (type == "uni")
                {
                    _items = (from u in _db.LookUpAdminBndUnion
                              where u.UpazilaGeoCode == code
                              select new ListItems
                              {
                                  code = u.UnionGeoCode,
                                  ab_name = u.UnionName,
                                  ab_name_bn = u.UnionNameBn
                              }).ToList();
                }
                else if (type == "mou")
                {
                    _items = (from u in _db.LookUpAdminBndMouza
                              where u.UnionGeoCode == code
                              select new ListItems
                              {
                                  code = u.MouzaGeoCode,
                                  ab_name = u.MouzaName,
                                  ab_name_bn = u.MouzaNameBn
                              }).ToList();
                }

                if (_items.Count > 0)
                {
                    return Json(_items);
                }
                else
                {
                    _items = null;

                    noti = new Notification
                    {
                        id = string.Empty,
                        status = "error",
                        message = "Sorry, no data found."
                    };

                    return Json(noti);
                }
            }
            catch (Exception ex)
            {
                var message = ch.ExtractInnerException(ex);

                noti = new Notification
                {
                    id = string.Empty,
                    status = "error",
                    message = message
                };

                return Json(noti);
            }
        }

        public string RedirectPage(int ProjectTypeId)
        {
            string result = string.Empty;

            switch (ProjectTypeId)
            {
                case 1:
                    return "view31";

                case 2:
                    return "view32";

                case 3:
                    return "view33";

                case 4:
                    return "view34";

                case 5:
                    return "view35";

                case 6:
                    return "view36";

                case 7:
                    return "view37";

                case 8:
                    return "view38";

                case 9:
                    return "view39";

                case 10:
                    return "view310";

                case 11:
                    return "view311";

                case 12:
                    return "view312";

                case 13:
                    return "view313";

                default:
                    return "list";
            }
        }

        public string GetCallCenterInfo()
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

            return callAt;
        }

        //common/gunl :: GetUserNotificationList
        [HttpGet]
        public JsonResult gunl()
        {
            try
            {
                UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");

                var _details = (from qd in _db.CcModProjectQueryDetail

                                join sdi in _db.AdminModUsersDetail on qd.SenderUserId equals sdi.UserId //sender user info
                                join sri in _db.AdminModUserRegistrationDetail on sdi.UserRegistrationId equals sri.UserRegistrationId //sender reg info

                                join rei in _db.AdminModUsersDetail on qd.ReceiverUserId equals rei.UserId //receiver user info
                                join rri in _db.AdminModUserRegistrationDetail on rei.UserRegistrationId equals rri.UserRegistrationId //receiver reg info

                                where qd.ReceiverUserId == ui.UserID && qd.QueryStateId == 2

                                select new
                                {
                                    qd.ProjectQueryId,
                                    qd.ProjectId,
                                    qd.SenderUserId,
                                    SenderUserName = sri.UserName,
                                    SenderFullName = sdi.ApplicantTypeId == 1 ? sdi.ApplicantName : sdi.OrganizationName,
                                    SenderDesignation = sdi.UserDesignation,
                                    QuerySubject = qd.QuerySubject.Substring(0, 35),
                                    QueryBody = qd.QueryBody.Substring(0, 35),
                                    qd.ReceiverUserId,
                                    ReceiverUserName = rri.UserName,
                                    ReceiverFullName = rei.ApplicantTypeId == 1 ? rei.ApplicantName : rei.OrganizationName,
                                    ReceiverDesignation = rei.UserDesignation,
                                    qd.QueryStateId,
                                    SentOn = qd.QuerySentOn.ToString("dd MMM, yyyy HH:mm")
                                }).OrderByDescending(o => o.ProjectQueryId).Take(10).ToList();

                if (_details.Count > 0)
                {
                    return Json(_details);
                }
                else
                {
                    _details = null;

                    noti = new Notification
                    {
                        id = string.Empty,
                        status = "error",
                        message = "Sorry, no data found."
                    };

                    return Json(noti);
                }
            }
            catch (Exception ex)
            {
                var message = ch.ExtractInnerException(ex);

                noti = new Notification
                {
                    id = string.Empty,
                    status = "error",
                    message = message
                };

                return Json(noti);
            }
        }

        //common/SaveCertificateDownloadHistory :: scdh
        [HttpPost]
        public JsonResult scdh(long _pid)
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
            int result = 0;

            try
            {
                if (_pid != 0)
                {
                    using var dbContextTransaction = _db.Database.BeginTransaction();
                    CcModDownloadCertificateHist dsh = new CcModDownloadCertificateHist()
                    {
                        ProjectId = _pid,
                        DownloadDateTime = DateTime.Now
                    };

                    try
                    {
                        _db.CcModDownloadCertificateHist.Add(dsh);
                        result = _db.SaveChanges();

                        if (result > 0)
                        {
                            dbContextTransaction.Commit();

                            noti = new Notification
                            {
                                id = _pid.ToString(),
                                status = "success",
                                message = "Certificate download history has been saved successfully."
                            };
                        }
                        else
                        {
                            dbContextTransaction.Rollback();

                            noti = new Notification
                            {
                                id = _pid.ToString(),
                                status = "error",
                                message = "Certificate download history information not saved."
                            };
                        }
                    }
                    catch (Exception ex)
                    {
                        dbContextTransaction.Rollback();
                        var message = ch.ExtractInnerException(ex);

                        noti = new Notification
                        {
                            id = _pid.ToString(),
                            status = "error",
                            message = "Certificate Download History: Transaction has been rollbacked. " + message
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                var message = ch.ExtractInnerException(ex);

                noti = new Notification
                {
                    id = "0",
                    status = "error",
                    message = message
                };
            }

            return Json(noti);
        }
    }
}