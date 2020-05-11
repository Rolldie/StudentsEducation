using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if(string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            AppUser = await _service.GetUserAsync(id);
            if (AppUser == null) return NotFound();

            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            //TODO: did something with this!!
            var user = await _service.GetUserAsync(AppUser.Id);
            user.Email = AppUser.Email;
            user.UserName = AppUser.UserName;
            user.DbId = AppUser.DbId;

            await _service.UpdateUserAsync(user);
            
            
            
            return RedirectToPage("./Index");

        }
    }
}