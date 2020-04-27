using StudentsEducation.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StudentsEducation.Domain.Validators
{

    //To use this validator, class must be typed by controlType (use interface), this is also very bad decision
    public class MarkValidation : ValidationAttribute
    {
        string anotherProperty;
        double markValue;
        public MarkValidation(string dependendproperty)
        {
            anotherProperty = dependendproperty;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            markValue = (double)value;

            var prop = validationContext.ObjectType.GetProperty(anotherProperty);
            if (prop == null) return new ValidationResult($"Не было найденто свойство: {anotherProperty}");

            var typedObj = prop.GetValue(validationContext.ObjectInstance);
            var objWithTypedControl = (ITypedByControl)typedObj;
            string MarkType = objWithTypedControl.GetControlType().ControlName;
            if (MarkType == "Зачет" || MarkType == "Залік")
            {
                if (markValue > 1 || markValue < 0) return new ValidationResult("Для этой работы необходимо установить 1, если зачет и 0 если нет!");
                else return null;
            }
            else
            {
                var marks = objWithTypedControl.GetControlType().ValueDifference.Split("-");
                var left = Convert.ToInt32(marks[0]);
                var right = Convert.ToInt32(marks[1]);
                if (markValue >= left && markValue <= right) return null;
                else return new ValidationResult($"Значение должно быть между {left} и {right}");
            }
        }
    }
}
