
using FoolProof.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StudentsEducation.Domain.Entities
{
    public class Schedule:BaseEntity
    {
        [Required]
        [DataType(DataType.Date)]
        public DateTime StartsIn { get; set; }
        [Required]
        [GreaterThan("StartsFrom")]
        [DataType(DataType.Date)]
        public DateTime EndsIn { get; set; }


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
