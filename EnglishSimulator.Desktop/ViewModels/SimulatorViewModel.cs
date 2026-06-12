namespace EnglishSimulator.Desktop.ViewModels
{
	public class SimulatorViewModel(IMessageBoxService messageBoxService) : ViewModel(messageBoxService), ITransientDependency
	{
		#region Свойства


		#endregion

		public override async Task InitializeViewModelAsync()
		{
			Caption = "SIMULATOR";
		}
	}
}
