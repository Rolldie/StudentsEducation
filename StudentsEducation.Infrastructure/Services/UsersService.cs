using Microsoft.AspNetCore.Identity;
using StudentsEducation.Infrastructure.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsEducation.Infrastructure.Services
{
    public class UsersService
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;

        public UsersService(RoleManager<Role> roleManager,
            SignInManager<AppUser> signInManager,
            UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<IEnumerable<AppUser>> GetUsersByRoleAsync(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            return await _userManager.GetUsersInRoleAsync(role.Name);
        }


    }
}
