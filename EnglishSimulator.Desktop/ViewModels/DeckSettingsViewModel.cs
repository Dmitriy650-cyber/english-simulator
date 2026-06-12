namespace EnglishSimulator.Desktop.ViewModels
{
	public class DeckSettingsViewModel(IMessageBoxService messageBoxService) : ViewModel(messageBoxService), ITransientDependency
	{
		public override async Task InitializeViewModelAsync()
		{
			Caption = "DECK SETTINGS";
		}
	}
}
