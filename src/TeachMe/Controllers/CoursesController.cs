using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using TeachMe.Models;

namespace TeachMe.Controllers
{
    public class CoursesController : Controller
    {
        private ApplicationDbContext _context;

        public CoursesController(ApplicationDbContext context)
        {
            _context = context;    
        }



        // GET: Courses
        public IActionResult Index(string search)
        {
            var cur = _context.Courses.ToList();

            if (!System.String.IsNullOrEmpty(search))
            {

                cur = cur.Where(c => c.Title.Equals(search)).ToList();
            }

            return View(cur);
            //return View(_context.Courses.ToList().OrderBy(s => s.Title));
        }




        // GET: Courses/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Course course = _context.Courses.Single(m => m.CourseID == id);
            if (course == null)
            {
                return HttpNotFound();
            }

            return View(course);
        }



        // GET: Courses/Create
        public IActionResult Create()
        {
            return View();
        }




        // POST: Courses/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Course course)
        {
            if (ModelState.IsValid)
            {
                _context.Courses.Add(course);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(course);
        }





        // GET: Courses/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Course course = _context.Courses.Single(m => m.CourseID == id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }





        // POST: Courses/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Course course)
        {
            if (ModelState.IsValid)
            {
                _context.Update(course);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(course);
        }





        // GET: Courses/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Course course = _context.Courses.Single(m => m.CourseID == id);
            if (course == null)
            {
                return HttpNotFound();
            }

            return View(course);
        }






        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            //Find all the rows in StudentsCourses that include the given course id
            var rowsToDelete = _context.StudentsCourses.Where(sc => sc.Course.CourseID == id).ToList();
            foreach ( var c in rowsToDelete)
            {
                //Remov each row
                _context.StudentsCourses.Remove(c);
            }
            Course course = _context.Courses.Single(m => m.CourseID == id);
            _context.Courses.Remove(course);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
