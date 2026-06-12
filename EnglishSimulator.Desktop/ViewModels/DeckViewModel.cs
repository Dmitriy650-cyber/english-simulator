namespace EnglishSimulator.Desktop.ViewModels
{
	public class DeckViewModel(IMessageBoxService messageBoxService) : ViewModel(messageBoxService), ITransientDependency
	{
		public override async Task OnInitializedViewModel()
		{
			Caption = "DECKS";
		}
	}
}
