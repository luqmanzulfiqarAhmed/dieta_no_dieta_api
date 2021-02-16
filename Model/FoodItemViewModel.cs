using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EF_DietaNoDietaApi.Model
{
    public class FoodItemViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid foodItemId { get; set; }

        [Required(ErrorMessage = "Food Description is required")]
        public String food_description { get; set; }
        [Required(ErrorMessage = "Food Id is required")]
        public String food_id { get; set; }
        [Required(ErrorMessage = "Food Name is required")]
        public String food_name { get; set; }

        [Required(ErrorMessage = "Food Type is required")]
        public String food_type { get; set; }

        [Required(ErrorMessage = "Food URL is required")]
        public String food_url { get; set; }
    }
}
