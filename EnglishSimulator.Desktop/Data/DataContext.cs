namespace EnglishSimulator.Desktop.Data
{
    public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
    {
        public DbSet<Sentence> Sentences { get; set; }
        public DbSet<Deck> Decks { get; set; }
        public DbSet<RepetitionInterval> RepetitionIntervals { get; set; }
    }
}
