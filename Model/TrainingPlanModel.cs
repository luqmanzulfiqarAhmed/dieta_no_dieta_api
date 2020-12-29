using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EF_DietaNoDietaApi.Model
{
    public class TrainingPlanModel
    {        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid trainingPlanId { get; set; }

        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Please enter a valid email address")]
        [Required(ErrorMessage = "User Email is required")]
        public String userEmail { get; set; }
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Please enter a valid email address")]
        
        [Required(ErrorMessage = "Trainer Email is required")]
        public String trainerEmail { get; set; }
        [Required(ErrorMessage = "Duration is required")]
        public String duration { get; set; }        
        public String date { get; set; }
        [Required(ErrorMessage = "Number of hours is required")]
        public String numOfHrs { get; set; }

        [Required(ErrorMessage = "Exercise is required")]
        public List<ExerciseModel> exercises { get; set; }
    }
}
