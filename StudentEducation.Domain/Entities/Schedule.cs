
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StudentsEducation.Domain.Entities
{
    public class Schedule:BaseEntity
    {



        //references

        [Required] 
        public virtual  Group Group { get; set; }

        [Required]
        public virtual Subject Subject { get; set; }

        [Required]
        public virtual Teacher Teacher { get; set; }


        public virtual IEnumerable<Skip> Skips { get; set; }
    }
}
