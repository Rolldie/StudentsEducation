using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using StudentsEducation.Infrastructure.Identity.Data;
using StudentsEducation.Infrastructure.Services;

namespace StudentsEducation.Web.Areas.Admin.Pages.Users_relation
{
    public class EditModel : PageModel
    {
        private readonly IdentityService _service;
        public EditModel(IdentityService service)
        {
            _service = service;
        }
        [BindProperty]
        public AppUser AppUser { get; set; }
        [BindProperty]
        [Required(ErrorMessage ="Пользователю необходимо выбрать роль!")]
        public string SelectedRole { get; set; }
        public SelectList RoleSelectList { get; set; }
        public Role Role { get; set; }
        public async Task<IActionResult> OnGetAsync(string id)
        {
            if(string.IsNullOrEmpty(id))
            {
                return NotFound();
            }
            await UpdateFields(id);
            if (AppUser == null) return NotFound();
            return Page();
        }
        public async Task UpdateFields(string id)
        {
            AppUser = await _service.GetUserAsync(id);
            if (AppUser == null) return;
            var userRole = await _service.GetRoleByUserAsync(AppUser);
            if (userRole == null)
                await _service.SetNewRoleToUserAsync(AppUser, "Administrator");
            userRole = await _service.GetRoleByUserAsync(AppUser);
            SelectedRole = userRole.Id;
            RoleSelectList = new SelectList((await _service.GetRolesAsync()), "Id", "Name", SelectedRole);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await UpdateFields(AppUser.Id);
                return Page();
            }
            //TODO: did something with this!!
            var user = await _service.GetUserAsync(AppUser.Id);
            user.Email = AppUser.Email;
            user.UserName = AppUser.UserName;
            user.DbId = AppUser.DbId;

            await _service.UpdateUserAsync(user);
            await _service.SetNewRoleToUserByIdAsync(user, SelectedRole);
            
            return RedirectToPage("./Index");

        }
    }
}