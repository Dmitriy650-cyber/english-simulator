namespace EnglishSimulator.Desktop.ViewModels
{
	public class HelpViewModel(IMessageBoxService messageBoxService, INavigationService navigationService) : ViewModel(messageBoxService), ITransientDependency
	{
		public override async Task InitializeViewModelAsync()
		{
			Caption = "HELP";
		}

		#region Команды

		/// <summary>
		/// Вренуться на страницу колод
		/// </summary>
		public ICommand? GoBackCommand => new LambdaCommand(() =>
		{
			navigationService.NavigateTo(nameof(DeckPage), null!);
		});

		#endregion
	}
}
