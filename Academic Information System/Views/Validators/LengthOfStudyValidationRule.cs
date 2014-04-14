using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Globalization;

namespace AiS.Views.Validators
{
    public class LengthOfStudyValidationRule : ValidationRule
    {
        public LengthOfStudyValidationRule()
        {
        }

        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            DateTime dt;
            bool b = DateTime.TryParseExact(value as string, new[] { "Rok" }, cultureInfo, DateTimeStyles.None, out dt);
            if (value == null || !b)
            {
                return new ValidationResult(false, "Hodnota nie je platná.");
            }
            TimeSpan t = dt.TimeOfDay;
            TimeSpan low = new TimeSpan(3);
            TimeSpan up = new TimeSpan(6);
            if (t < low || up < t)
            {
                return new ValidationResult(false, "Dĺžka štúdia musí byť od 3 rokov až po 6 rokov.");
            }
            return new ValidationResult(true, "");
        }
    }
}
