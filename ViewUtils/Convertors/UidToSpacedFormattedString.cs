using System;
using System.Windows.Data;

namespace ViewUtils.Convertors
{
  public class UidToSpacedFormattedString : IValueConverter
  {
    public object Convert(object values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
      var result = string.Empty;
      if (values == null)
        return string.Empty;

      var chunkSize = ChunkSize;
      var number    = values.ToString();
     
      var stringLength = number.Length;
      for (var i = 0; i < stringLength; i += chunkSize)
      {
        if (i + chunkSize > stringLength) chunkSize = stringLength - i;

        result += number.Substring(i, chunkSize) + " ";
      }

      return result;
    }

    public object ConvertBack(object value, Type targetTypes, object parameter, System.Globalization.CultureInfo culture)
    {
      throw new NotImplementedException();
    }

    private const int ChunkSize = 4;
  }
}
