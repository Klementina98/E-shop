using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using proekt.Models;
using proekt2.Models;

namespace PhoneApp.Controllers
{
    public class ShoppingCartController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: ShoppingCart
        public ActionResult Index(int id)
        {

            ArtWorks artworks = db.ArtWorks.Find(id);
            if (artworks == null)
            {
                return HttpNotFound();
            }
            List<ArtWorks> listId = new List<ArtWorks>();

            if (Session["userArtWorks"] == null)
            {
                Debug.Print("Null e i se kreira nova");
                listId.Add(artworks);
                Session["userArtWorks"] = listId;
                return RedirectToAction("Index", "ArtWorks");
            }
            else
            {

                List<ArtWorks> listArtWorks = Session["userArtWorks"] as List<ArtWorks>;

                foreach (ArtWorks p in listArtWorks.ToList())
                {
                    if (p.Id == id)
                    {
                        Debug.WriteLine("Exists");
                        //return Content("<script language='javascript' type='text/javascript'>alert('Already in there! ');</script>");
                        TempData["error"] = "in";
                        return RedirectToAction("Details", "ArtWorks", artworks);
                        //return RedirectToAction("Details", "Phones");
                        //return base.View();

                    }
                }


                listId = Session["userArtWorks"] as List<ArtWorks>;
                listId.Add(artworks);
                Session["userArtWorks"] = listId;

                Debug.Print("Sesijata postoi i dodavame nov artworks " + id);

                return RedirectToAction("Index", "Artworks");


            }



        }

        public ActionResult showCart()
        {
            if (Session["userArtWorks"] == null)
            {
                return View();
            }
            else
            {

                ViewBag.total = 0;
                foreach (ArtWorks p in Session["userArtWorks"] as IEnumerable<ArtWorks>)
                {
                    var sto = (Int32)ViewBag.total;
                    ViewBag.total = p.price + ((Int32)ViewBag.total);

                }
                Session["total"] = (Int32)ViewBag.total;
                return View(Session["userArtWorks"]);
            }

        }
        public ActionResult deleteFromCart(int ID)
        {
            List<ArtWorks> listId = Session["userArtWorks"] as List<ArtWorks>;

            ArtWorks artwork = db.ArtWorks.Find(ID);
            Debug.Print("Ne nosi vo else znaci brisime artworks " + artwork.name);

            listId.Remove(artwork);
            var st = listId.Find(i => i.Id == ID);
            listId.Remove(st);

            ViewBag.total = ((Int32)Session["total"]) - artwork.price;


            return listId.Count == 0 ? View("showCart", Session["userArtWorks"] = null) : View("showCart", Session["userArtWorks"] = listId);

        }

    }
}