using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using proekt.Models;
using proekt2.Models;

namespace proekt2.Controllers
{
    public class ArtWorksController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ArtWorks
        public ActionResult Index(string sortOrder)
        {
            ViewBag.ArtistSortParm = String.IsNullOrEmpty(sortOrder) ? "artist" : "";
            ViewBag.PriceSortParm = "price";
            ViewBag.PriceDescSortParm = "price_desc";

            //   ViewBag.PriceSortParm = sortOrder == "price" ? "price" : "price_desc";

            if (sortOrder == "price_desc")
                return View(db.ArtWorks.ToList().OrderByDescending(a => a.price));
           

            if (sortOrder == "price")
                return View(db.ArtWorks.ToList().OrderBy(a => a.price));

            if (sortOrder == "artist")
                return View(db.ArtWorks.ToList().OrderBy(a => a.artist_Id));

            else
                return View(db.ArtWorks.ToList());
      
        }

        // GET: ArtWorks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArtWorks artWorks = db.ArtWorks.Find(id);
            if (artWorks == null)
            {
                return HttpNotFound();
            }
            return View(artWorks);
        }

      
        // GET: ArtWorks/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            var model = db.Artists.ToList();
            ViewBag.artists = model;


            return View();
        }

        // POST: ArtWorks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ArtWorks artWorks)
        {
            if (ModelState.IsValid)
            {

                artWorks.artist = db.Artists.Find(artWorks.artist_Id);

                db.ArtWorks.Add(artWorks);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(artWorks);
        }

        // GET: ArtWorks/Edit/5

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArtWorks artWorks = db.ArtWorks.Find(id);
            if (artWorks == null)
            {
                return HttpNotFound();
            }
            return View(artWorks);
        }

        // POST: ArtWorks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,name,img,price,year")] ArtWorks artWorks)
        {
            if (ModelState.IsValid)
            {
                db.Entry(artWorks).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(artWorks);
        }

        // GET: ArtWorks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArtWorks artWorks = db.ArtWorks.Find(id);
            if (artWorks == null)
            {
                return HttpNotFound();
            }
            return View(artWorks);
        }

        // POST: ArtWorks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ArtWorks artWorks = db.ArtWorks.Find(id);
            db.ArtWorks.Remove(artWorks);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
