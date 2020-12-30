using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EF_DietaNoDietaApi.Model
{
    public class DietPlanModel
    {        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid dietPlanId { get; set; }

        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Please enter a valid email address")]        
        public String userEmail { get; set; }
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Please enter a valid email address")]
        
        [Required(ErrorMessage = "Trainer Email is required")]
        public String trainerEmail { get; set; }
        public String date { get; set; }

        [Required(ErrorMessage = "Weight is required")]
        public String weight { get; set; }

        [Required(ErrorMessage = "Water is required")]
        public String water { get; set; }

        [Required(ErrorMessage = "Thie Size is required")]
        public String thieSize { get; set; }

        [Required(ErrorMessage = "vaste is required")]
        public String vaste { get; set; }

        [Required(ErrorMessage = "Sleep Hours are required")]
        public String sleepHours { get; set; }

        [Required(ErrorMessage = "Sleep Quality is required")]
        public String sleepQuality { get; set; }

        [Required(ErrorMessage = "Diet Plan Name is required")]
        public String dietPlanName { get; set; }
        [Required(ErrorMessage = "Plan Type is required")]
        public String planType { get; set; }
        [Required(ErrorMessage = "Food items are required")]
        public List<FoodItemsModel> foodItemsModels { get; set; }

        [Required(ErrorMessage = "You must specify isWishlist true or false")]
        public String isWishlist = "false";
    }
}
