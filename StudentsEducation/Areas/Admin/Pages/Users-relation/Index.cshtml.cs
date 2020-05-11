using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentsEducation.Infrastructure.Identity.Data;
using StudentsEducation.Infrastructure.Services;

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
            Users = await _service.GetUsersAsync();
            OutUsers = Users.Select(e => new OutputUser
            {
                Id=e.Id,
                UserName = e.UserName,
                DbId=e.DbId!=null?int.Parse(e.DbId):-1,
                Role = _service.GetRolesByUser(e).Result.FirstOrDefault(),
                Email = e.Email
            });
        }
    }
}