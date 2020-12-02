using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EF_DietaNoDietaApi.Model
{
    public class RegisterModel
    {
       // [Required (ErrorMessage ="User Name is required")]
        //public String userName { get; set; }
       // public String firstName { get; set; }
        [Required(ErrorMessage = "Email is required")]
        public String email { get; set; }


        [Required(ErrorMessage = "Phone Number is required")]
        public String phoneNumber { get; set; }
      
        //  [Required(ErrorMessage = "Password is required")]
        public String password { get; set; }

    }
}
