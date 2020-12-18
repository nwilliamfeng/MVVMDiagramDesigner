using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows;
using System.Windows.Media;
 

namespace DemoApp
{
    
    public class DesignerElementConverter : IValueConverter
    {


        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var attrs = System.ComponentModel.TypeDescriptor.GetAttributes(value.GetType());
            var attr = attrs.OfType<DescriptionAttribute>().FirstOrDefault();
            if (attr == null)
                return DependencyProperty.UnsetValue;
            return attr.Description;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
