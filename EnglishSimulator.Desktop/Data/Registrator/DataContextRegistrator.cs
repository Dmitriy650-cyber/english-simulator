namespace EnglishSimulator.Desktop.Data.Registrator
{
    public static class DataContextRegistrator
    {
		public static IServiceCollection AddDataContext(this IServiceCollection services) => services
			.AddDbContext<DataContext>(options => options.UseSqlite("Data Source=./Data/database.db"));
	}
}
