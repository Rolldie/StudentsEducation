using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StudentsEducation.Domain.Entities
{
    public class Work:BaseEntity
    {
        [Required]
        public string Name { get; set; }

        //references
        [Required]
        public virtual Subject Subject { get; set; }
        [Required]
        public virtual WorkControlType WorkControlType { get; set; }
    }
}
