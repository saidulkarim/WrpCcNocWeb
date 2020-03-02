using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WrpCcNocWeb.Models;
using Microsoft.AspNetCore.Http;
using WrpCcNocWeb.Helpers;
using static WrpCcNocWeb.Helpers.CommonHelper;
using WrpCcNocWeb.Models.UserManagement;
using WrpCcNocWeb.DatabaseContext;

namespace WrpCcNocWeb.Controllers
{
    public class homeController : Controller
    {
        #region Initialization
        HttpContext context;
        private readonly LoggedUserInfo iLoggedUser;
        private readonly WrpCcNocDbContext _db = new WrpCcNocDbContext();
        private readonly CommonHelper ch = new CommonHelper();
        #endregion

        private readonly ILogger<homeController> _logger;

        public homeController(ILogger<homeController> logger)
        {
            _logger = logger;
        }

        public IActionResult index()
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
            //string UserRegistrationID = HttpContext.Session.GetString("UserRegistrationID").ToString();
            //string UserName = HttpContext.Session.GetString("UserName").ToString();
            //string UserEmail = HttpContext.Session.GetString("UserEmail").ToString();
            //string UserMobile = HttpContext.Session.GetString("UserMobile").ToString();

            //if (ui != null)
            //{
            //    TempData["Message"] = ch.ShowMessage(Sign.Info, Sign.Info.ToString(), "Hello, this is " + ui.UserID);
            //}
            //else
            //{
            //    TempData["Message"] = ch.ShowMessage(Sign.Info, Sign.Info.ToString(), "Hello, Session not working.");
            //    return RedirectToActionPermanent("login", "account");
            //}

            return View();
        }

        public IActionResult privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
