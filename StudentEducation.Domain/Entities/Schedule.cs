
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StudentsEducation.Domain.Entities
{
    public class Schedule:BaseEntity
    {


        [Required] 
        public virtual  Group Group { get; set; }

        [Required]
        public virtual Subject Subject { get; set; }

        [Required]
        public virtual Teacher Teacher { get; set; }
    }
}
