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
            var query = _dbContext.CcModAppProjectCommonDetail.Where(w => w.ProjectId == projectId).AsNoTracking();

            object geoData = query.Select(pInfo => new
            {
                pid = pInfo.ProjectId,
                name = pInfo.ProjectName,
                background = pInfo.BackgroundAndRationale,
                target = pInfo.ProjectTarget,
                objective = pInfo.ProjectObjective,
                activity = pInfo.ProjectActivity,
                start_date = pInfo.ProjectStartDate.Value.ToString("dd MMM, yyyy"),
                completion_date = pInfo.ProjectCompletionDate.Value.ToString("dd MMM, yyyy"),
                estimated_cost = pInfo.ProjectEstimatedCost,
                outcome = pInfo.ProjectOutcome,
                output = pInfo.ProjectOutput,
                locations = _dbContext.CcModPrjLocationDetail.Where(w => w.ProjectId == projectId).AsNoTracking().Select(locs => new
                {
                    dist_code = locs.DistrictGeoCode,
                    district = locs.LookUpAdminBndDistrict.DistrictName,
                    upaz_code = locs.UpazilaGeoCode,
                    upazila = locs.LookUpAdminBndUpazila.UpazilaName,
                    union_code = locs.UnionGeoCode,
                    union = locs.LookUpAdminBndUnion.UnionName,
                    lat = string.IsNullOrEmpty(locs.Latitude) ? "" : locs.Latitude,
                    lng = string.IsNullOrEmpty(locs.Longitude) ? "" : locs.Longitude,
                }),
                location_files = _dbContext.CcModAppPrjLocationFiles
                                            .Where(w => w.ProjectId == projectId)
                                            .AsNoTracking()
                                            .Select(s => s.AdditionalAttachmentFile)
                                            .ToArray(),
                kml_file_name = pInfo.ProjectBoundaryMap,
                kml_file_content = ReadKmlFile(pInfo.ProjectBoundaryMap)
            });

            var jsonData = Json(geoData);
            return Json(jsonData);
        }

        private string ReadKmlFile(string fileName)
        {
            string result = string.Empty;

            if (!string.IsNullOrEmpty(fileName))
            {
                string path = Path.Combine(hostingEnvironment.WebRootPath, "docs/map_kml", fileName);
                XDocument doc = XDocument.Load(path);
                result = KmlToGeoJsonConverter.FromKml(doc.ToString());
                result = result.Replace(": ", ":");
                result = result.Contains("\"type\":\"Feature\"") ? result : result.Replace("\"feature\":\"Feature\",", "\"feature\":\"Feature\", \"type\":\"Feature\",");
            }

            return result;
        }
    }
}