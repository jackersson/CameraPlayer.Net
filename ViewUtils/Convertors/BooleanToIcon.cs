using System;
using System.Globalization;
using System.Windows.Data;
using ApplicationResources;

namespace ViewUtils.Convertors
{
  public class BooleanToIcon : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      if (targetType != typeof(bool))
        return Loader.ErrorIcon;

      var target = (bool) value;
      return target ? Loader.OkIcon : Loader.ErrorIcon;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      throw new NotSupportedException();
    }
  }
}
