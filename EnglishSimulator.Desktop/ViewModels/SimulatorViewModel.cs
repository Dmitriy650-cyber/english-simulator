namespace EnglishSimulator.Desktop.ViewModels
{
	public class SimulatorViewModel(
		IMessageBoxService messageBoxService,
		IMessageBusService messageBusService) : ViewModel(messageBoxService), IDisposable, ITransientDependency
	{
		private IDisposable? _subscribeToDeckViewModelMessage;

		#region Свойства

		/// <summary>
		/// Текущая колода
		/// </summary>
		public Deck? Deck
		{
			get => field;
			set => Set(ref field, value);
		}

		#endregion

		public override async Task InitializeViewModelAsync()
		{
			Caption = "SIMULATOR";

			_subscribeToDeckViewModelMessage = messageBusService
				.RegisterHandler<DeckViewModelToSimularViewModelMessage>(ReceiveDeckViewModelMessage);
		}

		#region Сообщения

		/// <summary>
		/// Получить сообщение от DeckViewModel
		/// </summary>
		/// <param name="message"></param>
		private void ReceiveDeckViewModelMessage(DeckViewModelToSimularViewModelMessage message)
		{
			Deck = message.Deck;
		} 

		#endregion

		public void Dispose()
		{
			_subscribeToDeckViewModelMessage?.Dispose();
		}
	}
}
