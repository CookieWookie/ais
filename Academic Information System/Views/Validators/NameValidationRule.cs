using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace AiS.Views.Validators
{
    public class NameValidationRule : ValidationRule
    {
        public NameValidationRule()
        {
        }

        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            if (value == null)
            {
                return new ValidationResult(false, "Hodnota nie je platná.");
            }
            string s = (string)value;
            if (string.IsNullOrEmpty(s))
            {
                return new ValidationResult(false, "Hodnota nie je platná.");
            }
            return new ValidationResult(true, "");
        }
    }
}
