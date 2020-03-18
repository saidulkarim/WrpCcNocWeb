using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WrpCcNocWeb.DatabaseContext;

namespace WrpCcNocWeb.Controllers
{
    public class mapController : Controller
    {
        //private readonly WrpCcNocDbContext _dbContext;
        private readonly WrpCcNocDbContext _dbContext = new WrpCcNocDbContext();                

        
        public IActionResult viewLocation(long projectId) {
            ViewBag.ProjectId = projectId;
            return View();
        }
                
        public JsonResult GetProjectLocation(long projectId)
        {            
            var query = _dbContext.CcModPrjLocationDetail.Where(w=>w.ProjectId != null && w.ProjectId == projectId).AsNoTracking();
                        
            object geoData = query.Select(pLoc => new
            {
                project_id = pLoc.ProjectId,
                project_name = pLoc.CcModAppProjectCommonDetail.ProjectName,
                lat = pLoc.Latitude,
                lng = pLoc.Longitude,
                dist_code = pLoc.DistrictGeoCode,
                upaz_code = pLoc.UpazilaGeoCode,
                union_code = pLoc.UnionGeoCode,
                district = pLoc.LookUpAdminBndDistrict.DistrictName,
                upazila = pLoc.LookUpAdminBndUpazila.UpazilaName,
                union = pLoc.LookUpAdminBndUnion.UnionName,
                image_file_name = pLoc.ImageFileName
            });

            var jsonData = Json(geoData);
            return Json(jsonData);
        }
    }
}