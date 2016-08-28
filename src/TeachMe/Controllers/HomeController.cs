using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Mvc;
using TeachMe.Models;
using TeachMe.ViewModels;

namespace TeachMe.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _context = new ApplicationDbContext();

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        //Return the start view
        public IActionResult Start()
        {
            return View();
        }


       

        public IActionResult Social()
        {

            return View();
        }





        //Show all rewards, Return the RewardsDetails view +Search
        public IActionResult RewardsDetails( string search)
        {

            var rew = _context.Rewards.ToList();

            if (!String.IsNullOrEmpty(search))
            {
                rew = rew.Where(r => r.Title.Contains(search)).ToList();
            }
            return View(rew);
        }


        //chaeck if i can to redirect to student controller

        public IActionResult StudentDetails(string search,string filter)
        {
             ViewData["sc"] = _context.StudentsCourses.ToList();

            var stud = _context.Students.ToList().OrderBy(u => u.FirstName).ToList();
  
            if (!String.IsNullOrEmpty(search))
            {
                stud = stud.Where(s => s.FirstName.Equals(search)).ToList().OrderBy(u => u.FirstName).ToList();
            }
            
            //return View(_context.Students.ToList().OrderBy(s => s.FirstName));

            if (!String.IsNullOrEmpty(filter))
            {
                switch (filter)
                {
                    case "FirstName":
                        stud = stud.OrderBy(u => u.FirstName).ToList();
                        break;
                    case "LastName":
                        stud = stud.OrderBy(u => u.LastName).ToList();
                        break;
                    case "Money":
                        stud = stud.OrderBy(u => u.Jubot).ToList();
                        break;
                    case "Money Per Hour":
                        stud = stud.OrderBy(u => u.JubotPerHour).ToList();
                        break;
                    case "Address":
                        stud = stud.OrderBy(u => u.Address).ToList();
                        break;
                    default:
                        stud = stud.OrderBy(u => u.FirstName).ToList();
                        break;
                }
            }

            return View(stud);
        }

        //Return the Index view
        public IActionResult Index()
        {
            return View();
        }


        //Return the contact view
        public IActionResult Contact()
        {
           
            return View();
        }





        public IActionResult Error()
        {
            return View();
        }


        public IActionResult StudentsGraph()
        {
            return View();
        }

        //Hold the adress of student for studenst map
        public class places
        {
            public string addr { get; set; }
        }


        //Get address of students
        public JsonResult getAddresses()
        {
            List<places> address = new List<places>();

            foreach (var item in _context.Students)
            {
                places place = new places();

                if (!String.IsNullOrEmpty(item.Address))
                {
                    place.addr = item.Address;
                    address.Add(place);
                }
            }
            return Json(address);
        }




        //Return the student map view
        public IActionResult StudentsMap()
        {
            return View();
        }



        /*
        public List<Course> IActionResult(int id)
        {
            var courses = _context.StudentsCourses.Where(s => s.Student.StudentID == id).Select(c => c.Course);
           return ((List<Course>)courses);
        }*/

        
        //Search for review by name , address and jubot per hour
        [HttpPost]
        public IActionResult Search(string Name, string Address, int jubot)
        {
            
            var name = from l in _context.Students
                        where l.FirstName.Equals(Name) && l.Address.Contains(Address) && l.JubotPerHour >= jubot
                       select l;


            if (name == null)
            {

                List<object> BothModels = new List<object>();
                BothModels.Add(_context.Students.ToList());
                BothModels.Add(_context.Reviews.ToList());
                return View("StudentsReview",BothModels);
            }

            else
            {
                List<object> BothModels = new List<object>();
                BothModels.Add(name.ToList());
                BothModels.Add(_context.Reviews.ToList());
                return View("StudentsReview", BothModels);
            }
        }




        //Show all students reviews
        public IActionResult StudentsReview()
        {
            List<object> BothModels = new List<object>();
            BothModels.Add(_context.Students.ToList());
            BothModels.Add(_context.Reviews.ToList());
            return View(BothModels);

        }




        //Insrt Review for student
        public IActionResult InsertReview(string Name, string Revieww, int rate, int StudentID)
        {
            Review review = new Review();


            review.Date = DateTime.Now;  
            review.Name = Name;
            review.Revieww = Revieww;
            review.StudentID = StudentID;
            review.rate = (Rate)rate;
            _context.Reviews.Add(review);
            _context.SaveChanges();
            return RedirectToAction("StudentsReview");
        }





        //Show rewards of student
        public IActionResult showRewards(int id)
        {

            //Find the student by ID
            var stud = _context.Students.SingleOrDefault(x => x.StudentID == id);
            //Get full Name
            ViewBag.Name = stud.FirstName + " " + stud.LastName;
            //Find all the rewards that the user can get
            var result = from reward in _context.Rewards
                         where stud.Jubot >= reward.NumOfJubot
                         select new RewardDet()
                         {
                             RewardName = reward.Title,
                             Picture = reward.Picture,
                             Video = reward.Video,
                         };

         
           
            return View(result);
        }




        //Return the Course Graph
        public IActionResult CoursesGraph()
        {

            return View();
        }


        



        public JsonResult ShowCourses(int id)
        {
            var result = _context.StudentsCourses.Where(s => s.Student.StudentID == id).Select(c => c.Course);


            return Json(result);
        }




        //Hold the values of the CourseGraph
        public class Result
        {
            public string State { get; set; }
            public int freq { get; set; }
        }

        

        //Courses Graph Init
        public JsonResult CoursesGraphInit()
        {

            List<Result> CorsesGraph = new List<Result>();
            //Create table with the courses
            var query = from sc in _context.StudentsCourses
                        join c in _context.Courses on sc.Course.CourseID equals c.CourseID
                        select new
                        {
                            c.Title,
                            c.CourseID
                        };

            //Group by the courses table by name and count
            var group = from x in query.ToList()
                        group x by x.Title into cat
                        select new
                        {
                            FirstName = cat.Key,
                            Num = cat.Count()
                        };

            //Insert the values in group to list of result
            foreach (var item in group)
            {
                Result Course = new Result();
                Course.State = item.FirstName;
                Course.freq = item.Num;


                CorsesGraph.Add(Course);
            }
            //return the list
            return Json(CorsesGraph);
        }


        //Students Graph Init
        public JsonResult StudentsGraphInit()
        {

            List<Result> StudentsGraph = new List<Result>();


            var query = from y in _context.Students
                        join l in _context.StudentsCourses on y.StudentID equals l.Student.StudentID
                        select new
                        {
                            y.FirstName,
                            y.StudentID
                        };
            //group for the graph
            var group = from x in query.ToList()
                        group x by x.FirstName into cat
                        select new
                        {
                            StudentName = cat.Key,
                            Num = cat.Count()
                        };

            foreach (var item in group)
            {
                Result numFoods = new Result();
                numFoods.State = item.StudentName;
                numFoods.freq = item.Num;
                StudentsGraph.Add(numFoods);
            }

            return Json(StudentsGraph);
        }

    }


   


}





