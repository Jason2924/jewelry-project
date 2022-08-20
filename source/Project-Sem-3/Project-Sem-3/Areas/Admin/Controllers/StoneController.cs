using Project_Sem_3.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Project_Sem_3.Areas.Admin.Controllers
{
    public class StoneController : Controller
    {
        private JewelryShoppingEntities db = new JewelryShoppingEntities();

        // GET: Admin/Stone
        public ActionResult Index()
        {
            if (Session["admin"] == null)
                return RedirectToAction("Index", "Login");
            var stoneMsts = db.StoneMsts.Include(s => s.ItemMst).Include(s => s.StoneQualityMst).Include(s => s.StoneTypeMst);
            return View(stoneMsts.ToList());
        }

        // GET: Admin/Stone/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["admin"] == null)
                return RedirectToAction("Index", "Login");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StoneMst stoneMst = db.StoneMsts.Find(id);
            if (stoneMst == null)
            {
                return HttpNotFound();
            }
            return View(stoneMst);
        }

        // GET: Admin/Stone/Create
        public ActionResult Create(int? itemId, string itemStyleCode)
        {
            if (Session["admin"] == null)
                return RedirectToAction("Index", "Login");
            ViewBag.Item_ID = itemId;
            ViewBag.Item_StyleCode = itemStyleCode;
            ViewBag.StoneQuality_ID = new SelectList(db.StoneQualityMsts, "ID", "Quality");
            ViewBag.StoneType_ID = new SelectList(db.StoneTypeMsts, "ID", "Name");
            return View();
        }

        // POST: Admin/Stone/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Item_ID,StoneQuality_ID,StoneType_ID,Carat,Dimension,Amount,Image")] StoneMst stoneMst, int itemID, string itemStyleCode)
        {
            if (ModelState.IsValid)
            {
                stoneMst.Item_ID = itemID;
                db.StoneMsts.Add(stoneMst);
                db.SaveChanges();
                return RedirectToAction("Details", "Item", new { id = itemID });
            }
            ViewBag.Item_ID = itemID;
            ViewBag.Item_StyleCode = itemStyleCode;
            ViewBag.StoneQuality_ID = new SelectList(db.StoneQualityMsts, "ID", "Quality", stoneMst.StoneQuality_ID);
            ViewBag.StoneType_ID = new SelectList(db.StoneTypeMsts, "ID", "Name", stoneMst.StoneType_ID);
            return View(stoneMst);
        }

        // GET: Admin/Stone/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["admin"] == null)
                return RedirectToAction("Index", "Login");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StoneMst stoneMst = db.StoneMsts.Find(id);
            if (stoneMst == null)
            {
                return HttpNotFound();
            }
            ViewBag.StoneQuality_ID = new SelectList(db.StoneQualityMsts, "ID", "Quality", stoneMst.StoneQuality_ID);
            ViewBag.StoneType_ID = new SelectList(db.StoneTypeMsts, "ID", "Name", stoneMst.StoneType_ID);
            return View(stoneMst);
        }

        // POST: Admin/Stone/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Item_ID,StoneQuality_ID,StoneType_ID,Carat,Dimension,Amount,Image")] StoneMst stoneMst)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stoneMst).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "Item", new { id = stoneMst.Item_ID });
            }
            ViewBag.StoneQuality_ID = new SelectList(db.StoneQualityMsts, "ID", "Quality", stoneMst.StoneQuality_ID);
            ViewBag.StoneType_ID = new SelectList(db.StoneTypeMsts, "ID", "Name", stoneMst.StoneType_ID);
            return View(stoneMst);
        }

        // GET: Admin/Stone/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["admin"] == null)
                return RedirectToAction("Index", "Login");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StoneMst stoneMst = db.StoneMsts.Find(id);
            if (stoneMst == null)
            {
                return HttpNotFound();
            }
            return View(stoneMst);
        }

        // POST: Admin/Stone/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StoneMst stoneMst = db.StoneMsts.Find(id);
            db.StoneMsts.Remove(stoneMst);
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
            IEnumerable<StoneMst> stoneList = new List<StoneMst>();
            stoneList = db.StoneMsts.Where(x => x.StoneTypeMst.Name.Contains(key));
            return View(stoneList);
        }
    }
}