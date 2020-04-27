using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StudentsEducation.Domain.Entities
{
    public class Skip :BaseEntity
    {
        [Required]
        [StringLength(500)]
        public string Information { get; set; }


        //references
        public virtual Student Student { get; set; }
        [Required]
        public virtual Schedule Schedule { get; set; }
    }
}
