using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeachMe.Models
{
    public class StudentsAndCourses
    {
       
        public int ID { get; set; }

        public Student Student { get; set; }

        public Course Course { get; set; }
    }
}
