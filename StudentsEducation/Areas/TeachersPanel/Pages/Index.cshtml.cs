using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace StudentsEducation.Web.Areas.TeachersPanel.Pages
{
    public class IndexModel : PageModel
    {


        public class InputGroup
        {
            public int GroupId { get; set; }
            public int GroupName { get; set; }
        }

        public class InputSubject
        {
            public int SubjId { get; set; }
            public string SubjName { get; set; }
        }


        public void OnGet()
        {

        }
    }
}