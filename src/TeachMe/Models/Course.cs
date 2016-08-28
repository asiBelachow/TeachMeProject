using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TeachMe.Models
{
    public class Course
    {
        [Key]
        public int CourseID { get; set; }


        [Required]
        [StringLength(50, ErrorMessage = "Course name cannot be longer than 50 characters.")]
        [Display(Name="Course Name")]
        public string Title { get; set; }


        public ICollection<StudentsAndCourses> StudentsCourses { get; set; }



    }
}
