using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ToDoListProject.DTOs;
using ToDoListProject.Models;
using ToDoListProject.Services;

namespace ToDoListProject.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> manager;
        private readonly IConfiguration configuration;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(UserManager<ApplicationUser> manager, IConfiguration configuration, SignInManager<ApplicationUser> signInManager)
        {
            this.manager = manager;
            this.configuration = configuration;
            this.signInManager = signInManager;
        }
        //Registeration Action
        [HttpPost("Register")]
        public async Task<IActionResult> Registeration(RegisterUserDTO registerUserDTO)
        {
            if (ModelState.IsValid == true)
            {
                ApplicationUser user = new ApplicationUser();
                user.FirstName = registerUserDTO.FirstName;
                user.LastName = registerUserDTO.LastName;
                user.Email = registerUserDTO.Email;
                user.Password = registerUserDTO.Password;
                user.UserName = registerUserDTO.UserName;
                user.Role = registerUserDTO.Role;
                IdentityResult result = await manager.CreateAsync(user, registerUserDTO.Password);
                if (result.Succeeded)
                {
                    return Ok("Succeeded");
                }
                return BadRequest(result.Errors);
            }
            return BadRequest(ModelState);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LogInDTO logInDTO)
        {
            if(ModelState.IsValid == true)
            {
                ApplicationUser user = await manager.FindByNameAsync(logInDTO.UserName);
                if(user != null)
                {
                   bool isFound = await manager.CheckPasswordAsync(user, logInDTO.Password);
                    if(isFound == true)
                    {
                        var claims = new List<Claim>();
                        claims.Add(new Claim(ClaimTypes.Name, user.UserName));
                        claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
                        claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

                        var roles = await manager.GetRolesAsync(user);
                        foreach (var role in roles)
                        {
                            claims.Add(new Claim(ClaimTypes.Role, role));
                        }

                        SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]));

                        SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                        JwtSecurityToken token = new JwtSecurityToken(
                                issuer: configuration["JWT:Issuer"],
                                audience: configuration["JWT:Audiance"],
                                claims:claims,
                                expires:DateTime.Now.AddDays(1),
                                signingCredentials: credentials
                            );

                        return Ok(new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                            expiration = token.ValidTo
                        });
                    }
                    return Unauthorized();
                }
                return Unauthorized();
            }
            return Unauthorized();
        }

        //logout Action
        [HttpGet("Logout")]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return Ok(new { Message = "Logout Is Successful" });
        }
    }
}
