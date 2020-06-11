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
                    return "view";

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
    }
}