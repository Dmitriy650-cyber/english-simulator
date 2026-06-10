namespace EnglishSimulator.Desktop.ViewModels.Registrator
{
	internal static class ViewModelsRegistrator
	{
		public static IServiceCollection AddViewModels(this IServiceCollection services) => services
			.AddSingleton<MainViewModel>()
			.AddTransient<DeckViewModel>()
			.AddTransient<EditViewModel>()
			.AddTransient<DeckSettingsViewModel>()
			.AddTransient<SimulatorViewModel>()
			.AddTransient<HelpViewModel>()
			.AddTransient<CongratulationsViewModel>()
			;
	}
}
