using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Globalization;

namespace AiS.Views.Validators
{
    public class DateValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null || !(value is DateTime))
            {
                return new ValidationResult(false, "Zadaná hodnota nie je platný dátum.");
            }
            return new ValidationResult(true, "");
        }
    }
}
