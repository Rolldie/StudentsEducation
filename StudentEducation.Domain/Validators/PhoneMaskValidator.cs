using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text;

namespace StudentsEducation.Domain.Validators
{
    public class PhoneMaskValidator:ValidationAttribute
    {
        private readonly string _phoneMask;

        public PhoneMaskValidator(string phoneMask)
        {
            _phoneMask = phoneMask;
        }
        public override bool IsValid(object value)
        {
            var phoneNumber = (string)value;
            bool result = true;
            if(this._phoneMask!=null)
            {
                result = IsMatch(_phoneMask, phoneNumber);
            }
            return result;
        }
        internal bool IsMatch(string mask, string phoneNumber)
        {
            if (mask.Length != phoneNumber.Trim().Length) return false;
            for (int i = 0; i < mask.Length; i++)
            {
                if (mask[i] == 'd' && char.IsDigit(phoneNumber[i]) == false)
                {
                    // Digit expected at this position.
                    return false;
                }
                if (mask[i] == '-' && phoneNumber[i] != '-')
                {
                    // Spacing character expected at this position.
                    return false;
                }
            }
            return true;
        }
        public override string FormatErrorMessage(string name)
        {
            return String.Format(CultureInfo.CurrentCulture, ErrorMessageString, name, _phoneMask);
        }
    }
}
