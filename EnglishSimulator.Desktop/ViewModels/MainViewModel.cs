namespace EnglishSimulator.Desktop.ViewModels
{
    internal class MainViewModel : ViewModel
    {
		#region Свойства

		#region Заголовок страницы

		/// <summary>
		/// Заголовок страницы
		/// </summary>
		private string? _Caption;

		/// <summary>
		/// Заголовок страницы
		/// </summary>
		public string? Caption
		{
			get => _Caption;
			set => Set(ref _Caption, value);
		}

		#endregion

		#region Содержание страницы

		/// <summary>
		/// Содержание страницы
		/// </summary>
		private ViewModel? _CurrentChildPage;

		/// <summary>
		/// Содержание страницы
		/// </summary>
		public ViewModel? CurrentChildPage
		{
			get => _CurrentChildPage;
			set => Set(ref _CurrentChildPage, value);
		} 

		#endregion

		#endregion

		#region Команды

		#region Команда: показать страницу "Колоды"

		/// <summary>
		/// Команда: показать страницу "Колоды"
		/// </summary>
		private ICommand? _ShowDeckPageCommand;
        /// <summary>
        /// Команда: показать страницу "Колоды"
        /// </summary>
        public ICommand? ShowDeckPageCommand => _ShowDeckPageCommand ??=
            new LambdaCommand(OnShowDeckPageCommandExecuted);

        /// <summary>
        /// Показать страницу "Колоды"
        /// </summary>
        private void OnShowDeckPageCommandExecuted(object p)
        {
            CurrentChildPage = new DeckViewModel();
            Caption = "DECKS";
        }

		#endregion

		#region Команда: показать страницу "Редактор"

		/// <summary>
		/// Команда: показать страницу "Редактор"
		/// </summary>
		private ICommand? _ShowEditPageCommand;
		/// <summary>
		/// Команда: показать страницу "Редактор"
		/// </summary>
		public ICommand? ShowEditPageCommand => _ShowEditPageCommand ??=
            new LambdaCommand(OnShowEditPageCommandExecuted);

		/// <summary>
		/// Показать страницу "Редактор"
		/// </summary>
		private void OnShowEditPageCommandExecuted(object p)
		{
            CurrentChildPage = new EditViewModel();
            Caption = "EDIT DECK";
		}

		#endregion

		#region Команда: показать страницу "Настройки колоды"

		/// <summary>
		/// Команда: показать страницу "Настройки колоды"
		/// </summary>
		private ICommand? _ShowDeckSettingsPageCommand;
		/// <summary>
		/// Команда: показать страницу "Настройки колоды"
		/// </summary>
		public ICommand? ShowDeckSettingsPageCommand => _ShowDeckSettingsPageCommand ??=
            new LambdaCommand(OnShowDeckSettingsPageCommandExecuted);

		/// <summary>
		/// Показать страницу "Настройки колоды"
		/// </summary>
		private void OnShowDeckSettingsPageCommandExecuted(object p)
        {
            CurrentChildPage = new DeckSettingsViewModel();
            Caption = "DECK SETTINGS";
        }

		#endregion

		#region Команда: показать страницу "Тренажер"

		/// <summary>
		/// Команда: показать страницу "Тренажер"
		/// </summary>
		private ICommand? _ShowSimulatorPageCommand;
		/// <summary>
		/// Команда: показать страницу "Тренажер"
		/// </summary>
		public ICommand? ShowSimulatorPageCommand => _ShowSimulatorPageCommand ??=
            new LambdaCommand(OnShowSimulatorPageCommandExecuted);

		/// <summary>
		/// Показать страницу "Тренажер"
		/// </summary>
		/// <param name="p"></param>
		private void OnShowSimulatorPageCommandExecuted(object p)
        {
            CurrentChildPage = new SimulatorViewModel();
            Caption = "SIMULATOR";
        }

		#endregion

		#region Команда: показать страницу "Помощь"

		/// <summary>
		/// Команда: показать страницу "Помощь"
		/// </summary>
		private ICommand? _ShowHelpPageCommand;
		/// <summary>
		/// Команда: показать страницу "Помощь"
		/// </summary>
		public ICommand? ShowHelpPageCommand => _ShowHelpPageCommand ??=
            new LambdaCommand(OnShowHelpPageCommandExecuted);

		/// <summary>
		/// Показать страницу "Помощь"
		/// </summary>
		/// <param name="p"></param>
		private void OnShowHelpPageCommandExecuted(object p)
        {
            CurrentChildPage = new HelpViewModel();
            Caption = "HELP";
        }

		#endregion

		#region Команда: показать страницу "Поздравления"

		/// <summary>
		/// Команда: показать страницу "Поздравления"
		/// </summary>
		private ICommand? _ShowCongratulationsPageCommand;
		/// <summary>
		/// Команда: показать страницу "Поздравления"
		/// </summary>
		public ICommand? ShowCongratulationsPageCommand => _ShowCongratulationsPageCommand ??=
			new LambdaCommand(OnShowCongratulationsPageCommandExecuted);

		/// <summary>
		/// Показать страницу "Поздравления"
		/// </summary>
		private void OnShowCongratulationsPageCommandExecuted()
		{
			CurrentChildPage = new CongratulationsViewModel();
			Caption = "CONGRATULATIONS";
		}

		#endregion

		#endregion

		public MainViewModel()
		{
			//OnShowDeckPageCommandExecuted(null!);
			OnShowEditPageCommandExecuted(null!);
		}
	}
}
