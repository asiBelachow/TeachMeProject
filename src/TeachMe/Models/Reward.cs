using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TeachMe.Models
{
    public class Reward
    {
        [Key]
        public int RewardID { get; set; }


        [Required]

        public string Title { get; set; }


        [Required]
        public string Description { get; set; }


        [Display(Name = "Jubot")]
        [Required]
        [Range(1, 1000000)]
        public int NumOfJubot { get; set; }

        public string Video { get; set; }

        public string Picture { get; set; }
    }
}
