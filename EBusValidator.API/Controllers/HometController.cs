using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EBusValidator.API.Controllers
{
    public class HomeController : Controller
    {
        // GET: Homet
        public ActionResult Index()
        {
            return View();
        }
    }
}