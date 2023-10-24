using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SafetyAndSecurityForNSI.Controllers
{
    public class DashBoardController : Controller
    {
        // GET: AdminDashBoard
        [Authorize]
        public ActionResult AdminView()
        {
            return View();
        }
        public ActionResult UserView()
        {
            return View();
        }
    }
}