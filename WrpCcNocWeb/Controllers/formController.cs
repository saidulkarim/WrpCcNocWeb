using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WrpCcNocWeb.DatabaseContext;
using WrpCcNocWeb.Helpers;
using WrpCcNocWeb.Models;
using WrpCcNocWeb.Models.TempModels;
using WrpCcNocWeb.Models.UserManagement;
using WrpCcNocWeb.Models.Utility;
using WrpCcNocWeb.ViewModels;
using static WrpCcNocWeb.Helpers.CommonHelper;

namespace WrpCcNocWeb.Controllers
{
    public class formController : Controller
    {
        #region Initialization
        private readonly LoggedUserInfo iLoggedUser;
        private readonly WrpCcNocDbContext _db = new WrpCcNocDbContext();
        private readonly CommonHelper ch = new CommonHelper();
        private Notification noti = new Notification();
        #endregion

        public IActionResult index()
        {
            ViewBag.ProjectTypeId = new SelectList(_db.LookUpCcModProjectType.ToList(), "ProjectTypeId", "ProjectType");
            return View();
        }

        [HttpPost]
        public IActionResult index(SelectedForm _selectedForm)
        {
            if (!string.IsNullOrEmpty(_selectedForm.ProjectTypeId))
            {
                HttpContext.Session.SetComplexData("SelectedForm", _selectedForm);
                return RedirectToAction("apply", "form");
            }
            else
            {
                TempData["Message"] = ch.ShowMessage(Sign.Warning, "Required", "Please select a form to apply.");
            }

            ViewBag.ProjectTypeId = new SelectList(_db.LookUpCcModProjectType.ToList(), "ProjectTypeId", "ProjectType", _selectedForm.ProjectTypeId);
            return View();
        }

        public IActionResult apply()
        {
            SelectedForm sf = HttpContext.Session.GetComplexData<SelectedForm>("SelectedForm");
            ViewBag.ProjectTypeId = sf.ProjectTypeId;
            ViewBag.ProjectTitle = sf.ProjectTitle;

            #region Dropdown Data Loading
            ViewBag.LookUpAdminBndDistrict = _db.LookUpAdminBndDistrict.ToList();
            ViewBag.LookUpAdminBndUpazila = _db.LookUpAdminBndUpazila.ToList();
            ViewBag.LookUpAdminBndUnion = _db.LookUpAdminBndUnion.ToList();

            ViewBag.LookUpCcModHydroRegion = _db.LookUpCcModHydroRegion.ToList();
            ViewBag.LookUpCcModDeltPlan2100HotSpot = _db.LookUpCcModDeltPlan2100HotSpot.ToList();
            ViewBag.LookUpCcModHydroSystem = _db.LookUpCcModHydroSystem.ToList();

            ViewBag.LookUpCcModTypeOfFlood = _db.LookUpCcModTypeOfFlood.ToList();
            ViewBag.LookUpCcModFloodFrequency = _db.LookUpCcModFloodFrequency.ToList();
            ViewBag.LookUpCcModDrainageCondition = _db.LookUpCcModDrainageCondition.ToList();
            ViewBag.LookUpCcModEIAParameter = _db.LookUpCcModEIAParameter.ToList();
            ViewBag.LookUpCcModSIAParameter = _db.LookUpCcModSIAParameter.ToList();
            ViewBag.LookUpCcModEcoAndFinancial = _db.LookUpCcModEcoAndFinancial.ToList();

            ViewBag.LookUpCcModNWPArticle = _db.LookUpCcModNWPArticle.ToList();
            ViewBag.LookUpCcModNWMPProgramme = _db.LookUpCcModNWMPProgramme.ToList();
            ViewBag.LookUpCcModSDGGoal = _db.LookUpCcModSDGGoal.ToList();
            ViewBag.LookUpCcModSDGIndicator = _db.LookUpCcModSDGIndicator.ToList();
            ViewBag.LookUpCcModDeltPlan2100Goal = _db.LookUpCcModDeltPlan2100Goal.ToList();
            ViewBag.LookUpCcModGPWMGroupType = _db.LookUpCcModGPWMGroupType.ToList();
            #endregion

            return View();
        }

        //form/GeneralInfoSave :: gis
        [HttpPost]
        public JsonResult gis(GeneralInfo _gi)
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
            CcModAppProjectCommonDetail general = _gi.CommonDetail;
            CcModPrjLocationDetail location = _gi.LocationDetail;

            long ProjectID = 0;
            int result = 0;

            try
            {
                if (general != null && general.ProjectTypeId != 0)
                {
                    using (var dbContextTransaction = _db.Database.BeginTransaction())
                    {
                        try
                        {
                            general.UserId = ui.UserID;

                            _db.CcModAppProjectCommonDetail.Add(general);
                            result = _db.SaveChanges();

                            if (result > 0)
                            {
                                ProjectID = general.ProjectId;
                                location.ProjectId = ProjectID;

                                if (!string.IsNullOrEmpty(location.DistrictGeoCode))
                                {
                                    _db.CcModPrjLocationDetail.Add(location);
                                    result = _db.SaveChanges();

                                    if (result > 0)
                                    {
                                        dbContextTransaction.Commit();

                                        noti = new Notification
                                        {
                                            id = location.ProjectId.ToString(),
                                            status = "success",
                                            message = "General information has been saved successfully."
                                        };
                                    }
                                    else
                                    {
                                        dbContextTransaction.Rollback();

                                        noti = new Notification
                                        {
                                            id = location.ProjectId.ToString(),
                                            status = "error",
                                            message = "General information not saved."
                                        };
                                    }
                                }
                            }
                            else
                            {
                                dbContextTransaction.Rollback();

                                noti = new Notification
                                {
                                    id = location.ProjectId.ToString(),
                                    status = "error",
                                    message = "General information not saved."
                                };
                            }
                        }
                        catch (Exception ex)
                        {
                            dbContextTransaction.Rollback();
                            var message = ch.ExtractInnerException(ex);

                            noti = new Notification
                            {
                                id = location.ProjectId.ToString(),
                                status = "error",
                                message = "Transaction has been rollbacked. " + message
                            };
                        }
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

        //form/HydroSystemDetailSave :: hsds
        [HttpPost]
        public JsonResult hsds(CcModHydroSystemDetail _hsd)
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
            int result = 0;

            try
            {
                if (_hsd != null && _hsd.ProjectId != 0)
                {
                    using (var dbContextTransaction = _db.Database.BeginTransaction())
                    {
                        try
                        {
                            _db.CcModHydroSystemDetail.Add(_hsd);
                            result = _db.SaveChanges();

                            if (result > 0)
                            {
                                dbContextTransaction.Commit();

                                noti = new Notification
                                {
                                    id = _hsd.HydroSysDetailId.ToString(),
                                    status = "success",
                                    message = "Hydrological system information has been saved successfully."
                                };
                            }
                            else
                            {
                                dbContextTransaction.Rollback();

                                noti = new Notification
                                {
                                    id = _hsd.HydroSysDetailId.ToString(),
                                    status = "error",
                                    message = "Hydrological system information not saved."
                                };
                            }
                        }
                        catch (Exception ex)
                        {
                            dbContextTransaction.Rollback();
                            var message = ch.ExtractInnerException(ex);

                            noti = new Notification
                            {
                                id = _hsd.HydroSysDetailId.ToString(),
                                status = "error",
                                message = "Transaction has been rollbacked. " + message
                            };
                        }
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