namespace EnglishSimulator.Desktop.ViewModels
{
	public class SimulatorViewModel(
		IMessageBoxService messageBoxService) : ViewModel(messageBoxService), ITransientDependency
	{
		#region Свойства

		/// <summary>
		/// Текущая колода
		/// </summary>
		public Deck? Deck
		{
			get => field;
			set => Set(ref field, value);
		}

		#endregion

		public override async Task InitializeViewModelAsync()
		{
			Caption = "SIMULATOR";
		}
	}
}
