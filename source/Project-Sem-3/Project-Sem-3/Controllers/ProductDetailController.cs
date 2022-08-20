using Project_Sem_3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace Project_Sem_3.Controllers
{
    public class ProductDetailController : Controller
    {
        private JewelryShoppingEntities db = new JewelryShoppingEntities();
        // GET: ProductDetail
        public ActionResult Index(int id)
        {
            var item = db.ItemMsts.Join(db.ItemViews, im => im.ID, iv => iv.Item_ID,
                (im, iv) => new Product { Item = im, Discount = iv.Discount, Sale = iv.Sale })
                .SingleOrDefault(i => i.Item.ID == id);
            var stoneType = db.StoneMsts.SelectMany(s => db.StoneTypeMsts.Where(st => st.ID == s.StoneType_ID && s.Item_ID == id)).Distinct();
            ViewBag.StoneType = stoneType;
            return View(item);
        }

        [HttpPost, ActionName("Inquiry")]
        public ActionResult Inquiry(int productId, string styleCode, string fullName, string phone, string email, string address, string city, string country, 
            string brand, string goldKarat, string goldWeight, string[] stone, string[] quality, string[] carat, string[] amount)
        {
            if (CheckRegex("FullName", fullName) == false && CheckRegex("Phone", phone) == false && CheckRegex("Email", email) == false 
                && CheckRegex("Address", address) == false && CheckRegex("City", city) == false && CheckRegex("Country", country) == false
                && CheckRegex("Brand", brand) == false && CheckRegex("GoldKarat", goldKarat) == false && CheckRegex("GoldWeight", goldWeight) == false)
            {
                InquiryMst inquiry = new InquiryMst();
                inquiry.FullName = fullName;
                inquiry.Phone = phone;
                inquiry.Email = email;
                inquiry.Address = address;
                inquiry.City = city;
                inquiry.Country = country;
                string content = "Style Code : " + styleCode + ", Brand : " + brand + ", Gold Karat : " + goldKarat + ", Gold Weight : " + goldWeight;
                for (var i = 0; i < stone.Length; i++)
                {
                    content += "  |  Stone : " + stone[i] + ", Quality : " + quality[i] + ", Carat : " + carat[i] + ", Amount : " + amount[i];
                }
                inquiry.Content = content;
                inquiry.Status = false;
                db.InquiryMsts.Add(inquiry);
                db.SaveChanges();
            }
            return RedirectToAction("Index", new { id = productId });
        }

        public JsonResult StoneInfo()
        {
            db.Configuration.ProxyCreationEnabled = false;
            var type = db.StoneTypeMsts.ToList();
            var quality = db.StoneQualityMsts.ToList();
            var stone = new { Type = type, Quality = quality };
            return Json(stone, JsonRequestBehavior.AllowGet);
        }

        private string GetError(string field, string value, string pattern)
        {
            if (value == null || value == "")
            {
                return "Can not empty";
            }
            Regex regex = new Regex(pattern);
            Match match = regex.Match(value);
            if (!match.Success)
            {
                return "Please input valid " + field;
            }
            return "";
        }

        private bool CheckRegex(string field, string value)
        {
            var result = "";
            switch (field)
            {
                case "FullName": result = GetError(field, value, @"^(([a-zA-Z])+([ ]{0,1})?)+$"); break;
                case "Phone": result = GetError(field, value, @"^[0-9]{10,12}$"); break;
                case "Email": result = GetError(field, value, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"); break;
                case "Address": result = GetError(field, value, @"^(([a-zA-Z0-9])+([ ,-]*)?)+$"); break;
                case "City": result = GetError(field, value, @"^(([a-zA-Z])+([ ]{0,1})?)+$"); break;
                case "Country": result = GetError(field, value, @"^(([a-zA-Z])+([ ]{0,1})?)+$"); break;
                case "Brand": result = GetError(field, value, @"^(([a-zA-Z0-9])+([ ,-]*)?)+$"); break;
                case "GoldKarat": result = GetError(field, value, @"^[0-9]{1,2}$"); break;
                case "GoldWeight": result = GetError(field, value, @"^[0-9]+([.]?([0-9]+))$"); break;
            }
            if (result != "") { TempData[field] = result; return true; }
            return false;
        }
    }
}