using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Globalization;

namespace AiS.Views.Validators
{
    public class ExamDateValidationRule : ValidationRule
    {
        public ExamDateValidationRule()
        {
        }

        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            string x = value as string;
            DateTime date;
            bool b = DateTime.TryParseExact(x, new[] { "dd.MM.yyyy", "d.M.yyyy" }, cultureInfo, DateTimeStyles.None, out date);
            if (b == null || !b)
            {
                return new ValidationResult(false, "Zadaná hodnota nie je platná");
            }
            DateTime today = DateTime.Now.AddDays;

            if (date >= today)
            {
                return new ValidationResult(false, "Na skusku sa da prihlasit najskor 3 dni pred jej začiatkom");
            }

            return new ValidationResult(true, "");
        }
    }
}
