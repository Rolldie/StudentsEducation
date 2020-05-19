using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.CSharp;
using StudentsEducation.Web.Areas.Account.Data;
using StudentsEducation.Web.Areas.Account.Services;

namespace StudentsEducation.Web.Areas.Admin.Pages.Users_relation
{
    public class IndexModel : PageModel
    {
        private readonly IdentityService _service;

        public IndexModel(IdentityService service)
        {
            _service = service;
        }

        public IEnumerable<AppUser> Users { get; set; }
        public IEnumerable<OutputUser> OutUsers { get; set; }
        public class OutputUser
        {
            public string Id { get; set; }
            public string Email { get; set; }
            public string UserName { get; set; }
            public string Role { get; set; }
            public int DbId { get; set; }
        }
        public async Task OnGetAsync()
        {
            await InitializeLists();
        }

        public async Task InitializeLists()
        {
            Users = await _service.GetUsersAsync();
            OutUsers = Users.Select(e => new OutputUser
            {
                Id = e.Id,
                UserName = e.UserName,
                DbId = e.DbId != null ? int.Parse(e.DbId) : -1,
                Role =_service.GetRoleByUserAsync(e).Result.Name,
                Email = e.Email
            }); 
        }


        public async Task<IActionResult> OnPostAsync(string id)
        {
            string RequireRole = "Administrator";
            if (string.IsNullOrEmpty(id)) return Page();
            var user = await _service.GetUserAsync(id);
            var role = await _service.GetRoleByUserAsync(user);
            if (role.Name == RequireRole && (await _service.GetUsersByRoleNameAsync(RequireRole)).Count() > 1)
                await _service.DeleteUserAsync(id);
            else if (role.Name == RequireRole) return RedirectToPage(Url.Content("~/Error"));
            else
            {
                await _service.DeleteUserAsync(id);
            }
            await InitializeLists();
            return Page();
        }
    }
}