using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Globalization;

namespace AiS.Views.Validators
{
    public class ExamTimeValidationRule : ValidationRule
    {
        public ExamTimeValidationRule()
        {
        }

        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            DateTime dt;
            bool b = DateTime.TryParseExact(value as string, new[] { "HH:mm", "H:mm" }, cultureInfo, DateTimeStyles.None, out dt);
            if (value == null || !b)
            {
                return new ValidationResult(false, "Hodnota nie je platná.");
            }
            TimeSpan t = dt.TimeOfDay;
            TimeSpan low = new TimeSpan(7, 30, 0);
            TimeSpan up = new TimeSpan(16, 0, 0);
            if (t < low || up < t)
            {
                return new ValidationResult(false, "Termín začiatku skúšky môže byť medzi 7:30 a 16:00.");
            }
            return new ValidationResult(true, "");
        }
    }
}
