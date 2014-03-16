using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace AiS.Views.Validators
{
    public class DateTimeValidationRule : ValidationRule
    {
        public DateTimeValidationRule()
        {
        }

        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            throw new NotImplementedException();
        }
    }
}
