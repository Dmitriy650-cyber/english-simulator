namespace EnglishSimulator.Desktop.ViewModels
{
	public class DeckViewModel(IMessageBoxService messageBoxService, INavigationService navigationService, IDeckRepository deckRepository) : ViewModel(messageBoxService), ITransientDependency
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

		public ICommand? StartLessonCommand => new LambdaCommand(() =>
		{
			navigationService.NavigateTo(nameof(SimulatorPage));
		});
	}
}
