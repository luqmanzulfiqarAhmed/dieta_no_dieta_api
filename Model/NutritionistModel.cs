using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EF_DietaNoDietaApi.Model
{
    public class NutritionistModel
    {
        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Please enter a valid email address")]
        [Key]
        public String email { get; set; }
        [Required(ErrorMessage = "Age is required")]
        public String age { get; set; }
        [Required(ErrorMessage = "Experience is required")]
        public String experience { get; set; }
        [Required(ErrorMessage = "Gender is required")]
        public String gender { get; set; }
        [Required(ErrorMessage = "Full Name is required")]
        public String fullName { get; set; }
        [Required(ErrorMessage = "Phone Number is required")]
        public String phoneNumber { get; set; }


        public List<RatingHistory> RatingHistories { get; set; }
        public float TotalRatings { get; set; }
        public float TotalStars { get; set; }
        public float AverageStars { get; set; }
    }
}
