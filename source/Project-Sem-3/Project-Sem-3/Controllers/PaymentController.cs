using Project_Sem_3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_Sem_3.Controllers
{
    public class PaymentController : Controller
    {
        private JewelryShoppingEntities db = new JewelryShoppingEntities();
        // GET: Payment
        public ActionResult Index(bool? isSuccess)
        {
            if (isSuccess == true)
            {
                ViewBag.Success = "Your order has be sent, we will accept your order in 24 hours!";
                return View();
            }
            else if (Session["cart"] == null)
            {
                return RedirectToAction("Index", "Cart");
            }
            else if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        [HttpPost, ActionName("Index")]
        public ActionResult Index(OrderMst order)
        {
            if (ModelState.IsValid)
            {
                string username = (string)Session["user"];
                var user = db.UserMsts.SingleOrDefault(u => u.Username == username);
                order.User_ID = user.ID;
                // 1 = Awaiting, 2 = Acepted, 3 = Shipping, 4 = Done, 0 = Cancel
                order.Status = 1;
                db.OrderMsts.Add(order);
                db.SaveChanges();
                int orderId = db.OrderMsts.Max(o => o.ID);
                CreateOrderDetail(orderId);
                Session["cart"] = null;
                return RedirectToAction("Index", new { isSuccess = true });
            }
            return View(order);
        }

        private void CreateOrderDetail(int orderId)
        {
            List<Cart> cartList = (List<Cart>)Session["cart"];
            foreach (var cart in cartList)
            {
                OrderDetailMst orderDetail = new OrderDetailMst();
                orderDetail.Item_ID = cart.Item.ID;
                orderDetail.Order_ID = orderId;
                orderDetail.Price = cart.Price;
                orderDetail.Quantity = cart.Quantity;
                db.OrderDetailMsts.Add(orderDetail);
            }
            db.SaveChanges();
        }

        public JsonResult UserInfo()
        {
            db.Configuration.ProxyCreationEnabled = false;
            string username = (string)Session["user"];
            var user = db.UserMsts.SingleOrDefault(u => u.Username == username);
            return Json(user, JsonRequestBehavior.AllowGet);
        }
    }
}