namespace EnglishSimulator.Desktop.ViewModels
{
	public class HelpViewModel(IMessageBoxService messageBoxService) : ViewModel(messageBoxService), ITransientDependency
	{
		public override async Task OnInitializedViewModel()
		{
			Caption = "HELP";
		}
	}
}
