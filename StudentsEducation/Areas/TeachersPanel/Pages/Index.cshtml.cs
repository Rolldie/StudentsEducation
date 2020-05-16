using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Razor;
using StudentsEducation.Domain.Entities;
using StudentsEducation.Domain.Interfaces;
using StudentsEducation.Infrastructure.Services;

namespace StudentsEducation.Web.Areas.TeachersPanel.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IdentityService _service;
        private readonly ITeachersAndScheduleSerivce _teachService;
        private readonly ICathedrasAndGroupsService _cathService;
        public IndexModel(ICathedrasAndGroupsService cathService,IdentityService service,ITeachersAndScheduleSerivce teacherService)
        {
            _cathService = cathService;
            _service = service;
            _teachService = teacherService;
        }
        
        public class ShowModel
        {
            public int Id { get; set; }
            public string Text { get; set; }
        }


        [BindProperty]
        public int GroupId { get; set; }
        [BindProperty]
        public int CathedraId { get; set; }
        [BindProperty]
        public Teacher Teacher { get; set; }
        [BindProperty]
        public int TeacherId { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var user =await  _service.GetCurrentUser(HttpContext);
            if (string.IsNullOrEmpty(user.DbId)) 
                return RedirectToPage(Url.Content("~/Error"), new { ErrorMessage = "Ошибка, нет пользователя в Базе данных!" }); 
            int id = int.Parse(user.DbId);
            Teacher = await _teachService.GetTeacherAsync(id);
            if(Teacher==null) 
                return RedirectToPage(Url.Content("~/Error"), new { ErrorMessage = "Ошибка, нет пользователя в Базе данных!" });
            ViewData["Teacher"] = Teacher.Name;
            ViewData["Groups"]= new SelectList((await _teachService.GetTeachersGroups(Teacher.Id)).Select(e=>new ShowModel() 
            { Id = e.Id, Text = e.Name + " " + e.StartEducationDate.ToShortDateString() + "-" + e.EndEducationDate.ToShortDateString() }) ,"Id","Text");

            ViewData["Cathedras"] = new SelectList(await _cathService.GetCathedrasAsync(), "Id", "Name");
            TeacherId = Teacher.Id;
            return Page();
        }
    }
}