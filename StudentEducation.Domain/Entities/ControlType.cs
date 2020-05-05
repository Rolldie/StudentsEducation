using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StudentsEducation.Domain.Entities
{
    public class ControlType:BaseEntity
    {
        [Required]
        [StringLength(30)]
        public string ControlName { get; set; }
        [Required]
        [StringLength(7)]
        public string ValueDifference { get; set; }

       
        
        //Reference
        public virtual IEnumerable<Work> Works { get; set; }
        public virtual IEnumerable<Subject> Subjects { get; set; }
    }
}
