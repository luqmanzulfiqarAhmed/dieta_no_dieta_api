﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

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

        [Required(ErrorMessage = "Neutrtionist Email is required")]
        public String neutrtionistEmail { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime date { get; set; }

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
        public List<FoodItemsModel> foodItemsModels { get; set; }

        public static explicit operator DietPlanModel(DietPlanViewModel v)
        {
            DietPlanModel model = new DietPlanModel();
            model.userEmail = v.userEmail;
            model.neutrtionistEmail = v.neutrtionistEmail;
            model.isWishlist = v.isWishlist;
            model.missionName = v.missionName;
            model.foodTime = v.foodTime;
            model.planType = v.planType;
            model.sleepHours = v.sleepHours;
            model.sleepQuality = v.sleepQuality;
            model.thieSize = v.thieSize;
            model.weight = v.weight;
            model.water = v.water;
            //  model.date = v.date;
            model.date = DateTime.Today.Date;
            model.dietPlanName = v.dietPlanName;
            List<FoodItemsModel> foodItemsModels = new List<FoodItemsModel>();

            foreach (FoodItemViewModel food in v.foodItemsModels)
            {
                string[] arr = food.food_description.Split(" ");
                FoodItemsModel mode = new FoodItemsModel();
                mode.food_id = food.food_id;
                mode.food_name = food.food_name;
                mode.food_type = food.food_type;
                mode.food_url = food.food_url;

                mode.foodQuantity = arr[1].ToString();
                var match = Regex.Match(mode.foodQuantity, @"([-+]?[0-9]*\.?[0-9]+)");
                if (match.Success)
                    mode.foodQuantity = Convert.ToString(match.Groups[1].Value);

                mode.foodCalories = arr[4].ToString();
                match = Regex.Match(mode.foodCalories, @"([-+]?[0-9]*\.?[0-9]+)");
                if (match.Success)
                    mode.foodCalories = Convert.ToString(match.Groups[1].Value);

                match = Regex.Match(mode.foodQuantity, @"([-+]?[0-9]*\.?[0-9]+)");
                if (match.Success)
                    mode.foodQuantity = Convert.ToString(match.Groups[1].Value);

                mode.foodFat = arr[7].ToString();
                match = Regex.Match(mode.foodFat, @"([-+]?[0-9]*\.?[0-9]+)");
                if (match.Success)
                    mode.foodFat = Convert.ToString(match.Groups[1].Value);

                mode.foodCarbs = arr[10].ToString();
                match = Regex.Match(mode.foodCarbs, @"([-+]?[0-9]*\.?[0-9]+)");
                if (match.Success)
                    mode.foodCarbs = Convert.ToString(match.Groups[1].Value);

                mode.foodProtein = arr[13].ToString();
                match = Regex.Match(mode.foodProtein, @"([-+]?[0-9]*\.?[0-9]+)");
                if (match.Success)
                    mode.foodProtein = Convert.ToString(match.Groups[1].Value);

                foodItemsModels.Add(mode);
            }
            model.foodItemsModels = foodItemsModels;

            return model;
        }
    }

}
