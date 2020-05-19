using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentsEducation.Domain.Entities;
using StudentsEducation.Domain.Interfaces;
using StudentsEducation.Web.Areas.Account.Services;

namespace StudentsEducation.Web.Areas.Account.Pages
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    public class RoleIdentificationModel : PageModel
    {
        private readonly IdentityService _service;
        private readonly IAsyncRepository<Teacher> _teacherRep;
        public RoleIdentificationModel(IdentityService service, IAsyncRepository<Teacher> repository)
        {
            _service = service;
            _teacherRep = repository;
        }

        public string ReturnUrl{get;set;}
        //can be improved by using reflection or smth else
        [BindProperty]
        public Teacher InpTeacher { get; set; }
        public Teacher OutpTeacher { get; set; }
        public async Task<IActionResult>  OnGetAsync(string userId)
        {
             ReturnUrl= Url.Content("~/Index");
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToPage(ReturnUrl);
            }
            var user = await _service.GetUserAsync(userId);

            if (user == null) return RedirectToPage(ReturnUrl);

            var role = await _service.GetRoleByUserAsync(user);

            
            //it means teacher//
            if (role.IsDatabaseFieldsRequired)
            {
                Role = $"Роль {role.Name}, для этой учетной записи нужно заполнить дополнительные поля!";

                InpTeacher = await _teacherRep.CreateAsync(new Teacher() { Name = "---" });
                user.DbId = InpTeacher.Id.ToString();
                await _service.UpdateUserAsync(user);

                return Page();
            }
            else return RedirectToPage(ReturnUrl);
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                await _teacherRep.UpdateAsync(InpTeacher);
                return RedirectToPage(ReturnUrl);
            }
            else
            {
                return Page();
            }
        }



        public string Role { get; set; }
    }
}