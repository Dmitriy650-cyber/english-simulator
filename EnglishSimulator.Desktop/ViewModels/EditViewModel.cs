namespace EnglishSimulator.Desktop.ViewModels
{
	internal class EditViewModel : ViewModel
	{
		#region Свойства

		#region Заголовок страницы

		/// <summary>
		/// Заголовок страницы
		/// </summary>
		private string? _Caption = "EDITOR";

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
