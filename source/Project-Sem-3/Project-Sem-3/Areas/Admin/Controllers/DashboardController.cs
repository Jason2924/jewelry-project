using Project_Sem_3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_Sem_3.Areas.Admin.Controllers
{
    public class DashboardController : Controller
    {
        private JewelryShoppingEntities db = new JewelryShoppingEntities();
        // GET: Admin/AdminHome
        public ActionResult Index()
        {
            if (Session["admin"] == null)
                return RedirectToAction("Index", "Login");
            ViewBag.itemCount = db.ItemMsts.Count();
            ViewBag.newOrderCount = db.OrderMsts.Count(x => x.Status == 1);
            ViewBag.userCount = db.UserMsts.Count();
            ViewBag.newInquiryCount = db.InquiryMsts.Count(x => x.Status == false);
            return View();
        }
    }
}