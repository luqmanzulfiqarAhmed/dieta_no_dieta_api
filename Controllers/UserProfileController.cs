﻿using System;
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
    [Route("api/UserProfile")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly MySqlDbContext dbContext;

        public UserProfileController(MySqlDbContext context, IConfiguration configuration)
        {

            dbContext = context;
            _config = configuration;
        }
        [HttpGet]
        [Route("getProfiles")]//for admin only
        public async Task<IActionResult> getProfiles([FromQuery] int item){
            IQueryable<Model.UserModel> result=null;
            if (item == 0)//get all users
            {
                result = dbContext.Users.Where(p => p.UserRole == UserRoles.User);
                return StatusCode(StatusCodes.Status200OK, result);
            }
            else if (item == 1)//get all verified users
            {
                result = dbContext.Users.Where(p => p.UserRole == UserRoles.User && p.isVeified == "true");
                return StatusCode(StatusCodes.Status200OK, result);
            }
            else if (item == 2)//get all blocked users
            {
                result = dbContext.Users.Where(p => p.UserRole == UserRoles.User && p.isVeified == "false");
                return StatusCode(StatusCodes.Status200OK, result);
            }
            else if (item == 3)//get all not waiting users
            {
                result = dbContext.Users.Where(p => p.UserRole == UserRoles.User && p.isVeified == "waiting");
                return StatusCode(StatusCodes.Status200OK, result);
            }
            return StatusCode(StatusCodes.Status406NotAcceptable, new {Message = "type of user not found" });

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

        [HttpPost]
        [Route("assign/Neutritionist")]//http://localhost:5000/api/Authenticate/Register/Trainer
        public async Task<Object> assignNeutritionist([FromQuery] String neutritionistEmail, [FromQuery] String userEmail)
        {

            await using var transaction = await dbContext.Database.BeginTransactionAsync();
            var user = dbContext.Users.FindAsync(userEmail);
            if (user.Result == null)
                return StatusCode(StatusCodes.Status409Conflict, new Response { Status = "409", Message = "User does not found" });
            var neutrtionist = dbContext.Nutritionist.FindAsync(neutritionistEmail);
            if (neutrtionist.Result == null)
                return StatusCode(StatusCodes.Status409Conflict, new Response { Status = "409", Message = "Neutrtionist does not found" });

            //dbContext.Users.Attach(user.Result);
            //dbContext.Entry(user.Result).Property(x => x.neutritionistEmail).IsModified = true;
            //dbContext.Entry(user.Result).Property(x => x.neutritionistEmail).CurrentValue= neutritionistEmail;
            user.Result.neutritionistEmail = neutritionistEmail;            
            var result = dbContext.Users.Update(user.Result);
            int num = dbContext.SaveChanges();
            
            if (result  != null && num !=0)
                return Ok( new Response { Status = "200", Message = "Neutrtionist assigned successfully" });
            else
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "500", Message = "Neutrtionist could not assigned" });

        }

    }
}
