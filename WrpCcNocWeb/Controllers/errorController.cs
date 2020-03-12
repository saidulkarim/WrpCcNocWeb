using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WrpCcNocWeb.Controllers
{
    public class errorController : Controller
    {
        public IActionResult index()
        {
            return View();
        }
    }
}