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
        [Required]
        public string RoleSelect { get; set; }

        public SelectList Roles { get; set; }
        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }



            [Required]
            [Display(Name="Login")]
            [StringLength(30)]
            public string UserName { get; set; }

            public Role Role { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
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
                   // _service.
                    /*                  
                                        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                                        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                                        var callbackUrl = Url.Page(
                                            "/Account/ConfirmEmail",
                                            pageHandler: null,
                                            values: new { area = "Identity", userId = user.Id, code = code },
                                            protocol: Request.Scheme);

                                        await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                                            $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");
                                     */
                    /* if (_userManager.Options.SignIn.RequireConfirmedAccount)
                      {
                          return RedirectToPage("RegisterConfirmation", new { email = Input.Email });
                      }
                      else
                      {
                          await _signInManager.SignInAsync(user, isPersistent: false);*/

                    return RedirectToPage("./" + Input.Role.Name);
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
