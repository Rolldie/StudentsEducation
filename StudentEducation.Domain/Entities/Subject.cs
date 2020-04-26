using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StudentsEducation.Domain.Entities
{
    public class Subject:BaseEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int CourseNumber { get; set; }



        //references
        [Required]
        public virtual ControlType ControlType { get; set; }
        public virtual IEnumerable<Work> Works { get; set; }
        public virtual IEnumerable<Schedule> Schedules { get; set; }

    }
}
