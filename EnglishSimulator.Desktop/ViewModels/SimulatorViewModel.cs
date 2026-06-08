namespace EnglishSimulator.Desktop.ViewModels
{
	internal class SimulatorViewModel : ViewModel
	{
		#region Свойства

		#region Заголовок страницы

		/// <summary>
		/// Заголовок страницы
		/// </summary>
		private string? _Caption = "SIMULATOR";

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
