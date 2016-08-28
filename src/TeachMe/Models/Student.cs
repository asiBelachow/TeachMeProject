using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TeachMe.Models
{

    public enum deg
    {
        Bachelor = 1,
        Master = 2,
        Doctor = 3,
    }

    //For-Students Year
    public enum Year
    {
        A = 1,
        B = 2,
        C = 3,
        D = 4,
        E = 5
    }
    public class Student
    {
        [Key]
        public int StudentID { get; set; }



        [Required]
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters."), RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Please enter a valid name - only letters")]
        [Display(Description = "First Name")]
        public string FirstName { get; set; }


        [Required]
        [StringLength(50, ErrorMessage = "Last name cannot be longer than 50 characters."), RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Please enter a valid name - only letters")]
        [Display(Description = "Last Name")]
        public string LastName { get; set; }


        [Required]
        [Display(Name = "Year")]
        public Year year { get; set; }


        [Required]
        [Display(Name = "Degree")]
        public deg degree { get; set; }


        [Required]
        [StringLength(50, ErrorMessage = "Address cannot be longer than 50 characters.")]
        public string Address { get; set; }


        [Display(Name = "Phone Number")]
        [Required]
        [RegularExpression(@"\d{9,10}", ErrorMessage = "please enter a valid phone number - 9 or 10 digits")]
        public  string PhoneNumber { get; set; }


        [Required(ErrorMessage = "Jubot Per Hour is required")]
        [Display(Name = "Money Per Hour")]
        [Range(1, 300, ErrorMessage = "Jubot can only be between 1-300")]
        public int JubotPerHour { get; set; }

        [Display(Name = "Money")]
        [Range(0, 10000000000)]
        public int Jubot { get; set; }

       
        public ICollection<StudentsAndCourses> StudentsCourses { get; set; }
        [Display(Name = "Student Review")]
        public ICollection<Review> StudentReview { get; set; }

   
    }
}
