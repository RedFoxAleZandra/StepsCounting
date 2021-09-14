using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace StepsCounting
{
    public class StepsBackgroundConverter : IValueConverter
    {
        public int MinimumSteps{ get; set; }

        public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {

            bool input = (int)value >= MinimumSteps;
            switch (input)
            {
                case true:
                    return Brushes.LightGreen;
                default:
                    return DependencyProperty.UnsetValue;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            return null;
        }

    }
}
