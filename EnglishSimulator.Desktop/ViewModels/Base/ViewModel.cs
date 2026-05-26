namespace EnglishSimulator.Desktop.ViewModels.Base
{
	internal class ViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler? PropertyChanged;

		protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
		{
			if (Equals(field, value)) return false;
			field = value;
			OnPropertyChanged(propertyName);

			return true;
		}
	}
}
