using System;
using System.Globalization;
using System.Windows.Data;

namespace ViewUtils.Convertors
{
  public class LongToDateTime : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      if (value == null) return null;
      var newvalue = (long)value;
      return newvalue < MinDateTime ? DateTime.Now.Ticks.ToString(Format) : new DateTime(newvalue).ToString(Format);
    }
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      var time = (DateTime?)value;
      return time?.Ticks;
    }

    private const string Format = "hh:mm:ss dd.MM.yy";
    private static readonly long MinDateTime = DateTime.MinValue.Ticks;
  }
}
