using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentsEducation.Domain.Entities;
using StudentsEducation.Domain.Interfaces;

namespace StudentsEducation.Web.Areas.Admin.Pages.Teachers_Schedules
{
    public class ScheduleModel : PageModel
    {
        private readonly ITeachersAndScheduleSerivce _service;
        public ScheduleModel(ITeachersAndScheduleSerivce service)
        {
            _service = service;
        }
        public IEnumerable<Schedule> Schedules { get; set; }
        public IEnumerable<Schedule> NewSchedules { get; set; }
        public IEnumerable<Schedule> CurrentSchedules { get; set; }
        public IEnumerable<Schedule> PrevSchedules { get; set; }

        private async Task InitializeSchedules(int id)
        {
            Schedules = await _service.GetSchedulesByTeacherAsync(id);
            NewSchedules = Schedules.Where(e => e.EndsIn < DateTime.Now.Date);
            PrevSchedules = Schedules.Where(e => e.StartsIn > DateTime.Now.Date);
            CurrentSchedules = Schedules.Where(e => e.StartsIn.Date <= DateTime.Now.Date && e.EndsIn.Date >= DateTime.Now.Date);
        }


        public async Task<IActionResult> OnGetAsync(int ?id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }
            else
            {
                await InitializeSchedules(id.Value); 
                return Page();
            }
        }

    }
}