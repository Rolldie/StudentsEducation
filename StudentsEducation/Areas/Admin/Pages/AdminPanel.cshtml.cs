using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Castle.DynamicProxy.Contributors;
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
            PageAndName.Add("./Cathedras-Groups/Index", "Кафедра и группы////");
            PageAndName.Add("./ControlTypes/Index", "Типы контроля////");
            PageAndName.Add("./Subjects-Works/Index", "Предметы и их работы////");
            PageAndName.Add("./Teachers-Schedules/Index", "Преподаватели и их расписание///");
            PageAndName.Add("./Users-relation/Index", "Пользователи и отношение к базе данных");
            PageAndName.Add("./Roles/Index", "Роли пользователей////");
            PageAndName.Add("./Students/Index", "Студенты и их успеваемость");
        }
    }
}