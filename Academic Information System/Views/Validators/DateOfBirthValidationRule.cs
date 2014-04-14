using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Globalization;

namespace AiS.Views.Validators
{
    public class DateOfBirthValidationRule : ValidationRule
    {
        public DateOfBirthValidationRule()
        {
        }

        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            throw new NotImplementedException();
        }
    }
}
