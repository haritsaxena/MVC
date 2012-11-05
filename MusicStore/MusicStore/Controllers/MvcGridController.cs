using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MusicStore.Models;
using MvcJqGrid;

namespace MusicStore.Controllers
{
    public class MvcGridController : Controller
    {
        private MusicStoreDBContext db = new MusicStoreDBContext();

        //
        // GET: /MvcGrid/

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GridDataBasic(GridSettings gridSettings)
        {
            var totalCustomers = db.Albums.Count();
            var albums = db.Albums.ToList();

            var jsonData = new
            {
                total = totalCustomers / gridSettings.PageSize + 1,
                page = gridSettings.PageIndex,
                records = totalCustomers,
                rows = (
                    from c in albums
                    select new
                    {
                        id = c.AlbumId,
                        cell = new[] 
                        { 
                            c.AlbumId.ToString(), 
                            c.Title,
                            c.Artist.Name,
                            c.Genre.Name,
                            c.Price.ToString(),
                            c.AlbumArtUrl
                        }
                    }).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);  
        }
    }
}
