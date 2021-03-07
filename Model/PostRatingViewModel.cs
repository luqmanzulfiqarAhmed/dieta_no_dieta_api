using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EF_DietaNoDietaApi.Model
{
    public class PostRatingViewModel
    {
        [Required(ErrorMessage = "Nutrtionist Email is required")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Please enter a valid email address")]
        public String NutrtionistEmail { get; set; }

        [Required(ErrorMessage = "User Email is required")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Please enter a valid email address")]
        public String UserEmail { get; set; }


        [Required(ErrorMessage = "Stars Field is required")]
        public String Stars { get; set; }

    }
}
