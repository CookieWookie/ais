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

        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)// cervena ciarka :-(
        {
            string x = value as string;
            int number;
            bool a = Int32.TryParse(x, out number);
            if (a == null)
            {
                return new ValidationResult(false, "Zadaná hodnota musí byť číslo .");// zmen co nam napise ked tak

            }
            if (a == true && (number < 3 || number > 6))
            {
                return new ValidationResult(false, "Zadaná hodnota musí byť číslo>=3 alebo <=6 .");// zmen to co nam napise ked tak
 
            }
            if (a == true && (number >= 3 || number <= 6))//ked tak podmienka do pice
            {
                return new ValidationResult(true, "");

            }

        }
    }
}
