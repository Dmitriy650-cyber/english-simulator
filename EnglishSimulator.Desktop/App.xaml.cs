namespace EnglishSimulator.Desktop
{
	public partial class App : Application
	{
		public static Window? FocusedWindow => Current.Windows.Cast<Window>().FirstOrDefault(n => n.IsFocused);
		public static Window? ActivedWindow => Current.Windows.Cast<Window>().FirstOrDefault(n => n.IsActive);

		private static IHost? _host;
		public static IHost Host => _host ??=
			Program.CreateHostBuilder(Environment.GetCommandLineArgs()).Build();

		public static IServiceProvider Services => Host.Services;

		public static void ConfigureServices(HostBuilderContext host, IServiceCollection services) => services
			.Scan(scan => scan
				.FromApplicationDependencies()
				.AddClasses(classes => classes.AssignableTo<ISingletonDependency>())
				.AsSelf()
				.AsImplementedInterfaces()
				.WithSingletonLifetime()
				.AddClasses(classes => classes.AssignableTo<ITransientDependency>())
				.AsSelf()
				.WithTransientLifetime())
			.AddDbContext<DataContext>(options => options.UseSqlite("Data Source=./Data/database.db"));

		protected override async void OnStartup(StartupEventArgs e)
		{
			var host = Host;

			base.OnStartup(e);

			await host.StartAsync();
		}

		protected override async void OnExit(ExitEventArgs e)
		{
			base.OnExit(e);

			using (Host) await Host.StopAsync();
		}
	}
}
