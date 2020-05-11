using StudentsEducation.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentsEducation.Domain.Entities
{
    [Display(Name = "Предмет")]
    public class Subject:BaseEntity, ITypedByControl
    {
        [Required]
        [StringLength(200)]
        [Display(Name = "Название предмета")]
        public string Name { get; set; }



        //references
        public virtual ControlType ControlType { get; set; }
        public virtual IEnumerable<Work> Works { get; set; }
        public virtual IEnumerable<Schedule> Schedules { get; set; }


        //interface methods to use in MarkValidation
        public ControlType GetControlType()
        {
            return ControlType;
        }
    }
}
