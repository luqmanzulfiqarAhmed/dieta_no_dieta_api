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
        public async Task<IActionResult> getDietPlan([FromQuery] String trainerEmail, String userEmail)
        {
            try
            {
                IQueryable<Model.DietPlanModel> result = null;
                result = dbContext.DietPlans.Where(p => p.userEmail == userEmail && p.trainerEmail == trainerEmail).Include(e => e.foodItemsModels).ThenInclude(f=>f.foodDescriptionModels);                                
                
                return StatusCode(StatusCodes.Status200OK, result);
            }
            catch (Exception ex) {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
            }
        }

        [HttpGet]
        [Route("getDietPlan/Wishlist")]
        public async Task<IActionResult> getDietPlanWishList([FromQuery] String trainerEmail)
        {
            try
            {
                IQueryable<Model.DietPlanModel> result = null;
                result = dbContext.DietPlans.Where(p => p.isWishlist== "true" && p.trainerEmail == trainerEmail).Include(e => e.foodItemsModels).ThenInclude(f => f.foodDescriptionModels);

                return StatusCode(StatusCodes.Status200OK, result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
            }
        }

        [HttpPost]
        [Route("addDietPlan")]//http://localhost:5000/api/Authenticate/Register
        public async Task<IActionResult> addPlan([FromBody] DietPlanModel dietPlanModel)
        {
            //var found =  dbContext.Users.First(x=> x.email == user.email);            
            await using var transaction = await dbContext.Database.BeginTransactionAsync();                        
            await dbContext.DietPlans.AddAsync(dietPlanModel);
            int num1 = await dbContext.SaveChangesAsync();            
            await transaction.CommitAsync();
            if (num1 != 0 )
                return Ok(new Response { Status = "200", Message = "Diet Plan added Successfully" });
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "500", Message = "Plan could not insert" });
            }

        }


        [HttpPut]
        [Route("updateRequest")]
        //authorize this method for admin only
        public async Task<IActionResult> updateRequest([FromQuery] String email,[FromQuery] String request) {
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
                return StatusCode(StatusCodes.Status200OK, new { Message = "Profile Updated Successfully!",Profile= user });
            }
            catch (Exception ex) {
                return StatusCode(StatusCodes.Status404NotFound, new { Message = "Email not found!" });
            }
        }
    }
}
