using Project.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Project.Controllers
{
    public class CommentController : Controller
    {
        VehicleRentalDBEntities _db = new VehicleRentalDBEntities();
        // GET: Comment
        public ActionResult Index()
        {
            return View(_db.tblComments.ToList());
        }

        // GET: ArticlesComments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var articlesComment = _db.tblComments.Find(id);
            if (articlesComment == null)
            {
                return HttpNotFound();
            }
            return View(articlesComment);
        }

        // GET: ArticlesComments/Create
        public ActionResult Create()
        {
            return View();
        }
      
        [HttpPost]

        [ValidateAntiForgeryToken]
        public ActionResult Add(FormCollection form)
        {
            var comment = form["Comment"].ToString();
            var articleId = int.Parse(form["VehicleId"]);
            var rating = int.Parse(form["Rating"]);
            var username = User.Identity.Name;
            tblComment artComment = new tblComment()
            {
                VehicleId = articleId,
                Comments = comment,
                Rating = rating,
                ThisDateTime = DateTime.Now,
                UserName = username
            };

            _db.tblComments.Add(artComment);
            _db.SaveChanges();

            return RedirectToAction("Rate", "Item", new { id = articleId });
        }

        // POST: ArticlesComments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CommentId,Comments,ThisDateTime,VehicleId,Rating")] tblComment articlesComment)
        {
            if (ModelState.IsValid)
            {
                _db.tblComments.Add(articlesComment);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(articlesComment);
        }

        // GET: ArticlesComments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblComment articlesComment = _db.tblComments.Find(id);
            if (articlesComment == null)
            {
                return HttpNotFound();
            }
            return View(articlesComment);
        }

        // POST: ArticlesComments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CommentId,Comments,ThisDateTime,ArticleId,Rating")] tblComment articlesComment)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(articlesComment).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(articlesComment);
        }

        // GET: ArticlesComments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblComment articlesComment = _db.tblComments.Find(id);
            if (articlesComment == null)
            {
                return HttpNotFound();
            }
            return View(articlesComment);
        }

        // POST: ArticlesComments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblComment articlesComment = _db.tblComments.Find(id);
            _db.tblComments.Remove(articlesComment);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}