using MVCMusicStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCMusicStore.Controllers
{
    [Authorize]
    public class CheckoutController : Controller
    {
        MusicStoreEntities storeDB = new MusicStoreEntities();
        const string PromoCode = "FREE";

        //
        // GET: /Checkout/AddressAndPayment
        public ActionResult AddressAndPayment()
        {
            return View();
        }

        //
        // POST: /Checkout/AddressAndPayment
        [HttpPost]
        public ActionResult AddressAndPayment(OrderVeiwModel orderVM)
        {
            var order = new Order();
            TryUpdateModel(order);

            try
            {
                if (string.Equals(orderVM.PromoCode, PromoCode, StringComparison.OrdinalIgnoreCase) == true)
                {
                    order.Total = 0;
                }

                // ignore what came across, set username and date
                order.Username = User.Identity.Name;
                order.OrderDate = DateTime.Now;

                // Save order
                storeDB.Orders.Add(order);
                storeDB.SaveChanges();

                //Process the order
                var cart = ShoppingCart.GetCart(this.HttpContext);
                cart.CreateOrder(order);

                return RedirectToAction("Complete", new { id = order.OrderId });

            }
            catch
            {

                return View(order);
            }
        }

        //
        //GET /Checkout/Complete
        public ActionResult Complete(int id)
        {
            //Validate customer owns this order
            bool isValid = storeDB.Orders.Any(
                o => o.OrderId == id &&
                o.Username == User.Identity.Name);

            if(isValid)
            {
                return View(id);
            }
            else
            {
                return View("Error");
            }
        }

    }
}