using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentsEducation.Domain.Entities;
using StudentsEducation.Infrastructure.Services;

namespace StudentsEducation.Web.Areas.Admin.Pages.Students
{
    public class IndexModel : PageModel
    {
        private readonly StudentsService studentService;
        public IndexModel(StudentsService service)
        {
            studentService = service;
        }

        public IEnumerable<Student> Students { get; set; }


        public void OnGet()
        {
            Students = studentService.GetStudentsAsync().Result;
        }
    }
}