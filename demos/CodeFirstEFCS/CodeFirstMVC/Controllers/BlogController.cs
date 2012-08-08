using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CodeFirstMVC.Models;
using System.Data;

namespace CodeFirstMVC.Controllers
{
    public class BlogController : Controller
    {
      BlogContext db = new BlogContext();

        //
        // GET: /Blog/

        public ActionResult Index()
        {
            return View(db.Blogs);
        }

        //
        // GET: /Blog/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Blog/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Blog/Create

        [HttpPost]
        public ActionResult Create(Blog blog)
        {
            try
            {
              db.Blogs.Add(blog);
              db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /Blog/Edit/5
 
        public ActionResult Edit(int id)
        {
          return View(db.Blogs.Find(id));
        }

        //
        // POST: /Blog/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, Blog blog)
        {
            try
            {
              db.Entry(blog).State = EntityState.Modified;
              db.SaveChanges();
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Blog/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(db.Blogs.Find(id));
        }

        //
        // POST: /Blog/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, Blog blog)
        {
            try
            {
                // TODO: Add delete logic here
              db.Entry(blog).State = EntityState.Deleted;
              db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
