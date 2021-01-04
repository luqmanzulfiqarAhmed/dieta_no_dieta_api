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
        [Required(ErrorMessage = "Food Name is required")]
        public String foodName { get; set; }

        [Required(ErrorMessage = "Food Time is required")]
        public String foodTime { get; set; }

        [Required(ErrorMessage = "Food Description is required")]
        public List<FoodDescriptionModel> foodDescriptionModels { get; set; }
    }
}
