namespace EnglishSimulator.Desktop.ViewModels
{
	public class EditViewModel(IMessageBoxService messageBoxService) : ViewModel(messageBoxService), ITransientDependency
	{
		public override async Task OnInitializedViewModel()
		{
			Caption = "EDITOR";
		}
	}
}
