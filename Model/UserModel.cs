using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EF_DietaNoDietaApi.Model
{

    
    public class UserModel
    {
        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Please enter a valid email address")]
        [Key]        
        public String email { get; set; }

        [Required(ErrorMessage = "Full Name is required")]        
        public String fullName { get; set; }

        [Required(ErrorMessage = "Phone Number is required")]
        public String phoneNumber { get; set; }

        
        public String isVeified { get; set; }

        public String password { get; set; }

        [Required(ErrorMessage = "User Role is required")]
        public String UserRole { get; set; }

        [Required(ErrorMessage = "Fitness level is required")]
        public String fitnessLevel { get; set; }

        [Required(ErrorMessage = "Date is required")]
        public String date { get; set; }
        
        public String address { get; set; }
    }
}
