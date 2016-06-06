using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace NoteApplication.Converters
{
	public class DateTimeToDateTimeOffsetConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, string language)
		{
			try
			{
				DateTime date = (DateTime) value;
				return new DateTimeOffset(date);
			}
			catch (Exception)
			{
				return DateTimeOffset.MinValue;
			}

		}

		public object ConvertBack(object value, Type targetType, object parameter, string language)
		{
			try
			{
				DateTimeOffset dateTimeOffset = (DateTimeOffset)value;
				return dateTimeOffset.DateTime;
			}
			catch (Exception)
			{
				return DateTime.MinValue;
			}
		}
	}
}
