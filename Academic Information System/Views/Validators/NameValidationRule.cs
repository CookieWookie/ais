﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace AiS.Views.Validators
{
    public class NameValidationRule : ValidationRule
    {
        public NameValidationRule()
        {
        }

        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            string s = value as string;
            if (string.IsNullOrWhiteSpace(s))
            {
                return new ValidationResult(false, "Hodnota nie je platná.");
            }
            return new ValidationResult(true, "");
        }
    }
}
