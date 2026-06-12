namespace EnglishSimulator.Desktop.ViewModels.Base
{
	public class ViewModel : INotifyPropertyChanged
	{
		/// <summary>
		/// Заголовок страницы
		/// </summary>
		public string? Caption
		{
			get => field;
			set => Set(ref field, value);
		}

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

		public virtual Task OnInitializedViewModel() => Task.CompletedTask;
	}
}
