using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using TeachMe.Models;
using System;

namespace TeachMe.Controllers
{
    public class ReviewsController : Controller
    {
        private ApplicationDbContext _context;

        public ReviewsController(ApplicationDbContext context)
        {
            _context = context;    
        }




        // GET: Reviews
        public IActionResult Index()
        {
            var applicationDbContext = _context.Reviews.Include(r => r.Student);
            return View(applicationDbContext.ToList());
        }




        // GET: Reviews/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Review review = _context.Reviews.Single(m => m.ReviewID == id);
            if (review == null)
            {
                return HttpNotFound();
            }

            return View(review);
        }





        // GET: Reviews/Create
        public IActionResult Create()
        {
            ViewData["StudentID"] = new SelectList(_context.Students, "StudentID", "Student");
            return View();
        }






        // POST: Reviews/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Review review)
        {

            if (ModelState.IsValid)
            {
                DateTime now = DateTime.Now;
                review.Date = now;
                _context.Reviews.Add(review);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["StudentID"] = new SelectList(_context.Students, "StudentID", "Student", review.StudentID);
            return View(review);
        }





        // GET: Reviews/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Review review = _context.Reviews.Single(m => m.ReviewID == id);
            if (review == null)
            {
                return HttpNotFound();
            }
            ViewData["StudentID"] = new SelectList(_context.Students, "StudentID", "Student", review.StudentID);
            return View(review);
        }





        // POST: Reviews/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Review review)
        {
            if (ModelState.IsValid)
            {
                _context.Update(review);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["StudentID"] = new SelectList(_context.Students, "StudentID", "Student", review.StudentID);
            return View(review);
        }

        // GET: Reviews/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Review review = _context.Reviews.Single(m => m.ReviewID == id);
            if (review == null)
            {
                return HttpNotFound();
            }

            return View(review);
        }





        // POST: Reviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Review review = _context.Reviews.Single(m => m.ReviewID == id);
            _context.Reviews.Remove(review);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
