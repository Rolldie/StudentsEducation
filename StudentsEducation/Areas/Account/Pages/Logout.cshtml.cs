using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using StudentsEducation.Infrastructure.Services;

namespace StudentsEducation.Web.Areas.Account.Pages
{
    [Authorize]
    public class LogoutModel : PageModel
    {

        private readonly IdentityService _service;
        private readonly ILogger<LogoutModel> _logger;
        public LogoutModel(IdentityService service, ILogger<LogoutModel> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl)
        {
           returnUrl = returnUrl ?? Url.Content("~/");
           await _service.LogoutAsync();
            _logger.LogInformation("User logged out.");
           return LocalRedirect(returnUrl);
        }
    }
}