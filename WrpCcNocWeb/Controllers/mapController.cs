using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WrpCcNocWeb.DatabaseContext;
using KmlToGeoJson;

namespace WrpCcNocWeb.Controllers
{
    public class mapController : Controller
    {
        private readonly IWebHostEnvironment hostingEnvironment;

        public mapController(IWebHostEnvironment hostingEnvironment)
        {
            this.hostingEnvironment = hostingEnvironment;
        }
        private readonly WrpCcNocDbContext _dbContext = new WrpCcNocDbContext();

        public IActionResult viewLocation(long projectId)
        {
            ViewBag.ProjectId = projectId;
            return View();
        }

        public JsonResult GetProjectLocation(long projectId)
        {
            var query = _dbContext.CcModPrjLocationDetail.Where(w => w.ProjectId == projectId).AsNoTracking();

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
                image_file_name = pLoc.ImageFileName,
                kml_file_name = pLoc.MapFileName,
                kml_file_content = ReadKmlFile(pLoc.MapFileName)
            });

            var jsonData = Json(geoData);
            return Json(jsonData);
        }

        private string ReadKmlFile(string fileName)
        {
            string result = string.Empty;
            string path = Path.Combine(hostingEnvironment.WebRootPath, "docs/map_kml", fileName);
            XDocument doc = XDocument.Load(path);
            result = KmlToGeoJsonConverter.FromKml(doc.ToString());
            result = result.Replace(": ", ":");
            result = result.Contains("\"type\":\"Feature\"") ? result : result.Replace("\"feature\":\"Feature\",", "\"feature\":\"Feature\", \"type\":\"Feature\",");
            return result;
        }
    }
}