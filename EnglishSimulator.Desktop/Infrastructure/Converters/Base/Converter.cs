namespace EnglishSimulator.Desktop.Infrastructure.Converters.Base
{
	internal abstract class Convertor : IValueConverter
	{
		public abstract object Convert(
			object value,
			Type targetType,
			object parameter,
			CultureInfo culture);

		public virtual object ConvertBack(
			object value,
			Type targetType,
			object parameter,
			CultureInfo culture) => throw new NotSupportedException();
	}
}
