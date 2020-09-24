using StudentsEducation.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentsEducation.Domain.Entities
{
    [Display(Name = "Предмет")]
    public class Subject:BaseEntity
    {
        [Required(ErrorMessage = "Это поле является необходимым!")]
        [StringLength(200)]
        [Display(Name = "Название предмета")]
        public string Name { get; set; }


        public int ControlTypeId { get; set; }
        public virtual ControlType ControlType { get; set; }


        public virtual IEnumerable<Schedule> Schedules { get; set; }
        public virtual IEnumerable<Work> Works { get; set; }

    }
}
