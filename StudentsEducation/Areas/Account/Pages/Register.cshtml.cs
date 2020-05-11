using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using StudentsEducation.Domain.Entities;
using StudentsEducation.Infrastructure.Identity.Data;
using StudentsEducation.Infrastructure.Services;

namespace StudentsEducation.Web.Areas.Account.Pages
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly IdentityService _service;
        private readonly ILogger<RegisterModel> _logger;

        public RegisterModel(IdentityService service,
            ILogger<RegisterModel> logger)
        {
            _service = service;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }
        [BindProperty]
        [Required(ErrorMessage ="Вы не выбрали роль пользователя!")]
        public string RoleSelect { get; set; }

        public SelectList Roles { get; set; }
        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage="Поле почты является необходимым!")]
            [EmailAddress(ErrorMessage ="Не правильно введена почта!")]
            [Display(Name = "Почтовы адрес Email")]
            public string Email { get; set; }



            [Required(ErrorMessage ="Поле имени пользователя является обязательным!")]
            [Display(Name="Имя пользователя")]
            [StringLength(30)]
            public string UserName { get; set; }

            public Role Role { get; set; }

            [Required(ErrorMessage ="Вы не ввели пароль!")]
            [StringLength(100, ErrorMessage = "{0} должен быть как минимум {2} и как максимум {1} символов длиной.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Пароль")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Подтвердите пароль")]
            [Compare("Password", ErrorMessage = "Пароли не совпадают.")]
            public string ConfirmPassword { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            await InitRoles();
        }
        public async Task InitRoles()
        {
            Roles = new SelectList((await _service.GetRolesAsync()), "Id", "Name",RoleSelect);
        }
        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                Input.Role = await _service.GetRoleAsync(RoleSelect);
                var user = new AppUser { UserName = Input.UserName, Email = Input.Email };
                var result = await _service.RegisterUser(user, Input.Password);

                if (result.Succeeded)
                { 
                   _logger.LogInformation("User created a new account with password.");
                   await _service.AddUserToRoleAsync(user, Input.Role);

                   return RedirectToPage("./RoleIdentification",new { userId = user.Id });
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                
            }
            await InitRoles();
            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
