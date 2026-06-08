namespace EnglishSimulator.Desktop.ViewModels.Registrator
{
	internal static class ViewModelsRegistrator
	{
		public static IServiceCollection AddViewModels(this IServiceCollection services) => services
			.AddSingleton<MainViewModel>()
			.AddSingleton<DeckViewModel>()
			.AddTransient<EditViewModel>()
			.AddTransient<DeckSettingsViewModel>()
			.AddTransient<SimulatorViewModel>()
			.AddSingleton<HelpViewModel>()
			.AddSingleton<CongratulationsViewModel>()
			;
	}
}
