using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace StudentsEducation.Web.Areas.Admin.Pages
{
    public class AdminPanelModel : PageModel
    {
        public Dictionary<string, string> PageAndName { get; set; }
        public void OnGet()
        {
            PageAndName = new Dictionary<string, string>();
            PageAndName.Add("./Cathedras-Groups/Index", "Кафедра и группы");
            PageAndName.Add("./ControlTypes/Index", "Типы контроля");
            PageAndName.Add("./Subjects/Index", "Предметы");
            PageAndName.Add("./Schedules/Index", "Расписание преподавателей");
            PageAndName.Add("./Users/Index", "Пользователи");
            PageAndName.Add("./Roles/Index", "Роли пользователей");
            PageAndName.Add("./Students/Index", "Студенты");

        }
    }
}