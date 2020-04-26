using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StudentsEducation.Domain.Entities
{
    /*TODO:This is wrong, can be optimized */
   public class WorkControlType:BaseEntity
    {
        [Required]
        [StringLength(30)]
        public string ControlName { get; set; }
        [Required]
        [StringLength(10)]
        public string ValueDifference { get; set; }

        public virtual IEnumerable<Work> Works { get; set; }
    }
}
