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
            DateTime dt;
            if (value == null || !DateTime.TryParseExact(value as string, new[] { "dd.MM.yyyy", "d.M.yyyy" }, cultureInfo, DateTimeStyles.AssumeLocal, out dt))
            {
                return new ValidationResult(false, "Zadaná hodnota nie je dátum.");
            }
            return new ValidationResult(true, "");
        }
    }
}
