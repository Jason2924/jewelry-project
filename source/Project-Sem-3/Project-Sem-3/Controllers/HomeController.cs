using Project_Sem_3.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_Sem_3.Controllers
{
    public class HomeController : Controller
    {
        private JewelryShoppingEntities db = new JewelryShoppingEntities();
        public ActionResult Index()
        {
            List<Product> latest = (List<Product>)db.ItemMsts.Join(db.ItemViews, im => im.ID, iv => iv.Item_ID,
                (im, iv) => new Product { Item = im, Discount = iv.Discount, Sale = iv.Sale })
                .Take(10).OrderBy(i => i.Item.CreatedDate).ToList();
            List<Product> sale = (List<Product>)db.ItemMsts.Join(db.ItemViews, im => im.ID, iv => iv.Item_ID,
                (im, iv) => new Product { Item = im, Discount = iv.Discount, Sale = iv.Sale })
                .Where(i => i.Discount > 0 && i.Item.Price > 0).Take(10).ToList();
            ViewBag.Latest = latest;
            ViewBag.Sale = sale;
            return View();
        }

    }
}