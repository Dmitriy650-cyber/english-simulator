namespace EnglishSimulator.Desktop.ViewModels
{
	public class DeckViewModel(
		IMessageBoxService messageBoxService, 
		INavigationService navigationService, 
		IDeckRepository deckRepository,
		IDialogService dialogService) : ViewModel(messageBoxService), ITransientDependency
	{
		public ObservableCollection<Deck> Decks { get; set; } = [];

		public override async Task InitializeViewModelAsync()
		{
			Caption = "DECKS";

			await MakeRepositoryRequestAsync(async () =>
			{
				var response = await deckRepository.GetDecksAsync();

				if (response.IsFail)
				{
					MessageBoxService.Error(response.ErrorMessage);
					return;
				}

				Decks.AddRange(response.Data);
			});
		}

		#region Команды

		/// <summary>
		/// Начать урок
		/// </summary>
		public ICommand? StartLessonCommand => new LambdaCommand(() =>
		{
			navigationService.NavigateTo(nameof(SimulatorPage));
		});

		/// <summary>
		/// Добавить новую колоду
		/// </summary>
		public ICommand? AddDeckCommand => new LambdaCommand(async () =>
		{
			var result = await dialogService.ShowAddDeckDialogAsync();

			if (result != null)
			{
				MessageBoxService.Information(result);
			}
			else
			{
				MessageBoxService.Information("Cancel");
			}
		});

		/// <summary>
		/// Перейти на страницу редактирования колоды
		/// </summary>
		public ICommand? EditDeckCommand => new LambdaCommand(() =>
		{
			navigationService.NavigateTo(nameof(EditPage));
		});

		/// <summary>
		/// Удалить выбранную колоду
		/// </summary>
		public ICommand? DeleteDeckCommand => new LambdaCommand(async () =>
		{
			var result = await dialogService.ShowDialogAsync();

			if (result == true)
			{
				MessageBoxService.Information("True");
			}
			else
			{
				MessageBoxService.Information("False");
			}
		});

		#endregion
	}
}
