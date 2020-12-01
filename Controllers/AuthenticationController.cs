using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DietaNoDietaApi.Authentication;
using DietaNoDietaApi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DietaNoDietaApi.Controllers
{
    [Route("api/Authenticate")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration _configuration;

        public AuthenticationController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            _configuration = configuration;
        }
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> register([FromBody] RegisterModel register)
        {

            var userExist = await userManager.FindByEmailAsync(register.email);
            register.password = "!NoPass1";
            if (userExist != null)
                return StatusCode(StatusCodes.Status409Conflict, new Response { Status = "409", Message = "User with this Email already exist" });
            ApplicationUser user = new ApplicationUser()
            {
                Email = register.email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = register.email,
                PhoneNumber = register.phoneNumber
            };
            //genertae password and send email to user 
            var result = await userManager.CreateAsync(user, register.password);
            if (!result.Succeeded)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "500", Message = "User could not register" });
            }
            if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            if (!await roleManager.RoleExistsAsync(UserRoles.User))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.User));
            if (!await roleManager.RoleExistsAsync(UserRoles.Neutritionist))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Neutritionist));
            if (!await roleManager.RoleExistsAsync(UserRoles.Trainer))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Trainer));
            if (await roleManager.RoleExistsAsync(UserRoles.User)) {                                
                //IEnumerable<String> admin = new[] { UserRoles.Admin };                
                await userManager.AddToRoleAsync(user, UserRoles.User);
            
            }
            return Ok(new Response { Status = "Succcess", Message = "User Registered Successfully" });


        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> login([FromBody] LoginModel login)
        {

            var userExist = await userManager.FindByEmailAsync(login.email);            
            if (userExist == null)
                return StatusCode(StatusCodes.Status409Conflict, new Response { Status = "409", Message = "User with this Email does not exist" });
            ApplicationUser user = new ApplicationUser()
            {
                Email = login.email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = login.email,                
            };            
            var result = await signInManager.PasswordSignInAsync(userExist, login.password, false, false);            
            if (user != null && result.Succeeded) {

                var userRoles = await userManager.GetRolesAsync(user);
                var authClaims = new List<Claim> {
                        new Claim(ClaimTypes.Email,user.Email),
                        new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                };
                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role,userRole));

                }
                var signInKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:Secret"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddMonths(4), 
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(signInKey,SecurityAlgorithms.HmacSha256)
                    );
                return Ok(new { Token = new JwtSecurityTokenHandler().WriteToken(token) } );
            }            
            return StatusCode(StatusCodes.Status406NotAcceptable, new Response { Status = "Unauthorized", Message = "Password is incorrect" });
            


        }

    }
}
