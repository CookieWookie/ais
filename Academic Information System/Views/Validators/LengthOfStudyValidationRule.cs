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
            string x = value as string;
            int number;
            bool b = Int32.TryParse(x, out number);
            if (b == null || !b)
            {
                return new ValidationResult(false, "Zadaná hodnota musí byť číslo.");
            }

            if (number < 3 || number > 6)
            {
                return new ValidationResult(false, "Dĺžka štúdia môže byť v rozsahu od 3-6 rokov.");
            }
            
                return new ValidationResult(true, "");



        }
    }
}
