using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoListProject.DTOs;
using ToDoListProject.Models;

namespace ToDoListProject.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class RoleController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;

        //private readonly IdentityRole identityRole;

        public RoleController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager) //IdentityRole identityRole)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            // this.identityRole = identityRole;
        }

        [HttpPost("AddRole")]
        public async Task<IActionResult> CreateRole(RoleDTO roleDTO)
        {
            if(ModelState.IsValid == true)
            {
                bool isFound = await roleManager.RoleExistsAsync(roleDTO.RoleName);
                if (isFound == true)
                {
                    ModelState.AddModelError("", "Role already exists.");
                }
                else
                {
                    IdentityRole identityRole = new IdentityRole();
                    identityRole.Name = roleDTO.RoleName;
                    IdentityResult result = await roleManager.CreateAsync(identityRole);
                    if (result.Succeeded)
                    {
                        return Ok("Role is added");
                    }
                    else
                    {
                        foreach (IdentityError error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }
            }
            return BadRequest(ModelState);
            
        }

        [HttpPost("AssignRole")]
        public async Task<IActionResult> AssignRole(string userid, string roleName)
        {
            var user = await userManager.FindByIdAsync(userid);
            if (user == null)
            {
                return NotFound("User Not Found.");
            }
            if(!await roleManager.RoleExistsAsync(roleName))
            {
                return NotFound("This Role Is Not Found.");
            }
            IdentityResult result = await userManager.AddToRoleAsync(user, roleName);
            if(result.Succeeded)
            {
                return Ok("Assigning is done");
            }
            return BadRequest(ModelState);
        }

        [HttpGet("RemoveAllUsers")]
        public async Task<IActionResult> RemoveAllUsers()
        {
            var users = await userManager.Users.ToListAsync();
            foreach (var user in users)
            {
               IdentityResult result = await userManager.DeleteAsync(user);
                if (!result.Succeeded)
                {
                    return BadRequest($"Failed to delete user : {user.FirstName + user.LastName}");
                }
            }
            return Ok("All Users Are Deleted");
        }
    }
}
