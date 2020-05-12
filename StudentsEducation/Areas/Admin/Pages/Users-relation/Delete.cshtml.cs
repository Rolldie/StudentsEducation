using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentsEducation.Domain.Entities;
using StudentsEducation.Domain.Interfaces;
using StudentsEducation.Infrastructure.Identity.Data;
using StudentsEducation.Infrastructure.Services;

namespace StudentsEducation.Web.Areas.Admin.Pages.Users_relation
{
    public class DeleteModel : PageModel
    {
        private readonly IdentityService _service;
        private readonly IAsyncRepository<Teacher> _repos;

        public DeleteModel(IdentityService service,IAsyncRepository<Teacher> repository)
        {
            _repos = repository;
            _service = service; 
        }
        [BindProperty]
        public AppUser AppUser { get; set; }
        public Teacher Teacher { get; set; }
        public async Task<IActionResult> OnGetAsync(string userId)
        {
            if(string.IsNullOrEmpty(userId))
            {
                return RedirectToPage(Url.Content("./Index"));
            }
            AppUser = await _service.GetUserAsync(userId);
            if (AppUser == null) return RedirectToPage(Url.Content("./Index"));
            if (!string.IsNullOrEmpty(AppUser.DbId))
                Teacher = await _repos.GetByIdAsync(int.Parse(AppUser.DbId));

            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (AppUser == null) return RedirectToPage(Url.Content("./Index"));

            if (!(await _service.DeleteUserAsync(AppUser.Id)))
                return this.RedirectToPage(Url.Content("~/Error"), new { errorMessage = "Ошибка, невозможно удалить единственного администратора" });

            return RedirectToPage(Url.Content("./Index"));
        }
    }
}