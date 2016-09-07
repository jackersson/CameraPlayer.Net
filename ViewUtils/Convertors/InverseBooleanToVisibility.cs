using System;
using System.Globalization;
using System.Windows.Data;

namespace ViewUtils.Convertors
{
  public class InverseBooleanToVisibility : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      if (targetType != typeof (bool))
        return false;

      return !(bool)value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      throw new NotSupportedException();
    }

  }
}
