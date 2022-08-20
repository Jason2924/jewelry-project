using Project_Sem_3.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Project_Sem_3.Areas.Admin.Controllers
{
    public class ItemController : Controller
    {
        private JewelryShoppingEntities db = new JewelryShoppingEntities();

        // GET: Admin/Item
        public ActionResult Index()
        {
            if (Session["admin"] == null)
                return RedirectToAction("Index", "Login");
            var itemMsts = db.ItemMsts.Include(i => i.BrandMst).Include(i => i.CategoryMst).Include(i => i.CertificateMst);
            return View(itemMsts.ToList());
        }

        // GET: Admin/Item/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["admin"] == null)
                return RedirectToAction("Index", "Login");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemMst itemMst = db.ItemMsts.Find(id);
            if (itemMst == null)
            {
                return HttpNotFound();
            }
            return View(itemMst);
        }

        // GET: Admin/Item/Create
        public ActionResult Create()
        {
            if (Session["admin"] == null)
                return RedirectToAction("Index", "Login");
            ViewBag.Brand_ID = new SelectList(db.BrandMsts, "ID", "Name");
            ViewBag.Category_ID = new SelectList(db.CategoryMsts, "ID", "Name");
            ViewBag.Certificate_ID = new SelectList(db.CertificateMsts, "ID", "Type");
            return View();
        }

        // POST: Admin/Item/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,StyleCode,Brand_ID,Category_ID,Certificate_ID,Name,NumberInSet,Quantity,GoldKarat,GoldWeight,Wastage,Price,MRP,Description,CreatedDate")] ItemMst itemMst,
            HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (ModelState.IsValid)
                {
                    var path = "";
                    if (file != null)
                    {
                        if (file.ContentLength > 0)
                        {
                            if (Path.GetExtension(file.FileName).ToLower() == ".jpg" || Path.GetExtension(file.FileName).ToLower() == ".png" ||
                                Path.GetExtension(file.FileName).ToLower() == ".gif" || Path.GetExtension(file.FileName).ToLower() == ".jpeg")
                            {
                                int maxId = db.ItemMsts.Max(i => i.ID) + 1;
                                string extension = Path.GetExtension(file.FileName);
                                string fileName = maxId + extension;
                                path = Path.Combine(Server.MapPath("~/Content/images/product"), fileName);
                                file.SaveAs(path);
                                itemMst.Image = fileName;
                            }
                        }

                    }
                }
                db.ItemMsts.Add(itemMst);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Brand_ID = new SelectList(db.BrandMsts, "ID", "Name", itemMst.Brand_ID);
            ViewBag.Category_ID = new SelectList(db.CategoryMsts, "ID", "Name", itemMst.Category_ID);
            ViewBag.Certificate_ID = new SelectList(db.CertificateMsts, "ID", "Type", itemMst.Certificate_ID);
            return View(itemMst);
        }

        // GET: Admin/Item/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["admin"] == null)
                return RedirectToAction("Index", "Login");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemMst itemMst = db.ItemMsts.Find(id);
            if (itemMst == null)
            {
                return HttpNotFound();
            }
            ViewBag.Image = "/Content/images/product/" + itemMst.Image;
            ViewBag.Brand_ID = new SelectList(db.BrandMsts, "ID", "Name", itemMst.Brand_ID);
            ViewBag.Category_ID = new SelectList(db.CategoryMsts, "ID", "Name", itemMst.Category_ID);
            ViewBag.Certificate_ID = new SelectList(db.CertificateMsts, "ID", "Type", itemMst.Certificate_ID);
            return View(itemMst);
        }

        // POST: Admin/Item/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,StyleCode,Brand_ID,Category_ID,Certificate_ID,Name,NumberInSet,Quantity,GoldKarat,GoldWeight,Wastage,Price,MRP,Description,Image,CreatedDate")] ItemMst itemMst,
            HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                var path = "";
                if (file != null)
                {
                    if (file.ContentLength > 0)
                    {
                        if (Path.GetExtension(file.FileName).ToLower() == ".jpg" || Path.GetExtension(file.FileName).ToLower() == ".png" ||
                            Path.GetExtension(file.FileName).ToLower() == ".gif" || Path.GetExtension(file.FileName).ToLower() == ".jpeg")
                        {
                            string extension = Path.GetExtension(file.FileName);
                            // string fileName = Path.GetFileNameWithoutExtension(file.FileName);
                            // string fullPath = System.Guid.NewGuid().ToString("N") + "-" + itemMst.ID + extension;
                            string fileName = itemMst.ID + extension;
                            path = Path.Combine(Server.MapPath("~/Content/images/product"), fileName);
                            file.SaveAs(path);
                            itemMst.Image = fileName;
                        }
                    }
                    
                }
                db.Entry(itemMst).State = EntityState.Modified;
                db.SaveChanges();
            }
            ViewBag.Brand_ID = new SelectList(db.BrandMsts, "ID", "Name", itemMst.Brand_ID);
            ViewBag.Category_ID = new SelectList(db.CategoryMsts, "ID", "Name", itemMst.Category_ID);
            ViewBag.Certificate_ID = new SelectList(db.CertificateMsts, "ID", "Type", itemMst.Certificate_ID);
            return View(itemMst);
        }

        // GET: Admin/Item/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["admin"] == null)
                return RedirectToAction("Index", "Login");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemMst itemMst = db.ItemMsts.Find(id);
            if (itemMst == null)
            {
                return HttpNotFound();
            }
            return View(itemMst);
        }

        // POST: Admin/Item/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ItemMst itemMst = db.ItemMsts.Find(id);
            db.ItemMsts.Remove(itemMst);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Search(string key)
        {
            if (Session["admin"] == null)
                return RedirectToAction("Index", "Login");
            IEnumerable<ItemMst> itemList = new List<ItemMst>();
            itemList = db.ItemMsts.Where(i => i.Name.Contains(key));
            return View(itemList);
        }
    }
}