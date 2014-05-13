using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace AiS.Views.Validators
{
    public class TimeValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            TimeSpan ts;
            if (value == null || !TimeSpan.TryParseExact(value.ToString(), new[] { "HH:mm", "H:m" }, cultureInfo, out ts))
            {
                return new ValidationResult(false, "Zadaná hodnota nie je platný čas.");
            }
            return new ValidationResult(true, "");
        }
    }
}
