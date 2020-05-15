﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentsEducation.Domain.Entities
{
    [Display(Name = "Тип контроля")]
    public class ControlType:BaseEntity
    {
        [Required(ErrorMessage = "Это поле является необходимым!")]
        [StringLength(30)]
        [Display(Name = "Название типа контроля")]
        public string ControlName { get; set; }
        [Required(ErrorMessage = "Это поле является необходимым!")]
        [Range(-1,5)]
        [Display(Name = "Нижняя граница")]
        public int LowValue { get; set; }
        [Required(ErrorMessage = "Это поле является необходимым!")]
        [Range(1,1000)]
        [Display(Name = "Верхняя граница")]
        public int HighValue { get; set; }
       
        
        //Reference
        public virtual IEnumerable<Work> Works { get; set; }
        public virtual IEnumerable<Subject> Subjects { get; set; }
    }
}
