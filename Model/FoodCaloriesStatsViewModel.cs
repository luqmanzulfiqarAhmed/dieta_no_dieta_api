using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EF_DietaNoDietaApi.Model
{
    public class FoodCaloriesStatsViewModel
    {
        public double BreakfastCaloriesInPercent { get; set; }
        public double BreakfastCaloriesInNumber { get; set; }
        public double LunchCaloriesInPercent { get; set; }
        public double LunchCaloriesInNumber { get; set; }
        public double DinnerCaloriesInPercent { get; set; }
        public double DinnerCaloriesInNumber { get; set; }
        public double SnacksCaloriesInPercent { get; set; }
        public double SnacksCaloriesInNumber { get; set; }
        public List<FoodItemsCalories> FoodItemsCalories { get; set; }
    }
}
