using iTed2.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace iTed2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            iTed2Service service = new iTed2Service();
            var topVideoList = service.getTopThreeVideo();

            return View(topVideoList);
        }

        public ActionResult About()
        {
            ViewBag.Message = "MerryXmas";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}