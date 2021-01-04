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
    [Route("api/NutritionistProfile")]
    [ApiController]
    public class NutritionistProfileController: ControllerBase
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
    }
}
