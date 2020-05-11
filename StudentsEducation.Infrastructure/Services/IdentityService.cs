using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StudentsEducation.Domain.Entities;
using StudentsEducation.Domain.Interfaces;
using StudentsEducation.Infrastructure.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentsEducation.Infrastructure.Services
{
    public class IdentityService
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IAsyncRepository<Teacher> _service;
        public IdentityService(RoleManager<Role> roleManager,
            SignInManager<AppUser> signInManager,
            UserManager<AppUser> userManager,
            IAsyncRepository<Teacher> service
           // IdentityUserRole<string> userRole)
           )
        {
            _service = service;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _userManager = userManager;
            //_userRole = userRole;
        }

        public async Task<IEnumerable<Role>> GetRolesAsync()
        {
            return await _roleManager.Roles.ToListAsync();
        }

        public async Task<IEnumerable<AppUser>> GetUsersByRoleAsync(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            return await _userManager.GetUsersInRoleAsync(role.Name);
        }
        public async Task<Role> GetRoleAsync(string roleId)
        {
            return await _roleManager.FindByIdAsync(roleId);
        }
        public async Task<AppUser> GetUserAsync(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }

        public async Task<IEnumerable<string>> GetRolesByUser(AppUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var result = roles.AsEnumerable();
            return result;
        }
        public async Task CreateRoleAsync(Role role)
        {
            await _roleManager.CreateAsync(role);
        }
        public async Task UpdateRoleAsync(Role role)
        {
            await _roleManager.UpdateAsync(role);
        }
        public async Task DeleteRoleAsync(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            await _roleManager.DeleteAsync(role);
        }

        public async Task LoginAsync(AppUser user,string password, bool isPersistent)
        {
            await _signInManager.CheckPasswordSignInAsync(user, password, false);
            await _signInManager.SignInAsync(user, isPersistent);
        }
        public async Task LogoutAsync()
        {
           await _signInManager.SignOutAsync();
        }

        public async Task<IEnumerable<AppUser>> GetUsersAsync()
        {
            return await _userManager.Users.ToListAsync();
        }
        public async Task UpdateUserAsync(AppUser user)
        {
            await _userManager.UpdateAsync(user);
        }

        public async Task<IdentityResult> RegisterUser(AppUser user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<AppUser> GetUserByUserNameAsync(string userName)
        {
            return await _userManager.FindByNameAsync(userName);
        }

        public async Task CreateTeacherRelativeToUserAsync(Teacher teacher, AppUser user)
        {
            var resTeacher=await _service.CreateAsync(teacher);

            user.DbId = resTeacher.Id.ToString();
            await _userManager.UpdateAsync(user);
        }
    }
}
