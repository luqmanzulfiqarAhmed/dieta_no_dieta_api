using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EF_DietaNoDietaApi.Model;
using EF_DietaNoDietaApi.MySql;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EF_DietaNoDietaApi.Controllers
{
    [Route("api/DietPlan/")]
    [ApiController]
    public class DietPlanController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly MySqlDbContext dbContext;

        public DietPlanController(MySqlDbContext context, IConfiguration configuration)
        {

            dbContext = context;
            _config = configuration;
        }
        [HttpGet]
        [Route("getDietPlan/forUser")]
        public async Task<IActionResult> getDietPlan([FromQuery] String neutrtionistEmail, String userEmail, String foodTime)
        {
            try
            {
                IQueryable<Model.DietPlanModel> result = null;
                result = dbContext.DietPlans.Where(p => p.userEmail == userEmail && p.neutrtionistEmail == neutrtionistEmail && p.foodTime == foodTime);

                return StatusCode(StatusCodes.Status200OK, result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
            }
        }

        [HttpGet]
        [Route("getDietPlan/forUser/ById")]
        public async Task<IActionResult> getDietPlanForUserById([FromQuery] Guid id)
        {
            try
            {

                IQueryable<Model.DietPlanModel> result = null;
                result = dbContext.DietPlans.Where(p => p.isWishlist == "false" && p.dietPlanId == id).Include(e => e.foodItemsModels);

                return StatusCode(StatusCodes.Status200OK, result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
            }
        }

        [Route("getDietPlan/forUser/FoodDetail/ById")]
        public async Task<IActionResult> getDietPlanForUserFoodById([FromQuery] Guid id)
        {
            try
            {

                IQueryable<Model.FoodItemsModel> result = null;
                result = dbContext.foodItems.Where(p => p.foodItemId == id);

                return StatusCode(StatusCodes.Status200OK, result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
            }
        }

        [HttpGet]
        [Route("getDietPlan/Wishlist")]
        public async Task<IActionResult> getDietPlanWishList([FromQuery] String neutrtionistEmail, [FromQuery] String foodTime)
        {
            try
            {
                IQueryable<Model.DietPlanModel> result = null;
                result = dbContext.DietPlans.Where(p => p.isWishlist == "true" && p.neutrtionistEmail == neutrtionistEmail && p.foodTime == foodTime);

                return StatusCode(StatusCodes.Status200OK, result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
            }
        }
        [HttpGet]
        [Route("getDietPlan/Wishlist/ById")]
        public async Task<IActionResult> getDietPlanWishListById([FromQuery] Guid id)
        {
            try
            {

                IQueryable<Model.DietPlanModel> result = null;
                result = dbContext.DietPlans.Where(p => p.isWishlist == "true" && p.dietPlanId == id).Include(e => e.foodItemsModels);

                return StatusCode(StatusCodes.Status200OK, result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
            }
        }
        [HttpGet]
        [Route("getDietPlan/Wishlist/FoodDetail/ById")]
        public async Task<IActionResult> getDietPlanWishListFoddById([FromQuery] Guid id)
        {
            try
            {

                IQueryable<Model.FoodItemsModel> result = null;
                result = dbContext.foodItems.Where(p => p.foodItemId == id);

                return StatusCode(StatusCodes.Status200OK, result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
            }
        }
        [HttpPost]
        [Route("addDietPlan")]//http://localhost:5000/api/Authenticate/Register
        public async Task<IActionResult> addPlan([FromBody] DietPlanViewModel dietPlanViewModel)
        {
            //var found =  dbContext.Users.First(x=> x.email == user.email);            
            DateTime today = DateTime.Today;
            dietPlanViewModel.date = today.ToString("dd/MM/yyyy");
            //dietPlanViewModel.date = today;
            DietPlanModel dietPlanModel = new DietPlanModel();
            dietPlanModel = (DietPlanModel)dietPlanViewModel;
            //dietPlanModel.date = today;
            //dietPlanViewModel.foodItemsModels;


            await using var transaction = await dbContext.Database.BeginTransactionAsync();
            await dbContext.DietPlans.AddAsync(dietPlanModel);
            int num1 = await dbContext.SaveChangesAsync();
            await transaction.CommitAsync();
            if (num1 != 0)
                return Ok(new Response { Status = "200", Message = "Diet Plan added Successfully" });
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "500", Message = "Plan could not insert" });
            }

        }



        [HttpDelete]
        [Route("DropDietPlan")]
        public async Task<IActionResult> drop()
        {
            dbContext.foodItems.RemoveRange(dbContext.foodItems);
            dbContext.SaveChanges();
            dbContext.DietPlans.RemoveRange(dbContext.DietPlans);
            dbContext.SaveChanges();
            return StatusCode(StatusCodes.Status200OK, "droped");
        }
        [HttpGet]
        [Route("getDietPlan/forUser/StatsDetailed")]
        public async Task<IActionResult> getDietPlanStats2([FromQuery] String userEmail)
        {
            var startDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day).AddDays(-7);
            var endDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);

            var result = dbContext.DietPlans
                .Where(x => x.userEmail.Equals(userEmail) && (x.date.Day >= startDate.Day && x.date.Day <= endDate.Day))
                .Include(y => y.foodItemsModels)
                .ToArrayAsync();
            DietPlanModel[] plans = result.Result;
            int size = 0;
            for (int i = 0; i < plans.Length; i++)
            {
                size += plans[i].foodItemsModels.Count();
            }
            return StatusCode(StatusCodes.Status200OK, size);
        }

        [HttpGet]
        [Route("getDietPlan/forUser/Stats")]
        public async Task<IActionResult> getDietPlanStats([FromQuery] String userEmail)
        {

            try
            {


                DateTime fromDate = DateTime.Today;
                var startDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day).AddDays(-7);
                var endDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);

                List<StatsViewModel> statsViewModels = new List<StatsViewModel>();



                var result = dbContext.DietPlans
                .Where(x => x.userEmail.Equals(userEmail) && (x.date.Day >= startDate.Day && x.date.Day <= endDate.Day))
                .Include(y => y.foodItemsModels)
                .ToArrayAsync();

                DietPlanModel[] model2 = result.Result;
                for (int i = 0; i < model2.Count(); i++)
                {
                    StatsViewModel stats = new StatsViewModel();
                    stats.count = i;
                    for (int j = 0; j < model2[i].foodItemsModels.Count(); j++)
                    {
                        float quantity = float.Parse(model2[i].foodItemsModels[j].foodQuantity);
                        stats.Quantity += quantity;

                        //float calories = float.Parse(model2[i].foodItemsModels[j].foodCalories);
                        //stats.Calories+= calories;

                        float protien = float.Parse(model2[i].foodItemsModels[j].foodProtein);
                        stats.Protein += protien;

                        float carbs = float.Parse(model2[i].foodItemsModels[j].foodCarbs);
                        stats.Carbs += carbs;

                        float fat = float.Parse(model2[i].foodItemsModels[j].foodFat);
                        stats.Fat += fat;


                    }
                    statsViewModels.Add(stats);
                }
                var send = statsViewModels;
                return StatusCode(StatusCodes.Status200OK, send);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex });
            }
        }

        [HttpGet]
        [Route("getDietPlan/forUser/Stats/Detailed/Calories/Percent/Days")]
        public async Task<IActionResult> getDietPlanStatsDayCalories([FromQuery] String userEmail)
        {
            try
            {
                var startDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day)
                .AddDays(-7);
                var endDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);

                DietPlanModel[] plans = await dbContext.DietPlans
                    .Where(x => x.userEmail.Equals(userEmail)
                    && (x.date.Day >= startDate.Day && x.date.Day <= endDate.Day))
                    .Include(y => y.foodItemsModels)
                    .ToArrayAsync();

                double BreakfastCaloriesInNumber = 0, LunchCaloriesInNumber = 0,
                    DinnerCaloriesInNumber = 0, SnacksCaloriesInNumber = 0;

                double totalBreakfastCal = 0, totalLunchCal = 0, totalSnacksCal = 0, totalDinnerCal = 0;

                double allCollories = 0;

                FoodCaloriesStatsViewModel foodCaloriesStats = new FoodCaloriesStatsViewModel();
                foodCaloriesStats.FoodItemsCalories = new List<FoodItemsCalories>();
                float day = 0;
                for (int i = 0; i < plans.Length; i++)
                {
                    int totalColories = 0;

                    if (plans[i].foodTime.Equals("Morning"))
                    {
                        for (int j = 0; j < plans[i].foodItemsModels.Count(); j++)
                        {
                            BreakfastCaloriesInNumber += int.Parse(plans[i].foodItemsModels[j].foodCalories);
                            totalColories += int.Parse(plans[i].foodItemsModels[j].foodCalories);
                            totalBreakfastCal += float.Parse(plans[i].foodItemsModels[j].foodCalories);
                            totalBreakfastCal += float.Parse(plans[i].foodItemsModels[j].foodCarbs);
                            totalBreakfastCal += float.Parse(plans[i].foodItemsModels[j].foodFat);
                            totalBreakfastCal += float.Parse(plans[i].foodItemsModels[j].foodProtein);

                        }
                    }
                    if (plans[i].foodTime.Equals("Lunch"))
                    {
                        for (int j = 0; j < plans[i].foodItemsModels.Count(); j++)
                        {
                            LunchCaloriesInNumber += int.Parse(plans[i].foodItemsModels[j].foodCalories);
                            totalColories += int.Parse(plans[i].foodItemsModels[j].foodCalories);
                            totalLunchCal += float.Parse(plans[i].foodItemsModels[j].foodCalories);
                            totalLunchCal += float.Parse(plans[i].foodItemsModels[j].foodCarbs);
                            totalLunchCal += float.Parse(plans[i].foodItemsModels[j].foodFat);
                            totalLunchCal += float.Parse(plans[i].foodItemsModels[j].foodProtein);
                        }
                    }
                    if (plans[i].foodTime.Equals("Snacks"))
                    {
                        for (int j = 0; j < plans[i].foodItemsModels.Count(); j++)
                        {
                            SnacksCaloriesInNumber += int.Parse(plans[i].foodItemsModels[j].foodCalories);
                            totalColories += int.Parse(plans[i].foodItemsModels[j].foodCalories);
                            totalSnacksCal += float.Parse(plans[i].foodItemsModels[j].foodCalories);
                            totalSnacksCal += float.Parse(plans[i].foodItemsModels[j].foodCarbs);
                            totalSnacksCal += float.Parse(plans[i].foodItemsModels[j].foodFat);
                            totalSnacksCal += float.Parse(plans[i].foodItemsModels[j].foodProtein);
                        }
                    }
                    if (plans[i].foodTime.Equals("Dinner"))
                    {
                        for (int j = 0; j < plans[i].foodItemsModels.Count(); j++)
                        {
                            DinnerCaloriesInNumber += int.Parse(plans[i].foodItemsModels[j].foodCalories);
                            totalColories += int.Parse(plans[i].foodItemsModels[j].foodCalories);
                            totalDinnerCal += float.Parse(plans[i].foodItemsModels[j].foodCalories);
                            totalDinnerCal += float.Parse(plans[i].foodItemsModels[j].foodCarbs);
                            totalDinnerCal += float.Parse(plans[i].foodItemsModels[j].foodFat);
                            totalDinnerCal += float.Parse(plans[i].foodItemsModels[j].foodProtein);
                        }
                    }
                    if (i != 0)
                    {
                        if (plans[i].date != plans[i - 1].date)
                        {
                            FoodItemsCalories itemsCalories = new FoodItemsCalories();
                            itemsCalories.Day = i;
                            itemsCalories.Calories = totalColories;
                            foodCaloriesStats.FoodItemsCalories.Add(itemsCalories);

                        }
                    }
                    else
                    {
                        FoodItemsCalories itemsCalories = new FoodItemsCalories();
                        itemsCalories.Day = i;
                        itemsCalories.Calories = totalColories;
                        foodCaloriesStats.FoodItemsCalories.Add(itemsCalories);
                    }
                    allCollories += totalColories;
                }

                foodCaloriesStats.BreakfastCaloriesInNumber = BreakfastCaloriesInNumber;
                foodCaloriesStats.LunchCaloriesInNumber = LunchCaloriesInNumber;
                foodCaloriesStats.SnacksCaloriesInNumber = SnacksCaloriesInNumber;
                foodCaloriesStats.DinnerCaloriesInNumber = DinnerCaloriesInNumber;

                if (totalBreakfastCal != 0)
                {
                    double result = (BreakfastCaloriesInNumber / allCollories) * 100;
                    foodCaloriesStats.BreakfastCaloriesInPercent = Math.Round(result);
                }
                if (totalLunchCal != 0)
                {
                    double result = (LunchCaloriesInNumber / allCollories) * 100;
                    foodCaloriesStats.LunchCaloriesInPercent = Math.Round(result);
                }
                if (totalSnacksCal != 0)
                {
                    double result = (SnacksCaloriesInNumber / allCollories) * 100;
                    foodCaloriesStats.SnacksCaloriesInPercent = Math.Round(result);
                }
                if (totalDinnerCal != 0)
                {
                    double result = (SnacksCaloriesInNumber / allCollories) * 100;
                    foodCaloriesStats.SnacksCaloriesInPercent = Math.Round(result);
                }

                return StatusCode(StatusCodes.Status200OK, foodCaloriesStats);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpGet]
        [Route("getDietPlan/forUser/Stats/Calories/Detailed/Days")]
        public async Task<IActionResult> getDietPlanStatsDay([FromQuery] String userEmail)
        {
            int index = 0;
            try
            {
                var startDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day)
                .AddDays(-7);
                var endDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);

                DietPlanModel[] plans = await dbContext.DietPlans
                    .Where(x => x.userEmail.Equals(userEmail)
                    && (x.date >= startDate && x.date <= endDate))
                    .Include(y => y.foodItemsModels)
                    .ToArrayAsync();

                int size = 0;
                for (int i = 0; i < plans.Length; i++)
                {
                    size += plans[i].foodItemsModels.Count();
                }
                FoodNameStatsViewModel[] stats = new FoodNameStatsViewModel[size];
                for (int i = 0; i < stats.Length; i++)
                {
                    stats[i] = new FoodNameStatsViewModel();
                }
                for (int i = 0; i < plans.Length; i++)
                {

                    for (int j = 0; j < plans[i].foodItemsModels.Count(); j++)
                    {
                        //index = int.Parse() % size;
                        string foodName = plans[i].foodItemsModels[j].food_name;
                        var sum = foodName.Select(part => Convert.ToInt32(part)).Sum();
                        index = int.Parse(sum.ToString()) % size;
                        if (stats[index].NumberOfTimesEaten != 0)
                        {
                            ++stats[index].NumberOfTimesEaten;
                            stats[index].Calories += float.Parse(plans[i].foodItemsModels[j].foodCalories);
                        }
                        else
                        {
                            stats[index].foodName = plans[i].foodItemsModels[j].food_name;
                            stats[index].NumberOfTimesEaten = 1;
                            stats[index].Calories = float.Parse(plans[i].foodItemsModels[j].foodCalories);
                        }
                    }
                }
                List<FoodNameStatsViewModel> FoodNameStatsViewModel = new List<FoodNameStatsViewModel>();
                for (int i = 0; i < stats.Length; i++)
                {
                    if (stats[i].foodName != null)
                        FoodNameStatsViewModel.Add(stats[i]);
                }
                return StatusCode(StatusCodes.Status200OK, FoodNameStatsViewModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, index);
            }
        }

        [HttpGet]
        [Route("getDietPlan/forUser/Stats/Macros/Detailed/Days")]
        public async Task<IActionResult> getDietPlanStatsMacrosDay([FromQuery] String userEmail)
        {
            int index = 0;
            try
            {
                var startDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day)
                .AddDays(-7);
                var endDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);

                DietPlanModel[] plans = await dbContext.DietPlans
                    .Where(x => x.userEmail.Equals(userEmail)
                    && (x.date >= startDate && x.date <= endDate))
                    .Include(y => y.foodItemsModels)
                    .ToArrayAsync();

                int size = 0;
                for (int i = 0; i < plans.Length; i++)
                {
                    size += plans[i].foodItemsModels.Count();
                }
                FoodNameMacrosStats[] stats = new FoodNameMacrosStats[size];
                for (int i = 0; i < stats.Length; i++)
                {
                    stats[i] = new FoodNameMacrosStats();
                }
                for (int i = 0; i < plans.Length; i++)
                {

                    for (int j = 0; j < plans[i].foodItemsModels.Count(); j++)
                    {
                        //index = int.Parse() % size;
                        string foodName = plans[i].foodItemsModels[j].food_name;
                        var sum = foodName.Select(part => Convert.ToInt32(part)).Sum();
                        index = int.Parse(sum.ToString()) % size;
                        if (stats[index].NumberOfTimesEaten != 0)
                        {
                            ++stats[index].NumberOfTimesEaten;
                            stats[index].Protein += float.Parse(plans[i].foodItemsModels[j].foodProtein);
                            stats[index].Carbs += float.Parse(plans[i].foodItemsModels[j].foodCarbs);
                            stats[index].Fat += float.Parse(plans[i].foodItemsModels[j].foodCalories);
                        }
                        else
                        {
                            stats[index].foodName = plans[i].foodItemsModels[j].food_name;
                            stats[index].NumberOfTimesEaten = 1;
                            stats[index].Protein += float.Parse(plans[i].foodItemsModels[j].foodProtein);
                            stats[index].Carbs += float.Parse(plans[i].foodItemsModels[j].foodCarbs);
                            stats[index].Fat += float.Parse(plans[i].foodItemsModels[j].foodCalories);
                        }
                    }
                }
                List<FoodNameMacrosStats> FoodNameStatsViewModel = new List<FoodNameMacrosStats>();
                for (int i = 0; i < stats.Length; i++)
                {
                    if (stats[i].foodName != null)
                        FoodNameStatsViewModel.Add(stats[i]);
                }
                return StatusCode(StatusCodes.Status200OK, FoodNameStatsViewModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("getDietPlan/forUser/Stats/Detailed/Macros/Percent/Days")]
        public async Task<IActionResult> getDietPlanStatsDayMacros([FromQuery] String userEmail)
        {
            try
            {
                var startDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day)
                .AddDays(-7);
                var endDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);

                DietPlanModel[] plans = await dbContext.DietPlans
                    .Where(x => x.userEmail.Equals(userEmail)
                    && (x.date.Day >= startDate.Day && x.date.Day <= endDate.Day))
                    .Include(y => y.foodItemsModels)
                    .ToArrayAsync();

                double
                       BreakfastCaloriesInNumber = 0,
                       BreakfastProtienInNumber = 0,
                       BreakfastCarbsInNumber = 0,
                       BreakfastFatInNumber = 0,

                       LunchCaloriesInNumber = 0,
                       LunchProtienInNumber = 0,
                       LunchCarbsInNumber = 0,
                       LunchFatInNumber = 0,

                       DinnerCaloriesInNumber = 0,
                       DinnerProtienInNumber = 0,
                       DinnerCarbsInNumber = 0,
                       DinnerFatInNumber = 0,

                       SnacksCaloriesInNumber = 0,
                       SnacksProtienInNumber = 0,
                       SnacksCarbsInNumber = 0,
                       SnacksFatInNumber = 0;

                double totalBreakfastCal = 0, totalLunchCal = 0, totalSnacksCal = 0, totalDinnerCal = 0;

                int allCollories = 0;

                FoodMacrosStatsViewModel foodCaloriesStats = new FoodMacrosStatsViewModel();
                foodCaloriesStats.foodItemsMacros = new List<FoodItemsMacros>();
                for (int i = 0; i < plans.Length; i++)
                {
                    int totalColories = 0;
                    double totalCarbs = 0, totalProtien = 0, totalFat = 0;

                    if (plans[i].foodTime.Equals("Morning"))
                    {
                        for (int j = 0; j < plans[i].foodItemsModels.Count(); j++)
                        {
                            totalColories += int.Parse(plans[i].foodItemsModels[j].foodCalories);
                            totalProtien += float.Parse(plans[i].foodItemsModels[j].foodProtein);
                            totalCarbs += float.Parse(plans[i].foodItemsModels[j].foodCarbs);
                            totalFat += float.Parse(plans[i].foodItemsModels[j].foodFat);

                            BreakfastCaloriesInNumber += float.Parse(plans[i].foodItemsModels[j].foodCalories);
                            BreakfastCarbsInNumber += float.Parse(plans[i].foodItemsModels[j].foodCarbs);
                            BreakfastProtienInNumber += float.Parse(plans[i].foodItemsModels[j].foodProtein);
                            BreakfastFatInNumber += float.Parse(plans[i].foodItemsModels[j].foodFat);

                        }
                    }
                    if (plans[i].foodTime.Equals("Lunch"))
                    {
                        for (int j = 0; j < plans[i].foodItemsModels.Count(); j++)
                        {
                            totalColories += int.Parse(plans[i].foodItemsModels[j].foodCalories);
                            totalProtien += float.Parse(plans[i].foodItemsModels[j].foodProtein);
                            totalCarbs += float.Parse(plans[i].foodItemsModels[j].foodCarbs);
                            totalFat += float.Parse(plans[i].foodItemsModels[j].foodFat);

                            LunchCaloriesInNumber += float.Parse(plans[i].foodItemsModels[j].foodCalories);
                            LunchCarbsInNumber += float.Parse(plans[i].foodItemsModels[j].foodCarbs);
                            LunchFatInNumber += float.Parse(plans[i].foodItemsModels[j].foodFat);
                            LunchProtienInNumber += float.Parse(plans[i].foodItemsModels[j].foodProtein);
                        }
                    }
                    if (plans[i].foodTime.Equals("Snacks"))
                    {
                        for (int j = 0; j < plans[i].foodItemsModels.Count(); j++)
                        {
                            totalColories += int.Parse(plans[i].foodItemsModels[j].foodCalories);
                            totalProtien += float.Parse(plans[i].foodItemsModels[j].foodProtein);
                            totalCarbs += float.Parse(plans[i].foodItemsModels[j].foodCarbs);
                            totalFat += float.Parse(plans[i].foodItemsModels[j].foodFat);

                            SnacksCaloriesInNumber += float.Parse(plans[i].foodItemsModels[j].foodCalories);
                            SnacksCarbsInNumber += float.Parse(plans[i].foodItemsModels[j].foodCarbs);
                            SnacksFatInNumber += float.Parse(plans[i].foodItemsModels[j].foodFat);
                            SnacksProtienInNumber += float.Parse(plans[i].foodItemsModels[j].foodProtein);
                        }
                    }
                    if (plans[i].foodTime.Equals("Dinner"))
                    {
                        for (int j = 0; j < plans[i].foodItemsModels.Count(); j++)
                        {
                            totalColories += int.Parse(plans[i].foodItemsModels[j].foodCalories);
                            totalProtien += float.Parse(plans[i].foodItemsModels[j].foodProtein);
                            totalCarbs += float.Parse(plans[i].foodItemsModels[j].foodCarbs);
                            totalFat += float.Parse(plans[i].foodItemsModels[j].foodFat);

                            DinnerCaloriesInNumber += float.Parse(plans[i].foodItemsModels[j].foodCalories);
                            SnacksCarbsInNumber += float.Parse(plans[i].foodItemsModels[j].foodCarbs);
                            SnacksFatInNumber += float.Parse(plans[i].foodItemsModels[j].foodFat);
                            SnacksProtienInNumber += float.Parse(plans[i].foodItemsModels[j].foodProtein);
                        }
                    }
                    if (i != 0)
                    {
                        if (plans[i].date != plans[i - 1].date)
                        {
                            FoodItemsMacros itemsCalories = new FoodItemsMacros();
                            itemsCalories.Day = i;
                            itemsCalories.Carbs = totalColories;
                            itemsCalories.Fat = totalFat;
                            itemsCalories.Protein = totalProtien;

                            foodCaloriesStats.foodItemsMacros.Add(itemsCalories);

                        }
                    }
                    else
                    {
                        FoodItemsMacros itemsCalories = new FoodItemsMacros();
                        itemsCalories.Day = 0;
                        itemsCalories.Carbs = totalColories;
                        itemsCalories.Fat = totalFat;
                        itemsCalories.Protein = totalProtien;

                        foodCaloriesStats.foodItemsMacros.Add(itemsCalories);
                    }
                    allCollories += totalColories;
                }

                foodCaloriesStats.BreakfastCaloriesInNumber = BreakfastCaloriesInNumber;
                foodCaloriesStats.LunchCaloriesInNumber = LunchCaloriesInNumber;
                foodCaloriesStats.SnacksCaloriesInNumber = SnacksCaloriesInNumber;
                foodCaloriesStats.DinnerCaloriesInNumber = DinnerCaloriesInNumber;

                if (allCollories != 0)
                {
                    double result = (BreakfastCaloriesInNumber / allCollories) * 100;
                    foodCaloriesStats.BreakfastCaloriesInPercent = Math.Round(result);

                    result = (LunchCaloriesInNumber / allCollories) * 100;
                    foodCaloriesStats.LunchCaloriesInPercent = Math.Round(result);

                    result = (SnacksCaloriesInNumber / allCollories) * 100;
                    foodCaloriesStats.SnacksCaloriesInPercent = Math.Round(result);

                    result = (DinnerCaloriesInNumber / allCollories) * 100;
                    foodCaloriesStats.DinnerCaloriesInNumber = Math.Round(result);
                }
                if (totalLunchCal != 0)
                {
                    double result = (LunchCaloriesInNumber / allCollories) * 100;
                    foodCaloriesStats.LunchCaloriesInPercent = Math.Round(result);
                }
                if (totalSnacksCal != 0)
                {
                    double result = (SnacksCaloriesInNumber / allCollories) * 100;
                    foodCaloriesStats.SnacksCaloriesInPercent = Math.Round(result);
                }
                if (totalDinnerCal != 0)
                {
                    double result = (DinnerCaloriesInNumber / allCollories) * 100;
                    foodCaloriesStats.SnacksCaloriesInPercent = Math.Round(result);
                }

                return StatusCode(StatusCodes.Status200OK, foodCaloriesStats);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }


        [HttpGet]
        [Route("getDietPlanTest")]
        public async Task<IActionResult> getDietPlanTest()
        {
            var a = dbContext.DietPlans.Include(x => x.foodItemsModels);
            return StatusCode(StatusCodes.Status200OK, a);
        }
        [HttpPut]
        [Route("updateRequest")]
        //authorize this method for admin only
        public async Task<IActionResult> updateRequest([FromQuery] String email, [FromQuery] String request)
        {
            var found = dbContext.Users.FindAsync(email);
            UserModel result = found.Result;

            if (result == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, new Response { Status = "404", Message = "User with this email not found" });
            }
            result.isVeified = request;
            dbContext.Users.Update(result);
            await dbContext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status200OK, new Response { Status = "200", Message = "Status Changed Successfully!" });
        }

        [HttpPut]
        [Route("updateProfile")]
        public async Task<IActionResult> updateProfile([FromBody] UserModel user)
        {
            //var found = dbContext.Users.FindAsync(user.email);
            //UserModel result = found.Result;
            //if (user.password != result.password)
            //{
            //    return StatusCode(StatusCodes.Status401Unauthorized, new Response { Status = "401", Message = "Incorect Password" });
            //}
            //if (result == null)
            //{
            //    return StatusCode(StatusCodes.Status404NotFound, new Response { Status = "404", Message = "User with this email not found" });
            //}
            //String verify = result.isVeified;
            //user.isVeified = verify;
            //dbContext.Users.Update(user);
            //await dbContext.SaveChangesAsync();

            try
            {
                var local = dbContext.Set<UserModel>()
                                     .Local
                                     .FirstOrDefault(entry => entry.email.Equals(user.email));
                // check if local is not null 
                if (local != null)
                {
                    // detach
                    dbContext.Entry(local).State = EntityState.Detached;
                }
                // set Modified flag in your entry
                dbContext.Entry(user).State = EntityState.Modified;
                // save 
                dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { Message = "Profile Updated Successfully!", Profile = user });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { Message = "Email not found!" });
            }
        }
    }
}
