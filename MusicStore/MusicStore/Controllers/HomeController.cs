using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MusicStore.Models;

namespace MusicStore.Controllers
{
    public class HomeController : Controller
    {
        private MusicStoreDBContext db = new MusicStoreDBContext();

        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult DailyDeal()
        {
            var album = GetDailyDeal();
            return PartialView("_DailyDeal", album);
        }

        private Album GetDailyDeal()
        {
            return db.Albums
            .OrderBy(a => a.Price)
            .First();
        }
    }
}
