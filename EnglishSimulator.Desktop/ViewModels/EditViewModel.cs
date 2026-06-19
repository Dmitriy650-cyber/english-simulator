namespace EnglishSimulator.Desktop.ViewModels
{
	public class EditViewModel(
		IMessageBoxService messageBoxService,
		IMessageBusService messageBusService,
		INavigationService navigationService,
		IDialogService dialogService) : ViewModel(messageBoxService), IDisposable, ITransientDependency
	{
		private IDisposable? _subscribeToDeckViewModelMessage;

		public ObservableCollection<Sentence> Sentences { get; set; } = [];

		#region Свойства

		/// <summary>
		/// Текущая колода
		/// </summary>
		public Deck? Deck
		{
			get => field;
			set => Set(ref field, value);
		}
		
		/// <summary>
		/// Выбранное предложение
		/// </summary>
		public Sentence? SelectedSentence
		{
			get => field;
			set => Set(ref field, value);
		}

		#endregion

		public override async Task InitializeViewModelAsync()
		{
			Caption = "EDITOR";

			_subscribeToDeckViewModelMessage = messageBusService
				.RegisterHandler<DeckViewModelToEditViewModelMessage>(ReceiveDeckViewModelMessage);
		}

		#region Команды

		/// <summary>
		/// Вернуться на страницу DeckPage
		/// </summary>
		public ICommand? GoBackCommand => new LambdaCommand(() =>
		{
			navigationService.NavigateTo(nameof(DeckPage));
		});

		/// <summary>
		/// Добавить предложение
		/// </summary>
		public ICommand? AddSentenceCommand => new LambdaCommand(() =>
		{
			var result = dialogService.ShowSentenceDialogAsync(null);
		});

		/// <summary>
		/// Редактировать предложение
		/// </summary>
		public ICommand? EditSentenceCommand => new LambdaCommand(() =>
		{
			var result = dialogService.ShowSentenceDialogAsync(SelectedSentence!);
		}, () => SelectedSentence is not null);

		/// <summary>
		/// Удалить предложение
		/// </summary>
		public ICommand? DeleteSentenceCommand => new LambdaCommand(() =>
		{
			var result = dialogService.ShowDialogAsync();
		}, () => SelectedSentence is not null);

		#endregion

		#region Сообщения

		/// <summary>
		/// Получить сообщение от DeckViewModel
		/// </summary>
		/// <param name="message"></param>
		private void ReceiveDeckViewModelMessage(DeckViewModelToEditViewModelMessage message)
		{
			Deck = message.Deck;
			Sentences.AddRange(Deck.Sentences);
		} 

		#endregion

		public void Dispose()
		{
			_subscribeToDeckViewModelMessage?.Dispose();
		}
	}
}
