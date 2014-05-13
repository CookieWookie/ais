using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace AiS.Views.Validators
{
    public class NumberValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            int number;
            if (value == null || !int.TryParse(value.ToString(), out number))
            {
                return new ValidationResult(false, "Hodnota nie je číslo.");
            }
            return new ValidationResult(true, "");
        }
    }
}
