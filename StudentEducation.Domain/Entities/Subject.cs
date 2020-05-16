using StudentsEducation.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentsEducation.Domain.Entities
{
    [Display(Name = "Предмет")]
    public class Subject:BaseEntity, ITypedByControl
    {
        [Required(ErrorMessage = "Это поле является необходимым!")]
        [StringLength(200)]
        [Display(Name = "Название предмета")]
        public string Name { get; set; }



        //references
        public int ControlTypeId { get; set; }
        public virtual ControlType ControlType { get; set; }
        public virtual IEnumerable<Work> Works { get; set; }
        public virtual IEnumerable<Schedule> Schedules { get; set; }
        public virtual IEnumerable<FinalControl> FinalControls { get; set; }


        //interface methods to use in MarkValidation
        public ControlType GetControlType()
        {
            return ControlType;
        }
    }
}
