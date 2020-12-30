using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EF_DietaNoDietaApi.Model
{
    public class FoodDescriptionModel
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid foodDescriptionId { get; set; }
        [Required(ErrorMessage = "Mission Name is required")]
        public String missionName { get; set; }
        [Required(ErrorMessage = "Food Quantity is required")]
        public String foodQuantity { get; set; }

        [Required(ErrorMessage = "Food Calories are required")]
        public String foodCalories { get; set; }
        [Required(ErrorMessage = "Food Fat is required")]
        public String foodFat { get; set; }

        [Required(ErrorMessage = "Food Carbs are required")]
        public String foodCarbs { get; set; }
        [Required(ErrorMessage = "Food Protein is required")]
        public String foodProtein { get; set; }
    }
}
