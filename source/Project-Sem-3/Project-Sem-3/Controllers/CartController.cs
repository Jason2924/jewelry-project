using Project_Sem_3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_Sem_3.Controllers
{
    public class CartController : Controller
    {
        private JewelryShoppingEntities db = new JewelryShoppingEntities();
        // GET: Cart
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Insert(int id, int quantity)
        {
            List<Cart> cartList = new List<Cart>();
            if (Session["cart"] != null)
            {
                cartList = (List<Cart>)Session["cart"];
            }
            if (cartList.Exists(c => c.Item.ID == id))
            {
                var cart = cartList.SingleOrDefault(c => c.Item.ID == id);
                cart.Quantity += quantity;
            }
            else
            {
                Cart cart = new Cart();
                Product product = db.ItemMsts.Join(db.ItemViews, im => im.ID, iv => iv.Item_ID,
                (im, iv) => new Product { Item = im, Discount = iv.Discount, Sale = iv.Sale })
                .SingleOrDefault(i => i.Item.ID == id);
                cart.Item = product.Item;
                cart.Price = (decimal)product.Sale;
                cart.Quantity = quantity;
                cartList.Add(cart);
            }
            Session["cart"] = cartList;
            return RedirectToAction("Index", "Cart");
        }

        public ActionResult Update(int id, int quantity)
        {
            List<Cart> cartList = (List<Cart>)Session["cart"];
            var cart = cartList.SingleOrDefault(c => c.Item.ID == id);
            cart.Quantity = quantity;
            Session["cart"] = cartList;
            return RedirectToAction("Index", "Cart");
        }

        public ActionResult Delete(int id)
        {
            List<Cart> cartList = (List<Cart>)Session["cart"];
            var cart = cartList.SingleOrDefault(c => c.Item.ID == id);
            cartList.Remove(cart);
            if (cartList.Count() == 0)
            {
                Session["cart"] = null;
            }
            else
            {
                Session["cart"] = cartList;
            }
            return RedirectToAction("Index", "Cart");
        }
    }
}