using EnglishSimulator.Desktop.Models;
using EnglishSimulator.Desktop.Services;

namespace EnglishSimulator.Desktop.ViewModels
{
	public class DeckViewModel(
		IMessageBoxService messageBoxService, 
		INavigationService navigationService, 
		IDeckRepository deckRepository,
		IDialogService dialogService) : ViewModel(messageBoxService), ITransientDependency
	{
		public ObservableCollection<DeckModel> Decks { get; set; } = [];

		/// <summary>
		/// Выбранная колода
		/// </summary>
		public DeckModel? SelectedDeck
		{
			get => field;
			set => Set(ref field, value);
		}

		public override async Task InitializeViewModelAsync()
		{
			Caption = "DECKS";

			await GetNewDecksAsync();
		}

		private async Task GetNewDecksAsync()
		{
			if (Decks.Count > 0)
				Decks.Clear();

			await MakeRepositoryRequestAsync(async () =>
			{
				var response = await deckRepository.GetDecksAsync();

				if (response.IsFail)
				{
					MessageBoxService.Error(response.ErrorMessage);
					return;
				}

				foreach (var deck in response.Data)
				{
					var deckModel = new DeckModel();
					deckModel.Deck = deck;
					deckModel.CountNewSentences = deck.Sentences.Where(n => n.State == nameof(SentenceState.New) && n.RepeatDate < DateTime.Now).Count();
					deckModel.CountLearnSentences = deck.Sentences.Where(n => n.State == nameof(SentenceState.Learn) && n.RepeatDate < DateTime.Now).Count();
					deckModel.CountDueSentences = deck.Sentences.Where(n => n.State == nameof(SentenceState.Due) && n.RepeatDate < DateTime.Now).Count();
					Decks.Add(deckModel);
				}
			});
		}

		#region Команды

		/// <summary>
		/// Перейти на страницу помощи
		/// </summary>
		public ICommand? GoToHelpPageCommand => new LambdaCommand(() =>
		{
			navigationService.NavigateTo(nameof(HelpPage), null!);
		});

		/// <summary>
		/// Начать урок
		/// </summary>
		public ICommand? StartLessonCommand => new LambdaCommand(async () =>
		{
			if (SelectedDeck!.CountNewSentences == 0
				&& SelectedDeck.CountDueSentences == 0
				&& SelectedDeck.CountLearnSentences == 0)
			{
				var result = await dialogService.ShowDialogAsync("There are no available sentences for training at the moment. Repeat mode will be enabled. Do you agree?");

				if (result == false)
					return;
			}

			navigationService.NavigateTo(nameof(SimulatorPage), SelectedDeck.Deck!);
		}, () => SelectedDeck is not null);

		/// <summary>
		/// Добавить новую колоду
		/// </summary>
		public ICommand? AddDeckCommand => new LambdaCommand(async () =>
		{
			var result = await dialogService.ShowAddDeckDialogAsync("ENTER DECK NAME");

			if (result != null)
			{
				var response = await deckRepository.CreateOrUpdateDeckAsync(new Deck
				{
					Name = result
				});
				
				if (response.IsFail)
				{
					MessageBoxService.Error(response.ErrorMessage);
					return;
				}

				await GetNewDecksAsync();
			}
		});

		/// <summary>
		/// Перейти на страницу редактирования колоды
		/// </summary>
		public ICommand? EditDeckCommand => new LambdaCommand(() =>
		{
			navigationService.NavigateTo(nameof(EditPage), SelectedDeck!.Deck!);
		}, () => SelectedDeck is not null);

		/// <summary>
		/// Удалить выбранную колоду
		/// </summary>
		public ICommand? DeleteDeckCommand => new LambdaCommand(async () =>
		{
			var result = await dialogService.ShowDialogAsync();

			if (result == true)
			{
				var response = await deckRepository.DeleteDeckAsync(SelectedDeck!.Deck!.Id);

				if (response.IsFail)
				{
					MessageBoxService.Error(response.ErrorMessage);
					return;
				}

				await GetNewDecksAsync();
			}
		}, () => SelectedDeck is not null);

		#endregion
	}
}
