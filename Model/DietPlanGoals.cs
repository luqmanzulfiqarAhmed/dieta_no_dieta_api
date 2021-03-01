using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EF_DietaNoDietaApi.Model
{
    public class DietPlanGoals
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid GoalId { get; set; }

        [Required(ErrorMessage = "Nutrtionist Email is required")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Please enter a valid email address")]        
        public String NutrtionistEmail { get; set; }
        [Required(ErrorMessage = "User Email is required")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Please enter a valid email address")]        
        public String UserEmail { get; set; }

        [Required(ErrorMessage = "Name is required")]        
        public String Name { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public String Description { get; set; }
        [Required(ErrorMessage = "Starting Date is required")]
        public String StartingDate { get; set; }

        [Required(ErrorMessage = "Ending Date is required")]
        public String EndingDate { get; set; }
        [Required(ErrorMessage = "Expected Result is required")]
        public String ExpectedResult { get; set; }

        [Required(ErrorMessage = "Goal Type is required")]
        public String GoalType { get; set; }

        public String isCompleted { get; set; }



    }
}
