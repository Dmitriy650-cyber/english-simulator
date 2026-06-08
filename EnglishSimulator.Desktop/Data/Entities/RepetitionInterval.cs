namespace EnglishSimulator.Desktop.Data.Entities
{
    public class RepetitionInterval
    {
        public int Id { get; set; }
        public int Stage { get; set; }
        public int CountDays { get; set; }

        public virtual Deck? Deck { get; set; }
        public int DeckId { get; set; }
    }
}
