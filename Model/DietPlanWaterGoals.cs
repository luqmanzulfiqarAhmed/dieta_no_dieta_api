using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EF_DietaNoDietaApi.Model
{
    public class DietPlanWaterGoals
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid WaterGoalId { get; set; }

        [Required(ErrorMessage = "Nutrtionist Email is required")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Please enter a valid email address")]        
        public String NutrtionistEmail { get; set; }
       
        [Required(ErrorMessage = "User Email is required")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Please enter a valid email address")]        
        public String UserEmail { get; set; }

        [Required(ErrorMessage = "Target glass is required")]
        public String TargetGlass { get; set; }
        
        public String DrunkGlass { get; set; }

        public String isCompleted { get; set; }



    }
}
