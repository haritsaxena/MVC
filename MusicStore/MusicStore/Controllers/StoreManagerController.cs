using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MusicStore.Models;


namespace MusicStore.Controllers
{
    public class StoreManagerController : Controller
    {
        private MusicStoreDBContext db = new MusicStoreDBContext();

        //
        // GET: /StoreManager/

        public ActionResult Index()
        {
            var albums = db.Albums.Include(a => a.Genre).Include(a => a.Artist);
            return View(albums.ToList());
        }

        //
        // GET: /StoreManager/Details/5

        public ActionResult Details(int id = 0)
        {
            Album album = db.Albums.Find(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            return View(album);
        }

        //
        // GET: /StoreManager/Create

        public ActionResult Create()
        {
            ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "Name");
            ViewBag.ArtistId = new SelectList(db.Artists, "ArtistId", "Name");
            return View();
        }

        //
        // POST: /StoreManager/Create

        [HttpPost]
        public ActionResult Create(Album album)
        {
            if (ModelState.IsValid)
            {
                db.Albums.Add(album);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "Name", album.GenreId);
            ViewBag.ArtistId = new SelectList(db.Artists, "ArtistId", "Name", album.ArtistId);
            return View(album);
        }

        //
        // GET: /StoreManager/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Album album = db.Albums.Find(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            // ViewBag.GenreId = new SelectList(db.Genres.OrderBy(g => g.Name), "GenreId", "Name", album.GenreId);

            ViewBag.GenreId =
                this.db.Genres.OrderBy(g => g.Name).AsEnumerable().Select(
                    g =>
                    new SelectListItem
                        {
                            Text = g.Name, 
                            Value = g.GenreId.ToString(), 
                            Selected = album.GenreId == g.GenreId 
                        });

            this.ViewBag.ArtistId = new SelectList(this.db.Artists, "ArtistId", "Name", album.ArtistId);
            return View(album);
            //return View("ModifiedEdit", album);
        }

        //
        // POST: /StoreManager/Edit/5

        //[HttpPost]
        //public ActionResult Edit(Album album)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(album).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "Name", album.GenreId);
        //    ViewBag.ArtistId = new SelectList(db.Artists, "ArtistId", "Name", album.ArtistId);
        //    return View(album);
        //}

        [HttpPost]
        public ActionResult Edit()
        {
            var album = new Album();
            TryUpdateModel(album);
            if (ModelState.IsValid)
            {
                db.Entry(album).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "Name", album.GenreId);
            //ViewBag.ArtistId = new SelectList(db.Artists, "ArtistId", "Name", album.ArtistId);
            return View(album);
        }
        //
        // GET: /StoreManager/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Album album = db.Albums.Find(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            return View(album);
        }

        //
        // POST: /StoreManager/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Album album = db.Albums.Find(id);
            db.Albums.Remove(album);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }


        public ActionResult QuickSearch(string term)
        {
            var artists = GetArtists(term).Select(a => new { value = a.Name });
            return Json(artists, JsonRequestBehavior.AllowGet);
        }
        private List<Artist> GetArtists(string searchString)
        {
            return db.Artists.Where(a => a.Name.Contains(searchString)).ToList();
        }


        public ActionResult All()
        {
            return View();
        }

        public JsonResult AllAlbums()
        {
            var albums = new List<Album>()
                             {
                                 new Album() {AlbumId = 1, Title = "Asd", Price = (decimal) 23.22 },
                                 new Album() {AlbumId = 2, Title = "efg", Price = (decimal) 177.22}
                             };

            var productQuery = from prod in albums select new {prod.AlbumId, prod.Title, prod.Price };

            //// http://josephschrag.blogspot.in/2012/02/using-jqgrid-with-jsonresult.html
            //// http://www.codeproject.com/Articles/424640/ASP-NET-MVC-HTML-Helper-for-the-jqGrid
            var results = new
            {
                total = 1, //number of pages
                page = 1, //current page
                records = 2, //total items
                rows = productQuery
            };
            //var albums = db.Albums.Include(a => a.Genre).Include(a => a.Artist);
            return Json(results, JsonRequestBehavior.AllowGet);

            
        }
    }
}