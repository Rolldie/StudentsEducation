using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StudentsEducation.Domain.Entities
{
    public class Teacher:BaseEntity
    {
        [Required]
        public string Name { get; set; }


        //references
        public virtual IEnumerable<Schedule> Schedules { get; set; }

    }
}
