﻿using StudentsEducation.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StudentsEducation.Domain.Entities
{
    public class Subject:BaseEntity, ITypedByControl
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int CourseNumber { get; set; }



        //references
        [Required]
        public virtual ControlType ControlType { get; set; }
        public virtual IEnumerable<Work> Works { get; set; }
        public virtual IEnumerable<Schedule> Schedules { get; set; }


        //interface methods to use in MarkValidation
        public ControlType GetControlType()
        {
            return ControlType;
        }
    }
}
