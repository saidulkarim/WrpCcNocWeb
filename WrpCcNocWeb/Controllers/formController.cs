using System;
using System.Collections.Generic;
using System.Linq;
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
using Microsoft.EntityFrameworkCore;

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

                if (_selectedForm.ProjectTypeId.Equals("1"))
                    return RedirectToAction("fcmp", "form");
                else
                    TempData["Message"] = ch.ShowMessage(Sign.Warning, "Under Development", "Sorry, select project is under development.");
            }
            else
            {
                TempData["Message"] = ch.ShowMessage(Sign.Warning, "Required", "Please select a form to apply.");
            }

            ViewBag.ProjectTypeId = new SelectList(_db.LookUpCcModProjectType.ToList(), "ProjectTypeId", "ProjectType", _selectedForm.ProjectTypeId);
            return View();
        }

        public IActionResult status()
        {
            return View();
        }

        public IActionResult certificate()
        {
            return View();
        }

        //form/FloodControlManagementProject :: fcmp       
        public IActionResult fcmp()
        {
            ViewData["Title"] = "Flood Control Management Project";
            SelectedForm sf = HttpContext.Session.GetComplexData<SelectedForm>("SelectedForm");
            ViewBag.ProjectTypeId = sf.ProjectTypeId;
            ViewBag.ProjectTitle = sf.ProjectTitle;

            ProjectStatusInfo _psi = GetProjectId(sf.ProjectTypeId.ToString());

            if (_psi == null)
            {
                CcModAppProjectCommonDetail _pcd = new CcModAppProjectCommonDetail
                {
                    ProjectId = 0
                };

                ViewBag.ProjectCommonDetail = _pcd;

                LoadDropdownData();

                TempData["Message"] = ch.ShowMessage(Sign.Info, "Information", "Please enter new project information.");
                //return RedirectToAction("index", "form");
            }
            else if (_psi.ProjectId != 0 && _psi.AppSubmissionId == null)
            {
                TempData["Message"] = ch.ShowMessage(Sign.Warning, "Problem Occured", "Sorry, " + sf.ProjectTitle + " form submission status not found! Please contact with System Administrator.");
                return RedirectToAction("index", "form");
            }
            else
            {
                LoadDropdownData();

                CcModAppProjectCommonDetail _pcd = _db.CcModAppProjectCommonDetail.Find(_psi.ProjectId);
                ViewBag.ProjectCommonDetail = _pcd;
                CcModPrjLocationDetail _locationdetail = _db.CcModPrjLocationDetail.Where(w => w.ProjectId == _psi.ProjectId).FirstOrDefault();
                ViewBag.ProjectLocationDetail = _locationdetail;
                CcModAppProject_31_IndvDetail _indvdetail = _db.CcModAppProject_31_IndvDetail.Where(w => w.ProjectId == _psi.ProjectId).FirstOrDefault();
                ViewBag.ProjectIndvDetail31 = _indvdetail;

                var _hydroregiondetail = _db.CcModPrjHydroRegionDetail.Where(w => w.ProjectId == _psi.ProjectId)
                                            .Select(x => new { x.PrjHydroRegionDetailId, x.ProjectId, x.HydroRegionId }).ToList();
                ViewBag.HydroRegionDetail = _hydroregiondetail;

                var _hotspotdetail = _db.CcModBDP2100HotSpotDetail.Where(w => w.ProjectId == _psi.ProjectId)
                                        .Select(x => new { x.HotSpotDetailId, x.ProjectId, x.DeltaPlanHotSpotId }).ToList();
                ViewBag.BDP2100HotSpotDetail = _hotspotdetail;

                var _typesofflood = _db.CcModPrjTypesOfFloodDetail.Where(w => w.ProjectId == _psi.ProjectId)
                                       .Select(x => new { x.FloodTypeDetailId, x.ProjectId, x.FloodTypeId }).ToList();
                ViewBag.TypesOfFloodDetail = _typesofflood;

                var _compatnwpdetail = _db.CcModPrjCompatNWPDetail.Where(w => w.ProjectId == _psi.ProjectId)
                                       .Select(x => new { x.PrjCompatNWPId, x.ProjectId, x.NationalWaterPolicyArticleId }).ToList();
                ViewBag.CompatNWPDetail = _compatnwpdetail;

                var _compatnwmpdetail = _db.CcModPrjCompatNWMPDetail.Where(w => w.ProjectId == _psi.ProjectId)
                                       .Select(x => new { x.PrjCompatNWMPId, x.ProjectId, x.NWMPProgrammeId }).ToList();
                ViewBag.CompatNWMPDetail = _compatnwmpdetail;

                var _compatsdgdetail = _db.CcModPrjCompatSDGDetail.Where(w => w.ProjectId == _psi.ProjectId)
                                       .Select(x => new { x.SDGCompabilityId, x.ProjectId, x.SDGGoalId }).ToList();
                ViewBag.CompatSDGDetail = _compatsdgdetail;

                var _compatsdgindidetail = _db.CcModPrjCompatSDGIndiDetail.Where(w => w.ProjectId == _psi.ProjectId)
                                       .Select(x => new { x.SDGIndicatorDetailId, x.ProjectId, x.SDGIndicatorId }).ToList();
                ViewBag.CompatSDGIndiDetail = _compatsdgindidetail;

                var _bdp2100goaldetail = _db.CcModBDP2100GoalDetail.Where(w => w.ProjectId == _psi.ProjectId)
                                       .Select(x => new { x.DeltaGoalDetailId, x.ProjectId, x.DeltPlan2100GoalId }).ToList();
                ViewBag.BDP2100GoalDetail = _bdp2100goaldetail;

                var _gpwmgrouptype = _db.CcModGPWMGroupTypeDetail.Where(w => w.ProjectId == _psi.ProjectId)
                                       .Select(x => new { x.GPWMGroupTypeDetailId, x.ProjectId, x.GPWMGroupTypeId }).ToList();
                ViewBag.GPWMGroupType = _gpwmgrouptype;
            }

            return View();
        }

        private void LoadDropdownData()
        {
            #region Dropdown Data Loading
            ViewBag.LookUpAdminBndDistrict = _db.LookUpAdminBndDistrict.ToList();
            //ViewBag.LookUpAdminBndUpazila = _db.LookUpAdminBndUpazila.ToList();
            //ViewBag.LookUpAdminBndUnion = _db.LookUpAdminBndUnion.ToList();

            ViewBag.LookUpCcModHydroRegion = _db.LookUpCcModHydroRegion.ToList();
            ViewBag.LookUpCcModDeltPlan2100HotSpot = _db.LookUpCcModDeltPlan2100HotSpot.ToList();
            ViewBag.LookUpCcModHydroSystem = _db.LookUpCcModHydroSystem.ToList();

            ViewBag.LookUpCcModTypeOfFlood = _db.LookUpCcModTypeOfFlood.ToList();
            ViewBag.FloodFrequencyId = _db.LookUpCcModFloodFrequency.ToList();
            ViewBag.DesignSubmittedParameterId = _db.LookUpCcModDesignSubmitParam.ToList();

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
            ViewBag.RecommendationId = _db.LookUpCcModRecommendation.ToList();
            #endregion
        }

        #region Dropdown and General Info Save
        private ProjectStatusInfo GetProjectId(string projectTypeId)
        {
            var result = _db.CcModAppProjectCommonDetail
                    .Where(w => w.ProjectTypeId == projectTypeId.ToInt() && w.AppSubmissionId == 0)
                    .Select(x => new ProjectStatusInfo
                    {
                        ProjectId = x.ProjectId,
                        AppSubmissionId = x.AppSubmissionId
                    }).FirstOrDefault();

            return result;
        }

        //form/GeneralInfoSave :: gis
        [HttpPost]
        public JsonResult gis(CcModAppProjectCommonDetail _pcd)
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
            long ProjectID = 0;
            int result = 0;

            try
            {
                if (_pcd != null && _pcd.ProjectTypeId != 0)
                {
                    using (var dbContextTransaction = _db.Database.BeginTransaction())
                    {
                        try
                        {
                            _pcd.UserId = ui.UserID;
                            _pcd.AppSubmissionId = 0;
                            _pcd.ApplicationStateId = 1; //should come from database

                            _db.CcModAppProjectCommonDetail.Add(_pcd);
                            result = _db.SaveChanges();

                            if (result > 0)
                            {
                                ProjectID = _pcd.ProjectId;
                                dbContextTransaction.Commit();

                                noti = new Notification
                                {
                                    id = _pcd.ProjectId.ToString(),
                                    status = "success",
                                    message = "General information has been saved successfully."
                                };
                            }
                            else
                            {
                                dbContextTransaction.Rollback();

                                noti = new Notification
                                {
                                    id = _pcd.ProjectId.ToString(),
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
                                id = _pcd.ProjectId.ToString(),
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
        #endregion

        #region Project Location
        //form/ProjectLocationDataSave :: plds
        [HttpPost]
        public JsonResult plds(CcModPrjLocationDetail _plds)
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
            int result = 0;

            try
            {
                if (_plds.ProjectId != 0)
                {
                    using var dbContextTransaction = _db.Database.BeginTransaction();
                    if (_plds.LocationId != 0)
                    {
                        _db.Entry(_plds).State = EntityState.Modified;
                        result = _db.SaveChanges();

                        if (result > 0)
                        {
                            dbContextTransaction.Commit();

                            noti = new Notification
                            {
                                id = _plds.LocationId.ToString(),
                                status = "success",
                                message = "Project location information has been updated successfully."
                            };
                        }
                        else
                        {
                            dbContextTransaction.Rollback();

                            noti = new Notification
                            {
                                id = _plds.LocationId.ToString(),
                                status = "error",
                                message = "Project location not updated."
                            };
                        }
                    }
                    else
                    {
                        try
                        {
                            if (!string.IsNullOrEmpty(_plds.DistrictGeoCode))
                            {
                                _db.CcModPrjLocationDetail.Add(_plds);
                                result = _db.SaveChanges();

                                if (result > 0)
                                {
                                    dbContextTransaction.Commit();

                                    noti = new Notification
                                    {
                                        id = _plds.LocationId.ToString(),
                                        status = "success",
                                        message = "Project location information has been saved successfully."
                                    };
                                }
                                else
                                {
                                    dbContextTransaction.Rollback();

                                    noti = new Notification
                                    {
                                        id = _plds.LocationId.ToString(),
                                        status = "error",
                                        message = "Project location not saved."
                                    };
                                }
                            }
                            else
                            {
                                noti = new Notification
                                {
                                    id = _plds.LocationId.ToString(),
                                    status = "error",
                                    message = "Please select project district."
                                };
                            }
                        }
                        catch (Exception ex)
                        {
                            dbContextTransaction.Rollback();
                            var message = ch.ExtractInnerException(ex);

                            noti = new Notification
                            {
                                id = _plds.LocationId.ToString(),
                                status = "error",
                                message = "Transaction has been rollbacked. " + message
                            };
                        }
                    }
                }
                else
                {
                    noti = new Notification
                    {
                        id = "0",
                        status = "error",
                        message = "Please save general information first."
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
                    message = message
                };
            }

            return Json(noti);
        }

        //form/GetHydroSystemDetail :: get_pld
        [HttpGet]
        public JsonResult get_pld(long project_id)
        {
            try
            {
                var _details = (from d in _db.CcModPrjLocationDetail
                                join district in _db.LookUpAdminBndDistrict on d.DistrictGeoCode equals district.DistrictGeoCode into dist
                                from ds in dist.DefaultIfEmpty()
                                join upazila in _db.LookUpAdminBndUpazila on d.UpazilaGeoCode equals upazila.UpazilaGeoCode into upaz
                                from up in upaz.DefaultIfEmpty()
                                join union in _db.LookUpAdminBndUnion on d.UnionGeoCode equals union.UnionGeoCode into unio
                                from un in unio.DefaultIfEmpty()
                                where d.ProjectId != null && d.ProjectId == project_id
                                select new
                                {
                                    d.LocationId,
                                    d.ProjectId,
                                    d.DistrictGeoCode,
                                    ds.DistrictName,
                                    d.UpazilaGeoCode,
                                    up.UpazilaName,
                                    d.UnionGeoCode,
                                    un.UnionName,
                                    Latitude = string.IsNullOrEmpty(d.Latitude) ? string.Empty : d.Latitude,
                                    Longitude = string.IsNullOrEmpty(d.Longitude) ? string.Empty : d.Longitude
                                }).OrderBy(o => o.LocationId).ToList();

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
        #endregion

        #region Hydrological System
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
                        if (_hsd.HydroSysDetailId != 0)
                        {
                            _db.Entry(_hsd).State = EntityState.Modified;
                            result = _db.SaveChanges();

                            if (result > 0)
                            {
                                dbContextTransaction.Commit();

                                noti = new Notification
                                {
                                    id = _hsd.HydroSysDetailId.ToString(),
                                    status = "success",
                                    message = "Hydrological system information has been updated successfully."
                                };
                            }
                            else
                            {
                                dbContextTransaction.Rollback();

                                noti = new Notification
                                {
                                    id = _hsd.HydroSysDetailId.ToString(),
                                    status = "error",
                                    message = "Hydrological system information not updated."
                                };
                            }
                        }
                        else
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

        //form/GetHydroSystemDetail :: get_hsd
        [HttpGet]
        public JsonResult get_hsd(long project_id)
        {
            //List<CcModHydroSystemDetail> _details = new List<CcModHydroSystemDetail>();
            //_details = _db.CcModHydroSystemDetail.Where(w => w.ProjectId != null && w.ProjectId == project_id).ToList();

            try
            {
                var _details = (from d in _db.CcModHydroSystemDetail
                                join l in _db.LookUpCcModHydroSystem on d.HydroSystemCategoryId equals l.HydroSystemCategoryId
                                where d.ProjectId != null && d.ProjectId == project_id
                                select new
                                {
                                    d.HydroSysDetailId,
                                    d.ProjectId,
                                    d.HydroSystemCategoryId,
                                    l.HydroSystemCategory,
                                    d.NameOfHydroSystem,
                                    d.HydroSystemLengthArea,
                                    d.HydroSystemUnit
                                }).OrderBy(o => o.HydroSysDetailId).ToList();

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
        #endregion

        #region Flood Frequency
        //form/FloodFrequencyDetailSave :: ffds
        [HttpPost]
        public JsonResult ffds(CcModFloodFrequencyDetail _ffd)
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
            int result = 0;

            try
            {
                if (_ffd != null && _ffd.ProjectId != 0)
                {
                    using (var dbContextTransaction = _db.Database.BeginTransaction())
                    {
                        if (_ffd.FloodFrequencyDetailId != 0)
                        {
                            _db.Entry(_ffd).State = EntityState.Modified;
                            result = _db.SaveChanges();

                            if (result > 0)
                            {
                                dbContextTransaction.Commit();

                                noti = new Notification
                                {
                                    id = _ffd.FloodFrequencyId.ToString(),
                                    status = "success",
                                    message = "Flood frequency information has been updated successfully."
                                };
                            }
                            else
                            {
                                dbContextTransaction.Rollback();

                                noti = new Notification
                                {
                                    id = _ffd.FloodFrequencyId.ToString(),
                                    status = "error",
                                    message = "Flood frequency information not updated."
                                };
                            }
                        }
                        else
                        {
                            try
                            {
                                _db.CcModFloodFrequencyDetail.Add(_ffd);
                                result = _db.SaveChanges();

                                if (result > 0)
                                {
                                    dbContextTransaction.Commit();

                                    noti = new Notification
                                    {
                                        id = _ffd.FloodFrequencyId.ToString(),
                                        status = "success",
                                        message = "Flood frequency information has been saved successfully."
                                    };
                                }
                                else
                                {
                                    dbContextTransaction.Rollback();

                                    noti = new Notification
                                    {
                                        id = _ffd.FloodFrequencyId.ToString(),
                                        status = "error",
                                        message = "Flood frequency information not saved."
                                    };
                                }
                            }
                            catch (Exception ex)
                            {
                                dbContextTransaction.Rollback();
                                var message = ch.ExtractInnerException(ex);

                                noti = new Notification
                                {
                                    id = _ffd.FloodFrequencyId.ToString(),
                                    status = "error",
                                    message = "Transaction has been rollbacked. " + message
                                };
                            }
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

        //form/GetFloodFrequencyDetail :: get_ffd
        [HttpGet]
        public JsonResult get_ffd(long project_id)
        {
            //List<CcModHydroSystemDetail> _details = new List<CcModHydroSystemDetail>();
            //_details = _db.CcModHydroSystemDetail.Where(w => w.ProjectId != null && w.ProjectId == project_id).ToList();

            try
            {
                var _details = (from d in _db.CcModFloodFrequencyDetail
                                join l in _db.LookUpCcModFloodFrequency on d.FloodFrequencyId equals l.FloodFrequencyId
                                where d.ProjectId != null && d.ProjectId == project_id
                                select new
                                {
                                    d.FloodFrequencyDetailId,
                                    d.ProjectId,
                                    d.FloodFrequencyId,
                                    l.FloodFrequency,
                                    d.FloodFrequencyLevel
                                }).ToList();

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
        #endregion

        #region Irrigated Crop
        //form/IrrigatedCropAreaSave :: icas
        [HttpPost]
        public JsonResult icas(CcModPrjIrrigCropAreaDetail _ica)
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
            int result = 0;

            try
            {
                if (_ica != null && _ica.ProjectId != 0)
                {
                    using (var dbContextTransaction = _db.Database.BeginTransaction())
                    {
                        if (_ica.IrrigatedCropId != 0)
                        {
                            _db.Entry(_ica).State = EntityState.Modified;
                            result = _db.SaveChanges();

                            if (result > 0)
                            {
                                dbContextTransaction.Commit();

                                noti = new Notification
                                {
                                    id = _ica.IrrigatedCropId.ToString(),
                                    status = "success",
                                    message = "Irrigated crop area information has been updated successfully."
                                };
                            }
                            else
                            {
                                dbContextTransaction.Rollback();

                                noti = new Notification
                                {
                                    id = _ica.IrrigatedCropId.ToString(),
                                    status = "error",
                                    message = "Irrigated crop area information not updated."
                                };
                            }
                        }
                        else
                        {
                            try
                            {
                                _db.CcModPrjIrrigCropAreaDetail.Add(_ica);
                                result = _db.SaveChanges();

                                if (result > 0)
                                {
                                    dbContextTransaction.Commit();

                                    noti = new Notification
                                    {
                                        id = _ica.IrrigatedCropId.ToString(),
                                        status = "success",
                                        message = "Irrigated crop area information has been saved successfully."
                                    };
                                }
                                else
                                {
                                    dbContextTransaction.Rollback();

                                    noti = new Notification
                                    {
                                        id = _ica.IrrigatedCropId.ToString(),
                                        status = "error",
                                        message = "Irrigated crop area information not saved."
                                    };
                                }
                            }
                            catch (Exception ex)
                            {
                                dbContextTransaction.Rollback();
                                var message = ch.ExtractInnerException(ex);

                                noti = new Notification
                                {
                                    id = _ica.IrrigatedCropId.ToString(),
                                    status = "error",
                                    message = "Transaction has been rollbacked. " + message
                                };
                            }
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

        //form/GetIrrigatedCropArea :: get_ica
        [HttpGet]
        public JsonResult get_ica(long project_id)
        {
            //List<CcModHydroSystemDetail> _details = new List<CcModHydroSystemDetail>();
            //_details = _db.CcModHydroSystemDetail.Where(w => w.ProjectId != null && w.ProjectId == project_id).ToList();

            try
            {
                var _details = (from d in _db.CcModPrjIrrigCropAreaDetail
                                    //join l in _db.Irri on d.FloodFrequencyId equals l.FloodFrequencyId
                                where d.ProjectId != null && d.ProjectId == project_id
                                select new
                                {
                                    d.IrrigatedCropId,
                                    d.ProjectId,
                                    d.CropName,
                                    d.Area
                                }).ToList();

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
        #endregion

        #region Analyze Options to fulfill objective
        //form/AnalyzeOptionsFulfillObjectiveSave :: aofos
        [HttpPost]
        public JsonResult aofos(CcModAnalyzeOptionsDetail _aofd)
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
            int result = 0;

            try
            {
                if (_aofd != null && _aofd.ProjectId != 0)
                {
                    using (var dbContextTransaction = _db.Database.BeginTransaction())
                    {
                        if (_aofd.AnalyzeOptionsId != 0)
                        {
                            _db.Entry(_aofd).State = EntityState.Modified;
                            result = _db.SaveChanges();

                            if (result > 0)
                            {
                                dbContextTransaction.Commit();

                                noti = new Notification
                                {
                                    id = _aofd.AnalyzeOptionsId.ToString(),
                                    status = "success",
                                    message = "Information has been updated successfully."
                                };
                            }
                            else
                            {
                                dbContextTransaction.Rollback();

                                noti = new Notification
                                {
                                    id = _aofd.AnalyzeOptionsId.ToString(),
                                    status = "error",
                                    message = "Information not updated."
                                };
                            }
                        }
                        else
                        {
                            try
                            {
                                _db.CcModAnalyzeOptionsDetail.Add(_aofd);
                                result = _db.SaveChanges();

                                if (result > 0)
                                {
                                    dbContextTransaction.Commit();

                                    noti = new Notification
                                    {
                                        id = _aofd.AnalyzeOptionsId.ToString(),
                                        status = "success",
                                        message = "Information has been saved successfully."
                                    };
                                }
                                else
                                {
                                    dbContextTransaction.Rollback();

                                    noti = new Notification
                                    {
                                        id = _aofd.AnalyzeOptionsId.ToString(),
                                        status = "error",
                                        message = "Information not saved."
                                    };
                                }
                            }
                            catch (Exception ex)
                            {
                                dbContextTransaction.Rollback();
                                var message = ch.ExtractInnerException(ex);

                                noti = new Notification
                                {
                                    id = _aofd.AnalyzeOptionsId.ToString(),
                                    status = "error",
                                    message = "Transaction has been rollbacked. " + message
                                };
                            }
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

        //form/GetAnalyzeOptionsFulfillObjective :: get_aofo
        [HttpGet]
        public JsonResult get_aofo(long project_id)
        {
            try
            {
                var _details = (from d in _db.CcModAnalyzeOptionsDetail
                                where d.ProjectId != null && d.ProjectId == project_id
                                select new
                                {
                                    d.AnalyzeOptionsId,
                                    d.ProjectId,
                                    d.OptionNumber,
                                    d.AnalyzeDescription,
                                    d.AnalyzeRemarks
                                }).ToList();

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
        #endregion

        #region Design Submitted with Project Document
        //form/DesignSubmittedWithProjectDocumentSave :: dswpds
        [HttpPost]
        public JsonResult dswpds(CcModDesignSubmitDetail _dspd)
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
            int result = 0;

            try
            {
                if (_dspd != null && _dspd.ProjectId != 0)
                {
                    using (var dbContextTransaction = _db.Database.BeginTransaction())
                    {
                        if (_dspd.DesignSubmittedId != 0)
                        {
                            _db.Entry(_dspd).State = EntityState.Modified;
                            result = _db.SaveChanges();

                            if (result > 0)
                            {
                                dbContextTransaction.Commit();

                                noti = new Notification
                                {
                                    id = _dspd.DesignSubmittedId.ToString(),
                                    status = "success",
                                    message = "Design submitted with project document information has been updated successfully."
                                };
                            }
                            else
                            {
                                dbContextTransaction.Rollback();

                                noti = new Notification
                                {
                                    id = _dspd.DesignSubmittedId.ToString(),
                                    status = "error",
                                    message = "Design submitted with project document information not updated."
                                };
                            }
                        }
                        else
                        {
                            try
                            {
                                _db.CcModDesignSubmitDetail.Add(_dspd);
                                result = _db.SaveChanges();

                                if (result > 0)
                                {
                                    dbContextTransaction.Commit();

                                    noti = new Notification
                                    {
                                        id = _dspd.DesignSubmittedId.ToString(),
                                        status = "success",
                                        message = "Design submitted with project document information has been saved successfully."
                                    };
                                }
                                else
                                {
                                    dbContextTransaction.Rollback();

                                    noti = new Notification
                                    {
                                        id = _dspd.DesignSubmittedId.ToString(),
                                        status = "error",
                                        message = "Design submitted with project document information not saved."
                                    };
                                }
                            }
                            catch (Exception ex)
                            {
                                dbContextTransaction.Rollback();
                                var message = ch.ExtractInnerException(ex);

                                noti = new Notification
                                {
                                    id = _dspd.DesignSubmittedId.ToString(),
                                    status = "error",
                                    message = "Transaction has been rollbacked. " + message
                                };
                            }
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

        //form/GetAnalyzeOptionsFulfillObjective :: get_aofo
        [HttpGet]
        public JsonResult get_dswpd(long project_id)
        {
            try
            {
                var _details = (from d in _db.CcModDesignSubmitDetail
                                join l in _db.LookUpCcModDesignSubmitParam on d.DesignSubmittedParameterId equals l.DesignSubmittedParameterId
                                where d.ProjectId != null && d.ProjectId == project_id
                                select new
                                {
                                    d.DesignSubmittedId,
                                    d.ProjectId,
                                    d.DesignSubmittedParameterId,
                                    l.ParameterName,
                                    dswpdYN = d.YesNoId == 0 ? "No" : "Yes",
                                    d.DesignSubmitApplicantCmt,
                                    d.DesignSubmitAuthorityCmt
                                }).ToList();

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
        #endregion

        #region EIA
        //form/EIADetailSave :: eiads
        [HttpPost]
        public JsonResult eiads(CcModPrjEIADetail _eia)
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
            int result = 0;

            try
            {
                if (_eia != null && _eia.ProjectId != 0)
                {
                    using (var dbContextTransaction = _db.Database.BeginTransaction())
                    {
                        if (_eia.EIAId != 0)
                        {
                            _db.Entry(_eia).State = EntityState.Modified;
                            result = _db.SaveChanges();

                            if (result > 0)
                            {
                                dbContextTransaction.Commit();

                                noti = new Notification
                                {
                                    id = _eia.EIAId.ToString(),
                                    status = "success",
                                    message = "Environmental Impact Assessment (EIA) information has been updated successfully."
                                };
                            }
                            else
                            {
                                dbContextTransaction.Rollback();

                                noti = new Notification
                                {
                                    id = _eia.EIAId.ToString(),
                                    status = "error",
                                    message = "Environmental Impact Assessment (EIA) information not updated."
                                };
                            }
                        }
                        else
                        {
                            try
                            {
                                _db.CcModPrjEIADetail.Add(_eia);
                                result = _db.SaveChanges();

                                if (result > 0)
                                {
                                    dbContextTransaction.Commit();

                                    noti = new Notification
                                    {
                                        id = _eia.EIAId.ToString(),
                                        status = "success",
                                        message = "Environmental Impact Assessment (EIA) information has been saved successfully."
                                    };
                                }
                                else
                                {
                                    dbContextTransaction.Rollback();

                                    noti = new Notification
                                    {
                                        id = _eia.EIAId.ToString(),
                                        status = "error",
                                        message = "Environmental Impact Assessment (EIA) information not saved."
                                    };
                                }
                            }
                            catch (Exception ex)
                            {
                                dbContextTransaction.Rollback();
                                var message = ch.ExtractInnerException(ex);

                                noti = new Notification
                                {
                                    id = _eia.EIAId.ToString(),
                                    status = "error",
                                    message = "Transaction has been rollbacked. " + message
                                };
                            }
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

        //form/GetAnalyzeOptionsFulfillObjective :: get_eiad
        [HttpGet]
        public JsonResult get_eiad(long project_id)
        {
            try
            {
                var _details = (from d in _db.CcModPrjEIADetail
                                join l in _db.LookUpCcModEIAParameter on d.EIAParameterId equals l.EIAParameterId
                                where d.ProjectId != null && d.ProjectId == project_id
                                select new
                                {
                                    d.EIAId,
                                    d.ProjectId,
                                    d.EIAParameterId,
                                    l.ParameterName,
                                    d.PreProjectSituation,
                                    d.PostProjectSituation,
                                    d.PositiveNegativeImpact,
                                    d.MitigationPlan
                                }).ToList();

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
        #endregion

        #region SIA
        //form/SIADetailSave :: eiads
        [HttpPost]
        public JsonResult siads(CcModPrjSIADetail _sia)
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
            int result = 0;

            try
            {
                if (_sia != null && _sia.ProjectId != 0)
                {
                    using (var dbContextTransaction = _db.Database.BeginTransaction())
                    {
                        if (_sia.SIAId != 0)
                        {
                            _db.Entry(_sia).State = EntityState.Modified;
                            result = _db.SaveChanges();

                            if (result > 0)
                            {
                                dbContextTransaction.Commit();

                                noti = new Notification
                                {
                                    id = _sia.SIAId.ToString(),
                                    status = "success",
                                    message = "Social Impact Assessment (EIA) information has been updated successfully."
                                };
                            }
                            else
                            {
                                dbContextTransaction.Rollback();

                                noti = new Notification
                                {
                                    id = _sia.SIAId.ToString(),
                                    status = "error",
                                    message = "Social Impact Assessment (EIA) information not updated."
                                };
                            }
                        }
                        else
                        {
                            try
                            {
                                _db.CcModPrjSIADetail.Add(_sia);
                                result = _db.SaveChanges();

                                if (result > 0)
                                {
                                    dbContextTransaction.Commit();

                                    noti = new Notification
                                    {
                                        id = _sia.SIAId.ToString(),
                                        status = "success",
                                        message = "Social Impact Assessment (EIA) information has been saved successfully."
                                    };
                                }
                                else
                                {
                                    dbContextTransaction.Rollback();

                                    noti = new Notification
                                    {
                                        id = _sia.SIAId.ToString(),
                                        status = "error",
                                        message = "Social Impact Assessment (EIA) information not saved."
                                    };
                                }
                            }
                            catch (Exception ex)
                            {
                                dbContextTransaction.Rollback();
                                var message = ch.ExtractInnerException(ex);

                                noti = new Notification
                                {
                                    id = _sia.SIAId.ToString(),
                                    status = "error",
                                    message = "Transaction has been rollbacked. " + message
                                };
                            }
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

        //form/GetAnalyzeOptionsFulfillObjective :: get_eiad
        [HttpGet]
        public JsonResult get_siad(long project_id)
        {
            try
            {
                var _details = (from d in _db.CcModPrjSIADetail
                                join l in _db.LookUpCcModSIAParameter on d.SIAParameterId equals l.SIAParameterId
                                where d.ProjectId != null && d.ProjectId == project_id
                                select new
                                {
                                    d.SIAId,
                                    d.ProjectId,
                                    d.SIAParameterId,
                                    l.SIAParameterName,
                                    d.PreProjectSituation,
                                    d.PostProjectSituation,
                                    d.PositiveNegativeImpact,
                                    d.MitigationPlan
                                }).ToList();

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
        #endregion

        #region Economical & Financial Analysis
        //form/EconomicalFinancialAnalysisSave :: efas
        [HttpPost]
        public JsonResult efas(CcModPrjEcoFinAnalysisDetail _efas)
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
            int result = 0;

            try
            {
                if (_efas != null && _efas.ProjectId != 0)
                {
                    using (var dbContextTransaction = _db.Database.BeginTransaction())
                    {
                        if (_efas.EconomicalAndFinancialId != 0)
                        {
                            _db.Entry(_efas).State = EntityState.Modified;
                            result = _db.SaveChanges();

                            if (result > 0)
                            {
                                dbContextTransaction.Commit();

                                noti = new Notification
                                {
                                    id = _efas.EconomicalAndFinancialId.ToString(),
                                    status = "success",
                                    message = "Economical & financial analysis information has been updated successfully."
                                };
                            }
                            else
                            {
                                dbContextTransaction.Rollback();

                                noti = new Notification
                                {
                                    id = _efas.EconomicalAndFinancialId.ToString(),
                                    status = "error",
                                    message = "Economical & financial analysis information not updated."
                                };
                            }
                        }
                        else
                        {
                            try
                            {
                                _db.CcModPrjEcoFinAnalysisDetail.Add(_efas);
                                result = _db.SaveChanges();

                                if (result > 0)
                                {
                                    dbContextTransaction.Commit();

                                    noti = new Notification
                                    {
                                        id = _efas.EconomicalAndFinancialId.ToString(),
                                        status = "success",
                                        message = "Economical & financial analysis information has been saved successfully."
                                    };
                                }
                                else
                                {
                                    dbContextTransaction.Rollback();

                                    noti = new Notification
                                    {
                                        id = _efas.EconomicalAndFinancialId.ToString(),
                                        status = "error",
                                        message = "Economical & financial analysis information not saved."
                                    };
                                }
                            }
                            catch (Exception ex)
                            {
                                dbContextTransaction.Rollback();
                                var message = ch.ExtractInnerException(ex);

                                noti = new Notification
                                {
                                    id = _efas.EconomicalAndFinancialId.ToString(),
                                    status = "error",
                                    message = "Transaction has been rollbacked. " + message
                                };
                            }
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

        //form/GerEconomicalFinancialAnalysis :: get_efad
        [HttpGet]
        public JsonResult get_efad(long project_id)
        {
            try
            {
                var _details = (from d in _db.CcModPrjEcoFinAnalysisDetail
                                join l in _db.LookUpCcModEcoAndFinancial on d.EcoAndFinancialParamId equals l.EcoAndFinancialParamId
                                where d.ProjectId != null && d.ProjectId == project_id
                                select new
                                {
                                    d.EconomicalAndFinancialId,
                                    d.ProjectId,
                                    d.EcoAndFinancialParamId,
                                    l.EcoAndFinancialParamName,
                                    EcoAndFinancialParamUnit = l.EcoAndFinancialUnit,
                                    d.EcoAndFinancialApplicantCmt,
                                    d.EcoAndFinancialAuthorityCmt
                                }).ToList();

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
        #endregion

        #region Form 3.1 One to One Save > Flood Control Management > Technical Info
        //Tehnical Info
        //form/Form31TechInfoOneToOneSave :: f31otos
        [HttpPost]
        public JsonResult f31tiotos(Form31TechInfo _form31TechInfo)
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
            using var dbContextTransaction = _db.Database.BeginTransaction();
            int result = 0;
            Int64 ProjectId = _form31TechInfo.CommonDetail.ProjectId;

            try
            {
                if (_form31TechInfo != null && ProjectId != 0)
                {
                    try
                    {
                        CcModAppProjectCommonDetail pcd = _db.CcModAppProjectCommonDetail.Find(ProjectId);

                        if (pcd == null)
                        {
                            noti = new Notification
                            {
                                id = "",
                                status = "error",
                                message = "General information is missing. Please enter general information first."
                            };

                            goto Finish;
                        }

                        #region getting client end data
                        CcModAppProjectCommonDetail CommonDetail = _form31TechInfo.CommonDetail;
                        CcModAppProject_31_IndvDetail Project31Indv = _form31TechInfo.Project31Indv;

                        List<CcModBDP2100HotSpotDetail> BDP2100HotSpot = _form31TechInfo.BDP2100HotSpot;
                        List<CcModPrjHydroRegionDetail> HydroRegion = _form31TechInfo.HydroRegion;
                        List<CcModPrjTypesOfFloodDetail> TypesOfFlood = _form31TechInfo.TypesOfFlood;
                        #endregion

                        #region Common Detail Data Binding
                        pcd.AnnualRainFallLast1Year = CommonDetail.AnnualRainFallLast1Year;
                        pcd.AnnualRainFallLast2Years = CommonDetail.AnnualRainFallLast2Years;
                        pcd.AnnualRainFallLast3Years = CommonDetail.AnnualRainFallLast3Years;
                        pcd.AnnualRainFallLast4Years = CommonDetail.AnnualRainFallLast4Years;
                        pcd.AnnualRainFallLast5Years = CommonDetail.AnnualRainFallLast5Years;
                        pcd.IssueChallageProblem = CommonDetail.IssueChallageProblem;
                        pcd.YesNoStakeId = CommonDetail.YesNoStakeId;
                        pcd.DiscussWithStakeApplicantCmt = CommonDetail.DiscussWithStakeApplicantCmt;
                        pcd.DiscussWithStakeAuthorityCmt = CommonDetail.DiscussWithStakeAuthorityCmt;
                        pcd.DiscussWithStakePosFeedback = CommonDetail.DiscussWithStakePosFeedback;
                        pcd.DiscussWithStakeNegFeedback = CommonDetail.DiscussWithStakeNegFeedback;
                        //pcd.DiscussWithStakeParticipntLst //file
                        //pcd.DiscussWithStakeMeetingMin //file
                        pcd.AnalyzeOptYesNoId = CommonDetail.AnalyzeOptYesNoId;
                        pcd.AnalyzeOptionsApplicantCmt = CommonDetail.AnalyzeOptionsApplicantCmt;
                        pcd.AnalyzeOptionsAuthorityCmt = CommonDetail.AnalyzeOptionsAuthorityCmt;
                        pcd.EnvAndSocialYesNoId = CommonDetail.EnvAndSocialYesNoId;
                        pcd.EnvAndSocialApplicantCmt = CommonDetail.EnvAndSocialApplicantCmt;
                        pcd.EnvAndSocialAuthorityCmt = CommonDetail.EnvAndSocialAuthorityCmt;
                        #endregion

                        _db.Entry(pcd).State = EntityState.Modified;
                        result = _db.SaveChanges();

                        if (result > 0)
                        {
                            CcModAppProject_31_IndvDetail p31i = _db.CcModAppProject_31_IndvDetail.Where(w => w.ProjectId == ProjectId).FirstOrDefault();

                            if (p31i == null)
                            {
                                _db.CcModAppProject_31_IndvDetail.Add(Project31Indv);

                                if (BDP2100HotSpot.Count > 0)
                                    _db.CcModBDP2100HotSpotDetail.AddRange(BDP2100HotSpot);

                                if (HydroRegion.Count > 0)
                                    _db.CcModPrjHydroRegionDetail.AddRange(HydroRegion);

                                if (TypesOfFlood.Count > 0)
                                    _db.CcModPrjTypesOfFloodDetail.AddRange(TypesOfFlood);

                                result = _db.SaveChanges();

                                if (result > 0)
                                {
                                    dbContextTransaction.Commit();

                                    noti = new Notification
                                    {
                                        id = pcd.ProjectId.ToString(),
                                        status = "success",
                                        message = "Information has been save successfully. "
                                    };

                                    goto Finish;
                                }
                                else
                                {
                                    goto Rollback;
                                }
                            }
                            else
                            {
                                #region Project 31 Indvidual Data Binding
                                p31i.Project31IndvId = Project31Indv.Project31IndvId;
                                p31i.ProjectId = Project31Indv.ProjectId;
                                p31i.ConnectivityAmidWaterland = Project31Indv.ConnectivityAmidWaterland;
                                p31i.CatchmentArea = Project31Indv.CatchmentArea;
                                p31i.HighestFloodLevel = Project31Indv.HighestFloodLevel;
                                p31i.MaximumDischarge = Project31Indv.MaximumDischarge;
                                p31i.DrainageConditionId = Project31Indv.DrainageConditionId;
                                p31i.WaterSalinity = Project31Indv.WaterSalinity;
                                p31i.WaterDO = Project31Indv.WaterDO;
                                p31i.WaterTDS = Project31Indv.WaterTDS;
                                p31i.WaterPhLevel = Project31Indv.WaterPhLevel;
                                p31i.HighLandPercent = Project31Indv.HighLandPercent;
                                p31i.MediumHighLandPercent = Project31Indv.MediumHighLandPercent;
                                p31i.MediumLowLandPercent = Project31Indv.MediumLowLandPercent;
                                p31i.LowLandPercent = Project31Indv.LowLandPercent;
                                p31i.VeryLowLandPercent = Project31Indv.VeryLowLandPercent;
                                p31i.CultivableCrops = Project31Indv.CultivableCrops;
                                p31i.CropProduction = Project31Indv.CropProduction;
                                p31i.FishProduction = Project31Indv.FishProduction;
                                p31i.FishDiversity = Project31Indv.FishDiversity;
                                p31i.FishMigration = Project31Indv.FishMigration;
                                p31i.FloraAndFauna = Project31Indv.FloraAndFauna;
                                p31i.LandLessPeoplePercentage = Project31Indv.LandLessPeoplePercentage;
                                p31i.SmallFarmerPercentage = Project31Indv.SmallFarmerPercentage;
                                p31i.AvgMonthlyIncome = Project31Indv.AvgMonthlyIncome;
                                p31i.UseOfToolsYesNoId = Project31Indv.UseOfToolsYesNoId;
                                p31i.ToolsApplicantComments = Project31Indv.ToolsApplicantComments;
                                p31i.ToolsAuthorityComments = Project31Indv.ToolsAuthorityComments;
                                #endregion

                                _db.Entry(p31i).State = EntityState.Modified;
                                result = _db.SaveChanges();

                                if (result > 0)
                                {
                                    dbContextTransaction.Commit();

                                    noti = new Notification
                                    {
                                        id = pcd.ProjectId.ToString(),
                                        status = "success",
                                        message = "Information has been save successfully. "
                                    };

                                    goto Finish;
                                }
                                else
                                {
                                    goto Rollback;
                                }
                            }
                        }
                        else
                        {
                            goto Rollback;
                        }
                    }
                    catch (Exception ex)
                    {
                        dbContextTransaction.Rollback();
                        var message = ch.ExtractInnerException(ex);

                        noti = new Notification
                        {
                            id = "", //_form31.HydroSysDetailId.ToString(),
                            status = "error",
                            message = "Saving data transaction has been rollbacked. " + message
                        };

                        goto Finish;
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

                goto Finish;
            }

        Rollback:
            dbContextTransaction.Rollback();

            noti = new Notification
            {
                id = "",
                status = "error",
                message = "Saving data transaction has been rollbacked."
            };

            goto Finish;

        Finish:
            return Json(noti);
        }

        //Deed Obligatory
        //form/Form31DeedObligatoryOneToOneSave :: f31otos        
        [HttpPost]
        public JsonResult f31dootos(Form31DeedObligatory _form31DeedObli)
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
            using var dbContextTransaction = _db.Database.BeginTransaction();
            int result = 0;
            Int64 ProjectId = _form31DeedObli.CommonDetail.ProjectId;
            Int64 Project31IndvId = _form31DeedObli.Project31Indv.Project31IndvId;

            try
            {
                if (_form31DeedObli != null && ProjectId != 0)
                {
                    try
                    {
                        CcModAppProjectCommonDetail pcd = _db.CcModAppProjectCommonDetail.Find(ProjectId);
                        CcModAppProject_31_IndvDetail p31id = _db.CcModAppProject_31_IndvDetail.Find(Project31IndvId); //.Where(w => w.ProjectId == ProjectId).FirstOrDefault();

                        if (pcd == null)
                        {
                            noti = new Notification
                            {
                                id = "",
                                status = "error",
                                message = "General information is missing. Please enter general information first."
                            };

                            goto Finish;
                        }

                        if (p31id == null)
                        {
                            noti = new Notification
                            {
                                id = "",
                                status = "error",
                                message = "Technical information is missing. Please enter Technical information first."
                            };

                            goto Finish;
                        }

                        #region getting client end data
                        CcModAppProjectCommonDetail CommonDetail = _form31DeedObli.CommonDetail;
                        CcModAppProject_31_IndvDetail Project31Indv = _form31DeedObli.Project31Indv;

                        List<CcModPrjCompatNWPDetail> CompatNWPDetail = _form31DeedObli.CompatNWPDetail;
                        List<CcModPrjCompatNWMPDetail> CompatNWMPDetail = _form31DeedObli.CompatNWMPDetail;
                        List<CcModPrjCompatSDGDetail> CompatSDGDetail = _form31DeedObli.CompatSDGDetail;
                        List<CcModPrjCompatSDGIndiDetail> CompatSDGIndiDetail = _form31DeedObli.CompatSDGIndiDetail;
                        List<CcModBDP2100GoalDetail> BDP2100GoalDetail = _form31DeedObli.BDP2100GoalDetail;
                        List<CcModGPWMGroupTypeDetail> GPWMGroupTypeDetail = _form31DeedObli.GPWMGroupTypeDetail;
                        #endregion

                        #region Common Detail Data Binding
                        pcd.CompatNWPYesNoId = CommonDetail.CompatNWPYesNoId;
                        pcd.CompatibilityNWPApplicantCmt = CommonDetail.CompatibilityNWPApplicantCmt;
                        pcd.CompatibilityNWPAuthorityCmt = CommonDetail.CompatibilityNWPAuthorityCmt;
                        //pcd.CompatibilityNWPDocLink //file
                        pcd.NWMPCompatYesNoId = CommonDetail.NWMPCompatYesNoId;
                        pcd.NWMPApplicantCmt = CommonDetail.NWMPApplicantCmt;
                        pcd.NWMPAuthorityCmt = CommonDetail.NWMPAuthorityCmt;
                        //pcd.NWMPDocLink //file
                        pcd.FYPYesNoId = CommonDetail.FYPYesNoId;
                        pcd.FYPApplicantCmt = CommonDetail.FYPApplicantCmt;
                        pcd.FYPAuthorityCmt = CommonDetail.FYPAuthorityCmt;
                        pcd.SDGYesNoId = CommonDetail.SDGYesNoId;
                        pcd.SDGApplicantCmt = CommonDetail.SDGApplicantCmt;
                        pcd.SDGAuthorityCmt = CommonDetail.SDGAuthorityCmt;
                        //pcd.SDGDocLink //file
                        pcd.DeltaPlanYesNoId = CommonDetail.DeltaPlanYesNoId;
                        pcd.DeltaPlan2100ApplicantCmt = CommonDetail.DeltaPlan2100ApplicantCmt;
                        pcd.DeltaPlan2100AuthorityCmt = CommonDetail.DeltaPlan2100AuthorityCmt;
                        //pcd.DeltaPlan2100DocLink //file
                        pcd.CostalZoneYesNoId = CommonDetail.CostalZoneYesNoId;
                        pcd.CostalZoneApplicantCmt = CommonDetail.CostalZoneApplicantCmt;
                        pcd.CostalZoneAuthorityCmt = CommonDetail.CostalZoneAuthorityCmt;
                        //pcd.CostalZoneDocLink //file.
                        pcd.AgriculturalYesNoId = CommonDetail.AgriculturalYesNoId;
                        pcd.AgriApplicantCmt = CommonDetail.AgriApplicantCmt;
                        pcd.AgriAuthorityCmt = CommonDetail.AgriAuthorityCmt;
                        //pcd.AgriDocLink //file
                        pcd.FisheriesYesNoId = CommonDetail.FisheriesYesNoId;
                        pcd.FisheriesApplicantCmt = CommonDetail.FisheriesApplicantCmt;
                        pcd.FisheriesAuthorityCmt = CommonDetail.FisheriesAuthorityCmt;
                        //pcd.FisheriesDocLink //file
                        pcd.IWRMYesNoId = CommonDetail.IWRMYesNoId;
                        pcd.IWRMApplicantCmt = CommonDetail.IWRMApplicantCmt;
                        pcd.IWRMAuthorityCmt = CommonDetail.IWRMAuthorityCmt;
                        pcd.GPWMYesNoId = CommonDetail.GPWMYesNoId;
                        pcd.GPWMApplicantCmt = CommonDetail.GPWMApplicantCmt;
                        pcd.GPWMAuthorityCmt = CommonDetail.GPWMAuthorityCmt;
                        pcd.FeasibilityYesNoId = CommonDetail.FeasibilityYesNoId;
                        pcd.FeasibilityApplicantCmt = CommonDetail.FeasibilityApplicantCmt;
                        pcd.FeasibilityAuthorityCmt = CommonDetail.FeasibilityAuthorityCmt;
                        pcd.SocialIssuesYesNoId = CommonDetail.SocialIssuesYesNoId;
                        pcd.SocialIssuesApplicantCmt = CommonDetail.SocialIssuesApplicantCmt;
                        pcd.SocialIssuesAuthorityCmt = CommonDetail.SocialIssuesAuthorityCmt;
                        #endregion

                        #region Project 31 Individual Data Binding
                        p31id.DuplicatYesNoId = Project31Indv.DuplicatYesNoId;
                        p31id.DuplicationApplicantComments = Project31Indv.DuplicationApplicantComments;
                        p31id.DuplicationAuthorityComments = Project31Indv.DuplicationAuthorityComments;
                        #endregion

                        _db.Entry(pcd).State = EntityState.Modified;
                        _db.Entry(p31id).State = EntityState.Modified;

                        if (CompatNWPDetail.Count > 0)
                            _db.CcModPrjCompatNWPDetail.AddRange(CompatNWPDetail);
                        if (CompatNWMPDetail.Count > 0)
                            _db.CcModPrjCompatNWMPDetail.AddRange(CompatNWMPDetail);
                        if (CompatSDGDetail.Count > 0)
                            _db.CcModPrjCompatSDGDetail.AddRange(CompatSDGDetail);
                        if (CompatSDGIndiDetail.Count > 0)
                            _db.CcModPrjCompatSDGIndiDetail.AddRange(CompatSDGIndiDetail);
                        if (BDP2100GoalDetail.Count > 0)
                            _db.CcModBDP2100GoalDetail.AddRange(BDP2100GoalDetail);
                        if (GPWMGroupTypeDetail.Count > 0)
                            _db.CcModGPWMGroupTypeDetail.AddRange(GPWMGroupTypeDetail);

                        result = _db.SaveChanges();

                        if (result > 0)
                        {
                            dbContextTransaction.Commit();

                            noti = new Notification
                            {
                                id = pcd.ProjectId.ToString(),
                                status = "success",
                                message = "Information has been saved successfully. "
                            };

                            goto Finish;
                        }
                    }
                    catch (Exception ex)
                    {
                        dbContextTransaction.Rollback();
                        var message = ch.ExtractInnerException(ex);

                        noti = new Notification
                        {
                            id = "", //_form31.HydroSysDetailId.ToString(),
                            status = "error",
                            message = "Saving data transaction has been rollbacked. " + message
                        };

                        goto Finish;
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

                goto Finish;
            }

        Rollback:
            dbContextTransaction.Rollback();

            noti = new Notification
            {
                id = "",
                status = "error",
                message = "Saving data transaction has been rollbacked."
            };

            goto Finish;

        Finish:
            return Json(noti);
        }

        //05 03 2020
        //Tehnical Info
        //form/Form31RecommandDetailSave :: f31rds > Administrative
        [HttpPost]
        public JsonResult f31rds(CcModAppProjectCommonDetail _recDetail)
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
            using var dbContextTransaction = _db.Database.BeginTransaction();
            int result = 0;
            Int64 ProjectId = _recDetail.ProjectId;

            try
            {
                if (_recDetail != null && ProjectId != 0)
                {
                    try
                    {
                        CcModAppProjectCommonDetail pcd = _db.CcModAppProjectCommonDetail.Find(ProjectId);

                        if (pcd == null)
                        {
                            noti = new Notification
                            {
                                id = "",
                                status = "error",
                                message = "General information is missing. Please enter general information first."
                            };

                            goto Finish;
                        }

                        #region Common Detail Data Binding for Recommendation
                        pcd.RecommendationId = _recDetail.RecommendationId;
                        pcd.RecommandCmt = _recDetail.RecommandCmt;
                        #endregion

                        _db.Entry(pcd).State = EntityState.Modified;
                        result = _db.SaveChanges();

                        if (result > 0)
                        {
                            dbContextTransaction.Commit();

                            noti = new Notification
                            {
                                id = pcd.ProjectId.ToString(),
                                status = "success",
                                message = "Recommendation information has been save successfully. "
                            };

                            goto Finish;
                        }
                        else
                        {
                            goto Rollback;
                        }
                    }
                    catch (Exception ex)
                    {
                        dbContextTransaction.Rollback();
                        var message = ch.ExtractInnerException(ex);

                        noti = new Notification
                        {
                            id = "", //_form31.HydroSysDetailId.ToString(),
                            status = "error",
                            message = "Saving data transaction has been rollbacked. " + message
                        };

                        goto Finish;
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

                goto Finish;
            }

        Rollback:
            dbContextTransaction.Rollback();

            noti = new Notification
            {
                id = "",
                status = "error",
                message = "Saving data transaction has been rollbacked."
            };

            goto Finish;

        Finish:
            return Json(noti);
        }
        #endregion

        //form/CommonDeleteMethod :: cdm
        [HttpPost]
        public JsonResult cdm(long id, long projectId, string modelName)
        {
            if (id != 0)
            {
                if (projectId != 0)
                {
                    using var dbContextTransaction = _db.Database.BeginTransaction();
                    try
                    {
                        switch (modelName)
                        {
                            case "CcModPrjLocationDetail":
                                CcModPrjLocationDetail _loc = _db.CcModPrjLocationDetail.Where(w => w.ProjectId == projectId && w.LocationId == id).FirstOrDefault();
                                _db.CcModPrjLocationDetail.Remove(_loc);
                                break;

                            case "CcModHydroSystemDetail":
                                CcModHydroSystemDetail _hsd = _db.CcModHydroSystemDetail.Where(w => w.ProjectId == projectId && w.HydroSysDetailId == id).FirstOrDefault();
                                _db.CcModHydroSystemDetail.Remove(_hsd);
                                break;

                            case "CcModFloodFrequencyDetail":
                                CcModFloodFrequencyDetail _fsd = _db.CcModFloodFrequencyDetail.Where(w => w.ProjectId == projectId && w.FloodFrequencyDetailId == id).FirstOrDefault();
                                _db.CcModFloodFrequencyDetail.Remove(_fsd);
                                break;

                            case "CcModPrjIrrigCropAreaDetail":
                                CcModPrjIrrigCropAreaDetail _ica = _db.CcModPrjIrrigCropAreaDetail.Where(w => w.ProjectId == projectId && w.IrrigatedCropId == id).FirstOrDefault();
                                _db.CcModPrjIrrigCropAreaDetail.Remove(_ica);
                                break;

                            case "CcModAnalyzeOptionsDetail":
                                CcModAnalyzeOptionsDetail _aod = _db.CcModAnalyzeOptionsDetail.Where(w => w.ProjectId == projectId && w.AnalyzeOptionsId == id).FirstOrDefault();
                                _db.CcModAnalyzeOptionsDetail.Remove(_aod);
                                break;

                            case "CcModPrjEIADetail":
                                CcModPrjEIADetail _eia = _db.CcModPrjEIADetail.Where(w => w.ProjectId == projectId && w.EIAId == id).FirstOrDefault();
                                _db.CcModPrjEIADetail.Remove(_eia);
                                break;

                            case "CcModPrjSIADetail":
                                CcModPrjSIADetail _sia = _db.CcModPrjSIADetail.Where(w => w.ProjectId == projectId && w.SIAId == id).FirstOrDefault();
                                _db.CcModPrjSIADetail.Remove(_sia);
                                break;

                            //CcModPrjEcoFinAnalysisDetail
                            case "CcModPrjEcoFinAnalysisDetail":
                                CcModPrjEcoFinAnalysisDetail _efa = _db.CcModPrjEcoFinAnalysisDetail.Where(w => w.ProjectId == projectId && w.EconomicalAndFinancialId == id).FirstOrDefault();
                                _db.CcModPrjEcoFinAnalysisDetail.Remove(_efa);
                                break;
                        }

                        _db.SaveChanges();
                        dbContextTransaction.Commit();

                        noti = new Notification
                        {
                            id = id.ToString(),
                            status = "success",
                            message = "Information has been deleted successfully."
                        };
                    }
                    catch (Exception ex)
                    {
                        dbContextTransaction.Rollback();
                        var message = ch.ExtractInnerException(ex);

                        noti = new Notification
                        {
                            id = id.ToString(),
                            status = "error",
                            message = message + "<br /> Model Name: " + modelName
                        };
                    }
                }
            }

            return Json(noti);
        }

        //form/GetSingleLocation :: gsl
        [HttpGet]
        public JsonResult gsl(long id, long projectId)
        {
            CcModPrjLocationDetail _loc = _db.CcModPrjLocationDetail.Where(w => w.ProjectId == projectId && w.LocationId == id).FirstOrDefault();
            return Json(_loc);
        }

        //form/GetSingleHydrologicalSystem :: gshs
        [HttpGet]
        public JsonResult gshs(long id, long projectId)
        {
            CcModHydroSystemDetail _hsd = _db.CcModHydroSystemDetail.Where(w => w.ProjectId == projectId && w.HydroSysDetailId == id).FirstOrDefault();
            return Json(_hsd);
        }

        //form/GetSingleFloodFrequency :: gsff
        [HttpGet]
        public JsonResult gsff(long id, long projectId)
        {
            CcModFloodFrequencyDetail _ffd = _db.CcModFloodFrequencyDetail.Where(w => w.ProjectId == projectId && w.FloodFrequencyDetailId == id).FirstOrDefault();
            return Json(_ffd);
        }

        //form/GetSingleIrrigCropArea :: gsica
        [HttpGet]
        public JsonResult gsica(long id, long projectId)
        {
            CcModPrjIrrigCropAreaDetail _ica = _db.CcModPrjIrrigCropAreaDetail.Where(w => w.ProjectId == projectId && w.IrrigatedCropId == id).FirstOrDefault();
            return Json(_ica);
        }

        //form/GetSingleAnalyzeOptionsDetail :: gsaod
        [HttpGet]
        public JsonResult gsaod(long id, long projectId)
        {
            CcModAnalyzeOptionsDetail _aod = _db.CcModAnalyzeOptionsDetail.Where(w => w.ProjectId == projectId && w.AnalyzeOptionsId == id).FirstOrDefault();
            return Json(_aod);
        }

        //form/GetSingleDesignSubmittedWithProjectDocument :: gsdswpd
        [HttpGet]
        public JsonResult gsdswpd(long id, long projectId)
        {
            CcModDesignSubmitDetail _dsd = _db.CcModDesignSubmitDetail.Where(w => w.ProjectId == projectId && w.DesignSubmittedId == id).FirstOrDefault();
            return Json(_dsd);
        }

        //form/GetSingleDesignSubmittedWithProjectDocument :: gseia
        [HttpGet]
        public JsonResult gseia(long id, long projectId)
        {
            CcModPrjEIADetail _eia = _db.CcModPrjEIADetail.Where(w => w.ProjectId == projectId && w.EIAId == id).FirstOrDefault();
            return Json(_eia);
        }

        //form/GetSingleDesignSubmittedWithProjectDocument :: gssia
        [HttpGet]
        public JsonResult gssia(long id, long projectId)
        {
            CcModPrjSIADetail _sia = _db.CcModPrjSIADetail.Where(w => w.ProjectId == projectId && w.SIAId == id).FirstOrDefault();
            return Json(_sia);
        }

        //form/GetSingleEcoFinAnalysisDetail :: gsefa
        [HttpGet]
        public JsonResult gsefa(long id, long projectId)
        {
            CcModPrjEcoFinAnalysisDetail _efa = _db.CcModPrjEcoFinAnalysisDetail.Where(w => w.ProjectId == projectId && w.EconomicalAndFinancialId == id).FirstOrDefault();
            return Json(_efa);
        }
    }
}