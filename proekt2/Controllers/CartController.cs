using proekt2.Models;
using Stripe;
//using StripeAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace proekt2.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Index()
        {
            return View();
        }

        // GET: Cart/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Cart/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cart/Create
        [HttpPost]
        public ActionResult Create(ChargeDTO chargeDTO)
        {
            StripeConfiguration.ApiKey = "sk_test_p7M6bUes8HjJDp7SReq3plsl00PG28G5da";

            var customerOptions = new CustomerCreateOptions
            {
                Description = chargeDTO.name,
                Name = chargeDTO.name,
                Source = chargeDTO.stripeToken,
                Email = chargeDTO.email,
                Metadata = new Dictionary<string, string>()
                {
                    { "Phone Number", chargeDTO.phone },
                    { "Street", chargeDTO.street }
                }
            };
            var customerService = new CustomerService();
            Customer customer = customerService.Create(customerOptions);


            var options = new ChargeCreateOptions
            {
                Amount = chargeDTO.price * 100,
                Currency = "eur",
                Description = "Charge for " + customer.Email,
                //Source = chargeDTO.stripeToken,
                Customer = customer.Id
            };

            var service = new ChargeService();
            Charge charge = service.Create(options);

            var model = new ChargeViewModel();
            model.ChargeId = charge.Id;
            Session["userPhones"] = null;

            return View("OrderStatus", model);
        }

        // GET: Cart/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Cart/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Cart/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Cart/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
