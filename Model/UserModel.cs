using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DietaNoDietaApi.Model
{
    public class UserModel
    {
        [Required(ErrorMessage = "Email is required")]
        public String email { get; set; }


        [Required(ErrorMessage = "Phone Number is required")]
        public String phoneNumber { get; set; }

        
        public bool isVeified { get; set; }

        public String password { get; set; }

        [Required(ErrorMessage = "User Role is required")]
        public String UserRole { get; set; }

        [Required(ErrorMessage = "Fitness level is required")]
        public String fitnessLevel { get; set; }

    }
}
