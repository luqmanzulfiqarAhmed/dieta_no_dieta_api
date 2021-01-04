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


        [Required(ErrorMessage = "Phone Number is required")]
        public String phoneNumber { get; set; }

        public String isVeified { get; set; }

        public String UserRole { get; set; }

        [Required(ErrorMessage = "Fitness level is required")]
        public String fitnessLevel { get; set; }

        public String date { get; set; }
        //profile

        public String fullName { get; set; }

        public String age { get; set; }
        public String height { get; set; }

        public String currentWeight { get; set; }
        public String objectiveWeight { get; set; }

        public String currentVaste { get; set; }
        public String objectiveVaste { get; set; }

        public String currentBiseps { get; set; }
        public String objectiveBiseps { get; set; }

        public String currentHips { get; set; }
        public String objectiveHips { get; set; }

        public String currentThai { get; set; }
        public String objectiveThai { get; set; }
        public String gender { get; set; }        
        public String imc { get; set; }


    }
}