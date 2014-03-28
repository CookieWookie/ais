using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using AiS.Models;

namespace AiS.Views.Converters
{
    [ValueConversion(typeof(StudyProgramme), typeof(string))]
    public class StudyProgrammeConverter : IValueConverter
    {
        public StudyProgrammeConverter()
        {
        }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            StudyProgramme sp = value as StudyProgramme;
            return sp.Name;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
