using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WrpCcNocWeb.DatabaseContext;

namespace WrpCcNocWeb.Controllers
{
    public class MapViewerController : Controller
    {
        //private readonly WrpCcNocDbContext _dbContext;
        private readonly WrpCcNocDbContext _dbContext = new WrpCcNocDbContext();

        //public MapViewerController(WrpCcNocDbContext context)
        //{
        //    _dbContext = context;
        //}

        public IActionResult DataMapViewer()
        {
            ViewData["Message"] = "Map Viewer";

            //ViewData["EquipmentTypes"] = Json(_dbContext.EquipmentInfoes.Select(eq => new { eq.EquipmentId, eq.NameOfEquipment }).ToList()).ToString();
            //ViewBag.EquipmentTypes = Json(_dbContext.EquipmentInfoes.Select(eq => new { eq.EquipmentId, eq.NameOfEquipment }).ToList()).ToString();

            return View();
        }

        //GetProjectLocations
        //public JsonResult GetMapLocationData(int? equipmentId = null, string adminLevel = "dist", string divCode = "", string distCode = "", string upazCode = "")
        [HttpGet]
        public JsonResult GetEquipmentLocations(long projectId, string adminLevel = "dist", string distCode = "", string upazCode = "")
        {
            var query = _dbContext.CcModPrjLocationDetail.AsNoTracking();

            //    .Where(si => (equipmentId == null || si.EquipmentId.Equals(equipmentId)));
            //if (!string.IsNullOrEmpty(upazCode))
            //{
            //    query = query.Where(si => ((distCode.Equals("") || si.DistCode.Equals(distCode)) && (si.UpazCode.Equals(upazCode))));
            //}
            //else if (!string.IsNullOrEmpty(distCode))
            //{
            //    query = query.Where(si => (si.DistCode.Equals(distCode)));
            //}
            //else if (!string.IsNullOrEmpty(divCode))
            //{
            //    query = query.Where(si => (si.DistrictInfo.DivCode.Equals(divCode)));
            //}
            
            //{
            //    "": "Low Lift Pump (LLP)",
            //        "address": "NSS, Pallabi Residential Area, Haji Bari Mosjid",
            //        "road_or_village": "Pallabi Residential Area",
            //        "mobile_no": "01712-795359",
            //        "image_name": "680_1.jpg",
            //        "lat": 22.1380571,
            //        "long": 90.2302515,
            //        "district_code": 1004,
            //        "upazila_code": 100409,
            //        "union_code": 10040902,
            //        "mauza_code": 10040903400,
            //        "district_name": "Barguna",
            //        "upazila_name": "Amtali",
            //        "union_name": "Ward No-02",
            //        "mauza_name": "Kontakata(Dakshin)"
            //}

            object geoData = query.Select(pLoc => new
            {
                project_idd = pLoc.ProjectId,
                project_name = pLoc.CcModAppProjectCommonDetail.ProjectName,                
                lat = pLoc.Latitude,
                lng = pLoc.Longitude,
                dist_code = pLoc.DistrictGeoCode,
                upaz_code = pLoc.UpazilaGeoCode,
                union_code = pLoc.UnionGeoCode,                
                district = pLoc.LookUpAdminBndDistrict.DistrictName,
                upazila = pLoc.LookUpAdminBndUpazila.UpazilaName,
                union = pLoc.LookUpAdminBndUnion.UnionName               
            });


            var jsonData = Json(geoData);

            return jsonData;


            //object geoData = null;

            //switch (adminLevel)
            //{
            //    case "div":
            //        {
            //            geoData = query
            //                .GroupBy(si => si.DistrictInfo.DivisionInfo.DivCode)
            //                .Select(si => new
            //                {
            //                    divCode = si.FirstOrDefault().DistrictInfo.DivisionInfo.DivCode,
            //                    data = si.Count()
            //                });
            //        }
            //        break;

            //    case "dist":
            //        {
            //            geoData = query
            //                .GroupBy(si => si.DistCode)
            //                .Select(si => new
            //                {
            //                    divCode = si.FirstOrDefault().DistrictInfo.DivisionInfo.DivCode,
            //                    distCode = si.FirstOrDefault().DistCode,
            //                    data = si.Count()
            //                });
            //        }
            //        break;

            //    case "upaz":
            //        {
            //            geoData = query
            //                .GroupBy(si => si.UpazCode)
            //                .Select(si => new
            //                {
            //                    divCode = si.FirstOrDefault().DistrictInfo.DivisionInfo.DivCode,
            //                    distCode = si.FirstOrDefault().DistCode,
            //                    upazCode = si.FirstOrDefault().UpazCode,
            //                    data = si.Count()
            //                });

            //        }
            //        break;

            //    case "union":
            //        {
            //            geoData = query
            //                .GroupBy(si => si.UnionCode)
            //                .Select(si => new
            //                {
            //                    divCode = si.FirstOrDefault().DistrictInfo.DivisionInfo.DivCode,
            //                    distCode = si.FirstOrDefault().DistCode,
            //                    upazCode = si.FirstOrDefault().UpazCode,
            //                    unionCode = si.FirstOrDefault().UnionCode,
            //                    data = si.Count()
            //                });
            //        }
            //        break;

            //    default:
            //        {
            //            geoData = query
            //                .GroupBy(si => si.DistCode)
            //                .Select(si => new
            //                {
            //                    divCode = si.FirstOrDefault().DistrictInfo.DivisionInfo.DivCode,
            //                    distCode = si.FirstOrDefault().DistCode,
            //                    data = si.Count()
            //                });
            //        }
            //        break;
            //}


            //var jsonData = Json(geoData);

            //return jsonData;

            //return Json(jsonData);


            /*
            ViewBag.EquipmentId = equipmentId;
            ViewBag.Title = _dbContext.EquipmentInfoes
                .Where(eqi => equipmentId.Equals()null || eqi.EquipmentId.Equals()equipmentId)
                .Select(eqi => eqi.NameOfEquipment)
                .FirstOrDefault();

            var adminInfo = "";

            if (upazCode != "")
            {
                var upazInfo = _dbContext.UpazilaInfoes
                    .Where(upaz => upaz.UpazCode.Equals()upazCode)
                    .Select(upaz => new
                    {
                        UpazName = upaz.UpazName,
                        DistName = upaz.DistrictInfo.DistName,
                        DivName = upaz.DistrictInfo.DivisionInfo.DivName
                    }).FirstOrDefault();

                if (upazInfo != null)
                    adminInfo = $"{upazInfo.UpazName}, {upazInfo.DistName}, {upazInfo.DivName}";
            }
            else if (distCode != "")
            {
                var distInfo = _dbContext.DistrictInfoes
                    .Where(dist => dist.DistCode.Equals()distCode)
                    .Select(dist => new
                    {
                        DistName = dist.DistName,
                        DivName = dist.DivisionInfo.DivName
                    }).FirstOrDefault();

                if (distInfo != null)
                    adminInfo = $"{distInfo.DistName}, {distInfo.DivName}";
            }
            else if (divCode != "")
            {
                adminInfo = _dbContext.DivisionInfoes
                    .FirstOrDefault(div => div.DivCode.Equals()divCode)
                    ?.DivName;
            }


            ViewBag.AdminInfo = adminInfo;
            ViewBag.DivCode = divCode;
            ViewBag.DistCode = distCode;
            ViewBag.UpazCode = upazCode;

            ViewData["EquipmentId"] = equipmentId;
            ViewData["Title"] = _dbContext.EquipmentInfoes
                .Where(eqi => equipmentId.Equals()null || eqi.EquipmentId.Equals()equipmentId)
                .Select(eqi => eqi.NameOfEquipment)
                .FirstOrDefault();
*/

            //PagedList<SurveyInfo> model = new PagedList<SurveyInfo>(query, page, pageSize);

            //surveyInfoes;
            //var surveyInfoes1 = _dbContext.SurveyInfoes
            //    .Where(si => (equipmentId == null || si.EquipmentId == equipmentId)
            //    && (divCode == "" || si.DistrictInfo.DivCode == divCode)
            //    && (distCode == "" || si.DistCode == distCode)
            //    && (upazCode == "" || si.UpazCode == upazCode))
            //    .Include(si => si.EquipmentInfo)
            //    .Include(si => si.DistrictInfo)
            //    .Include(si => si.UpazilaInfo)
            //    .Include(si => si.UnionInfo)
            //    .Include(si => si.AgencyInfo)
            //    .Include(si => si.ElectricMotorPowerSourceInfo)
            //    .Include(si => si.SurfaceWaterSourceInfo);

            //return View(await surveyInfoes1.ToListAsync());
        }

        public IActionResult map()
        {
            return View();
        }
    }
}