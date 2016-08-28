using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using TeachMe.Models;

namespace TeachMe.Controllers
{
    public class StudentsAndCoursesController : Controller
    {
        private ApplicationDbContext _context;

        public StudentsAndCoursesController(ApplicationDbContext context)
        {
            _context = context;    
        }




        // GET: StudentsAndCourses
        public IActionResult Index()
        {
            return View(_context.StudentsCourses.ToList());
        }




        // GET: StudentsAndCourses/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            StudentsAndCourses studentsAndCourses = _context.StudentsCourses.Single(m => m.ID == id);
            if (studentsAndCourses == null)
            {
                return HttpNotFound();
            }

            return View(studentsAndCourses);
        }




        // GET: StudentsAndCourses/Create
        public IActionResult Create()
        {
            return View();
        }





        // POST: StudentsAndCourses/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(StudentsAndCourses studentsAndCourses)
        {
            if (ModelState.IsValid)
            {
                _context.StudentsCourses.Add(studentsAndCourses);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(studentsAndCourses);
        }






        // GET: StudentsAndCourses/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            StudentsAndCourses studentsAndCourses = _context.StudentsCourses.Single(m => m.ID == id);
            if (studentsAndCourses == null)
            {
                return HttpNotFound();
            }
            return View(studentsAndCourses);
        }





        // POST: StudentsAndCourses/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(StudentsAndCourses studentsAndCourses)
        {
            if (ModelState.IsValid)
            {
                _context.Update(studentsAndCourses);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(studentsAndCourses);
        }





        // GET: StudentsAndCourses/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            StudentsAndCourses studentsAndCourses = _context.StudentsCourses.Single(m => m.ID == id);
            if (studentsAndCourses == null)
            {
                return HttpNotFound();
            }

            return View(studentsAndCourses);
        }





        // POST: StudentsAndCourses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            StudentsAndCourses studentsAndCourses = _context.StudentsCourses.Single(m => m.ID == id);
            _context.StudentsCourses.Remove(studentsAndCourses);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
