using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StudentsEducation.Domain.Entities
{
    public class WorksSchedule:BaseEntity
    {
        [Required]
        [DataType(DataType.Date)]
        public DateTime Start { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime End { get; set; }
        

        public virtual Work Work { get; set; }
        public int WorkId { get; set; }

        public virtual  Schedule Schdeule { get; set; }
        public int ScheduleId { get; set; }
    }
}
