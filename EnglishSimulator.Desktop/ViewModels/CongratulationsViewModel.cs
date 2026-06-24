namespace EnglishSimulator.Desktop.ViewModels
{
	public class CongratulationsViewModel(
		IMessageBoxService messageBoxService,
		INavigationService navigationService) : ViewModel(messageBoxService), ITransientDependency
	{
		public override async Task InitializeViewModelAsync()
		{
			Caption = "CONGRATULATIONS";
		}

		#region Команды

		/// <summary>
		/// Вернуться на страницу колод
		/// </summary>
		public ICommand? CompletedCommand => new LambdaCommand(() =>
		{
			navigationService.NavigateTo(nameof(DeckPage), null!);
		});

		#endregion
	}
}
