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
    [Route("api/NutritionistProfile/")]
    [ApiController]
    public class NutritionistProfileController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly MySqlDbContext dbContext;

        public NutritionistProfileController(MySqlDbContext context, IConfiguration configuration)
        {

            dbContext = context;
            _config = configuration;
        }
        [HttpGet]
        [Route("getProfiles")]//for admin only
        public async Task<IActionResult> getProfiles([FromQuery] int item)
        {
            IQueryable<Model.NutritionistModel> result = null;
            if (item == 0)//get all users
            {
                result = dbContext.Nutritionist;
                return StatusCode(StatusCodes.Status200OK, result);
            }
            //else if (item == 1)//get all verified users
            //{
            //    result = dbContext.Nutritionist.Where(p => p.UserRole == UserRoles.User && p.isVeified == "true");
            //    return StatusCode(StatusCodes.Status200OK, result);
            //}
            //else if (item == 2)//get all blocked users
            //{
            //    result = dbContext.Nutritionist.Where(p => p.UserRole == UserRoles.User && p.isVeified == "false");
            //    return StatusCode(StatusCodes.Status200OK, result);
            //}
            //else if (item == 3)//get all not waiting users
            //{
            //    result = dbContext.Nutritionist.Where(p => p.UserRole == UserRoles.User && p.isVeified == "waiting");
            //    return StatusCode(StatusCodes.Status200OK, result);
            //}
            return StatusCode(StatusCodes.Status406NotAcceptable, new { Message = "type of user not found" });

        }

        [HttpGet]
        [Route("getUsers")]
        public async Task<IActionResult> getUsers([FromQuery] String neutritionistEmail)
        {
            try
            {

                var result = dbContext.Users
                       .Where(p => p.neutritionistEmail == neutritionistEmail)
                       .Select(x => new
                       {
                           x.email,
                           x.date,
                           x.fullName
                       });

                return StatusCode(StatusCodes.Status200OK, result);

            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
            }
        }

         
        [HttpPut]
        [Route("updateProfile")]
        public async Task<IActionResult> updateProfile([FromBody] NutritionistModel user)
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
                var local = dbContext.Set<NutritionistModel>()
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

        [HttpPost]
        [Route("PostGoals")]
        public async Task<IActionResult> PostGoals([FromBody] DietPlanGoals goals)
        {
            goals.isCompleted = "false";
            float percent= (float.Parse(goals.CurrentWeight)/ float.Parse(goals.TargetWeight)) * 100 ;
            goals.WeightPercentage = (int)percent;

            await using var transaction = await dbContext.Database.BeginTransactionAsync();
            var user = await dbContext.Users.FindAsync(goals.UserEmail);
            var nutrtionist = await dbContext.Nutritionist.FindAsync(goals.NutrtionistEmail);
            if (user is null)
                return StatusCode(StatusCodes.Status404NotFound, new Response { Status = "404", Message = "User does not found" });
            if (nutrtionist is null)
                return StatusCode(StatusCodes.Status404NotFound, new Response { Status = "404", Message = "Nutrtionist does not found" });
            var result = await dbContext.DietPlanGoals.Where(p => p.UserEmail == goals.UserEmail && p.GoalType == goals.GoalType && p.isCompleted == "false").ToListAsync();
            if (result.Count() != 0)
                return StatusCode(StatusCodes.Status406NotAcceptable, new Response { Status = "406", Message = "There is already a incomplete goal of this type." });
            await dbContext.DietPlanGoals.AddAsync(goals);
            int num = await dbContext.SaveChangesAsync();
            await transaction.CommitAsync();
            if (num != 0)
                return Ok(new Response { Status = "200", Message = "Goal created Successfully" });
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "500", Message = "Something went wrong" });
            }
        }

        [HttpPost]
        [Route("addWaterGoal")]//http://localhost:5000/api/Authenticate/Register
        public async Task<IActionResult> addWaterGoals([FromBody] DietPlanWaterGoals goals)
        {

            goals.isCompleted = "false";
            float target = float.Parse(goals.WaterInLtrs);
            goals.TargetGlasses = 4 * target;
            goals.PerGlassValue = 1 / goals.TargetGlasses;
            goals.GlassesUserDrank = 0;
            goals.WaterPercentage = 0;

            await using var transaction = await dbContext.Database.BeginTransactionAsync();
            var user = await dbContext.Users.FindAsync(goals.UserEmail);
            var nutrtionist = await dbContext.Nutritionist.FindAsync(goals.NeutrtionistEmail);

            if (user is null)
                return StatusCode(StatusCodes.Status404NotFound, new Response { Status = "404", Message = "User does not found" });
            
            if (nutrtionist is null)
                return StatusCode(StatusCodes.Status404NotFound, new Response { Status = "404", Message = "Nutrtionist does not found" });
            
            var result = await dbContext.DietPlanWaterGoals.Where(p => p.UserEmail == goals.UserEmail  && p.isCompleted == "false").ToListAsync();
            if (result.Count() != 0)
                return StatusCode(StatusCodes.Status406NotAcceptable, new Response { Status = "406", Message = "There is already a incomplete goal of this type." });
            
            await dbContext.DietPlanWaterGoals.AddAsync(goals);            
            int num = await dbContext.SaveChangesAsync();
            await transaction.CommitAsync();
            
            if (num != 0)
                return Ok(new Response { Status = "200", Message = "Water Goals created Successfully" });
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "500", Message = "Something went wrong" });
            }

        }

        [HttpPut]
        [Route("updateWaterGoal")]//http://localhost:5000/api/Authenticate/Register
        public async Task<IActionResult> updateWaterGoal([FromQuery] String email ,[FromQuery] String GlassesUserDrank)
        {
            try
            {
                DietPlanWaterGoals planWaterGoals = dbContext.DietPlanWaterGoals
                 .Where(x => x.UserEmail == email && x.isCompleted.Equals( "false")).FirstOrDefault();
            if (planWaterGoals == null)
                return StatusCode(StatusCodes.Status404NotFound, "Ongoing Water goal against this user not found");
            
            planWaterGoals.AcheivedValue += planWaterGoals.PerGlassValue;
            int glass = int.Parse(GlassesUserDrank);
            if(planWaterGoals.TargetGlasses > 0)
            {
                planWaterGoals.WaterPercentage = (glass /planWaterGoals.TargetGlasses) *100;
                planWaterGoals.GlassesUserDrank = glass;
            }
            if (planWaterGoals.AcheivedValue >= 1)
            {
                planWaterGoals.isCompleted = "true";
                return StatusCode(StatusCodes.Status200OK, "Task completed successfully");
            }
            dbContext.DietPlanWaterGoals.Update(planWaterGoals);
            await dbContext.SaveChangesAsync();

            return StatusCode(StatusCodes.Status200OK, planWaterGoals.AcheivedValue);
            }catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, ex.Message);
            }
        }

        [HttpPut]
        [Route("updateWeightGoal")]//http://localhost:5000/api/Authenticate/Register
        public async Task<IActionResult> updateWeightGoal([FromQuery] String email, [FromQuery] String currentWeight)
        {
            DietPlanGoals planWeightGoals = dbContext.DietPlanGoals
                 .Where(x => x.UserEmail == email && x.isCompleted.Equals("false")).FirstOrDefault();
            if (planWeightGoals == null)
                return StatusCode(StatusCodes.Status404NotFound, "Ongoing weight goal against this user not found");

            planWeightGoals.CurrentWeight = currentWeight;
            float percent = (float.Parse(planWeightGoals.CurrentWeight) / float.Parse(planWeightGoals.TargetWeight)) * 100;
            planWeightGoals.WeightPercentage = (int)percent;

            dbContext.DietPlanGoals.Update(planWeightGoals);
            await dbContext.SaveChangesAsync();

            if (planWeightGoals.CurrentWeight.Equals(planWeightGoals.TargetWeight))
                return StatusCode(StatusCodes.Status200OK, "You have acheived your target");



            return StatusCode(StatusCodes.Status200OK, "Current weight updated");
        }


        [HttpGet]
        [Route("GetAllWaterGoals")]
        public async Task<IActionResult> GetAllWaterGoals([FromQuery] String userEmail )
        {
            var goals = await dbContext.DietPlanWaterGoals.Where(p => p.UserEmail == userEmail && p.isCompleted == "true").ToListAsync();
            return StatusCode(StatusCodes.Status200OK, goals);
        }
        [HttpGet]
        [Route("GetOnGoinglWaterGoal")]
        public  IActionResult GetOnGoingWaterGoal([FromQuery] String userEmail)
        {
            var goals =  dbContext.DietPlanWaterGoals.Where(p => p.UserEmail == userEmail && p.isCompleted == "false" ).FirstOrDefault();
            return StatusCode(StatusCodes.Status200OK, goals);
        }
        [HttpGet]
        [Route("GetAllGoals")]
        public async Task<IActionResult> GetAllGoals([FromQuery] String nutrtionistEmail, [FromQuery] String isComplete)
        {
            var goals = await dbContext.DietPlanGoals.Where(p => p.NutrtionistEmail == nutrtionistEmail && p.isCompleted == isComplete).ToListAsync();
            return StatusCode(StatusCodes.Status200OK, goals);
        }
        [HttpGet]
        [Route("GetGoals")]
        public async Task<IActionResult> GetGoals([FromQuery] String nutrtionistEmail, [FromQuery] String goalType, [FromQuery] string isComplete)
        {
            var goals = await dbContext.DietPlanGoals.Where(p => p.NutrtionistEmail == nutrtionistEmail && p.GoalType == goalType && p.isCompleted == isComplete).ToListAsync();
            return StatusCode(StatusCodes.Status200OK, goals);
        }


        [HttpPost]
        [Route("postRating")]
        public async Task<IActionResult> PostRating(PostRatingViewModel postRatingViewModel)
        {
            try
            {
                int stars = 0;
                stars = int.Parse(postRatingViewModel.Stars);
                var user =await dbContext.Users.FindAsync(postRatingViewModel.UserEmail);
                if (user == null)
                    return StatusCode(StatusCodes.Status404NotFound, new { Message = "User not found" });

                var nutritionist = await dbContext.Nutritionist.FindAsync(postRatingViewModel.NutrtionistEmail);
               // NutritionistModel nutritionist = nutritionistModel.Result;
                if (nutritionist == null)
                    return StatusCode(StatusCodes.Status404NotFound, new { Message = "Nutrtionist not found" });

                var history = dbContext.RatingHistory.Where(x => x.UserEmail == postRatingViewModel.UserEmail);
                if (history.Count() != 0)
                    return StatusCode(StatusCodes.Status409Conflict, new { Message = "User already rated this nutrtionist" });

                RatingHistory rating = new RatingHistory()
                {
                    UserEmail = postRatingViewModel.UserEmail,
                    Stars = postRatingViewModel.Stars
                };
                var result = await dbContext.RatingHistory.AddAsync(rating);
                if (result == null)
                    return StatusCode(StatusCodes.Status503ServiceUnavailable, new { Message = "Something went wrong please try again" });

                float totalRatings = nutritionist.TotalRatings;
                totalRatings++;
                float totalStars = nutritionist.TotalStars;
                totalStars += stars;
                float averageStars = totalStars / totalRatings;

                nutritionist.TotalStars = totalStars;
                nutritionist.TotalRatings = totalRatings;
                nutritionist.AverageStars = averageStars;

                await dbContext.SaveChangesAsync();
                return StatusCode(StatusCodes.Status200OK, new { Message = "Thank you for posting. New rating is: " + nutritionist.AverageStars.ToString() });

            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
            }
        }


    }
}
