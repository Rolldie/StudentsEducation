﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentsEducation.Domain.Entities
{
    [Display(Name = "Преподаватель")]
    public class Teacher:BaseEntity
    {
        [Required]
        [StringLength(100)]
        [Display(Name = "ФИО преподавателя")]
        public string Name { get; set; }




        //references
        public virtual IEnumerable<Schedule> Schedules { get; set; }

    }
}
