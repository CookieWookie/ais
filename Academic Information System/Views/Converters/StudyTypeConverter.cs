using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using AiS.Models;

namespace AiS.Views.Converters
{
    [ValueConversion(typeof(StudyType), typeof(string))]
    public class StudyTypeConverter : IValueConverter
    {
        private static readonly Dictionary<StudyType, string> display = new Dictionary<StudyType, string>();

        public StudyTypeConverter()
        {
        }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            StudyType st = (StudyType)value;
            string result;
            if (!display.TryGetValue(st, out result))
            {
                result = st.GetDescription();
                display[st] = result;
            }
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
