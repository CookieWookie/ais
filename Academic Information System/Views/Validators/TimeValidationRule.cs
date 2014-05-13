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
            DateTime dt;
            if (value == null || !DateTime.TryParseExact(value.ToString(), new[] { "HH:mm" }, cultureInfo, System.Globalization.DateTimeStyles.AssumeLocal, out dt))
            {
                return new ValidationResult(false, "Zadaná hodnota nie je čas.");
            }
            return new ValidationResult(true, "");
        }
    }
}
