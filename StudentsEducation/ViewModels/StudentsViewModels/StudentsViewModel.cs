using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentsEducation.Web.ViewModels.StudentsViewModels
{
    public class StudentsViewModel
    {
        public IEnumerable<StudentListViewModel> StudentsList { get; set; }
    }
}
