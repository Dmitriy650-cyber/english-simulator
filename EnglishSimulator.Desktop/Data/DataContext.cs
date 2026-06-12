namespace EnglishSimulator.Desktop.Data
{
    public class DataContext : DbContext
    {
		public DbSet<Sentence> Sentences => Set<Sentence>();
		public DbSet<Deck> Decks => Set<Deck>();
		public DbSet<RepetitionInterval> RepetitionIntervals => Set<RepetitionInterval>();

		public DataContext()
		{
			Database.EnsureCreated();
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			var dbPath = Path.Combine(Directory.GetCurrentDirectory(), "database.db");
			optionsBuilder.UseSqlite($"Data Source={dbPath}");
		}
    }
}
