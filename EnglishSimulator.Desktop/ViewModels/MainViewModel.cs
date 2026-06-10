using EnglishSimulator.Desktop.Views.Pages;

namespace EnglishSimulator.Desktop.ViewModels
{
    internal class MainViewModel : ViewModel
    {
		#region Свойства

		#region Заголовок страницы

		/// <summary>
		/// Заголовок страницы
		/// </summary>
		private string? _Caption;

		/// <summary>
		/// Заголовок страницы
		/// </summary>
		public string? Caption
		{
			get => _Caption;
			set => Set(ref _Caption, value);
		}

		#endregion

		#region Содержание страницы

		/// <summary>
		/// Содержание страницы
		/// </summary>
		private ViewModel? _CurrentChildPage;

		/// <summary>
		/// Содержание страницы
		/// </summary>
		public ViewModel? CurrentChildPage
		{
			get => _CurrentChildPage;
			set => Set(ref _CurrentChildPage, value);
		} 

		#endregion

		#endregion

		#region Команды

		#endregion

		public MainViewModel(IServiceProvider serviceProvider)
		{
			CurrentChildPage = serviceProvider.GetRequiredService<DeckViewModel>();
		}
	}
}
