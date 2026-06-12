namespace EnglishSimulator.Desktop.ViewModels
{
    public class MainViewModel : ViewModel, ITransientDependency
    {
		#region Свойства

		/// <summary>
		/// Содержание страницы
		/// </summary>
		public ViewModel? CurrentChildPage
		{
			get => field;
			set => Set(ref field, value);
		}

		#endregion

		#region Команды

		#endregion

		public MainViewModel(IServiceProvider serviceProvider)
		{
			CurrentChildPage = serviceProvider.GetRequiredService<DeckViewModel>();
		}
	}
}
