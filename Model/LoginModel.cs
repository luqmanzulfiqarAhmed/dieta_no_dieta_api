using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EF_DietaNoDietaApi.Model
{
    public class LoginModel
    {

        [Required(ErrorMessage = "Email is required")]
        public String email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public String password { get; set; }
    }
}
