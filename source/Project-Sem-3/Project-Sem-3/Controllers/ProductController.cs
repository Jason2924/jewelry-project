using PagedList;
using Project_Sem_3.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Project_Sem_3.Controllers
{
    public class ProductController : Controller
    {
        private JewelryShoppingEntities db = new JewelryShoppingEntities();
        // GET: Product
        public ActionResult Index(string sort, int page = 1, int size = 12)
        {
            IEnumerable<Product> productList = new List<Product>();
            switch (sort)
            {
                case "name-asc":
                    productList = db.ItemMsts.Join(db.ItemViews, im => im.ID, iv => iv.Item_ID,
                        (im, iv) => new Product { Item = im, Discount = iv.Discount, Sale = iv.Sale })
                        .OrderBy(i => i.Item.Name).ToPagedList(page, size);
                    break;
                case "name-desc":
                    productList = db.ItemMsts.Join(db.ItemViews, im => im.ID, iv => iv.Item_ID,
                        (im, iv) => new Product { Item = im, Discount = iv.Discount, Sale = iv.Sale })
                        .OrderByDescending(i => i.Item.Name).ToPagedList(page, size);
                    break;
                case "price-asc":
                    productList = db.ItemMsts.Join(db.ItemViews, im => im.ID, iv => iv.Item_ID,
                        (im, iv) => new Product { Item = im, Discount = iv.Discount, Sale = iv.Sale })
                        .OrderBy(i => i.Sale).ToPagedList(page, size);
                    break;
                case "price-desc":
                    productList = db.ItemMsts.Join(db.ItemViews, im => im.ID, iv => iv.Item_ID,
                        (im, iv) => new Product { Item = im, Discount = iv.Discount, Sale = iv.Sale })
                        .OrderByDescending(i => i.Sale).ToPagedList(page, size);
                    break;
                default:
                    productList = db.ItemMsts.Join(db.ItemViews, im => im.ID, iv => iv.Item_ID,
                        (im, iv) => new Product { Item = im, Discount = iv.Discount, Sale = iv.Sale })
                        .OrderBy(i => i.Item.ID).ToPagedList(page, size);
                    break;
            }
            SetViewBag();
            ViewBag.Sort = sort;
            return View(productList);
        }

        public ActionResult Category(int id, string sort, int page = 1, int size = 12)
        {
            IEnumerable<Product> productList = new List<Product>();
            switch (sort)
            {
                case "name-asc":
                    productList = db.ItemMsts.Join(db.ItemViews, im => im.ID, iv => iv.Item_ID,
                        (im, iv) => new Product { Item = im, Discount = iv.Discount, Sale = iv.Sale })
                        .Where(i => i.Item.Category_ID == id)
                        .OrderBy(i => i.Item.Name).ToPagedList(page, size);
                    break;
                case "name-desc":
                    productList = db.ItemMsts.Join(db.ItemViews, im => im.ID, iv => iv.Item_ID,
                        (im, iv) => new Product { Item = im, Discount = iv.Discount, Sale = iv.Sale })
                        .Where(i => i.Item.Category_ID == id)
                        .OrderByDescending(i => i.Item.Name).ToPagedList(page, size);
                    break;
                case "price-asc":
                    productList = db.ItemMsts.Join(db.ItemViews, im => im.ID, iv => iv.Item_ID,
                        (im, iv) => new Product { Item = im, Discount = iv.Discount, Sale = iv.Sale })
                        .Where(i => i.Item.Category_ID == id)
                        .OrderBy(i => i.Sale).ToPagedList(page, size);
                    break;
                case "price-desc":
                    productList = db.ItemMsts.Join(db.ItemViews, im => im.ID, iv => iv.Item_ID,
                        (im, iv) => new Product { Item = im, Discount = iv.Discount, Sale = iv.Sale })
                        .Where(i => i.Item.Category_ID == id)
                        .OrderByDescending(i => i.Sale).ToPagedList(page, size);
                    break;
                default:
                    productList = db.ItemMsts.Join(db.ItemViews, im => im.ID, iv => iv.Item_ID,
                        (im, iv) => new Product { Item = im, Discount = iv.Discount, Sale = iv.Sale })
                        .Where(i => i.Item.Category_ID == id)
                        .OrderBy(i => i.Item.ID).ToPagedList(page, size);
                    break;
            }
            SetViewBag();
            ViewBag.Sort = sort;
            ViewBag.Item = db.CategoryMsts.SingleOrDefault(c => c.ID == id);
            return View(productList);
        }

        public ActionResult Brand(int id, string sort, int page = 1, int size = 12)
        {
            IEnumerable<Product> productList = new List<Product>();
            switch (sort)
            {
                case "name-asc":
                    productList = db.ItemMsts.Join(db.ItemViews, im => im.ID, iv => iv.Item_ID,
                        (im, iv) => new Product { Item = im, Discount = iv.Discount, Sale = iv.Sale })
                        .Where(i => i.Item.Brand_ID == id)
                        .OrderBy(i => i.Item.Name).ToPagedList(page, size);
                    break;
                case "name-desc":
                    productList = db.ItemMsts.Join(db.ItemViews, im => im.ID, iv => iv.Item_ID,
                        (im, iv) => new Product { Item = im, Discount = iv.Discount, Sale = iv.Sale })
                        .Where(i => i.Item.Brand_ID == id)
                        .OrderByDescending(i => i.Item.Name).ToPagedList(page, size);
                    break;
                case "price-asc":
                    productList = db.ItemMsts.Join(db.ItemViews, im => im.ID, iv => iv.Item_ID,
                        (im, iv) => new Product { Item = im, Discount = iv.Discount, Sale = iv.Sale })
                        .Where(i => i.Item.Brand_ID == id)
                        .OrderBy(i => i.Sale).ToPagedList(page, size);
                    break;
                case "price-desc":
                    productList = db.ItemMsts.Join(db.ItemViews, im => im.ID, iv => iv.Item_ID,
                        (im, iv) => new Product { Item = im, Discount = iv.Discount, Sale = iv.Sale })
                        .Where(i => i.Item.Brand_ID == id)
                        .OrderByDescending(i => i.Sale).ToPagedList(page, size);
                    break;
                default:
                    productList = db.ItemMsts.Join(db.ItemViews, im => im.ID, iv => iv.Item_ID,
                        (im, iv) => new Product { Item = im, Discount = iv.Discount, Sale = iv.Sale })
                        .Where(i => i.Item.Brand_ID == id)
                        .OrderBy(i => i.Item.ID).ToPagedList(page, size);
                    break;
            }
            SetViewBag();
            ViewBag.Sort = sort;
            ViewBag.Item = db.BrandMsts.SingleOrDefault(b => b.ID == id);
            return View(productList);
        }

        public ActionResult Stone(int id, string sort, int page = 1, int size = 12)
        {
            IEnumerable<Product> productList = new List<Product>();
            switch (sort)
            {
                case "name-asc":
                    productList = db.ItemMsts.Join(db.ItemViews, im => im.ID, iv => iv.Item_ID,
                        (im, iv) => new Product { Item = im, Discount = iv.Discount, Sale = iv.Sale })
                        .Where(i => i.Item.StoneMsts.Contains(i.Item.StoneMsts.FirstOrDefault(s => s.StoneType_ID == id)))
                        .OrderBy(i => i.Item.Name).ToPagedList(page, size);
                    break;
                case "name-desc":
                    productList = db.ItemMsts.Join(db.ItemViews, im => im.ID, iv => iv.Item_ID,
                        (im, iv) => new Product { Item = im, Discount = iv.Discount, Sale = iv.Sale })
                        .Where(i => i.Item.StoneMsts.Contains(i.Item.StoneMsts.FirstOrDefault(s => s.StoneType_ID == id)))
                        .OrderByDescending(i => i.Item.Name).ToPagedList(page, size);
                    break;
                case "price-asc":
                    productList = db.ItemMsts.Join(db.ItemViews, im => im.ID, iv => iv.Item_ID,
                        (im, iv) => new Product { Item = im, Discount = iv.Discount, Sale = iv.Sale })
                        .Where(i => i.Item.StoneMsts.Contains(i.Item.StoneMsts.FirstOrDefault(s => s.StoneType_ID == id)))
                        .OrderBy(i => i.Sale).ToPagedList(page, size);
                    break;
                case "price-desc":
                    productList = db.ItemMsts.Join(db.ItemViews, im => im.ID, iv => iv.Item_ID,
                        (im, iv) => new Product { Item = im, Discount = iv.Discount, Sale = iv.Sale })
                        .Where(i => i.Item.StoneMsts.Contains(i.Item.StoneMsts.FirstOrDefault(s => s.StoneType_ID == id)))
                        .OrderByDescending(i => i.Sale).ToPagedList(page, size);
                    break;
                default:
                    productList = db.ItemMsts.Join(db.ItemViews, im => im.ID, iv => iv.Item_ID,
                        (im, iv) => new Product { Item = im, Discount = iv.Discount, Sale = iv.Sale })
                        .Where(i => i.Item.StoneMsts.Contains(i.Item.StoneMsts.FirstOrDefault(s => s.StoneType_ID == id)))
                        .OrderBy(i => i.Item.ID).ToPagedList(page, size);
                    break;
            }
            SetViewBag();
            ViewBag.Sort = sort;
            ViewBag.Item = db.StoneTypeMsts.SingleOrDefault(s => s.ID == id);
            return View(productList);
        }

        private void SetViewBag()
        {
            var categories = db.CategoryMsts.ToList();
            var brands = db.BrandMsts.ToList();
            var stones = db.StoneTypeMsts.ToList();
            ViewBag.Categories = categories;
            ViewBag.Brands = brands;
            ViewBag.Stones = stones;
        }
    }
}