namespace EnglishSimulator.Desktop.ViewModels
{
	public class CongratulationsViewModel(IMessageBoxService messageBoxService) : ViewModel(messageBoxService), ITransientDependency
	{
		public override async Task OnInitializedViewModel()
		{
			Caption = "CONGRATULATIONS";
		}
	}
}
