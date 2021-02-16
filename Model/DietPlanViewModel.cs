
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EF_DietaNoDietaApi.Model
{
    public class DietPlanViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid dietPlanId { get; set; }

        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Please enter a valid email address")]
        public String userEmail { get; set; }
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Please enter a valid email address")]

        [Required(ErrorMessage = "Neutrtionist Email is required")]
        public String neutrtionistEmail { get; set; }
        public String date { get; set; }
        public String weight { get; set; }
        public String water { get; set; }
        public String thieSize { get; set; }
        public String sleepHours { get; set; }
        public String sleepQuality { get; set; }

        [Required(ErrorMessage = "Diet Plan Name is required")]
        public String dietPlanName { get; set; }
        [Required(ErrorMessage = "Plan Type is required")]
        public String planType { get; set; }

        [Required(ErrorMessage = "Food Time is required")]
        public String foodTime { get; set; }

        [Required(ErrorMessage = "Mission Name is required")]
        public String missionName { get; set; }

        [Required(ErrorMessage = "You must specify isWishlist true or false")]
        public String isWishlist { get; set; }
        [Required(ErrorMessage = "Food items are required")]
        public List<FoodItemViewModel> foodItemsModels { get; set; }

    }

}
