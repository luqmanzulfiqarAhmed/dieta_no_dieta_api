using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EF_DietaNoDietaApi.Model
{
    public class FoodItemsModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid foodItemId { get; set; }

        public String food_id { get; set; }
        [Required(ErrorMessage = "Food Name is required")]
        public String food_name { get; set; }

        [Required(ErrorMessage = "Food Type is required")]
        public String food_type { get; set; }

        [Required(ErrorMessage = "Food URL is required")]
        public String food_url { get; set; }

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
