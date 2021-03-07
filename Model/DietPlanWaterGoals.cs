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
        public Guid GoalId { get; set; }

        [Required(ErrorMessage = "User Email is required")]
        public String UserEmail { get; set; }

        [Required(ErrorMessage = "User Email is required")]
        public String NeutrtionistEmail { get; set; }

        [Required(ErrorMessage = "Water in liters is required")]
        public String WaterInLtrs { get; set; }

        [Required(ErrorMessage = "Start date is required")]
        public String StartDate { get; set; }
        [Required(ErrorMessage = "End date is required")]
        public String EndDate { get; set; }

        public String isCompleted { get; set; }
        public float TaretGlasses { get; set; }

        public float PerGlassValue { get; set; }

        public float AcheivedValue { get; set; }
    }
}
