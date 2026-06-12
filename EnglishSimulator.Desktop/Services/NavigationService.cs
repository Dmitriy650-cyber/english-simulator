namespace EnglishSimulator.Desktop.Services
{
	public class NavigationService(MainViewModel mainViewModel, IServiceProvider serviceProvider) : INavigationService, ISingletonDependency
	{
		public void NavigateTo(string pageName)
		{
			switch (pageName)
			{
				case nameof(DeckPage):
					mainViewModel.CurrentChildPage = serviceProvider.GetRequiredService<DeckViewModel>();
					break;
				case nameof(EditPage):
					mainViewModel.CurrentChildPage = serviceProvider.GetRequiredService<EditViewModel>();
					break;
				case nameof(DeckSettingsPage):
					mainViewModel.CurrentChildPage = serviceProvider.GetRequiredService<DeckSettingsViewModel>();
					break;
				case nameof(SimulatorPage):
					mainViewModel.CurrentChildPage = serviceProvider.GetRequiredService<SimulatorViewModel>();
					break;
				case nameof(HelpPage):
					mainViewModel.CurrentChildPage = serviceProvider.GetRequiredService<HelpViewModel>();
					break;
				case nameof(CongratulationsPage):
					mainViewModel.CurrentChildPage = serviceProvider.GetRequiredService<CongratulationsViewModel>();
					break;
			}
		}

		public void NavigateTo<TViewModel>() where TViewModel : ViewModel
		{
			var viewModel = serviceProvider.GetRequiredService<TViewModel>();
			mainViewModel.CurrentChildPage = viewModel;
		}
	}
}
