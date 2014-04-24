using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace AiS.Views.Validators
{
    public class SemesterValidationRule : ValidationRule
    {
        public SemesterValidationRule()
        {
        }

        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            string x = value as string;
            int number;
            bool b = Int32.TryParse(x, out number);
            if (b == null || !b)
            {
                return new ValidationResult(false, "Zadaná hodnota musí byť číslo.");
            }

            if (number < 1 || number > 12)
            {
                return new ValidationResult(false, "Dĺžka štúdia môže byť v rozsahu od 1-12 semestrov.");
            }

            return new ValidationResult(true, "");
        }
    }
}
