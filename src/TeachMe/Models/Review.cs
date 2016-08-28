using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TeachMe.Models
{

    public enum Rate
    {
        [Display(Name = "1 Star")]
        OneStar = 1,

        [Display(Name = "2 Stars")]
        TwoStars = 2,

        [Display(Name = "3 Stars")]
        ThreeStars = 3,

        [Display(Name = "4 Stars")]
        FourStars = 4,

        [Display(Name = "5 Stars")]
        FiveStars = 5,
    }
    public class Review
    {
        [Key]
        public int ReviewID { get; set; }
        [ForeignKey("Students")]
        public int StudentID { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters."), RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Please enter a valid name - only letters")]
        public string Name { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Review cannot be longer than 50 characters."), RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Please enter a valid review - only letters")]
        [Display(Name ="Your Review")]
        public string Revieww { get; set; }

        [Required]
        public Rate rate { get; set; }

        public DateTime Date { get; set; }

        public virtual Student Student { get; set; }
    }
}
