using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using TeachMe.Models;
using System.Collections.Generic;
using System;

namespace TeachMe.Controllers
{
    public class StudentsController : Controller
    {
        private ApplicationDbContext _context= new ApplicationDbContext();

        public StudentsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Students

      
        public IActionResult Index(string search)
        {
            var stud = _context.Students.ToList();
            if (!String.IsNullOrEmpty(search))
            {
                stud = stud.Where(s => s.FirstName.Equals(search)).ToList().OrderBy(u=>u.FirstName).ToList();

            }
            return View(stud);
            //return View(_context.Students.ToList().OrderBy(s =>s.FirstName));
        }


      


        //Get studets's course list by ID
        public IActionResult CourseList( int ? id)
        {
            if(id==null)
            {
                return HttpNotFound();
            }
            //Find all students's courses
            var courses = _context.StudentsCourses.Where(s => s.Student.StudentID == id).Select(c => c.Course);
            
            if (courses==null)
            {
                return RedirectToAction("Index");
            }
            return View(courses);
        }

        // GET: Students/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            //Get course list by ID
            var courses = _context.StudentsCourses.Where(s => s.Student.StudentID == id).Select(c => c.Course);
            ViewBag.Courses = courses;
            Student student = _context.Students.Single(m => m.StudentID == id);
            if (student == null)
            {
                return HttpNotFound();
            }

            return View(student);
        }





        // GET: Students/Create
        public IActionResult Create()
        {
            ViewBag.Courses = _context.Courses.ToList().OrderBy(s => s.Title);
            return View();
        }






        //Get all courses by ID and add to the StudentsCourse Table
        // POST: Students/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Student student, int[] selectedCourses)
        {
            if (selectedCourses != null)
            {
                student.Jubot = 400;
                foreach (var course in selectedCourses)
                {
                    //Find the course by ID
                    var courseToAdd = _context.Courses.SingleOrDefault(x => x.CourseID == course);
                    var sc = new StudentsAndCourses()
                    {
                        Student = student,
                        Course = courseToAdd
                    };
                    //Add the student and realted course to StudentCourse Table
                   _context.StudentsCourses.Add(sc);
                }
                //Save the changes
               // _context.SaveChanges();
            }
            //Add the student to Students Table
            if (ModelState.IsValid)
            {
              
               
                _context.Students.Add(student);
                _context.SaveChanges();
                return RedirectToAction("StudentDetails" ,"Home");
            }
            return View(student);
        }




        // GET: Students/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            //Find the courses that selected
            var selectedCourses=_context.StudentsCourses.Where(s =>s.Student.StudentID==id).Select(c => c.Course);
            var courses = _context.Courses.ToList();
            //Find the courses that not selected
            var notselectedCourses = courses.Except(selectedCourses).ToList();
            //Send the datato the view
            ViewBag.SelectedCourse = selectedCourses;
            ViewBag.NotSelectedCourses = notselectedCourses;
            Student student = _context.Students.Single(m => m.StudentID == id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }







        // POST: Students/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Student student,int[] toremove ,int[] toadd)
        {
            //Renove the 
            if(toremove != null)
            {
                //Find the row in the table that match the studentID
                var remove = _context.StudentsCourses.Where(s => s.Student.StudentID == student.StudentID);
                if (toremove.Length!=0)
                {
                    foreach (var item in remove)
                    {
                        //Remove from the tabel
                        _context.StudentsCourses.Remove(item);

                    }
                    _context.SaveChanges();
                }

            }
            //Add Courses to the StudentCourses Table
            if (toadd !=null)
            {
            foreach (var course in toadd)
            {
                //Find the course by ID
                var courseToAdd = _context.Courses.SingleOrDefault(x => x.CourseID == course);
                var sc = new StudentsAndCourses()
                {
                    Student = student,
                    Course = courseToAdd

                };
                //Add the student and realted course to StudentCourse Table
                _context.StudentsCourses.Add(sc);

            }
            //Save the changes
            _context.SaveChanges();
        }


            //Update the student
            if (ModelState.IsValid)
            {
                _context.Update(student);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(student);
        }


        // GET: Students/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Student student = _context.Students.Single(m => m.StudentID == id);
            if (student == null)
            {
                return HttpNotFound();
            }

            return View(student);
        }


        //Find to 5 students
        public ActionResult AjaxTop5()
        {
            // return by descending clicks , and take 5 products.
            var stud = _context.Students.Include(x=>x.StudentsCourses).OrderByDescending(s => s.Jubot).Take(5);
            return View("~/Views/Shared/_StudentList", stud);
        }

        //Find all
        public ActionResult AjaxShowAll()
        {
            // return by descending clicks , and take 5 products.
            var stud = _context.Students;
            return View("~/Views/Shared/_StudentList", stud);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            //Find the courses that belong to the user
            var coursesToDelete = _context.StudentsCourses.Where(s => s.Student.StudentID == id);
            if (coursesToDelete != null)
            {
                foreach( var item in coursesToDelete)
                {
                    //Renove the from the tabel
                    _context.StudentsCourses.Remove(item);
                    
                }
                _context.SaveChanges();
            }
            Student student = _context.Students.Single(m => m.StudentID == id);
            _context.Students.Remove(student);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

     
        

        
    }
}
