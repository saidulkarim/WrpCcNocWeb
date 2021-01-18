using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WrpCcNocWeb.DatabaseContext;
using WrpCcNocWeb.Helpers;
using WrpCcNocWeb.Models;
using WrpCcNocWeb.Models.UserManagement;
using WrpCcNocWeb.Models.Utility;

namespace WrpCcNocWeb.Controllers
{
    public class controlsController : Controller
    {
        #region Initialization
        private readonly LoggedUserInfo iLoggedUser;
        private readonly WrpCcNocDbContext _db = new WrpCcNocDbContext();
        private commonController cc = new commonController();
        private EmailService es = new EmailService();
        private SmsService ss = new SmsService();
        private readonly CommonHelper ch = new CommonHelper();
        private Notification noti = new Notification();
        #endregion

        #region Contructor
        public controlsController()
        {

        }
        #endregion

        #region Hydrological System
        //controls/HydroSystemDetailSave :: hsds
        [HttpPost]
        public JsonResult hsds(CcModHydroSystemDetail _hsd)
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
            int result = 0;

            try
            {
                if (_hsd != null && _hsd.ProjectId != 0)
                {
                    using var dbContextTransaction = _db.Database.BeginTransaction();
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

        //controls/GetHydroSystemDetail :: get_hsd
        [HttpGet]
        public JsonResult get_hsd(long project_id)
        {
            try
            {
                var _details = (from d in _db.CcModHydroSystemDetail
                                join l in _db.LookUpCcModHydroSystem on d.HydroSystemCategoryId equals l.HydroSystemCategoryId
                                where d.ProjectId == project_id
                                select new
                                {
                                    d.HydroSysDetailId,
                                    d.ProjectId,
                                    d.HydroSystemCategoryId,
                                    l.HydroSystemCategory,
                                    d.NameOfHydroSystem,
                                    d.HydroSystemLengthArea,
                                    d.HydroSystemUnit,
                                    Description = string.IsNullOrEmpty(d.Description) ? "" : d.Description
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

        //controls/GetSingleHydrologicalSystem :: gshs
        [HttpGet]
        public JsonResult gshs(long id, long projectId)
        {
            CcModHydroSystemDetail _hsd = _db.CcModHydroSystemDetail.Where(w => w.ProjectId == projectId && w.HydroSysDetailId == id).FirstOrDefault();
            return Json(_hsd);
        }
        #endregion

        #region Annual Rainfall
        //controls/SaveAnnualRainfallDetail :: sv_arr_dt
        [HttpPost]
        public JsonResult sv_arr_dt(CcModAnnualRainfallDetail _arr)
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
            int result = 0;

            try
            {
                if (_arr.ProjectId != 0)
                {
                    using var dbContextTransaction = _db.Database.BeginTransaction();
                    if (_arr.AnnualRainfallDetailId != 0)
                    {
                        _db.Entry(_arr).State = EntityState.Modified;
                        result = _db.SaveChanges();

                        if (result > 0)
                        {
                            dbContextTransaction.Commit();

                            noti = new Notification
                            {
                                id = _arr.ProjectId.ToString(),
                                status = "success",
                                message = "Data has been updated successfully."
                            };
                        }
                        else
                        {
                            dbContextTransaction.Rollback();

                            noti = new Notification
                            {
                                id = _arr.ProjectId.ToString(),
                                status = "error",
                                message = "Data not updated."
                            };
                        }
                    }
                    else
                    {
                        try
                        {
                            if (_arr.RainfallYear > 0)
                            {
                                _db.CcModAnnualRainfallDetail.Add(_arr);
                                result = _db.SaveChanges();

                                if (result > 0)
                                {

                                    dbContextTransaction.Commit();

                                    noti = new Notification
                                    {
                                        id = _arr.ProjectId.ToString(),
                                        status = "success",
                                        message = "Data has been saved successfully."
                                    };
                                }
                                else
                                {
                                    dbContextTransaction.Rollback();

                                    noti = new Notification
                                    {
                                        id = _arr.ProjectId.ToString(),
                                        status = "error",
                                        message = "Data not saved."
                                    };
                                }
                            }
                            else
                            {
                                noti = new Notification
                                {
                                    id = _arr.ProjectId.ToString(),
                                    status = "error",
                                    message = "Please select a rainfall year."
                                };
                            }
                        }
                        catch (Exception ex)
                        {
                            dbContextTransaction.Rollback();
                            var message = ch.ExtractInnerException(ex);

                            noti = new Notification
                            {
                                id = _arr.ProjectId.ToString(),
                                status = "error",
                                message = "Rainfall data saving transaction has been rollbacked. " + message
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

        //controls/GetAnnualRainfallDetail :: get_arrd
        [HttpGet]
        public JsonResult get_arrd(long project_id)
        {
            try
            {
                var _details = (from d in _db.CcModAnnualRainfallDetail
                                where d.ProjectId == project_id
                                select new
                                {
                                    AnnualRainfallDetailId = d.AnnualRainfallDetailId,
                                    ProjectId = d.ProjectId,
                                    RainfallYear = d.RainfallYear,
                                    RainfallYearBn = d.RainfallYear.ToString().NumberEnglishToBengali(),
                                    RainfallMm = d.RainfallMm,
                                    RainfallMmBn = d.RainfallMm.ToString().NumberEnglishToBengali(),
                                    CollectedStationName = d.CollectedStationName,
                                    Season = d.Season
                                }).OrderBy(o => o.AnnualRainfallDetailId).ToList();

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

        //controls/GetAnnualRainfallDetailTemp      
        public List<CcModAnnualRainfallDetailTemp> GetAnnualRainfallDetailTemp(long project_id)
        {
            List<CcModAnnualRainfallDetailTemp> _details = new List<CcModAnnualRainfallDetailTemp>();
            try
            {
                _details = (from d in _db.CcModAnnualRainfallDetail
                            where d.ProjectId == project_id
                            select new CcModAnnualRainfallDetailTemp
                            {
                                AnnualRainfallDetailId = d.AnnualRainfallDetailId,
                                ProjectId = d.ProjectId,
                                RainfallYear = d.RainfallYear.ToString(),
                                RainfallYearBn = d.RainfallYear.ToString().NumberEnglishToBengali(),
                                RainfallMm = d.RainfallMm.ToString(),
                                RainfallMmBn = d.RainfallMm.ToString().NumberEnglishToBengali(),
                                CollectedStationName = d.CollectedStationName,
                                Season = d.Season
                            }).OrderBy(o => o.AnnualRainfallDetailId).ToList();

                if (_details.Count > 0)
                {
                    return _details;
                }
                else
                {
                    _details = new List<CcModAnnualRainfallDetailTemp>();
                }
            }
            catch (Exception ex)
            {
                _details = new List<CcModAnnualRainfallDetailTemp>();
            }

            return _details;
        }
        #endregion

        #region Highest Flood Level
        //controls/SaveHighestFloodLevelDetail :: sv_hfl_dt
        [HttpPost]
        public JsonResult sv_hfl_dt(CcModHighestFloodLevelDetail _hfl)
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
            int result = 0;

            try
            {
                if (_hfl.ProjectId != 0)
                {
                    using var dbContextTransaction = _db.Database.BeginTransaction();
                    if (_hfl.HighestFloodLevelDetailId != 0)
                    {
                        _db.Entry(_hfl).State = EntityState.Modified;
                        result = _db.SaveChanges();

                        if (result > 0)
                        {
                            dbContextTransaction.Commit();

                            noti = new Notification
                            {
                                id = _hfl.ProjectId.ToString(),
                                status = "success",
                                message = "Data has been updated successfully."
                            };
                        }
                        else
                        {
                            dbContextTransaction.Rollback();

                            noti = new Notification
                            {
                                id = _hfl.ProjectId.ToString(),
                                status = "error",
                                message = "Data not updated."
                            };
                        }
                    }
                    else
                    {
                        try
                        {
                            if (_hfl.FloodYear > 0)
                            {
                                _db.CcModHighestFloodLevelDetail.Add(_hfl);
                                result = _db.SaveChanges();

                                if (result > 0)
                                {

                                    dbContextTransaction.Commit();

                                    noti = new Notification
                                    {
                                        id = _hfl.ProjectId.ToString(),
                                        status = "success",
                                        message = "Data has been saved successfully."
                                    };
                                }
                                else
                                {
                                    dbContextTransaction.Rollback();

                                    noti = new Notification
                                    {
                                        id = _hfl.ProjectId.ToString(),
                                        status = "error",
                                        message = "Data not saved."
                                    };
                                }
                            }
                            else
                            {
                                noti = new Notification
                                {
                                    id = _hfl.ProjectId.ToString(),
                                    status = "error",
                                    message = "Please select a flood year."
                                };
                            }
                        }
                        catch (Exception ex)
                        {
                            dbContextTransaction.Rollback();
                            var message = ch.ExtractInnerException(ex);

                            noti = new Notification
                            {
                                id = _hfl.ProjectId.ToString(),
                                status = "error",
                                message = "Highest flood level data saving transaction has been rollbacked. " + message
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

        //controls/GetHighestFloodLevelDetail :: get_hfld
        [HttpGet]
        public JsonResult get_hfld(long project_id)
        {
            try
            {
                var _details = (from d in _db.CcModHighestFloodLevelDetail
                                where d.ProjectId == project_id
                                select new
                                {
                                    HighestFloodLevelDetailId = d.HighestFloodLevelDetailId,
                                    ProjectId = d.ProjectId,
                                    FloodYear = d.FloodYear,
                                    FloodYearBn = d.FloodYear.ToString().NumberEnglishToBengali(),
                                    HighestFloodLevel = d.HighestFloodLevel,
                                    HighestFloodLevelBn = d.HighestFloodLevel.ToString().NumberEnglishToBengali(),
                                    Datum = d.Datum
                                }).OrderBy(o => o.HighestFloodLevelDetailId).ToList();

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

        //controls/GetHighestFloodLevelDetailTemp      
        public List<CcModHighestFloodLevelDetailTemp> GetHighestFloodLevelDetailTemp(long project_id)
        {
            List<CcModHighestFloodLevelDetailTemp> _details = new List<CcModHighestFloodLevelDetailTemp>();
            try
            {
                _details = (from d in _db.CcModHighestFloodLevelDetail
                            where d.ProjectId == project_id
                            select new CcModHighestFloodLevelDetailTemp
                            {
                                HighestFloodLevelDetailId = d.HighestFloodLevelDetailId,
                                ProjectId = d.ProjectId,
                                FloodYear = d.FloodYear.ToString(),
                                FloodYearBn = d.FloodYear.ToString().NumberEnglishToBengali(),
                                HighestFloodLevel = d.HighestFloodLevel.ToString(),
                                HighestFloodLevelBn = d.HighestFloodLevel.ToString().NumberEnglishToBengali(),
                                Datum = d.Datum
                            }).OrderBy(o => o.HighestFloodLevelDetailId).ToList();

                if (_details.Count > 0)
                {
                    return _details;
                }
                else
                {
                    _details = new List<CcModHighestFloodLevelDetailTemp>();
                }
            }
            catch (Exception ex)
            {
                _details = new List<CcModHighestFloodLevelDetailTemp>();
            }

            return _details;
        }
        #endregion

        #region Maximum Discharge
        //controls/SaveMaximumDischargeDetail :: sv_mxdrg_dt
        [HttpPost]
        public JsonResult sv_mxdrg_dt(CcModMaxDischargeDetail _mxdrg)
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
            int result = 0;

            try
            {
                if (_mxdrg.ProjectId != 0)
                {
                    using var dbContextTransaction = _db.Database.BeginTransaction();
                    if (_mxdrg.MaxDischargeDetailId != 0)
                    {
                        _db.Entry(_mxdrg).State = EntityState.Modified;
                        result = _db.SaveChanges();

                        if (result > 0)
                        {
                            dbContextTransaction.Commit();

                            noti = new Notification
                            {
                                id = _mxdrg.ProjectId.ToString(),
                                status = "success",
                                message = "Data has been updated successfully."
                            };
                        }
                        else
                        {
                            dbContextTransaction.Rollback();

                            noti = new Notification
                            {
                                id = _mxdrg.ProjectId.ToString(),
                                status = "error",
                                message = "Data not updated."
                            };
                        }
                    }
                    else
                    {
                        try
                        {
                            if (_mxdrg.DischargeYear > 0)
                            {
                                _db.CcModMaxDischargeDetail.Add(_mxdrg);
                                result = _db.SaveChanges();

                                if (result > 0)
                                {
                                    dbContextTransaction.Commit();

                                    noti = new Notification
                                    {
                                        id = _mxdrg.ProjectId.ToString(),
                                        status = "success",
                                        message = "Data has been saved successfully."
                                    };
                                }
                                else
                                {
                                    dbContextTransaction.Rollback();

                                    noti = new Notification
                                    {
                                        id = _mxdrg.ProjectId.ToString(),
                                        status = "error",
                                        message = "Data not saved."
                                    };
                                }
                            }
                            else
                            {
                                noti = new Notification
                                {
                                    id = _mxdrg.ProjectId.ToString(),
                                    status = "error",
                                    message = "Please select a flood year."
                                };
                            }
                        }
                        catch (Exception ex)
                        {
                            dbContextTransaction.Rollback();
                            var message = ch.ExtractInnerException(ex);

                            noti = new Notification
                            {
                                id = _mxdrg.ProjectId.ToString(),
                                status = "error",
                                message = "Maximum discharge data saving transaction has been rollbacked. " + message
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

        //controls/GetMaximumDischargeDetail :: get_mxdrgd
        [HttpGet]
        public JsonResult get_mxdrgd(long project_id)
        {
            try
            {
                var _details = (from d in _db.CcModMaxDischargeDetail
                                where d.ProjectId == project_id
                                select new
                                {
                                    MaxDischargeDetailId = d.MaxDischargeDetailId,
                                    ProjectId = d.ProjectId,
                                    DischargeYear = d.DischargeYear.ToString(),
                                    DischargeYearBn = d.DischargeYear.ToString().NumberEnglishToBengali(),
                                    DischargeAmount = d.DischargeAmount.ToString(),
                                    DischargeAmountBn = d.DischargeAmount.ToString().NumberEnglishToBengali()
                                }).OrderBy(o => o.MaxDischargeDetailId).ToList();

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

        //controls/GetMaximumDischargeDetailTemp
        public List<CcModMaxDischargeDetailTemp> GetMaximumDischargeDetailTemp(long project_id)
        {
            List<CcModMaxDischargeDetailTemp> _details = new List<CcModMaxDischargeDetailTemp>();
            try
            {
                _details = (from d in _db.CcModMaxDischargeDetail
                            where d.ProjectId == project_id
                            select new CcModMaxDischargeDetailTemp
                            {
                                MaxDischargeDetailId = d.MaxDischargeDetailId,
                                ProjectId = d.ProjectId,
                                DischargeYear = d.DischargeYear.ToString(),
                                DischargeYearBn = d.DischargeYear.ToString().NumberEnglishToBengali(),
                                DischargeAmount = d.DischargeAmount.ToString(),
                                DischargeAmountBn = d.DischargeAmount.ToString().NumberEnglishToBengali()
                            }).OrderBy(o => o.MaxDischargeDetailId).ToList();

                if (_details.Count > 0)
                {
                    return _details;
                }
                else
                {
                    _details = new List<CcModMaxDischargeDetailTemp>();
                }
            }
            catch (Exception ex)
            {
                _details = new List<CcModMaxDischargeDetailTemp>();
            }

            return _details;
        }
        #endregion

        #region Flood Frequency Analysis
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
                    using var dbContextTransaction = _db.Database.BeginTransaction();
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
                                message = "Flood frequency operational transaction has been rollbacked. " + message
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

        //form/GetSingleFloodFrequency :: gsff
        [HttpGet]
        public JsonResult gsff(long id, long projectId)
        {
            CcModFloodFrequencyDetail _ffd = _db.CcModFloodFrequencyDetail.Where(w => w.ProjectId == projectId && w.FloodFrequencyDetailId == id).FirstOrDefault();
            return Json(_ffd);
        }

        //form/GetFloodFrequencyDetail :: get_ffd
        [HttpGet]
        public JsonResult get_ffd(long project_id)
        {
            try
            {
                var _details = (from d in _db.CcModFloodFrequencyDetail
                                join l in _db.LookUpCcModFloodFrequency on d.FloodFrequencyId equals l.FloodFrequencyId
                                where d.ProjectId == project_id
                                select new
                                {
                                    d.FloodFrequencyDetailId,
                                    d.ProjectId,
                                    d.FloodFrequencyId,
                                    l.FloodFrequency,
                                    d.FloodFrequencyLevel,
                                    d.Datum
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
        //controls/IrrigatedCropAreaSave :: icas
        [HttpPost]
        public JsonResult icas(CcModPrjIrrigCropAreaDetail _ica)
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
            int result = 0;

            try
            {
                if (_ica != null && _ica.ProjectId != 0)
                {
                    using var dbContextTransaction = _db.Database.BeginTransaction();
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
                                message = "Irrigated crop area information transaction has been rollbacked. " + message
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

        //controls/GetIrrigatedCropArea :: get_ica
        [HttpGet]
        public JsonResult get_ica(long project_id)
        {
            try
            {
                var _details = (from d in _db.CcModPrjIrrigCropAreaDetail
                                where d.ProjectId == project_id
                                select new
                                {
                                    d.IrrigatedCropId,
                                    d.ProjectId,
                                    d.CropName,
                                    d.Area,
                                    d.ProductionInTon,
                                    d.ProductionAmount
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

        //controls/GetSingleIrrigCropArea :: gsica
        [HttpGet]
        public JsonResult gsica(long id, long projectId)
        {
            CcModPrjIrrigCropAreaDetail _ica = _db.CcModPrjIrrigCropAreaDetail.Where(w => w.ProjectId == projectId && w.IrrigatedCropId == id).FirstOrDefault();
            return Json(_ica);
        }
        #endregion

        #region Fish Production and Diversity 
        //controls/SaveFishProductionDiversity :: sfpd
        [HttpPost]
        public JsonResult sfpd(CcModFishProdDiversityDetail _fpd)
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
            int result = 0;

            try
            {
                if (_fpd != null && _fpd.ProjectId != 0)
                {
                    using var dbContextTransaction = _db.Database.BeginTransaction();
                    if (_fpd.FishProdDiversityDetailId != 0)
                    {
                        _db.Entry(_fpd).State = EntityState.Modified;
                        result = _db.SaveChanges();

                        if (result > 0)
                        {
                            dbContextTransaction.Commit();

                            noti = new Notification
                            {
                                id = _fpd.FishProdDiversityDetailId.ToString(),
                                status = "success",
                                message = "Fish production and diversity information has been updated successfully."
                            };
                        }
                        else
                        {
                            dbContextTransaction.Rollback();

                            noti = new Notification
                            {
                                id = _fpd.FishProdDiversityDetailId.ToString(),
                                status = "error",
                                message = "Fish production and diversity information not updated."
                            };
                        }
                    }
                    else
                    {
                        try
                        {
                            _db.CcModFishProdDiversityDetail.Add(_fpd);
                            result = _db.SaveChanges();

                            if (result > 0)
                            {
                                dbContextTransaction.Commit();

                                noti = new Notification
                                {
                                    id = _fpd.FishProdDiversityDetailId.ToString(),
                                    status = "success",
                                    message = "Fish production and diversity information has been saved successfully."
                                };
                            }
                            else
                            {
                                dbContextTransaction.Rollback();

                                noti = new Notification
                                {
                                    id = _fpd.FishProdDiversityDetailId.ToString(),
                                    status = "error",
                                    message = "Fish production and diversity information not saved."
                                };
                            }
                        }
                        catch (Exception ex)
                        {
                            dbContextTransaction.Rollback();
                            var message = ch.ExtractInnerException(ex);

                            noti = new Notification
                            {
                                id = _fpd.FishProdDiversityDetailId.ToString(),
                                status = "error",
                                message = "Fish production and diversity information transaction has been rollbacked. " + message
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

        //controls/GetFishProductionDiversity :: get_fpd
        [HttpGet]
        public JsonResult get_fpd(long project_id)
        {
            try
            {
                var _details = (from d in _db.CcModFishProdDiversityDetail
                                where d.ProjectId == project_id
                                select new
                                {
                                    d.FishProdDiversityDetailId,
                                    d.ProjectId,
                                    d.TypesOfFisheries,
                                    TypesOfFisheriesBn = (d.TypesOfFisheries == "Capture") ? "ধরা/ শিকার" : "চাষাবাদ",
                                    d.Diversity,
                                    d.FishProductionInTon,
                                    ProductionInTonBn = d.FishProductionInTon.ToString().NumberEnglishToBengali()
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

        //controls/GetSingleFishProductionDiversity :: gsfpd
        [HttpGet]
        public JsonResult gsfpd(long id, long projectId)
        {
            CcModFishProdDiversityDetail _fpd = _db.CcModFishProdDiversityDetail.Where(w => w.ProjectId == projectId && w.FishProdDiversityDetailId == id).FirstOrDefault();
            return Json(_fpd);
        }

        //controls/GetFishProductionDiversityDetailTemp      
        public List<CcModFishProdDiversityDetailTemp> GetFishProductionDiversityDetailTemp(long project_id)
        {
            List<CcModFishProdDiversityDetailTemp> _details = new List<CcModFishProdDiversityDetailTemp>();
            try
            {
                _details = (from d in _db.CcModFishProdDiversityDetail
                            where d.ProjectId == project_id
                            select new CcModFishProdDiversityDetailTemp
                            {
                                FishProdDiversityDetailId = d.FishProdDiversityDetailId,
                                ProjectId = d.ProjectId,
                                TypesOfFisheries = d.TypesOfFisheries,
                                TypesOfFisheriesBn = (d.TypesOfFisheries == "Capture") ? "ধরা/ শিকার" : "চাষাবাদ",
                                Diversity = d.Diversity,
                                FishProductionInTon = d.FishProductionInTon.ToString(),
                                FishProductionInTonBn = d.FishProductionInTon.ToString().NumberEnglishToBengali()
                            }).OrderBy(o => o.FishProdDiversityDetailId).ToList();

                if (_details.Count > 0)
                {
                    return _details;
                }
                else
                {
                    _details = new List<CcModFishProdDiversityDetailTemp>();
                }
            }
            catch (Exception ex)
            {
                _details = new List<CcModFishProdDiversityDetailTemp>();
            }

            return _details;
        }
        #endregion

        #region Design Submitted with Project Document
        //controls/DesignSubmittedWithProjectDocumentSave :: dswpds
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

        //controls/GetAnalyzeOptionsFulfillObjective :: get_aofo
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

        //controls/GetSingleDesignSubmittedWithProjectDocument :: gsdswpd
        [HttpGet]
        public JsonResult gsdswpd(long id, long projectId)
        {
            CcModDesignSubmitDetail _dsd = _db.CcModDesignSubmitDetail.Where(w => w.ProjectId == projectId && w.DesignSubmittedId == id).FirstOrDefault();
            return Json(_dsd);
        }
        #endregion

        #region Analyze Options to fulfill objective
        //controls/AnalyzeOptionsFulfillObjectiveSave :: aofos
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

        //controls/GetAnalyzeOptionsFulfillObjective :: get_aofo
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

        //controls/GetSingleAnalyzeOptionsDetail :: gsaod
        [HttpGet]
        public JsonResult gsaod(long id, long projectId)
        {
            CcModAnalyzeOptionsDetail _aod = _db.CcModAnalyzeOptionsDetail.Where(w => w.ProjectId == projectId && w.AnalyzeOptionsId == id).FirstOrDefault();
            return Json(_aod);
        }
        #endregion
    }
}