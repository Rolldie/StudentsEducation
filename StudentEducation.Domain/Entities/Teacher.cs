using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StudentsEducation.Domain.Entities
{
    [Display(Name = "Учитель")]
    public class Teacher:BaseEntity
    {
        [Required]
        [StringLength(100)]
        [Display(Name = "ФИО учителя")]
        public string Name { get; set; }




        //references
        public virtual IEnumerable<Schedule> Schedules { get; set; }

    }
}
