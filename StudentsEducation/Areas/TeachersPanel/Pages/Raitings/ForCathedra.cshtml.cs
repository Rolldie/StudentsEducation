using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentsEducation.Domain.Entities;
using StudentsEducation.Domain.Interfaces;

namespace StudentsEducation.Web.Areas.TeachersPanel.Pages.Raitings
{
    public class ForCathedraModel : PageModel
    {
        private readonly ICathedrasAndGroupsService _cathService;
        public ForCathedraModel(ICathedrasAndGroupsService cathService)
        {
            _cathService = cathService;
        }
        [BindProperty(SupportsGet = true)]
        [DataType(DataType.Date)]
        public DateTime DateStart { get; set; }
        [BindProperty(SupportsGet =true)]
        [DataType(DataType.Date)]
        public DateTime DateEnd { get; set; }
        [BindProperty(SupportsGet =true)]
        public int CathedraId { get; set; }


        public  Cathedra Cathedra { get; set; }

        public async Task<IActionResult>OnGetAsync(int?CathedraId)
        {
            if (!CathedraId.HasValue) return NotFound();
            if (DateStart == DateTime.MinValue && DateEnd == DateTime.MinValue)
            {
                DateStart = DateTime.Now.AddMonths(-3);
                DateEnd = DateTime.Now.AddMonths(+3);
            }
            Cathedra = await _cathService.GetCathedraByIdAsync(CathedraId.Value);
            return Page();
        }

        public async Task<double> GetGroupAcademic(int groupId)
        {
            return await _cathService.GetGroupAcademicPerfomanceAsync(groupId,DateStart,DateEnd);
        }
    }
}