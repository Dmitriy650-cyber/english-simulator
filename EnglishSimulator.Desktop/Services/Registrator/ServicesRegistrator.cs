namespace EnglishSimulator.Desktop.Services.Registrator
{
	internal static class ServicesRegistrator
	{
		public static IServiceCollection AddServices(this IServiceCollection services) => services
			.AddSingleton<IMessageBoxService, MessageBoxService>()
			.AddSingleton<IMessageBusService, MessageBusService>()
			;
	}
}
