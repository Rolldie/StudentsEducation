using StudentsEducation.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StudentsEducation.Domain.Entities
{
    public class Work:BaseEntity, ITypedByControl
    {
        [Required]
        public string Name { get; set; }

        //references
        [Required]
        public virtual Subject Subject { get; set; }
        public virtual ControlType ControlType { get; set; }



        //interface method to use MarkValidation in subentities
        public ControlType GetControlType()
        {
            return ControlType;
        }
    }
}
