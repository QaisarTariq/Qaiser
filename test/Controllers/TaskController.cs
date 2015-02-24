using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Task;
using Task.Models;

namespace task
{
    public class TaskController : Controller
    {
        
        private DB_TaskEntities db = new DB_TaskEntities();

        

        public ActionResult Index()
        {
            return View(db.tbl_Task.ToList());
        }

       [HttpGet]
        public ViewResult index(string q)
        
       {
            var title = from p in db.tbl_Task select p;
            if (!string.IsNullOrWhiteSpace(q))
            {
                title = title.Where(p => p.strTitle.Contains(q));               
            }

            return View(title);
            
        }

       
        public ActionResult Details(int id = 0)
        {
            tbl_Task tbl_task = db.tbl_Task.Find(id);
            if (tbl_task == null)
            {
                return HttpNotFound();
            }
            return View(tbl_task);
        }

       
        public ActionResult Create()
        {
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(tbl_Task tbl_task)
        {
            if (ModelState.IsValid)
            {
                db.tbl_Task.Add(tbl_task);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbl_task);
        }

       
        public ActionResult Edit(int id = 0)
        {
            tbl_Task tbl_task = db.tbl_Task.Find(id);
            if (tbl_task == null)
            {
                return HttpNotFound();
            }
            return View(tbl_task);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tbl_Task tbl_task)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_task).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_task);
        }

       
        public ActionResult Delete(int id = 0)
        {
            tbl_Task tbl_task = db.tbl_Task.Find(id);
            if (tbl_task == null)
            {
                return HttpNotFound();
            }
            return View(tbl_task);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_Task tbl_task = db.tbl_Task.Find(id);
            db.tbl_Task.Remove(tbl_task);
            db.SaveChanges();
            return RedirectToAction("Index");
        }



        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

    }
}