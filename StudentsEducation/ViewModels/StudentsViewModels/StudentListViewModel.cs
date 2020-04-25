using StudentsEducation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentsEducation.Web.ViewModels.StudentsViewModels
{
    public class StudentListViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        public Group Group { get; set; }

        //Will be added sun
    }
}
