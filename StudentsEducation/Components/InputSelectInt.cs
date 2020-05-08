using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using StudentsEducation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace StudentsEducation.Web.Components
{
    public class InputSelectInt<T> : InputSelect<T>
    {

        protected override bool TryParseValueFromString(string value, out T result, out string validationErrorMessage)
        {
            if (typeof(T) == typeof(int))
            {
                if (int.TryParse(value, out var resultInt))
                {
                    result = (T)(object)resultInt;
                    validationErrorMessage = null;
                    return true;
                }
                else
                {
                    result = default;
                    validationErrorMessage = "The chosen value is not a valid number.";
                    return false;
                }
            }
            else
            {
                return base.TryParseValueFromString(value, out result, out validationErrorMessage);
            }
        }
    }
}
