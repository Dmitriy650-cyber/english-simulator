namespace EnglishSimulator.Desktop.Services
{
	public class NavigationService(IServiceProvider serviceProvider) : INavigationService, ISingletonDependency
	{
		public void NavigateTo(string pageName, object inputData)
		{
			var mainViewModel = serviceProvider.GetRequiredService<MainViewModel>();
			ViewModel viewModel;

			switch (pageName)
			{
				case nameof(DeckPage):
					viewModel = serviceProvider.GetRequiredService<DeckViewModel>();
					viewModel.InputData = inputData;
					mainViewModel.CurrentChildPage = viewModel;
					break;
				case nameof(EditPage):
					viewModel = serviceProvider.GetRequiredService<EditViewModel>();
					viewModel.InputData = inputData;
					mainViewModel.CurrentChildPage = viewModel;
					break;
				case nameof(DeckSettingsPage):
					viewModel = serviceProvider.GetRequiredService<DeckSettingsViewModel>();
					viewModel.InputData = inputData;
					mainViewModel.CurrentChildPage = viewModel;
					break;
				case nameof(SimulatorPage):
					viewModel = serviceProvider.GetRequiredService<SimulatorViewModel>();
					viewModel.InputData = inputData;
					mainViewModel.CurrentChildPage = viewModel;
					break;
				case nameof(HelpPage):
					viewModel = serviceProvider.GetRequiredService<HelpViewModel>();
					viewModel.InputData = inputData;
					mainViewModel.CurrentChildPage = viewModel;
					break;
				case nameof(CongratulationsPage):
					viewModel = serviceProvider.GetRequiredService<CongratulationsViewModel>();
					viewModel.InputData = inputData;
					mainViewModel.CurrentChildPage = viewModel;
					break;
			}
		}

		public void NavigateTo<TViewModel>() where TViewModel : ViewModel
		{
			var mainViewModel = serviceProvider.GetRequiredService<MainViewModel>();
			var viewModel = serviceProvider.GetRequiredService<TViewModel>();
			mainViewModel.CurrentChildPage = viewModel;
		}
	}
}
