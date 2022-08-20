using PagedList;
using Project_Sem_3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_Sem_3.Controllers
{
    public class SearchController : Controller
    {
        private JewelryShoppingEntities db = new JewelryShoppingEntities();
        // GET: Search
        public ActionResult Index(string keyword, int page = 1, int size = 15)
        {
            IEnumerable<Product> productList = new List<Product>();
            productList = db.ItemMsts.Join(db.ItemViews, im => im.ID, iv => iv.Item_ID,
                (im, iv) => new Product { Item = im, Discount = iv.Discount, Sale = iv.Sale })
                .Where(i => i.Item.Name.Contains(keyword)).OrderBy(i => i.Item.CreatedDate).ToPagedList(page, size);
            ViewBag.Key = keyword;
            return View(productList);
        }
    }
}