﻿
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using EF_DietaNoDietaApi.Model;
using EF_DietaNoDietaApi.MySql;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
//using Microsoft.IdentityModel.Tokens;


namespace EF_DietaNoDietaApi.Controllers
{
    [Route("api/Authenticate")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
                
        private readonly IConfiguration _config;
        private readonly MySqlDbContext dbContext;

        public AuthenticationController(MySqlDbContext context, IConfiguration configuration)
        {

            dbContext = context;
            _config = configuration;
        }

        [HttpPost]
        [Route("Register")]//http://localhost:5000/api/Authenticate/Register
        public async Task<IActionResult> register([FromBody] RegisterModel register)
        {
            //var found =  dbContext.Users.First(x=> x.email == user.email);
            await using var transaction = await dbContext.Database.BeginTransactionAsync();
            var found = dbContext.RegisterUsers.FindAsync(register.email);            
            if (found.Result != null)
                return StatusCode(StatusCodes.Status409Conflict, new Response { Status = "409", Message = "User with this Email already exist" });
            UserModel user = new UserModel();
            user.email = register.email;
            user.fitnessLevel = register.fitnessLevel;
            user.phoneNumber = register.phoneNumber;             
            user.isVeified = "waiting";
            user.UserRole = register.UserRole;
            register.password = "abc123";
            await dbContext.RegisterUsers.AddAsync(register);
            int num1 = await dbContext.SaveChangesAsync();
            await dbContext.Users.AddAsync(user);
            int num2 = await dbContext.SaveChangesAsync();
            await transaction.CommitAsync();
            if (num1 != 0 && num2 != 0)
                return Ok(new Response { Status = "200", Message = "User registered Successfully" });
            else
            {                
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "500", Message = "User could not register" });
            }
         
        }


        [HttpPost]
        [Route("Register/Trainer")]//http://localhost:5000/api/Authenticate/Register/Trainer
        public async Task<IActionResult> registerTrainer([FromBody] TrainerModel trainer)
        {
            //var found =  dbContext.Users.First(x=> x.email == user.email);
            await using var transaction = await dbContext.Database.BeginTransactionAsync();
            var found = dbContext.RegisterUsers.FindAsync(trainer.email);
            if (found.Result != null)
                return StatusCode(StatusCodes.Status409Conflict, new Response { Status = "409", Message = "Trainer with this Email already exist" });
            
            RegisterModel register = new RegisterModel();
            register.email = trainer.email;
            register.password = "abc123";
            register.phoneNumber = trainer.phoneNumber;
            register.UserRole = "Trainer";
            register.fitnessLevel = "0";
            await dbContext.Trainer.AddAsync(trainer);
            int num2 = await dbContext.SaveChangesAsync();
            await dbContext.RegisterUsers.AddAsync(register);
            int num1 = await dbContext.SaveChangesAsync();            
            await transaction.CommitAsync();
            if (num1 != 0 && num2 !=0)
                return Ok(new Response { Status = "200", Message = "Trainer registered Successfully" });
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "500", Message = "Trainer could not register" });
            }

        }

        [HttpPost]
        [Route("Register/Nutritionist")]//http://localhost:5000/api/Authenticate/Register/Nutritionist
        public async Task<IActionResult> registerNutritionist([FromBody] NutritionistModel nutritionist)
        {
            //var found =  dbContext.Users.First(x=> x.email == user.email);
            try
            {
                await using var transaction = await dbContext.Database.BeginTransactionAsync();
                var found =  dbContext.RegisterUsers.FindAsync(nutritionist.email);                
                if (found.Result != null)
                    return StatusCode(StatusCodes.Status409Conflict, new Response { Status = "409", Message = "Nutritionist with this Email already exist" });

                RegisterModel register = new RegisterModel();
                register.email = nutritionist.email;
                register.password = "abc123";
                register.phoneNumber = nutritionist.phoneNumber;
                register.UserRole = "Nutritionist";
                register.fitnessLevel = "0";
                await dbContext.Nutritionist.AddAsync(nutritionist);
                int num2 = await dbContext.SaveChangesAsync();
                await dbContext.RegisterUsers.AddAsync(register);
                int num1 = await dbContext.SaveChangesAsync();
                await transaction.CommitAsync();
                if (num1 != 0 && num2 != 0)
                    return Ok(new Response { Status = "200", Message = "Nutritionist registered Successfully" });
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "500", Message = "Nutritionist could not register" });
                }
            }
            catch (Exception ex) {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "500", Message = ex.Message });
            }
        }

        [HttpPost]
        [Route("Login/Nutritionist")]//http://localhost:5000/api/Authenticate/Login/Trianer
        public async Task<IActionResult> loginNutritionist([FromBody] LoginModel login)
        {
            var found = dbContext.RegisterUsers.FindAsync(login.email);
            if (found.Result == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, new Response { Status = "400", Message = "User with this email not found" });
            }
            if (found.Result.password != login.password)
            {
                return StatusCode(StatusCodes.Status406NotAcceptable, new Response { Status = "Unauthorized", Message = "Wrong Password" });
            }
            var result = dbContext.Nutritionist.FindAsync(login.email);
            NutritionistModel nutritionist = result.Result;

            // String token = GenerateJWTToken(user);
            return Ok(new { Status = "200", Message = "User Logged in Successfully", Profile = nutritionist, UserRole = "Nutritionist" });


        }


        [HttpGet]
        [Route("test")]
        public String get() {

                String test = "working";
            return test;
        }

        [HttpPost]
        [Route("Login")]////http://localhost:5000/api/Authenticate/Login
        public async Task<IActionResult> login([FromBody] LoginModel login)
        {
            var found = dbContext.RegisterUsers.FindAsync(login.email);
            if (found.Result == null) {
                return StatusCode(StatusCodes.Status404NotFound, new Response { Status = "400", Message = "User with this email not found" });
            }
            if (found.Result.password != login.password)
            {
                return StatusCode(StatusCodes.Status406NotAcceptable, new Response { Status = "Unauthorized", Message = "Wrong Password" });
            }
            var result = dbContext.Users.FindAsync(login.email);
            UserModel user= result.Result;
            if (user.isVeified == "true" )
            {
               // String token = GenerateJWTToken(user);
                return Ok(new { Status = "200", Message = "User Logged in Successfully",Profile = user});
            }

            return StatusCode(StatusCodes.Status406NotAcceptable, new Response { Status = "Unauthorized", Message = "Not verified by admin" });
        }

        [HttpPost]
        [Route("Login/Trainer")]//http://localhost:5000/api/Authenticate/Login/Trianer
        public async Task<IActionResult> loginTrainer([FromBody] LoginModel login)
        {
            var found = dbContext.RegisterUsers.FindAsync(login.email);
            if (found.Result == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, new Response { Status = "400", Message = "User with this email not found" });
            }
            if (found.Result.password != login.password)
            {
                return StatusCode(StatusCodes.Status406NotAcceptable, new Response { Status = "Unauthorized", Message = "Wrong Password" });
            }
            var result = dbContext.Trainer.FindAsync(login.email);
            TrainerModel trainer = result.Result;
            
                // String token = GenerateJWTToken(user);
                return Ok(new { Status = "200", Message = "User Logged in Successfully", Profile = trainer,UserRole= "Trainer"});            

            
        }

        String GenerateJWTToken(UserModel userInfo)
        {
            var securityKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"]));
            var credentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(securityKey, Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, userInfo.email),            
            new Claim("role",userInfo.UserRole),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
            var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);

        }














        //[HttpPost]
        //[Route("Register")]
        //public async Task<IActionResult> register([FromBody] RegisterModel register)
        //{

        //    var userExist = await userManager.FindByEmailAsync(register.email);
        //    register.password = "!NoPass1";
        //    if (userExist != null)
        //        return StatusCode(StatusCodes.Status409Conflict, new Response { Status = "409", Message = "User with this Email already exist" });
        //    ApplicationUser user = new ApplicationUser()
        //    {
        //        Email = register.email,
        //        SecurityStamp = Guid.NewGuid().ToString(),
        //        UserName = register.email,
        //        PhoneNumber = register.phoneNumber
        //    };
        //    //genertae password and send email to user 
        //    var result = await userManager.CreateAsync(user, register.password);
        //    if (!result.Succeeded)
        //    {

        //        return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "500", Message = "User could not register" });
        //    }
        //    if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
        //        await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
        //    if (!await roleManager.RoleExistsAsync(UserRoles.User))
        //        await roleManager.CreateAsync(new IdentityRole(UserRoles.User));
        //    if (!await roleManager.RoleExistsAsync(UserRoles.Neutritionist))
        //        await roleManager.CreateAsync(new IdentityRole(UserRoles.Neutritionist));
        //    if (!await roleManager.RoleExistsAsync(UserRoles.Trainer))
        //        await roleManager.CreateAsync(new IdentityRole(UserRoles.Trainer));
        //    if (await roleManager.RoleExistsAsync(UserRoles.User)) {                                
        //        //IEnumerable<String> admin = new[] { UserRoles.Admin };                
        //        await userManager.AddToRoleAsync(user, UserRoles.User);

        //    }
        //    return Ok(new Response { Status = "Succcess", Message = "User Registered Successfully" });


        //}

        //[HttpPost]
        //[Route("Login")]
        //public async Task<IActionResult> login([FromBody] LoginModel login)
        //{

        //    var userExist = await userManager.FindByEmailAsync(login.email);            
        //    if (userExist == null)
        //        return StatusCode(StatusCodes.Status409Conflict, new Response { Status = "409", Message = "User with this Email does not exist" });
        //    ApplicationUser user = new ApplicationUser()
        //    {
        //        Email = login.email,
        //        SecurityStamp = Guid.NewGuid().ToString(),
        //        UserName = login.email,                
        //    };            
        //    var result = await signInManager.PasswordSignInAsync(userExist, login.password, false, false);            
        //    if (user != null && result.Succeeded) {

        //        var userRoles = await userManager.GetRolesAsync(user);
        //        var authClaims = new List<Claim> {
        //                new Claim(ClaimTypes.Email,user.Email),
        //                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
        //        };
        //        foreach (var userRole in userRoles)
        //        {
        //            authClaims.Add(new Claim(ClaimTypes.Role,userRole));

        //        }
        //        var signInKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
        //        var token = new JwtSecurityToken(
        //            issuer: _configuration["JWT:Secret"],
        //            audience: _configuration["JWT:ValidAudience"],
        //            expires: DateTime.Now.AddMonths(4), 
        //            claims: authClaims,
        //            signingCredentials: new SigningCredentials(signInKey,SecurityAlgorithms.HmacSha256)
        //            );
        //        return Ok(new { Token = new JwtSecurityTokenHandler().WriteToken(token) } );
        //    }            
        //    return StatusCode(StatusCodes.Status406NotAcceptable, new Response { Status = "Unauthorized", Message = "Password is incorrect" });



        //}


        //private String GenerateJWTToken(User userInfo)
        // {
        //     var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
        //     var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        //     var claims = new[]
        //     {
        //     new Claim(JwtRegisteredClaimNames.Sub, userInfo.UserName),
        //     new Claim("fullName", userInfo.FullName.ToString()),
        //     new Claim("role",userInfo.UserRole),
        //     new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        //     };
        //     var token = new JwtSecurityToken(
        //     issuer: _configuration["Jwt:Issuer"],
        //     audience: _configuration["Jwt:Audience"],
        //     claims: claims,
        //     expires: DateTime.Now.AddMinutes(30),
        //     signingCredentials: credentials
        //     );
        //     return new JwtSecurityTokenHandler().WriteToken(token);

        // }

    }
}
