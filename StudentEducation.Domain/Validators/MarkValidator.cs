using StudentsEducation.Domain.Entities;
using StudentsEducation.Domain.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace StudentsEducation.Domain.Validators
{

    //To use this validator, class must be typed by controlType (use interface), this is also very bad decision
    public class MarkValidation : ValidationAttribute
    {

        private readonly IAsyncRepository<Subject> _subjRep;
        private readonly IAsyncRepository<Work> _workRep;
        string anotherProperty;
        double markValue;
        public MarkValidation(string dependendproperty,IAsyncRepository<Subject> subjRepository)
        {
            _subjRep = subjRepository;
            anotherProperty = dependendproperty;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            markValue = (double)value;
            var prop = (int)validationContext.ObjectType.GetProperty(anotherProperty).GetValue(validationContext.ObjectInstance,null);
            var result=_subjRep.GetByIdAsync(prop);
            if (result == null) return new ValidationResult($"Не было найденто свойство: {anotherProperty}");
            var objWithTypedControl = (ITypedByControl)result;
            string MarkType = objWithTypedControl.GetControlType().ControlName;
            if (MarkType == "Зачет" || MarkType == "Залік")
            {
                if (markValue > 1 || markValue < 0) return new ValidationResult("Для этой работы необходимо установить 1, если зачет и 0 если нет!");
                else return null;
            }
            else
            {
                var marks = objWithTypedControl.GetControlType();
                var left = marks.LowValue;
                var right = marks.HighValue;
                if (markValue >= left && markValue <= right) return null;
                else return new ValidationResult($"Значение должно быть между {left} и {right}");
            }
           
        }
    }
}
