namespace EnglishSimulator.Desktop.ViewModels
{
	internal class DeckViewModel : ViewModel
	{
		public ObservableCollection<string> TestItems { get; set; } = [
			"item 1",
			"item 2",
			"item 3"
			];

		#region Свойства

		#region Заголовок страницы

		/// <summary>
		/// Заголовок страницы
		/// </summary>
		private string? _Caption = "DECKS";

		/// <summary>
		/// Заголовок страницы
		/// </summary>
		public string? Caption
		{
			get => _Caption;
			set => Set(ref _Caption, value);
		}

		#endregion

		#endregion
	}
}
